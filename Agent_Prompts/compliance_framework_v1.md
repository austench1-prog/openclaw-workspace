# MATS Compliance Framework v1
# Date: 2026-04-10
# Chairman approved

---

## Two-Line Compliance Model

### Line 1: System Universal Rules (Fixed, hardcoded in Gatekeeper)
Rules we define ourselves. Stricter than or equal to any platform requirement.
These are NOT extracted from individual contracts.
Once in Line 1, they apply to ALL platforms, ALL accounts, forever.

**SOP for Line 1 assignment:**
> First ask: Can this rule go into Line 1?
> If yes AND the impact on our trading range is small → put in Line 1
> If no OR the impact is too large → consider Line 2

**Current Line 1 Rules:**

| Rule | Value | Reason |
|---|---|---|
| Hard close time | 16:09 ET FLATTEN_ALL | All platforms close between 16:10-17:00. Our buffer protects us. |
| Trading days | Full days only (no holidays, no half-days) | Eliminates liquidity risk. No practical loss. |
| Leader account | Always Sim (virtual), never real | System design requirement. |
| Instrument whitelist | NQ/MNQ, ES/MES, GC/MGC only | All others auto-BLOCK. |
| Anti-hedging | Same symbol = same direction across ALL accounts (LLC + all Prop Firms) | Industry standard. We don't hedge anyway. No practical loss. |
| No paired locking | No fixed-ratio opposite positions across accounts | Industry standard. |
| Mandatory bracket | All orders must include stop loss | Risk management. |

---

### Line 2: Platform-Specific Rules (Extracted from each account contract)
Rules that each platform defines differently.
These ARE extracted from official contracts and stored in NotebookLM.
Gatekeeper queries these per-account before each trade.

**Current Line 2 Fields (extracted per account):**

| Field | Example: Apex EOD | Example: MFF |
|---|---|---|
| Profit Target | $3,000 | $3,000 |
| Max Drawdown | $2,000 | $1,500 |
| Drawdown Type | EOD | EOD |
| Daily Loss Limit | $1,000 | None |
| Min Trading Days | None | 5 days |
| Consistency Rule | None | 50% of target |
| Account Expiry | 2026-05-06 | — |
| Platform-specific instrument restrictions | GC suspended | — |

---

## Line 1 Assignment SOP

When a new rule is discovered, apply this test:

**Step 1:** Is this already an industry-wide standard that all platforms follow?
→ Yes: Strong candidate for Line 1

**Step 2:** If we put this in Line 1, what is the impact on our trading range?
→ Small impact or no impact: Put in Line 1
→ Large impact (significantly limits strategy): Consider Line 2

**Step 3:** Does this rule actually protect us, even if it's stricter than required?
→ Yes (e.g. 16:09 instead of 16:59): Put in Line 1

**Step 4:** If Line 1 is still unclear: default to Line 2 (safer, more flexible)

---

## Pending: Cross-Asset Class Hedging (Gold vs NQ/ES)

**Current status: REVIEW pending**

Question: Can Gold (GC) run opposite direction to NQ/ES across accounts?

Analysis:
- Gemini suggests this is allowed (different asset class = not considered hedging)
- Not yet verified from official contracts

**Temporary rule:** Until verified, Gold direction is treated independently.
If Gold and NQ/ES are simultaneously open in opposite directions → REVIEW (human confirmation)

Action required:
- [ ] Scrape Apex Prohibited Activities section from dashboard
- [ ] Scrape MFF prohibited trading section
- [ ] Confirm or deny Gold vs NQ/ES cross-direction rule
- [ ] After verification: assign to Line 1 or Line 2

---

## New Platform Onboarding SOP

When adding a new Prop Firm to the system:

**Step 1: Line 1 Compatibility Check**
Run the new platform's rules against all Line 1 rules.
- All Line 1 rules pass → Platform can be onboarded
- Any Line 1 rule fails → Go to Step 2

**Step 2: Conflict Resolution**
If a platform's rules conflict with Line 1:
- Option A: Accept the restriction (if Line 1 rule still applies, just reject this platform)
- Option B: Discuss whether to adjust Line 1 (only if the impact is acceptable and the platform is worth it)
- Option C: Reject the platform

**Key principle:**
> Line 1 is the frame. Every new platform must fit inside it.
> We don't change the frame for every new platform.
> We only discuss changing the frame if there's strong reason to.

Line 1 stability = system simplicity.

---

*v1 | Chairman approved 2026-04-10*
