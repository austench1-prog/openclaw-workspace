# OC + Hermes System Judgment v1
# Date: 2026-06-19 | Status: DISCUSSION DRAFT (architecture only, no implementation)
# Author: Dragon (OC) | Model: Opus 4.8
# Scope: Architecture discussion, design evaluation, responsibility assignment ONLY.
#        No changes to any existing system, config, file, permission, or automation.
#        Any change requires explicit Chairman approval before implementation.

---

## 0. Purpose

This document answers THREE core questions, on top of the existing **Multi-Agent Trading System v3.0** (do not break its main chain), the newly installed **Hermes Agent**, the existing **OpenClaw (OC)** platform, two same-spec Mac minis, the 温总 execution front line, and the future TradingView / AMP cloud channel:

1. **The 7 pain points — who solves each, and how?**
2. **OC vs Hermes — boundary, interface, information flow?**
3. **How to upgrade to the next-stage architecture WITHOUT breaking the v3.0 main execution chain?**

Maturity labels used throughout:
- **MATURE** — pattern clear, tech path proven, can design & build directly.
- **CONDITIONAL** — main tech feasible, but depends on platform / broker / account permission / website flow / data provider.
- **NEW MODULE** — still missing a defined module, spec, interface, or validation flow.
- **OUT OF SCOPE** — not part of what these 7 pain points are meant to solve.

---

## 1. Anchor: What v3.0 Already Gives Us (do not rebuild)

v3.0 is the execution backbone. It is **finalized and operational** as of 2026-06-14:

| v3.0 Asset | State | Verdict for next stage |
|---|---|---|
| Layer A: Prop Intelligence + NotebookLM compliance | ✅ Active (6 sources) | KEEP. Hermes feeds it, does not replace it. |
| Layer B: Trading Strategy + Order Strategy | 🔄 Materials in, assembling | KEEP. This is the human/OC strategy core. |
| Layer C: Gatekeeper + Execution Engine | ✅ Paper 10/10, exec fixed | KEEP. This is the risk gate — never bypass. |
| Layer D: Tradovate→NinjaTrader + IBKR dual path | ✅ Defined | KEEP. Main execution endpoints. |
| Layer E: 温总 Win PC + Signal Server + Replikanto + DragonFileSignal | ✅ Operational | KEEP. The live execution front line. |
| Layer F: Dragon-A / Dragon-B ops roles | ✅ Done | EVOLVE → some Layer F work shifts to Hermes (see §3). |

**Core finding:** v3.0 solves **execution** very well (Layers C/D/E). What v3.0 does NOT fully solve is the **front edge** (visual input, live quotes, proactive alerts, daily-plan parameterization) and the **back office** (platform website work, compliance-library refresh, account/admin ops). The 7 pain points cluster almost entirely in those two gaps — which is exactly where Hermes belongs.

---

## 2. The 7 Pain Points — Owner + Method + Maturity

| # | Pain | Primary Owner | Method (short) | Maturity |
|---|---|---|---|---|
| 1 | No vision (AI can't see screen) | **Chairman → OC** | Evidence-input layer (Module A): screenshots/snapshots/structured data in | **MATURE** |
| 2 | No live quotes | **Data source → OC** | Market-data layer (Module B): API / TV alert / data feed; never the chat model | **CONDITIONAL** (needs a chosen data source) |
| 3 | No proactive alerts | **OC (relay) + Hermes (admin alerts)** | Event relay (Module C): TV alert → server relay → Telegram | **CONDITIONAL** (we have a partial chain; see §2.3) |
| 4 | Daily-plan code slavery (rewrite Pine daily) | **OC + Chairman** | Strategy parameterization (Module D): fixed logic + daily param pack | **NEW MODULE** |
| 5 | Multi-strategy / single-window conflict | **OC** | Multi-strategy router (Module E): pick E1/E2/E3 | **NEW MODULE** (design choice pending) |
| 6 | New Mac mini deployment method | **DONE** | Already migrated 6/18, verified | **OUT OF SCOPE** (resolved) |
| 7 | Platform website work + rule scrape + compliance refresh | **Hermes** | Website-work layer (Module F): F1 public / F2 logged-in / F3 allowlisted | **CONDITIONAL** (per-site validation) |

### 2.1 Pain 1 — No Vision  → MATURE
- **Method:** Module A (evidence input). Chairman provides screenshot / chart snapshot / page / CSV/JSON; the agent reasons on evidence, never guesses the screen.
- **Today:** Already works through OC (image tool reads screenshots Chairman sends).
- **Owner:** Chairman supplies evidence → OC interprets. Hermes can also receive evidence for back-office tasks.
- **Gap:** none technical. It is a *discipline* item: when no evidence is given, the agent must say "no evidence" rather than invent a chart.

### 2.2 Pain 2 — No Live Quotes  → CONDITIONAL
- **Method:** Module B. A trusted source must feed price: a market-data API, a TradingView alert payload, or a broker/data-provider feed. The conversational model is **never** the quote source.
- **Blocker = which source:** This is the one real decision. Options:
  - TradingView alert payloads (cheapest, event-driven, you already use TV).
  - A data API (e.g. broker feed / 3rd-party) for continuous monitoring.
  - 温总 NinjaTrader feed (already in-house, real-time, futures).
- **Recommendation:** Start with **TradingView alerts** for event triggers + **NinjaTrader in-house feed** for live futures price, before paying for any new data API. → makes Pain 2 effectively MATURE using existing assets.
- **Owner:** OC (it owns the execution/data front line via 温总).

### 2.3 Pain 3 — No Proactive Alerts  → CONDITIONAL (partially solved already)
- **We already have HALF of Module C.** The live chain `Dragon → HTTP → 温总:5000 → signal.txt → DragonFileSignal → Sim101 → Replikanto` is a working server-side relay (for execution). For *alerts*, the same relay pattern applies, output = Telegram instead of order.
- **Standard structure (target):**
  `TradingView Strategy/Alert → Server-Side Alert → Secure Relay → Validation/Logging/Dedup → Telegram`
- **Relay must have:** private auth / request validation, Event ID, dedup, replay protection, logging, failure alert, **notify-only by default** (no auto-order).
- **Future execution bridge** (separate, explicitly approved later):
  `TV Strategy → Server-Side Alert → Secure Relay → Notification / Paper / Future Approved Execution Bridge`
  — This bridge is NOT Pine itself and NOT Telegram itself; it is a distinct, separately-approved component.
- **Owner split:**
  - **OC** owns the *trade-event* relay (price/level alerts tied to strategy) — it already runs the execution relay.
  - **Hermes** owns *admin/ops* alerts (rule change, payout deadline, account expiry, report due).
- **Gap = NEW MODULE for the notification relay** (auth/dedup/replay layer). The transport exists; the hardened relay does not yet.

### 2.4 Pain 4 — Daily-Plan Code Slavery  → NEW MODULE
- **Core principle:** **Do NOT rewrite strategy logic daily. Only update parameters + the daily plan.**
- **Two layers:**
  - **Fixed layer** (rarely changes): entry/exit conditions, risk model, time/session filter, position logic, alert format.
  - **Daily param layer** (changes daily/weekly): key levels, direction bias, stop distance, target zone, no-trade zone, session enable/disable, risk cap, strategy state.
- **AI role:** read Chairman's morning plan → produce a **structured candidate param pack** → show the diff → after Chairman approves, write to the designated config location → version + date it.
- **Owner:** **OC** generates & versions the param pack; **Chairman** approves; (Pine input written/pasted per approved flow).
- **Why NEW:** we have no param-pack schema, no diff-view, no versioned param store yet. This is the single highest-leverage new module — it directly kills the "rewrite Pine every day" pain.

### 2.5 Pain 5 — Multi-Strategy / Single-Window Conflict  → NEW MODULE (choose architecture)
Three viable architectures:
- **E1. Single master orchestrator** — merge HTF swing + LTF intraday into one master signal. Best when rules are tightly coupled, want unified risk cap, single execution exit.
- **E2. Multi-signal + unified relay** — each strategy emits its own signal; all enter one relay; relay routes by `strategy_id / timeframe / instrument / session / priority / risk_state` → Telegram / log / ignore / paper / future approved bridge.
- **E3. Multi-chart / multi-alert + central registry** — each timeframe/strategy on its own chart+alert, a central registry tracks state. Best when strategies are independent, you want separate test cycles, alert-only first.
- **Recommendation:** **E2** is the best fit for us — it reuses the relay we're already building for Pain 3, gives clean per-strategy routing, keeps a single risk gate, and upgrades smoothly to a future execution bridge. E3 is the safe "alert-only first" stepping stone; E1 risks over-coupling.
- **Owner:** OC (execution routing is OC's domain).

### 2.6 Pain 6 — New Mac mini Deployment  → RESOLVED / OUT OF SCOPE
- Already migrated 6/18 (Migration Assistant), verified 6/19. 温总 PC signal chain + DragonVault 3am backup confirmed done. Disk clean (370G free). **Nothing pending.**
- Forward note: the *second* Mac mini = the natural **Hermes host** (separate user `austinha`, separate Telegram bot, separate `.hermes`). This gives physical OC/Hermes isolation — good for governance.

### 2.7 Pain 7 — Platform Website Work + Compliance Refresh  → CONDITIONAL (Hermes-owned)
- **This is Hermes's flagship job.** Three modes:
  - **F1 Public-page monitor** (MATURE-ish): periodically read FAQ / rulebook / payout policy / promo / discount / status / maintenance / public announcements → output new content, rule diffs, discount + validity, payout changes, items needing Chairman confirmation.
  - **F2 Logged-in info work** (CONDITIONAL per site): login → read account announcements / in-site rule updates / download rule docs / read authorized account-page info / read notices → organize → update compliance library → generate diff summary. Feasibility depends per site on: login flow, 2FA, CAPTCHA, dynamic pages, session expiration, ToS, automation allowance, re-maintenance after site redesign.
  - **F3 Allowlisted in-site work** (CONDITIONAL, per-platform Action Allowlist): only within an explicit allowlist — download docs, organize announcements, read/update explicitly-allowed site preferences, draft support messages, write confirmed data into compliance library.
- **Hard exclusions (from Chairman):** NO cash-account passwords; NO live-trading API keys. Anything beyond "platform info / in-site rules / docs / notices / compliance-library update" is NOT pre-assumed — defined later per-platform via Action Allowlist.
- **Owner:** **Hermes** end-to-end; output feeds OC's Layer A compliance library + NotebookLM; Chairman approves diffs.
- **Per-platform validation required (the "CONDITIONAL"):** each of Apex / TradeDay / TPT / Tradeify / MFF needs its own F2/F3 viability check (does automation survive login/2FA/CAPTCHA/ToS).

---

## 3. OC vs Hermes — Boundary, Interface, Information Flow

### 3.1 Responsibility boundary

| Domain | OC (龙哥) | Hermes | Human (Chairman) |
|---|---|---|---|
| Execution control / trade main chain | **OWNS** | — | — |
| Front-line routing (温总, signal server, Replikanto) | **OWNS** | — | — |
| Risk gating (Gatekeeper, FLATTEN_ALL) | **OWNS** | — | final risk authority |
| Strategy param pack (Module D) | **OWNS** (generate/version) | — | **approves** |
| Trade-event alerts (price/level) | **OWNS** | — | — |
| Website work / rule scrape (Module F) | — | **OWNS** | authorizes scope |
| Compliance library refresh | feeds Layer A | **OWNS** the gathering | **approves** diffs |
| Account / admin / payout / expiry tracking | — | **OWNS** | decides |
| Reminders / reports / logging / closure | shared | **OWNS** back-office | — |
| Final strategy judgment | proposes/gates | supports only | **OWNS** |
| Key authorizations / rule approval | — | — | **OWNS** |

**One-line split:**
- **OC = the hands & the gate** (execution, routing, risk, trade alerts, param packs).
- **Hermes = the back office & the field researcher** (website work, rules, compliance lib, accounts, admin, reminders, reports).
- **Chairman = the brain** (strategy direction, authorizations, approvals).
- **TradingView / AMP = a separate research / alert / test / future-cloud-execution channel** — NOT a replacement for the NinjaTrader / IBKR main chain.

### 3.2 Interface = structured artifacts, NOT chat

The two agents must exchange **structured records**, not loose conversation. Defined artifact types:

| Artifact | Direction | Purpose |
|---|---|---|
| **Task Ticket** | OC ↔ Hermes | a unit of delegated work, with owner/state/deadline |
| **Status Table** | both → shared | current state of accounts, platforms, tasks |
| **Rule Update / Diff Summary** | Hermes → OC/Chairman | compliance changes found on platforms |
| **Strategy Param Pack** | OC → Chairman → store | daily/weekly parameters (Module D output) |
| **Report** | Hermes → Chairman | daily/weekly back-office summary |
| **Approval Result** | Chairman → both | go/no-go on diffs, packs, authorizations |

### 3.3 Information-flow mechanism (the open design question)

Two same-spec Mac minis, two separate users, two Telegram bots. How do OC and Hermes actually exchange the artifacts above? Candidate mechanisms (to decide later, NOT now):
- **Shared file area** (a synced/agreed directory of JSON/MD artifacts) — simplest, auditable, matches "write it down" doctrine.
- **A small structured store** (e.g. a shared registry file or lightweight DB) — better for status tables/dedup.
- **Direct agent-to-agent message** (one bot → other's endpoint) — fastest, but needs auth + logging.
- **Chairman-mediated** (Chairman forwards approvals) — safest for high-stakes, slowest.
- **Recommendation:** start with **shared structured files (JSON/MD artifacts) + Chairman-mediated approvals**; add direct agent-to-agent messaging only after the artifact schema is stable. This keeps everything auditable and avoids "verbal/scattered chat" coupling.

---

## 4. Upgrade Path — Without Breaking v3.0

**Principle: additive, not destructive.** v3.0 main chain (Layers C/D/E) is untouched. New modules attach at the front edge and back office.

**Stage 1 — Foundations (no risk to main chain):**
1. Define the **artifact schema** (task ticket, status table, diff summary, param pack) — pure spec, no code change.
2. Decide **Module B data source** (recommend: TV alerts + 温总 NinjaTrader feed first).
3. Decide **Module E architecture** (recommend: E2, alert-only first via E3 as stepping stone).

**Stage 2 — Highest-leverage new module:**
4. Build **Module D (strategy param pack)** — fixed layer vs daily param layer, diff-view, versioned store. Kills Pain 4. Alert-only / paper, never touches live orders yet.

**Stage 3 — Alerting & relay:**
5. Build the **hardened notification relay** (Module C: auth/dedup/replay/logging, notify-only). Solves Pain 3 cleanly. Reuses the 温总 relay pattern.

**Stage 4 — Hermes back office:**
6. Stand up **Module F** per platform: F1 public monitor first (lowest risk), then F2/F3 per-site validation under Action Allowlist. Feeds compliance library. Solves Pain 7.

**Stage 5 — Governance:**
7. Lock the **OC/Hermes information-flow mechanism** (shared files + Chairman approval), then optionally add agent-to-agent messaging.

**Never in this scope (require separate explicit approval):** any execution bridge from TV/relay to live orders; any cash-account password handling; any live-trading API key handling.

---

## 5. Summary Verdict

- **v3.0 stays the backbone.** It solves execution; we do not rebuild it.
- **The 7 pains are front-edge + back-office gaps**, mapped cleanly to 6 modules.
- **OC = hands/gate, Hermes = back office/research, Chairman = brain, TV/AMP = side channel.**
- **Most leverage = Module D (param pack)** — it ends the daily Pine rewrite.
- **Already partly solved:** Pain 3 (relay pattern exists), Pain 6 (deployment done), Pain 1 (vision via evidence input).
- **Biggest unknowns = per-site F2/F3 viability (Pain 7)** and **the agent-to-agent info-flow mechanism (§3.3)** — both need validation, not assumption.

---

## 6. Open Decisions for Chairman (nothing built until these are chosen)

1. **Module B data source:** TV alerts + 温总 feed first? Or pay for a data API now?
2. **Module E architecture:** E2 (multi-signal + unified relay) as target, E3 alert-only first? Confirm.
3. **OC↔Hermes mechanism:** shared structured files + Chairman approval first? Confirm.
4. **Build order:** confirm Stage 1→5 sequence, or re-prioritize.
5. **Pain 7 first target platform:** which Prop Firm site does Hermes validate first (Apex? TradeDay?)?

---

*DISCUSSION DRAFT v1 | 2026-06-19 | No implementation until Chairman approves. All changes to v3.0 require Chairman approval.*
