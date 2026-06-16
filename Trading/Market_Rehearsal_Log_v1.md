# Market Rehearsal Log (行情预演记录) v1

**Owner:** Chairman (Austin) | **Maintained by:** Dragon
**Created:** 2026-06-15
**Last updated:** 2026-06-16

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

## R003 — 2026-06-15 18:30 PDT · NQ · 做空推演 (Short rehearsal)

**Chart:** `strategy_charts/2026-06-15_Short_Harami_FVG_target.jpg` (15m + 1H)
**Type:** 行情推演 + 预测(正在走的行情;不在交易计划内,目的=完善素材库 + 将来对照)。防火墙适用,不是规则。

### Chairman 原话分析(尽量照讲述形式)
看到一个很好的小的下跌行情,主要靠三个点:
1. 价格已回落到前高点下面 —— 突破后又回到前高的下方。
2. 形成一个向下箭头(两根绿色小柱体),再一个向上箭头指到一根两边被完全包裹的孕线组合(前面完全包住后面;判断当下后面那根红的还没出现)。
3. 行情一定会往某个方向去:这里有一个黄色的 4H + 15m 组成的 FVG —— 这种 FVG 对价格有强烈吸引力。下方 15m 200日均线也有吸引力,但不作目标。

**目标:** 放在 FVG 下方、形成该 FVG 那根 4H 的开盘价上。从 15m 看也是合理目标。
**止损:** 孕线那根长柱体上方、1H 线上方(可优化点)。
**关键:** 确认用的是 1H 图(不是 4H)→ 小而稳的交易区间,不是大区间。Chairman 期望这个一定能实现。

### ✅ 成败判定标准(预测的核心 — 将来回头能明确判成/败)
- **成功** = 价格向下走到「FVG 那根 4H 的开盘价」目标位(未先被止损)。
- **失败** = 价格先上破止损位(孕线长柱体上方 / 1H 上方)。
- 回看时按以上二者明确判定。

### Outcome ✅ (已填 2026-06-16)
- [x] **是否到达目标(FVG 那根 4H 开盘价 ≈ Target 30,549)?** → **是。目标位已达成。**
- [x] **是否先触止损(STOP 30,872.50)?** → 注意:止损位(昨日高点)**事后也被突破了** — 但行情**先达到了目标**。
- **判定: 看准了(方向/目标正确) — 但「入场机会」不对。**

### 🔑 Chairman 复盘核心结论(2026-06-16) — 规则重要性的铁证
- **按规则本不该入场:** 昨日在**开盘价 + 1 小时形成之后**,按规则是**不应该入场**的。后来看到机会就入场了。
- **看准 ≠ 该做:** 目标看得没错,但行情反向要走的位置**很不明确** → 成功率不高。
- **失败的本质:** 看得准,但**入场机会不对 + 耐心不够** = 失败。
- **正面验证:** 同样用昨天看到的交易位置,今天把 **6 个考核账户全部做到 PPT $1500**(见 execution_run_log Session 3)。
- **结论(Chairman):** 如果能**遵守规则 + 足够耐心 + 我们的策略**,是可以拿到非常好的结果的。
- → **这条复盘反向印证了我们的纪律原则:「日内交易并不是日日交易」、「看到但不满足要求 = 不进场,损失的只是一个交易机会」。**

---

## R004 — 2026-06-16 07:57 PDT · NQ · 今日推演 (Today's rehearsal)

**Chart:** `rehearsal_charts/2026-06-16_R004_NQ_15m_4H_today_rehearsal.jpg` (15m + 4H)
**Type:** 行情推演(预推测)。防火墙适用 — 不是规则。图上同时保留了昨日(R003)的推演用作对照。

### 关键位(图上读取)
- **STOP / 前高点:** 30,872.50(昨日高点,事后已被突破)
- **前高点次位:** 30,815.25 / 30,815.50
- **4H & 15m FVG↑↑(黄带):** 约 30,649.00 – 30,715.25
- **Target(橙线):** 30,549.00(昨日目标,已达成)
- **今日可能触及位(绿虚线):** 30,191.25(15m) / 下方跳空区参考 ~30,100

### Chairman 今日推演原话(尽量照讲述形式)
- 图上新添了**今天的推演**:上方画了关键点位,下方画了今天可能要触及的位置。
- **关键:一个关键的 15 分钟的、非常瘦的十字星。它的上位决定了今天的走势。**
- **剧本 A(盘整):** 如果行情很快向上突破这个十字星位置 → 今天可能是一个盘整局面。
- **剧本 B(下行):** 如果不能再回到这个位置,甚至向下突破这根十字线 → 基本会去冲突破**星期一的开盘涨**,进入**跳空区域**。跳空区域的防守**非常弱**。
- **宏观意义:** 星期二收盘之后,基本上我们会看到整个大盘**未来几个礼拜的走势**。
- **FVG 边理论验证:** 昨日用 FVG 的近边作为止盈 → 今日证明**这个做法是有其好处的**。

### ✅ 成败判定标准(将来回头能明确判)
- **剧本 A 应验** = 价格快速上破关键 15m 十字星位 → 今日走盘整。
- **剧本 B 应验** = 价格不能回到该位 / 向下破十字线 → 冲星期一开盘涨 + 进跳空区(防守弱)。
- 回看时按以上二者明确判定哪个剧本应验。

### Outcome (to fill later)
- [ ] 价格对关键 15m 十字星位的反应? (上破 / 下破)
- [ ] 是剧本 A(盘整) 还是 剧本 B(下行冲跳空)?
- [ ] 是否触及下方 ~30,191 / 跳空区?
- 判定:____ (剧本 A / 剧本 B / 均不符)

### 🔄 R004 Update1 — 2026-06-16 09:38 PDT · 行情已朝看的方向走 + OB 论证
**Chart:** `rehearsal_charts/2026-06-16_R004_update1_OB_confirmed.jpg` (15m + 4H)

**已发生(验证中):**
- 昨日黄色目标位(Target 30,549)**已被突破**。
- 也碰到了另一个吸引价格过来的点:**200 日均线**。

**新增 OB 论证（为什么这个 Order Block 非常确立）:**
- 用打勾的那根**反向柱体**看作一个 **OB (Order Block)**。OB 区间约 **30,711 顶 → ~30,640 底**。
- 确立理由 1:它在关键点位（前高/STOP 区）**下方**。
- 确立理由 2(历史回看):两个打箭头的方向 = **sweep point(流动性扫荡)**;打叉的地方 = 有一个 **FVG**。
- 上述因素合起来 → **这个 OB 非常确立**。

**看法未变 + 今日偏好:**
- 价格始终会去触碰**星期一的开盘价**（绿色「星期一 1 小时开盘区域」~30,549 顶 / 30,191.25 底）。
- 进入该 1H 区域后的三种走法:① 直接穿过不回头 ② 在区域内盘整 ③ 穿过后再回头。
- **Chairman 最大认为:** 会需要在这个 1H 区域内**盘整**,但**今天会突破**。

**记录目的(Chairman 原话):** 把这些记下来 = 让我们知道**我们是怎么看盘的**、**是什么因素决定了我们对盘面的看法**。

### 🔄 R004 Update2 — 2026-06-16 16:22 PDT · 下午盘面观察
**Chart:** `rehearsal_charts/2026-06-16_R004_update2_afternoon_downward.jpg` (5m + 1H)

**当前市况（实时观察，无交易）:**
- 大盘继续向下态势，NQ 现在约 30,340。
- 收盘未突破星期一的一小时线下方 → 一小时盘整还没有走出来。
- 今日开盘第一根一小时线收盘时，出现了一个**做空机会**：止损位非常干净，但**没有进行任何交易**，仅作观察记录。

**目标向下调整:**
- 黄色目标已向下修正：往**一小时 200 日均线**方向（约 29,707）。
- 同时完全填补星期一开盘跳空空间。

**下一步期待:**
- 副本 A：放弃一小时盘整区间，向下冲击跳空区 + 1H 200MA（目标 ~29,707）。
- 副本 B：价格回升趼 OB 区，再寻機做空。

**待后补充 Outcome:**
- [ ] 盘整是否向下突破？
- [ ] 目标 ~29,707 是否到达？

---

## Prior charts to backfill
Chairman noted several earlier charts already shared should also be folded in as rehearsal records. **TODO:** locate and append as R000-series once Chairman points them out.
