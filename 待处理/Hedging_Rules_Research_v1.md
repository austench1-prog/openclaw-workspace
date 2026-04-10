# Hedging & Cross-Account Trading Rules - Research Draft
# Date: 2026-04-10
# Status: DRAFT - Pending official source verification
# Source: Chairman directive + Gemini analysis

---

## Chairman Directive

Research and establish a system-wide rule covering:
1. Intra-platform hedging restrictions (within same prop firm)
2. Cross-platform hedging restrictions (e.g. long NQ on Apex, short NQ on MFF = prohibited)
3. Cross-asset class rules (NQ/ES vs Gold GC)
4. Use the STRICTEST requirement across all platforms as the system standard

Current market trend: Prop firms are getting increasingly strict on hedging detection.

---

## Known Facts (to be verified from official contracts)

### What is almost certainly prohibited (industry standard):
- Same symbol, opposite direction, across multiple prop firm accounts = PROHIBITED
  - e.g. Long NQ on Apex + Short NQ on MFF → account termination
- Same account, same symbol, opposite direction = PROHIBITED
  - e.g. Long NQ + Short MNQ in same account

### What needs verification:
- Gold (GC) vs NQ/ES cross-direction = unclear, needs official source
- "Relative value" cross-asset hedging = unclear
- Exact definition of what each platform considers "hedging"

---

## Gemini Analysis (for reference, not for direct use in system)

Asset class groupings proposed:
- Group 1 (Equity Indices): NQ, ES, YM, RTY, MNQ, MES
- Group 2 (Metals): GC, MGC, SI
- Group 3 (Energy): CL

Proposed rules:
1. Within same group: must be same direction across ALL accounts (LLC + all Prop Firms)
2. Cross-group: allowed to run different directions (Gold can be opposite to NQ/ES)
3. No "paired locking": prohibit fixed-ratio opposite positions (e.g. always 1 GC vs 1 NQ)

**NOTE: This is Gemini's analysis, NOT from official contracts. Do not use in Gatekeeper until verified.**

---

## Research Required

For each platform (Apex, MFF, TradeDay, TPT):
- [ ] Find "Prohibited Trading Practices" or "Hedging" section in official rules
- [ ] Confirm: is cross-platform same-symbol hedging explicitly prohibited?
- [ ] Confirm: is cross-asset class (Gold vs Indices) explicitly addressed?
- [ ] Confirm: what triggers the hedging detection algorithm?

Sources to check:
- Apex: dashboard.apextraderfunding.com/legal/trading-rules
- MFF: help.myfundedfutures.com
- TradeDay: official rules page
- TPT: official rules page

---

## System Rule Draft (pending verification)

**SYSTEM HEDGING RULE v0.1 (DRAFT - NOT YET ACTIVE)**

Rule A: Same symbol, all accounts must be same direction
- NQ/MNQ: all accounts (LLC + all Prop Firms) must be same direction or flat
- ES/MES: all accounts must be same direction or flat
- GC/MGC: all accounts must be same direction or flat
- Violation → BLOCK

Rule B: Cross-group hedging (pending verification)
- Gold vs NQ/ES opposite direction: [PENDING - needs official source]
- Until verified: treat as REVIEW (human confirmation required)

Rule C: No paired/systematic hedging
- Prohibit fixed-ratio opposite positions across accounts regardless of asset class
- Violation → BLOCK

---

## Action Items
- [ ] Open Chrome on 小塔 + login to Apex dashboard
- [ ] Dragon scrapes Prohibited Activities section
- [ ] Same for MFF
- [ ] Chairman reviews findings
- [ ] Finalize system rule and add to Gatekeeper + NotebookLM

---

*DRAFT | 2026-04-10 | Not active until verified from official sources*
