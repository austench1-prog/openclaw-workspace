# Market Rehearsal Log (行情预演记录) v1

**Owner:** Chairman (Austin) | **Maintained by:** Dragon
**Created:** 2026-06-15
**Last updated:** 2026-06-15

---

## ⚠️ PURPOSE & FIREWALL (READ FIRST)

This document records the Chairman's **rehearsals (预演)** of market direction — his view of how price *may* unfold, derived by applying our **existing** rules and strategies.

This is **NOT prediction** (prediction implies a certainty commitment, which does not fit our system). It is a **rehearsal / read-through** done in a calm, neutral mindset.

### The One-Way Firewall (单向防火墙) — NON-NEGOTIABLE

```
   Rules / Strategy  ───►  Rehearsal     ✅ allowed (we use rules to reason)
   Rehearsal         ──X─► Rules/Strategy ❌ FORBIDDEN by default
```

- Rules and strategy **may feed** a rehearsal.
- A rehearsal **may NEVER reverse-flow** to become a rule or strategy.
- **Exception:** If, during a rehearsal, we believe we have discovered a *genuinely new rule*, it must be **manually migrated** into the proper Rules/Strategy doc as a **separate, explicit entry** — never silently absorbed. Until then, it stays here, isolated.
- Reason: If rehearsals leak into rules, the whole system can never be fixed/stabilized.

### Why we keep this log (long-term value)

Rehearsal and live order placement are **two different psychological states**:
- **Rehearsal** = calm, neutral mind, easier to escape emotional bias.
- **Live order** = real money, emotional constraint.

By logging rehearsals separately, we can later **compare rehearsal win-rate vs live-execution win-rate**. The gap tells us *which part needs strengthening* — judgment (the read) vs execution psychology (the trigger).

### Logging discipline
- Each rehearsal = one dated, numbered entry (R001, R002, ...).
- Record the **read** at the time, the **conditions** for confirmation/invalidation, and **what the rehearsal "would" do** — even when no live trade is taken.
- Later, mark **outcome** (did the read play out?) for win-rate comparison.
- Charts archived in `Trading/rehearsal_charts/`.

---

## Terminology note (locked)

- **"OB" = Order Block** (institutional order-block supply/demand zone). NOT "old block." Earlier entries below that said "old block" mean **Order Block (OB)**. Corrected 2026-06-15 08:45 per Chairman.

---

## R001 — 2026-06-15 (Mon) · NQ (Micro E-mini Nasdaq-100)

**Chart:** `rehearsal_charts/2026-06-15_R001_NQ_15m_4H.jpg` (15m left / 4H right)
**Live trade taken:** NO (Monday + chop + no qualifying R:R; "intraday ≠ daily trading")
**Context price:** ~30,722

### Structure read
- **Red Order Block / OB (~30,800):** 4H prior supply zone (Order Block). Theory: must be **fully broken** (completely cleared), not just tagged.
- **4H Candle "A":** Just formed; treated as the **trade stage / zone** for this read.
- **15m low wick (white arrow):** Judged that this wick **will be broken to the upside**. Implication: for price to go **down**, it must **first break up through this wick** — i.e. up-break is the precondition for any downside.

### Rehearsal trade logic (two conditions)
Based on two points — ① the wick **must** be up-broken + ② the Order Block (OB) **must** be fully broken — within 4H candle **A** as the trade zone, **if** a point with sufficient R:R appears, there **is** an entry opportunity.
- Today: none found / not forced → **no trade**.

### Break vs Chop — decision thresholds (locked)
- **Upside confirmed:** break **above the red Order Block (OB)**.
- **Downside confirmed:** at minimum break **below the blue Asian-session zone**.
- Until either occurs → **regime = CHOP / range** → stay flat, do nothing.

### Ideal path (Chairman's script)
1. Break **up** through red Order Block (OB) →
2. **Pull back down to touch the 15m 200-MA (white line, ~29,700)** →
3. Resume **up**.
- **Robustness condition:** if the **200-MA rises above the green gap (MFVG)** zone, upside is more stable — pullback pressure is smaller.

### Discipline note
> "Intraday trading is not daily trading." (日内交易并不是日日交易。)
> Monday + chop + no R:R = no trade. Flat is the correct position.

### Update 1 — 2026-06-15 08:25 PDT (wick logic CONFIRMED)

**Chart:** `rehearsal_charts/2026-06-15_R001_NQ_update1_wick_confirmed.jpg`
**Price:** ~30,835

- **Up-break happened** → validates the R001 read that the 15m low-wick formation **would** break upward. The "wick must break up first" logic is confirmed.
- **Current read:** price continues up. Now testing/poking the top of the red Order Block / OB (~30,835); not yet a clean decisive close above — breakout *test* in progress.
- **Next long-entry plan (rehearsal):**
  - **4H stop reference is UNCHANGED** (stays below, anchored on the 4H structure).
  - **But** if a fresh long opportunity appears, find a **tighter, more reasonable stop on the 15m using the yellow FVG** (~30,575–30,675) — i.e. drop the stop to the 15m FVG instead of the wide 4H stop. That = a better long.
  - **Trigger condition:** if price pulls back into the yellow FVG and forms an up-move off it, that is a **strengthened up-range**, expected to then break its own prior high.

### Update 2 — 2026-06-15 08:43 PDT (BLOCK REDEFINED — new view)

**Chart:** `rehearsal_charts/2026-06-15_R001_NQ_update2_block_redefined.jpg`
**Price:** ~30,810

**New view — Order Block reconstruction:**
- **Old 4H red Order Block (OB) REMOVED** — it has been broken, so it no longer applies. We drop it.
- **New OB built from the white-arrow wick candle** (the deep low-wick) → this becomes the **origin of a new Order Block**. Unlike the prior (downward) OB, this is now an **UP OB** (向上 Order Block).
- **Key focus = the X-marked reversal candle** (the opposing/counter bar). This candle **strengthens the OB**. What to watch most: when price returns to the red OB area, what effect this X candle produces.
- **Highest-probability path:** price reaches the **wick of the candle just BEFORE the X-marked bar**, uses that as the **stop point**, and continues UP.

**The two-OB combo = a divider line (up vs down):**
- If price forms/holds **above** → continuation UP; we **stop looking at any retest below** that line.
- If price goes **below** → the combo flips into a **DOWN Order Block**; then we look toward the **200-MA** (~29,700) as the next target.

**Regime:** still CHOP / range until a side resolves. Today still **no trade** (assessment only, building rehearsal data).

### Outcome (to fill later)
- [x] Did the wick get up-broken first? → YES (confirmed 08:25).
- [ ] Did the Order Block (OB) fully break (up, clean close) OR Asian zone break (down)?
- [ ] Did the pullback-to-FVG → strengthened up-move → new prior-high break play out?
- Rehearsal read verdict: ____ (correct / partial / wrong)

---

## R002 — 2026-06-15 15:04 PDT · NQ · 回调/反转预推 (Pullback / Reversal rehearsal)

**Charts:** `rehearsal_charts/2026-06-15_R002_NQ_pullback_fib_1.jpg`, `..._fib_2.jpg` (TradingView, MasterPattern, 4H + 15m, with Fibonacci)
**Type:** 预推/预演 (rehearsal) — firewall applies, NOT a rule.

### Structure read
- **灰色盒区域(顶部 ~30,900):** 重点关注区。价格在盒里已**突破前高**,处于猛烈的 4H 上升趋势。
- 盒内出现**孕线形态(Inside Bar / Harami)** → 强烈的**回调 / 反转**信号。
  - **准确定义(Chairman 2026-06-15 纠正):** 是**两根 4H 线的组合** — 前面一根**长的绿色 4H 线(母线)** 完整包裹住后面一根**红色 4H 线(子线)**。即前一根线完全包住后一根 = 孕线(孕妇的孕)。不是单根短引线。

### Fibonacci 关键位 (图上)
- 0.295 = 30,704.75 · 0.41 = 30,620.25 · 0.5 · 0.705 = 30,403.75 · 0.79 = 30,341.50 · 1.0 = 30,181

### 预演逻辑
- 看价格在 **0.295~0.41 区间**怎么走,会不会碰到 **0.5**。
- **0.5 以下、被 0.5 包含的那根 4H 线的最低点** = 重要参考指标。
- **入场前提:要等至少一根 1H 线走出来之后**才考虑是否入场。
- **背离(GS)是重要参考:** 现在**尚未形成背离**。但价格可能在灰盒附近走一段后形成背离 → **若此时产生背离,向下走的形势基本可得到印证**。

### Outcome (to fill later)
- [ ] 价格在 0.295~0.41 如何反应?是否碰 0.5?
- [ ] 灰盒附近是否形成背离(GS)?
- [ ] 背离出现后向下是否得到印证?
- Rehearsal read verdict: ____ (correct / partial / wrong)

---

## Prior charts to backfill
Chairman noted several earlier charts already shared should also be folded in as rehearsal records. **TODO:** locate and append as R000-series once Chairman points them out.
