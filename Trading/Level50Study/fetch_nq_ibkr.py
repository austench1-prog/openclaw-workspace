#!/usr/bin/env python3
"""
Fetch NQ SEP26 1-minute historical bars from IBKR TWS API.
Connects to TWS on 192.168.0.226:7497 (paper trading port).
Outputs: raw_nq_1m.csv
"""

import time
import csv
import threading
from datetime import datetime, timezone
from ibapi.client import EClient
from ibapi.wrapper import EWrapper
from ibapi.contract import Contract

HOST = "192.168.0.226"
PORT = 7497
CLIENT_ID = 42  # arbitrary, must not conflict with active sessions

OUTPUT = "/Users/austinai/.openclaw/workspace/Trading/Level50Study/results/raw_nq_1m.csv"

class IBApp(EWrapper, EClient):
    def __init__(self):
        EClient.__init__(self, self)
        self.bars = []
        self.done = threading.Event()
        self.error_msg = None

    def error(self, reqId, errorCode, errorString, advancedOrderRejectJson=""):
        if errorCode in (2104, 2106, 2158, 2119):
            # Info-level messages, not errors
            print(f"[INFO {errorCode}] {errorString}")
        else:
            print(f"[ERROR reqId={reqId} code={errorCode}] {errorString}")
            if errorCode in (162, 321, 200):
                self.error_msg = f"{errorCode}: {errorString}"
                self.done.set()

    def historicalData(self, reqId, bar):
        self.bars.append({
            "datetime": bar.date,
            "open":     bar.open,
            "high":     bar.high,
            "low":      bar.low,
            "close":    bar.close,
            "volume":   bar.volume,
        })

    def historicalDataEnd(self, reqId, start, end):
        print(f"[OK] Data received: {len(self.bars)} bars ({start} → {end})")
        self.done.set()

    def nextValidId(self, orderId):
        print(f"[CONNECTED] nextValidId={orderId}")
        self.request_data()

    def request_data(self):
        contract = Contract()
        contract.symbol   = "NQ"
        contract.secType  = "FUT"
        contract.exchange = "CME"
        contract.currency = "USD"
        contract.lastTradeDateOrContractMonth = "202609"  # NQ SEP26 (month only, let IBKR resolve exact date)

        # Request last 5 trading days of 1-min bars, regular trading hours only = 0 (include extended)
        self.reqHistoricalData(
            reqId=1,
            contract=contract,
            endDateTime="",           # now
            durationStr="5 D",
            barSizeSetting="1 min",
            whatToShow="TRADES",
            useRTH=0,                 # include extended hours
            formatDate=1,             # human-readable timestamps
            keepUpToDate=False,
            chartOptions=[]
        )
        print("[REQUESTING] NQ SEP26 1-min bars, last 5 trading days...")


def main():
    import os
    os.makedirs("/Users/austinai/.openclaw/workspace/Trading/Level50Study/results", exist_ok=True)

    app = IBApp()
    app.connect(HOST, PORT, CLIENT_ID)

    # Run message loop in background thread
    thread = threading.Thread(target=app.run, daemon=True)
    thread.start()

    # Wait for data (max 60 seconds)
    got_data = app.done.wait(timeout=60)

    app.disconnect()

    if not got_data or app.error_msg:
        print(f"[FAILED] {app.error_msg or 'Timeout waiting for data'}")
        return 1

    if not app.bars:
        print("[FAILED] No bars received")
        return 1

    # Write CSV
    with open(OUTPUT, "w", newline="") as f:
        writer = csv.DictWriter(f, fieldnames=["datetime","open","high","low","close","volume"])
        writer.writeheader()
        writer.writerows(app.bars)

    # Summary
    print(f"\n{'='*60}")
    print(f"Output:     {OUTPUT}")
    print(f"Rows:       {len(app.bars)}")
    print(f"Contract:   NQ SEP26 (20260919) CME")
    print(f"First bar:  {app.bars[0]['datetime']}")
    print(f"Last bar:   {app.bars[-1]['datetime']}")
    print(f"Timezone:   US/Eastern (IBKR default for US futures)")
    print(f"\nFirst 5 bars:")
    for b in app.bars[:5]:
        print(f"  {b['datetime']}  O={b['open']}  H={b['high']}  L={b['low']}  C={b['close']}  V={b['volume']}")
    print(f"\nLast 5 bars:")
    for b in app.bars[-5:]:
        print(f"  {b['datetime']}  O={b['open']}  H={b['high']}  L={b['low']}  C={b['close']}  V={b['volume']}")
    return 0

if __name__ == "__main__":
    exit(main())
