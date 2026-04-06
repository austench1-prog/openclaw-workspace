# 2026-OPS-001: AnyDesk System Architecture SOP
# Source: Dragon
# Issued by: Austin Chien (Chairman)
# Executor: Dragon (OpenClaw / Mac mini)
# Version: 1.0 | Date: 2026-04-05

---

## I. AnyDesk Role in This System

AnyDesk = Remote Visual Operations Interface (ONLY)

Dragon uses AnyDesk to gain graphical control of the Windows environment,
bypassing physical isolation and protocol limitations,
for NON-TRADING system-level tasks only.

---

## II. Trigger Scenarios (3 Principles)

### 1. System Deployment & Configuration
- Installing NinjaTrader 8, Replikanto, Python, etc on Windows
- Modifying Windows registry, firewall settings
- When file-sharing method (Plan B) is not available or has failed

### 2. Troubleshooting & Visual Confirmation
- Strategy errors or unexpected NinjaTrader popups
- Account connection interruptions (Disconnected status)
- Dragon remotely identifies exact error codes and resolves

### 3. Cross-Device Sync & File Audit
- Extracting trade logs or CSV data from Windows
- Dragon acts as relay: retrieves file, forwards to Chairman's Telegram

---

## III. Hard Redlines (NEVER VIOLATE)

- NO live trade execution via AnyDesk simulated clicks (high risk, high latency)
- NO persistent connection: connect for task, disconnect immediately after
- NO modification of AnyDesk unattended access password

---

## IV. Three-Device Coordination

| Device | Dragon's Role | AnyDesk Task |
|---|---|---|
| Mac mini (Core) | Command Center | Initiates connection, executes visual ops |
| Windows PC (Controlled) | Execution End | Silent background, unattended access ON |
| MacBook Air (Monitor) | Observer | Chairman monitors Dragon's progress |

---

## V. Standard Operating Procedure

1. Heartbeat Check: Ping Windows IP to confirm online
2. Establish Link: Launch Mac AnyDesk, login to Windows with preset password
3. Visual Alignment: Identify target window (e.g. NT8 position)
4. Execute Task: Complete installation or configuration
5. Status Report: Screenshot Windows desktop → send to Chairman Telegram → disconnect

---

## VI. Current Priority Task

**Goal:** Fix NinjaTrader HTTP Listener (port 5000 not responding)
**Alternative approach being evaluated:** File-based signal via OneDrive shared folder

---

*This document is the binding operational instruction for all AnyDesk usage.*
