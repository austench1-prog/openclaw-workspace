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

### New Platform SOP
- Run Line 1 compatibility check first
- All pass → onboard
- Any fail → discuss before accepting

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

## Design Principles (Chairman's core directives)

1. **Reduce human involvement** = reduce human error. System must self-verify.
2. **Human involvement only in safe windows**: pre-market checklist + post-market checklist. Not during live trading.
3. **Rules > Logic**: Prop firm rules override statistical logic. Stay compliant.
4. **Line 1 is the frame**: New platforms must fit inside it, not the other way around.
5. **Write it down**: Mental notes don't survive session restarts. Files do.
6. **Trading strategy** defines the boundary (entry/SL/TP/zone). **Order strategy** optimizes within it.
7. **Source = contract only**: All compliance data from Tier 1 (account contracts), not from websites or memory.

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
