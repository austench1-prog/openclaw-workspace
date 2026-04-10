# Execution Run Log v1
# Date: 2026-04-09
# Phase 5 Test Session

---

## Session 1 — 2026-04-09 ~20:15-20:53 PDT

| Time | Signal | Result | Notes |
|---|---|---|---|
| 20:15 | Server ping | Online ✅ | Signal Server running |
| 20:17 | BUY\|NQ\|1 | Executed ✅ | First successful order |
| 20:33 | FLATTEN_ALL | OK ✅ | Cleared |
| 20:33 | BUY\|NQ\|1\|SL=19100\|TP=19300 | Closed immediately | SL/TP price too close to market |
| 20:34 | BUY\|NQ\|1\|SL=18800\|TP=19600 | Closed immediately | Prices already inside market range |
| 20:35 | SELL\|NQ\|1\|SL=25267\|TP=19000 | Error | SL below market for SELL order |
| ~20:48 | SELL\|NQ\|1\|SL=25310\|TP=25270 | No fill | DragonFileSig 1 Min accidentally unchecked (mouse click) |
| ~20:50 | (Chairman re-enabled strategy) | Executed immediately ✅ | Order filled as soon as strategy re-enabled |
| 20:52 | FLATTEN_ALL | OK ✅ | Session ended |

---

## Lessons Learned

1. **SL/TP must be realistic relative to current market price**
   - SELL: SL must be ABOVE market, TP must be BELOW market
   - BUY: SL must be BELOW market, TP must be ABOVE market
   - Wide enough to not trigger immediately

2. **DragonFileSig 1 Minute can be accidentally unchecked**
   - Risk: Mouse accidentally clicks the checkbox
   - First diagnostic step when signal doesn't execute: CHECK if 1 Minute is still green
   - SOP: Always verify strategy status before assuming signal failure

3. **Market orders (no TYPE parameter) work correctly**
   - Signal format confirmed working: SELL|NQ|1|SL=25310|TP=25270

---

## Phase 5 Status: PASSED ✅

Complete chain verified:
Dragon → Signal Server → signal.txt → DragonFileSignal → Sim101 → Market order + SL/TP

---

*Session logged: 2026-04-09 | Dragon*
