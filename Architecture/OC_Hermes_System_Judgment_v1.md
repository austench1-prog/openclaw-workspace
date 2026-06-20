# OC + Hermes System Judgment v1 (LEAN BACKBONE)
# Date: 2026-06-19 | Status: DISCUSSION DRAFT (architecture only, no implementation)
# Author: Dragon (OC) | Model: Opus 4.8
# Scope: Architecture discussion + responsibility design ONLY.
#        No change to any existing system/config/file/permission/automation.
#        Any change requires explicit Chairman approval before implementation.

---

## 0. Guiding Filter (Chairman, 2026-06-19, LOCKED)

> **Effort-to-value is the first filter.** If a feature gives ~1 unit of efficiency
> but costs ~5 units of work to build/maintain → REMOVE it.
> The system does NOT need to solve every problem. Keep pursuing the system's
> possibilities, but do NOT keep making trouble for it.
> **Aim for the cleanest, simplest version.**

This filter governs the whole design. Anything system-to-system that is very hard,
or only marginally useful, is dropped now and revisited only if a real pain forces it.

---

## 1. The Lean Backbone — only 4 things worth doing

| # | Keep | Why | Cost |
|---|---|---|---|
| 1 | **Module A — Visual / Evidence Input** | Chairman sends screenshot/chart/data; AI reasons on evidence, never guesses the screen | **ZERO** (already works) |
| 2 | **Module D — Strategy Param Pack** | ⭐ Highest leverage. Update parameters daily, NOT rewrite Pine every day | Medium |
| 3 | **Module C — Notification Relay** | Price hits a level → system pings Chairman automatically | Medium (half the chain exists) |
| 4 | **Hermes — F1 public-page monitor + back office / compliance / admin** | Watch platform public pages for rule/payout/promo changes; run reminders/reports/logging | Medium |

**Roles:** OC = hands + gate. Hermes = back office + field research. Chairman = brain.

---

## 2. DROPPED / PARKED (revisit only if a real pain forces it)

| Dropped item | Reason |
|---|---|
| **AMP Futures auto-execution** | Which platform trades AMP is undecided; NinjaTrader path very hard; auto-order has NO complete solution. System-to-system too hard → fully excluded for now. |
| **Module B — buy a paid data API** | High cost + maintenance for low marginal value. Use existing assets only (TV alerts + 温总 NinjaTrader feed) if/when needed. |
| **Module F2/F3 — logged-in / in-site automation** | Per-site 2FA / CAPTCHA / redesigns = bottomless maintenance. Only F1 public-page monitoring for now. |
| **Module E — multi-strategy router (E1/E2/E3)** | Over-engineering unless many strategies run at once. Parked until strategy count actually demands it. |
| **Execution Bridge (TV/relay → live order)** | High risk; out of scope by design. Requires separate explicit approval. |

**Hard exclusions (permanent unless separately approved):** live execution bridge; cash-account passwords; live-trading API keys.

---

## 3. The 4 Modules in Plain Terms

### Module A — Visual / Evidence Input  (KEEP, zero cost)
- AI cannot see the screen. Chairman provides screenshot / chart snapshot / page / CSV / JSON / text → AI judges on that evidence.
- Already works today (OC reads images sent in Telegram).
- **The only real rule (discipline):** when no evidence is given, the AI must say "I don't see it, send the chart" — never invent a price/level.

### Module D — Strategy Param Pack  (KEEP, ⭐ highest leverage)
- **Principle: do NOT rewrite strategy logic daily. Only update parameters + the daily plan.**
- **Fixed layer (rarely changes):** entry/exit conditions, risk model, time/session filter, position logic, alert format.
- **Daily param layer (changes daily/weekly):** key levels, direction bias, stop distance, target zone, no-trade zone, session on/off, risk cap, strategy state.
- **Flow:** Chairman gives morning plan → OC produces a structured candidate param pack → shows the diff → Chairman approves → write to the designated config location → version + date it.
- **Owner:** OC generates & versions; Chairman approves. This directly kills the "rewrite Pine every day" pain.

### Module C — Notification Relay  (KEEP, half exists)
- **Target:** `TradingView Strategy/Alert → Server-Side Alert → Secure Relay → Validation/Logging/Dedup → Telegram`.
- We already run a working relay pattern for execution (`Dragon → HTTP → 温总:5000 → signal.txt → DragonFileSignal → Sim101`). The notification relay reuses the same idea, output = Telegram, **notify-only, no auto-order**.
- Relay needs: private auth / request validation, Event ID, dedup, replay protection, logging, failure alert.
- **Owner:** OC owns trade-event alerts; Hermes owns admin/ops alerts (rule change, payout deadline, account expiry, report due).

### Hermes — F1 Public-Page Monitor + Back Office  (KEEP)
- **F1 only:** periodically read official PUBLIC pages — FAQ, rulebook, payout policy, promo/discount, platform status, maintenance, public announcements → output: new content, rule diffs, discount + validity, payout changes, items needing Chairman confirmation.
- **Back office:** reminders, reports, logging, closure, account/admin/payout/expiry tracking.
- **Compliance library:** Hermes does NOT build a new library. It feeds the EXISTING v3.0 Layer A (NotebookLM + Account-Sourced Compliance Pack) by producing diff summaries; Chairman approves before anything updates the library.
- **Owner:** Hermes end-to-end; Chairman approves diffs.

---

## 4. OC vs Hermes — Boundary & Information Flow

### 4.1 Boundary

| Domain | OC | Hermes | Chairman |
|---|---|---|---|
| Execution / trade main chain / front-line routing | **OWNS** | — | — |
| Risk gating (Gatekeeper, FLATTEN_ALL) | **OWNS** | — | final authority |
| Strategy param pack (Module D) | **OWNS** (make/version) | — | **approves** |
| Trade-event alerts (price/level) | **OWNS** | — | — |
| Public-page monitoring + rule diffs (F1) | — | **OWNS** | authorizes scope |
| Compliance library refresh | consumes (Layer A) | **OWNS** gathering | **approves** diffs |
| Account / admin / payout / expiry | — | **OWNS** | decides |
| Reminders / reports / logging | shared | **OWNS** back office | — |
| Final strategy judgment & key authorizations | proposes/gates | supports only | **OWNS** |

### 4.2 Information flow = structured artifacts, not chat
- Exchange via **structured records**: task ticket, status table, rule/diff summary, strategy param pack, report, approval result.
- **Mechanism (start simple):** shared structured files (JSON/MD) + Chairman-mediated approval. Add direct agent-to-agent messaging only after the artifact schema is stable. Keeps everything auditable, avoids verbal/scattered coupling.

---

## 5. Upgrade Path — additive, never breaks v3.0

v3.0 main chain (Layers C/D/E execution) is untouched. New work attaches at the front edge + back office.

1. **Stage 1 — Module D (param pack).** Highest leverage. Alert-only / paper, never touches live orders. Kills the daily-Pine-rewrite pain.
2. **Stage 2 — Module C (notification relay).** Hardened notify-only relay (auth/dedup/replay/logging). Price-hits-level → Telegram.
3. **Stage 3 — Hermes F1 + back office.** Public-page monitoring + reminders/reports; feed compliance diffs to Layer A.
4. **Module A** = already on; just hold the discipline (no evidence → no guess).

(Everything in §2 stays parked.)

---

## 6. Summary Verdict

- **v3.0 stays the backbone** (it solves execution; do not rebuild).
- **Lean version = 4 keepers:** A (free), D (⭐), C (half-built), Hermes F1 + back office.
- **Dropped:** AMP auto-exec, paid data API, F2/F3 site automation, multi-strategy router, execution bridge.
- **OC = hands/gate, Hermes = back office/research, Chairman = brain.**
- **Compliance library:** reuse v3.0's, do not build a second one.
- **Info flow:** shared structured files + Chairman approval first.

---

## 7. Decisions (Chairman, LOCKED 2026-06-19)

1. **Build order:** ✅ **Stage 1 = Module D (param pack) FIRST.** It is the daily-pain, independent foundation; Module C depends on it.
2. **Module C trigger source:** ✅ **温总 NinjaTrader feed ONLY** (no TradingView alert, no paid API). Reason: TV and NinjaTrader prices sometimes differ, and trades settle on NinjaTrader — so the alert price MUST equal the fill price, or the alert is false. This also makes the whole param→watch→alert chain self-contained (温总 feed + OC param pack, no external dependency).
3. **Hermes F1 first target platform:** ✅ **TradeDay** (accounts expiring ~7/4 = real driver). Note: F1 = public-page monitoring, needs NO login, so Apex's login difficulty does not affect F1.
4. **Info-flow mechanism:** ✅ **Shared file bus = Git private repo on OC side, Hermes pulls (read-only) + Chairman approval.** Full spec in `OC_Hermes_Bus_Spec.md`.

---

*DISCUSSION DRAFT v1 (lean) | 2026-06-19 | No implementation until Chairman approves. v3.0 changes require Chairman approval.*
