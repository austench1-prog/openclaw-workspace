# OC File Ownership & Migration Matrix v1.0 (EXECUTION)
# Date: 2026-06-23 | Status: President 5项决定已锁。分层盘点法。
# 方法：目录/功能单元级 Ownership Matrix + 文件级 Exception Register。
# Owner: OC / HE(Hermes) / XB(小白) / PR(治理). 铁律:每正式文件唯一 owner。

## President 锁定的5项（v1.0 基准）
1. 合规库(NotebookLM)正本 **留OC**(当Layer A); HE 只读+产差异摘要。
2. D类小白快照 = Copy到Bus、**正本永留OC**。
3. 底片 **冻结**(austin_raw_archive 绝不改)。
4. F类重复/旧版 **这轮一起清**。
5. 物理迁移先不做; 本矩阵只定归属; 确认后分批受控移。

---

## PART 1 — 目录/功能单元级 Ownership Matrix（主表）

| 目录/单元 | 功能类别 | Owner | 读 | 写 | 目标位置 | 动作 | 混合? |
|---|---|---|---|---|---|---|---|
| MEMORY/SOUL/AGENTS/IDENTITY/USER/TOOLS/HEARTBEAT | Governance | OC | OC | OC | OC | Keep | 否 |
| Dragon_*_SOP / Team_Protocol / ToDo / Decision_Log / Master_Task_List / Quick_Reference | Governance | OC | all | OC | OC | Keep | 否 |
| Architecture/ | Governance | PR | all | OC代笔 | OC+镜像Bus 00 | Keep | 否 |
| Multi_Agent_Trading_System_v3.0(现行) / MATS_v1_scope_lock | Strategy | OC | all | OC | OC | Keep | 否 |
| Trading/ (执行+策略主体,见例外) | Execution/Strategy | OC | OC | OC | OC | Keep | **是→见Reg** |
| Agent_Prompts/ (混合) | mixed | — | — | — | — | **拆**→见Reg | **是** |
| scripts/ (运维脚本) | Execution | OC | OC | OC | OC | Keep | 否 |
| NotebookLM_Sources/ | Platform/Compliance | **OC正本** | all | OC | OC(Layer A) | Keep; HE只读产差异 | 否 |
| Business_OS/01_APM,02_Meritpoint,05_Banking,06_Compliance_Cal,07_Finance,09_Decisions | Ops/Admin | **HE** | OC+HE | HE | Bus 02_Platform_Ops | Review→Move | 否 |
| Business_OS/00_Dashboard,03_Shared,04_SOPs,08_Templates | Ops | HE | OC+HE | HE | Bus 02 | Review | 否 |
| 待处理/ (混合旧草稿) | mixed | — | — | — | — | **拆**→见Reg | **是** |
| 我的文件/ (给总裁回看copy) | Convenience | OC | OC | OC | OC | Keep | 否 |
| memory/ (日记+active_work) | Governance | OC | OC | OC | OC | Keep | 否 |
| Persona/ Security/ SOPs/ assets/ | Governance/Ops | OC | OC | OC | OC | Keep | 否 |

---

## PART 2 — 文件级 Exception Register（例外登记册）
> 只登记"不随目录默认归属、需单独拎出"的文件。

### EX-A: Agent_Prompts/ 拆分（混合目录）
| 文件 | 归 | 理由 |
|---|---|---|
| gatekeeper_v1 / gatekeeper_paper_test / pre_trade_check / daily_checklist / ninja_startup_sop / compliance_framework / indicator_dev_workflow / ops_watchlist / setup_alert_agent_skeleton | **OC** | 执行/闸门/工作流 = OC核心 |
| prop_firm_rules_agent v1/v2/v2_1 / prop_firm_compliance_protocol v2/v2_1 / prop_firm_purchase_sop / tradovate_daily_risk_sop / trading_schedule_sop | **HE** | Prop Firm规则/平台/行政 = Hermes主业 |

### EX-B: 待处理/ 拆分（混合旧草稿）
| 文件 | 归/动作 |
|---|---|
| PropFirm_LLC_Migration_Plan / Hedging_Rules_Research / 账户风控表单体系(+原版) | **HE**(平台/合规) Review→Move |
| Agent_Plan_A/B/Comparison/Deep_Analysis | **F类旧版**→Archive(已被v3.0取代) |
| System_Trading_Protocol_v1.2_pending / NotebookLM=Proposal Skill / README | OC Review |

### EX-C: Trading/ 内部例外
| 项 | 归/动作 |
|---|---|
| Trading/austin_raw_archive/ | **冻结**(底片,铁律绝不改) |
| Trading/atm_templates_backup / stopstrategy_backup | Archive(Keep) |
| Trading/Account_Registry / Eval_Account_Risk_Form / Open_Issues | **HE候选**(账户行政) — 但与执行强相关,**留OC+HE只读** |
| Trading/material_specimens,strategy_charts,rehearsal_charts | OC正本; XB只读快照(Copy) |
| Trading/discount-zone-* / ATM_project / atm_templates_FINAL_4 / execution_run_log | OC; XB只读快照(Copy) |
| Trading/Dragon_Team_System_Gemini / Dual_Engine_Gemini | F类(Jimmy旧产物)→Review/Archive |

---

## PART 3 — F类 这轮一起清（President决定4，本轮执行）
> 旧版/重复 → 移到 99_Archive 或 workspace 内 _archive/。**移动=可恢复,不删除。**
| 文件 | 处理 |
|---|---|
| Multi_Agent_Trading_System v1.0/v2.0/v3.0_Draft/v3.0_Print | Archive(保留v3.0现行) |
| Multi_Agent_Reference_Notes v1.0/v2.0 / Multi_Agent_Structure v1.0/v2.0 | Archive |
| Master_Tree v1.0/v2.0 / Dual_System_Blueprint v1.0/v2.0 | Archive |
| Notes_20260406(+Arch+Role) / Progress_Report_Apr | Archive |
| Architecture/OC_Hermes_Architecture_Diagram.html(旧图,已被MATS_v2取代) | Archive |
| 待处理/Agent_Plan_* | Archive |

---

## 两车道映射（用现有 mats-bus,不改 — President决定)
- 01_Research = Research Lane (OC→XB→PR→OC)
- 02_Platform_Ops = Ops/Compliance Lane (HE→PR→OC)
- 03_Approvals = Governance批准
- 04_Execution = Execution Packets
- 99_Schemas = Governance schema

---
## 下一步(矩阵确认后)
1. 执行 PART 3 F类归档(本轮,可自动)。
2. 给XB建OC策略只读快照(Copy到Bus 01)。
3. C类/HE文件分批受控移(物理迁移,待批)。
4. Hermes受控接mats-bus(只读+写02车道)。

*Matrix v1.0 | 2026-06-23 | Dragon | President 5决定已锁*
