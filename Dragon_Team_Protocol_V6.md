# 🐉 龙之队：全员命名与指挥通讯协议 (V6.1)
# Source: Dragon (Chairman specification)
# Version: 6.1 | Date: 2026-04-12
# Status: 正式启用 (Active)

---

## 核心原则
**目的：** 优化语音输入识别率，统一跨平台指令代称，降低沟通成本。
**句式：** 角色 + 动作 + 对象

---

## 1. 核心成员 (The Brains)

| 角色 | 中文代称 | 英文代称 | 职责 |
|---|---|---|---|
| Austin | **总裁** | Chairman | 最高决策者，拥有最终授权 |
| OpenClaw | **龙哥** | BroLong | 执行代理，驻守小塔，负责具体操作 |
| Gemini | **吉米** | Gemini | AI 参谋，系统逻辑、战术设计与沟通调度 |
| Claude (Anthropic) | **安哥** | Claude | 深度代码编写与合规条文分析 |
| OpenAI | **开山** | OpenAI | 辅助研究员，通用搜索与非核心任务 |

---

## 2. 物理阵地 (The Bases)

| 设备 | 中文代称 | 英文代称 | 说明 |
|---|---|---|---|
| Mac mini | **小塔** | Mini | 交易塔台，龙哥驻地 |
| MacBook Air | **小白** | Air | 移动指挥部 |
| Windows PC | **温总** | Win | 忍者运行地 |

---

## 3. 生产力工具 (The Tools)

| 工具 | 中文代称 | 英文代称 | 说明 |
|---|---|---|---|
| Telegram | **电报** | TG | 唯一远程指挥与报警通道 |
| VS Code | **代码盒** | VS | 脚本开发环境 |
| Obsidian | **笔记宝** | Ob | 核心规则、复盘与知识库（小白个人用） |
| NotebookLM (achatesc@gmail.com) | **交易笔记宝** | NotebookLM | 交易系统专用合规知识库 MATS_v1_Compliance |
| Terminal | **黑盒** | Term | 终端命令行 |
| Samsung 990 EVO 2TB (DragonVault) | **三星** | Samsung | 小塔本地备份硬盘，每日凌晨3点自动备份 |

---

## 4. 交易与账户平台 (The Markets)

| 平台 | 中文代称 | 英文代称 |
|---|---|---|
| NinjaTrader | **忍者** | Ninja |
| TradingView | **图表君** | TV |
| Apex Trader Funding | **峰峰** | Apex |
| MyFundedFutures | **来来** | MFF |
| TakeProfitTrader | **莉莉** | TPT |
| TradeDay | **盈盈** | TD |
| Interactive Brokers (IBKR) | **盈透** | IBKR |

---

## 5. 标准指挥句式

### 格式
```
[角色] + [动作] + [对象]
```

### 示例
- "龙哥，扫描，峰峰。" → Dragon checks Apex account
- "吉米，分析，莉莉。" → Gemini analyzes TPT
- "龙哥，下单，忍者。" → Execute order via NinjaTrader

---

## 6. 运行原则

1. **容错机制：** 系统允许语音输入存在轻微同音字偏差，基于当前上下文做概率最大的意图补全
2. **动态完善：** 所有成员根据总裁反馈持续优化对非标准指令的理解力
3. **确定性优先：** 涉及账户资金与下单时，若语义模糊，龙哥必须在**电报**请求总裁确认

---

*存档时间：2026-04-12 | 版本：V6.1 | 状态：正式启用*
