# Level50 Continuation Study — NinjaTrader 8 Setup Instructions
# Version: 1.1 | 2026-06-25 | Dragon

---

## Step 1: Install the Code

1. Open NinjaTrader 8
2. Top menu: **Tools → Edit NinjaScript → Strategy**
3. Click **New Strategy**
4. Name it exactly: `Level50ContinuationStudy`
5. Select all default content (Ctrl+A) → Delete
6. Paste the code (Ctrl+V) — Dragon will put it in clipboard
7. Press **F5** to compile → status bar shows **Compiled successfully**

If compile errors appear, send screenshot to Dragon.

---

## Step 2: Prepare NQ Data

Required: **100 trading days of NQ 1-Minute bars**

1. Open a **NQ** chart set to **1-Minute**
2. Right-click chart → **Data Series**
3. Set **Days to Load** to **140** (extra buffer)
4. Click **OK** → wait for data to finish loading

If data is insufficient:
- Go to **Tools → Historical Data Manager → Download**
- Select NQ, 1 Minute, last 6 months

---

## Step 3: Load the Strategy

**Option A: Run on chart (recommended)**

1. On the NQ 1-Minute chart
2. Right-click → **Strategies → Add Strategy**
3. Select `Level50ContinuationStudy` → double-click
4. Settings:
   - **Calculate** = `On each tick`
   - All other settings: leave default
5. Click **OK**
6. Strategy processes historical data; progress shows in Output window

**Option B: Strategy Analyzer (batch backtest)**

1. Top menu → **New → Strategy Analyzer**
2. Instrument: NQ current contract or @NQ Continuous
3. Strategy: `Level50ContinuationStudy`
4. Data Series: 1 Minute
5. Date range: last 100 trading days
6. Click **Run**

---

## Step 4: Retrieve Results

CSV files are automatically saved to:

```
C:\Users\auste\Documents\NinjaTrader 8\csv\
```

Three files will be created:
- `Level50Study_5m_YYYYMMDD_HHmmss.csv` — 5-minute events (one row per event)
- `Level50Study_15m_YYYYMMDD_HHmmss.csv` — 15-minute events
- `Level50Study_SUMMARY_YYYYMMDD_HHmmss.txt` — summary report with comparison table

Send all three files to Dragon for analysis.

---

## Troubleshooting

**Q: Compile error — namespace not found?**
A: Send screenshot to Dragon. NT8 version difference, fixable in 1 minute.

**Q: No output in Output window?**
A: Open it via Control Center → New → Output Window

**Q: CSV files are empty?**
A: Confirm strategy finished running (no "Running..." indicator on chart).
   Or check Strategy Analyzer shows "Run Complete".

**Q: Only a few days of data?**
A: Use Historical Data Manager to download NQ 1-Minute for the last 6 months.

---

## Data Quality Notes

- **AMBIGUOUS rows**: Both High and Level25 hit within the same 1-minute bar — sequence
  cannot be determined. Required by spec; not an error.
- **UNRESOLVED rows**: Neither target reached before session end.
  Kept in report; excluded from UP/DOWN ratio calculations.

---

## Version History
- v1.0: Initial version (2026-06-25)
- v1.1: Fixed HTF bar index reading (BarsInProgress-driven approach)

---

*Dragon | 2026-06-25*
