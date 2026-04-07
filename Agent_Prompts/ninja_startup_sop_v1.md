# 忍者开机 SOP (Ninja Startup SOP)
# Source: Dragon (based on Chairman specification)
# Version: 1.0 | Date: 2026-04-07
# CRITICAL: All 5 checkpoints must pass before any order is placed

---

## 5-Point Pre-Trade Checklist

### Checkpoint 1: Prop Firm Connection
- Open NinjaTrader → Connections
- Confirm target prop firm account is connected (Green status)
- Confirm account: **APEX-165583-123** (or target account) shows as Active
- ❌ If disconnected → reconnect before proceeding

### Checkpoint 2: Replikanto Leader Account
- Open Replikanto control panel
- Confirm **Leader Account = Sim101** (or designated leader)
- Confirm leader account status is Active and connected
- ❌ If wrong account → change leader before proceeding

### Checkpoint 3: Replikanto 双重勾选（最关键）

**必须同时满足两个条件，缺一不可：**

| 条件 | 说明 | 结果 |
|---|---|---|
| On ✅ + Cross Order ✅ | 两个都勾 | ✅ 成交，按 MNQ 微型复制（损失$13）|
| On ✅ + Cross Order ❌ | 只勾 On | ❌ 成交，按 NQ 全尺寸复制（损失$130！）|
| On ❌ + Cross Order ✅ | 只勾 Cross | ❌ 不成交 |

**操作：**
- Apex 那行 **On = 绿灯** ✅
- Apex 那行 **Cross Order = MNQ** ✅
- ❌ 缺任何一个 → 禁止下单

### Checkpoint 4: Chart Account Alignment
- Go to Chart/Chart Trader
- Confirm **Account = Sim101** (Leader Account, NOT Apex directly)
- Confirm chart is showing the **correct instrument** for this session
- Confirm DragonFileSignal Strategy is **Enabled** on this chart
- ❌ If wrong account or instrument → fix before proceeding

### Checkpoint 5: Full Alignment Verification
Before ANY order:
```
Prop Firm connected?      ✅ / ❌
Leader Account = Sim101?  ✅ / ❌
Cross Order selected?     ✅ / ❌
Follower (Apex) active?   ✅ / ❌
Chart Account = Sim101?   ✅ / ❌
Chart instrument correct? ✅ / ❌
Strategy Enabled?         ✅ / ❌
```
**All 7 must be ✅ before any signal is sent.**

---

## Multi-Strategy / Multi-Account Rules

- Each Leader Account handles ONE instrument or ONE strategy
- Current setup: 3 Leader Accounts available (Sim101, SimNQ, SimFF)
- If trading multiple instruments simultaneously → use separate leader accounts
- Never mix instruments in same Replikanto panel

---

## Dragon's Self-Check Protocol

Before sending any signal, Dragon must verify:
1. SSH to Windows → check NinjaTrader process running
2. Signal Server responding
3. Send test ping → confirm response
4. Only THEN send trading signal

---

## Replikanto 关键规则（2026-04-07 实测确认）

### 状态持久性
- **On（绿/白）** 和 **Cross Order** 一旦设置，**不会自动改变**
- 只有人为操作才会改变
- 开机后状态保持上次设置

### 颜色含义
- **绿色** = 已勾选/已激活
- **白色** = 未勾选/未激活

### 双重条件（缺一不可）
```
On = 绿 ✅  +  Cross Order = 勾 ✅  =  Apex 按 MNQ 复制（小额）
On = 绿 ✅  +  Cross Order = 无 ❌  =  Apex 按 NQ 全尺寸复制（大额！）
On = 白 ❌  +  Cross Order = 勾 ✅  =  不复制
```

---

## Strategy 启动规则

### 只启用一个 DragonFileSig
- **1 Minute = 绿色**（启用）✅
- **5 Minute = 白色**（禁用）
- 两个同时启用会互相干扰

### Enabled 颜色确认
- 绿色行 = Strategy 正在运行，会读取信号
- 白色行 = Strategy 停止，不读信号

---

## Failure Modes Observed

| Failure | Cause | Fix |
|---|---|---|
| Order placed but no Apex fill | Cross Order not selected | Check Replikanto Cross Order |
| Strategy shows but no execution | DragonFileSignal not Enabled（白色）| Enable in Strategy Manager |
| Signal sent but no chart response | Wrong account on chart | Set chart account to Sim101 |
| FLATTEN_ALL doesn't work | Strategy CLOSE signal name mismatch | Fixed in v2: ExitLong() no filter |
| Two strategies interfering | 1min + 5min both enabled | Disable 5min, keep only 1min |
| SL triggered too fast | SL set too tight | Adjust SL points in signal |

---

## Version Notes
V1.0: 2026-04-07 live testing
- Confirmed: Cross Order + On must both be green for MNQ fill
- Confirmed: Replikanto settings are persistent (no auto-reset)
- Confirmed: Green = active, White = inactive
- Confirmed: Only enable 1 Minute strategy, disable 5 Minute
- Confirmed: SL/TP auto-trigger works correctly
- Fixed: CLOSE/FLATTEN_ALL now uses ExitLong() without signal name filter
