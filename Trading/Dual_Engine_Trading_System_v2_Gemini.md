# "Dual Engine" Trading System Architecture v2.0
# Source: Gemini (吉米) output
# Date: 2026-04-09
# Status: Notes - reference for Strategy Pack and future system design

---

## Core Concept

**Trading System = Trading Strategy Layer + Execution Strategy Layer**

Two layers, decoupled responsibilities:
- Trading Strategy: "Legislator" — sets rules, defines the frame
- Order Strategy: "Front Commander" — tactical execution within the frame

---

## Layer 1: Trading Strategy Layer — "Define & Constrain"

**Core function:** Define the battlefield boundary, establish the physical framework.

### Three Structural Points
- **Entry Point**: Where the logic triggers
- **Stop Loss**: Where the logic is invalidated
- **Take Profit**: Where the logic is fulfilled

### Dual-Mode Zone Generation
- **Manual Mode**: Experienced trader draws zones (key swing highs/lows, structure)
- **System Intelligence Mode (ML)**: Imitation Learning — system absorbs trader's logic, auto-generates zones matching trader's style

### Top-Level Constraints
- **Minimum expected profit**: Threshold for "worth taking"
- **Maximum acceptable loss**: Ceiling for "acceptable to lose"

### Role
> It is the "Constitution." It doesn't determine how to win, but it defines the boundaries the trade cannot cross.

---

## Layer 2: Execution Strategy Layer — "Quantify & Enhance"

**Core function:** Within the strategy's boundaries, alter the P&L distribution through micro-operations.

### Sub-Zoning Execution
- Split large zone (e.g. 100 points) into sub-zones (e.g. 50/50)
- **Upper zone strategy**: Trend-adding, using profitable positions for breakout potential
- **Lower zone strategy**: Cost averaging / safety cushion — micro-range trades to cover potential cost

### Dynamic Optimization Goals
- **Risk Erasure (极致控亏)**: Through precise order placement, reduce or eliminate risk before price hits physical stop — achieve "zero-risk holding"
- **Profit Expansion (最大化盈利)**: Through grid, scaling, or layered profit-taking, achieve compound returns beyond single entry

### Role
> It is the "Front Commander." Use tactical means to optimize the strategy's original win/loss probability into "win more, lose minimally."

---

## System Loop Logic

```
Strategy Layer → Certainty (direction, frame, constraints)
Execution Layer → Flexibility (manage P&L ratio within the frame)

Final P&L = Statistical Edge of Trading Strategy + Tactical Optimization of Order Strategy
```

---

## Implementation Notes (from Gemini)

### System Intelligence Mode Implementation
- Start with "labeled historical screenshots" in Obsidian
- Store manually-drawn zones from TOS or NinjaTrader
- Future: AI agent extracts features (slope, volume distribution, MA distances)
- Train a "zone predictor" from these samples

### Sub-Zone Execution in IBKR
Using Conditional Orders:
- When Price < (Entry + 20% Range) → Strategy A (conservative entry)
- When Price > (Entry + 50% Range) → Strategy B (push stop, lock profit)

---

## Two Integration Versions (Gemini proposals)

### Version 1: Sub-system Plugin Mode (Short-term, safer)
- Create `Execution_Subsystem` module
- Use `ib_insync` or IBKR Client Portal API
- Manage order lifecycle, track fills, get real-time account data
- Input: JSON signal (contract, direction, price, stop loss)
- Output: Execution result logged to Obsidian

### Version 2: Full Ecosystem Integration (Long-term vision)
Role split:
- **Analyzer Agent**: ThinkScript logic for SPX/ES/NQ ORB and swing point identification
- **Risk Manager Agent**: Position sizing per APM LLC risk parameters
- **Execution Agent (Dragon)**: Python-driven IBKR API + NinjaTrader/Replikanto multi-account copy

Cross-platform:
- Mac mini as AI compute core
- Communicates with Windows NinjaTrader and IBKR TWS via local API
- All decisions auto-exported to Obsidian in Markdown

---

## Suggested Next Steps (from Gemini)

1. **If starting now**: Version 1 first — connect Python to IBKR TWS API, verify Meritpoint Logic LLC account query
2. **If planning architecture**: Version 2 — defines how Dragon and other agents work together as a full system

---

*Source: Gemini (吉米) | Recorded: 2026-04-09 | Reference for Phase 6 Strategy Pack + future IBKR integration*
