# Camarilla Weekly Indicator - Original Specification
# Source: Chairman original instruction (Telegram 2026-04-02 10:20 PM)
# Status: LOCKED - do not modify without Chairman approval

---

## Original Instruction (verbatim)

"code for TOS, P = (WH + WL + WC + WO) / 4
B = 2P
NWH = B - WL
NWL = B - WH
WH = this week high
WL = this week low
WO = this Monday open
WC = this Friday close
NWH = next week high
NWL = next week low"

---

## Confirmed Logic

| Variable | Definition |
|---|---|
| WH | This week's High |
| WL | This week's Low |
| WO | This week's Monday open (first bar of the week) |
| WC | This week's Friday close (last bar of the week, after 16:00 ET) |
| P | (WH + WL + WC + WO) / 4 |
| B | 2 × P |
| NWH | B - WL → Next week predicted High |
| NWL | B - WH → Next week predicted Low |

---

## Instrument

- **Futures only** (NQ, ES) — NOT SPX or equities
- Futures week starts Sunday 6pm ET, ends Friday 5pm ET
- WO = first tick of Sunday 6pm ET open (futures week open)
- WC = Friday 5pm ET close (futures week close)
- In TOS: `open(WEEK)[1]` and `close(WEEK)[1]` correctly map to these

---

## Display Rules

- Lines appear: after Friday close of this week (data complete)
- Lines disappear: at end of next week (replaced by new prediction)
- Color: Sky blue (distinct from daily indicators)
- Show: NWH line, NWL line, MID line, cloud between NWH and NWL

---

## Version History

| Version | Date | Change |
|---|---|---|
| v1 (original) | 2026-04-02 | First implementation |
| v2-v9 | 2026-04-13 | Various attempts to fix line display range |
| Pending v10 | TBD | To be written after Chairman confirms new instruction |

---

## Pending: Chairman's New Instruction

Chairman is preparing a revised/clarified instruction.
Do not write new code until new instruction is confirmed and saved here.

---

*Spec created: 2026-04-13 | Source: Telegram 2026-04-02 | Chairman approved*
