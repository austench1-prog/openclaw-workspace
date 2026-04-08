# Compliance Output Schema v1
# Phase 3: Structured output for Gatekeeper consumption
# Date: 2026-04-08

---

## Output Format

Every compliance check must produce a structured result in this format:

```
PLATFORM: [Apex / MFF / TradeDay / TPT]
ACCOUNT: [account ID]
CHECK_DATE: [YYYY-MM-DD]
SOURCE_DATE: [date of source document]
SOURCE_URL: [official URL]

RESULT: [ALLOW / BLOCK / REVIEW / REDUCE_SIZE]
RISK_LEVEL: [LOW / MEDIUM / HIGH / CRITICAL]

CHECKS:
- Drawdown type: [result]
- DD remaining: [amount] / [max] = [%]
- Daily Loss Limit: [amount / N/A]
- Consistency rule: [status]
- Overnight allowed: [YES / NO]
- Current time compliance: [OK / VIOLATION]
- Trading day: [FULL / HOLIDAY / HALF-DAY]
- Max contracts: [number]

VIOLATIONS: [list any rule violations found]
WARNINGS: [list any near-limit conditions]
NOTES: [any additional context]
```

---

## Decision Enumeration

| Value | Meaning |
|---|---|
| ALLOW | All checks passed, proceed |
| BLOCK | Hard violation found, do not execute |
| REVIEW | Ambiguous info, human confirmation needed |
| REDUCE_SIZE | Conditions met but risk elevated, reduce position |

---

## Apex EOD Eval Rule Card (APEX-165583-123)

```
PLATFORM: Apex Trader Funding
ACCOUNT: APEX-165583-123
PRODUCT: 50k Tradovate EOD Trail
SOURCE: https://support.apextraderfunding.com/hc/en-us/articles/46724640813083
LAST_VERIFIED: 2026-04-08

PARAMETERS:
- Drawdown type: EOD (calculated at market close, enforced next session)
- Max Drawdown: $2,000
- Daily Loss Limit: $1,000 (fixed, does NOT fail eval if hit)
- Profit Target: $3,000
- Max Contracts: 6
- Min Trading Days: NONE
- Consistency Rule: NOT APPLIED
- Overnight: PROHIBITED (4:59 PM ET official / 4:00 PM ET system)
- Expiry: 2026-05-06

RULE_UPDATE_POLICY: Rules may change at any time. Re-verify weekly.
NEXT_VERIFY_DUE: 2026-04-15
```

---

## Query Templates for NotebookLM

### Template 1: Pre-trade rule check
```
Based only on the sources, for account [ACCOUNT_ID] on [PLATFORM]:
1. Is [TRADE_TYPE] allowed right now?
2. What is the current DD remaining limit?
3. Are there any rule violations or warnings?
Please cite the source document and date.
```

### Template 2: Rule change check
```
Compare the current sources for [PLATFORM] with the previous version.
List any changes found. If no changes, confirm rules are unchanged.
```

### Template 3: Consistency check
```
For [PLATFORM] account, current total profit is [AMOUNT].
What is the maximum allowed profit for today under the consistency rule?
Cite the source.
```

---

*v1 | 2026-04-08 | Phase 3 complete*
