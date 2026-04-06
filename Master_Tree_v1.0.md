# 龙大哥交易系统 — 总树状图
# Source: Dragon | Version: 1.0 | Date: 2026-04-05
# 一页纸版本，可直接打印

---

```
龙大哥交易系统
│
├── 系统A：自有资金（投资系统）
│   ├── SPX 期权 $30K（TOS → IBKR 迁移中）
│   │   ├── 0DTE 策略（手动 + AI 提醒）
│   │   └── 执行：TOS 看盘，IBKR 下单
│   └── 期货 $6K（AMP Futures）
│       ├── 策略验证场
│       └── 执行：NinjaTrader + 信号通道
│
├── 系统B：Prop Firm（经营系统 / 类工资）
│   ├── 启动资金 $4K
│   ├── 平台（优先顺序）
│   │   ├── 1. TradeDay（Static DD，最安全）
│   │   ├── 2. MFF（EOD DD）
│   │   ├── 3. TPT（EOD/Static，待确认）
│   │   └── 4. Apex（Intraday DD，最后）
│   ├── 执行通道
│   │   ├── NinjaTrader 8（Windows PC）
│   │   └── Replikanto（Master → 4家账户复制）
│   └── 目标：每月稳定 Payout
│
├── AI 系统（工具层）
│   ├── ✅ 已完成
│   │   ├── Prop Rule Copilot V2.1（合规检查）
│   │   ├── Mac-Windows 信号通道（HTTP）
│   │   ├── NinjaScript 接收器（Windows）
│   │   └── Python 信号服务器（Windows）
│   │
│   ├── 🔄 进行中
│   │   ├── NinjaScript File Strategy（读 signal.txt）
│   │   ├── Setup Alert Agent（策略信号提醒）
│   │   └── IBKR 连接（等账户开设）
│   │
│   └── 📋 计划中
│       ├── Pre-market Brief（每日盘前推送）
│       ├── Review Copilot（收盘复盘）
│       ├── Trade Gatekeeper（信号过滤）
│       └── 总风控 Agent（全账户合并）
│
├── 基础设施
│   ├── Mac mini（主脑 / OpenClaw）
│   ├── Windows PC（NinjaTrader / Replikanto）
│   ├── MacBook Air（移动端监控）
│   ├── GitHub（代码同步）
│   └── Obsidian（知识库）
│
└── 公司架构
    ├── APM LLC（Nevada，S-Corp，运营收入）
    │   └── 银行：Citibank 开户中
    └── Meritpoint Logic LLC（Nevada，Disregarded，交易账户）
        └── 475(f) MTM 已提交
```

---

## 执行优先级（本周）

| 优先级 | 任务 | 状态 |
|---|---|---|
| 🔴 1 | NinjaScript 接入 signal.txt → 模拟下单 | 今天装了，明天测试 |
| 🔴 2 | Prop Firm 自动化政策确认（4家）| 待确认 |
| 🔴 3 | TradeDay 新账户购买 | 待执行 |
| 🟡 4 | IBKR 公司账户申请 | 周一 |
| 🟡 5 | Setup Alert Agent（你提供策略条件）| 待你输入 |

---

## 信号流（当前状态）

```
你的判断
    ↓
Telegram → 龙大哥
    ↓
Mac mini Python 发信号
    ↓  HTTP (192.168.0.59:5000)
Windows Python Server
    ↓  写入 signal.txt
NinjaScript Strategy（读取文件）
    ↓  下单
NinjaTrader 账户
    ↓
Replikanto 复制 → 4家 Prop Firm
```

*打印建议：A4横向，字号9pt*
