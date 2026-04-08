# 龙哥执行 To Do List (v1)
# Source: 外部参考（吉米/Gemini 整理）+ 总裁确认方向
# Version: 1.0 | Date: 2026-04-07
# 状态：草稿，待龙哥 Review 后与总裁讨论定稿

---

## 总目标

把当前已经跑通的自动下单链路，升级成一个可控、可校验、可小账户实战运行的 v1 多 Agent 交易系统。

核心主线：
```
规则信息进来 → NotebookLM 核验 → Gatekeeper 放行/拦截 → Execution 小账户执行 → 结果回写
```

---

## Phase 0：锁定边界

- [ ] 确认 v1 只做 4 个模块（Prop Agent / NotebookLM / Strategy Pack / Gatekeeper+Execution）
- [ ] 明确当前不做：SPX 0DTE / 多策略并行 / 完全无人值守 / CEO/LLC / 完整绩效系统
- [ ] 输出：`MATS_v1_scope_lock.md`（v1做什么 / 不做什么 / 唯一主链）

---

## Phase 1：建立黄金合规资料包

- [ ] A. 整理官方规则资料（官网/FAQ/payout规则/折扣页）
- [ ] B. 整理合同与截图资料（原始合同/dashboard截图/关键条款）
- [ ] C. 整理内部资料（规则笔记/流程卡片/checklist草稿）
- [ ] D. 确定 v1 平台名单（不全市场铺开）
- [ ] 统一命名格式：`平台_资料类型_日期_版本`
- [ ] 去重/删旧/标记过期/冲突资料放"待核验"区
- [ ] 输出：`compliance_source_inventory_v1.md`

---

## Phase 2：NotebookLM 接入合规 Skill

- [ ] 建立 v1 专用 notebook（不混杂项）
- [ ] 导入黄金合规资料包
- [ ] 设计 3 类固定提问模板（规则查询/差异核验/执行前核验）
- [ ] 做首轮准确率测试（10个已知答案问题）
- [ ] 输出：`notebooklm_compliance_test_v1.md`

---

## Phase 3：合规输出结构化

- [ ] 定义合规输出固定字段（平台/结论/风险等级/是否可执行/来源依据）
- [ ] 定义 4 个结论枚举：ALLOW / BLOCK / REVIEW / REDUCE_SIZE
- [ ] 每个平台出一张 v1 规则核验卡片

---

## Phase 4：建立 Gatekeeper

- [ ] 定义输入（交易建议/规则核验/账户状态/风险状态）
- [ ] 定义输出（放行/拦截/降仓/人工确认）
- [ ] 建最小放行规则（5条核心逻辑）
- [ ] 做纸面测试（10个模拟场景）

---

## Phase 5：Execution 挂入主链

- [ ] 复核当前执行链路状态（Signal Server/NinjaTrader/Replikanto/账户映射）
- [ ] 定义放行后触发规则（只有 ALLOW 或条件性 REDUCE_SIZE 才接单）
- [ ] 小账户试运行（最小账户/最小风险/全程人工盯盘）
- [ ] 建执行日志：`execution_run_log_v1.md`

---

## Phase 6：Strategy Pack（只做一个策略）

- [ ] 总裁选定一个最成熟策略
- [ ] 整理成 checklist（前置/成熟/失效/禁做/风险限制）
- [ ] 固定输出格式（setup状态/已满足/未满足/是否接近执行）
- [ ] 与 Gatekeeper 对接

---

## Phase 7：龙哥双角色落地

- [ ] 龙哥-A 职责清单固定：`ops_watchlist_v1.md`
- [ ] 龙哥-B 职责收缩：规则查询组织/checklist整理/日报/信息传递
- [ ] 确认：龙哥不再裸做规则裁定，合规判断走 NotebookLM

---

## Phase 2.5：独立验证机制（待完善）

- [ ] 当前问题：10题准确率测试属于"自己出题考自己"，无法发现 source 本身的错误
- [ ] 待办：找到独立第三方验证方法（例：从官方 FAQ 直接抽题，独立对比答案）
- [ ] 完成后：替换掉当前的自测 SOP

*保留当前 SOP，完善后再替换*

---

## Phase 8：v1 验收

- [ ] 逐条检查 8.1 验收问题
- [ ] 输出：`MATS_v1_acceptance_report.md`

---

## 执行顺序（最简版）

```
1. 锁定 v1 范围
2. 整理黄金合规资料包
3. 搭好 NotebookLM 合规 skill
4. 做首轮准确率测试
5. 把合规结果结构化
6. 建 Gatekeeper
7. 把已跑通的 Execution 挂到 Gatekeeper 后面
8. 小账户实盘试运行
9. 再接入一个策略的 Strategy Pack
10. 做 v1 验收
```

---

## 最后一句任务定义

> 先不要扩系统，先把"规则准确、风控可拦、执行可跑、小账户可测"这条主链做实。

---

*草稿 | 待龙哥 Review + 总裁讨论定稿 | 2026-04-07*
