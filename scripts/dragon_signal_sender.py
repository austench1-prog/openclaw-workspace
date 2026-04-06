# Dragon Signal Sender
# Source: Dragon
# Version: 1.0 | Date: 2026-04-05
#
# Runs on Mac mini, sends trading signals to NinjaTrader on Windows PC
# Format: ACTION|SYMBOL|QTY
# Example: BUY|NQ|1  /  SELL|MNQ|2  /  CLOSE|ES|0

import requests
import json
from datetime import datetime

# Windows PC IP address on local network
# Find it on Windows: open CMD → type ipconfig → look for IPv4 Address
WINDOWS_PC_IP = "192.168.0.59"  # Windows PC (NinjaTrader)
NINJATRADE_PORT = 5000
BASE_URL = f"http://{WINDOWS_PC_IP}:{NINJATRADE_PORT}"

# Supported symbols
VALID_SYMBOLS = ["NQ", "MNQ", "ES", "MES", "GC", "MGC"]
VALID_ACTIONS = ["BUY", "SELL", "CLOSE"]


def send_signal(action: str, symbol: str, qty: int = 1) -> dict:
    """
    Send a trading signal to NinjaTrader.
    
    action: BUY / SELL / CLOSE
    symbol: NQ / MNQ / ES / MES / GC / MGC
    qty: number of contracts (use 0 for CLOSE)
    """
    action = action.upper()
    symbol = symbol.upper()
    
    # Validate
    if action not in VALID_ACTIONS:
        return {"status": "error", "message": f"Invalid action: {action}"}
    if symbol not in VALID_SYMBOLS:
        return {"status": "error", "message": f"Invalid symbol: {symbol}"}
    
    # Build signal
    signal = f"{action}|{symbol}|{qty}"
    timestamp = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    
    print(f"[{timestamp}] Sending signal: {signal}")
    
    try:
        response = requests.post(
            BASE_URL,
            data=signal,
            timeout=5
        )
        result = response.text.strip()
        print(f"[{timestamp}] Response: {result}")
        
        return {
            "status": "ok" if result.startswith("OK") else "error",
            "signal": signal,
            "response": result,
            "timestamp": timestamp
        }
    except requests.exceptions.ConnectionError:
        msg = f"Cannot connect to NinjaTrader at {WINDOWS_PC_IP}:{NINJATRADE_PORT}"
        print(f"[ERROR] {msg}")
        return {"status": "error", "message": msg}
    except Exception as e:
        print(f"[ERROR] {e}")
        return {"status": "error", "message": str(e)}


def emergency_flatten_all():
    """
    EMERGENCY: Flatten all positions immediately.
    Call this when you need to stop everything NOW.
    """
    timestamp = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    print(f"[{timestamp}] ⚠️ EMERGENCY FLATTEN ALL")
    try:
        response = requests.post(BASE_URL, data="FLATTEN_ALL", timeout=5)
        result = response.text.strip()
        print(f"[{timestamp}] Response: {result}")
        return {"status": "ok", "action": "FLATTEN_ALL", "response": result}
    except Exception as e:
        print(f"[EMERGENCY ERROR] {e}")
        return {"status": "error", "message": str(e)}


def close_all():
    """Close all positions gracefully"""
    timestamp = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    print(f"[{timestamp}] CLOSE ALL positions")
    try:
        response = requests.post(BASE_URL, data="CLOSE_ALL", timeout=5)
        result = response.text.strip()
        print(f"[{timestamp}] Response: {result}")
        return {"status": "ok", "action": "CLOSE_ALL", "response": result}
    except Exception as e:
        print(f"[ERROR] {e}")
        return {"status": "error", "message": str(e)}


def test_connection():
    """Test if NinjaTrader is reachable"""
    print(f"Testing connection to {WINDOWS_PC_IP}:{NINJATRADE_PORT}...")
    try:
        response = requests.get(BASE_URL, timeout=3)
        print("NinjaTrader is reachable")
        return True
    except:
        print(f"Cannot reach NinjaTrader at {WINDOWS_PC_IP}:{NINJATRADE_PORT}")
        print("Check: 1) Windows firewall allows port 5000  2) IP address is correct")
        return False


# --- Examples ---
if __name__ == "__main__":
    # Step 1: Find Windows PC IP
    # On Windows: open CMD → ipconfig → look for IPv4 Address
    # Update WINDOWS_PC_IP above
    
    # Step 2: Test connection
    if test_connection():
        # Step 3: Send test signal (1 MNQ on demo)
        result = send_signal("BUY", "MNQ", 1)
        print(json.dumps(result, indent=2))
