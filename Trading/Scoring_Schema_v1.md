# 评分字段规范 v1.0（Scoring Schema）
# 建立：2026-06-18 | 作者：Dragon
# 目的：给零件实例 + 推演日志补上标准化「结果字段」，使其可聚合统计（频率 / 胜率 / 期望值）。

---

## 这是什么 / 为什么需要

素材库现状（A 评估）：每条记录都有「形态长什么样 + 怎么判读」，
但**没有标准化的结果字段** → 无法聚合算分。

本规范定义一套**统一的、可机读的结果字段块**，挂到两个地方：
1. 零件实例（`material_specimens/<类目>/_INDEX.md` 的每条 S-xxx）
2. 推演日志（`Market_Rehearsal_Log_v1.md` 的每条 R-xxx 收尾）

**不破坏原稿铁律：** 底片（`austin_raw_archive/`）永不加这个；评分只加在②层（系统记录）。
原话、原图、判读全部保留，结果字段是**追加**，不是替换。

---

## 核心设计原则

1. **机读优先：** 字段用固定 `key: value` 格式，方便以后脚本聚合。
2. **R 为单位，不是钱：** 一切盈亏用 R（风险倍数）衡量，跨账户跨手数可比。
   - 1R = 入场价到 SL 的距离。盈利到 2 倍这个距离 = +2R。
3. **客观可判定：** 每个字段都能从图/价位明确读出，不靠感觉。
4. **未交易也要记：** 看到机会但没进场（flat），照样记 —— 这是频率分母的一部分。

---

## 标准结果字段块（STAT BLOCK）

复制以下模板，填好后追加到记录末尾。所有字段必填；无则填 `NA` 并注明原因。

```
--- STAT ---
component: <零件类型，如 pinbar_doji / order_block / fvg_first_edge / three_strike / candle_retracement>
direction: <long | short>
date: <YYYY-MM-DD>
symbol: <NQ | ES | GC | ...>
timeframe: <如 15m | 4H | 15m+4H>
session: <asia | london | ny_am | ny_pm | overnight>
entry: <入场价，或 NA(no-trade)>
sl: <止损价>
tp1: <第一目标价>
tp2: <第二目标价，无则 NA>
risk_pts: <入场到SL的点数 = 1R 的点数>
outcome: <win | loss | breakeven | no_trade | open>
r_result: <实际结果，以R为单位，如 +2.3 / -1.0 / 0 ；no_trade/open 填 NA>
hit_first: <tp | sl | none>   # 价格先碰到哪个
mae_r: <最大不利偏移，以R为单位，如 -0.6（进场后最多逆走多少）；可选，NA>
mfe_r: <最大有利偏移，以R为单位，如 +3.1（进场后最多顺走多少）；可选，NA>
quality: <A | B | C>   # 这次形态符合规则的严格度：A=四要素齐全/教科书；B=可接受；C=勉强
note: <一句话补充，可选>
--- END STAT ---
```

---

## 字段说明（关键几个）

- **component**：聚合的主键。同一 component 的所有记录汇到一起算分。
  类型命名要和 `material_specimens/` 类目对齐（见下方对照表）。
- **outcome**：
  - `win` / `loss` / `breakeven` = 实际进场并平仓
  - `no_trade` = 看到了符合的形态但没进（统计频率用，不算胜负）
  - `open` = 还没结束（活跃推演，待 update 收尾）
- **r_result**：胜率×R的核心。聚合时：
  - 平均期望值 = Σ(r_result) / N（只算已平仓的 win/loss/breakeven）
- **quality**：用来分层。可以只统计 A 级形态的胜率，对比 A vs B vs C，
  验证「严格筛选是否真的提高胜率」。

---

## component 命名对照表（与现有类目对齐）

| component 值 | 对应类目 / 规则 |
|---|---|
| `pinbar_doji` | material_specimens/pinbar_doji（引线/十字星反转） |
| `candle_retracement` | material_specimens/candle_retracement（反直觉K线） |
| `three_strike` | material_specimens/three_strike_reversal（三振反转） |
| `order_block` | 素材库正文【Order Block（OB）逻辑】 |
| `fvg_first_edge` | 素材库正文【FVG 多时间框架用法】（第一边进场） |
| `mfvg` | 素材库正文【MFVG 定义】 |
| `wick_sweep_ob` | 素材库正文【引线的双重用法】（sweep→OB+趋势线） |
| `harami_doji_start` | 素材库正文【被完全包裹的十字星=新段起点】 |
| `gap_down_quiet_breakout` | 素材库正文【大跳空下开盘·极静突破】 |

> 新增类型时，同步更新本表 + `material_specimens/` 建类目。

---

## 聚合统计输出格式（未来脚本目标）

填够样本后，按 component 聚合，输出每个零件的「跑分卡」：

```
=== 跑分卡：pinbar_doji ===
样本数(N)：       18   （其中已平仓 12，no_trade 6）
出现频率：        ~3次/周
胜率(win rate)：  58%   （7胜 5负）
平均盈利R：       +2.1R
平均亏损R：       -1.0R
期望值(EV)：      +0.8R/笔
A级胜率：         71%（7样本）  ← 严格筛选对比
B级胜率：         40%（5样本）
建议：            ✅ 可用，优先 A 级条件
```

判定门槛（初版，Chairman 可调）：
- EV > +0.3R/笔 且 频率 ≥ 2次/周 → ✅ 进组装候选
- EV 在 0 ~ +0.3R → ⚠️ 观察，需更多样本
- EV < 0 → ❌ 暂不用（或只在 A 级条件下用）

---

## 使用流程（落地）

1. **存量补录：** 把已有的 R001~R008 推演、各 specimen，逐条补 STAT BLOCK。
   - 能从图/记录读出结果的直接填；读不出的标 `outcome: open/NA` 待 Chairman 确认。
2. **增量标配：** 以后每条新推演/新实例，收尾时**必带** STAT BLOCK。
3. **定期聚合：** 攒够样本（建议每类 ≥ 20）→ Dragon 跑聚合 → 出跑分卡 → Chairman 看分决定取舍。
4. **组装：** 跑分好的零件 → 进入策略组装候选池。

---

## 边界（防火墙，承接现有铁律）

- STAT BLOCK 只描述**客观结果**，不改写 Chairman 原话/判读。
- no_trade 的记录同样有价值（频率分母），不要因为"没交易"就不记。
- 推演（rehearsal）的 r_result 是**纸面结果**（若当时不进场，按推演路径计），
  要和**实盘 r_result** 分开标记 —— 用 `note:` 注明 `paper` 或 `live`。
  （这对应 MEMORY 里"推演胜率 vs 实盘胜率"的长期对比价值。）

---

*规范作者：Dragon | 待 Chairman 审阅后启用*
