# NinjaTrader 8 ATM Strategy — 真实结构核实 (2026-06-22)

> 方法：SSH 免密进温总 PC (auste@192.168.0.226) 直接读 ATM 模板 XML 文件。
> 不需 GUI/截图。模板路径：`C:\Users\auste\...\OneDrive\<文档>\NinjaTrader 8\templates\AtmStrategy\`
> 温总 PC 现有 11 个 ATM 模板，其中 Test 5x* 系列已在试"5手分3批"结构。

## 核心结论
**ATM 100% 能承载折扣区模型的"出场侧"**（分批TP + 保本上移 + 移动止损）。温总已在摸同样路子。
加仓/分叉 = 手动（Chairman 已确认走 A 路线）。

## ATM 模板 XML 结构（关键字段）

顶层（整张模板级）：
- `<EntryQuantity>` = 总进场手数（如 5）
- `<CalculationMode>` = **Ticks** 或 **Price** 或 **Pips/Percent** — 决定下面 SL/TP 数字含义
  - Ticks = 相对点数（NQ 1 tick = 0.25 点 = $5/手）；Price = 绝对价格（可填结构位！）
- `<DefaultQuantity>` / `<EntriesPerDirection>` / `<TimeInForce>`

`<Brackets>` 下多个 `<Bracket>`（= 分批，每批独立管理）：
- `<Quantity>` 该批手数（各批之和 = EntryQuantity）
- `<StopLoss>` 该批止损（Ticks 或 Price）
- `<Target>` 该批止盈
- `<StopStrategy>`（可选，止损动态化）：
  - `<AutoBreakEvenProfitTrigger>` 赚到X→触发保本
  - `<AutoBreakEvenPlus>` 保本后止损放在 入场±Y
  - `<AutoTrailSteps>` 移动止损阶梯：每个 `<AutoTrailStep>` = {ProfitTrigger 利润达到→StopLoss 止损跳到某位}
  - 可多个 AutoTrailStep 叠成阶梯（冲最高TP用）

## 温总现有样本（实证）
| 模板 | EntryQty | 结构 |
|---|---|---|
| Test 5x300x3 | 5 | B1(2手,SL30,TP300,BE100+25,trail400→60) / B2(2手,SL40,TP300,BE100+60) / B3(1手,SL60,TP300) |
| Test 5x | 5 | B1(2,SL30,TP60,BE100+25) / B2(2,SL40,TP80,BE100+60) / B3(1,SL60,TP300) |
| Test 5x4060 | 5 | B1(2,SL40,TP80) / B2(2,SL60,TP300,BE120+60) / B3(1,SL120,TP300) |
全部 CalculationMode=Ticks。

## 对折扣区模型的映射（A路线）
- **进场+加仓 = 手动**（buy/sell/buy stop/sell stop），ATM 不自动加仓。
- **出场结构 = ATM 模板**：把"剩N手分批冲TP + 保本 + 移动止损"做成 Brackets。
- **TP/SL 现实问题（Chairman 锁定）**：模板用标准点数打底（CalcMode=Ticks），实盘手动拖到真实结构位即可。或改 CalcMode=Price 直接填结构价。
- **关键待解 = 统一下单管理 + 整体风控**：A路线靠"模板预设 + 手动纪律 + 现有 FLATTEN_ALL 信号链 + 后台风控(MFF→Tradovate / TPT→CQG)"兜；全自动 gatekeeper 是 B路线(NinjaScript)的事。

## 工具路线（对应三步骤验证法）
1. 现在：读/改 ATM 模板 XML（SSH 文本，最准） → 出方案
2. 尼家模拟账验证：手动 ATM + SuperDOM
3. 后期：NinjaScript + ATM API + 中心化 risk manager（全自动，B路线）

## 远程访问现状
- SSH(22) 开 + 免密(密钥)可达；信号链 port 5000 活。
- RDP(3389)/VNC(5900) 未开 — 需要 GUI 时再开 Windows 自带 RDP(mstsc)，多数情况读文件即可不需要。
