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

⬜ = 待落地  ✅ = 已确认/已设  ❓ = 未知,需查 Tier1

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
