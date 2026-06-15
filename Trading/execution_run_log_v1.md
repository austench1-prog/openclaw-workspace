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

## Session 2 — 2026-06-15 ~15:14-15:18 PDT (TPT 5天要求 · 仅记录)

| 项 | 内容 |
|---|---|
| 目的 | 让**今天(星期一)有一整天的交易记录**。这 4 个 TPT 账户是**今天新买的** |
| 大目标 | 本周完成 test → 进入 **PRO account**。**5 个交易日是 TPT 的硬性要求** |
| 产品 | MNQ SEP26 (微型) |
| 账户 | TPT 4 个账户(通过 Replikanto 跟单) |
| 时间 | 2026-06-15 (周一) 约 15:14-15:18 PDT |
| 结果 | 符合理想 ✅ 盈利 ~$102 |
| 性质 | 合规性交易(为满足 5 交易日硬性要求 · 第 1 天),非策略交易 — 不做分析 |

*说明:Chairman 明确表示此笔仅归档到历史,不需过多分析。本周进度:第 1/5 交易日完成。*

**⚠️ 重要补充(平台隔离):**
- 本次交易在 **NinjaTrader(尼加)** 上手动完成,**符合平台“全手动”要求**。
- **后续交易会换到另一个平台进行** — 目的:避免与其他**自动交易相互影响、破坏平台规则**。
- 原则:手动 test 账户 与 自动执行链 必须**平台隔离**,不可混在同一平台/连接。

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
