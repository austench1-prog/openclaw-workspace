# Prop Firm Agent - Three-Source Intelligence Protocol V2.1
# Source: Dragon (based on Chairman specification)
# Version: 2.1 | Date: 2026-04-05
# Supersedes: V2.0
# Status: Active specification

---

## Core Architecture: Three-Source Mutual Verification

Dragon monitors THREE dimensions simultaneously.
When conflict exists: apply MOST CONSERVATIVE PRINCIPLE.

| Dimension | Role | Frequency | Output | Priority |
|---|---|---|---|---|
| 1. Trading Platform | Real-time battlefield | High (5min / pre-order) | Live equity, floating drawdown, available margin | HIGHEST (execution basis) |
| 2. Official Website | Legal / financial backbone | Medium (pre-market / post-market) | Payout progress, account status, official PDF rules | MEDIUM (settlement basis) |
| 3. Third-Party Sources | Intelligence patch | Low (weekly / on rule change) | YouTube tutorials, X ban warnings, community interpretation | DYNAMIC (qualitative basis) |

**Conflict resolution rule:**
```
if (Platform_Drawdown > Official_Dashboard_Drawdown):
    use Platform_Drawdown
# Always use whichever data is most dangerous to the account
```

---

## Pre-Market: Rule Modeling & Intelligence

**Trigger:** "Dragon, sync latest Apex/MFF updates."

**Action A (Official site):** Extract static indicators from Dashboard
- Start Balance, Account Status, Drawdown Floor, Days Traded

**Action B (Third-party):** Search X and YouTube:
- Query: "[Firm Name] rule changes 2026"
- Query: "[Firm Name] automation policy banned"
- Query: "[Firm Name] copy trading warning"

**Dragon's reasoning protocol:**
If official site says "EA allowed" BUT X community reports "bans for copy trading":
```json
"compliance_warning": "HIGH - Community reports bans for Copy Trading despite official policy. Human review required."
```

---

## Intraday: Dynamic Risk Control

**Primary source:** Trading platform (Tradovate etc.) — always trusted over dashboard.

**Real-time conflict handling:**
- If dashboard shows loss = $0 but platform shows PnL = -$800
  → Use platform data immediately
  → Do NOT wait for dashboard to refresh

**Monitoring behavior (human-like randomization):**
- Refresh interval: 280-350 seconds (not fixed 300)
- Mouse movement: non-linear with micro-jitter
- IP: Mac mini Henderson residential IP (no VPN, no proxy)
- Method: Browser-based visual reading, not API scraping

---

## Post-Market: Evidence Chain Closure

After each trading session:
1. Save platform screenshot with timestamp
2. Save official dashboard screenshot
3. Record any third-party intelligence from that day
4. Store all in Obsidian with path reference in JSON

Purpose: Complete evidence chain for payout disputes.
Format: "At [timestamp], official site stated [X], third-party stated [Y], we acted [Z]."

---

## Compliance Definition: Visual Simulation + Fingerprint

**Physical isolation:**
- Dragon runs on Mac mini with Henderson residential IP
- Does NOT use direct API scraping
- Reads screen pixels like a human user (monitoring only)

**Human-like behavior (monitoring only - NOT for execution):**
- Random refresh intervals (280-350 seconds)
- Non-linear mouse trajectories with micro-jitter
- Purpose: DATA READING ONLY. Never simulate clicks for order placement.

**HARD RULE - Execution Standard:**
> Any 1% uncertainty on the execution side = DO NOT attempt.
> Apply highest standard. No gambling.
> Only GREEN = explicit written permission from firm.
> Vague language = RED. Community ban reports = RED. No exceptions.

**Order execution path:**
- ONLY via: AI signal → NinjaTrader → Replikanto → Prop Firm accounts
- NEVER via: browser simulation, dashboard clicks, or any unverified method

**Conflict arbitration:**
- Dragon does NOT guess rules
- Dragon FINDS conflicts and escalates to Chairman
- Dragon never makes unilateral decisions on ambiguous rules

---

## Full JSON Output Template (Multi-Source)

```json
{
  "account_context": {
    "firm": "Apex",
    "account_id": "",
    "timestamp": "2026-04-05T22:00:00Z",
    "live_platform_data": {
      "equity": 50200.00,
      "floating_pnl": 0.00,
      "drawdown_floor": 47500.00,
      "room_remaining": 2700.00,
      "status": "Active"
    },
    "official_dashboard": {
      "balance_shown": 50180.00,
      "payout_eligible": false,
      "days_traded": 5,
      "data_lag_detected": false
    }
  },
  "rule_intelligence": {
    "official_text": "[exact raw text from platform]",
    "third_party_insight": "[YouTube / X community finding]",
    "source_url": "",
    "evidence_screenshot_path": "/logs/screenshots/apex_dashboard_20260405.png",
    "safe_score": "GREEN | YELLOW | RED",
    "risk_notes": "[specific risk observation]",
    "compliance_warning": "[if any - exact flag reason]"
  },
  "action_gate": {
    "can_trade": false,
    "block_reason": "[if blocked]",
    "monitoring_active": true,
    "next_check_seconds": 310,
    "human_review_required": false
  }
}
```

---

## Trial Plan: Start with Apex

1. Prepare clean Chrome profile on Mac mini for Apex
2. Login to Apex official site + trading backend
3. Execute one "deep analysis" run
4. Output: Official rule summary + live trading status + X/YouTube intelligence

---

## Platform Notes V2.1

### Apex
- Automation policy: officially "allowed for personal use" — YELLOW (vague terms)
- Community warning: fast-frequency execution may trigger review
- Recommended: keep execution on 1min+ candle intervals
- Floating equity trap: trailing follows unrealized P&L — most dangerous

### MFF
- Trade Copier feature exists = likely GREEN for copier-based automation
- Verify current copier policy before use

### TPT (TakeProfitTrader)
- Automation policy: UNVERIFIED — treat as YELLOW until confirmed
- Search X: "TakeProfitTrader automation banned 2026"

### TradeDay
- Automation policy: likely manual only — treat as RED until confirmed
- Tier 1 news: absolute prohibition
- Search X: "TradeDay automated trading policy"

---

*Version: 2.1 | Supersedes V2.0*
*Chairman approval required for V3.0*
