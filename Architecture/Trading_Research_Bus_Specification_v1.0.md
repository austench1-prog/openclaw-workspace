# Trading Research Bus Specification v1.0
# Date: 2026-06-22 | Status: DISCUSSION DRAFT (Dragon 起草, 待 Chairman 审)
# Scope: 定义 OC ↔ 小白研究中心 ↔ Hermes ↔ Chairman 之间的结构化文件交换格式。
#        架构 + 格式 ONLY。不实施。落地需 Chairman 批准。
# 关系：实现 Xiaobai_Research_Center_v1 §4-5 信息流 + OC_Hermes_Bus_Spec 的统一版。

---

## 0. 设计原则（不可违反）
1. **真相在文件，不在聊天。** 所有正式交换 = 结构化文件，不靠 Telegram/口头。
2. **一 ID 一文件，append 历史，绝不删单。** 改 status 或加 result，不覆盖原文。
3. **各守车道。** OC 写执行/任务，小白写研究输出，Hermes 写运营，Chairman 写批准。
4. **只读快照 + Chairman 唯一批准关口。** 小白只读 OC 快照、只写自己 Output；批准后才成正式 release。
5. **schema 锁死。** 坏单 = 异常处理，不静默吞。
6. **YAML 头 + Markdown 正文** 统一格式（机器读头、人读正文）。

---

## 1. 总线物理形态（🔒 Chairman 2026-06-22 锁定：一条总线）
**🔒 一条总线 `mats-bus`（Git 私有库），三方共挂。**（OC↔Hermes↔小白 统一,取代原计划的独立 oc-hermes-bus）
- 理由：避免两套目录/两套同步/双倍维护（违反"1分效率5分工作量就砍"）。
- 权限模型（GitHub 实现）：
  | 角色 | 读 | 写 |
  |---|---|---|
  | OC(小塔) | 全部 | `01_Research/Incoming`, `04_Execution`, `03_Approvals/Pending`(提请) |
  | 小白研究中心 | 全部(只读OC快照) | 仅 `01_Research/Output`, `01_Research/Failed`, `In_Progress` |
  | Hermes | 全部 | 仅 `02_Platform_Ops/*` |
  | Chairman | 全部 | `03_Approvals/{Approved,Rejected}` |
- (备选已否决：两条总线方案不采用。)

## 2. 目录结构
```
mats-bus/
├── 01_Research/        # OC↔小白 研究车道
│   ├── Incoming/       # OC 写入: 待小白处理的任务包
│   ├── In_Progress/    # 小白认领后移入
│   ├── Output/         # 小白写回: 研究报告 + 质量门报告
│   └── Failed/         # 小白无法完成: 异常单
├── 02_Platform_Ops/    # Hermes 车道
│   ├── Rule_Updates/   ├── Compliance_Review/   └── Alerts/
├── 03_Approvals/       # Chairman 车道
│   ├── Pending/        # OC/小白 提请批准
│   ├── Approved/       └── Rejected/
├── 04_Execution/       # OC 车道
│   ├── OC_Ready/       # 已批准、可供执行的正式 release
│   └── Historical/
└── 99_Schemas/         # 本规格 + JSON schema 校验文件
```

---

## 3. 四种核心格式（schema 锁定）

### 3.1 ① 研究任务包 (Research Request Packet)
- 位置：`01_Research/Incoming/<task_id>.md`
- 写入方：OC | 命名：`R-YYYY-NNN`
```yaml
---
type: research_request
task_id: R-2026-001
status: incoming            # incoming|in_progress|done|failed
requested_by: OC
created: 2026-06-22T21:40:00-07:00
strategy_version: DZ-NQ-v1.0
instrument: NQ               # NQ|MNQ|ES|GC...
timeframe: 5m
objective: "Review trade quality and identify process failures."
input_files:                # 相对总线路径,只读快照
  - 01_Research/Incoming/R-2026-001/trade_log.csv
  - 01_Research/Incoming/R-2026-001/strategy_parameters.yaml
  - 01_Research/Incoming/R-2026-001/execution_notes.md
required_outputs:           # 必须产出哪些(对应§3.2/3.3)
  - quality_gate_report
  - process_failures
  - parameter_risks
  - recommended_next_test
skills_requested:           # 指定用哪些 trading-skill(白名单内)
  - edge-strategy-reviewer
  - signal-postmortem
  - data-quality-checker
priority: normal            # low|normal|high
---
## 正文(人类可读)
研究目标、背景、特殊约束、Chairman 想知道的问题。
```

### 3.2 ② 研究报告 (Research Report)
- 位置：`01_Research/Output/<task_id>_research.md`
- 写入方：小白
```yaml
---
type: research_report
task_id: R-2026-001
status: done
produced_by: Xiaobai(ClaudeCode+skills)
engine: claude-code
skills_used: [edge-strategy-reviewer, signal-postmortem]
created: 2026-06-22T22:10:00-07:00
links_to_request: 01_Research/Incoming/R-2026-001.md
confidence: medium          # low|medium|high
beta_skills_flagged: [signal-postmortem]   # 标注 beta 能力
---
## 发现 Findings
## 过程失败 Process Failures
## 参数风险 Parameter Risks
## 建议的下一步测试 Recommended Next Test
## 证据/数据来源(可追溯)
```

### 3.3 ③ 质量门报告 (Quality Gate Report)
- 位置：`01_Research/Output/<task_id>_qgate.md`
- 写入方：小白 | 核心 = PASS/REVISE/REJECT 判决
```yaml
---
type: quality_gate_report
task_id: R-2026-001
status: done
produced_by: Xiaobai(ClaudeCode+skills)
skill: edge-strategy-reviewer
created: 2026-06-22T22:12:00-07:00
verdict: REVISE             # PASS|REVISE|REJECT
gates:                      # 8项质量门逐项打分
  edge_credibility: pass
  overfit_risk: revise
  sample_size: fail
  regime_dependence: pass
  exit_calibration: pass
  risk_concentration: pass
  execution_realism: revise
  failure_quality: pass
blocking_issues: [sample_size_insufficient]
---
## 逐项判决理由
## 阻断项(必须解决才能 PASS)
## 修订建议
```

### 3.4 ④ 批准文件 (Approval Manifest)
- 位置：`03_Approvals/Approved/<task_id>_approval.md`（或 Rejected/）
- 写入方：**仅 Chairman**
```yaml
---
type: approval_manifest
task_id: R-2026-001
decision: approved          # approved|rejected|hold
decided_by: Chairman
decided: 2026-06-22T23:00:00-07:00
based_on:                   # 依据哪些报告
  - 01_Research/Output/R-2026-001_qgate.md
  - 01_Research/Output/R-2026-001_research.md
release_to_OC: true         # 批准后是否生成 OC 可用 release
release_id: DZ-NQ-v1.1      # 若 release,新正式版本号
scope: "仅采用参数风险结论,不采用新加仓逻辑"   # Chairman 限定采用范围
---
## Chairman 批注
## 采用/不采用 明细
```

---

## 4. 生命周期(一个 task 的完整流转)
```
OC 写 ① → Incoming/
  ↓ 小白认领,移 In_Progress/
小白跑 Claude Code+skills
  ↓ 成功 → 写 ②③ 到 Output/, status=done
  ↓ 失败 → 写异常到 Failed/, status=failed
OC/小白 把 ②③ 提请 → 03_Approvals/Pending/
  ↓ Chairman 审
Chairman 写 ④ → Approved/ 或 Rejected/
  ↓ 若 approved + release_to_OC
OC 生成正式 release → 04_Execution/OC_Ready/<release_id>
```
- **绝不删单**：每一步改 status 或加新文件,原文 append 历史。
- **坏单**：schema 校验失败 → 移 Failed/,记异常,不静默。

## 5. SSH 触发(阶段2,先不做)
- 阶段1：纯文件,小白人工/定时查 Incoming。
- 阶段2：OC 只发 `run_research_task R-2026-001`;小白固定 runner 读 task→读资料→调 Claude Code+指定 skills→写回 Output。SSH = 启动按钮,非自由命令通道。

## 6. 待 Chairman 拍的决策(汇总)
1. ✅ **总线一条 `mats-bus`(Chairman 2026-06-22 锁定)。**
2. ⏳ 本 4 格式 schema 是否认可?(可加字段/改字段)
3. ⏳ 8项质量门名称是否照搬 edge-strategy-reviewer,还是定制我们自己的?
4. ⏳ release 版本号规则(DZ-NQ-v1.1 这种)是否可用?

---

## 7. 下一步(本 Spec 审过之后)
- a. Chairman 审 + 拍 §6 四问。
- b. 建 `mats-bus` Git 私有库 + 目录骨架 + 权限。
- c. 小白装 Claude Code + 筛选首轮 skills(§Xiaobai_Research_Center_v1 §7)。
- d. 跑通第一个 task R-2026-001(用今天的折扣区4模板 + 一笔模拟交易记录做试)。

---
*DRAFT v1.0 | 2026-06-22 | Dragon 起草 | 不实施待 Chairman 审*
