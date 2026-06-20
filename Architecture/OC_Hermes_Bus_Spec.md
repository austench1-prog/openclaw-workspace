# OC ↔ Hermes Information Bus — Specification v1
# Date: 2026-06-19 | Status: DISCUSSION DRAFT (design only, nothing built)
# Author: Dragon (OC) | Model: Opus 4.8
# Scope: Defines HOW OC and Hermes exchange information. Design only.
#        No repo created, no directory built, no automation enabled.
#        Implementation requires explicit Chairman approval.

---

## 0. Why this document exists

The information flow between OC and Hermes is the Chairman's #1 concern and the
single most important problem in this whole architecture. If the two agents
"guess each other's meaning" through scattered chat, the system can never be
made self-consistent or debugged. This spec locks the channel so that:

- There is ONE source of truth (files), not chat.
- Every exchange is a standard file, not a free-form sentence.
- Everything is versioned, auditable, and recoverable.

---

## 1. Three-Layer Communication Model (Chairman, 2026-06-19, LOCKED)

### Layer 1 — Shared File Bus  (PRIMARY / source of truth)
- The main channel. Both sides exchange STANDARD FILES through a unified directory.
- Five document types: **task / state / result / exception / approval**.
- Main communication is NOT "OC sends Hermes a sentence." It is
  **"OC writes a standard file to the task directory; Hermes reads it."**
- Typical traffic:
  - **Hermes → OC:** rule updates, account-status changes, renew alerts,
    pre-trade admin pack, platform exceptions, strategy approval suggestions.
  - **OC → Hermes:** today's execution-plan summary, account actions to file,
    execution results to archive, exceptions to track, events needing daily/weekly report.

### Layer 2 — Message / Notification Layer  (SECONDARY / doorbell only)
- Telegram notifications, exception alerts, daily summaries, task-done pings.
- **Messages are reminders, NOT the source of truth.**
- Truth always lives in the files. A message only says:
  "there is a new task / a new change / an exception / a report."

### Layer 3 — Human Entry Layer  (CHAIRMAN ENTRY)
- Chairman on 小白 (MacBook Air) via Obsidian / VS Code / Terminal / Chat window
  issues commands to the system.
- This is the **Chairman entry**, NOT the primary agent-to-agent channel.

---

## 2. Bus Implementation = Git repository (LOCKED)

**Decision (Chairman 2026-06-19): the file bus is a Git repository, hosted on the OC
side as a PRIVATE GitHub repo; Hermes pulls from it.**

Why Git (vs iCloud/Dropbox sync, vs LAN share):

| Option | Verdict | Reason |
|---|---|---|
| iCloud / Dropbox sync | ❌ rejected | sync lag + conflict copies ("file 2.md") → split truth |
| LAN share (SMB/NFS) | ❌ not chosen | breaks if one machine is off; permission pairing |
| **Git repo (GitHub private)** | ✅ **CHOSEN** | versioned history, who/when/what-changed, conflict-safe merge, full audit trail; OC already uses Git+GitHub → near-zero extra cost |

Key properties this gives us:
- **Audit:** every file change records who, when, what, and the prior version.
- **Approval integrity:** the `approvals/` folder shows exactly what Chairman
  approved, what an agent wrote, and whether anything was altered.
- **Conflict safety:** simultaneous edits force a merge; no silent dual truth.

Ownership / location:
- Repo lives on the **OC side** (OC is the owner/committer of record).
- Hosted as a **private GitHub repo**.
- **Hermes pulls** from it (and pushes its own outbound files into its lane — see §4).

---

## 3. Directory Structure (LOCKED)

A deliberately simple tree, so "when something breaks, you know where to look":

```
oc-hermes-bus/                 (private Git repo)
├─ inbox/
│  ├─ oc_to_hermes/            # task tickets OC → Hermes
│  └─ hermes_to_oc/            # task tickets Hermes → OC
├─ state/
│  ├─ account_registry/        # account registry snapshots
│  ├─ rule_status/             # current rule status per platform
│  ├─ strategy_status/         # current strategy state
│  └─ route_status/            # execution route status
├─ reports/                    # daily / weekly / review reports
├─ logs/                       # execution logs, error logs, event logs
└─ approvals/                  # items awaiting Chairman confirmation
```

- **inbox/** = task tickets, split by direction (sender writes into the matching folder).
- **state/** = current truth snapshots (account/rule/strategy/route).
- **reports/** = summaries (daily/weekly/review).
- **logs/** = append-only logs (execution/error/event).
- **approvals/** = anything that needs Chairman go/no-go.

---

## 4. Read/Write Lanes (who writes where)

To keep merges clean, each side OWNS its write lane:

| Folder | OC writes | Hermes writes | Chairman acts |
|---|---|---|---|
| inbox/oc_to_hermes | ✅ | reads only | — |
| inbox/hermes_to_oc | reads only | ✅ | — |
| state/* | ✅ (route/strategy) | ✅ (account/rule) | reads |
| reports/ | ✅ (exec reports) | ✅ (admin reports) | reads |
| logs/ | ✅ | ✅ | reads |
| approvals/ | proposes | proposes | ✅ approves (writes decision) |

Rule of thumb: **the recipient never edits an incoming ticket's body; it changes
the ticket's `status` field and/or writes a new `result` ticket.** Original
tickets are never deleted (full traceability).

---

## 5. Document Schemas (LOCKED — both sides must read/write these exactly)

All bus files are Markdown with a YAML front-matter header. Filename convention:
`YYYY-MM-DD_<ID>_<short-slug>.md` (e.g. `2026-06-19_T001_renew-check.md`).

### 5.1 Task ticket
```
---
id: T001                 # T=task, unique
type: task               # task | state | result | exception | approval
from: OC                 # OC | Hermes | Chairman
to: Hermes               # OC | Hermes | Chairman
created: 2026-06-19T23:27-07:00
status: open             # open | in_progress | done | blocked
priority: normal         # low | normal | high | urgent
deadline: 2026-07-01     # optional
subject: 检查6账户renew状态
links: []                # optional related ids
---
Body: what to do, dependencies, definition of done.
```

### 5.2 State snapshot
```
---
id: S-acct-2026-06-19
type: state
domain: account_registry # account_registry | rule_status | strategy_status | route_status
from: Hermes
created: 2026-06-19T23:27-07:00
supersedes: S-acct-2026-06-18   # prior snapshot id
---
Body: the current snapshot (table or structured list).
```

### 5.3 Result ticket
```
---
id: R001
type: result
ref: T001                # the task this answers
from: Hermes
created: 2026-06-20T08:00-07:00
outcome: done            # done | partial | failed
---
Body: what was done, evidence/links, anything outstanding.
```

### 5.4 Exception ticket
```
---
id: X001
type: exception
from: Hermes
severity: high           # low | medium | high | critical
created: 2026-06-19T23:27-07:00
area: platform           # platform | execution | data | account | system
---
Body: what went wrong, impact, suggested action, what needs Chairman.
```

### 5.5 Approval ticket
```
---
id: A001
type: approval
from: OC                 # proposer
created: 2026-06-19T23:27-07:00
status: pending          # pending | approved | rejected
ref: T001                # what this approval concerns
---
Body: the change/diff/action requiring Chairman go/no-go.
# Chairman writes decision below:
decision:                # approved | rejected
decided_by: Chairman
decided_at:
note:
```

---

## 6. Core Disciplines (non-negotiable)

1. **Truth in files, not chat.** Telegram only rings the doorbell.
2. **Never delete a ticket.** Change `status`, or add a `result`. Full trail.
3. **Each side owns its write lane** (§4) → clean merges, no dual truth.
4. **Approvals are explicit and recorded** in `approvals/` with Chairman's
   decision written into the file — not given verbally.
5. **One ID, one file, append history** — never rewrite history silently.
6. **Schema-locked** — both agents read/write the exact headers in §5; a
   malformed ticket is treated as an exception, not silently guessed.

---

## 7. What is NOT built yet (this is design only)

- No repo created, no GitHub private repo provisioned.
- No directories made on either machine.
- No automation, no cron, no agent wired to read/write the bus.
- Hermes has no pull access configured yet.

All of the above happen only AFTER Chairman approves moving to implementation.

---

## 8. Decisions before implementation (Chairman, 2026-06-19)

1. **Repo name:** ✅ **`oc-hermes-bus`** (private GitHub repo, OC side).
2. **Hermes access:** ✅ **read-only deploy key** (Hermes PULLS only; cannot push).
   - ⚠️ Consequence to resolve at implementation: read-only means Hermes cannot
     push its own outbound tickets (`hermes_to_oc/`). Two options (pick at build time):
     (a) Hermes notifies OC via Layer-2 ping → OC writes the ticket on Hermes's behalf; OR
     (b) later grant Hermes a write credential scoped to its own lane only.
     Default for now = (a), keeping Hermes strictly read-only.
3. **Sync cadence:** ⏳ **TBD at implementation** (proposed: event-driven via Layer-2
   ping + fallback periodic pull).
4. **Mount paths:** ⏳ **TBD at implementation** (one path on OC, one on Hermes under `austinha`).

---

*DISCUSSION DRAFT v1 | 2026-06-19 | Design only. Implementation requires Chairman approval.*
