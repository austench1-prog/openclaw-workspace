# Prop Firm Account Migration to APM LLC
# Source: External analysis (Gemini)
# Date: 2026-04-08
# Status: Pending Chairman decision

---

## Goal
Move all Prop Firm accounts under APM LLC for tax/compliance purposes.

---

## Platform-by-Platform Execution

### TPT (Take Profit Trader) — Most Friendly
- Encouraged by platform
- File "Account Ownership Transfer" ticket via Help Desk
- Provide APM LLC EIN and articles
- Payout via Deel: register APM LLC as Entity Account
- Single-member LLC can transfer without re-testing

### Apex — Most Rigid
- Do NOT modify existing personal account
- Register NEW Apex account under APM LLC email
- Must use APM LLC commercial card for payment
- W-9/W-8BEN-E filed under APM LLC at payout stage
- Action: when buying new accounts, use APM LLC info from the start

### MFF — Manual Process
- Email support@myfundedfutures.com with Articles of Organization
- Request Funded Agreement be signed to APM LLC
- Ensure commercial card billing address matches registration address

### TradeDay — Transparent Process
- Members Area → Settings → Billing → change taxpayer to "Company"
- Re-submit W-9 under APM LLC
- Maintain Non-Professional data rate if APM LLC is sole holding vehicle

---

## Agent Teams Adjustments Needed

| Module | Action |
|---|---|
| NotebookLM (ls) | Add APM LLC rule dictionary. Enforce company card check for Apex. |
| Dragon-A | Tag all NinjaTrader accounts as [APM_LLC] or [PERSONAL] |
| Gatekeeper | Add position audit: total position across personal + LLC accounts must not exceed platform per-person max |

---

## Action Priority

1. TPT first (easiest, first LLC revenue case)
2. Apex: all new accounts open under APM LLC from now on
3. MFF: file transfer request
4. TradeDay: update billing settings

---

## Financial Benefit
All exam fees, software fees paid by APM LLC card → deductible expenses → clean year-end reporting

---

## Pending Decisions
- [ ] Chairman confirm: proceed with LLC migration?
- [ ] Get APM LLC commercial card (Citibank account already open)
- [ ] Update purchase SOP to use APM LLC card instead of personal card

---

*Pending Chairman review and decision*
