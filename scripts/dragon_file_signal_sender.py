# Dragon File Signal Sender
# Source: Dragon
# Version: 1.0 | Date: 2026-04-05
#
# Runs on Mac mini, writes signal files to Windows shared folder
# NinjaTrader strategy on Windows reads the file and executes orders

import os
import time
import json
from datetime import datetime

# Windows shared folder path (mount on Mac via Finder)
# After Windows sharing is set up:
# Finder → Go → Connect to Server → smb://192.168.0.59/DragonSignals
# Then mount point will be something like /Volumes/DragonSignals/
SHARED_FOLDER = "/Volumes/DragonSignals"
SIGNAL_FILE = "signal.txt"

VALID_SYMBOLS = ["NQ", "MNQ", "ES", "MES", "GC", "MGC"]
VALID_ACTIONS = ["BUY", "SELL", "CLOSE"]


def check_mount():
    """Check if Windows shared folder is mounted"""
    if os.path.exists(SHARED_FOLDER):
        print(f"[OK] Shared folder mounted at {SHARED_FOLDER}")
        return True
    else:
        print(f"[ERROR] Shared folder not mounted at {SHARED_FOLDER}")
        print("To mount:")
        print("  1. Open Finder")
        print("  2. Go → Connect to Server")
        print(f"  3. Enter: smb://192.168.0.59/DragonSignals")
        print("  4. Enter Windows credentials if prompted")
        return False


def send_signal(action: str, symbol: str, qty: int = 1) -> dict:
    """
    Write a trading signal to the shared folder.
    NinjaTrader will pick it up and execute.
    
    action: BUY / SELL / CLOSE
    symbol: NQ / MNQ / ES / MES / GC / MGC
    qty: number of contracts
    """
    action = action.upper()
    symbol = symbol.upper()
    
    if action not in VALID_ACTIONS:
        return {"status": "error", "message": f"Invalid action: {action}"}
    if symbol not in VALID_SYMBOLS:
        return {"status": "error", "message": f"Invalid symbol: {symbol}"}
    if not check_mount():
        return {"status": "error", "message": "Shared folder not mounted"}
    
    signal = f"{action}|{symbol}|{qty}"
    timestamp = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    signal_path = os.path.join(SHARED_FOLDER, SIGNAL_FILE)
    
    try:
        with open(signal_path, 'w') as f:
            f.write(signal)
        
        print(f"[{timestamp}] Signal sent: {signal}")
        
        # Wait up to 3 seconds for NinjaTrader to process
        done_path = os.path.join(SHARED_FOLDER, "signal_done.txt")
        for i in range(6):
            time.sleep(0.5)
            if os.path.exists(done_path):
                done_content = open(done_path).read()
                print(f"[{timestamp}] Confirmed executed: {done_content}")
                return {"status": "ok", "signal": signal, "confirmed": True}
        
        print(f"[{timestamp}] Signal written, waiting for NinjaTrader confirmation...")
        return {"status": "ok", "signal": signal, "confirmed": False}
        
    except Exception as e:
        print(f"[ERROR] {e}")
        return {"status": "error", "message": str(e)}


if __name__ == "__main__":
    print("Dragon File Signal Sender")
    print("=" * 40)
    
    if check_mount():
        print("\nTest: Sending BUY MNQ 1 signal...")
        result = send_signal("BUY", "MNQ", 1)
        print(json.dumps(result, indent=2))
    else:
        print("\nMount the shared folder first, then run again.")
