# Dragon Indicator Development Workflow v1
# Date: 2026-04-13
# Status: FINALIZED - Chairman approved

---

## Core Principles
- New indicators default to TradingView (TV) first
- 小塔 (Mac mini) = Dragon's primary operation base
- Maximum Dragon autonomy as priority

---

## Platform Roles

| Platform | Role |
|---|---|
| TradingView | Primary creation and validation platform |
| Other platforms (TOS, NinjaTrader, IBKR) | Migration decision by Chairman only |

---

## Workflow

1. Receive indicator instruction
2. Dragon writes code on 小塔
3. Dragon uses Playwright + Chrome to operate TV Pine Editor autonomously
4. Code inserted, chart validated — fully autonomous
5. Screenshot sent to Chairman for confirmation
6. Package deliverable version
7. Wait for Chairman to decide on migration

---

## Deliverable Standard

Each indicator must include:
- Indicator name
- Purpose
- Core logic (plain text spec file)
- Parameters table
- TV Pine Script code
- Migration notes

---

## Migration Decision
- No automatic migration after TV validation
- Whether to migrate to TOS / NinjaTrader / IBKR = Chairman decides

---

## Assistance Protocol
- If Dragon sends a request requiring Chairman response, auto-remind once after 5 minutes

---

## Offline Rules
- Chairman notifies Dragon when going offline
- Dragon does not disturb for non-essential items during offline period

---

*v1 | Chairman approved 2026-04-13*
