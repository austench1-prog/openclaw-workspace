# MATS Integration & File Ownership Plan v0.1
# Date: 2026-06-23 | Status: DISCUSSION DRAFT (President 主笔, Dragon 记录)
# Scope: 集成 + 文件归属规划 ONLY。无文件/权限/自动化/生产流程改动，待 President 批准矩阵后才动。

---

## 1. 现状
Hermes 安装完成、是活的第二个 Agent：
- Hermes Agent active / gateway running / 主模型 gpt-5.5 / 独立 .hermes / Migration_Pack 完成。
- **下一阶段 = 不是安装，是集成进 MATS 运行结构。**

## 2. 目标结构（四角色职责）
| 角色 | 主责 |
|---|---|
| President(总裁) | 最终决策、批准、优先级、release 授权 |
| OC | 执行控制、正式策略版本、活跃参数、交易工作流 |
| Hermes | 平台运营、Prop Firm 规则、网站信息、行政、提醒、合规更新 |
| 小白 | 策略研究、交易复盘、质量分析、Claude Code/trading-skills 工作流 |
> OC 保留执行 + 正式策略归属；Hermes 接平台/运营；小白做研究/复盘；President 唯一批准权。

## 3. 第一优先：OC 文件归属 & 迁移矩阵 v0.1（关键第一步）
移动任何文件前，先给 OC 当前所有文件建完整归属图。
**目的不是立即物理迁移，是先判定：**
1. 哪些文件仍由 OC 正式拥有；
2. 哪些转 Hermes 拥有；
3. 哪些小白可作只读研究快照接收；
4. 哪些属于共享 MATS Bus；
5. 哪些是重复/过时/该归档。

**每个文件/文件夹字段：**
| 字段 | 含义 |
|---|---|
| Current Location | 当前 OC 路径 |
| File/Folder Name | 精确名 |
| Functional Category | Execution/Strategy/Platform/Ops/Research/Governance/Archive |
| Official Owner | OC/Hermes/小白/President |
| Read Access | 哪些节点可读 |
| Write Access | 哪个节点可改 |
| Target Location | OC/MATS Bus/小白研究区/Archive |
| Migration Action | Keep/Copy/Move/Archive/Review |
| Current Status | 已迁/待定/需决策 |
| Notes | 依赖、风险、版本顾虑 |
> 铁律：每个正式文件必须有**唯一 official owner**，即使多节点可读。

## 4. 文件归属方向
**留 OC：** 正式策略版本 / 活跃参数 / 执行逻辑 / Gatekeeper+Route Mapper / NinjaTrader+DragonFileSignal 执行文件 / 当前生产文档 / 已批准 release。
**转 Hermes：** Prop Firm 规则册 / 平台FAQ公告 / payout政策 / 折扣促销跟踪 / 账户行政 / 续费到期提醒 / 平台运营笔记 / 合规知识库。
**小白只读接收：** 策略快照 / 交易日志 / 参数快照 / 执行笔记 / 截图 / 研究请求。（小白产报告，不直接改 OC 正式策略文件。）
**President/治理区：** 批准manifest / 最终release / 角色定义 / Bus schema / 架构决策 / 归档版本政策。

## 5. 一条总线，两条逻辑车道（v0.1 提议的新结构）
```
MATS_Bus/
├── 00_Governance/
├── 01_Research_Lane/
├── 02_Ops_Compliance_Lane/
├── 03_Execution_Packets/
└── 99_Archive/
```
- **Research Lane:** OC→研究任务包→小白→研究审查报告→President审→批准manifest→OC。
- **Ops/Compliance Lane:** Hermes→平台更新/Ops包→President审→批准更新→OC收相关批准摘要。

## 6. Hermes 集成 Phase 1（矩阵审过后按序）
1. 用受限权限连 MATS Bus；
2. 读 governance schema + 已批准材料；
3. 只写 02_Ops_Compliance_Lane；
4. 跑一个测试读写包；
5. 启动一个 F1 公开页监控试点(首站 TradeDay)；
6. 发标准平台更新包给 President；
7. 测试已批准的 Hermes→OC 信息链。
> Hermes 不直接改研究输出、执行包、正式策略文件。

## 7. 小白研究中心
Jimmy/Gemini CLI + Claude Code + trading-skills 共存。小白 = 研究工作站，非审批、非执行节点。

## 8. 立即下一步
1. 建 OC 文件归属 & 迁移矩阵 v0.1；
2. 与 President 审矩阵；
3. 标出之前部分迁移中已迁的文件；
4. 物理移动前先逻辑分离；
5. Hermes 拥有的材料分批受控移动；
6. 给小白建 OC 策略只读快照；
7. 矩阵确认后才把 Hermes 连上 MATS Bus。
> 矩阵批准前，不改任何文件/权限/自动化/生产流程。

---
*DISCUSSION DRAFT v0.1 | 2026-06-23 | President 主笔, Dragon 记录 | 待批准*
