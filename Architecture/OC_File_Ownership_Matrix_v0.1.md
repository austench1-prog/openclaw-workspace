# OC File Ownership & Migration Matrix v0.1 (DRAFT)
# Date: 2026-06-23 | Status: DRAFT for President review. 无物理迁移,仅归属判定。
# 粒度 = 目录/类别(不逐个366文件)。铁律:每个正式文件唯一 official owner。
# Owner缩写: OC / HE(Hermes) / XB(小白) / PR(President/治理)

---

## A. 顶层治理/身份文件（系统宪法）
| 项 | 类别 | Owner | 读 | 写 | 目标 | 动作 |
|---|---|---|---|---|---|---|
| MEMORY.md / SOUL.md / AGENTS.md / IDENTITY.md / USER.md / TOOLS.md | Governance | **OC** | OC | OC | 留OC | Keep |
| Dragon_*_SOP / Dragon_Team_Protocol / Dragon_ToDo | Governance | **OC** | all | OC | 留OC | Keep |
| HEARTBEAT.md / Quick_Reference / 01_Decision_Log / Master_Task_List | Governance | **OC** | OC | OC | 留OC | Keep |
| 角色定义/Bus schema/架构决策(Architecture/*) | Governance | **PR治理** | all | OC(代笔) | 留OC + 镜像Bus 00 | Keep+Copy |

## B. 执行/策略（OC 核心,留 OC）
| 项 | 类别 | Owner | 写 | 动作 |
|---|---|---|---|---|
| Multi_Agent_Trading_System_v3.0* / MATS_v1_scope_lock | Strategy | **OC** | OC | Keep |
| Trading/discount-zone-* (spec/math/notes/ATR) | Strategy | **OC** | OC | Keep |
| Trading/ATM_project/ + atm_templates_FINAL_4/ | Execution | **OC** | OC | Keep |
| Agent_Prompts/ (gatekeeper/pre_trade/ninja_startup/compliance_framework...) | Execution | **OC** | OC | Keep |
| Trading/execution_run_log / Account_Registry / Eval_Account_Risk_Form | Execution | **OC** | OC | Keep |
| Trading/Risk_Control / Trading_Strategy / System_Landing / Market_Rehearsal | Strategy | **OC** | OC | Keep |
| Trading/Chairman_Trading_Profile / ICT_Glossary / AP_Background | Strategy | **OC** | OC | Keep |
| scripts/ (git_sync/backup等运维脚本) | Execution | **OC** | OC | Keep |

## C. 平台/合规/行政（→ 转 Hermes 拥有）
| 项 | 类别 | 当前Owner | 目标Owner | 目标位置 | 动作 |
|---|---|---|---|---|---|
| NotebookLM_Sources/ (Apex/MFF规则raw + Compliance Pack) | Platform/Compliance | OC | **HE** | Bus 02_Platform_Ops | **Review→Move** |
| Agent_Prompts/prop_firm_* (rules/compliance/purchase SOP) | Platform | OC | **HE** | Bus 02 | Review→Move |
| Agent_Prompts/tradovate_daily_risk_sop / trading_schedule_sop | Ops | OC | **HE** | Bus 02 | Review→Move |
| Business_OS/06_Compliance_Calendar / 05_Banking / 07_Finance_Admin | Ops/Admin | OC | **HE** | Bus 02 | Review→Move |
| Business_OS/01_APM_LLC / 02_Meritpoint / 09_Decisions | Admin | OC | **HE** | Bus 02 | Review→Move |
| 账户续费/到期提醒 (cron + 相关记录) | Ops | OC | **HE** | Bus 02 | Review |
| 待处理/PropFirm_LLC_Migration / Hedging_Rules_Research | Platform | OC | **HE** | Bus 02 | Review |

## D. 研究输入（→ 小白只读快照）
| 项 | 类别 | Owner | 小白接收方式 | 动作 |
|---|---|---|---|---|
| Trading/discount-zone-* 策略快照 | Research input | OC(正本) | 只读快照→Bus 01 | Copy(snapshot) |
| Trading/execution_run_log 交易日志 | Research input | OC(正本) | 只读快照→Bus 01 | Copy |
| atm_templates_FINAL_4 参数快照 | Research input | OC(正本) | 只读快照→Bus 01 | Copy |
| Trading/material_specimens / strategy_charts / rehearsal_charts 截图 | Research input | OC(正本,底片) | 只读快照→Bus 01 | Copy(不动底片) |
| 研究请求(R-xxx任务包) | Research | OC | 写入Bus 01_Incoming | Keep |

## E. 归档/底片（不动,Archive）
| 项 | 类别 | Owner | 动作 |
|---|---|---|---|
| Trading/austin_raw_archive/ (素材底片,铁律绝不改) | Archive | OC | **Keep冻结** |
| Trading/atm_templates_backup / stopstrategy_backup | Archive | OC | Keep |
| 历史版本 (Master_Tree_v1/v2, MATS v1.0/v2.0, Notes_2026xx) | Archive | OC | Keep/Archive |
| memory/2026-04~05 历史日记 | Archive | OC | Keep |
| 待处理/ (大部分=旧草稿) | Review | OC | **Review**逐个定 |

## F. 重复/过时（待清理判定）
| 项 | 疑似 | 动作 |
|---|---|---|
| Multi_Agent_* 多版本(v1/v2/v3/Draft/Print) | 旧版重复 | Review→Archive旧版 |
| Dual_System_Blueprint v1/v2 | 旧版 | Review |
| OC_Hermes_Architecture_Diagram.html(旧) vs MATS_Architecture_v2(新) | 旧图已被取代 | Archive旧图 |
| 我的文件/ (给总裁回看的copy) | 派生副本 | Keep(便利) |

---

## 关键判断 / 待 President 拍
1. **C类(平台/合规/行政)转 Hermes** —— 方向对吗？尤其 NotebookLM 合规库:v0.1说"沿用不新建,Hermes只产差异" → 那合规库**正本留OC还是转HE**? (建议:正本留OC当Layer A,HE只读+产差异摘要)
2. **D类小白快照** —— Copy快照到Bus,正本永远留OC,对吗?
3. **底片austin_raw_archive** —— 冻结不动(铁律),确认。
4. **F类重复/旧版** —— 要不要这轮一起清,还是单独一轮?
5. 物理迁移**先不做**,本矩阵只定归属;确认后再分批受控移动(v0.1 §8)。

---
*Matrix v0.1 DRAFT | 2026-06-23 | Dragon 扫描生成 | 待 President 审*
