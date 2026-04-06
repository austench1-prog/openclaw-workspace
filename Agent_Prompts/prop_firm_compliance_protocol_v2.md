# Prop Firm Agent - Compliance Protocol V2.0
# Source: Dragon (based on Chairman specification)
# Version: 2.0 | Date: 2026-04-05
# Status: Active specification - do not modify without Chairman approval

---

## 1. Data Extraction Schema (8 Categories)

| Category | Depth | Critical Fields | Optional Fields |
|---|---|---|---|
| A. Account Identity | Static | Firm, Account ID, Stage, Status, Broker, Timezone | Plan Name, Reset Fee/Availability |
| B. Financial Targets | Dynamic | Start Balance, Current Equity, Target, Days Traded | Payout Eligibility, Consistency Threshold |
| C. Risk Hard Limits | Core | Daily Loss, Total Drawdown, Drawdown Type (EOD/Intraday), Floor | Scaling Rules, Max Consecutive Loss |
| D. Instruments/Position | Execution | Allowed Symbols, Max Contracts, Micros vs Full-Size | Symbol-specific limits |
| E. Time Restrictions | Environment | News Allowed, EOD Close Required, Weekend Hold, Trading Hours | FOMC/CPI specific windows |
| F. Compliance Lineage | Legal | Automation Policy raw text, Screenshot Path, Source URL | API/Webhook/Copier Policy |

---

## 2. Agent Internal Logic: Three-Step Strategy

### Step 1: Deep Scan + Semantic Alignment

- Drawdown algorithm: Must confirm Trailing_Realtime vs EOD
  - If real-time trailing: add 10% safety margin to risk calculations
- Timezone standardization: Convert all time fields to UTC or EST
- Never rely on dashboard display alone; cross-reference raw data

### Step 2: Generate Compliance Profile (Safety Score)

```
GREEN  - Automation clearly permitted, no complex news restrictions
         → Auto-run allowed

YELLOW - Automation policy ambiguous (e.g., "individual review")
         OR has scaling rules / step requirements
         → Human review required before execution

RED    - Automation explicitly prohibited
         OR account status abnormal
         → Human takeover, no auto execution
```

### Step 3: Pre-Flight Check Before Every Order

```python
def pre_flight_check():
    # 1. Environment validation
    assert account_id matches environment (Live vs Demo)
    
    # 2. Liquidity check
    assert symbol in allowed_symbols
    
    # 3. Circuit breaker
    assert distance_to_daily_loss_limit > 0.5%
    assert distance_to_drawdown_floor > 0.5%
    
    # Only proceed if all checks pass
```

---

## 3. Mandatory Output Format (JSON)

```json
{
  "profile": {
    "firm": "Apex",
    "account_id": "12345",
    "stage": "Funded",
    "status": "Active"
  },
  "risk_engine": {
    "dynamic_floor": 48500.50,
    "current_drawdown": 450.25,
    "is_trailing": true,
    "news_lock_active": false,
    "days_traded": 3,
    "min_days_required": 7
  },
  "compliance_audit": {
    "status": "GREEN | YELLOW | RED",
    "automation_policy_raw": "[exact text from platform]",
    "flags": ["automation_ambiguous", "trailing_no_formula"],
    "evidence_path": "/logs/screenshots/policy_20260405.png",
    "last_checked": "2026-04-05T22:00:00Z"
  },
  "execution_gate": {
    "can_place_order": false,
    "block_reason": "Manual confirmation required - automation policy unclear"
  }
}
```

---

## 4. Red Flag Fields - Force ReadOnly Mode

Agent must block ALL order placement if any of these are detected:

| Trigger | Condition |
|---|---|
| Keyword match | Policy text contains: "Prohibited: High-frequency" / "No AI trading" / "Manual execution only" |
| Data inconsistency | Dashboard balance vs API equity discrepancy > 1% |
| Timezone unknown | Cannot determine Daily Loss reset time |
| Shadow rule | Consistency Rule mentioned but no specific percentage given |
| Trailing no formula | Drawdown type is Trailing but calculation formula not provided |

---

## 5. Rule Change Monitoring

On every login, agent must:
1. Load previous `risk_rules.json`
2. Compare with current extracted data
3. If ANY of these changed → immediate Telegram alert:
   - `max_daily_loss` value
   - `drawdown_type`
   - `news_trading_allowed`
   - `automation_policy`

Alert format:
```
⚠️ RULE CHANGE DETECTED
Firm: [name] | Account: [id]
Changed: max_daily_loss 500 → 450
Action required: Manual review before next trade
```

---

## 6. Standard Agent Prompt (Compliance Officer Mode)

```
You are a senior risk compliance officer.

After logging into the account:
1. EXTRACT: Pull all 8 data categories, output as JSON
2. TRACE: For every rule about "automation" and "drawdown",
   include the raw text excerpt from the platform
3. ASSESS: Based on automation_policy text, assign safety rating
   (GREEN / YELLOW / RED) with justification
4. FLAG: If Drawdown Type is Trailing but no calculation formula
   is provided, set Compliance_Flag = True immediately
5. GATE: Do not pass any order to execution if compliance_audit
   status is not GREEN

Never summarize rules from memory. Always extract from live source.
```

---

## 7. Platform-Specific Notes

### Apex (Intraday Trailing - HIGHEST RISK)
- Trailing follows UNREALIZED equity in real time
- Safety margin: +15% above standard (not 10%)
- Flag immediately if floating P&L > 50% of drawdown limit

### MFF (EOD Trailing)
- Check: drawdown locks static once account reaches starting_balance + $100
- 50% consistency rule during evaluation

### TakeProfitTrader / TPT (EOD or Static depending on plan)
- Confirm: Prime (EOD) vs Classic (Intraday) — different risk profiles
- No daily loss limit but trailing DD acts as one

### TradeDay (Static)
- Simplest drawdown — floor never moves
- 30% consistency rule
- Hard prohibition: no trading during Tier 1 news

---

*Version: 2.0 | Next review when Chairman requests V3.0*
*Do not modify this specification without explicit approval*
