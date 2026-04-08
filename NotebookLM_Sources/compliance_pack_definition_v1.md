# Account-Sourced Compliance Pack - Formal Definition
# Version: 1.0 | Date: 2026-04-08
# Status: APPROVED - Chairman confirmed

---

## Official Name

**Account-Sourced Compliance Pack**
(Internal shorthand: "Golden Compliance Pack" / 黄金合规资料包 — same thing)

---

## One-Line Definition

> The Account-Sourced Compliance Pack is the upstream source of truth for all compliance decisions in MATS v1. It is not a summary or a reference — it is the evidence base that determines whether a trade can be authorized.

---

## Five-Layer Definition

### Layer 1: It is an evidence package, not a knowledge base

Not written by Dragon. Not scraped from public websites. Not from third-party reviews.
It is the closest available material to the actual rule source:

- Purchase contract for the specific account
- Dashboard account parameters at time of purchase
- Official backend rules page
- Support replies
- Account status screenshots

Core: not "knowledge" — **evidence**.

### Layer 2: It is the highest-priority source

Why "golden"? Because it is the layer that takes precedence when sources conflict.

**Rule: Compliance data must come only from account-level authorized sources.**
Public official websites and third-party evaluations may serve as peripheral reference only.
They cannot serve as authorization basis for Gatekeeper decisions.

### Layer 3: It is the input food for NotebookLM

NotebookLM does not find truth on its own.
The Account-Sourced Compliance Pack IS the truth source.
NotebookLM is only the interpreter.

```
Evidence collected → Account-Sourced Compliance Pack
    ↓
NotebookLM (interprets and answers rule questions)
    ↓
Gatekeeper (uses verified answers to make decisions)
```

### Layer 4: It is the evidence base for Gatekeeper

NotebookLM does not make final decisions — it outputs "basis for Gatekeeper."
This means: if a rule cannot be traced back to a specific item in the Account-Sourced Compliance Pack, it cannot be used for automatic authorization.

**No evidence in the pack = no automatic ALLOW.**

### Layer 5: It is account-level, not platform-level

Not:
- "Apex rules"
- "MFF rules"

But:
- "Compliance pack for purchased Apex account APEX-165583-123"
- "Compliance pack for purchased MFF account MFFUEVRPD122274040"

Because what matters is not the public platform rules — it is the actual contract and backend rules for the specific account that was purchased.

---

## Full Formal Definition

> An Account-Sourced Compliance Pack is a fixed collection of contract documents, backend rule pages, dashboard parameters, and official communications associated with a specific purchased trading account. It serves as the primary compliance evidence base for NotebookLM interpretation, Gatekeeper authorization decisions, and Execution layer dependencies within the MATS v1 system.

---

## What Qualifies as Evidence (Tier 1 - Authorized Sources)

| Type | Example | Status |
|---|---|---|
| Account purchase contract | User Agreement signed at purchase | ✅ Tier 1 |
| Backend trading rules page | dashboard.apextraderfunding.com/legal | ✅ Tier 1 |
| Dashboard account parameters | Account type, DD amount, expiry date | ✅ Tier 1 |
| Official support reply | Email or ticket response | ✅ Tier 1 |

## What Does NOT Qualify (Tier 2 - Reference Only)

| Type | Status |
|---|---|
| Public official website | ⚠️ Reference only, may be outdated |
| Third-party review / YouTube | ❌ Not valid as authorization basis |
| Internal notes / memory | ❌ Not valid without source citation |
| Community forums / Discord | ❌ Not valid |

---

## SOP: When a New Account is Purchased

1. Immediately retrieve: contract, backend rules page, dashboard parameters
2. Save to: `NotebookLM_Sources/[Platform]_[AccountID]_pack/`
3. Load into NotebookLM MATS_v1_Compliance notebook
4. Run 10-question accuracy test
5. Update Gatekeeper rule card for that account

---

*v1.0 | Chairman approved 2026-04-08*
