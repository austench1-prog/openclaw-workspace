# Dragon Team Trading System v1.0 Framework
# Source: Gemini (吉米) output
# Date: 2026-04-09
# Status: Notes - reference for system architecture

---

## Five-Layer Model

### Layer 1: Infrastructure Execution Layer
- **Node**: Win PC (温总), NinjaTrader Desktop
- **Core**: Tradovate (Meritpoint Logic LLC) as unified execution channel
- **Tool**: Replikanto for multi-account sync
- **Status**: Running ✅

### Layer 2: AI Brain (Dragon-A, Dragon-B)
- **Dragon-A**: System Engineer — hardware, API stability, SSH, network security
- **Dragon-B**: Business Assistant — organize info, call NotebookLM, prep pre-trade checklist

### Layer 3: Intelligence & Compliance Layer
- **Module A** (Prop Intelligence Agent): Auto-fetch platform rules, FAQ, discounts
- **Module B** (NotebookLM Compliance Skill): Truth filter — verify rules against local knowledge base (contracts, screenshots, PDFs)

### Layer 4: Strategy & Decision Layer
- **Module C** (Strategy Pack): Single mature strategy structured — identify setup, define entry/SL/TP
- **Module D** (Gatekeeper): Risk gate — combine account equity, compliance result, current position → ALLOW / BLOCK / REVIEW

### Layer 5: Execution Layer
- **Module E** (Execution Agent): v1 officially enabled
- **Function**: Implement sub-zone order strategy within trading zone — risk erasure + profit optimization
- **Operating rule**: Small accounts only, Dragon monitors, human review

---

## Business Logic Flow

```
1. [Discover] Strategy Pack identifies NQ setup
2. [Verify] Prop Intelligence fetches rules → NotebookLM cross-checks → confirm no payout conflict
3. [Approve] Gatekeeper outputs: "ALLOW: Execute Strategy A (conservative)"
4. [Execute] Execution Agent sends via API to NinjaTrader (Tradovate LLC) → Replikanto syncs to Prop Firm accounts
5. [Monitor] Dragon monitors latency and fill confirmation → reports back
```

---

## v1 Acceptance Criteria (Gemini version)

1. **Accuracy**: NotebookLM rule query accuracy ≥ 95%
2. **Automation**: "Setup → Compliance → Gatekeeper" flow automated
3. **Real execution**: ≥ 20 trades auto-executed in Meritpoint Logic small account
4. **Circuit breaker**: Dragon triggers system halt within 500ms on network/API anomaly

---

## Role Summary

| Role | Function |
|---|---|
| Dragon (BroLong) | Ops hub + business assistant (dual role) |
| NotebookLM | Truth filter for compliance and rules |
| Tradovate (LLC) | Unified execution home port |
| Replikanto | Cross-entity sync engine |

---

*Source: Gemini (吉米) | Recorded: 2026-04-09 | Reference for system architecture discussion*
