# Gatekeeper v1 - Pre-Trade Compliance Gate
# Version: 1.0 | Date: 2026-04-08
# Status: Draft - Paper testing phase

---

## Role

The Gatekeeper is the final gate before any trade is executed.
It receives inputs from Module B (NotebookLM compliance result) and Module C (Strategy Pack setup judgment),
then produces a single decision: ALLOW / BLOCK / REVIEW / REDUCE_SIZE.

---

## Inputs

| Input | Source | Required |
|---|---|---|
| Trade intent | Chairman / Strategy Pack | Yes |
| Platform compliance result | NotebookLM (Module B) | Yes |
| Account status | Playwright account scraper | Yes |
| Current equity | Playwright account scraper | Yes |
| DD remaining | Calculated from equity + floor | Yes |
| Setup status | Strategy Pack (Module C) | Optional |

---

## Output Decisions

| Decision | Meaning | Action |
|---|---|---|
| ALLOW | All checks passed | Pass to Execution |
| BLOCK | Hard rule violation | Stop, do not execute, alert Chairman |
| REVIEW | Ambiguous or incomplete info | Hold, request human confirmation |
| REDUCE_SIZE | Conditions met but risk too close | Execute with reduced position size |

---

## ⚠️ CRITICAL: Minimum Hold Time Rule (PLACEHOLDER - PENDING RESEARCH)

**Status: NOT YET DEFINED — DO NOT DEPLOY LIVE UNTIL THIS IS RESOLVED**

This is a potential account termination risk. Many Prop Firms prohibit high-frequency scalping (positions held for only a few seconds).

**Required action:**
- [ ] Research minimum hold time rules for: Apex, MFF, TradeDay, TPT
- [ ] Identify the strictest requirement across all platforms
- [ ] Set SYSTEM-WIDE minimum hold time = strictest platform requirement
- [ ] Add to Gatekeeper: if position held < minimum time → BLOCK close order

**Why this matters:** Accounts can be terminated immediately for violating this rule.

---

## Approved Instruments Whitelist

**Only the following instruments are permitted in MATS v1. All others are automatically BLOCK.**

| Pair | Full Contract | Micro Contract | Ratio | Notes |
|---|---|---|---|---|
| NQ / MNQ | NQ (Nasdaq-100) | MNQ (Micro) | 10 MNQ = 1 NQ | ✅ Allowed |
| ES / MES | ES (S&P 500) | MES (Micro) | 10 MES = 1 ES | ✅ Allowed |
| GC / MGC | GC (Gold) | MGC (Micro) | 10 MGC = 1 GC | ✅ Allowed in system |

**Any instrument not on this list → automatic BLOCK, no exceptions.**

### Two-Layer Instrument Check

The Gatekeeper applies TWO independent checks:

**Layer 1 — System Whitelist (permanent, never changes):**
NQ/MNQ, ES/MES, GC/MGC are the only allowed instruments across all platforms.

**Layer 2 — Platform/Account Restriction (dynamic, read from Compliance Pack):**
Each platform may have temporary or permanent restrictions on specific instruments.
Example: Apex currently suspends GC/MGC — this is a platform-level rule, not a system rule.

Both layers must ALLOW for a trade to proceed.
If Layer 1 passes but Layer 2 blocks → BLOCK with reason "Platform restriction: [instrument] suspended on [platform]"

### Contract Size Conversion (for max position check)

All position limits are expressed in full-contract equivalents:

- Apex $50K max: **6 NQ-equivalent contracts**
- 1 NQ = 10 MNQ
- 1 ES = 10 MES
- 1 GC = 10 MGC

Examples:
- 6 MNQ = 0.6 NQ equivalent → ALLOW
- 60 MNQ = 6 NQ equivalent → at limit, ALLOW
- 61 MNQ = 6.1 NQ equivalent → BLOCK (over limit)
- Mix: 3 NQ + 30 MNQ = 3 + 3 = 6 NQ equivalent → at limit

---

## Minimum Rule Set (v1)

### Hard BLOCK conditions (any one triggers BLOCK)

1. Rule verification incomplete or failed → BLOCK
2. Account DD remaining < 10% of max DD → BLOCK
3. Current time >= 16:09 ET → BLOCK + FLATTEN_ALL (system hard deadline, before any platform's 16:10 cutoff)
4. Not a full trading day (holiday / early close) → BLOCK
5. Platform explicitly prohibits the intended trade type → BLOCK

### Time-based action sequence

- **16:09 ET** → FLATTEN_ALL + BLOCK all new orders (system enforced, no exceptions)
- Official platform deadline: 16:59 ET (Apex) — our 16:09 provides 50-minute buffer

### REDUCE_SIZE conditions

6. DD remaining between 10% and 25% of max DD → REDUCE_SIZE (half position)
7. Daily P&L approaching consistency limit (>80% of max) → REDUCE_SIZE

### REVIEW conditions

8. Compliance source is older than 7 days → REVIEW
9. Conflicting rule information across sources → REVIEW
10. Account status unconfirmed → REVIEW

### ALLOW condition

All checks pass and none of the above triggered → ALLOW

---

## Paper Test Scenarios (10 cases)

| # | Scenario | Expected Decision |
|---|---|---|
| 1 | Normal trade, all checks pass, DD healthy | ALLOW |
| 2 | Time is 16:05 ET | BLOCK |
| 3 | DD remaining = $150 (Apex $2000 max) | BLOCK |
| 4 | DD remaining = $350 (Apex $2000 max) | REDUCE_SIZE |
| 5 | Rule source last updated 10 days ago | REVIEW |
| 6 | Holiday today | BLOCK |
| 7 | MFF daily profit at $1,400 (limit $1,500) | REDUCE_SIZE |
| 8 | Overnight position attempt after 16:00 ET | BLOCK |
| 9 | Conflicting info on max contracts | REVIEW |
| 10 | Normal trade, DD healthy, within hours | ALLOW |

---

## Integration Notes

- Gatekeeper does NOT collect rules itself (that is Module A + B)
- Gatekeeper does NOT execute trades (that is Module E)
- Gatekeeper only makes the GO / NO-GO decision
- All BLOCK decisions must be logged with reason
- All REVIEW decisions must alert Chairman via Telegram

---

*Draft v1 | Paper testing complete before live integration*
