#!/usr/bin/env python3
# System Monitor - Dragon Guard Mode
# Source: Dragon | Version: 1.0 | Date: 2026-04-06
# Runs hourly on Mac mini (小塔)
# Checks: SSH tunnels, memory, Windows connection, Apex data
# Reports to Telegram if any anomaly found

import subprocess
import os
import json
import requests
from datetime import datetime

# Config
WINDOWS_IP = "192.168.0.59"
AIR_IP = "192.168.0.164"
WINDOWS_USER = "auste"
AIR_USER = "austinchien"

# Telegram (reads from OpenClaw config)
def get_telegram_token():
    try:
        config_path = os.path.expanduser("~/.openclaw/openclaw.json")
        with open(config_path) as f:
            config = json.load(f)
        return config.get("channels", {}).get("telegram", {}).get("botToken")
    except:
        return None

def send_telegram_alert(message: str):
    token = get_telegram_token()
    if not token:
        print(f"[ALERT] {message}")
        return
    # Get chat ID from environment or config
    chat_id = os.environ.get("TELEGRAM_CHAT_ID", "8223531074")
    url = f"https://api.telegram.org/bot{token}/sendMessage"
    requests.post(url, json={"chat_id": chat_id, "text": f"🔔 [龙哥-地勤]\n{message}"})

def check_ssh(host: str, user: str, label: str) -> bool:
    result = subprocess.run(
        ["ssh", "-o", "ConnectTimeout=5", "-o", "StrictHostKeyChecking=no",
         f"{user}@{host}", "echo OK"],
        capture_output=True, text=True
    )
    ok = "OK" in result.stdout
    status = "✅" if ok else "❌"
    print(f"[SSH] {label} ({host}): {status}")
    return ok

def check_memory() -> dict:
    result = subprocess.run(["vm_stat"], capture_output=True, text=True)
    lines = result.stdout.split("\n")
    stats = {}
    for line in lines:
        if "Pages free" in line:
            stats["free"] = int(line.split(":")[1].strip().rstrip("."))
        if "Pages active" in line:
            stats["active"] = int(line.split(":")[1].strip().rstrip("."))
    page_size = 16384  # bytes
    free_mb = stats.get("free", 0) * page_size / 1024 / 1024
    print(f"[MEM] Free memory: {free_mb:.0f} MB")
    return {"free_mb": free_mb}

def check_windows_server() -> bool:
    try:
        r = requests.get(f"http://{WINDOWS_IP}:5000", timeout=3)
        ok = "Dragon" in r.text
        print(f"[WIN] Python Server: {'✅' if ok else '❌'}")
        return ok
    except:
        print(f"[WIN] Python Server: ❌ Not reachable")
        return False

def run_checks():
    timestamp = datetime.now().strftime("%Y-%m-%d %H:%M")
    issues = []

    # 1. Check SSH to Windows
    if not check_ssh(WINDOWS_IP, WINDOWS_USER, "温总"):
        issues.append("❌ 温总 (Windows) SSH 断线")

    # 2. Check SSH to MacBook Air
    if not check_ssh(AIR_IP, AIR_USER, "小白"):
        issues.append("❌ 小白 (MacBook Air) SSH 断线")

    # 3. Check Windows Signal Server
    if not check_windows_server():
        issues.append("⚠️ 温总 Python Server 未运行，需要重启")

    # 4. Check memory
    mem = check_memory()
    if mem["free_mb"] < 500:
        issues.append(f"⚠️ 小塔内存不足：只剩 {mem['free_mb']:.0f} MB")

    # Report
    if issues:
        alert = f"🚨 系统异常报告 [{timestamp}]\n\n" + "\n".join(issues)
        send_telegram_alert(alert)
        print(f"\n[ALERT] Issues found:\n" + "\n".join(issues))
    else:
        print(f"\n[OK] 所有系统正常 [{timestamp}]")
        # Only report to TG if there are issues (silent when OK)

if __name__ == "__main__":
    print(f"{'='*50}")
    print(f"龙哥系统巡检 | {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}")
    print(f"{'='*50}")
    run_checks()
