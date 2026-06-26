#!/usr/bin/env python3
"""Check what contract conId=770561204 actually resolves to."""
import threading
from ibapi.client import EClient
from ibapi.wrapper import EWrapper
from ibapi.contract import Contract

HOST = "192.168.0.226"
PORT = 7497
CLIENT_ID = 45

class IBApp(EWrapper, EClient):
    def __init__(self):
        EClient.__init__(self, self)
        self.done = threading.Event()

    def error(self, reqId, errorCode, errorString, advancedOrderRejectJson=""):
        if errorCode not in (2104, 2106, 2158, 2119):
            print(f"[ERROR {errorCode}] {errorString}")
            self.done.set()

    def contractDetails(self, reqId, contractDetails):
        c = contractDetails.contract
        cd = contractDetails
        print(f"conId:           {c.conId}")
        print(f"symbol:          {c.symbol}")
        print(f"secType:         {c.secType}")
        print(f"lastTrade:       {c.lastTradeDateOrContractMonth}")
        print(f"localSymbol:     {c.localSymbol}")
        print(f"tradingClass:    {c.tradingClass}")
        print(f"exchange:        {c.exchange}")
        print(f"currency:        {c.currency}")
        print(f"multiplier:      {c.multiplier}")
        print(f"longName:        {cd.longName}")
        print(f"minTick:         {cd.minTick}")
        print(f"priceMagnifier:  {cd.priceMagnifier}")

    def contractDetailsEnd(self, reqId):
        self.done.set()

    def nextValidId(self, orderId):
        c = Contract()
        c.conId = 770561204
        c.exchange = "CME"
        self.reqContractDetails(1, c)

def main():
    app = IBApp()
    app.connect(HOST, PORT, CLIENT_ID)
    t = threading.Thread(target=app.run, daemon=True)
    t.start()
    app.done.wait(timeout=15)
    app.disconnect()

if __name__ == "__main__":
    main()
