# 待办清单 & 需求清单
> 建立日期：2026-04-04 | 配合 Multi_Agent_Trading_System_v1.0.md

---

# 一、待办清单（按优先级）

## 🔴 本周必做（2天内）

- [ ] **Prop Rule Copilot v1** — 录入4家 Prop Firm 规则，能回答"今天能不能开仓"
- [ ] **Pre-market Brief v1** — 每天盘前自动推送财经日历 + 账户状态摘要
- [ ] 确认各 Prop Firm 账户当前规模和余额
- [ ] 更新 `prop-firm-rules.md` 账户实际数据
- [ ] **SPX 0DTE AI 辅助提醒 v1** — 0DTE 不适合纯手动，优先做 setup 提醒+信号 alert

## 🟡 本月内完成

- [ ] **平台信息 Agent v1** — Prop Firm 规则自动更新 + 折扣/活动跟踪
- [ ] **Review Copilot v1** — 收盘后自动生成复盘模板
- [ ] SPX 0DTE 策略结构化（把策略思路写成可执行规则）
- [ ] **IBKR 公司账户开设**（银行账户完成后优先做，SPX 0DTE 自动化的基础）
- [ ] IBKR `ib_insync` 连接测试脚本（账户开好后立刻做）
- [ ] TOS Alert → Webhook → AI 过滤 → IBKR 下单（半自动 0DTE 流程）
- [ ] AMP Futures API 接入评估

## 🟢 第2-3个月

- [ ] **Trade Gatekeeper v1** — TradingView Webhook → AI 过滤 → 执行
- [ ] **总风控 Agent v1** — 全账户合并风险监控
- [ ] Prop Firm 账户目标计划（第一个月通过目标数量）
- [ ] 两家 LLC CEO Agent 架构决策
- [ ] 环境部署能力差距清单

## 🔵 长期（3个月以上）

- [ ] 执行 Agent（统一下单，多平台）
- [ ] 资产配置 Agent（策略效率排名 + 资金调度）
- [ ] 期权专属模块（SPX 0DTE 独立策略系统）
- [ ] 多 Agent 协作完整架构上线

---

# 二、需求清单（按模块）

## Agent 1：平台信息 Agent

| 需求 | 类型 | 优先级 |
|---|---|---|
| 读取并更新4家 Prop Firm 规则 | 数据 | 🔴 高 |
| 跟踪 Prop Firm 折扣/促销活动 | 信息 | 🟡 中 |
| 支持 payout 申请流程指引 | 流程 | 🟡 中 |
| 新考试账户开设步骤指引 | 流程 | 🟡 中 |
| 跟踪 X / YouTube 关注来源 | 信息 | 🟢 低 |

## Agent 2：策略信号 Agent

| 需求 | 类型 | 优先级 |
|---|---|---|
| 策略条件结构化输入 | 规则 | 🔴 高 |
| 盘中 setup 成熟提醒 | 实时 | 🟡 中 |
| 信号漏检补救（异常提醒）| 实时 | 🟡 中 |
| 多品种/多 timeframe 支持 | 扩展 | 🟢 低 |

## Agent 3：执行 Agent

| 需求 | 类型 | 优先级 |
|---|---|---|
| 统一接收各 Agent 下单建议 | 核心 | 🟡 中 |
| 人工审批流程 | 安全 | 🔴 高 |
| 多平台下单支持 | 扩展 | 🟢 低 |
| 订单记录 | 日志 | 🟡 中 |

## Agent 4：总风控 Agent

| 需求 | 类型 | 优先级 |
|---|---|---|
| 全账户每日亏损合并计算 | 核心 | 🔴 高 |
| 当日盈利目标追踪 | 核心 | 🔴 高 |
| 账户间风险叠加分析 | 分析 | 🟡 中 |
| 策略效率排名 | 分析 | 🟢 低 |
| 资金分配建议 | 分析 | 🟢 低 |

## Prop Firm 模块

| 需求 | 类型 | 优先级 |
|---|---|---|
| Apex 规则：Intraday Trailing DD 计算 | 核心 | 🔴 高 |
| TPT 规则：Prime vs Classic 判断 | 核心 | 🔴 高 |
| MFF 规则：50% 一致性检查 | 核心 | 🔴 高 |
| TradeDay 规则：数据日过滤 + 30% 一致性 | 核心 | 🔴 高 |
| Payout 进度追踪 | 追踪 | 🟡 中 |

## 平台接入

| 平台 | 接入方式 | 状态 |
|---|---|---|
| TradingView | Webhook | 🟡 计划中 |
| NinjaTrader / Tradovate | API | 🟡 计划中 |
| AMP Futures | API | 🟡 评估中 |
| TOS | Webhook / 截图 | 🟢 待定 |
| IBKR | ib_insync | 🟢 待开户 |

---

# 三、思维导图结构（文字版）

```
龙大哥交易 AI 系统
│
├── 信息层
│   ├── 平台信息 Agent
│   │   ├── Prop Firm 规则库
│   │   ├── 行业信息追踪（X / YouTube）
│   │   └── 平台操作流程支持
│   └── 市场信息（财经日历 / 新闻）
│
├── 分析层
│   ├── 策略信号 Agent
│   │   ├── SPX 0DTE 策略模块（独立）
│   │   └── 期货策略模块（ES / NQ）
│   └── Camarilla 指标库（F1 / F2 / F3 / 周线）
│
├── 决策层
│   ├── Prop Rule Copilot（今天能不能开仓）
│   ├── Trade Gatekeeper（信号准入过滤）
│   └── 总风控 Agent（全账户合并风险）
│
├── 执行层
│   ├── 执行 Agent（统一下单）
│   │   ├── Apex 账户
│   │   ├── TPT 账户
│   │   ├── MFF 账户
│   │   ├── TradeDay 账户
│   │   └── 自有账户（TOS / AMP / IBKR）
│   └── 人工审批节点（Human-in-the-loop）
│
├── 复盘层
│   ├── Review Copilot（每日收盘复盘）
│   └── Pre-market Brief（每日盘前摘要）
│
└── 管理层
    ├── APM LLC Agent
    ├── Meritpoint Logic LLC Agent
    └── 资产配置 Agent（策略效率 + 资金调度）
```

---

*此文件随项目推进持续更新，不设版本上限*
