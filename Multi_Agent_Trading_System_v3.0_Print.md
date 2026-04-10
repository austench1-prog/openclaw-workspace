# Multi-Agent Trading System v3.0 — Print Version
# Date: 2026-04-09 | Status: Finalized

---

## One-Line Definition

> Rule verification → Strategy judgment → Risk gating → Automated execution → Dual endpoints (NinjaTrader + IBKR) → Meritpoint Logic LLC + Prop Firm accounts in parallel.

---

## Main Chain (How the System Works)

```
INPUT
Market Info / Prop Rules / Platform Status
         │
         ▼
┌─────────────────────────────────────┐
│  LAYER A: Rule & Compliance         │
│  ┌──────────────────────────────┐   │
│  │ Prop Intelligence Agent      │   │
│  │ (collect raw rules)          │   │
│  └──────────────────────────────┘   │
│  ┌──────────────────────────────┐   │
│  │ NotebookLM Compliance Skill  │   │
│  │ (verify against contracts)   │   │
│  └──────────────────────────────┘   │
└─────────────────────────────────────┘
         │
         ▼
┌─────────────────────────────────────┐
│  LAYER B: Strategy Judgment         │
│  ┌──────────────────────────────┐   │
│  │ Trading Strategy             │   │
│  │ - Entry / SL / TP            │   │
│  │ - Trading Zone               │   │
│  └──────────────────────────────┘   │
│  ┌──────────────────────────────┐   │
│  │ Order Strategy               │   │
│  │ - Sub-zone execution         │   │
│  │ - Position mgmt              │   │
│  │ - Loss compression           │   │
│  └──────────────────────────────┘   │
└─────────────────────────────────────┘
         │
         ▼
┌─────────────────────────────────────┐
│  LAYER C: Gating & Execution        │
│  ┌──────────────────────────────┐   │
│  │ GATEKEEPER                   │   │
│  │ ALLOW / BLOCK / REVIEW /     │   │
│  │ REDUCE_SIZE                  │   │
│  │ Hard close: 16:09 ET         │   │
│  └──────────────────────────────┘   │
│           │ ALLOW                   │
│           ▼                         │
│  ┌──────────────────────────────┐   │
│  │ EXECUTION ENGINE             │   │
│  │ Account / Route Mapper       │   │
│  └──────────────────────────────┘   │
└─────────────────────────────────────┘
         │
    ┌────┴────┐
    │         │
    ▼         ▼
NinjaTrader  IBKR
Path         Path
```

---

## Layer D: Account & Route

```
NinjaTrader / Tradovate Path
(all same connection method)
├── Apex APEX-165583-123
│   (Prop Firm, EOD $50K, Active)
├── MFF MFFUEVRPD122274040
│   (Prop Firm, Suspended)
├── Future Prop Firm Accounts
└── Meritpoint Logic LLC via Tradovate
    (Entity Account, pending open)

IBKR Path
└── Meritpoint Logic LLC
    (Approved, pending funding)
```

---

## Layer E: Infrastructure

```
Win PC (温总) 192.168.0.226
└── NinjaTrader 8
    └── DragonFileSignal Strategy
        └── signal.txt ← Signal Server (port 5000)
            └── Replikanto
                └── Sim101 → Apex Follower
```

---

## Layer F: Management

```
Dragon-A (External / Ops)          Dragon-B (Internal / Assistant)
├── Hardware monitoring            ├── Rule queries
├── SSH / GitHub / API             ├── Pre-trade checklist
├── Environment maintenance        ├── Account daily report
└── Circuit breaker                └── Information relay
```

---

## Instrument Whitelist

| Pair | Status |
|---|---|
| NQ / MNQ | ✅ Always allowed |
| ES / MES | ✅ Always allowed |
| GC / MGC | ✅ System allowed (Apex: currently suspended) |
| All others | ❌ Auto BLOCK |

---

## Gatekeeper Rules Summary

| Condition | Decision |
|---|---|
| Time ≥ 16:09 ET | BLOCK + FLATTEN_ALL |
| DD remaining < 10% | BLOCK |
| DD remaining 10-25% | REDUCE_SIZE |
| Compliance source > 7 days old | REVIEW |
| Conflicting rule sources | REVIEW |
| Holiday / non-full trading day | BLOCK |
| All checks pass | ALLOW |

---

## Phase Progress

| Phase | Description | Status |
|---|---|---|
| 0 | Scope lock | ✅ |
| 1 | Account-Sourced Compliance Pack v2 | ✅ |
| 2 | NotebookLM (6 sources, 20/20 pass) | ✅ |
| 3 | Compliance output schema | ✅ |
| 4 | Gatekeeper paper test (10/10) | ✅ |
| **5** | **Execution integration** | **🔄 Next** |
| 6 | Strategy Pack | ⏳ Pending Chairman strategy input |
| 7 | Dragon dual role | ✅ |
| 8 | v1 Acceptance report | ⏳ |

---

## Key Decisions Locked

- Strategy input: **Manual first, automate later**
- All compliance sources: **Account contracts only (Tier 1)**
- Hard close time: **16:09 ET**
- All new accounts: **APM LLC registration**
- Purchase card: **Visa ••8869** (update when new card available)

---

*v3.0 | Finalized 2026-04-09 | Chairman approved*
