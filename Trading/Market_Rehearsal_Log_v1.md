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

### 🔄 R004 Update3 — 2026-06-16 22:07 PDT · 4H 框架推演 + AP 斐波那契扩展
**Charts:**
- `rehearsal_charts/2026-06-16_R004_update3_4H_AP_fib_NQ_15m_4H.jpg` (NQ 15m + 4H)
- `rehearsal_charts/2026-06-16_R004_update3_ES_NQ_4H_divergence.jpg` (ES + NQ 4H)

**大局判断（强烈看空）:**
- ES + NQ 两张 4H 图：大盘在关键高位，出现**背离**（图上已标）。市场现在**强烈看空**。

**AP 斐波那契扩展型推导（向下）:**
- 目标直接放到 **4H 200 SMA**（200 日均线 ~29,468）。
- 验证合理性（几个关键位都能解释得通）：
  - 中线落在**星期一 1H 开盘价范围**内。
  - **0.79 位（~29,708）** 跟调整前的原目标位（跳空下沿）在同一区域附近。
  - → 关键位都有合理性 → 走到该位的合理性增加。

**交易区间（看空基础上）:**
- **止损：** 最长那根 4H 柱的上方（STOP 点已标，~30,867）——当前最合理的上方。
- **FVG：** 该 4H 柱形成的 4H+15m 重叠 FVG（~30,720-30,760），接近 15m 200 日均线——期待先去实现。
- **关键防守点：** 最后一根红色 4H 线上端最高点。
  - 突破 → 震荡向上去碰 15m 均线 + 填 4H/15m FVG。
  - 不碰直接下行 → 看作下行趋势开始，突破星期一 1H 开盘区为指标。

**📝 新素材（草稿）：** AP 斐波那契扩展型用法 — 用 AP 向下扩展画目标，用多个关键位（中线/0.79）与已知结构（开盘区/跳空下沿）交叉验证目标合理性。**待整理进素材库。**

**⚠️ 跨日目标延续（Chairman 2026-06-16 锁定）：** 星期二设定的目标——**1H 开盘价的下沿**——在星期二没有被碰到。因此**星期三始终把它作为第一目标，只要行情没有确认反转。**

**待补充 Outcome:**
- [ ] 是否破关键防守点（最后红 4H 上端）？
- [ ] 是否达 4H 200SMA 目标（~29,468）？
- [ ] 星期三是否碰到 1H 开盘价下沿（顺延目标）？

### 🔄 R004 Update4 — 2026-06-16 22:29 PDT · 新形态素材：转折箱体 (Reversal Box)
**Chart:** `rehearsal_charts/2026-06-16_R004_update4_reversal_box_4H_15m.jpg` (15m + 4H)

**📝 新素材草稿：转折箱体（Reversal Box）**

**核心概念：** 4H 画出 STOP 红线的那根柱，到 15m 图上查其结构——落在一个完美的反向十字星上。说明 4H 与 15m **表达一致、互不冲突**。

**箱体的判读（灰色框，15m）：**
- 框内：一根**绿色十字星（向上推，未推上去）** + 下一根**红色十字星（落回）**。
- 两根十字星同时出现 → 判定市场**向下**：第一星推不上去，第二星落回 → 至少会去碰那根绿线的**最低价**。

**完美环境（为什么这箱体可信）：**
- 15m：箱体上方有 **FVG**。
- 4H：同位置有**很长的上影线**（向下推的引线），可画出一个 **Block**。
- 4H 下推引线 + 15m FVG 叠加 = 完美环境。

**重要补充（多根原则）：** 无论向上或向下，不要认为只会有一根。判定向下时，红色上影线那根可能不是最高的，后面可能还跟至少一根更高的——不会有问题。
- **关键判据：** 只要它**没有突破自己前一根 15m 柱体的最高点**。
- 可以有多根，正常来讲 **3 根**（前面等于有两根比它更低的）= 可作确认。
- 如果后面再出一根形成 **Sweep Point** → 很好的条件。

**待整理进素材库（与 AP 扩展、OB 一起）。**

### 🔄 R004 Update5 — 2026-06-16 22:36 PDT · 素材补充：1H 中位线 + 十字星转折
**Chart:** `rehearsal_charts/2026-06-16_R004_update5_1H_midline_doji_5m.jpg` (5m + 1H)

**📝 素材补充（草稿）：1H 实体中位线的转折价值**

补充今中午错过的一个高价值位置（白箭头所指）：一根非常小的十字星**碰到了 1H 实体的中位线**，后面形成绿色——本身就是一个重要的反转指标。

**三个重要元素（不应被忽略）：**
1. 它在一个 **1H 非常重要的区间**里（绿色 1H 区间区间宽 → 中位线尤其重要）。
2. 它**完美碰在中线（中位线）上**。
3. 这种**十字星形状本身 = 行情转折的重要指标**。

**跨周期确认：** 1H 形成这样的位置时，去 5m 看组合也是非常清楚的结构——在 5m 可以找到好的盈亏比入场点。

**⚠️ 重点：** 这条补充**不是要用它做什么交易**，也不是重新看市场（市场没有任何改变）。是提醒：**后面的分析要注意这些点，不该被忽略**。今中午这里是震荡区，要不要交易是另一回事。

**另一种走势（同一区域、同一根十字星）：** 如果后面不是出现向上的柱体，而是**直接没突破十字星的上影、直接再走一根红色柱体** → 这是一个**非常强烈的市场要继续向下、直接碰黄色目标**的信号。（今天没这样走，所以可接受。）
- **结论：** 这根十字星的**两种走势都很重要**（向上震荡补 FVG / 直接下行冲目标），忽略这个信号是不应该的。

**待整理进素材库。**

---

## R005 — 2026-06-17 06:29 PDT · 星期三晨间推演更新（看法延续昨晚）
**Chart:** `rehearsal_charts/2026-06-17_R005_NQ_wed_morning_15m_4H.jpg` (15m + 4H)
**Symbol:** NQ | **Bias:** 区间震荡，延续昨晚看法（无大变化）

**核心：看法基本不变。** 大盘仍在区间里震荡。

**4H 转折区域（转折箱体）：**
- 4H 上看到上下两个箭头标示的区域 → 基本形成一个**转折区域 / 转折箱体**的形式。
- **但两个问题：** ① 4H 这根**还没结束**；② 形态**不是特别明显**。15m 里看更不明显。
- → 所以这只是一个**看法**，不是确认。

**维持的判断：** 继续维持会**向上去碰 15m 的 200 日均线 + 填满黄色 FVG**。
- 很有可能在**今早 6:30 开盘（即现在）**出现一个巨大的 sweep 去完成这个 → **但不参与这个交易。**

**今天的交易计划：** 看 **4H 绿色柱子形成以后**（即 **7 点以后**）。

**待补充 Outcome:**
- [ ] 今早 6:30 开盘是否出现 sweep 去碰 15m 200SMA / 填黄色 FVG？
- [ ] 4H 转折箱体是否随这根 4H 收盘变明显（确认/证伪）？
- [ ] 7 点后 4H 绿柱是否形成（交易计划触发条件）？
- [ ] 跨日目标延续：是否碰到星期二未碰的 1H 开盘价下沿？

### 🔄 R005 Update1 — 2026-06-17 09:06 PDT（纽约 12:06）· 午后收盘后：目标不变，确认增多
**Charts:**
- `rehearsal_charts/2026-06-17_R005_update1_NQ_15m_4H_noon_confirm.jpg` (15m + 4H)
- `rehearsal_charts/2026-06-17_R005_update1_ES_NQ_1H_divergence.jpg` (1H ES + 1H NQ)

**核心：** 纽约 12:00 收盘后，**目标不变**，但行情已给出**更多确认**。

**确认一（1H 背离）：** 1H 图上 **ES 与 NQ 在下方做出向上背离**（两者背离）。
**确认二（关键位重碰）：** NQ 向上箭头处 **再一次触碰关键点位 = 星期一 1H 开盘的中位线**。

**15m OB 演化（NQ 15m）：**
- 灰色 15m OB 形成后 → 已被**蓝色 OB 穿越** → 回上穿越后到达非常关键点位 + 形成背离 → **现又重新形成一个 15m OB**。
- 该 **OB 区域被认为非常有效**。

**目标（不变，更明确）：**
- 第一目标：**15m 200 日均线**。
- 最终目标：至少**填补掉上方黄色 FVG**。

**状态判断：** 还没完全走出震荡；但认为 **当前这根 4H 线走完时，震荡应会走出一个趋势**。（与晨间 R005 一致：等 4H 绿柱收完 / 趋势明确再动。）

**⚠️ 防火墙：** 仍是推演，不是规则。背离/OB 有效性/中位线重碰 都是应用现有规则得出的 read。

### 🔄 R005 Update2 — 2026-06-17 10:26 PDT（纽约 13:26）· 15m 收完：判断不变但不完美，看 MFVG 确认
**Chart:** `rehearsal_charts/2026-06-17_R005_update2_MFVG_breakout_confirm_15m.jpg` (NQ 15m)

**核心：** 15m 已收完。按上面的新素材做推演——**整体判断没变（仍看向上去最低目标位），但这根 15m 不太完美。**

**三个不太好的地方（都是强向上阻力）：**
1. 这根**带下引线的红色 15m**，**没 close 在 1H 区域上方**，而是稍偏下方。
2. 上方形成了一个**灰色 FVG**。
3. **白色框**那里三条线组成的形态，其**上端**也不好。

**提高成功率的关键确认位（新增）：**
- 把 9:30 形成的那根**长下引线**用绿色标为 **MFVG** 区域。
- **触发：若出现一根 15m 收在 MFVG 这根线上方 → 向上突破极大得到确认。**

**状态：** 有这三个缺点 → 表示会在区间内**震荡时间较长**；但缺点**不改变走势（向上）的判断**。

**跨引：** 本次“打勾=已向上突破”的定义 已同步补入素材库【震荡区间内 突破 vs 回调】。
**⚠️ 防火墙：** 仍是推演。除非“打勾=向上突破”这条定义被明确当作新规则迁入（已迁），MFVG 确认位等仍是推演读。

### 🔄 R005 Update3 — 2026-06-17 15:04 PDT·收盘后：全天推演总结 + 复盘教训 + 后续目标
**Chart:** `rehearsal_charts/2026-06-17_R005_update3_EOD_summary_4H_OB_lesson.jpg` (NQ 15m + 4H)

**✅ 总结（对了一半）：**
- 对：大盘像预期一样**突破了下方星期一开盘价**。
- 错：但**没有**如预期先去碰上方 15m 200 日均线 → 而是直接向下。

**🔍 复盘教训（Chairman 自批，关键）：**
1. **4H 白闪电那根引线被忽略了。** 今天后续多次分析没认真处理/关注它 — 这是个较大的问题。它移到 15m 左边 = 那个**红色 OB**。
2. **MFVG（白叉）只考虑了“突破”、漏了“没突破”。** 早上只给了“实体突破 MFVG = 强烈向上确认”；但白箭头那根绿 15m **只碰到/经过 MFVG、没突破** → 上方红 OB + 15m 灰 OB → 大盘继续向下 → 此时就应认真考虑“向上可能性已大幅减少”。
3. **4H 叉×那根绿柱（上影线 + 前两根的关系）**也是当时应高度关注的信号 — 因为忽略了**红色 4H OB**，后面没给足够重视，当时看法偏于“向上先突破”。

**📌 提炼出的规则级教训（待迁入素材库）：** 任何分析中，**大时区（如 4H）的 block 都不应被疏忽**。大时区里产生的细节，对大盘的影响往往更大。

**目标（下行，未变）：**
- 大盘已向下突破 → 按原预设目标一个个往下观察。
- 第一个下行目标 = **黄色 4H 目标价位（Target ~29,800）**（未改变）。
- 下一个 = **蓝色 4H 200 日均线（~29,460）** — 认为会被触及。
- 原话末尾有一个截断残句（“这是根据现”），Chairman 15:14 指示删除，已删。总结完整。

**⚠️ 防火墙：** 仍是推演 + 复盘。复盘发现的“大时区 block 不可忽”这条是真正的规则级收获 → 需**手动迁入素材库/策略文档**后才生效（不能静默变规则）。

---

## R006 — 2026-06-17 06:59 PDT · 新形态素材：Judas Swing（草稿、待完善）
**Charts:**
- `rehearsal_charts/2026-06-17_R006_NQ_judas_swing_15m_5m_1.jpg` (15m + 5m)
- `rehearsal_charts/2026-06-17_R006_NQ_judas_swing_15m_5m_2.jpg` (15m + 5m)
- `rehearsal_charts/2026-06-17_R006_NQ_judas_swing_15m_1m_3.jpg` (15m + 1m)
- `rehearsal_charts/2026-06-17_R006_NQ_judas_swing_15m_midline_4.jpg` (15m · 白箭头中位线实例)
**Symbol:** NQ | **类型:** 新素材草稿（不是完整素材，标注也需完善）

**核心场景：** Judas Swing 主要表现在**纽约开盘前后这一段时间市场的剧烈振荡**。从 15m / 5m / 1m 三图看，市场是剧烈的摆动。

**关注窗口：** 开盘后 15 分钟——即**纽约时间 6:30–6:45** 这一段的表现。

**结构标注（图上）：**
- **灰色区域** = 15m 的一个 **block**。
- **蓝色区域** = 5m 形成的一个 **block**。
- 上方同样有一个**红色 block**。

**交易判断（小区间的独立判断）：**
- 在这些区间之内，通过**快速计算**找**高盈亏比**的入场。
- 这是一个**小区间**，但已经超过 **100 个点位**。
- **前提条件（铁律）：** 必须有**极快的决定 + 极快的计算能力**，在**完全能掌控风控**的情况下 → 非常值得交易。

**⚙ 层级区分（重要）：** 这种判断是针对**一个小交易区间**的，跟**大的（区间震荡/等 7 点后 4H 绿柱）那个判断是两回事**。不要混。

**状态：** 这个标注也需要完善。**待整理进素材库**（与 AP 扩展、转折箱体、OB、1H 中位线一起）。

**🔄 R006 补充（图4，07:03 PDT；措辞已修正 07:16）— 长下影 / 小实体十字星（收在前根高点附近）的实例：**
- ⚠️ 更正：之前我误写为“实体中位线”。Chairman 纠正——这是一根**长下影 / 小实体十字星**。
- **白色箭头**那根：实体极小、影线与实体比例极不成比例，且**收在非常接近前一根线的高点**。
- **铁律（素材库已有的那句话）：** 看到这样的线 → **不可能直接上行，一定得先回来把这根线（引线）覆盖掉，然后才可能上行**。所以“后面很快被向下突破”= 先回来覆盖引线（图上紧接一根大红柱）。
- **归类（不合并）：** 原始实例已原样收入 `material_specimens/pinbar_doji/`（条目 S-002），与素材库【引线逻辑】规则交叉引用。
- **标注系统的使命（重点）：** 未来系统必须对这种**高概率 / 极好盈亏比的交易机会极快做出反应**。
- **人机分工的核心痛点（Chairman 明言）：** 这种机会 Chairman 自己做时**基本没办法操作、或不敢操作** → **这正是系统要解决的问题**。（与设计原则一致：减少人为参与 + 只在安全窗口人工）。

**区间价位参考（图4）：** STOP 红线 ~30,861.5；前高点 ~30,815；FVG++ (4H&15m) 黄区 ~30,720–30,750；15m 200SMA 白线 ~30,647；开盘绿区 上沿~30,375 / 下沿~30,275；今日可能触及位 ~30,191。当前价 ~30,364。

---

## R007 — 2026-06-17 15:31 PDT · 星期四日线级别预推演（ES + NQ 对照）
**Chart:** `rehearsal_charts/2026-06-17_R007_thursday_daily_ES_NQ_1D.jpg` (ES 1D + NQ 1D)
**Symbol:** NQ（主）+ ES（对照，未作主分析） | **级别：日线（首次上日线预推演）**

**关键位（NQ 日线）：**
- **STOP** 上移到**日线星期三的最高点**（前高线 ~30,815）。
- **向上可能达到位 = 星期三开盘价 = 星期二收盘价**（黄色位）。
- **向下：红色 overlap → Target** 这个位值得关注。
- **蓝色 4H 200SMA ~29,460**。

**期待的走势（推演）：**
- 大盘继续向上突破黄色可能达到位 → 在上面**盘整** → 但**不突破 STOP**。
- 这样形成**三天连续的高点向下** → 符合下跌行情。

**大趋势关注点（ES & NQ 共同）：**
- 关注 **X 热线**所形成的后续范围 — 正是这个范围使大盘到最高点时形成了**日线级别背离**，值得关注。

**周线：** 暂看不出太大趋势 → 暂不分析。

**ES 对照（辅助，不过多分析）：**
- ES 与 NQ 底部都画了**蓝色 OB++**。
- ⚠️ **ES 的 OB 更重要：** 它前面有一个重要位置——一根**反向红色柱体** → 更加重要。
- NQ **没有**这样的结构 → 所以目前不做过多分析。

**待补充 Outcome（星期四验证）：**
- [ ] 是否向上碰黄色位（周三开盘/周二收盘）后盘整、不破 STOP（周三高）？
- [ ] 是否形成三天连续高点向下？
- [ ] 是否向下去 Target / 4H 200SMA ~29,460？

**⚠️ 防火墙：** 推演，不是预测。日线背离 / OB / overlap 都是应用现有规则得出的 read。

### 🔄 R007 Update2 — 2026-06-17 20:17 PDT · 实际演进 + 佐证材料
**Chart:** `rehearsal_charts/2026-06-17_R007_update2_real_evolution_1m_15m.jpg` (NQ 1m + 15m)

**实际演进：** 市场就是按着预想的方向在走。目前仍未走出趋势。判断：还需要一根大的走势（无论向上或向下，或上下两个方向都走），市场才会离开这个区域。

**200日均线的重要观察：**
- 最初判断认为价格会下来碰到 200日均线 → 确实碰到了（验证）。
- 但 **均线本身已经向上移动**（均线也在带着走势）→ 实际碰到的位置比原先想象的**更高**。
- 疑问：趋势还会不会走到最初想象的那么低？—— 待观察。

**15m 震荡质量观察：**
- 15m 震荡没有产生很长的引线 → 影响反转的判断质量，对反转的**确认质量打折扣**。

**单独作为佐证材料：** 这张图是今天**素材（震荡区间 CHoCH/突破vs回调/三振反转原则）的实际市场作为佐证**——市场真实展现了我们素材里讨论过的结构形态。单独保存，不合并。

**⚠️ 防火墙：** 仍是推演 + 实际观察。

### 🔄 R007 Update1 — 2026-06-17 15:50 PDT · 星期四 4H+15m 交易级别分析（分水岭）
**Chart:** `rehearsal_charts/2026-06-17_R007_update1_thursday_4H_15m_divider.jpg` (NQ 15m + 4H)

**用途：** 这个 4H+15m 分析**指导每一天的交易**（比 R007 日线更落地到交易层）。

**结构标注：**
- **OB（STOP 区）：** 用昨天**最后一根 4H（也是最大一根）的下影线**画 OB → 作为 STOP（红框 ~30,585）。
- **MFVG：** 用**前一根绿色 4H 的下影线**画 MFVG（绿带 ~30,106）。
- **今日分水岭（15m）：** 一根**绿 15m、两边都是红线**，刚好在 **MFVG 上方** → 作为今天要看的**分水岭**。

**交易分水岭逻辑（今日核心）：**
- ✅ **15m 实线冲过分水岭 → 行情向上可能变大。**
- ✅ **一直无法突破 MFVG → 分界线处形成可作 STOP 的点 → 向下操作的好时机。**

**交易时机：** 1H 线还没结束；不认为亚盘值得交易 → **星期四交易看纽约开市**。

**联动：** 与 【进场时段判断原则】一致（亚盘不进、等纽约）；与 R005/R007 下行偏向一致（不破 MFVG/STOP → 向下）。
**⚠️ 防火墙：** 推演。分水岭/MFVG/OB 是应用现有规则的 read。

**🔄 R007 Update1b（15:58）— MFVG 线前移延长，论证其重要性：**
- 把绿色 MFVG 线**向前延长** → 覆盖了**昨日一个关键点位** → 可见这根线为何重要。
- **三重 confluence（决定这根线值得关注）：** ① 它所在位置**过去形成的走势**；② **今天所在的位置**；③ 它在**大的 4H 框架**上的位置。
- **判断：** 应该会在这个区域把大盘**走出一个 OB 的行情**。
- 衔接【大时区 Block 不可忽】：这正是“4H 引线/MFVG 高权重 + 跨日点位 confluence”的正面运用。

### 🔴 R007 Close — 2026-06-18 06:32 PDT · 收尾总结（失败推演，已关闭）
**Chart:** `austin_raw_archive/2026-06-18/0632_msg_R007_close_15m_4H_stop_broken.jpg` (NQ 15m + 4H)

**结果：** ❗ **推演失败** — 市场未按是昨天预想的路径走。

**关键事实：**
- 市场**冲破了 STOP 位**（昨天所设 STOP ~30,585），市场向上转折
- R007 原推演期待（不突破 STOP → 向下走向 Target）已无效
- “市场要先冲破蓝色 OB，才有可能去碑黄色 FVG”这个判断成立的机会不大

**彩答题（对照 R007 待寻题）：**
- [ ] 是否向上碘黄色位后盘整、不破 STOP？ → ❌ **已突破 STOP**
- [ ] 是否形成三天连续高点向下？ → ❌ 市场转上
- [ ] 是否向下去 Target / 4H 200SMA ~29,460？ → ❌ 未达到

**推演复盘记录（冲破一边构成缚口）：**
- 这次推演是首次小结为失败的案例。学习价值：**冲破 STOP 一边 = 原下行预期的结构基础崩塩，必须立即关闭这个推演、重新建立观点。**
- 不强轩纻散的失败推演公平有效：推演当时建立在已有的规则和结构上（R007 建立正确），市场没按运行不属于分析错误。

**状态：** 🔴 **已关闭**（ 2026-06-18 06:32 PDT）。后续推演将以 R008 开始。

**⚠️ 防火墙：** 已是推演结果记录。

---

## R008 — 2026-06-18 07:08 PDT · 今日新推演（R007失败后重建观点）
**Chart:** `rehearsal_charts/2026-06-18_R008_15m_4H_up_FVG_target.jpg` (NQ 15m + 4H)
**Symbol:** NQ | **时间框架：** 15m + 4H | **方向：** ⬆️ 向上

**背景：** R007 已关闭（失败推演，STOP被突破）。市场已转折向上，重新建立今日观点。

**关键结构更新：**
- **新 Block 层界：** 以 4H **十字星（蜀烛）的红色柱体部分**作为新的 OB Block （也是今早 0624 素材中该十字星的应用）
- **STOP：** 十字星下方（图标注 ~30,134 区域 / STOP 红虚线）
- **第一目标：** 填补黄色 FVG（FVG++ 4H & 15，当前价格上方 ~30,750+）

**关键位水平（图上可读）：**
- 前高点： ~30,815
- 黄色 FVG++ 4H & 15： ~30,750–30,815（第一目标）
- OB__（红色）： ~30,670–30,850（内含十字星）
- 当前价： ~30,570
- STOP： ~30,134（十字星下方）
- OB++（蓝色）： ~30,134 层
- Target（备注）： ~29,220 橙色线（更大级别参考，非今日目标）
- 4H 200SMA： ~29,080

**技术联动：** 与今日素材内容高度关联―
- 0624素材【被完全包裹的十字星 = 新一段上升起点】：这根十字星 + 其后大维柱上涌，正是该素材的实盘應用。
- 0624素材【关键位置突破后持续价值】：今日分界线作止损 / 入场参考。

**待验证（推演题）：**
- [ ] 价格是否到达黄色 FVG（~30,750+）？
- [ ] STOP (~30,134) 是否未被突破？
- [ ] 到达 FVG 后行情态态？（突破前高 / 回调 / 适度盘整）

**状态：** 🟡 **活跍**（2026-06-18 07:08 PDT开始）

**⚠️ 防火墙：** 推演不是预测。FVG 吸引 + 十字星结构 + OB Block 是应用现有规则的 read。

### 🔄 R008 Update1 — 2026-06-18 08:39 PDT · OB + STOP 上移，目标方向不变
**Chart:** `rehearsal_charts/2026-06-18_R008_update1_OB_STOP_moved_up.jpg` (NQ 15m + 1H)

**更新内容：**
- ⚫ **OB 上移：** Block 参考区域向上调整（~30,525–30,580 区域）
- ⚫ **STOP 上移：** ~30,134 → ~30,402（跟随 OB 向上锁定）
- ✅ 目标不变：黄色 FVG（~30,750+）
- ✅ 总体走势不变：方向仍为⬆️ 向上

**时间框架变化：** 左图依然 15m，右图由之前 4H 切换为 **1H**（中等级别确认）

**关键位务修订：**
- OB__（红/粉）： ~30,525–30,580
- OB++（蓝）： ~30,150–30,280
- STOP（红虚线）： ~30,402
- 白色虚线（今日分界线）： ~30,463
- 当前价： ~30,554
- Target（橙色）： ~29,260（大级别参考，非今日目标）

**⚠️ 防火墙：** 仍是推演。OB/STOP 上移 = 市场结构跟踪调整，不是新观点。

### ✅ R008 Update2 — 2026-06-18 11:33 PDT · 目标到达，今日推演关闭
**Chart:** `rehearsal_charts/2026-06-18_R008_update2_target_reached_5m_4H.jpg` (NQ 5m + 4H)

**更新内容：**
- ✅ **目标到达：** 市场已达到黄色 FVG 预期目标（~30,750+）
- 📌 今日推演至此结束，不再做进一步分析
- 📅 下一次推演：**今日（周四）收盘后**，做星期五的推演

**待验证结果（对照 R008 设定）：**
- [x] 价格是否到达黄色 FVG（~30,750+）？→ ✅ **YES，已到达**
- [x] STOP (~30,402) 是否未被突破？→ ✅ **YES，未触发**
- [ ] 到达 FVG 后行情形态？→ 待收盘后观察

**推演结论：** R008 **成功** — 从十字星 OB 出发，方向判断正确，目标达成。

**状态：** 🟢 **已关闭**（目标达成，2026-06-18 收盘前）

---

## R009 — 2026-06-21 14:48 PDT · NQ 上周推演收尾（周线级别）
**Chart:** `rehearsal_charts/2026-06-21_R009_NQ_lastweek_close_1W_4H.jpg` (NQ 1W + 4H)
**原稿底片:** `austin_raw_archive/2026-06-21/img_05_NQ_lastweek_rehearsal_close_1W_4H.jpg`
**性质:** 上周推演的**收尾总结**（本周推演待 18:00 盘后 + 看完开盘第一小时线后再做）

### 周线（1W）判断
- 上周形成一根**很小的周K** → 跟前一根线比 = **润线（inside bar / 母子线）**。
- 润线处在当前位置 → **有可能是市场转折的信号**。
- 但周线总体处在**较强上升动力**中 → **不判断周线级别会强烈下降**，但**有回调需求**。
- 周线结论：**仍是上涨。**

### 4H 判断（本周重点）
- 本周重点 = 判断 4H 是否**震荡**。
- 关键：只要**未突破 Sellside / Buyside 形成的交易区域** → 认为是震荡。
- **最可能走势（推演）：** 无论碰到与否向上第一目标位 → 都可能**向下填补绿色区域** → 碰到 **24小时均线**后再有向上冲可能。
- **但这些都不会在这一周内发生。**
- 4H 结论：**震荡为主，本周可能都以震荡为主。**

### 期望
- 希望周末结束时走出一个方向——**但不一定**。

### 状态
- 🟡 **上周推演收尾完成**。本周推演待盘后补上（看完开盘第一小时线）。

**⚠️ 防火墙：** 这是预演，不是预测，不是规则。周线上涨/4H震荡的读 = Chairman 当前中性心态下的读，仅记录。

---

## Prior charts to backfill
Chairman noted several earlier charts already shared should also be folded in as rehearsal records. **TODO:** locate and append as R000-series once Chairman points them out.
