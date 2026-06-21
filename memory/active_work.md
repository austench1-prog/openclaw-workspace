# Active Work — last updated 2026-06-20 17:21 PDT

## 今天做完的（6/20）
- 找回 6/12 One Setup For Life 原图（file_511），建 material_specimens/one_setup_ict/
- 补档 austin_raw_archive **2026-06-10 ~ 2026-06-14（32张图，5个日期）**
- 6/11 的 3 张 UUID 文件名图已规范命名 + _RAW.md 引用同步
- 发现并归档 6/14 ICT Time and Price 参考卡
- 67张图批次重发补档完成（批次1-8，共68张）
- staging 20张已移入 2026-06-10/（共29张）
- _RAW.md 已追加/新建：6/10, 6/11, 6/12, 6/13, 6/14, 6/15, 6/16
- 底片层：6/10–6/16 全部有图有记录
- **今天素材归档这摊 = 已完全收尾。**

## ⏳ 压着未完成
（已清空）

## 今天傍晚做完（17:00-17:21）
- 系统：加 OpenAI 作 backup provider（Anthropic 不再单点），thinking 全局关 off（根治 signature 报错）
- cron 修复：MFF风控提醒改 `7 17 * * 1-5`（周末不再乱叫）
- 图片整理①②③全完成：
  - ① 6/15+6/16 共27张 inbound 规范命名+补 _RAW.md（git mv 纯rename）
  - ② 复盘抓到 6/20 目录2张图实为历史截图（Chairman 决定不动）
  - ③ 新建 material_specimens/ob_composite/ 类目（OB综合结构），首批3张 specimen（6/13×1 + 6/14×2）+ _INDEX.md
- material_specimens 类目更新：现5个（pinbar_doji / candle_retracement / three_strike_reversal / one_setup_ict / **ob_composite**）

## 今天晚间补档收尾（17:30-18:30）
- **两个最低标准查实并达成**（今天补回的67张：pre_0615×47 + staging×20）：
  - ✅ 标准①原件全收：67张全在底片，无字节重复、无损坏（“缺20张”是 96→67 去重误判）
  - ✅ 标准②理解记录归类：逐张看图建 `pre_0615_backfill/_IMAGE_MANIFEST.md`（47张全对位：24 K线/21 文字/2 非交易）
- **①归位完成：** staging 发现是 2026-06-10 重复副本（20张字节相同已在 2026-06-10/）→ 详细版117行 _RAW.md 合并进 2026-06-10/_RAW.md（现160行，含 Chairman 原话逐字）+ 删除冗余 _conversation 目录。
- **② 6项补正文 = to-do**（已记在 `pre_0615_backfill/_IMAGE_MANIFEST.md` §四「待补/真空白」）：
  1. ICT KillZone / True Day Open / 16:00收盘位指标（49d925a6）
  2. TNTL + HH&LL Scalper 指标（893a74fb）
  3. 系统缺口清单（e3e32f4e：进场触发/每日方向流程/200SMA整合）
  4. T10·Deviation（ac4859ba 部分新内容）
  5. 4H vs 周线盘整完成度反思（d3cbe899）
  6. 外部风控素材图（365e35da，胜率×盈亏比）
  → 这 6 项只是“看清”，还没写进素材库正文，Chairman 决定何时补。

## 🗓️ 待办
- 周一开始：每日收到素材图即存当天 raw-archive（新规则已生效）
- **沉压待办：pre_0615 的 47张仍在 backfill 临时目录**，UUID名未改、未按日期归位（需 Chairman 补日期后迁移；manifest 已建可随时查）
