# Camarilla Weekly Indicator v2 - Specification
# Source: Chairman instruction (Telegram 2026-04-13, handwritten formula)
# Status: CONFIRMED by Chairman
# Version: Midnight Edition

---

## Formula

| Variable | Definition |
|---|---|
| LWH | Last week High |
| LWL | Last week Low |
| LWO | Last week Open — **Monday 00:00 midnight** (NOT Sunday 6pm ET) |
| LWC | Last week Close — Friday 15:59 |
| WO | This week Open — **Monday 00:00 midnight** (NOT Sunday 6pm ET) |

## Data Source Confirmation (2026-04-13)

| Variable | open(WEEK) value | Correct midnight value | Difference |
|---|---|---|---|
| WO (this week) | 24980 (Sun 6pm ET) | 25059 (Mon 00:00) | +79 pts |
| LWO (last week) | 24093 (Sun 6pm ET) | 24224 (Mon 00:00) | +131 pts |

**Conclusion:** Must use Monday 00:00 daily bar open, NOT weekly bar open.
| P | (LWH + LWL + LWO + LWC) / 4 |
| B | 2P + WO - LWC |
| NWH | B - LWL → This week predicted High |
| NWL | B - LWH → This week predicted Low |

---

## Key Difference from Original v1

| | v1 (Original) | v2 (Midnight) |
|---|---|---|
| B formula | B = 2P | B = 2P + WO(this week) - LWC |
| WO source | Weekly bar open | Monday 0:01 midnight |
| WC source | Weekly bar close | Friday 15:59 |
| Variable naming | WH/WL/WO/WC | LWH/LWL/LWO/LWC (L = Last week) |

---

## Naming Convention

**Midnight Version** = Uses 0:01 Monday as open (futures week open)
**Standard Version** = Uses 9:30 AM Monday as open (equity market open)

This label tells the user what data source is used when applying the indicator.

---

## Display Rules

- Lines appear: after this week's Monday 0:01 (WO is available)
- Show: NWH line, NWL line, cloud between them
- Only display within current week (Mon-Fri)
- Color: TBD

---

## Instruments

- Futures only: NQ, ES
- Futures week: Sunday 6pm ET to Friday 5pm ET
- LWO (0:01 Monday) corresponds to Sunday 6pm ET futures open

---

*Spec confirmed: 2026-04-13 | Chairman approved*
