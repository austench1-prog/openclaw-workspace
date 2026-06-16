# Trading Strategy & Order Strategy Framework
# Source: OpenAI output (Chairman provided)
# Date: 2026-04-09
# Status: Notes - for future Strategy Pack development
# Last updated: 2026-06-16

---

## ⚡ EXECUTION HARD RULES (执行铁律 — 优先于一切具体策略信号)

> 这些是风控/执行级铁律,**优先级高于下面所有具体策略信号**。任何具体信号与这些铁律冲突时,**铁律优先**。
> 来源:Chairman 2026-06-16 复盘 R003（见 `Market_Rehearsal_Log_v1.md`）。预演→规则的手动迁移。

### R-T1 — 交易时段铁律（最高优先级·风控级）
- NQ / ES 类产品,**亚盘通常不是我们的主要交易时段 → 原则上不主动参与亚盘交易**。
- **唯一例外:** 开盘第一小时的「1 小时级别」分析中,出现**非常明确且强烈**的入场条件时,才可考虑亚盘入场。
- 亚盘若不满足这种强条件 → **不勉强交易,优先等待纽约时段的交易机会**。
- **此条优先于所有具体策略信号。**

### R-T2 — 避免自相矛盾的决策（判断与行为必须一致）
- 当规则已判定「当前不应入场」（如:R-T1 亚盘未达强条件、开盘第一小时、区间未突破/震荡）→ **不得因为看到一个交易机会就入场**。
- 规则说不做、行为却做了 = **自相矛盾的决策,必须避免**。
- **后果佐证（成败问题的根源）:** 区间未突破时区间内全是震荡 → 方向难确定 → **止损点无法真正成立** → 目标再明确也是无效入场。

### 两条关系
**R-T1 先判定「这个时段/窗口能不能做」（门槛）；R-T2 守住「判定了不做就不能反悔」（纪律）。**

### R-T3 — 动态风控原则（按日按况设定当日风控线）
- **风控不是一劳永逸的。** 必须按**当天 / 当时 / 该账户的目标 + 当时账户实况**动态决定当日风控线。
- 当日风控线**不一定是盈利目标** — 可能是一个**最大亏损额**。
- **例(MFF Rapid):** 前两天各 $1,600(总 $3,200)。第 3 天的风控 **不是「盈利多少」,而是「最大亏损 ≤ $200」** — 只要亏不超 $200,总额仍 ≥$3,000 且 $1,600 占比仍 <50%,通过条件锁死。
- **意义:** 这就是用风控来管理考核账户、达到通过目标。人手算太繁杂 → **系统的真正价值 = 逐日动态算出“今天真正的风控线”。**
- 📌**加注(系统必备条件,非现在立即做):** 后续要为**每一类账户**创立专属的**计算公式**(根据该类账户的要求)→ 自动输出当日风控线。列入系统路线图。

### 附:「好 / 坏交易时机」判定标准（R-T2 的补充定义）
- **好的交易时机** = 目标明确 **且** 止损也明确、有合理性（两者同时成立）。
- **坏的交易时机**（以 R003 为例）:价格走向看得很明确（FVG + 均线强力吸引）,**但交易区间无明显突破 → 区间内全是震荡 → 止损点无法成立** → 目标明确 vs 止损不成立 = 无效入场。

---

## Overview

The complete trading system = Trading Strategy + Order Strategy

- **Trading Strategy**: Defines the opportunity (whether to trade)
- **Order Strategy**: Optimizes execution (how to trade well)

---

## Trading Strategy

### Core Responsibility
- Identify and confirm if a trade opportunity exists
- Determine: Original Entry Point, Base Stop Loss, Base Take Profit
- Form the **Trading Zone** (the operating boundary)
- Define: Minimum profit target + Maximum loss boundary

### Trading Zone Formation
1. **Manual**: Trader draws the zone based on experience/judgment
2. **System-generated**: System learns from trader's manual zones and auto-generates

### Key Distinction
Trading strategy answers: **"Is this trade worth taking?"**
It defines the basic feasibility, NOT the execution optimization.

---

## Order Strategy

### Core Responsibility
- Operates WITHIN the trading zone defined above
- Does NOT redefine the opportunity
- Subdivides the zone into smaller regions with different execution logic

### What It Solves
1. **Position Management**: How to enter in batches, allocate size, adjust dynamically
2. **Profit Optimization**: Layered execution to maximize efficiency
3. **Loss Control**: Compress losses, approach "minimal loss" state

### Core Principle
> Find smaller opportunities within the larger trading zone.
> Use finer execution to dynamically optimize risk/reward.

---

## Relationship

| | Trading Strategy | Order Strategy |
|---|---|---|
| Decides | Which battle to fight | How to fight it |
| Layer | Opportunity identification | Execution optimization |
| Output | Entry/SL/TP/Zone | Position sizing, scaling, timing |
| Missing it causes | Right direction, poor execution | Precise execution, wrong direction |

**Both are required.** Neither alone is sufficient.

---

## Complete Trading Cycle

```
Step 1: Trading Strategy → Entry Point + SL + TP → Trading Zone
Step 2: Determine zone source (manual or system-generated)
Step 3: Order Strategy subdivides zone → different execution per sub-zone
Step 4: Execute: position mgmt + risk control + profit optimization
Step 5: Complete cycle = Trading Strategy + Order Strategy
```

---

## Tree Structure

```
Trading System
├─ Trading Strategy
│  ├─ Define opportunity
│  ├─ Original entry point
│  ├─ Base stop loss
│  ├─ Base take profit
│  ├─ Form trading zone
│  ├─ Manual zone drawing
│  ├─ System-learned zone generation
│  └─ Min profit / max loss boundaries
│
└─ Order Strategy
   ├─ Based on trading zone
   ├─ Subdivide into smaller regions
   ├─ Match different order methods per region
   ├─ Position management
   ├─ Loss control
   ├─ Profit optimization
   └─ Execution enhancement in micro-opportunities
```

---

---

## 原始中文树状图（来源）

```
交易系统
├─ 交易策略
│  ├─ 定义交易机会
│  ├─ 确定原始进场点
│  ├─ 确定基础止损点
│  ├─ 确定基础止盈点
│  ├─ 形成交易区间
│  ├─ 手动画区间
│  ├─ 系统学习生成区间
│  └─ 定义最小盈利目标与最大亏损边界
│
└─ 下单策略
   ├─ 基于交易区间展开
   ├─ 划分更小区域
   ├─ 匹配不同下单方式
   ├─ 管理头寸
   ├─ 控制亏损
   ├─ 优化盈利
   └─ 在小机会中强化执行

最终闭环
= 交易策略 + 下单策略
= 完整交易系统
```

---

*Source: OpenAI | Recorded: 2026-04-09 | Use for Strategy Pack (Phase 6) development*
