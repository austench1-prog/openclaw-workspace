# Level50 Continuation Study — 温总 NT8 操作说明
# 版本：1.1 | 2026-06-25 | Dragon

---

## 第一步：安装代码

1. 打开 NinjaTrader 8
2. 顶部菜单 → **Tools → Edit NinjaScript → Strategy**
3. 左侧文件列表右键 → **New** → 命名为：`Level50ContinuationStudy`
4. 把 `Level50ContinuationStudy.cs` 的**全部内容**复制进去（替换默认代码）
5. 按 **F5** 编译 → 底部状态栏显示 **Compiled** = 成功

如果编译报错，截图发给 Dragon。

---

## 第二步：准备 NQ 数据

需要：**最近 100 个交易日的 NQ 1 分钟数据**

1. 打开或新建一个 **NQ（NQ 12-XX 或 NQ Continuous）** 的 **1-Minute** 图表
2. 右键图表 → **Data Series**
3. **Days to Load** 改为 **140**（多留一些缓冲）
4. 点 **OK** → 让数据加载完毕（看左下角进度条）

> ⚠️ 注意：NinjaTrader 需要先下载历史数据。
> 如果数据不足，去 **Tools → Historical Data Manager → Download**，
> 选 NQ、1 Minute、下载范围最近 6 个月。

---

## 第三步：加载策略

**方法 A：直接在图表上运行（推荐，实时看结果）**

1. 在刚才那个 NQ 1-Minute 图表上
2. 右键图表 → **Strategies → Add Strategy**
3. 找到 `Level50ContinuationStudy` → 双击
4. 参数设置：
   - **Calculate** = `On each tick`（默认应该已经是）
   - 其他参数默认即可
5. 点 **OK**
6. 策略会从历史数据开始跑，底部 Output 窗口会显示进度日志

**方法 B：Strategy Analyzer（批量回测）**

1. 顶部菜单 → **New → Strategy Analyzer**
2. Instrument: `NQ 12-XX`（当季合约）或 `@NQ` Continuous
3. Strategy: `Level50ContinuationStudy`
4. Data Series: 1 Minute
5. Date range: 最近 100 个交易日
6. 点 **Run**

---

## 第四步：查看结果

策略结束后，CSV 文件自动保存到：

```
C:\Users\[你的用户名]\Documents\NinjaTrader 8\csv\
```

会有三个文件：
- `Level50Study_5m_YYYYMMDD_HHmmss.csv` — 5分钟 event-level 数据（每行一个事件）
- `Level50Study_15m_YYYYMMDD_HHmmss.csv` — 15分钟 event-level 数据
- `Level50Study_SUMMARY_YYYYMMDD_HHmmss.txt` — 汇总报告（含对比表 + 结论）

把这三个文件发给 Dragon 分析。

---

## 常见问题

**Q: 编译时提示找不到某个 namespace？**
A: 截图发给 Dragon，可能是 NT8 版本差异，1分钟修复。

**Q: Output 窗口没有日志？**
A: 打开 Output 窗口：Control Center → New → Output Window

**Q: CSV 文件为空？**
A: 确认策略已经完整跑完（图表左上角策略名旁边不再有"Running..."）。
   或者在 Strategy Analyzer 里看到 "Run Complete"。

**Q: 数据只有几十天？**
A: Historical Data Manager 重新下载 NQ 1-Minute，选更长时间段。

---

## 数据质量说明

- **AMBIGUOUS** 行：同一根 1-Minute 柱同时碰到 High 和 Level25，无法判断顺序。
  这是规格书要求的处理方式，不是错误。
- **UNRESOLVED** 行：当天 session 结束前两个目标都没到达。
  保留在报告里，不计入 UP/DOWN 比率。

---

## 版本记录
- v1.0: 初版（2026-06-25）
- v1.1: 修正 HTF bar 索引读取方式（BarsInProgress 驱动）

---

*Dragon | 2026-06-25*
