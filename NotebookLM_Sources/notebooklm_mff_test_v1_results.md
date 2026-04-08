# NotebookLM Compliance Skill - MFF Accuracy Test v1 Results
# Notebook: MATS_v1_Compliance
# Date: 2026-04-08
# Result: 10/10 PASS

---

## Score: 10 / 10 — PASS

| # | Question | Expected | NotebookLM Answer | Pass/Fail |
|---|---|---|---|---|
| Q1 | MFF drawdown type | EOD Trailing | EOD Trailing (floor moves only at end of day) | ✅ |
| Q2 | Max EOD drawdown $50K | $1,500 (3% of $50K) | $1,500 | ✅ |
| Q3 | Daily loss limit | NONE | None confirmed | ✅ |
| Q4 | Consistency rule + base | 50%, base = $3,000 target | 50%, base = profit target $3,000 | ✅ |
| Q5 | Max daily profit | $1,500 | $1,500 (50% of $3,000) | ✅ |
| Q6 | Min trading days | 5 days | 5 days | ✅ |
| Q7 | Overnight allowed | Prohibited (system 16:00 ET) | Prohibited by protocol | ✅ |
| Q8 | Profit target | $3,000 | $3,000 | ✅ |
| Q9 | Floor behavior at start+$100 | Becomes Static | Stops moving, becomes static | ✅ |
| Q10 | Unrealized losses counted | Yes | Yes, open equity losses count for breach | ✅ |

---

## Key Corrections Confirmed

- MFF Max Drawdown = **$1,500** (3% of $50K), NOT $2,000 as previously recorded
- Consistency base = **profit TARGET** ($3,000), not current profit
- Unrealized (open) losses **do count** toward EOD threshold breach

---

## Conclusion

NotebookLM `MATS_v1_Compliance` now correctly answers MFF evaluation rules based on official sources.
Both Apex and MFF rule cards are verified and accurate.

**Phase 2 complete for both platforms.**

---

*Test conducted: 2026-04-08 | Dragon automated via Playwright*
