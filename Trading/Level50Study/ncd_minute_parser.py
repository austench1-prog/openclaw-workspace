#!/usr/bin/env python3
"""
NinjaTrader 8 NCD Minute Bar File Parser
Based on jrstokka/NinjaTraderNCDFiles NCDFile.cs
"""

import struct
import os
import sys
from datetime import datetime, timezone, timedelta

# .NET DateTime epoch: Jan 1, 0001 00:00:00
DOTNET_EPOCH = datetime(1, 1, 1, tzinfo=timezone.utc)
TICKS_PER_SECOND = 10_000_000

def dotnet_ticks_to_datetime(ticks):
    """Convert .NET DateTime ticks to Python datetime."""
    try:
        seconds = ticks / TICKS_PER_SECOND
        dt = DOTNET_EPOCH + timedelta(seconds=seconds)
        return dt
    except Exception:
        return None

def read_be_uint16(data, pos):
    return struct.unpack_from('>H', data, pos)[0], pos + 2

def read_be_uint32(data, pos):
    return struct.unpack_from('>I', data, pos)[0], pos + 4

def read_be_uint64(data, pos):
    return struct.unpack_from('>Q', data, pos)[0], pos + 8

def read_byte(data, pos):
    return data[pos], pos + 1

def parse_ncd_minute(filepath, header_size=40, max_bars=None):
    """
    Parse NCD minute bar file.
    Returns list of bars: (datetime, open, high, low, close, volume)
    """
    with open(filepath, 'rb') as f:
        raw = f.read()

    file_size = len(raw)
    pos = 0

    # --- Parse header ---
    if header_size == 40:
        if file_size < 40:
            return None, f"File too small ({file_size} bytes) for 40-byte header"
        tick_size_price  = struct.unpack_from('<d', raw, 0)[0]   # double LE
        tick_size_volume = struct.unpack_from('<d', raw, 8)[0]
        last_price       = struct.unpack_from('<d', raw, 16)[0]
        last_volume      = struct.unpack_from('<q', raw, 24)[0]  # long LE
        last_dt_ticks    = struct.unpack_from('<q', raw, 32)[0]
        pos = 40
    elif header_size == 0:
        tick_size_price = 0.25   # NQ tick
        tick_size_volume = 1.0
        last_price = 0.0
        last_volume = 0
        last_dt_ticks = 0
        pos = 0
    else:
        # Generic: skip header_size bytes, use defaults
        tick_size_price = 0.25
        tick_size_volume = 1.0
        last_price = 0.0
        last_volume = 0
        last_dt_ticks = 0
        pos = header_size

    last_close = last_price
    last_dt = last_dt_ticks

    bars = []
    errors = 0

    while pos < file_size - 1:
        if max_bars and len(bars) >= max_bars:
            break

        try:
            mask1 = raw[pos]; pos += 1
            mask2 = raw[pos]; pos += 1

            # --- TIME ---
            time_flag = mask1 & 0x03
            if time_flag == 0:      # NoTime
                time_delta = 0
            elif time_flag == 1:    # Time1: 1 byte
                b, pos = read_byte(raw, pos)
                time_delta = b
            elif time_flag == 2:    # Time2: 2 bytes BE
                v, pos = read_be_uint16(raw, pos)
                time_delta = v
            else:                   # Time4: 4 bytes BE
                v, pos = read_be_uint32(raw, pos)
                time_delta = v

            last_dt = last_dt + time_delta
            bar_dt = dotnet_ticks_to_datetime(last_dt)

            # --- OPEN ---
            open_flag = mask1 & 0x0C
            if open_flag == 0:
                bar_open = last_close
            elif open_flag == 4:    # Open1: 1 byte signed offset
                b, pos = read_byte(raw, pos)
                open_delta = b - 128
                bar_open = last_close + open_delta * tick_size_price
            elif open_flag == 8:    # Open2: 2 bytes BE
                v, pos = read_be_uint16(raw, pos)
                open_delta = v - 32768
                bar_open = last_close + open_delta * tick_size_price
            else:                   # Open4: 4 bytes BE
                v, pos = read_be_uint32(raw, pos)
                open_delta = v - 2147483648
                bar_open = last_close + open_delta * tick_size_price

            # --- VOLUME ---
            vol_flag = mask1 & 0xE0
            if vol_flag == 0:
                bar_volume = 0
            elif vol_flag == 32:    # Volume1
                b, pos = read_byte(raw, pos)
                bar_volume = b
            elif vol_flag == 64:    # Volume1X100
                b, pos = read_byte(raw, pos)
                bar_volume = b * 100
            elif vol_flag == 96:    # Volume1X500
                b, pos = read_byte(raw, pos)
                bar_volume = b * 500
            elif vol_flag == 128:   # Volume1X1000
                b, pos = read_byte(raw, pos)
                bar_volume = b * 1000
            elif vol_flag == 160:   # Volume2
                v, pos = read_be_uint16(raw, pos)
                bar_volume = v
            elif vol_flag == 192:   # Volume4
                v, pos = read_be_uint32(raw, pos)
                bar_volume = v
            else:                   # Volume8 (240)
                v, pos = read_be_uint64(raw, pos)
                bar_volume = v

            # --- CLOSE ---
            close_flag = mask2 & 0x03
            if close_flag == 0:
                bar_close = bar_open
            elif close_flag == 1:   # Close1
                b, pos = read_byte(raw, pos)
                close_delta = b - 128
                bar_close = bar_open + close_delta * tick_size_price
            elif close_flag == 2:   # Close2
                v, pos = read_be_uint16(raw, pos)
                close_delta = v - 32768
                bar_close = bar_open + close_delta * tick_size_price
            else:                   # Close4
                v, pos = read_be_uint32(raw, pos)
                close_delta = v - 2147483648
                bar_close = bar_open + close_delta * tick_size_price

            # --- HIGH ---
            high_flag = mask2 & 0x30
            if high_flag == 0:
                bar_high = bar_close
            elif high_flag == 16:   # High1
                b, pos = read_byte(raw, pos)
                bar_high = max(bar_open, bar_close) + b * tick_size_price
            elif high_flag == 32:   # High2
                v, pos = read_be_uint16(raw, pos)
                bar_high = max(bar_open, bar_close) + v * tick_size_price
            else:                   # High4
                v, pos = read_be_uint32(raw, pos)
                bar_high = max(bar_open, bar_close) + v * tick_size_price

            # --- LOW ---
            low_flag = mask2 & 0xC0
            if low_flag == 0:
                bar_low = bar_open
            elif low_flag == 64:    # Low1
                b, pos = read_byte(raw, pos)
                bar_low = min(bar_open, bar_close) - b * tick_size_price
            elif low_flag == 128:   # Low2
                v, pos = read_be_uint16(raw, pos)
                bar_low = min(bar_open, bar_close) - v * tick_size_price
            else:                   # Low4
                v, pos = read_be_uint32(raw, pos)
                bar_low = min(bar_open, bar_close) - v * tick_size_price

            last_close = bar_close

            bars.append((bar_dt, bar_open, bar_high, bar_low, bar_close, bar_volume))

        except (IndexError, struct.error) as e:
            errors += 1
            if errors > 5:
                break
            continue

    return bars, {
        'tick_size_price': tick_size_price,
        'tick_size_volume': tick_size_volume,
        'last_price': last_price,
        'last_volume': last_volume,
        'last_dt_ticks': last_dt_ticks,
        'header_size': header_size,
        'file_size': file_size,
        'errors': errors,
    }


def is_plausible_nq(bars, min_price=18000, max_price=23000):
    """Check if parsed bars look like NQ prices."""
    if not bars:
        return False, "No bars"
    valid = 0
    for dt, o, h, l, c, v in bars[:20]:
        if min_price <= o <= max_price and min_price <= c <= max_price:
            valid += 1
    pct = valid / min(len(bars), 20)
    return pct >= 0.5, f"{valid}/{min(len(bars),20)} bars in NQ range"


def main():
    filepath = '/Users/austinai/.openclaw/workspace/Trading/Level50Study/ncd_test/test_20260611.ncd'
    result_path = '/Users/austinai/.openclaw/workspace/Trading/Level50Study/ncd_test/parse_result.txt'

    if not os.path.exists(filepath):
        print(f"ERROR: File not found: {filepath}")
        sys.exit(1)

    file_size = os.path.getsize(filepath)
    lines = []
    lines.append(f"NCD Minute Bar Parser — NQ 06-26 2026-06-11")
    lines.append(f"File: {filepath}")
    lines.append(f"File size: {file_size} bytes")
    lines.append("")

    # Try all header sizes
    header_sizes = [0, 8, 16, 24, 32, 40]
    best_result = None
    best_header = None

    lines.append("=" * 60)
    lines.append("PROBING HEADER SIZES")
    lines.append("=" * 60)

    for hs in header_sizes:
        bars, info = parse_ncd_minute(filepath, header_size=hs, max_bars=200)
        if bars is None:
            lines.append(f"Header={hs}: FAILED — {info}")
            continue

        plausible, reason = is_plausible_nq(bars)
        first_bar = bars[0] if bars else None
        first_price = f"{first_bar[1]:.2f}" if first_bar else "N/A"
        lines.append(f"Header={hs:2d}: {len(bars):4d} bars, first_open={first_price:>10s}, NQ plausible={plausible} ({reason})")

        if plausible and best_result is None:
            best_result = bars
            best_header = hs
            best_info = info

    lines.append("")

    if best_result is None:
        # Fall back to first working result with most bars
        lines.append("No header size gave plausible NQ prices. Using header=40 for detailed output.")
        bars, info = parse_ncd_minute(filepath, header_size=40, max_bars=None)
        if bars:
            best_result = bars
            best_header = 40
            best_info = info

    if best_result is None:
        lines.append("CRITICAL: Could not parse any bars from file.")
    else:
        lines.append("=" * 60)
        lines.append(f"BEST RESULT: header_size={best_header}")
        lines.append("=" * 60)
        lines.append(f"Total bars parsed: {len(best_result)}")
        if isinstance(best_info, dict):
            lines.append(f"tick_size_price:  {best_info['tick_size_price']}")
            lines.append(f"tick_size_volume: {best_info['tick_size_volume']}")
            lines.append(f"initial_price:    {best_info['last_price']}")
            lines.append(f"initial_volume:   {best_info['last_volume']}")
            ticks = best_info['last_dt_ticks']
            if ticks > 0:
                dt = dotnet_ticks_to_datetime(ticks)
                lines.append(f"initial_datetime: {dt} (ticks={ticks})")
        lines.append("")

        # First 20 bars as CSV
        lines.append("FIRST 20 BARS (CSV):")
        lines.append("datetime_utc,open,high,low,close,volume")
        for bar in best_result[:20]:
            dt, o, h, l, c, v = bar
            dt_str = dt.strftime('%Y-%m-%d %H:%M:%S') if dt else 'N/A'
            lines.append(f"{dt_str},{o:.2f},{h:.2f},{l:.2f},{c:.2f},{v}")

        lines.append("")
        lines.append("FIRST 5 BARS (DETAILED):")
        for i, bar in enumerate(best_result[:5], 1):
            dt, o, h, l, c, v = bar
            dt_str = dt.strftime('%Y-%m-%d %H:%M:%S UTC') if dt else 'N/A'
            lines.append(f"  Bar {i}: {dt_str}")
            lines.append(f"         O={o:.2f} H={h:.2f} L={l:.2f} C={c:.2f} V={v}")

        # Also do full parse for bar count
        lines.append("")
        lines.append("FULL FILE PARSE (all bars):")
        all_bars, _ = parse_ncd_minute(filepath, header_size=best_header, max_bars=None)
        if all_bars:
            lines.append(f"  Total bars: {len(all_bars)}")
            dt_first, o_first, *_ = all_bars[0]
            dt_last, *_, c_last, v_last = all_bars[-1]
            lines.append(f"  First bar: {dt_first.strftime('%Y-%m-%d %H:%M') if dt_first else 'N/A'} open={o_first:.2f}")
            dt_last_bar, o_last, h_last, l_last, c_last2, v_last2 = all_bars[-1]
            lines.append(f"  Last bar:  {dt_last_bar.strftime('%Y-%m-%d %H:%M') if dt_last_bar else 'N/A'} close={c_last2:.2f}")

    # Write result
    result_text = "\n".join(lines)
    with open(result_path, 'w') as f:
        f.write(result_text)
        f.write("\n")

    print(result_text)
    print(f"\nResults saved to: {result_path}")


if __name__ == '__main__':
    main()
