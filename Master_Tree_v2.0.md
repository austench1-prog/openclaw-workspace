# 龙哥交易系统 — 总树状图
# Source: Dragon | Version: 2.0 | Date: 2026-04-07
# 一页纸版本，可直接打印

---

```
龙哥交易系统
│
├── 系统A：自有资金（投资系统）
│   ├── SPX 期权 $30K
│   │   ├── 0DTE 策略（手动看盘 + AI 辅助）
│   │   └── 执行：TOS 看盘 → IBKR 下单（迁移中）
│   └── 期货 $6K（AMP Futures）
│       ├── 策略验证场
│       └── 执行：忍者 → 信号通道
│
├── 系统B：峰峰经营系统（Prop Firm）
│   ├── 启动资金 $4K
│   ├── 平台优先顺序
│   │   ├── 1. 盈盈（TradeDay）— Static DD，最安全
│   │   ├── 2. 来来（MFF）— EOD DD
│   │   ├── 3. 莉莉（TPT）— EOD/Static
│   │   └── 4. 峰峰（Apex）— 测试账户（APEX-165583-123）
│   └── 执行通道
│       ├── 忍者（NinjaTrader 8）— 温总
│       └── 复制器（Replikanto）— Sim101 → 峰峰
│
├── 信号链路（已验证 ✅）
│   ├── 总裁判断方向
│   │       ↓ Telegram 指令
│   ├── 龙哥（小塔）发信号
│   │       ↓ HTTP → 温总 :5000
│   ├── Python Server（温总）
│   │       ↓ 写入 signal.txt
│   ├── DragonFileSignal Strategy（忍者）
│   │       ↓ 下单 Sim101
│   └── 复制器 → 峰峰 MNQ 自动成交
│
├── AI 系统（工具层）
│   ├── ✅ 已完成
│   │   ├── 信号通道（Mac → Win → 忍者）
│   │   ├── Prop Rule Copilot V2.1（合规检查）
│   │   ├── Playwright（自动读账户数据）
│   │   ├── 系统巡检（每小时检查SSH/内存）
│   │   └── 紧急平仓 FLATTEN_ALL / CLOSE_ALL
│   │
│   ├── 🔄 进行中
│   │   ├── NinjaScript SL/TP 颜色修复（需 Compile）
│   │   ├── IBKR 公司账户申请
│   │   └── 战术执行核心（总裁策略整理后启动）
│   │
│   └── 📋 计划中
│       ├── Pre-market Brief（每日盘前推送）
│       ├── Review Copilot（收盘复盘）
│       ├── Trade Gatekeeper（信号过滤）
│       ├── 总风控 Agent（全账户合并）
│       └── Gemini API 接入电报（策略判官）
│
├── 基础设施
│   ├── 小塔（Mac mini）— 龙哥主脑
│   ├── 温总（Windows PC）— 忍者/复制器
│   ├── 小白（MacBook Air）— 移动监控端
│   ├── GitHub（代码同步）
│   └── 笔记宝（Obsidian）— 知识库
│
└── 团队
    ├── 总裁（Austin）— 决策/点火
    ├── 龙哥（OpenClaw）— 系统工程师 + 总裁助理
    ├── 吉米（Gemini）— 离岸参谋
    └── 安哥（Claude）— 策略逻辑专家
```

---

## 本周执行优先级

| 优先级  | 任务                              | 状态    |
| ---- | ------------------------------- | ----- |
| 🔴 1 | Compile 忍者新版 Strategy（SL/TP 颜色） | 今天开盘前 |
| 🔴 2 | 盈盈（TradeDay）购买考试账户              | 本周    |
| 🔴 3 | IBKR 申请（银行账户到手后）                | 本周    |
| 🟡 4 | 总裁整理第一个策略逻辑                     | 随时    |
| 🟡 5 | 温总 WOL 设置（龙哥远程唤醒）               | 本周    |

---

## 当前信号流状态

```
Mac mini（小塔）→ 192.168.0.59:5000 → signal.txt
→ DragonFileSignal → Sim101 → Replikanto → 峰峰 MNQ
```

*打印建议：A4横向，字号9pt*
