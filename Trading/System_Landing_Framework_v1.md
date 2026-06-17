# System Landing Framework (系统落地评估框架) v1

**Owner:** Chairman (Austin) | **Maintained by:** Dragon
**Created:** 2026-06-16
**Status:** META-RULE — apply to EVERY real piece of work from now on.

---

## ⚠️ PURPOSE

This is a **meta-rule (元规则)**, one level above "which step to do next."

Core principle (Chairman, 2026-06-16):
> 每一个实际遇到的工作,都当成一次**系统落地的测试和评估机会**。
> "落地" ≠ 有想法、有策略,而是 **系统到底能不能真正跑起来**。

Do NOT treat real problems (account rules, risk settings, trade logging, platform auto-flatten, account status updates, contacts, execution flow) as scattered one-off issues. **Run every one of them through this framework.**

---

## ⚡ EFFICIENCY RULE — WHITELIST FIRST (白名单优先,不要什么都走流程)

**Chairman 2026-06-16 纠正（最高优先的方法论）:**
> 明明谁都知道是高频的事,不用讨论。**先列一个正面白名单** — 哪些事已明确是高频,直接按高频处理、直接开做。只有**真正不确定 / 从未遇过**的,才走下面的评估流程。

```
一件工作来了
   │
   ├─ 已明确是高频? ──→ 上白名单,直接按高频处理,开做 (不走流程)
   │
   └─ 真不确定 / 没遇过? ─→ 走下面的 STEP 0 + 四个评估问题
```

效率原则:不是每件事都要先跑程序。先看一眼 → 能上白名单的直接上 → 剩下的才讨论。

---

## STEP 0 — Classify first (最先判断 — 仅用于白名单之外的不确定项)

For each **uncertain** piece of work, ask one question:

> **偶然事件,还是未来高频工作?**

- **偶然 (one-off):** today 解决即可,以后很少再遇到 → solve as a one-time problem.
- **高频 (recurring):** 未来会不断重复 → 目标**不是**"Chairman 临时处理",而是:
  ```
  系统化 → 平台化 → 流程化 → 最终无需 Chairman 介入,系统/平台自动完成
  ```
  ↑ **这是最高目标 (highest goal).**

---

## THE FOUR EVALUATION QUESTIONS (对每件高频工作都问)

1. **我们现在已经有什么?** (What do we already have?)
2. **完成这件事还缺什么?** (What is still missing?)
3. **缺的东西是哪一类?** Classify the gap:
   - 📄 资料不完整 (data incomplete)
   - 📏 规则没搞清楚 (rules unclear)
   - ⚙️ 平台还没设置好 (platform not configured)
   - 🙋 需要 Chairman 继续参与,把关键流程固定下来 (needs Chairman to fix the flow)
4. **现有系统有没有可能达到最终自动化执行的目标?** (Can the current system reach full automation?)

---

## THE CAR ASSEMBLY METAPHOR (造车比喻)

落地 = 这台车能不能真正开起来。确认:

| 问题 | 含义 |
|---|---|
| 有没有图纸? | Do we have the blueprint / design? |
| 有没有零件? | Do we have the parts? |
| 零件齐不齐? | Are the parts complete? |
| 组装顺序清不清楚? | Is the assembly order clear? |
| **最后这台车能不能真正开起来?** | **Does it actually run?** |

---

## WORKING DISCIPLINE (节奏)

- **讨论一步,确认一步,再落实一步。** (Discuss one step, confirm one step, then land one step.)
- 不慌张,跟 Chairman 的思路一步一步来。
- 只有把事情一件一件固定下来,系统才不是停留在概念上,而是能真正进入日常使用。

---

## 平台架构理解：CQG vs Tradovate（2026-06-16 锁定）

**CQG = 底层基础设施（地基）。Tradovate = 上层前端/经纪平台（盖在地基上的房子）。**

- CQG 是行业底层供应商：聚合 85+ 数据源、连 45+ 交易所网关（DMA）、提供市场数据 + 订单路由 + 风控管理 API。
- Tradovate 是面向交易者的前端经纪平台，行情数据和交易所连接底层跑在 CQG 上。
- 很多 Prop Firm 直接用 CQG 作为交易平台前端（如 TPT = CQG；MFF = Tradovate）。

**对系统的三点实际意义：**
1. **数据同源 → 策略统一：** Tradovate 和 CQG 看到的行情/价格本质同一套（CQG 数据源）。不管账户用哪个前端，行情判断一致，策略层不受影响。
2. **风控入口分平台 → 设置要分开做：** 「每日盈利/亏损自动平仓」在各自前端设置。MFF→Tradovate Risk Settings；TPT→CQG（或 TPT 后台调 CQG）。界面/入口不同。
3. **强制平仓原理统一：** 「上游控制端碰到点位强制平仓」对两者都适用（底层都是 CQG 订单管理）。只是设置的「门」不同。

---

## EVALUATION LOG (套用记录)

> 每次用本框架评估一件真实工作,在此登记一行,链接到产出文件。

| 日期 | 工作 | 高频? | 缺口类型 | 产出/状态 |
|---|---|---|---|---|
| 2026-06-16 | 账户规则掌握 + 登记表 | 是(白名单) | 📄资料+📌规则(P1) | 进行中 |

---

## 🟢 WHITELIST (已明确高频 = 系统必须能做,直接按高频处理)

定版 2026-06-16:

1. **每日盈利目标的平台自动平仓** — 用 Tradovate「每日最高盈利」风控功能,交易中自动平仓,符合考核账户每日盈利目标。
2. **每个账户的规则,系统必须完整掌握** — 账户规则是系统的根本,必须全部搞清并固定。
3. **交易记录归档** — 每笔/每日进 `execution_run_log`。
4. **账户状态登记与更新** — 账户号/平台/规则/状态集中一张表,持续更新。
5. **行情预演 + 复盘归档** — 进 `Market_Rehearsal_Log`。

## 🔧 OPEN PROBLEMS (不是要不要做,而是怎么打通 / 内容待查)

- **P1: MFF 新账户规则的具体内容** — "2 today min" 含义? 有没有 50% 限制? (服务于白名单 #2)
- **P2: 连温总/小白自动读账户数据** — 技术连通方式未定,待试(停在探 5000 端口)。(服务于白名单 #4 的自动化)

---

*Created: 2026-06-16 | Dragon | This file governs how we land everything else.*
