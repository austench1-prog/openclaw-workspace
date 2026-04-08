# Account-Sourced Compliance Pack v2
# Status: ACTIVE
# Date built: 2026-04-08
# Built by: Dragon (automated via Playwright)

---

## What This Is

This is the official Account-Sourced Compliance Pack v2 for MATS v1.
It replaces the previous version which relied on public website summaries and internal notes.

All sources in this pack come directly from authenticated account backends.

---

## Pack Contents

### Apex APEX-165583-123

| Item | Source | Date Retrieved | File |
|---|---|---|---|
| Account type confirmation | dashboard.apextraderfunding.com/accounts/activeEvals | 2026-04-08 | Apex_ActiveEvals_raw.txt |
| EOD Eval Trading Rules | support.apextraderfunding.com/hc/en-us/articles/46724640813083 | 2026-04-08 | Apex_EOD_Eval_Trading_Rules_official.txt |
| User Agreement | dashboard.apextraderfunding.com/legal/user-agreement | 2026-04-08 | Apex_User_Agreement_raw.txt |
| Trading Rules index | dashboard.apextraderfunding.com/legal/trading-rules | 2026-04-08 | Apex_Trading_Rules_raw.txt |

**Confirmed parameters:**
- Product: 50k Tradovate EOD Trail
- Drawdown: EOD (NOT Intraday)
- Max DD: $2,000
- Daily Loss Limit: $1,000
- Max Contracts: 6
- Min Trading Days: NONE
- Consistency: NOT APPLIED
- Position close: 4:59 PM ET
- Expiry: 2026-05-06

---

### MFF MFFUEVRPD122274040

| Item | Source | Date Retrieved | File |
|---|---|---|---|
| Account dashboard data | myfundedfutures.com/stats | 2026-04-08 | (captured via Playwright) |
| Evaluation Rules | help.myfundedfutures.com/en/collections/5808821 | 2026-04-08 | MFF_Eval_Rules_raw.txt |
| EOD Trailing rules | help.myfundedfutures.com/en/articles/8348565 | 2026-04-08 | MFF_Eval_Rules_raw.txt |
| Consistency Rule | help.myfundedfutures.com/en/articles/11994562 | 2026-04-08 | MFF_Eval_Rules_raw.txt |
| Terms & Conditions | myfundedfutures.com/terms | 2026-04-08 | MFF_Terms_raw.txt |

**Confirmed parameters:**
- Max EOD Drawdown: $1,500 (3% of $50K)
- Daily Loss Limit: NONE
- Consistency Rule: 50% of profit TARGET ($3,000) = $1,500/day max
- Min Trading Days: 5
- EOD floor locks at: starting balance + $100 (Static)
- Unrealized losses count toward EOD threshold breach
- Current status: SUSPENDED ($12.94 DD remaining)

---

## NotebookLM Status

- Notebook: MATS_v1_Compliance
- Sources loaded: 6
- Apex accuracy test: 10/10 PASS (2026-04-08)
- MFF accuracy test: 10/10 PASS (2026-04-08)

---

## Version History

| Version | Date | Change |
|---|---|---|
| v1 | 2026-04-07 | Built from public sources and internal notes |
| v2 | 2026-04-08 | Rebuilt from account-level authenticated sources only |

---

## Refresh Schedule

Per Account-Sourced Compliance Pack definition:
- Rules may change at any time (Apex User Agreement confirmed this)
- Recommended re-verification: weekly
- Next due: 2026-04-15

---

## Authorization Statement

This pack serves as the evidence base for:
- NotebookLM rule interpretation
- Gatekeeper authorization decisions
- Execution layer dependencies

Any rule not traceable to this pack must not be used for automatic ALLOW decisions.

---

*v2 | Built 2026-04-08 | Dragon automated retrieval*
