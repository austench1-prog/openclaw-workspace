# Tradovate Connection and Order Script
# Source: Dragon
# Version: 0.1 | Date: 2026-04-05
# Environment: Demo first, then Live
# Supported instruments: NQ, MNQ, ES, MES, GC (Gold)

import requests
import json
import os
from datetime import datetime

# --- CONFIG ---
# Set these as environment variables, never hardcode
TRADOVATE_CLIENT_ID = os.environ.get("TRADOVATE_CLIENT_ID", "")
TRADOVATE_CLIENT_SECRET = os.environ.get("TRADOVATE_CLIENT_SECRET", "")
TRADOVATE_USERNAME = os.environ.get("TRADOVATE_USERNAME", "")
TRADOVATE_PASSWORD = os.environ.get("TRADOVATE_PASSWORD", "")

# Demo or Live
USE_DEMO = True  # Set to False when ready for live

BASE_URL = "https://demo.tradovateapi.com/v1" if USE_DEMO else "https://live.tradovateapi.com/v1"

# Instrument map
# Month codes: H=Mar, M=Jun, U=Sep, Z=Dec — update as needed
INSTRUMENTS = {
    "NQ":  "NQM5",   # Nasdaq 100 Futures
    "MNQ": "MNQM5",  # Micro Nasdaq 100
    "ES":  "ESM5",   # S&P 500 Futures
    "MES": "MESM5",  # Micro S&P 500
    "GC":  "GCM5",   # Gold Futures
    "MGC": "MGCM5",  # Micro Gold Futures
}

# --- AUTH ---
def get_access_token():
    url = f"{BASE_URL}/auth/accesstokenrequest"
    payload = {
        "name": TRADOVATE_USERNAME,
        "password": TRADOVATE_PASSWORD,
        "appId": "TradingBot",
        "appVersion": "1.0",
        "cid": TRADOVATE_CLIENT_ID,
        "sec": TRADOVATE_CLIENT_SECRET,
        "deviceId": "mac-mini-dragon"
    }
    response = requests.post(url, json=payload)
    data = response.json()
    if "accessToken" in data:
        print(f"[AUTH] Connected to Tradovate {'DEMO' if USE_DEMO else 'LIVE'}")
        return data["accessToken"]
    else:
        print(f"[AUTH ERROR] {data}")
        return None

# --- ACCOUNT ---
def get_accounts(token):
    url = f"{BASE_URL}/account/list"
    headers = {"Authorization": f"Bearer {token}"}
    response = requests.get(url, headers=headers)
    accounts = response.json()
    print(f"[ACCOUNTS] Found {len(accounts)} account(s)")
    for acc in accounts:
        print(f"  - {acc.get('name')} | Balance: {acc.get('cashBalance', 'N/A')}")
    return accounts

# --- MARKET DATA ---
def get_contract_id(token, symbol):
    url = f"{BASE_URL}/contract/find?name={symbol}"
    headers = {"Authorization": f"Bearer {token}"}
    response = requests.get(url, headers=headers)
    data = response.json()
    if "id" in data:
        print(f"[CONTRACT] {symbol} → ID: {data['id']}")
        return data["id"]
    else:
        print(f"[CONTRACT ERROR] {symbol} not found: {data}")
        return None

# --- PLACE ORDER ---
def place_order(token, account_id, contract_id, action, qty=1, order_type="Market"):
    """
    action: "Buy" or "Sell"
    order_type: "Market" or "Limit"
    """
    url = f"{BASE_URL}/order/placeorder"
    headers = {"Authorization": f"Bearer {token}"}
    payload = {
        "accountSpec": str(account_id),
        "accountId": account_id,
        "action": action,
        "symbol": contract_id,
        "orderQty": qty,
        "orderType": order_type,
        "isAutomated": True
    }
    response = requests.post(url, json=payload, headers=headers)
    result = response.json()
    print(f"[ORDER] {action} {qty}x contract {contract_id} → {result}")
    return result

# --- MAIN TEST ---
def run_connection_test():
    print("=" * 50)
    print(f"Tradovate Connection Test | {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}")
    print(f"Environment: {'DEMO' if USE_DEMO else 'LIVE'}")
    print("=" * 50)

    # Step 1: Authenticate
    token = get_access_token()
    if not token:
        print("[FAIL] Authentication failed. Check credentials.")
        return

    # Step 2: Get accounts
    accounts = get_accounts(token)
    if not accounts:
        print("[FAIL] No accounts found.")
        return

    account_id = accounts[0]["id"]
    print(f"[INFO] Using account ID: {account_id}")

    # Step 3: Test contract lookup
    for name, symbol in INSTRUMENTS.items():
        contract_id = get_contract_id(token, symbol)

    print("\n[TEST COMPLETE] Connection successful. Ready for order placement.")
    print("To place a test order, call place_order() with appropriate parameters.")
    print("WARNING: Always test on DEMO first.")

if __name__ == "__main__":
    # Check credentials are set
    if not TRADOVATE_CLIENT_ID or not TRADOVATE_USERNAME:
        print("[ERROR] Missing credentials.")
        print("Set environment variables:")
        print("  export TRADOVATE_CLIENT_ID=your_client_id")
        print("  export TRADOVATE_CLIENT_SECRET=your_secret")
        print("  export TRADOVATE_USERNAME=your_email")
        print("  export TRADOVATE_PASSWORD=your_password")
    else:
        run_connection_test()
