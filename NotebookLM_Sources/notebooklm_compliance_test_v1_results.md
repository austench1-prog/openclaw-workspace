# NotebookLM Compliance Skill - Accuracy Test v1 Results
# Notebook: MATS_v1_Compliance
# Date: 2026-04-08
# Tester: Dragon (automated via Playwright)
# Result: 10/10 PASS

---

## Test Setup

**Notebook:** MATS_v1_Compliance
**Sources loaded:** 4
1. Prop Firm Rules Internal Reference v1
2. Compliance Protocol v2.1
3. Evaluation Account Risk Form (English)
4. Pasted Text (supplementary)

**Method:** Each question was submitted to NotebookLM chat. Answers were verified against known correct answers.

---

## Test Results

| # | Question | Expected Answer | NotebookLM Answer | Pass/Fail |
|---|---|---|---|---|
| Q1 | What is the drawdown type for Apex $50K evaluation? | Intraday Trailing | Intraday Trailing — real-time, follows peak equity including unrealized PnL | ✅ PASS |
| Q2 | Is overnight holding allowed for Apex? What is the deadline? | PROHIBITED, 16:59 ET (system: 16:00 ET) | PROHIBITED. Official: 16:59 ET. System hard close: 16:00 ET. Weekend also prohibited. | ✅ PASS |
| Q3 | Minimum trading days for Apex $50K evaluation? | 7 days | 7 days | ✅ PASS |
| Q4 | Profit target for Apex $50K evaluation? | $3,000 realized only | $3,000 realized profit only, float excluded | ✅ PASS |
| Q5 | Maximum position size for Apex $50K? | 10 contracts | 10 contracts | ✅ PASS |
| Q6 | MFF consistency rule: what is the base and max daily profit? | Base = $3,000 target (NOT current profit). Max daily = $1,500 | Base = $3,000 profit target. Max daily = $1,500 (50% of target) | ✅ PASS |
| Q7 | What drawdown type does MFF use? | EOD Trailing | EOD Trailing — updates end of day, can become Static at starting balance + $100 | ✅ PASS |
| Q8 | Minimum trading days for MFF evaluation? | 5 days | 5 days | ✅ PASS |
| Q9 | Does Apex allow EA / automated trading in eval? | Yes (officially), use caution | Yes, allowed officially for personal use. Caution advised on execution frequency. | ✅ PASS |
| Q10 | Does MFF have a daily loss limit? | No | No daily loss limit. Max drawdown is $2,000 EOD trailing. | ✅ PASS |

---

## Score: 10 / 10 — PASS

---

## Key Findings

- NotebookLM correctly identified **Intraday Trailing** for Apex (common mistake: confusing with EOD)
- Correctly stated MFF consistency base = **profit TARGET** not current profit (critical rule)
- Correctly identified **both** the official 16:59 ET rule and the system 16:00 ET hard close
- All answers cited source documents

---

## Conclusion

NotebookLM `MATS_v1_Compliance` notebook is functioning as the compliance accuracy layer (Module B).
It can now serve as the reliable rule basis for Gatekeeper decisions.

**Next step:** Phase 3 — Structure compliance output for Gatekeeper consumption (ALLOW / BLOCK / REVIEW / REDUCE_SIZE)

---

*Test conducted: 2026-04-08 | Dragon automated test via Playwright*
