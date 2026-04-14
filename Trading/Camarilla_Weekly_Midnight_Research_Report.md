# Camarilla Weekly Midnight Edition - Research Report
# Date: 2026-04-14
# Status: Development paused - pending better solution for midnight open data

---

## Indicator Specification

### Purpose
Predict this week's High and Low targets based on last week's data.

### Formula (confirmed by Chairman)
- **LWH** = Last week High
- **LWL** = Last week Low
- **LWO** = Last week Monday **00:00 ET midnight** open price
- **LWC** = Last week Friday **16:00 ET** close price
- **WO** = This week Monday **00:00 ET midnight** open price
- **P** = (LWH + LWL + LWO + LWC) / 4
- **B** = 2P + WO - LWC
- **NWH** = B - LWL (This week predicted High)
- **NWL** = B - LWH (This week predicted Low)

### Display
- Blue horizontal lines for NWH and NWL
- Yellow midline
- Label with all input values for verification

---

## Core Problem: Getting the True Midnight (00:00 ET) Price

### Problem Statement
TradingView platform does not natively expose 00:00 ET (New York midnight) as a clean data point for futures contracts.

### What We Tried and What Happened

| Version | Approach | LWO Result | Expected | Difference |
|---|---|---|---|---|
| v14 | `open(WEEK)[1]` | 24,093.5 | 24,218 | -124 pts |
| v15 | `ta.valuewhen(dayofweek.monday, open, 1)` | 24,093.5 | 24,218 | -124 pts |
| v16 | 1h bars, `hour==0 and minute==0` | 24,036.38 | 24,218 | -182 pts |
| v17 | 1h bars, `hour==4` (UTC-4 for EDT) | 24,093.5 | 24,218 | -124 pts |
| v18 | timezone math, NY offset | 25,111 | 24,218 | wrong direction |
| v19 | `hour(time, "America/New_York")` | Syntax error | - | - |

### Root Cause Discovery (2026-04-14)

**Tested with F3 daily indicator which uses `if hour == 0 and minute == 0`:**
- F3 shows: O 0am = **24,980** (what the code thinks is midnight)
- Actual 1h chart bar at Mon Apr 13 00:00 = **25,055.75** (true midnight)
- Difference: **75 points**

**Conclusion:** In TradingView, for NQ1! futures:
- `hour == 0 and minute == 0` takes the **Sunday 18:00 ET** bar's price (futures day session open)
- This is because futures daily bars start at 18:00 ET (Sunday), which TradingView labels as "Monday 00:00" in its internal bar numbering
- The actual wall-clock midnight (00:00 ET Monday) bar is visible on the 1h chart but is NOT captured by `hour == 0` in Pine Script's default timezone handling

### What Works vs What Doesn't

| Source | Price | What it actually is |
|---|---|---|
| `open(WEEK)[1]` | 24,093.5 | Sunday 18:00 ET of 2 weeks ago |
| `ta.valuewhen(monday, open)` | 24,093.5 | Same as above |
| `hour == 0` on 1h bars | 24,980 | Sunday 18:00 ET (week start) |
| Actual 1h bar Mon 00:00 ET | 25,055.75 | True New York midnight ✅ |

---

## Pending Solution Approaches

### Option A: Use 2h bars with exact timestamp targeting
- The 2h bar that starts at Mon 00:00 ET should capture the true midnight price
- Need to test if `hour == 0` on 2h bars gives the correct bar

### Option B: Accept Sunday 18:00 ET as "week open"
- Many futures traders use this definition (first tick of the week)
- Simpler, more reliable, platform-consistent
- Not strictly "midnight" but is a valid reference point

### Option C: Use timestamp arithmetic
- Calculate the Unix timestamp for Mon 00:00 ET
- Use `request.security` with exact timestamp filtering
- More complex but most precise

---

## Current Status of Files

| File | Status | Notes |
|---|---|---|
| camarilla-weekly-thinkscript.txt | Original v1 (preserved) | Uses open(WEEK)[1] = Sunday 18:00 ET |
| camarilla-weekly-thinkscript-v2 through v9 | Various fixes | TOS syntax issues |
| camarilla-weekly-midnight-v10 through v19 | TV Pine Script attempts | Midnight data problem not solved |
| camarilla-formula3-futures.pine | Working ✅ | F3 daily indicator, verified working on TV |

### DragonLab TV Chart
- URL: `https://www.tradingview.com/chart/d1zYpYgC/`
- Account: austench1@gmail.com
- F3 indicator currently loaded and working

---

## Recommendation

**Do not modify any existing files.** 

Resume when a clear solution for capturing true midnight price is found.
Priority approach: Test Option A (2h bars) which may naturally align to ET midnight.

---

*Report by Dragon | 2026-04-14 | Development paused*
