# MATS v1 Scope Lock
# Version: 1.0 | Date: 2026-04-08
# Status: LOCKED - No scope changes without Chairman approval

---

## What v1 Does

### Four Core Modules

| Module | Role | Status |
|---|---|---|
| A: Prop Intelligence Agent | Collect raw platform rules and info | In progress |
| B: NotebookLM Compliance Skill | Verify rule accuracy from official sources | ✅ Live |
| C: Strategy Pack | Evaluate setup maturity for one strategy | Pending strategy input |
| D: Gatekeeper + Execution | Pre-trade compliance gate + live small account execution | Pending |

### The Only Main Chain

```
Chairman intent
    ↓
Module A: Collect rule info
    ├─→ Module B: Verify accuracy (NotebookLM)
    └─→ Module C: Evaluate setup
    ↓
Module D: Gatekeeper decision (ALLOW / BLOCK / REVIEW / REDUCE_SIZE)
    ↓
Module E: Execution on small account
    ↓
Result logged
```

### Infrastructure Layer (separate from decision chain)

- Win PC (温总): execution node
- NinjaTrader: execution terminal
- Replikanto: account copy / mapping
- Signal Server: command entry point

---

## What v1 Does NOT Do

- No SPX 0DTE or multi-strategy parallel operation
- No fully unattended trading
- No CEO / LLC / company governance automation
- No complete performance review system
- No multi-account matrix management
- No live market (only small eval accounts)

---

## v1 Acceptance Criteria

All five must be met before v1 is considered complete:

- [ ] A: NotebookLM answers rule questions based on official contract sources
- [ ] B: Gatekeeper produces ALLOW/BLOCK/REVIEW/REDUCE_SIZE with reasons
- [ ] C: At least one strategy can output setup status
- [ ] D: Execution completes real trades on small account and logs results
- [ ] E: Daily net value report auto-delivered to Telegram

---

## Scope Change Policy

Any addition to v1 scope requires:
1. Chairman explicit approval
2. Written record in this file

---

*Locked: 2026-04-08 | Chairman approved*
