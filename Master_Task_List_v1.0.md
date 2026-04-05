# Master Task List - Multi Agent Trading System
# Source: Dragon (task list only, not final deliverable)
# Version: 1.0 | Date: 2026-04-04
# Rule: All code sections use English only

---

## TASK LIST (9 tasks)

### Task 1: Full system architecture breakdown
- Total system goals
- Module and Agent group structure
- Agent responsibility boundaries
- Layer definitions: foundation / execution / control
- Which modules belong to own-capital vs prop firm system
- Which modules are shared vs separated

### Task 2: 2-3 structural versions from different angles
- Version A: Technical feasibility focus
- Version B: Execution sequence / rollout order focus
- Version C: Revenue and value maximization focus

### Task 3: Capital allocation detailed plan
- Own capital system: $30K SPX options + $6K futures
- Prop firm system: $4K eval account pool
- Risk framework per system
- KPI template per system
- Risk isolation between systems
- Revenue distribution framework
- Prop firm monthly target: min $1,400 USD (approx 10K RMB)

### Task 4: Prop firm system dedicated plan
- Define as operational cash flow / account pool system
- Account pool organization
- Eval → funded → payout → reinvestment cycle
- Roll-out vs front-load strategy
- Phase targets and KPIs

### Task 5: Own capital system dedicated plan
- SPX vs futures division of roles
- Risk control design
- KPI design
- Path to automation

### Task 6: Agent roles, boundaries, and priority
- Platform info Agent
- Platform process Agent
- Strategy signal Agent
- Unified execution Agent
- Risk gate Agent
- Portfolio risk control Agent
- Performance evaluation Agent
- Prop firm account pool Agent
- Platform selection Agent
- Environment gap Agent
- Priority: near-term vs later-stage

### Task 7: Agent prompt drafts
Priority Agent A: Prop firm rules and process Agent
Priority Agent B: Single strategy setup alert Agent
- Both should be reviewable and editable drafts

### Task 8: Summary tree / overview diagram
- Single-page system overview
- Simplified layer map

### Task 9: Pending decisions / answers / approvals list
- Items I need to decide immediately
- Questions I need to answer
- Items awaiting my approval

---

## DRAGON'S PRIORITY ORDER

### Tier 1 - Start immediately (this week)
1. Task 7A: Prop firm rules Agent prompt draft
   - Reason: directly protects accounts, can use today
2. Task 7B: Setup alert Agent prompt draft
   - Reason: directly reduces missed signals
3. Task 6: Agent roles and priority list (simplified version)
   - Reason: needed before building anything

### Tier 2 - Complete this month
4. Task 3: Capital allocation detailed plan
5. Task 4: Prop firm system dedicated plan
6. Task 5: Own capital system dedicated plan
7. Task 8: Summary overview diagram

### Tier 3 - After foundation is stable
8. Task 1: Full architecture breakdown (refine)
9. Task 2: Multiple structural versions

### Task 9 (Pending decisions list)
- This is ongoing, I will maintain it as we work

---

## ITEMS I CANNOT COMPLETE (and why)

### Cannot complete now:

| Item | Reason |
|---|---|
| Automated real-time signal detection | Requires live market data feed API (TOS webhook not yet connected) |
| IBKR execution scripts | Requires IBKR account to be opened first |
| Prop firm account status monitoring | No active accounts yet, no data to pull |
| TradingView webhook integration | Requires TradingView Pro and webhook endpoint setup |
| Full multi-agent orchestration | Requires platform API access (AMP, IBKR) not yet available |
| Strategy signal Agent (live) | Requires your strategy rules to be defined first (need your input) |

### Cannot complete without your input:

| Item | What I need from you |
|---|---|
| Setup alert Agent | Your specific strategy logic (conditions, entry, invalidation) |
| Prop firm KPI targets | Confirm monthly income target in USD |
| Risk thresholds | Confirm daily loss limits per account |
| Account pool plan | Confirm which platforms to open first and in what order |

---

## NEXT ACTIONS

Immediate (I will do):
- [ ] Draft Prop firm rules Agent prompt (Task 7A)
- [ ] Draft Setup alert Agent prompt skeleton (Task 7B) - needs your strategy input

Waiting for you:
- [ ] Your strategy logic for SPX 0DTE or futures setup alert
- [ ] Confirm prop firm opening order (TradeDay first, confirmed)
- [ ] IBKR account application (you said by Monday)
