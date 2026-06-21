# 实例集类目：OB 综合结构（Order Block + Sweep + 趋势线 + 多周期）

**类目创建：** 2026-06-20
**核心：** 收录「多周期综合分析图」——OB 框 + sweep point + 三点成线趋势线 + buyside/sellside 流动性 混合在一张图上的真实分析实例。这类图不是单一K线形态，而是结构级综合判读。

> 管理铁律见 `Trading_Material_Library_v1.md` →【素材库管理铁律】。
> 本文件只做**归类索引**。原始图与 Chairman 原话**原样保留、不合并、不修改**。
> 规则（抽象逻辑）在素材库正文，本目录只收**原始实例（specimen）**。

---

## 关联规则（指向素材库正文，不复制不改写）
- 【Order Block（OB）逻辑】：`Trading_Material_Library_v1.md` L354 专章。
- 【引线的双重用法（sweep point → OB + 趋势线）】：L282-303。
  - 第一重：引线 → 动态 OB；OB 随新 sweep point 不断重定义（黄色原始 OB → A/B 之间蓝色 OB++）。
  - 第二重：三点成线趋势线（防加戏硬约束，两点不够不下结论）。
  - 分工：OB 管「区间内」，趋势线管「突破后怎么走」。
- 【大时区 Block 不可忽视】：L174-182。大时区（4H）OB 是高权重结构，优先级高于小时区细节。

---

## Specimen 列表（按时间倒序）

### S001 — 2026-06-14 · NQ · OB 随 sweep 动态升级 + 三点成线趋势线
- **图：** `material_specimens/ob_composite/2026-06-14_NQ_OB_sweep_dynamic_trendline.jpg`
- **原稿：** `austin_raw_archive/2026-06-14/1650_trendline_OB_sweep_point_4h_trend.jpg`
- **图上元素：** 蓝色需求/OB++ 区、左侧黄色旧 OB 区、底部 sellside liquidity、右上 buyside liquidity、sweep point、A-F 点位、多条从低点扇形向上的趋势线。
- **判读（对应正文【引线的双重用法】）：** sweep 后 old block 随新关键低点重定义为新 OB；趋势线经至少三点确认后使用，观察价格突破交易区间后的反应。
- **类型：** OB 动态升级 + 三点成线 的标准综合实例。

### S002 — 2026-06-14 · NQ · OB + buyside/sellside 流动性结构
- **图：** `material_specimens/ob_composite/2026-06-14_NQ_OB_buyside_sellside_liquidity.jpg`
- **原稿：** `austin_raw_archive/2026-06-14/1657_ICT_wick_OB_buyside_sellside.jpg`
- **图上元素：** Buyside/Sellside 水平虚线、底部蓝色 OB++ 区、左侧黄色 OB 区、A-F 点位、多条趋势引线。A/C 附近长下影 Pin Bar。
- **判读：** liquidity sweep + OB 反转思路；与 S001 同一段行情的流动性标注版本。

### S003 — 2026-06-13 · NQ · 大周期 OB 对小周期影响（MTF）
- **图：** `material_specimens/ob_composite/2026-06-13_NQ_MTF_4h_OB_bigframe.jpg`
- **原稿：** `austin_raw_archive/2026-06-13/2114_NQ_MTF_4h_OB_order_block_analysis.jpg`
- **图上元素：** 大/小周期并排，黄色大区间框、竖向时间分隔、走势路径，叠加中文说明。
- **判读（对应正文【大时区 Block 不可忽视】）：** 大周期 OB 对小周期行情的影响、sweep point、sellside、PD 阵列概念。

---

## 待 Chairman 补充 / 实战持续收集
- 底片里还有大量同类综合图（6/10-6/16 多张 4H+15m 双图），实战中每遇到清晰的综合结构 → 原样收进本目录，新增 S-xxx 条目。
- 注意区分：本类目收**综合结构**；单一形态（pinbar/doji/反直觉回调/三振）各归专门类目，不混。

---

*管理铁律见 `Trading_Material_Library_v1.md` →【素材库管理铁律】*
