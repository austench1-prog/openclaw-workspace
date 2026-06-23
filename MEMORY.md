# MEMORY.md - Dragon Long-Term Memory
# Last updated: 2026-06-22
# This file is my curated long-term memory. Load in main session only.

---

## 中文名字锁定（2026-06-23）
- **DragonFileSignal** = **法师**（大法师）
- **Hermes agent / 小黑** = **星星**（新新）
- **Dragon（我）** = 龙哥
- **总裁** = Austin / President

---

## ⛔ 称呼铁律（2026-06-22 锁定，一刀切，不再反复）
**Austin 的称呼，未来统一只用「总裁 / President」。绝不再用 "Chairman"。**
- 中文 = 总裁；英文 = President。
- 过去文件里的旧 Chairman 不用清（历史无所谓）；**从现在起，新写的任何内容只用总裁/President。**
- 这条铁律的目的：终结"一会儿总裁一会儿Chairman"的反复。读到这里 = 立刻照办。

---

## ⭐⭐ 折扣区 ATM 模板 — 原理框架（2026-06-22 一整天搞懂，未来的我别再绕！）

> President 陪了一整天就为让我搞懂这套原理。**重点不是4个XML文件，是"理解模板的思维框架"——这个最容易丢。读到这里=立刻想起来，不要再从头绕一遍。**
> 干净文件在 `Trading/atm_templates_FINAL_4/`（4个DZ_*.xml + _README_权威说明.md）。详细清算在 `Trading/ATM_project/REVIEW_认知清算_v1.md`。

### 一句话原理
**ATM 一个模板 = 一笔进场 + 它的出场。ATM 不能自动加仓，也装不下"完整加仓策略"。**

### TP 公式（总裁 锁定的真理，别再瞎算）
**TP = 入场 + (止损距离 × 盈亏比倍数)。** 先定要亏多少(=止损距离)，TP由盈亏比出来。
- 例：价100亏100 → 1:1→TP200；1:5→TP600。

### 4个模板 = 同一设计 × 2参数对照（2×2）
全部：**进场4手 + 单止损 + 单TP A全出。无分批/runner/trail/保本。**
| 模板 | 止损 | 盈亏比 | TP A |
|---|---|---|---|
| DZ_80_1to5 | 80 | 1:5 | 400 |
| DZ_80_1to1 | 80 | 1:1 | 80 |
| DZ_30_1to5 | 30 | 1:5 | 150 |
| DZ_30_1to1 | 30 | 1:1 | 30 |
- 唯一变量=止损(80/30)×盈亏比(1:5/1:1)。MNQ: 1pt=4ticks。命名=DZ_止损_盈亏比。
- **止损80/30已ATR定案**(15M ATR≈69→80; 1M ATR≈17→30)，不再讨论。

### ATM 能力边界（我"每次到这就卡住"的根，搞清了别再卡）
- ATM **不能自动加仓**：模型"碰50%加1+回来加3=4手"是**2个价位的2笔进场**，一个ATM装不下 → **加仓=手动**(A路线锁定)。
- 这4个模板的角色 = **"出场参数包"**，不是"完整策略容器"。手动加仓后给仓位套对应出场。
- **"穿过线再回来才买"** = 用**限价单(Limit)挂在折扣线**，价格回踩才成交(下单时手动选,非模板字段)。
- **Stop Strategy 字段** = 止损自动移动(保本/trail)的设置；我们不用，**留空/None就对**。
- 远程能力：我SSH能读写推送模板XML(全自动)；但**下载回放数据/切Playback/图形挂单=GUI**，我做不了(NinjaTrader无命令行接口),需总裁在AnyDesk操作。OIF(incoming文件夹)能程序化下单但不能下数据。

### 操作闭环（手动补足决策树，免忘）
模板是TP A全出，实操手动补：①TP A前把部分仓止盈提到Original TP ②TP A处卖一半 ③冲过TP A后剩余仓止损跟Original对齐。之后可能走出交易区B→再做一次(今天不考虑B)。

### ❌ 已清除的错误认知（未来的我绝不能再犯）
5手(应4手) / 三档分批TP160-300-400(应单一TP A) / 保本BE / 移动止损trail / "占位vs策略树结构" / "简单vs复杂混在4个里" / "时间框架写进模板"(ATM框架无关=分型) / "TP=止损×倍数是我配的"(错,是TP公式算的)。旧占位版DZ_15M_80x400已彻底删除。

### 现状（2026-06-22）
ATM策略落地**暂停**(总裁定)——ATM边界已清楚:它当"出场管家"够用,但装不下完整加仓策略。要全自动1+3加仓需上NinjaScript(B路线,现阶段不做)。4模板已上温总PC+workspace,干净无残留。

---

## Who I Am

- Name: Dragon (龙哥)
- Role: Chief System Engineer + Executive Assistant to President (Austin)
- Host: Mac mini (小塔), running OpenClaw
- Primary channel: Telegram
- Language: Chinese with President, English for all workspace files and code

---

## Who I'm Working With

- **President (Austin / 总裁)**: The decision-maker. Strong systems thinker, excellent market judgment. Does not need to understand technical details — needs execution. Values simplicity, directness, and results.
- **Jimmy (吉米)**: Gemini — research and analysis. Good at frameworks, sometimes over-explains.
- **OpenAI (开山)**: Good at writing and structure. Tends to be verbose (President told him "just give the answer").
- **Team rule**: Dragon executes, Jimmy researches, OpenAI writes, President decides.

---

## System We're Building

**MATS v3.0 — Multi-Agent Trading System**

Purpose: Automate trading execution while maintaining compliance and risk control.

One-line definition:
> Rule verification → Strategy judgment → Risk gating → Automated execution → Dual endpoints (NinjaTrader + IBKR)

**Five Layers:**
- A: Rule & Compliance (Prop Intelligence Agent + NotebookLM)
- B: Strategy Judgment (Trading Strategy + Order Strategy)
- C: Gating & Execution (Gatekeeper + Execution Engine)
- D: Account & Route (NinjaTrader/Tradovate + IBKR)
- E: Infrastructure (Win PC + NinjaTrader + Replikanto + Signal Server)
- F: Management (Dragon-A ops + Dragon-B assistant)

**Phase status (as of 2026-04-10):**
- Phase 0-5 ✅ Complete
- Phase 6: Pending President's first strategy input
- Phase 7 ✅ Complete
- Phase 8: Final acceptance (after Phase 6)

---

## OC + Hermes 下一阶段架构（总裁 2026-06-19 锁定，讨论稿阶段—未实施）

全文：`Architecture/OC_Hermes_System_Judgment_v1.md`（精简主干版）+ `Architecture/OC_Hermes_Bus_Spec.md`（信息总线）。**纯设计，未建任何真东西。**

### 第一过滤器（总闸门，锁定）
- **1分效率却要 5分工作量 → 直接砍。** 系统不需解决所有问题；不断找可能性，但不一直给它找麻烦。追求最简洁版。

### Hermes = 独立第二执行层
- **机器名 = 小黑（Hermes 主机，2026-06-22 命名）。** 跟小塔同型号 Mac mini（规格待补记）。
- 第二台 Mac mini `austinha` 账户，独立 Telegram bot，独立 `.hermes`。模型：OpenAI gpt-5.5 主 / Anthropic opus-4-8 备。排除 Nous/OpenRouter/中国源。
- Hermes core workspace：`/Users/austinha/Documents/Hermes_Migration_Pack/Obsidian_Core`。
- **分工：OC = 手+闸门（执行/路由/风控/交易提醒/参数包）；Hermes = 后台+外勤（网站/规则/合规库/账户/行政/提醒/报告）；主席 = 大脑（策略方向/授权/批准）。**
- v3.0 = 骨架，不重建；Hermes 是加法，不破坏主链。

### 精简主干 = 只做 4 件
1. **Module A** 视觉证据输入 — 零成本已能用（纪律：没图不瞎猜）。
2. **Module D** 策略参数包 — ⭐最高杠杆，杀掉每天改 Pine（固定层逻辑 + 每日参数层）。
3. **Module C** 提醒中继 — 价格到位自动叫你，**只提醒不下单**。
4. **Hermes F1** 公开页监控 + 后台/合规/行政。
- **砍掉/搁置：** AMP 自动下单（系统对系统太难，完全排除）、买数据 API、F2/F3 登录态站内自动化、Module E 多策略路由器、执行桥（TV→实盘）。
- **永久排除（需单独批）：** 执行桥、现金账户密码、实盘交易 API Key。

### 已拍板决定
- **建设顺序：先做 Module D（地基，C 依赖它）。**
- **Module C 触发源 = 温总 NinjaTrader feed ONLY**（不用 TV、不买 API）。理由：TV 与尼加数据有时不一致，交易在尼加成交 → 提醒价必须 = 成交价。整条 参数→盯盘→提醒 链自给自足。
- **Hermes F1 首站 = TradeDay**（账户 ~7/4 到期驱动；F1 = 公开页，不需登录，Apex 登录难不影响 F1）。
- **合规库：沿用 v3.0 现有 NotebookLM + Compliance Pack，不新建**；Hermes 只抓变化 → 生成差异摘要 → 主席批准后才更新库。

### 信息总线（主席最担心的问题，锁定）
- **三层：** ①共享文件总线=主渠道/唯一真相源 ②消息层=门铃（不是真相）③人工入口=总裁入口。
- **总线 = Git 私有库 `mats-bus`**（2026-06-22 改：一条总线三方共挂 OC↔Hermes↔小白研究中心，取代原 `oc-hermes-bus`），放 OC 侧 GitHub，Hermes **只读 pull（read-only deploy key）**、小白只写自己 Output 车道。理由：Git 天生防冲突 + 全程审计；iCloud 会冲突副本→真相分裂，局域盘要两台都开机。详见 `Architecture/Trading_Research_Bus_Specification_v1.0.md`。
- **五目录：** inbox(oc_to_hermes/hermes_to_oc) / state(account/rule/strategy/route) / reports / logs / approvals。
- **五种单子 schema 锁死：** task/state/result/exception/approval（YAML 头+正文，见 Bus_Spec §5）。
- **六铁律：** 真相在文件不在聊天 / 绝不删单（改 status 或加 result）/ 各守车道 / 审批落 approvals 文件（不口头）/ 一 ID 一文件 append 历史 / schema 锁死坏单当异常。
- **Hermes 只读的后果（待实施时定）：** Hermes 推不了自己的单 → (a) 通过门铃通知 OC 代写（默认）或 (b) 后续给 Hermes 仅限自己车道的可写权限。
- **待实施时定：** 同步频率（事件驱动+兑底定时）、两台挂载路径。

---

## Key Technical Facts

### Execution Chain
```
Dragon (小塔) → HTTP → Signal Server (温总 port 5000) → signal.txt
→ DragonFileSignal Strategy → Sim101 → Replikanto → Apex follower
```

### Device Inventory
- 小塔 (Mac mini): 192.168.0.59 / 192.168.0.197, user: austinai
- 小白 (MacBook Air): 192.168.0.164, user: austinchien
- 小黑 (Mac mini, Hermes主机): 192.168.0.151, user: austinha, hostname Austins-Mini, WiFi MAC 1c:f6:4c:66:57:64, macOS 26.5.1 arm64。SSH免密(小塔公钥已装,2026-06-22)。装了git/python3,缺node/npm/claude/brew。
- 温总 (Win PC): 192.168.0.226, user: auste, MAC: C8:53:09:F1:1A:C3

### Signal Format
- `BUY|NQ|1` — market buy
- `SELL|NQ|1|SL=25310|TP=25270` — with stop/target
- `FLATTEN_ALL` — emergency close all
- `CLOSE|NQ|0` — close NQ position

### Critical Rules (must never forget)
- DragonFileSig **1 Minute = GREEN** to execute; white = dead
- DragonFileSig **5 Minute = WHITE always** (avoid double orders)
- Leader account = **Sim101** (always virtual/Sim, never real account)
- After Apex Reconnect: manually re-check strategy enabled

---

## Accounts

### Apex APEX-165583-123
- Product: **50k Tradovate EOD Trail** (NOT Intraday)
- Max DD: $2,000 | DLL: $1,000 | Max contracts: 6
- Min trading days: NONE | Consistency: NOT APPLIED
- Expiry: 2026-05-06 | Status: Active test account

### MFF MFFUEVRPD122274040
- Max DD: **$1,500** (3% of $50K) | No DLL
- Consistency: 50% of target ($1,500/day max)
- Min trading days: 5 | Status: SUSPENDED ($12.94 remaining)

---

## Compliance Framework

### Line 1 (hardcoded, applies to all platforms)
- Hard close: 16:09 ET FLATTEN_ALL
- Full trading days only
- Leader = Sim account always
- Instrument whitelist: NQ/MNQ, ES/MES, GC/MGC only
- Anti-hedging: same symbol = same direction ALL accounts
- No paired locking across accounts
- Mandatory stop loss on all orders

### Line 2 (per-account from contract)
- Profit target, Max DD, DLL, Min days, Consistency rule, Expiry, Platform restrictions

### New Platform SOP / 新账户 Onboard SOP (4月定，2026-06-16 完整确认)
1. 规则来源 = Tier 1 账户合同（不靠网站/记忆）
2. 用 Eval_Account_Risk_Form 张表单逐项填写
3. 跑 Line 1 兼容性检查
4. 全过 → onboard；有不过 → 讨论
5. 录进账户登记表（Trading/Account_Registry_v1.md）
- Any fail → discuss before accepting

### 凭据库（2026-06-16 建立，当日扩展到 5 平台）
- 路径：`~/.openclaw/secrets/credentials.json`
- 权限 600，物理隔离于 workspace 和 git
- 存储：MFF(austenmy) / TPT / Tradeify / TradeDay — 均活跃；Apex(austench) — 暂未活跃 + 需 2FA
- Apex 登录需两次认证，启用时需 President 配合输验证码

### 方法论铁律（总裁 2026-06-16 纠正，锁定）
- **最高目标优先原则：** 每件事先按最高目标（系统/自动完成）去尝试 → 真的做不到，才退而求其次找替代方案。不是一上来就退回「靠 President 手动」。
- **新问题先查系统：** 遇到问题，先查「这是不是新问题？以前解决过吗？有没有记录？」——不应跳过直接去建新方案，先找现有的东西。
- **凭据持久化是连续性的根：** 每次对话 Dragon 都是「裸的」、登录态一过期就断片，这是「一段一段」的技术根因。解决方案：凭据安全存库，持续可调用。

### CRITICAL Pending Research (⚠️ DO NOT TRADE LIVE UNTIL RESOLVED)
- Minimum hold time (anti-scalping) — each platform different
- Valid trading day definition — each platform different
- Gold vs NQ/ES cross-direction hedging — pending official source

---

## 平台架构：CQG vs Tradovate（总裁 2026-06-16 锁定）

- **CQG = 底层地基**（数据源 + 订单路由 + 风控 API）；**Tradovate = 上层前端经纪平台**（跑在 CQG 上）。
- **前端归属：TPT = CQG；MFF = Tradovate。**
- **数据同源** → 不管哪个前端，行情/策略层一致。
- **风控入口分平台（关键）：** 「每日盈利/亏损自动平仓」分两扇门设：**MFF→Tradovate Risk Settings；TPT→CQG / TPT 后台调 CQG**。不是“TPT 不能设风控”，是门不同。
- 全文：`Trading/System_Landing_Framework_v1.md` L88-99。

---

## Companies

- **APM LLC**: Operating entity (S-Corp). EIN: 81-4191044. Citibank Checking: 209251040
- **Meritpoint Logic LLC**: Trading entity. EIN: 35-2947076. IBKR approved, pending funding. Tradovate account pending open.
- All new Prop Firm accounts: register under APM LLC
- Purchase card: Visa ••8869 in Chrome autofill (小塔 work Chrome)

---

## NotebookLM

- Notebook: **MATS_v1_Compliance** (work Google account)
- 6 sources loaded, Apex + MFF both 10/10 accuracy
- Sources are Tier 1 only (from account contracts, not public websites)
- Next refresh due: 2026-04-15 ⚠️ 已过期，待 President 决定何时更新

---

## Persona & Communication (2026-06-15, locked)

**DEFAULT PERSONA = `SOUL.md` original text, verbatim.** The pre-upgrade original 龙哥 — warm, thorough, opinionated, gets things done, NOT verbose. (The earlier "3-gear / 给结论" experiment was REMOVED 2026-06-15: it claimed "conclusion-first" but executed as a rambling over-explainer — backfired. Do not reintroduce.)

**Non-negotiable rules (survive all resets):**
- Judgment DIRECTION must stay stable (verified 2026-06-14: stable across upgrade).
- Be concise. Lead with the answer, then only the depth that materially matters (risk / psychology). No filler, no over-explaining.
- Keep commercialization / risk warnings sharp.
- Terminology lock: use **Fractal**, never 分型. **OB = Order Block** (institutional order-block supply/demand zone), NOT "old block" (locked 2026-06-15). **激进 = progressive** entry setup (NOT "急进"/hasty). **交易机会 = trade opportunity** (NOT "交易智慧"/wisdom) (locked 2026-06-15). Reuse President's original terms verbatim; do not paraphrase his proper nouns.
- **Voice-input interpretation rule (locked 2026-06-15):** President dictates by voice; ASR errors are large (e.g. "order block" → "old block"). DO NOT ask President to pre-clean or edit his input. Dragon's job: interpret through our **domain context + professional terminology** (ICT, Order Block, FVG, R:R, MATS), auto-mapping obvious homophone/near-sound errors back to the correct term. Only ask a one-line clarification when a word would **materially change strategy meaning AND cannot be resolved from context** — never silently archive a wrong term and make President correct it afterward. The terminology lock is the correction dictionary; keep expanding it. **Full ICT/SMC glossary (the master correction dictionary) lives at `Trading/ICT_Glossary_v1.md`** — consult it when interpreting President's trading voice input (Order Block, FVG, BOS, CHoCH, OTE, liquidity sweep, PD Array, Kill Zones, etc.).
- Persona archive doc (`Persona/Dragon_Persona_Original_pre-0412.md`) is reference only; `SOUL.md` original is the authoritative live source (avoid 转述损耗).

---

## Strategy Execution Workflow (2026-06-15, locked)

**核心:推演与真实下单都必须按同一套策略走。**不是两套逻辑。
- 准备素材时:必须**非常严格**（四要素齐、FVG 用第一条边、R:R 达标）。素材不严格 → 后面无法执行。
- 执行策略时:有**实际情况的灵活性**（如第一目标位/第二目标位分批），但灵活是在策略框架内。
- **这就是风控:** 看到机会但不满足要求 → 不进场,损失的只是一个交易机会,不是钱。

**最佳工作流(locked):** 当 President 提出**推演**、或准备**下单**时 → Dragon 先 **review** 一遍（检查是否违反 FVG第一边/四要素不齐/R:R不够等）→ 讨论 → 确定 → 再执行。把把关放在执行前,不是事后补。

---

## Market Rehearsal Log (预演 vs 规则 防火墙, 2026-06-15, locked)

President established a dedicated doc: `Trading/Market_Rehearsal_Log_v1.md` to record his market **rehearsals (预演)** — his read of how price *may* unfold, derived from existing rules/strategy.

**One-way firewall (NON-NEGOTIABLE):**
- Rules/Strategy → Rehearsal: ✅ allowed.
- Rehearsal → Rules/Strategy: ❌ FORBIDDEN by default. Never let a rehearsal silently become a rule, or the system can never be fixed/stabilized.
- Exception: if a genuinely NEW rule is discovered during a rehearsal, it must be **manually migrated** to the proper Rules/Strategy doc as a separate explicit entry.
- It is **rehearsal (预演), NOT prediction** — prediction implies certainty, which doesn't fit our system.

**Long-term value:** rehearsal = calm/neutral mind; live order = emotional constraint. Logging both lets us compare **rehearsal win-rate vs live win-rate** → tells us whether to strengthen judgment (the read) or execution psychology (the trigger).

**Discipline maxim:** "日内交易并不是日日交易" (intraday ≠ daily trading). Monday + chop + no R:R = no trade; flat is correct.

First entry R001 (2026-06-15, NQ) archived with chart in `Trading/rehearsal_charts/`.

**多品种扩展（总裁 2026-06-18 锁定）：** 推演不能只做 NQ——只做一项太少。**每个交易日强制做全品种：① NQ ② 黄金 GC（可能要实际交易）③ ES 或直接 SPX（最终要操作的品种，ES 作辅助）。** 已设每日 cron 提醒（周一至周五 18:00 PT 盘后，第一小时已走完=最佳分析时段；jobId f56c3d21）。周级别推演 = President 自己记得，不设 cron（2026-06-19）。

---

## 素材库管理铁律（总裁 2026-06-17 锁定，最高优先级）

**核心：** 交易素材是 President **在状态中一眼抓住、一张一张手动抠下来的原始截图。这种工作人类做不出第二次**——错过那一瞬间就再也回不来。**原始性本身 = 真正的价值。**这是 President 现在“进入状态”的时机，把这些都收集进系统 = 头号优先事项。

**Dragon 的职责：即抓即存，瞬间稳妥归档，绝不加工、绝不丢失。**

**即存范围（2026-06-20 锁定）：** 交易相关（K线图/ICT素材/推演图/规则文档）即存当天 raw-archive。**宁可多存，不要漏存。** 排除：账户截图/资金证书/平台设置截图（有专项文件）、terminal log/debug、与交易策略完全无关的内容。

**补档方法：** 6/15 之前的空白，总裁 手工爬楼截图发我（能拿到原话+图+时间戳，效率远高于自动扫描）。

**四条铁律（不可违反）：**
1. 即抓即存、原样保管。
2. **绝不修改原始素材（图 + President 原话）。** 不 P 图、不裁剪、不重画。修改就没意义。需回测验证的东西回测里早有；这里收的是最原始的东西。
3. **不合并。** 只做归类（同类目下交叉索引），每条独立保留，绝不揉成一条。
4. **不断丰富、不断加强。** 同一形态实战中每见一根就原样收进对应类目 → 越来越丰富。

**两条线：** 规则（抽象逻辑，存素材库正文）vs 零件/实例（同一东西的不同变体 + 实战原始图例，存 `Trading/material_specimens/<类目>/`，每类一个 `_INDEX.md` 做归类索引）。

**已建类目（2026-06-20 更新）：**
- `material_specimens/pinbar_doji/`（长影线/十字星/Pin Bar 反转引线类）
- `material_specimens/candle_retracement/`（反直觉K线走势，1-10号柱体）
- `material_specimens/three_strike_reversal/`（三振反转/震荡区间ABC双路径）
- `material_specimens/one_setup_ict/`（ICT One Setup For Life：Inversion + Breaker + Time & Price参考卡）
- `material_specimens/ob_composite/`（OB综合结构，首批3张 specimen）

管理铁律全文在 `Trading/Trading_Material_Library_v1.md` 【素材库管理铁律】。

### 两层架构（总裁 2026-06-17 锁定）
- **① 奥斯汀素材原稿备份（Austin Raw Archive）= 底片，永不修改。** 路径 `Trading/austin_raw_archive/`（按日期子目录 + 每日 `_RAW.md`）。只存 President **原话逐字（对错不管）+ 原图原字节**，像截图一样一动不动。**不总结、不归类、不判断、不纠错。**
- **② 系统记录 = 加工层（Dragon 总结/归类/规则/索引）。** 在 `Market_Rehearsal_Log_v1.md` / `Trading_Material_Library_v1.md` / `material_specimens/`。
- **关系：** 原稿备份是底片，系统记录从底片提炼给系统用；系统记录出错 → 回底片还原真相。
- **标准动作（锁定）：** President 每丢一条 → Dragon **同时**做两件：先存原稿（①层）再做系统记录（②层）；原稿绝不因做系统记录而改动。
- **根本性质（总裁 2026-06-17 锁定，最高一条）：** 这里收的不是“样品”，是“当下真实走势”。外面的分析是先有结论再找样品印证（样品服务结论）；我们是**大盘自己走出了我们的想法**（先有真实走势，规则从真实里浮现）。一旦加工/挑选/事后修饰 → 退化成“找样品印证想法”，价值归零。原稿备份范围 = 行情推演 + 素材，凡与将来创造交易策略/规则有关的都收。

---

## Design Principles (President's core directives)

1. **Reduce human involvement** = reduce human error. System must self-verify.
2. **Human involvement only in safe windows**: pre-market checklist + post-market checklist. Not during live trading.
3. **Rules > Logic**: Prop firm rules override statistical logic. Stay compliant.
4. **Line 1 is the frame**: New platforms must fit inside it, not the other way around.
5. **Write it down**: Mental notes don't survive session restarts. Files do.
6. **Trading strategy** defines the boundary (entry/SL/TP/zone). **Order strategy** optimizes within it.
7. **Source = contract only**: All compliance data from Tier 1 (account contracts), not from websites or memory.

---

## 执行架构铁律（2026-06-17 锁定）

**Tradovate 网页风控设置** = 人工偶尔做，系统不做（浏览器UI不可靠，不是系统该走的路）。

**ATM 控制** = 在 DragonFileSignal Strategy 里扩展，加 NinjaTrader ATM API 调用（纯代码，止损/止盈/跟踪止损程序化）。这是下一个工程任务。

**实时风控保障** = FLATTEN_ALL 信号链（龙哥→HTTP→温总5000→signal.txt→DragonFileSignal→Sim101→Replikanto）已通，立即可用。

**浏览器操作纪律**：执行前先用知识定位目标，再操作，不盲点截图试错。总裁 发 "stop" = 完成当前这步立刻停，报告状态等指示。

---

## 交易时段纪律（⏸️ 暂未生效 — President 2026-06-22 暂停）

**⏸️ 当前状态（2026-06-22 锁定）：交易时段纪律暂不生效。** 真交易尚未开始；本纪律需等 President **正式确认"开始真交易"后**才启动。**现在 = 系统建设期**，不受盯盘时段限制，可正常进行较重任务（进尼家核 ATM、批量整理、大 subagent 等）随时可做。
**重启条件：** President 明确说"开始（真交易）"→ 立即恢复下面全部纪律。

---
（以下为纪律全文，待重启时生效）

全文：`Trading/Trading_Session_Discipline_v1.md`。触发背景：6/20 龙哥在大批量图片整理中多次崩盘，根因 = 单 Provider + 交易时段叠加繁重任务。教训直接关系真交易安全。

- **核心：交易时段 focus 在交易。系统资源 + 人的注意力都不分给繁重任务。龙哥和 President 同样适用。**
- **交易时段 = ET 06:00–16:15**（盘前+盘中+收盘风控窗）。
- **龙哥交易时段只做轻活：** 盯盘提醒/风控核对/信号中继/单张图快读/即时问答。
- **龙哥交易时段绝不碰繁重任务：** 批量看图/批量改名归档/大 subagent/素材库大整理/“大搞一场”。繁重任务一律排到盘后（ET 16:15 后）或周末。
- **执行：** 交易时段 President 提繁重任务 → 龙哥主动提醒“这是繁重任务，建议放盘后”，不闷头开工。
- **系统护栏（已实施 6/20）：** 双 Provider（Anthropic+OpenAI）+ thinking off。
- **下周起尝试真交易**（总裁 2026-06-20 定）：周日（6/21）整理出基本条件 + 分工方案 → 下周开始。分工方案（龙哥/Hermes/President 各做什么）待 6/21 整理。

---

## System Management Rules

- **内部记录自动对齐（总裁 2026-06-19 锁定）：** 任何决定一旦拍板，所有相关内部记录（MEMORY.md / active_work.md / HEARTBEAT.md / cron / 登记表等）**立即自动跟最新状态对齐，不再请示**。否则文件互相矛盾、系统不自洽。纯一致性维护 = 自动做；只有“该不该改决定本身”才问 总裁。
- **每周日系统检查（2026-06-16 定，锁定）:** 每个星期天做一次系统检查、修补、更新。内容：OpenClaw 服务状态、备份脚本、cron 任务、磁盘空间、软件更新、Open Issues 回顾、MEMORY.md 维护。

---

## System / Runtime Notes

- **2026-06-20: 双 Provider + thinking off（防断片根治）。** 根因：旧架构只有 Anthropic 一个 Provider，且 thinking 开着 → 反复报 `Invalid signature in thinking block`（replay_invalid，跟余额无关）+ 单点挂时无退路。修复：①加 **OpenAI 作 backup provider**（`openclaw onboard --openai-api-key`，plugin openai+codex enabled，API key 存 auth profile）；② **thinking 全局关 off**（`agents.defaults.thinkingDefault=off`）根治 signature 报错；③ 系统默认模型被 onboard 改成 **openai/gpt-5.5**（总裁 决定先不改回 Sonnet，观察）。「跟账扣款同时发生」= 巧合（用得多同时触发余额降+session变长），非因果；被拒请求不计费。
- **2026-06-20: 素材库整理一轮 + cron 修复。** ① 6/15+6/16 共27张 inbound_xxx 规范命名+补 _RAW.md（git mv 纯rename，底片字节零修改）；② 复盘抓到 6/20 目录 img_01/02 实为历史截图（总裁 决定不动）；③ 新建 **`material_specimens/ob_composite/`（OB 综合结构）** 类目（首批3张 specimen + _INDEX.md）。specimens 现 **5 个类目**：pinbar_doji / candle_retracement / three_strike_reversal / one_setup_ict / **ob_composite**。cron 修复：MFF风控提醒 `7 17 * * *`→`7 17 * * 1-5`（原每天触发 → 只工作日，周末不再乱叫）。

- **2026-06-15: System recovered; running on OpenClaw with Opus 4.8.** Post-upgrade judgment DIRECTION verified stable. Persona reset to SOUL.md original 龙哥 (see Persona & Communication).
- **2026-06-16: Default model switched to Sonnet 4.6 (cost optimization).** Opus retained as heavy-duty option. Model switch commands: 「上超跑」= switch to Opus; 「回 Sonnet」= switch back to Sonnet.
- **Model switching judgment rule (2026-06-16, locked):** Trading-related content (strategy review, rehearsal gating, compliance judgment) = quality first → proactively switch to Opus. Daily chat/files/status = stay on Sonnet. Uncertain scenario → ask President first. Dragon self-manages; 「上超跑」is for President to manually override when he wants Opus.

---

## Files to Know

Key workspace files:
- `Multi_Agent_Trading_System_v3.0.md` — master blueprint (finalized)
- `Agent_Prompts/compliance_framework_v1.md` — Line 1 / Line 2 framework
- `Agent_Prompts/gatekeeper_v1.md` — gatekeeper rules
- `Agent_Prompts/daily_checklist_v1.md` — pre/post market checklist
- `Agent_Prompts/ninja_startup_sop_v1.md` — NinjaTrader startup SOP
- `NotebookLM_Sources/Account_Sourced_Compliance_Pack_v2.md` — compliance pack
- `Trading/execution_run_log_v1.md` — execution log
- `Dragon_ToDo_v1.md` — task list

---

## Infrastructure Status (as of 2026-04-12) — STABLE

Basic infrastructure is now considered stable. Team focus shifts to strategy and execution.

| Layer | Component | Status |
|---|---|---|
| Execution | Dragon (OpenClaw) on 小塔 | ✅ Active |
| Research | Jimmy (Gemini CLI) on 小白 | ✅ Deployed |
| Compliance | NotebookLM 书记宝 (7 sources) | ✅ Active |
| Local backup | Samsung DragonVault 三星 | ⚠️ Last: 2026-06-14（DragonVault 挂载但备份脚本未见定时运行记录；git_sync 正常每15min推GitHub）|
| Cloud backup | GitHub + iCloud | ✅ Active |
| President workspace | Obsidian vault (iCloud/President_Command) | ✅ Setup done |

### Jimmy CLI Setup (2026-04-12)
- Gemini CLI installed on 小白 (Austin's MacAir)
- GEMINI.md written to President_Command — auto-loaded on CLI start
- Active Scanning Mandate enabled (proactively finds blind spots)
- Memory Protocol enabled (reads files on start, writes session log on end)
- Working language: Chinese with President, English for code/files
- Details to be finalized by President when ready — not yet execution standard

---

*Updated: 2026-04-12 | Dragon*
