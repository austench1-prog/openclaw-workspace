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

## Session 3 — 2026-06-16 (周二) ~09:42-10:46 PDT — PPT $1500 目标达成 ✅

**结果:四个真实考核账户全部命中 PPT $1500 目标。**

执行结构:Leader = **Sim101**,通过 **Replikanto v1.6.1.7** 跟单到 MFFU + TPT 跟随账户。
产品:**MNQ SEP26**(微型,主体)+ 部分 NQ SEP26。时段约 09:42-10:46 PDT(另有 6/15 尾盘几笔)。

### 真实考核账户(全部达标 ✅)

| 平台 | 账户 | Total PnL | Trailing Max Draw | 状态 |
|---|---|---|---|---|
| MFFU | MFFUEVRPD122274045 | **+$1,481.20** | $518.80 | ✅ 达标 |
| MFFU | MFFUEVRPD122274046 | **+$1,476.20** | $523.80 | ✅ 达标 |
| TPT | TAKEPROFIT152524137 | **+$1,494.00** | $506.00 | ✅ 达标 |
| TPT | TAKEPROFIT718789812 | **+$1,490.00** | $510.00 | ✅ 达标 |
| TPT | TAKEPROFIT800884314 | **+$1,489.00** | $511.00 | ✅ 达标 |
| TPT | TAKEPROFIT973527220 | **+$1,490.00** | $510.00 | ✅ 达标 |

→ **6 个真实账户全部命中 ≈$1,500 目标。** 全部平仓(Position=0),无隔夜风险。

### Sim / 内部账户(仅参考,不计考核)

| 账户 | Total PnL | 说明 |
|---|---|---|
| Sim101(Leader) | +$14,735.00 | 领单账户,虚拟 |
| SimFF | −$975.00 | 内部 Sim,不影响考核 |
| SimNQ | $0.00 | 未参与 |
| TAKEPROFITPRO704123103 | −$94.50 | PRO 账户,本次小幅回撤(非 $1500 目标账户) |

**记录要点:**
- **PPT $1500 = Profit Per Target,本日单日目标。四个真实考核账户全部达成。**
- 全部 Trailing Max Draw 健康(~$500 区间,远未触及风控硬线)。
- 平台:NinjaTrader 手动 + Replikanto 跟单,符合平台隔离原则。
- ⚠️ `TAKEPROFITPRO704123103` 当日 −$94.50,留意但金额极小、Trailing Draw $370.50 仍安全。

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

---

## Session 4 — 2026-06-16 早盘 (MNQ SEP26 全账户达标)

**执行时间:** 10:10 AM — 10:33 AM PDT
**领单:** Sim101 **跟单:** Replikanto v1.6.1.7 **品种:** MNQ SEP26

### 成绩
| 平台 | 账户 | 日盈亏 | 状态 |
|---|---|---|---|
| TPT | TAKEPROFIT152524137 | ~+$1,500 | ✅ 达标 |
| TPT | TAKEPROFIT718789812 | ~+$1,500 | ✅ 达标 |
| TPT | TAKEPROFIT800884314 | ~+$1,500 | ✅ 达标 |
| TPT | TAKEPROFIT973527220 | ~+$1,500 | ✅ 达标 |
| MFFU | MFFUEVRPD122274045 | **+$1,481.20** | ✅ Day 1 完成 |
| MFFU | MFFUEVRPD122274046 | **+$1,476.20** | ✅ Day 1 完成 |

**全部 6 个账户全部达标 ✅**

### 记录要点
- 成交证据：NinjaTrader 成交日志（Entry/Exit 名称带 03883AC3 信号 ID）已由 Chairman 截图确认。
- MFF 后台显示 $0：正常——Tradovate 日内盈亏当天收盘后才同步到 MFF 后台。
- 今日为 MFF 045/046 第 1 天，成绩顔卡下（不超 $1,600 一致性红线）✅。
- Replikanto 领单 Sim101 全链路正常，非手动盘中监控下执行。

*Session logged: 2026-06-16 | Dragon*
