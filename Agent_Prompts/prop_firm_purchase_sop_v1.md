# Prop Firm Account Purchase SOP v1
# Date: 2026-04-08
# Status: Active

---

## Prerequisites

- Small Mac (工作 Chrome) must be running with work Google account
- Payment method: Visa ••8869 stored in Chrome autofill
- Chairman must approve each purchase before Dragon executes
- Chrome CDP debug port must be active (port 9222)

---

## Purchase Decision Checklist (Chairman approves before Dragon acts)

- [ ] Which platform? (Apex / MFF / TradeDay / other)
- [ ] Which account size? ($25K / $50K / $100K / $150K)
- [ ] Which platform type? (Tradovate / Rithmic / WealthCharts)
- [ ] Which drawdown type? (EOD Trail / Intraday Trail)
- [ ] Confirm spend amount
- [ ] Confirm card: Visa ••8869

---

## Dragon Execution Steps

### Step 1: Navigate to purchase page
```
Apex EOD 50K Tradovate:
https://dashboard.apextraderfunding.com/signup/50k-Tradovate-eod-trail

Apex Intraday 50K Tradovate:
https://dashboard.apextraderfunding.com/signup/50k-Tradovate-intraday-trail
```

### Step 2: Fill purchase form via Playwright
- Select plan if needed
- Trigger Chrome autofill for payment (Visa ••8869)
- Screenshot before submitting for Chairman review

### Step 3: Chairman final confirm
- Dragon sends screenshot to Telegram
- Chairman says "confirm" → Dragon submits
- Chairman says "stop" → Dragon cancels

### Step 4: Post-purchase
- Screenshot confirmation page
- Note account ID
- Retrieve trading rules from account backend
- Build Account-Sourced Compliance Pack for new account
- Load into NotebookLM MATS_v1_Compliance
- Run 10-question accuracy test

---

## After Purchase: Compliance Pack Build (automatic)

Dragon will immediately:
1. Login to platform backend
2. Retrieve: account type, trading rules, user agreement
3. Save to `NotebookLM_Sources/[Platform]_[AccountID]_pack/`
4. Add to NotebookLM as new source
5. Update Gatekeeper rule card
6. Confirm account ready for trading

---

## Payment Security Rules

- Card number never stored in workspace files or chat
- Card stored only in Chrome autofill (work account)
- Dragon never has direct access to card number
- Every purchase requires explicit Chairman approval before submit
- Screenshot sent to Chairman before every submission

---

*v1 | 2026-04-08*
