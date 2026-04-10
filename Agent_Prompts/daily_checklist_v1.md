# Daily Trading Checklist v1
# Version: 1.0 | Date: 2026-04-09
# Chairman completes manually before and after each session

---

## PRE-MARKET CHECKLIST (Before Trading Starts)

Complete all items in order. Do not skip any item.
When all items are checked → System is ready to accept signals.

### Infrastructure
- [ ] 1. Win PC (温总) is powered on and online
- [ ] 2. Signal Server running (port 5000 responding)
- [ ] 3. NinjaTrader 8 is open
- [ ] 4. NinjaTrader connection = APEX (green)

### Strategy
- [ ] 5. DragonFileSig **1 Minute** = GREEN (enabled) ✅
- [ ] 6. DragonFileSig **5 Minute** = WHITE (disabled) ✅
- [ ] 7. DragonFileSig Account = **Sim101** ✅

### Replikanto
- [ ] 8. Replikanto panel is open
- [ ] 9. Leader Account = **Sim101**
- [ ] 10. Replikanto **On** = GREEN ✅
- [ ] 11. Replikanto **Cross Order** = GREEN ✅
- [ ] 12. Apex follower account = ON ✅

### Compliance (check before trading)
- [ ] 13. Today is a **full trading day** (no holiday, no early close)
- [ ] 14. Current time is before **16:09 ET**
- [ ] 15. Apex account DD remaining > 10% (check dashboard)
- [ ] 16. No rule changes flagged (NotebookLM last verified < 7 days)

### Final Confirmation
- [ ] 17. All above items checked → **System is READY**
- [ ] 18. Chairman signs off: session start time = ___________

---

## POST-MARKET CHECKLIST (After Trading Ends)

Complete all items in order. Do not skip any item.
When all items are checked → Session is officially closed.

### Positions
- [ ] 1. All positions closed (NinjaTrader Positions panel = empty)
- [ ] 2. No pending orders remaining

### Strategy
- [ ] 3. DragonFileSig 1 Minute = disabled (or leave enabled for tomorrow)
- [ ] 4. Replikanto On = confirm status

### Logging
- [ ] 5. Note today's P&L for each account
- [ ] 6. Note any system issues encountered

### Final Confirmation
- [ ] 7. All above items checked → **Session is CLOSED**
- [ ] 8. Chairman signs off: session end time = ___________

---

## Design Principle

> Human involvement is concentrated in the safe windows (pre-market and post-market).
> During live trading, the system operates automatically.
> The Checklist ensures no critical step is missed before the system takes over.

---

*v1 | 2026-04-09 | Chairman approved*
