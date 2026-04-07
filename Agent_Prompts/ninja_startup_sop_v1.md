# 忍者开机 SOP (Ninja Startup SOP)
# Source: Dragon (based on Chairman specification)
# Version: 1.0 | Date: 2026-04-07
# CRITICAL: All 5 checkpoints must pass before any order is placed

---

## 5-Point Pre-Trade Checklist

### Checkpoint 1: Prop Firm Connection
- Open NinjaTrader → Connections
- Confirm target prop firm account is connected (Green status)
- Confirm account: **APEX-165583-123** (or target account) shows as Active
- ❌ If disconnected → reconnect before proceeding

### Checkpoint 2: Replikanto Leader Account
- Open Replikanto control panel
- Confirm **Leader Account = Sim101** (or designated leader)
- Confirm leader account status is Active and connected
- ❌ If wrong account → change leader before proceeding

### Checkpoint 3: Replikanto Cross Order (品种对齐)
- In Replikanto, confirm **Cross Order is checked** for target instrument
- Confirm the correct instrument mapping is selected:
  - NQ → MNQ (for Apex)
  - ES → MES (if applicable)
- Confirm **Apex account is checked as Follower** (green light)
- ❌ If Cross Order not selected → select before proceeding

### Checkpoint 4: Chart Account Alignment
- Go to Chart/Chart Trader
- Confirm **Account = Sim101** (Leader Account, NOT Apex directly)
- Confirm chart is showing the **correct instrument** for this session
- Confirm DragonFileSignal Strategy is **Enabled** on this chart
- ❌ If wrong account or instrument → fix before proceeding

### Checkpoint 5: Full Alignment Verification
Before ANY order:
```
Prop Firm connected?      ✅ / ❌
Leader Account = Sim101?  ✅ / ❌
Cross Order selected?     ✅ / ❌
Follower (Apex) active?   ✅ / ❌
Chart Account = Sim101?   ✅ / ❌
Chart instrument correct? ✅ / ❌
Strategy Enabled?         ✅ / ❌
```
**All 7 must be ✅ before any signal is sent.**

---

## Multi-Strategy / Multi-Account Rules

- Each Leader Account handles ONE instrument or ONE strategy
- Current setup: 3 Leader Accounts available (Sim101, SimNQ, SimFF)
- If trading multiple instruments simultaneously → use separate leader accounts
- Never mix instruments in same Replikanto panel

---

## Dragon's Self-Check Protocol

Before sending any signal, Dragon must verify:
1. SSH to Windows → check NinjaTrader process running
2. Signal Server responding
3. Send test ping → confirm response
4. Only THEN send trading signal

---

## Failure Modes Observed

| Failure | Cause | Fix |
|---|---|---|
| Order placed but no Apex fill | Cross Order not selected | Check Replikanto Cross Order |
| Strategy shows but no execution | DragonFileSignal not Enabled | Enable in Strategy Manager |
| Signal sent but no chart response | Wrong account on chart | Set chart account to Sim101 |
| FLATTEN_ALL doesn't work | Strategy not running | Re-enable strategy |

---

## Version Notes
V1.0: Initial version based on 2026-04-07 live testing experience
- Confirmed: Cross Order must be selected in Replikanto for MNQ fills
- Confirmed: Chart must show Sim101, not Apex directly

*Next version: Add automated pre-flight check script*
