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

## Minimum Rule Set (v1)

### Hard BLOCK conditions (any one triggers BLOCK)

1. Rule verification incomplete or failed → BLOCK
2. Account DD remaining < 10% of max DD → BLOCK
3. Current time >= 16:00 ET → BLOCK
4. Not a full trading day (holiday / early close) → BLOCK
5. Platform explicitly prohibits the intended trade type → BLOCK

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
