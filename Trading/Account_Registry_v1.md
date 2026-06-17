# Account Registry (实时账户登记表) v1

**Owner:** Chairman (Austin) | **Maintained by:** Dragon
**Created:** 2026-06-16
**Source rule (铁律):** 合规数据只能来自 **Tier 1**(账户后台/合同本身),不靠网站或记忆。
**SOP:** 新账户用 4 月已定流程 - Tier 1 读规则 → 填本表 → 跑 Line 1 兼容性检查 → 全过 onboard。

> 本表只登记**当前实际在跑的账户**。旧账户见 `Eval_Account_Risk_Form_v1.md`(历史)。

---

## 总表(当前活跃账户)

| 平台 | 账号 | 计划 | 阶段 | 盈利目标 | Max DD | DD类型 | 一致性 | 最低天数 | $1600/日自动平仓 | 规则来源 | 状态 |
|---|---|---|---|---|---|---|---|---|---|---|---|
| MFFU | MFFUEVRPD122274045 | **Rapid** | Eval | $3,000 | $2,000 | EOD Trailing | **50%** | 2 | ⬜ 未设 | ✅ Tier1 后台 06-16 | 🟢 In Progress |
| MFFU | MFFUEVRPD122274046 | **Rapid** | Eval | $3,000 | $2,000 | EOD Trailing | **50%** | 2 | ⬜ 未设 | ✅ Tier1 后台 06-16 | 🟢 In Progress |
| TPT | TAKEPROFIT152524137 | Test $50K | Eval | $3,000 | $2,000 | Trailing | **50%** | **5** | ⬜ 未设 | ✅ Tier1 CQG 06-16 | 🟢 进行中 |
| TPT | TAKEPROFIT718789812 | Test $50K | Eval | $3,000 | $2,000 | Trailing | **50%** | **5** | ⬜ 未设 | ✅ Tier1 CQG 06-16 | 🟢 进行中 |
| TPT | TAKEPROFIT800884314 | Test $50K | Eval | $3,000 | $2,000 | Trailing | **50%** | **5** | ⬜ 未设 | ✅ Tier1 CQG 06-16 | 🟢 进行中 |
| TPT | TAKEPROFIT973527220 | Test $50K | Eval | $3,000 | $2,000 | Trailing | **50%** | **5** | ⬜ 未设 | ✅ Tier1 CQG 06-16 | 🟢 进行中 |
| TPT | TAKEPROFITPRO704123103 | ❓PRO阶段 | PRO/Funded | ❓ | ❓ | ❓ | ❓ | ❓ | ⬜ 未设 | ⏳ 待 Tier1 | 🟡 PRO待确认 |
| Tradeify | TDFYG50971738857 | Growth 50k | Eval | $3,000 | $2,000 | EOD Trailing | ⚠待确 | ⚠待确 | ⬜ 未设 | ✅ Tier1 06-16 | 🟢 活跃 |
| Tradeify | TDFYG50640402386 | Growth 50k | Eval | $3,000 | $2,000 | EOD Trailing | ⚠待确 | ⚠待确 | ⬜ 未设 | ✅ Tier1 06-16 | 🟢 活跃 |
| Tradeify | TDFYG50785559546 | Growth 50k | Eval | $3,000 | $2,000 | EOD Trailing | ⚠待确 | ⚠待确 | ⬜ 未设 | ✅ Tier1 06-16 | 🟢 活跃 |
| Tradeify | TDFYG50653786524 | Growth 50k | Eval | $3,000 | $2,000 | EOD Trailing | ⚠待确 | ⚠待确 | ⬜ 未设 | ✅ Tier1 06-16 | 🟢 活跃 |
| Tradeify | TDFYG50758269179 | Growth 50k | Eval | $3,000 | $2,000 | EOD Trailing | ⚠待确 | ⚠待确 | ⬜ 未设 | ✅ Tier1 06-16 | 🟢 活跃 |
| TradeDay | ELTDER260603051540656386 | $50k Intraday QP | Eval | ❓(~$3,000) | Intraday Trail | **Intraday** | 有 | 有 | ⬜ 未设 | ✅ Tier1 06-16 | 🟢 评估中 |

⬜ = 待落地  ✅ = 已确认/已设  ❓ = 未知,需查 Tier1  ⚠ = 后台未明示待合同确认

### TPT Test $50K 规则详情 (Tier1 CQG 后台实读 2026-06-16)
| 项 | 内容 |
|---|---|
| Prop Firm | TakeProfitTrader (TPT) |
| 计划 | Test $50K (One-Step) |
| 盈利目标 | **$3,000** |
| Max DD | **$2,000** (最低余额 = 起始 - $2,000, **Trailing**) |
| **一致性** | **50%** — 最大单日 ≤ 总盈利 50% |
| **最低交易日** | **5 天**（≠ MFF 的 2 天！） |
| 最大合约 | **6** |
| 持仓截止 | **17:00 EST**（注意是 EST，非 CT） |
| Counter Positions Rule | 不得违反 |
| 允许产品 | CME / COMEX / NYMEX / CBOT 期货；禁股票/期权/外汇/加密/CFD |
| 平台 | **CQG** |
| 状态示例(800884314) | 余额 $51,591，已赚 $1,591/$3,000，最大单日 49.63%，已交易 2/5 天 |

⚠️ **TPT vs MFF 关键差异：** 两者一致性都是 50%，但最低交易日 TPT=5 / MFF=2。**3 天通过方案只适用 MFF，不能套 TPT**（TPT 必须凑足 5 个交易日）。

### Tradeify Growth 50k ×5 规则详情 (Tier1 后台实读 2026-06-16)
账号：TDFYG50971738857 / 50640402386 / 50785559546 / 50653786524 / 50758269179
| 项 | 内容 |
|---|---|
| Prop Firm | Tradeify |
| 计划 | **Growth 50k** (Evaluation) |
| 盈利目标 | **$3,000** |
| Max DD | **$2,000** (Trailing, EOD) |
| **核心规则(One Rule)** | 不得跨过 $2,000 trailing max drawdown——官方强调「只有一条规则」 |
| **每日亏损限制** | **$1,250** (Daily Loss Limit) |
| Microscalping | 有检查(compliant)——允许但监控 |
| 一致性 | ⚠️ 后台未显示 consistency %（Tradeify Growth 以「无一致性规则」著称，待 Tier1 合同最终确认） |
| 最低交易日 | ⚠️ 后台未明示，待确认 |
| 平台 | **Tradovate** |
| 状态(全部) | 活跃，最后交易 Jun 10；当前都在亏损状态（~ -$840） |

### TradeDay $50k Intraday Eval (QP) 规则详情 (Tier1 后台实读 2026-06-16)
账号：ELTDER260603051540656386
| 项 | 内容 |
|---|---|
| Prop Firm | TradeDay |
| 计划 | **$50k Intraday Evaluation (Quick Pay)** |
| **DD 类型** | ⚠️ **Intraday Trailing**（不是 EOD！与 MFF/Tradeify 不同） |
| 最大回撤底线 | $48,370.15（当前） |
| 账户余额 | $48,445.30 |
| 持仓限制 | **5 合约 / 50 微型** |
| **一致性** | 有一致性规则（后台显示 Consistency: Pass；⚠️ 曾因不一致导致盈利目标上调） |
| 限制交易事件 | 有 Tier1 News Events 限制（FOMC/CPI/就业报告等） |
| 更新时间 | 每日 17:30 CT |
| 平台 | （待确认，Quick Pay） |
| 状态 | Being Evaluated；Days Traded: Pass / Consistency: Pass / Profit Target: Keep Trading |

⚠️ **TradeDay 关键差异：** DD 是 **Intraday Trailing**（日内浮盈也会抬底线，收盘后不锁），跟 MFF/Tradeify 的 EOD Trailing 机制不同。风控计算要分开。还有 **不一致会抬高盈利目标** 的特殊机制。

---

## 🟡 需挑救账户组 (RESCUE GROUP) — 6 个低余额账户（Chairman 2026-06-17 锁定）

**决策：不放弃。** 这 6 个 trailing 只剩 ~$75–100 的账户标注为「需挑救」，作为**一组合并**看，寻找机会和策略。

| # | 账户 | 平台 | 剩余 buffer | 推断到期 |
|---|---|---|---|---|
| 1 | TDFYG50971738857 | Tradeify | ~$75–100 | ~2026-07-04 |
| 2 | TDFYG50640402386 | Tradeify | ~$75–100 | ~2026-07-04 |
| 3 | TDFYG50785559546 | Tradeify | ~$75–100 | ~2026-07-04 |
| 4 | TDFYG50653786524 | Tradeify | ~$75–100 | ~2026-07-04 |
| 5 | TDFYG50758269179 | Tradeify | ~$75–100 | ~2026-07-04 |
| 6 | ELTDER260603051540656386 | TradeDay | $75.15 | ❓ 待查 |

**到期推断（Chairman 认可）：** 按购买日 2026-06-04 + 30 天 → **~2026-07-04 到期**（可能同一天买的）。
**挑救窗口：** 看起来还有 **~10 个交易日**可挑救。
**下一步（Chairman）：** 把 6 个合并看，寻找一个机会/策略。

⏳ **待办：**
- [ ] TradeDay 账户准确到期日（后台再查）。
- [ ] 6 账户“合并挑救”策略（待 Chairman 出机会/策略）。
- [ ] Tradeify Eval 30 天期限后台最终确认（是否真是 7/4）。

---

## 🔴 RESET / RENEW / 到期 · 后台实读 (Dragon 登后台 2026-06-17)

> 背景：6 个账号 trailing 只剩 ~$75–100，需在到期/续费前处理。原稿：`austin_raw_archive/2026-06-17/_RAW.md` [09:32/09:34/09:39]。

### Tradeify Growth 50k ×5（TDFYG）
- **购买：** 2026-06-04，**Growth $50k Pack 5**（一次买5个），**$447.20** 成功一笔 → 单个均价 **~$89.44/个**。卡 VISA •8869。
- **⚠️ 关键发现：Eval 账户没有 auto-renew 订阅。** Billing→Subscriptions 筛 Active = **“No subscriptions found”**。只有 Funded 账户才有月订阅/auto-renew；这 5 个是一次性买断的评估账户。
  → **意味着：不存在“续费日自动扣款”的危险；也不会自动给新 $2,000。账户死了就是死了，要继续只能主动 Reset 或买新。**
- **Reset 价 = $95.00/个**（后台 Reset 弹窗实读，卡 VISA •8869）。Reset = 重置到起始，但用原周期剩余时间。
- **买新价 ≈ $89.44/个**（Pack 5 均价；单买可能更高，待核）。
- **💡 初步结论：买新（~$89）比 Reset（$95）便宜**——且买新是全新 30 天 + 全新 $2,000，Reset 只用剩余时间。**除非单买价高于 $95，否则买新更划算。待确认单买价。**

### TradeDay $50k Intraday（ELTDER260603051540656386）
- **余额 $48,445.30 / DD 底线 $48,370.15 → 只剩 $75.15 buffer**（合之前 $75）。
- **TradeDay 2.0 促销（后台横幅）：** 账户起价 **$62.50**，**全场 50% OFF，码 TDNEW**。→ 买新很便宜。
- ⏳ **待查：** TradeDay 的 Reset 价 + 到期/续费日（dashboard 未显示；Shop/billing 待进一步）。

### ⚠️ 剩余缺口
- [ ] Tradeify 单买（非 Pack）价格 — 确认是否 > $95（决定买新 vs Reset）。
- [ ] TradeDay Reset 价 + 到期日。
- [ ] ⚠️ **到期日未查到：** Tradeify Eval 既然无订阅，可能是“买断后一直有效直到账户死/过期”——需确认 Eval 账户是否有“30 天使用期限”。若有，购买日 2026-06-04 + 30 天 = **约 2026-07-04 到期**（待后台确认）。

---

## 账户详情

### MFF Rapid 50K ×2 (MFFUEVRPD122274045 / 274046)

| 项 | 内容 (Tier1 后台实读 2026-06-16) |
|---|---|
| Prop Firm | MyFundedFutures (MFFU) |
| 计划 | **Rapid** |
| 阶段 | Evaluation (In Progress) |
| 账户规模 | $50,000 |
| 盈利目标 | **$3,000** |
| 起始余额 | $50,000 |
| 最低余额(DD底线) | **$48,000** (= Max DD $2,000) |
| Drawdown 类型 | **EOD Trailing**(收盘后才移动;日内浮盈不动底线;到 起始+$100 后锁 Static) |
| 日亏限制 | ❌ 无 |
| **一致性规则** | **50%(仅 Eval 阶段)** - 单日盈利 ≤ 总盈利的 50% |
| 最低交易日 | **2 天**(后台显示 "2 today min" = Minimum Trading Days) |
| 新闻交易 | ✅ 允许 (T1 News: Yes) |
| 自动化/跟单 | ✅ 官方允许 copy trading |
| 平台 | Tradovate |

---

## ⭐ MFF Rapid 通过路线图 (3 天方案 · 仅适用 MFF)

> ⚠️ **仅适用 MFF。不可直接套用于 TPT** - 两平台 50% 一致性相同,但**最低交易日不同**,路线图必须分别重算。

**一致性数学(锁定,与 TPT 算法一致):**
- 规则:单日盈利 ≤ 总盈利 × 50% → 反推 **总盈利 ≥ 最大单日 × 2**,且 ≥ $3,000。
- **2 天通过 ≈ 不可能:** 两天必须分毫不差相等。实操不可行。

**✅ 3 天方案(每日上限 $1,500):**

| 交易日 | 目标 | 累计 | 说明 |
|---|---|---|---|
| 第 1 天 | **~$1,500**(不超) | $1,500 | 已完成 2026-06-16 (045: +$1,481 / 046: +$1,476) |
| 第 2 天 | **~$1,500**(不超) | ~$3,000 | 待执行 → 第 3 天只需小额盈利凑天数即可。 |
| 第 3 天 | **小额盈利（~$20-25）** | ≥$3,000 | 凑第 3 个交易日，让总额超 $3,000 且 Day 2 占比 <50%。具体最小盈利按 Day 2 实际数字 R-T3 动态计算。 |

**🚨 红线:第 1、2 天任一天都不能 > $1,500**。Day 1 实际 045/046 = $1,481/$1,476 卡线下 ✅。

**风控触发逻辑(待落地;动态,按 R-T3 逐日计算):**
```
# 第1/2天 (决盈利阶段,上限 $1,600)
if mff_daily_pnl >= 1600 * 0.90:  发警告("接近 $1600")
if mff_daily_pnl >= 1600:         FLATTEN_ALL()
if mff_daily_loss >= 200:         FLATTEN_ALL()  # 默认亏损上限;R-T3 按当日实况动态调整
# 第3天 (已达 $3,200,风控转为限亏)
if mff_daily_loss >= 200:         FLATTEN_ALL()  # 保住 50% 一致性条件
# A方案(最高目标) = Tradovate Risk Settings 每日最高盈利自动平仓
```

---

## OPEN ITEMS (缺口)

- ⬜ **TPT 账户(×4)的 Tier1 规则** - 计划名/盈利目标/Max DD/最低天数 待从 TPT 后台读。(同 MFF,我自己登 TPT 后台读 → 需 TPT 凭据存入)
- ⬜ **TPT PRO 账户(704123103)规则** - PRO/Funded 阶段规则待确认。
- ⬜ **$1600 自动平仓 落地** - 通过 Tradovate「每日最高盈利」风控功能,交易中自动平仓(白名单 #1,最高目标)。
- ⬜ **凭据库扩展** - 已存 MFF;待加 TPT、Tradovate,实现"我自己登、自己读"的连续性。

---

*Created: 2026-06-16 | Dragon | 活跃账户的单一真相源 (single source of truth)*
