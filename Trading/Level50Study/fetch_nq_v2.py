#!/usr/bin/env python3
"""
Fetch NQ SEP26 (NQU6) 1-minute bars from IBKR.
Contract identified by conId=770561204 (NQU6, lastTrade=20260918, CME).
Covers: 2026-06-23 to 2026-06-25 only (validation period).
Output: raw_nq_1m_v2_NQU6.csv
"""

import threading
import csv
import os
from ibapi.client import EClient
from ibapi.wrapper import EWrapper
from ibapi.contract import Contract

HOST = "192.168.0.226"
PORT = 7497
CLIENT_ID = 44

OUTPUT = "/Users/austinai/.openclaw/workspace/Trading/Level50Study/results/raw_nq_1m_v4_NQU6_MIDPOINT.csv"

class IBApp(EWrapper, EClient):
    def __init__(self):
        EClient.__init__(self, self)
        self.bars = []
        self.done = threading.Event()
        self.error_msg = None

    def error(self, reqId, errorCode, errorString, advancedOrderRejectJson=""):
        if errorCode in (2104, 2106, 2158, 2119):
            pass
        else:
            print(f"[ERROR {errorCode}] {errorString}")
            if errorCode in (162, 321, 200, 354):
                self.error_msg = f"{errorCode}: {errorString}"
                self.done.set()

    def historicalData(self, reqId, bar):
        self.bars.append({
            "datetime": bar.date,
            "open":  bar.open,
            "high":  bar.high,
            "low":   bar.low,
            "close": bar.close,
            "volume": bar.volume,
        })

    def historicalDataEnd(self, reqId, start, end):
        print(f"[OK] {len(self.bars)} bars received ({start} -> {end})")
        self.done.set()

    def nextValidId(self, orderId):
        contract = Contract()
        contract.conId    = 770561204   # NQU6 — NQ SEP26, expires 20260918
        contract.exchange = "CME"

        # Request only 2026-06-25 ET close (covers 6/24 + 6/25)
        # endDateTime = "20260625 21:00:00 US/Eastern" covers full 6/25 session
        self.reqHistoricalData(
            reqId=1,
            contract=contract,
            endDateTime="20260626-00:00:00",  # UTC midnight ending covers 6/24 00:00 ET through 6/25 20:00 ET
            durationStr="3 D",
            barSizeSetting="1 min",
            whatToShow="MIDPOINT",  # NT8 uses 'Last' = MIDPOINT in IBKR API for futures
            useRTH=0,
            formatDate=1,    # 1 = IBKR formatted string in ET
            keepUpToDate=False,
            chartOptions=[]
        )
        print("[REQUESTING] NQU6 conId=770561204, 2D ending 2026-06-25 21:00 ET, 1-min TRADES...")

def main():
    os.makedirs(os.path.dirname(OUTPUT), exist_ok=True)
    app = IBApp()
    app.connect(HOST, PORT, CLIENT_ID)
    t = threading.Thread(target=app.run, daemon=True)
    t.start()
    app.done.wait(timeout=60)
    app.disconnect()

    if app.error_msg:
        print(f"[FAILED] {app.error_msg}")
        return 1
    if not app.bars:
        print("[FAILED] No bars received")
        return 1

    # Convert epoch to ET datetime string
    from datetime import datetime, timezone, timedelta
    ET = timezone(timedelta(hours=-4))  # EDT (summer)

    rows = []
    for b in app.bars:
        # formatDate=2 gives epoch seconds as string
        try:
            epoch = int(b['datetime'])
            dt_et = datetime.fromtimestamp(epoch, tz=ET)
            dt_str = dt_et.strftime("%Y-%m-%d %H:%M:%S")
        except ValueError:
            dt_str = b['datetime']  # fallback if already formatted
        rows.append({
            "datetime_et": dt_str,
            "open":  b['open'],
            "high":  b['high'],
            "low":   b['low'],
            "close": b['close'],
            "volume": b['volume'],
        })

    with open(OUTPUT, "w", newline="") as f:
        w = csv.DictWriter(f, fieldnames=["datetime_et","open","high","low","close","volume"])
        w.writeheader()
        w.writerows(rows)

    size = os.path.getsize(OUTPUT)
    print(f"\n{'='*60}")
    print(f"Contract:    NQ SEP26 (NQU6), conId=770561204, exchange=CME")
    print(f"Output:      {OUTPUT}")
    print(f"File size:   {size:,} bytes")
    print(f"Row count:   {len(rows)}")
    print(f"Timezone:    US/Eastern (EDT, UTC-4)")
    print(f"First bar:   {rows[0]['datetime_et']}")
    print(f"Last bar:    {rows[-1]['datetime_et']}")

    # Validation bars
    targets = {
        "2026-06-24 10:02:00": "10:02 ET 6/24",
        "2026-06-24 11:17:00": "11:17 ET 6/24",
        "2026-06-24 13:46:00": "13:46 ET 6/24",
        "2026-06-25 09:41:00": "09:41 ET 6/25",
    }
    lookup = {r['datetime_et']: r for r in rows}

    print(f"\n{'='*60}")
    print("VALIDATION TABLE vs NT8 REFERENCE BARS")
    print(f"{'='*60}")
    nt8 = {
        "2026-06-24 10:02:00": dict(open=29652.00, high=29690.50, low=29646.00, close=29665.75, volume=1261),
        "2026-06-24 11:17:00": dict(open=29780.25, high=29814.25, low=29767.00, close=29803.25, volume=1089),
        "2026-06-24 13:46:00": dict(open=29410.75, high=29433.00, low=29397.75, close=29397.75, volume=1114),
        "2026-06-25 09:41:00": dict(open=29995.75, high=30010.50, low=29944.50, close=29946.75, volume=3188),
    }

    all_pass = True
    for ts, label in targets.items():
        if ts not in lookup:
            print(f"\n{ts} — NOT FOUND IN CSV")
            all_pass = False
            continue
        row = lookup[ts]
        ref  = nt8[ts]
        o_diff = float(row['open'])  - ref['open']
        h_diff = float(row['high'])  - ref['high']
        l_diff = float(row['low'])   - ref['low']
        c_diff = float(row['close']) - ref['close']
        v_diff = int(row['volume'])  - ref['volume']
        ok = all(abs(x) < 0.01 for x in [o_diff,h_diff,l_diff,c_diff])
        status = "PASS" if ok else "FAIL"
        if not ok:
            all_pass = False
        print(f"\n{ts} [{status}]")
        print(f"  Field    IBKR          NT8           Diff")
        print(f"  Open     {float(row['open']):>10.2f}    {ref['open']:>10.2f}    {o_diff:+.2f}")
        print(f"  High     {float(row['high']):>10.2f}    {ref['high']:>10.2f}    {h_diff:+.2f}")
        print(f"  Low      {float(row['low']):>10.2f}    {ref['low']:>10.2f}    {l_diff:+.2f}")
        print(f"  Close    {float(row['close']):>10.2f}    {ref['close']:>10.2f}    {c_diff:+.2f}")
        print(f"  Volume   {int(row['volume']):>10d}    {ref['volume']:>10d}    {v_diff:+d}")

    print(f"\n{'='*60}")
    if all_pass:
        print("FINAL STATUS: PASS — eligible to begin event study")
    else:
        print("FINAL STATUS: FAIL — event study remains blocked")
    print(f"{'='*60}")
    return 0

if __name__ == "__main__":
    exit(main())
