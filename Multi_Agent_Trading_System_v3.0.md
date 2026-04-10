# Multi-Agent Trading System v3.0
# Version: 3.0 | Date: 2026-04-09
# Status: DRAFT - Pending Chairman review and approval
# Supersedes: v2.0 (approved 2026-04-08)

---

## One-Line Definition

> A complete trading system with rule verification, strategy judgment, risk gating, and automated execution as the main chain; NinjaTrader/Tradovate and IBKR as dual execution endpoints; supporting Meritpoint Logic LLC and existing Prop Firm Tradovate accounts in parallel.

---

## What Changed from v2.0

| Item | v2.0 | v3.0 |
|---|---|---|
| Execution endpoints | NinjaTrader only | NinjaTrader + IBKR (dual path) |
| Strategy layer | Not formally structured | Trading Strategy + Order Strategy as Layer B |
| Account structure | Prop Firm accounts only | + Meritpoint Logic LLC as entity account (same Tradovate path) |
| Layer naming | Mixed | Standardized 5-layer naming |
| IBKR | Not in scope | Approved, pending funding |
| Strategy input | N/A | Manual first, iterate to automated later |

---

## Five-Layer Architecture

### Layer A — Rule & Compliance Layer
- Prop Intelligence Agent
- NotebookLM Compliance Skill
- Account-Sourced Compliance Pack (v2)

### Layer B — Strategy Judgment Layer
**Trading Strategy:**
- Define opportunity
- Original entry point / Base stop loss / Base take profit
- Form trading zone
- Min profit target + Max loss boundary

**Order Strategy:**
- Subdivide trading zone into sub-zones
- Match different execution methods per zone
- Position management
- Loss compression (risk erasure)
- Profit optimization

### Layer C — Gating & Execution Layer
**Gatekeeper:**
- Compliance check (from Layer A)
- Risk check (DD, DLL, time, instrument whitelist)
- Decision: ALLOW / BLOCK / REVIEW / REDUCE_SIZE
- Hard close: 16:09 ET FLATTEN_ALL

**Execution Engine:**
- Account / Route Mapper
- NinjaTrader Execution Path
- IBKR Execution Path

### Layer D — Account & Route Layer

All accounts in this layer connect via the same technical path: Tradovate → NinjaTrader.
The difference is account type only (Prop Firm vs Entity), not the connection method.

```
├─ NinjaTrader / Tradovate Path (all accounts same connection method)
│  ├─ Apex APEX-165583-123 (Prop Firm, EOD $50K, Active)
│  ├─ MFF MFFUEVRPD122274040 (Prop Firm, Suspended)
│  ├─ Future Prop Firm Accounts (same path)
│  └─ Meritpoint Logic LLC via Tradovate (Entity Account, pending open)
│     └─ Same Tradovate→NinjaTrader path as Prop Firm accounts
│
└─ IBKR Path
   └─ Meritpoint Logic LLC IBKR Account (Approved, pending funding)
```

**Design principles for Layer D:**
- All Tradovate accounts use identical connection path (no distinction at infra level)
- Account type difference (Prop vs Entity) is a compliance/routing concern, not a technical concern
- Upper layer unified (same decision chain for all)
- No default cross-mixing between accounts

### Layer E — Infrastructure Layer
- NinjaTrader (Win PC 温总, 192.168.0.226)
- Tradovate
- Replikanto (Master→Follower account copy)
- Signal Server (Python HTTP, port 5000)
- DragonFileSignal Strategy
- Logging / Monitoring / Alerting

### Layer F — Management & Support Layer
- **Dragon-A** (System External): Hardware, SSH, GitHub, environment
- **Dragon-B** (System Internal): Rule queries, checklist, daily report

---

## Main Chain Flow

```
Market Info / Prop Rules / Platform Status
    ↓
Layer A: Prop Intelligence Agent (collect)
    ├─→ NotebookLM Compliance Skill (verify rules)
    └─→ Layer B: Strategy Pack
           ├─ Trading Strategy (zone, entry, SL, TP)
           └─ Order Strategy (sub-zone execution)
    ↓
Layer C: Gatekeeper (ALLOW / BLOCK / REVIEW / REDUCE_SIZE)
    ↓
Layer C: Execution Engine → Account / Route Mapper
    ├─ NinjaTrader Path → Tradovate
    │   ├─ Meritpoint Logic LLC
    │   └─ Prop Firm Accounts (via Replikanto)
    └─ IBKR Path
        └─ Meritpoint Logic LLC IBKR
    ↓
Real execution → Fill confirmation → Log → Monitor
```

---

## Simplified View (for presentations)

```
Complete Trading System v3.0
├─ Rule & Compliance
│  ├─ Prop Intelligence Agent
│  └─ NotebookLM Compliance Skill
│
├─ Strategy Judgment
│  ├─ Trading Strategy
│  └─ Order Strategy
│
├─ Gating & Execution
│  ├─ Gatekeeper
│  └─ Execution Engine
│
├─ Execution Endpoints
│  ├─ NinjaTrader → Tradovate
│  │  ├─ Meritpoint Logic LLC
│  │  └─ Prop Firm Accounts
│  └─ IBKR → Meritpoint Logic LLC
│
└─ Infrastructure & Ops
   ├─ Win PC / Signal Server / Replikanto
   └─ Dragon-A / Dragon-B
```

---

## Instrument Whitelist (permanent)

| Pair | System | Platform Status |
|---|---|---|
| NQ / MNQ | ✅ Always allowed | Apex: ✅ / MFF: ✅ |
| ES / MES | ✅ Always allowed | Apex: ✅ / MFF: ✅ |
| GC / MGC | ✅ System allowed | Apex: ⚠️ Currently suspended |

---

## v3.0 Phase Map

| Phase | Description | Status |
|---|---|---|
| 0 | Scope lock | ✅ Done |
| 1 | Account-Sourced Compliance Pack v2 | ✅ Done |
| 2 | NotebookLM (6 sources, 20/20 tests) | ✅ Done |
| 3 | Compliance output schema | ✅ Done |
| 4 | Gatekeeper paper test (10/10) | ✅ Done |
| 5 | Execution integration | 🔄 Next |
| 6 | Strategy Pack (needs Chairman input) | ⏳ Pending |
| 7 | Dragon dual role | ✅ Done |
| 8 | v1 acceptance report | ⏳ After Phase 5+6 |

---

## Version Control

- **All changes to this document require Chairman approval**
- v2.0 remains valid until this document is formally approved
- Upon approval, v3.0 supersedes v2.0

---

*v3.0 Draft | 2026-04-09 | Pending Chairman approval*
