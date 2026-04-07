#!/usr/bin/env python3
# wake_win.py - Wake 温总 (Windows PC)
# Run from Mac mini: python3 wake_win.py

import socket
import time
import subprocess

WIN_MAC = "90:10:57:d3:4e:83"
WIN_IP = "192.168.0.59"
BROADCAST = "192.168.0.255"
WIN_USER = "auste"

def send_magic_packet(mac, broadcast="192.168.0.255"):
    mac_bytes = bytes.fromhex(mac.replace(":", "").replace("-", ""))
    magic = b'\xff' * 6 + mac_bytes * 16
    with socket.socket(socket.AF_INET, socket.SOCK_DGRAM) as s:
        s.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
        s.sendto(magic, (broadcast, 9))
    print(f"[WOL] Magic Packet sent to {mac}")

def wait_for_win(timeout=60):
    print(f"[SSH] Waiting for 温总 (max {timeout}s)...")
    for i in range(timeout // 5):
        time.sleep(5)
        result = subprocess.run(
            ["ssh", "-o", "ConnectTimeout=3", "-o", "StrictHostKeyChecking=no",
             f"{WIN_USER}@{WIN_IP}", "echo ONLINE"],
            capture_output=True, text=True
        )
        if "ONLINE" in result.stdout:
            print(f"[SSH] 温总 online after {(i+1)*5}s")
            return True
        print(f"[SSH] Waiting... ({(i+1)*5}s)")
    return False

if __name__ == "__main__":
    print("🐉 唤醒温总 (Windows PC)")
    send_magic_packet(WIN_MAC, BROADCAST)
    if wait_for_win():
        print("✅ 温总已唤醒")
    else:
        print("❌ 温总唤醒失败，请手动开机")
