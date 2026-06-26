#!/usr/bin/env python3
"""
NinjaTrader 8 NCD Minute Bar File Parser
Based on reverse-engineered format from jrstokka/NinjaTraderNCDFiles NCDFile.cs
with empirical corrections from binary analysis.

KEY FINDINGS (2026-06-25):
- Header is NOT 40 bytes as documented. Records start at offset 35.
- Doubles in the header use a SWAPPED 32-bit word endian format:
  read as two LE uint32s, then combine as (high32 << 32) | low32.
- Multi-byte open/close deltas use SIGNED int16/int32 (NOT val - 32768).
- High/Low deltas use UNSIGNED uint16/uint32 (distance from max/min).
- NoTime mask means advance 1 minute; Time1/2/4 encode gap size in minutes.
- lastDateTime stored as swapped int64 at offset 24.
- Records start at byte offset 35.

Header layout (35 bytes):
  [0-7]:  int64 LE = 1 (version/file type indicator)
  [8-15]: swapped_double = tickSizePrice (e.g. 0.25 for NQ)
  [16-23]: swapped_double = lastPrice (reference price for first bar's open delta)
  [24-31]: swapped_int64 = lastDateTime (.NET ticks, 100ns units)
  [32-34]: 3 unknown bytes (possibly lastVolume prefix)
  [35...]: variable-length bar records
"""

import struct
import os
import sys
from datetime import datetime, timezone, timedelta

DOTNET_EPOCH = datetime(1, 1, 1, tzinfo=timezone.utc)
TICKS_PER_SECOND = 10_000_000
TICKS_PER_MINUTE = 600_000_000  # .NET ticks per minute


def dotnet_ticks_to_datetime(ticks):
    """Convert .NET DateTime ticks to Python datetime (UTC)."""
    if 0 < ticks < 9.5e18:
        try:
            return DOTNET_EPOCH + timedelta(seconds=ticks / TICKS_PER_SECOND)
        except Exception:
            return None
    return None


def read_swapped_double(data, pos):
    """Read a double stored as two LE uint32s with words swapped: (high32_LE)(low32_LE)."""
    high = struct.unpack('<I', data[pos:pos+4])[0]
    low  = struct.unpack('<I', data[pos+4:pos+8])[0]
    return struct.unpack('<d', struct.pack('<II', low, high))[0]


def read_swapped_int64(data, pos):
    """Read int64 stored as: high32(LE) then low32(LE), returning (high<<32)|low."""
    high = struct.unpack('<I', data[pos:pos+4])[0]
    low  = struct.unpack('<I', data[pos+4:pos+8])[0]
    return (high << 32) | low


def parse_ncd_minute(filepath, max_bars=None):
    """
    Parse a NinjaTrader 8 NCD minute bar file.
    
    Returns:
        (bars, info_dict) where bars is list of (datetime, open, high, low, close, volume)
        and info_dict contains header metadata.
    """
    with open(filepath, 'rb') as f:
        raw = f.read()

    file_size = len(raw)

    # --- Parse header (35 bytes) ---
    if file_size < 35:
        return [], {'error': f'File too small: {file_size} bytes'}

    version        = struct.unpack('<q', raw[0:8])[0]      # int64 LE
    tick_size_price = read_swapped_double(raw, 8)           # swapped double
    last_price     = read_swapped_double(raw, 16)           # swapped double
    last_dt_ticks  = read_swapped_int64(raw, 24)            # swapped int64

    info = {
        'file_size': file_size,
        'header_size': 35,
        'version': version,
        'tick_size_price': tick_size_price,
        'last_price': last_price,
        'last_dt_ticks': last_dt_ticks,
        'last_datetime': dotnet_ticks_to_datetime(last_dt_ticks),
        'errors': 0,
    }

    pos = 35
    last_close = last_price
    last_dt = last_dt_ticks
    bars = []

    while pos < file_size - 1:
        if max_bars and len(bars) >= max_bars:
            break

        if pos + 2 > file_size:
            break

        try:
            mask1 = raw[pos]; pos += 1
            mask2 = raw[pos]; pos += 1

            time_flag  = mask1 & 0x03   # bits 0-1
            open_flag  = mask1 & 0x0C   # bits 2-3
            vol_flag   = mask1 & 0xE0   # bits 5-7
            close_flag = mask2 & 0x03   # bits 0-1
            high_flag  = mask2 & 0x30   # bits 4-5
            low_flag   = mask2 & 0xC0   # bits 6-7

            # --- TIME ---
            # NoTime(0) = advance 1 minute
            # Time1/2/4 = advance N minutes (gap encoding)
            if time_flag == 0:
                last_dt += TICKS_PER_MINUTE
            elif time_flag == 1:
                td = raw[pos]; pos += 1
                last_dt += td * TICKS_PER_MINUTE
            elif time_flag == 2:
                td = struct.unpack_from('>H', raw, pos)[0]; pos += 2
                last_dt += td * TICKS_PER_MINUTE
            else:  # time_flag == 3
                td = struct.unpack_from('>I', raw, pos)[0]; pos += 4
                last_dt += td * TICKS_PER_MINUTE

            bar_dt = dotnet_ticks_to_datetime(last_dt)

            # --- OPEN (signed delta from lastClose) ---
            if open_flag == 0:
                bar_open = last_close
            elif open_flag == 4:    # Open1: 1 byte signed (bias 128)
                b = raw[pos]; pos += 1
                bar_open = last_close + (b - 128) * tick_size_price
            elif open_flag == 8:    # Open2: 2 bytes BE signed int16
                v = struct.unpack_from('>h', raw, pos)[0]; pos += 2
                bar_open = last_close + v * tick_size_price
            else:                   # Open4: 4 bytes BE signed int32
                v = struct.unpack_from('>i', raw, pos)[0]; pos += 4
                bar_open = last_close + v * tick_size_price

            # --- VOLUME ---
            vol = 0
            if vol_flag == 0:
                pass
            elif vol_flag == 32:    # Volume1: 1 byte
                vol = raw[pos]; pos += 1
            elif vol_flag == 64:    # Volume1X100: 1 byte * 100
                vol = raw[pos] * 100; pos += 1
            elif vol_flag == 96:    # Volume1X500: 1 byte * 500
                vol = raw[pos] * 500; pos += 1
            elif vol_flag == 128:   # Volume1X1000: 1 byte * 1000
                vol = raw[pos] * 1000; pos += 1
            elif vol_flag == 160:   # Volume2: 2 bytes BE unsigned
                vol = struct.unpack_from('>H', raw, pos)[0]; pos += 2
            elif vol_flag == 192:   # Volume4: 4 bytes BE unsigned
                vol = struct.unpack_from('>I', raw, pos)[0]; pos += 4
            else:                   # Volume8: 8 bytes BE unsigned (240)
                vol = struct.unpack_from('>Q', raw, pos)[0]; pos += 8

            # --- CLOSE (signed delta from open) ---
            if close_flag == 0:
                bar_close = bar_open
            elif close_flag == 1:   # Close1: 1 byte signed (bias 128)
                b = raw[pos]; pos += 1
                bar_close = bar_open + (b - 128) * tick_size_price
            elif close_flag == 2:   # Close2: 2 bytes BE signed int16
                v = struct.unpack_from('>h', raw, pos)[0]; pos += 2
                bar_close = bar_open + v * tick_size_price
            else:                   # Close4: 4 bytes BE signed int32
                v = struct.unpack_from('>i', raw, pos)[0]; pos += 4
                bar_close = bar_open + v * tick_size_price

            # --- HIGH (unsigned delta above max(open, close)) ---
            ref_high = max(bar_open, bar_close)
            if high_flag == 0:
                bar_high = bar_close
            elif high_flag == 16:   # High1: 1 byte unsigned
                b = raw[pos]; pos += 1
                bar_high = ref_high + b * tick_size_price
            elif high_flag == 32:   # High2: 2 bytes BE unsigned
                v = struct.unpack_from('>H', raw, pos)[0]; pos += 2
                bar_high = ref_high + v * tick_size_price
            else:                   # High4: 4 bytes BE unsigned
                v = struct.unpack_from('>I', raw, pos)[0]; pos += 4
                bar_high = ref_high + v * tick_size_price

            # --- LOW (unsigned delta below min(open, close)) ---
            ref_low = min(bar_open, bar_close)
            if low_flag == 0:
                bar_low = bar_open
            elif low_flag == 64:    # Low1: 1 byte unsigned
                b = raw[pos]; pos += 1
                bar_low = ref_low - b * tick_size_price
            elif low_flag == 128:   # Low2: 2 bytes BE unsigned
                v = struct.unpack_from('>H', raw, pos)[0]; pos += 2
                bar_low = ref_low - v * tick_size_price
            else:                   # Low4: 4 bytes BE unsigned
                v = struct.unpack_from('>I', raw, pos)[0]; pos += 4
                bar_low = ref_low - v * tick_size_price

            last_close = bar_close
            bars.append((bar_dt, bar_open, bar_high, bar_low, bar_close, vol))

        except (IndexError, struct.error) as e:
            info['errors'] += 1
            if info['errors'] > 10:
                break
            continue

    return bars, info


def is_plausible_nq(bars, min_price=15000, max_price=40000):
    """Check if parsed bars look like NQ futures prices."""
    if not bars:
        return False, "No bars"
    valid = sum(1 for dt, o, h, l, c, v in bars[:50]
                if min_price <= o <= max_price and min_price <= c <= max_price)
    pct = valid / min(len(bars), 50)
    return pct >= 0.5, f"{valid}/{min(len(bars),50)} bars in NQ range ({min_price}-{max_price})"


def main():
    filepath = '/Users/austinai/.openclaw/workspace/Trading/Level50Study/ncd_test/test_20260611.ncd'
    result_path = '/Users/austinai/.openclaw/workspace/Trading/Level50Study/ncd_test/parse_result.txt'

    if not os.path.exists(filepath):
        print(f"ERROR: File not found: {filepath}")
        sys.exit(1)

    bars, info = parse_ncd_minute(filepath)

    lines = []
    lines.append("=" * 70)
    lines.append("NCD Minute Bar Parser — NQ 06-26 2026-06-11")
    lines.append("=" * 70)
    lines.append(f"File: {filepath}")
    lines.append(f"File size: {info['file_size']} bytes")
    lines.append("")

    lines.append("=== HEADER ===")
    lines.append(f"Header size:     {info['header_size']} bytes (records start at offset 35)")
    lines.append(f"Version/type:    {info['version']}")
    lines.append(f"tick_size_price: {info['tick_size_price']} (swapped-double at offset 8)")
    lines.append(f"last_price:      {info['last_price']:.4f} (swapped-double at offset 16)")
    lines.append(f"last_dt_ticks:   {info['last_dt_ticks']}")
    lines.append(f"last_datetime:   {info['last_datetime']} UTC (swapped-int64 at offset 24)")
    lines.append(f"Parse errors:    {info['errors']}")
    lines.append("")

    lines.append("=== PARSE RESULTS ===")
    lines.append(f"Total bars parsed: {len(bars)}")

    if bars:
        first_dt = bars[0][0]
        last_dt = bars[-1][0]
        lines.append(f"First bar datetime: {first_dt} UTC")
        lines.append(f"Last bar datetime:  {last_dt} UTC")

        opens = [b[1] for b in bars]
        closes = [b[4] for b in bars]
        highs = [b[2] for b in bars]
        lows = [b[3] for b in bars]
        volumes = [b[5] for b in bars]

        plausible_count = sum(1 for c in closes if 15000 <= c <= 40000)
        lines.append(f"Plausible NQ bars (15000-40000): {plausible_count}/{len(bars)}")
        lines.append(f"Open range:  {min(opens):.2f} to {max(opens):.2f}")
        lines.append(f"Close range: {min(closes):.2f} to {max(closes):.2f}")
        lines.append(f"Volume range: {min(volumes)} to {max(volumes)}")
        lines.append("")

        # Check first 5 bars specifically
        first5_plausible = all(15000 <= b[4] <= 40000 for b in bars[:5])
        lines.append(f"First 5 bars all plausible: {first5_plausible}")
        lines.append("")

        lines.append("=== FIRST 20 BARS (CSV) ===")
        lines.append("datetime_utc,open,high,low,close,volume")
        for dt, o, h, l, c, v in bars[:20]:
            dt_str = dt.strftime('%Y-%m-%d %H:%M:%S') if dt else 'N/A'
            lines.append(f"{dt_str},{o:.2f},{h:.2f},{l:.2f},{c:.2f},{v}")

        lines.append("")
        lines.append("=== FIRST 5 BARS (DETAILED) ===")
        for i, (dt, o, h, l, c, v) in enumerate(bars[:5], 1):
            dt_str = dt.strftime('%Y-%m-%d %H:%M:%S UTC') if dt else 'N/A'
            plausible = 15000 <= o <= 40000 and 15000 <= c <= 40000
            lines.append(f"Bar {i}: {dt_str}")
            lines.append(f"        O={o:.2f} H={h:.2f} L={l:.2f} C={c:.2f} V={v}")
            lines.append(f"        NQ plausible: {plausible}")

    lines.append("")
    lines.append("=== FORMAT NOTES ===")
    lines.append("Header encoding uses 'swapped word' format:")
    lines.append("  - Doubles: read as two LE uint32, combine as (high32<<32)|low32 then as double")
    lines.append("  - DateTime: same swapped int64 format")
    lines.append("Record format (variable length, per bar):")
    lines.append("  mask1: [bits 0-1]=time_flag, [bits 2-3]=open_size, [bits 5-7]=vol_flag")
    lines.append("  mask2: [bits 0-1]=close_size, [bits 4-5]=high_size, [bits 6-7]=low_size")
    lines.append("  Time: NoTime(0)=+1min, Time1/2/4=+N min gap")
    lines.append("  Open/Close: signed int8/16/32 delta (bias-128 for 1-byte)")
    lines.append("  High/Low: unsigned distance from max/min(open,close)")

    result_text = "\n".join(lines)

    with open(result_path, 'w') as f:
        f.write(result_text)
        f.write("\n")

    print(result_text)
    print(f"\nResults saved to: {result_path}")


if __name__ == '__main__':
    main()
