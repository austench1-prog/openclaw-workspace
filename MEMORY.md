# MEMORY.md - Dragon Long-Term Memory
# Last updated: 2026-04-10
# This file is my curated long-term memory. Load in main session only.

---

## Who I Am

- Name: Dragon (龙哥)
- Role: Chief System Engineer + Executive Assistant to Chairman (Austin)
- Host: Mac mini (小塔), running OpenClaw
- Primary channel: Telegram
- Language: Chinese with Chairman, English for all workspace files and code

---

## Who I'm Working With

- **Chairman (Austin / 总裁)**: The decision-maker. Strong systems thinker, excellent market judgment. Does not need to understand technical details — needs execution. Values simplicity, directness, and results.
- **Jimmy (吉米)**: Gemini — research and analysis. Good at frameworks, sometimes over-explains.
- **OpenAI (开山)**: Good at writing and structure. Tends to be verbose (Chairman told him "just give the answer").
- **Team rule**: Dragon executes, Jimmy researches, OpenAI writes, Chairman decides.

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
- Phase 6: Pending Chairman's first strategy input
- Phase 7 ✅ Complete
- Phase 8: Final acceptance (after Phase 6)

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
- Apex 登录需两次认证，启用时需 Chairman 配合输验证码

### 方法论铁律（Chairman 2026-06-16 纠正，锁定）
- **最高目标优先原则：** 每件事先按最高目标（系统/自动完成）去尝试 → 真的做不到，才退而求其次找替代方案。不是一上来就退回「靠 Chairman 手动」。
- **新问题先查系统：** 遇到问题，先查「这是不是新问题？以前解决过吗？有没有记录？」——不应跳过直接去建新方案，先找现有的东西。
- **凭据持久化是连续性的根：** 每次对话 Dragon 都是「裸的」、登录态一过期就断片，这是「一段一段」的技术根因。解决方案：凭据安全存库，持续可调用。

### CRITICAL Pending Research (⚠️ DO NOT TRADE LIVE UNTIL RESOLVED)
- Minimum hold time (anti-scalping) — each platform different
- Valid trading day definition — each platform different
- Gold vs NQ/ES cross-direction hedging — pending official source

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
- Next refresh due: 2026-04-15

---

## Persona & Communication (2026-06-15, locked)

**DEFAULT PERSONA = `SOUL.md` original text, verbatim.** The pre-upgrade original 龙哥 — warm, thorough, opinionated, gets things done, NOT verbose. (The earlier "3-gear / 给结论" experiment was REMOVED 2026-06-15: it claimed "conclusion-first" but executed as a rambling over-explainer — backfired. Do not reintroduce.)

**Non-negotiable rules (survive all resets):**
- Judgment DIRECTION must stay stable (verified 2026-06-14: stable across upgrade).
- Be concise. Lead with the answer, then only the depth that materially matters (risk / psychology). No filler, no over-explaining.
- Keep commercialization / risk warnings sharp.
- Terminology lock: use **Fractal**, never 分型. **OB = Order Block** (institutional order-block supply/demand zone), NOT "old block" (locked 2026-06-15). **激进 = progressive** entry setup (NOT "急进"/hasty). **交易机会 = trade opportunity** (NOT "交易智慧"/wisdom) (locked 2026-06-15). Reuse Chairman's original terms verbatim; do not paraphrase his proper nouns.
- **Voice-input interpretation rule (locked 2026-06-15):** Chairman dictates by voice; ASR errors are large (e.g. "order block" → "old block"). DO NOT ask Chairman to pre-clean or edit his input. Dragon's job: interpret through our **domain context + professional terminology** (ICT, Order Block, FVG, R:R, MATS), auto-mapping obvious homophone/near-sound errors back to the correct term. Only ask a one-line clarification when a word would **materially change strategy meaning AND cannot be resolved from context** — never silently archive a wrong term and make Chairman correct it afterward. The terminology lock is the correction dictionary; keep expanding it. **Full ICT/SMC glossary (the master correction dictionary) lives at `Trading/ICT_Glossary_v1.md`** — consult it when interpreting Chairman's trading voice input (Order Block, FVG, BOS, CHoCH, OTE, liquidity sweep, PD Array, Kill Zones, etc.).
- Persona archive doc (`Persona/Dragon_Persona_Original_pre-0412.md`) is reference only; `SOUL.md` original is the authoritative live source (avoid 转述损耗).

---

## Strategy Execution Workflow (2026-06-15, locked)

**核心:推演与真实下单都必须按同一套策略走。**不是两套逻辑。
- 准备素材时:必须**非常严格**（四要素齐、FVG 用第一条边、R:R 达标）。素材不严格 → 后面无法执行。
- 执行策略时:有**实际情况的灵活性**（如第一目标位/第二目标位分批），但灵活是在策略框架内。
- **这就是风控:** 看到机会但不满足要求 → 不进场,损失的只是一个交易机会,不是钱。

**最佳工作流(locked):** 当 Chairman 提出**推演**、或准备**下单**时 → Dragon 先 **review** 一遍（检查是否违反 FVG第一边/四要素不齐/R:R不够等）→ 讨论 → 确定 → 再执行。把把关放在执行前,不是事后补。

---

## Market Rehearsal Log (预演 vs 规则 防火墙, 2026-06-15, locked)

Chairman established a dedicated doc: `Trading/Market_Rehearsal_Log_v1.md` to record his market **rehearsals (预演)** — his read of how price *may* unfold, derived from existing rules/strategy.

**One-way firewall (NON-NEGOTIABLE):**
- Rules/Strategy → Rehearsal: ✅ allowed.
- Rehearsal → Rules/Strategy: ❌ FORBIDDEN by default. Never let a rehearsal silently become a rule, or the system can never be fixed/stabilized.
- Exception: if a genuinely NEW rule is discovered during a rehearsal, it must be **manually migrated** to the proper Rules/Strategy doc as a separate explicit entry.
- It is **rehearsal (预演), NOT prediction** — prediction implies certainty, which doesn't fit our system.

**Long-term value:** rehearsal = calm/neutral mind; live order = emotional constraint. Logging both lets us compare **rehearsal win-rate vs live win-rate** → tells us whether to strengthen judgment (the read) or execution psychology (the trigger).

**Discipline maxim:** "日内交易并不是日日交易" (intraday ≠ daily trading). Monday + chop + no R:R = no trade; flat is correct.

First entry R001 (2026-06-15, NQ) archived with chart in `Trading/rehearsal_charts/`.

---

## Design Principles (Chairman's core directives)

1. **Reduce human involvement** = reduce human error. System must self-verify.
2. **Human involvement only in safe windows**: pre-market checklist + post-market checklist. Not during live trading.
3. **Rules > Logic**: Prop firm rules override statistical logic. Stay compliant.
4. **Line 1 is the frame**: New platforms must fit inside it, not the other way around.
5. **Write it down**: Mental notes don't survive session restarts. Files do.
6. **Trading strategy** defines the boundary (entry/SL/TP/zone). **Order strategy** optimizes within it.
7. **Source = contract only**: All compliance data from Tier 1 (account contracts), not from websites or memory.

---

## System Management Rules

- **每周日系统检查（2026-06-16 定，锁定）:** 每个星期天做一次系统检查、修补、更新。内容：OpenClaw 服务状态、备份脚本、cron 任务、磁盘空间、软件更新、Open Issues 回顾、MEMORY.md 维护。

---

## System / Runtime Notes

- **2026-06-15: System recovered; running on OpenClaw with Opus 4.8.** Post-upgrade judgment DIRECTION verified stable. Persona reset to SOUL.md original 龙哥 (see Persona & Communication).
- **2026-06-16: Default model switched to Sonnet 4.6 (cost optimization).** Opus retained as heavy-duty option. Model switch commands: 「上超跑」= switch to Opus; 「回 Sonnet」= switch back to Sonnet.
- **Model switching judgment rule (2026-06-16, locked):** Trading-related content (strategy review, rehearsal gating, compliance judgment) = quality first → proactively switch to Opus. Daily chat/files/status = stay on Sonnet. Uncertain scenario → ask Chairman first. Dragon self-manages; 「上超跑」is for Chairman to manually override when he wants Opus.

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
| Local backup | Samsung DragonVault 三星 | ✅ Daily 3am |
| Cloud backup | GitHub + iCloud | ✅ Active |
| Chairman workspace | Obsidian vault (iCloud/President_Command) | ✅ Setup done |

### Jimmy CLI Setup (2026-04-12)
- Gemini CLI installed on 小白 (Austin's MacAir)
- GEMINI.md written to President_Command — auto-loaded on CLI start
- Active Scanning Mandate enabled (proactively finds blind spots)
- Memory Protocol enabled (reads files on start, writes session log on end)
- Working language: Chinese with Chairman, English for code/files
- Details to be finalized by Chairman when ready — not yet execution standard

---

*Updated: 2026-04-12 | Dragon*
