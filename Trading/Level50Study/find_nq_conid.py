#!/usr/bin/env python3
"""
Find the exact conId for NQ SEP26 futures on CME.
Prints all matching contracts so we can identify the correct one.
"""

import threading
from ibapi.client import EClient
from ibapi.wrapper import EWrapper
from ibapi.contract import Contract

HOST = "192.168.0.226"
PORT = 7497
CLIENT_ID = 43

class IBApp(EWrapper, EClient):
    def __init__(self):
        EClient.__init__(self, self)
        self.done = threading.Event()
        self.contracts = []

    def error(self, reqId, errorCode, errorString, advancedOrderRejectJson=""):
        if errorCode in (2104, 2106, 2158, 2119):
            pass
        else:
            print(f"[ERROR {errorCode}] {errorString}")
            if errorCode in (200, 321, 162):
                self.done.set()

    def contractDetails(self, reqId, contractDetails):
        c = contractDetails.contract
        self.contracts.append({
            "conId":      c.conId,
            "symbol":     c.symbol,
            "secType":    c.secType,
            "lastTrade":  c.lastTradeDateOrContractMonth,
            "exchange":   c.exchange,
            "currency":   c.currency,
            "localSymbol": c.localSymbol,
            "tradingClass": c.tradingClass,
        })

    def contractDetailsEnd(self, reqId):
        print(f"[DONE] Found {len(self.contracts)} contracts")
        self.done.set()

    def nextValidId(self, orderId):
        contract = Contract()
        contract.symbol   = "NQ"
        contract.secType  = "FUT"
        contract.exchange = "CME"
        contract.currency = "USD"
        # Request all NQ futures to see what's available
        self.reqContractDetails(1, contract)
        print("[REQUESTING] Contract details for NQ FUT CME...")

def main():
    app = IBApp()
    app.connect(HOST, PORT, CLIENT_ID)
    t = threading.Thread(target=app.run, daemon=True)
    t.start()
    app.done.wait(timeout=30)
    app.disconnect()

    print(f"\n{'ConId':<12} {'Symbol':<6} {'LastTrade':<12} {'LocalSymbol':<12} {'TradingClass'}")
    print("-"*60)
    for c in sorted(app.contracts, key=lambda x: x['lastTrade']):
        print(f"{c['conId']:<12} {c['symbol']:<6} {c['lastTrade']:<12} {c['localSymbol']:<12} {c['tradingClass']}")

if __name__ == "__main__":
    main()
