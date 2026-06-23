# 小白 研究中心 部署架构 v1（Xiaobai Research & Quality-Gate Center）
# Date: 2026-06-22 | Status: DISCUSSION DRAFT (President 主笔方案, Dragon 记录)
# Scope: 架构 + 分工设计 ONLY。先研究，不实施。任何落地需 President 批准。
# 关系：本文 = OC_Hermes_System_Judgment_v1 的延伸（新增"小白研究中心"第4功能角色）。

---

## 0. 一句话结论（总裁 锁定）
小白装 **Claude Code**（不装第二个 OpenClaw），原生运行 **claude-trading-skills**。
Jimmy(Gemini CLI) 保留共存。OC 不跑此 skill，通过 Git/Obsidian 文件总线提交研究任务。

---

## 1. 四个功能角色（不是"五方"）
1. **President** = 大脑 / 唯一批准关口
2. **OC（小塔）** = 手 + 闸门（正式策略素材、执行参数、执行控制、当前有效版本、已批准内容接收）
3. **Hermes（第二台Mac mini）** = 后台 + 外勤（平台/规则/网站/行政/运营信息、合规库采集）
4. **小白研究中心（MacBook Air）** = 策略研究 + 复盘 + 质量门中心

小白内部 = **两个工具引擎共存**（不是组织节点）：
- Jimmy / Gemini CLI
- Claude Code + claude-trading-skills

---

## 2. 小白 host 方案（最优）
```
小白 MacBook Air (austinchien, 192.168.0.164)
├── Gemini CLI / Jimmy           (保留)
├── Claude Code                  (新装 = trading-skills 原生host)
│   └── claude-trading-skills
├── Trading_Research_Lab/
│   ├── CLAUDE.md
│   ├── .claude/skills/
│   ├── Research_Bus/
│   └── Output/
└── Obsidian Vault / Git File Bus
```
- **Claude Code = skill 原生 host**（项目级 CLAUDE.md + .claude/skills/，按需加载不塞满上下文）。
- ❌ 不要 "小白→装OpenClaw→再包Claude Code→再跑skill"：多一层编排、多维护点、不增研究能力。
- 关系：Claude Code = 研究工具运行主机；claude-trading-skills = 能力包；OpenClaw = OC执行控制系统(不承载此skill)。

## 3. 工具引擎分工（小白内部）
| 工具 | 职责 |
|---|---|
| Jimmy / Gemini CLI | 大范围资料阅读、长文档整理、通用研究、辅助开发、跨文档梳理 |
| Claude Code + trading-skills | 交易复盘、策略质量门、研究流程、回测审查、结构化研究报告 |
| President | 决定最终采用哪个结论 |
- 共存不替换。Claude skills 先只由 Claude Code 跑；未来验证可移植再单独适配 Gemini。

## 4. 信息流（Git文件总线为真相源，Obsidian为人类界面）
```
OC → Research Request Packet → Git/Obsidian Research Bus
→ 小白上的 Claude Code → 研究与质量审查报告(含 PASS/REVISE/REJECT 建议,非判决)
→ President Review → Approval Manifest → OC 接收已批准版本

Hermes → 平台/规则/账户更新 → Ops & Compliance Lane → President + OC 读摘要
```
**铁律：**
- OC 输出正式素材的**只读快照**。
- 小白**只读快照，不直接改正式策略库**；输出研究结论**不直接覆盖OC正式版本**。
- **President 批准后**才产生可供 OC 使用的正式 release。
- Hermes 只提交平台/规则/行政/网站信息+提醒，**不直接改策略结论**。

## 5. 文件总线结构（🔒 一条总线 mats-bus，三方共挂，总裁 2026-06-22 锁定）
```
mats-bus/   (= 唯一总线, OC↔Hermes↔小白 共用, 取代原 oc-hermes-bus)
├── 01_Research/ {Incoming, In_Progress, Output, Failed}
├── 02_Platform_Ops/ {Rule_Updates, Compliance_Review, Alerts}
├── 03_Approvals/ {Pending, Approved, Rejected}
├── 04_Execution/ {OC_Ready, Historical}
└── 99_Schemas/
```
研究任务 = 结构化文件(非聊天文字)：
```yaml
task_id: R-2026-001
status: incoming
requested_by: OC
strategy_version: DZ-NQ-v3.0
instrument: NQ
timeframe: 5m
objective: Review trade quality and identify process failures.
input_files: [trade_log.csv, strategy_parameters.yaml, execution_notes.md]
required_outputs: [review_recommendation, process_failures, parameter_risks, recommended_next_test]
```

## 6. SSH 用法（两阶段）
- **阶段1（先）：** OC 只写任务文件；小白人工/定时检查 Incoming/，确认 Claude Code 能稳定完成研究、写回报告。
- **阶段2（后）：** OC 只发 `run_research_task R-2026-001`；小白固定 runner 读task→读固定目录资料→调Claude Code+指定skills→生成输出→写回。
- SSH = "启动按钮"，**不是无限制远程控制通道**。OC 不发自由文本命令、不塞大段prompt。

## 7. skill 包先用哪些（不全装全开）
**🔒 第一轮白名单（总裁 2026-06-22 锁定，6个，复盘/质量门/回测类）：**
1. `signal-postmortem` — 信号复盘
2. `trade-performance-coach` — 交易纪律/执行质量
3. `weekly-performance-digest` — 周表现摘要
4. `backtest-expert` — 回测建议
5. `edge-strategy-reviewer` — 策略质量门(8项)
6. `data-quality-checker` — 数据/完成度质量
- （部分仍标 beta，报告里需 flag）
**先不用：** 股票筛选器、FMP/Finviz股票研究、美股主题轮动、"自动生成一大套策略直接进执行"的流程。
- 原因：与我们 期货/折扣区/Prop Firm 执行结构 不完全匹配。
- ⚠️ claude-trading-skills 偏股票/ETF研究工具箱，**不能假定直接适配 NQ/PropFirm/折扣区** → 先筛真正可用的，再建 futures adapter。

---

## 8. 下一步（总裁 锁定）= 不是安装，是先写规格
**先做 `Trading_Research_Bus_Specification_v1.0`** —— 定3种格式(现阶段)：
**3份**(现阶段)：①研究任务包 ②研究与质量审查报告(②③合并) ③批准文件。
- 小白是电脑/分析工具,不是审批机构;PASS/REVISE/REJECT = Claude 建议,President 才是关口。
- 未来半自动化时再拆成4份。

---
*DISCUSSION DRAFT v1 | 2026-06-22 | 总裁方案, Dragon记录 | 不实施待批准*
