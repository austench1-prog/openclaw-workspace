# 02 核心资产与工具库 (Assets Library)
> 触发词：「这个很重要」/ 「加入军械库」
> 作用：记录最重要的工具、策略和想法，简化成一眼能调用的说明

---

## 格式模板

```
### 资产名称
**类型：** 工具 / 策略 / 公式 / 框架
**一句话说明：** 这是什么，用来做什么
**关键参数：** 最核心的数字或逻辑
**文件位置：** workspace 里的路径
**使用场景：** 什么时候用
```

---

## 指标类

### Camarilla F1 - 日线基础版
**类型：** 技术指标
**一句话说明：** 用今天 H/L/C 预测明天 TH（目标高点）和 TL（目标低点）
**关键公式：**
```
P = (H + L + C) / 3
B = 2P
TH = B - L
TL = B - H
```
**文件位置：**
- TradingView：`camarilla-formula1-spx.pine`
- TOS：`camarilla-pivot-indicator-thinkscript.txt`
**使用场景：** SPX 日内交易，开盘前确认当天参考区间

---

### Camarilla F2 - 加权开盘版（B=2P-D）
**类型：** 技术指标
**一句话说明：** 在 F1 基础上引入开盘价修正，让区间更贴近实际开盘行为
**关键公式：**
```
P = (H + L + C) / 3
D = C - TO   (TO = 今天09:30开盘价)
B = 2P - D
TH = B - L
TL = B - H
```
**文件位置：**
- TradingView：`camarilla-formula2-spx.pine`
- TOS：`camarilla-formula2-thinkscript.txt`
**使用场景：** SPX，09:30 开盘后才能生效（需要 TO）

---

### Camarilla F2B - 加权开盘版（B=2P+D）
**类型：** 技术指标
**一句话说明：** F2 的反向版本，区间偏高
**关键公式：** B = 2P + D（其余同 F2）
**文件位置：** TOS：`camarilla-formula2b-thinkscript.txt`
**使用场景：** 与 F2 对比使用，判断哪个方向更准

---

### Camarilla F3 - NQ 期货版
**类型：** 技术指标
**一句话说明：** 专为 NQ 设计，用 24小时 H/L/C + 0:01am 开盘价预测明天区间
**关键公式：**
```
P = (H + L + C + O) / 4   (O = 0:01am NYT 期货开盘)
B = 2P
TH = B - L
TL = B - H
```
**文件位置：** TradingView：`camarilla-formula3-futures.pine`
**使用场景：** NQ 期货日内交易，显示窗口 00:00-16:00，16:01后自动切换明天预览

---

### Camarilla 周线版
**类型：** 技术指标
**一句话说明：** 用本周 H/L/C/O 预测下周 NWH（下周高）和 NWL（下周低）
**关键公式：**
```
P = (WH + WL + WC + WO) / 4
B = 2P
NWH = B - WL
NWL = B - WH
```
**文件位置：** TOS：`camarilla-weekly-thinkscript.txt`
**使用场景：** 每周五收盘后看下周区间

---

## 系统类

### Prop Rule Copilot（计划中）
**类型：** AI 工具
**一句话说明：** 开仓前自动检查各 Prop Firm 规则，告诉你今天能不能做、最大风险是多少
**关键逻辑：** 读取账户状态 → 对照规则库 → 输出可交易性判断
**文件位置：** 待建立
**使用场景：** 每天开盘前必查

---

### OpenClaw 双电脑架构
**类型：** 基础设施
**一句话说明：** 旧电脑主版本，新电脑副本，AirDrop 单向更新
**关键路径：**
- 主版本：旧电脑 `~/.openclaw`
- 副本：新电脑 `~/.openclaw`
- 更新包：`~/openclaw-backup-latest.zip`
**使用场景：** 每次旧电脑有重要更新后，同步到新电脑

---

## 参考视频类

### SMB Capital — How to use Claude To Gain a Huge Day Trading Edge
**类型：** 参考视频 / 策略派
**链接：** https://youtu.be/Rqmdw4xyIMM
**技术含量：** 7/10
**核心价值：**
- 交易"尸检"逻辑 → 对应 Review Copilot
- 盘前计划自动化 → 对应 Pre-market Brief
- Prompt 三原则：具体、迭代、先模拟盘验证
**局限：** 无 Prop Firm 多账户概念，Pine Script 部分已超越

---

### Alex Carter — How to Build Your Own AI Trading Bot Using Claude Code
**类型：** 参考视频 / 工程派
**链接：** https://youtu.be/tsCI72TWzsg
**技术含量：** 8.5/10
**核心价值：**
- Telegram → AI → 执行 闭环架构（你已有）
- TradingView Webhook → AI 过滤 → 执行 → 对应 Trade Gatekeeper
- 本地运行 + API Key 不过第三方（你已做到）
**局限：** 针对加密货币，无 Prop Firm 风控，无多账户同步

**综合判断：** 两个视频验证了你的方向是对的。你的架构已超越视频层级，差的是三个具体工具的落地：Trade Gatekeeper / Prop Rule Copilot / Review Copilot

---

## ⭐ 重要策略构想

### 战术执行核心（Tactical Execution Core）
**类型：** 核心策略构想（待实现）
**重要程度：** ⭐⭐⭐⭐⭐ 最高优先级
**来源：** 总裁口述 + 吉米（Gemini）分析，2026-04-06

**总裁的核心想法：**
> 我判断行情从100点到200点，但市场走得一波三折（100→150→105→155→145→210）。
> 我需要AI利用超快计算，在这曲折的路径里把风险"洗"掉。
> 目标：即便行情只走了70点就反转，我仍然是赚钱离场。

**三个执行维度：**

1. **动态成本缩减（Cost Reduction）**
   - 价格冲高时，自动减仓锁定部分利润
   - 回调时，将锁定利润再买回
   - 结果：整体底仓成本降低，即便未到目标价也盈利

2. **动势追踪与平滑（Momentum Smoothing）**
   - 实时计算 ATR / 微观斜率
   - 区分"健康回调"和"趋势衰竭"
   - 利用回调空间做极短线波段，用套利覆盖主仓位风险

3. **智能止盈与保本点锁定（Break-even Management）**
   - 价格越过关键阻力位 → 自动将止损拉到保本位
   - 每秒计算，根据盘口数据精确定位，而非凭感觉

**实施路径（总裁确认的方向）：**
- 不做全自动"黑盒"
- 做**半自动执行模组**：总裁定方向 → AI 接管过程 → 结算总裁依然绿单
- 第一步：总裁整理"真师傅"逻辑 → 龙哥建模 → 峰峰账户验证

**待总裁补充：**
- [ ] 你最常用的一种"微观动作"（比如：回调多少点减仓、什么条件加回来）
- [ ] 最想先在哪个品种实现（NQ / SPX 0DTE）
- [ ] 回调容忍度（比如：超过15点就减仓一半）

---

*触发词：「这个很重要」或「加入军械库」→ 龙哥自动追加新资产*
