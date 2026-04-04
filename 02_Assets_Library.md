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

*触发词：「这个很重要」或「加入军械库」→ 龙大哥自动追加新资产*
