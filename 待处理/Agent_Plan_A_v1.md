# 方案一 (Plan A) - Agent Teams 架构规划
# Source: 外部参考（吉米/Gemini 整理）
# Date: 2026-04-07
# Status: 独立方案，待与方案二对比

---

## 核心定位
自动下单链路已跑通，v1 直接纳入使用。
重心从"能不能自动下单"转为"让执行前的判断可信"。

---

## v1 四个核心模块

### 模块 A：Prop Intelligence Agent
- 查询 Prop Firm 规则、折扣、流程
- 提供结构化规则结论
- 输出风险提示

### 模块 B：NotebookLM Compliance Skill（★ 最高优先）
- 整理规则文件，基于来源核验规则
- 对比官网规则、内部笔记、流程卡片
- 降低规则信息错误率
- 执行前信息准确性增强层

### 模块 C：Strategy Pack
- 策略结构化
- 识别 setup
- 输出成熟度判断

### 模块 D：Gatekeeper + Execution
- Gatekeeper：执行前合规/风控放行
- Execution：调用已跑通的自动下单链路
- 小账户环境试运行

---

## 主链流程

```
市场/平台规则
→ Prop Intelligence Agent（收集）
→ NotebookLM Compliance Skill（核验）
→ Strategy Pack（判断 setup）
→ Gatekeeper（放行/拦截）
→ Execution（自动下单）
→ 小账户真实执行
```

---

## 优先级排序

1. NotebookLM Compliance Skill（修复信息准确率）
2. Prop Intelligence Agent（原始信息输入）
3. Gatekeeper（放行/拦截层）
4. Execution（已跑通，立即正式使用）
5. Strategy Pack（盈利能力核心，但非最紧迫）

---

## 龙哥定位（一分为二）

### 龙哥-A：系统工程师
- 环境维护、硬件监控、SSH/API、日志

### 龙哥-B：总裁助理
- 规则查询、Checklist、账户汇总
- 规则依据通过 NotebookLM 增强后输出

---

## 基础设施执行层（独立）

- 温总：执行节点
- NinjaTrader：执行终端
- Replikanto：复制与账户映射
- Signal Server：指令入口

---

## v1 验收标准

> 系统能够基于可靠资料回答并核验 Prop Firm 规则，能判断 setup 成熟度，能给出放行/拦截结论，并在小账户环境下通过已跑通链路完成真实执行。

---

*独立方案，勿与方案二混合。待总裁核准后正式采用。*
