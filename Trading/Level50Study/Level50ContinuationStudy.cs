// =============================================================================
// Level50ContinuationStudy.cs
// NQ Level50 Continuation Event Study
// Version: 1.1 | Written: 2026-06-25 | Dragon for President
//
// Purpose:
//   For each completed bullish 5-minute and 15-minute candle on NQ,
//   detect the first Level50 retrace and record whether price breaks
//   the candle High (UP) or touches Level25 (DOWN) first.
//   NO entries, NO exits, NO P&L --- observation only.
//
// How to install:
//   1. NinjaTrader 8 --- Tools --- Edit NinjaScript --- Strategy
//   2. Create new file: Level50ContinuationStudy.cs
//   3. Paste this entire file, replacing the default content
//   4. Compile (F5)
//
// How to run:
//   1. Open a NQ 1-Minute chart
//   2. Load at least 100 trading days of 1-Minute data
//      (right-click chart --- Data Series --- Days to Load: 140)
//   3. Strategies --- Add Strategy --- Level50ContinuationStudy
//   4. Set Calculate = On each tick
//   5. Run (Enable on chart OR use Strategy Analyzer in backtest mode)
//   6. CSV output: Documents\NinjaTrader 8\csv\
// =============================================================================

#region Using declarations
using System;
using System.IO;
using System.Collections.Generic;
using NinjaTrader.Cbi;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.Strategies;
using NinjaTrader.Data;
#endregion

namespace NinjaTrader.NinjaScript.Strategies
{
    public class Level50ContinuationStudy : Strategy
    {
        // --------- CSV output ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private string _csv5mPath;
        private string _csv15mPath;
        private StreamWriter _sw5m;
        private StreamWriter _sw15m;
        private string _sessionTemplate;

        // --------- Pending candle from HTF series ------------------------------------------------------------------------------------------------------
        // When a new HTF bar opens, we grab the just-closed HTF bar data
        private class HtfCandle
        {
            public DateTime OpenTime;
            public double Open, High, Low, Close, Range;
            public double Level50, Level25;
        }

        // --------- Active monitoring event ------------------------------------------------------------------------------------------------------------------------------
        private class MonitorEvent
        {
            public HtfCandle Candle;
            public bool      Level50Touched;
            public DateTime  FirstTouchTime;
            public double    MaxAfterTouch;
            public double    MinAfterTouch;
            public bool      Resolved;
            public string    Outcome;
            public DateTime  OutcomeTime;
            public int       MinutesToOutcome;
        }

        private MonitorEvent _evt5m  = null;
        private MonitorEvent _evt15m = null;

        // Track last HTF bar processed (avoid duplicate event per bar)
        private int _prev5mBarCount  = 0;
        private int _prev15mBarCount = 0;

        // --------- Summary counters ---------------------------------------------------------------------------------------------------------------------------------------------------
        private int _bull5m, _bull15m;
        private int _touch5m, _touch15m;
        private int _up5m, _dn5m, _amb5m, _unr5m;
        private int _up15m, _dn15m, _amb15m, _unr15m;

        private List<double> _tUp5m   = new List<double>();
        private List<double> _tDn5m   = new List<double>();
        private List<double> _tUp15m  = new List<double>();
        private List<double> _tDn15m  = new List<double>();
        private List<double> _rng5m   = new List<double>();
        private List<double> _rng15m  = new List<double>();

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description     = "NQ Level50 Continuation Event Study --- observation only, no orders.";
                Name            = "Level50ContinuationStudy";
                Calculate       = Calculate.OnEachTick;
                IsOverlay       = false;
                IsAutoScale     = false;
                // Prevent any accidental order placement
                EntriesPerDirection   = 0;
                EntryHandling         = EntryHandling.AllEntries;
                BarsRequiredToTrade   = 2;
            }
            else if (State == State.Configure)
            {
                // Primary series: 1-Minute  (BarsArray[0])
                // Series 1:       5-Minute  (BarsArray[1])
                // Series 2:       15-Minute (BarsArray[2])
                AddDataSeries(BarsPeriodType.Minute, 5);
                AddDataSeries(BarsPeriodType.Minute, 15);
            }
            else if (State == State.DataLoaded)
            {
                // Build output paths
                string csvDir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "NinjaTrader 8", "csv");
                Directory.CreateDirectory(csvDir);

                string stamp  = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                _csv5mPath    = Path.Combine(csvDir, "Level50Study_5m_"  + stamp + ".csv");
                _csv15mPath   = Path.Combine(csvDir, "Level50Study_15m_" + stamp + ".csv");

                string hdr =
                    "Date,Time,Instrument,StudyTimeframe," +
                    "HigherTimeframeCandleOpenTime,Open,High,Low,Close,Range," +
                    "Level50,Level25,FirstLevel50TouchTime," +
                    "Outcome,OutcomeTime,MinutesFromLevel50ToOutcome," +
                    "MaximumPriceAfterLevel50,MinimumPriceAfterLevel50," +
                    "SessionTemplate,DataResolution";

                _sw5m  = new StreamWriter(_csv5mPath,  false);
                _sw15m = new StreamWriter(_csv15mPath, false);
                _sw5m.WriteLine(hdr);
                _sw15m.WriteLine(hdr);
                _sw5m.Flush();
                _sw15m.Flush();

                try   { _sessionTemplate = TradingHours.Name; }
                catch { _sessionTemplate = "Default"; }

                Print("[Level50Study] DataLoaded. Output --- " + csvDir);
            }
            else if (State == State.Terminated)
            {
                // Close any still-active events as UNRESOLVED
                if (_evt5m  != null && !_evt5m.Resolved  && _evt5m.Level50Touched)
                    CloseAsUnresolved(_evt5m,  "5m",  _sw5m);
                if (_evt15m != null && !_evt15m.Resolved && _evt15m.Level50Touched)
                    CloseAsUnresolved(_evt15m, "15m", _sw15m);

                WriteSummary();

                if (_sw5m  != null) { _sw5m.Flush();  _sw5m.Close();  }
                if (_sw15m != null) { _sw15m.Flush(); _sw15m.Close(); }

                Print("[Level50Study] Done.");
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected override void OnBarUpdate()
        {
            // ------ Process HTF candle completions ------------------------------------------------------------------------------------------------
            // When BarsInProgress == 1 (5m series), a 5m bar just closed
            // When BarsInProgress == 2 (15m series), a 15m bar just closed
            if (BarsInProgress == 1)
            {
                OnHtfBarClosed(BarsArray[1], "5m");
                return;
            }
            if (BarsInProgress == 2)
            {
                OnHtfBarClosed(BarsArray[2], "15m");
                return;
            }

            // ------ BarsInProgress == 0: 1-minute primary tick ------------------------------------------------------------
            // Monitor active events every tick
            if (BarsInProgress != 0) return;
            if (CurrentBars[0] < 1) return;

            double barHigh = High[0];
            double barLow  = Low[0];
            DateTime t     = Time[0];

            MonitorActive(ref _evt5m,  "5m",  barHigh, barLow, t);
            MonitorActive(ref _evt15m, "15m", barHigh, barLow, t);
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Called when a higher-timeframe bar closes (BarsInProgress == 1 or 2)
        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void OnHtfBarClosed(Bars htfBars, string tf)
        {
            // index [0] on the HTF series inside OnBarUpdate(BarsInProgress==1/2)
            // is the bar that JUST CLOSED (current completed bar)
            double o = htfBars.GetOpen(htfBars.Count - 1);
            double h = htfBars.GetHigh(htfBars.Count - 1);
            double l = htfBars.GetLow(htfBars.Count - 1);
            double c = htfBars.GetClose(htfBars.Count - 1);
            DateTime ot = htfBars.GetTime(htfBars.Count - 1);

            // Not bullish --- ignore
            if (c <= o) return;

            double range = h - l;
            if (range <= 0) return;

            if (tf == "5m")
            {
                _bull5m++;
                _rng5m.Add(range);

                // Previous event with no Level50 touch --- discard quietly
                // Previous event with Level50 touch --- UNRESOLVED
                if (_evt5m != null && !_evt5m.Resolved && _evt5m.Level50Touched)
                    CloseAsUnresolved(_evt5m, "5m", _sw5m);

                _evt5m = MakeEvent(o, h, l, c, range, ot);
            }
            else
            {
                _bull15m++;
                _rng15m.Add(range);

                if (_evt15m != null && !_evt15m.Resolved && _evt15m.Level50Touched)
                    CloseAsUnresolved(_evt15m, "15m", _sw15m);

                _evt15m = MakeEvent(o, h, l, c, range, ot);
            }
        }

        private MonitorEvent MakeEvent(
            double o, double h, double l, double c, double range, DateTime ot)
        {
            return new MonitorEvent
            {
                Candle = new HtfCandle
                {
                    OpenTime = ot,
                    Open     = o,
                    High     = h,
                    Low      = l,
                    Close    = c,
                    Range    = range,
                    Level50  = l + 0.50 * range,
                    Level25  = l + 0.25 * range
                },
                Level50Touched   = false,
                Resolved         = false,
                MaxAfterTouch    = double.MinValue,
                MinAfterTouch    = double.MaxValue
            };
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Called every 1-minute tick to monitor an active event
        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void MonitorActive(
            ref MonitorEvent ev, string tf,
            double barHigh, double barLow, DateTime barTime)
        {
            if (ev == null || ev.Resolved) return;

            var c = ev.Candle;

            if (!ev.Level50Touched)
            {
                // Wait for price to retrace INTO Level50
                // Condition: bar traded at or through Level50 from above
                //   --- barLow <= Level50  AND  barHigh >= Level25  (still within the zone)
                // We also require barHigh < c.High (price is retracing, not still above)
                // Simplified: first bar whose Low <= Level50 gets the touch
                if (barLow <= c.Level50)
                {
                    ev.Level50Touched  = true;
                    ev.FirstTouchTime  = barTime;
                    ev.MaxAfterTouch   = barHigh;
                    ev.MinAfterTouch   = barLow;

                    if (tf == "5m")  _touch5m++;
                    else             _touch15m++;

                    // Check for immediate outcome on the same touch bar
                    CheckOutcome(ref ev, tf, barHigh, barLow, barTime);
                }
                return;
            }

            // Update running extremes
            if (barHigh > ev.MaxAfterTouch) ev.MaxAfterTouch = barHigh;
            if (barLow  < ev.MinAfterTouch) ev.MinAfterTouch = barLow;

            CheckOutcome(ref ev, tf, barHigh, barLow, barTime);
        }

        private void CheckOutcome(
            ref MonitorEvent ev, string tf,
            double barHigh, double barLow, DateTime barTime)
        {
            if (ev.Resolved) return;
            var c = ev.Candle;

            bool hitHigh   = barHigh > c.High;     // Broke above ImpulseHigh
            bool hitL25    = barLow  <= c.Level25;  // Touched or through Level25

            string outcome = null;
            if      (hitHigh && hitL25) outcome = "AMBIGUOUS";
            else if (hitHigh)           outcome = "UP";
            else if (hitL25)            outcome = "DOWN";

            if (outcome == null) return;

            ev.Resolved         = true;
            ev.Outcome          = outcome;
            ev.OutcomeTime      = barTime;
            ev.MinutesToOutcome = (int)Math.Round((barTime - ev.FirstTouchTime).TotalMinutes);

            RecordOutcome(tf, outcome, ev);
            WriteRow(tf == "5m" ? _sw5m : _sw15m, ev, tf);

            // Null out --- stop monitoring
            ev = null;
        }

        private void CloseAsUnresolved(MonitorEvent ev, string tf, StreamWriter sw)
        {
            ev.Resolved         = true;
            ev.Outcome          = "UNRESOLVED";
            ev.OutcomeTime      = DateTime.MinValue;
            ev.MinutesToOutcome = -1;

            RecordOutcome(tf, "UNRESOLVED", ev);
            WriteRow(sw, ev, tf);
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void RecordOutcome(string tf, string outcome, MonitorEvent ev)
        {
            if (tf == "5m")
            {
                switch (outcome)
                {
                    case "UP":         _up5m++;  if (ev.MinutesToOutcome >= 0) _tUp5m.Add(ev.MinutesToOutcome);  break;
                    case "DOWN":       _dn5m++;  if (ev.MinutesToOutcome >= 0) _tDn5m.Add(ev.MinutesToOutcome);  break;
                    case "AMBIGUOUS":  _amb5m++; break;
                    case "UNRESOLVED": _unr5m++; break;
                }
            }
            else
            {
                switch (outcome)
                {
                    case "UP":         _up15m++;  if (ev.MinutesToOutcome >= 0) _tUp15m.Add(ev.MinutesToOutcome);  break;
                    case "DOWN":       _dn15m++;  if (ev.MinutesToOutcome >= 0) _tDn15m.Add(ev.MinutesToOutcome);  break;
                    case "AMBIGUOUS":  _amb15m++; break;
                    case "UNRESOLVED": _unr15m++; break;
                }
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void WriteRow(StreamWriter sw, MonitorEvent ev, string tf)
        {
            if (sw == null || ev == null || !ev.Level50Touched) return;
            var c = ev.Candle;

            string outcomeTime = ev.OutcomeTime == DateTime.MinValue
                ? ""
                : ev.OutcomeTime.ToString("yyyy-MM-dd HH:mm:ss");
            string mins = ev.MinutesToOutcome < 0 ? "" : ev.MinutesToOutcome.ToString();
            string maxP = ev.MaxAfterTouch == double.MinValue ? "" : ev.MaxAfterTouch.ToString("F2");
            string minP = ev.MinAfterTouch == double.MaxValue ? "" : ev.MinAfterTouch.ToString("F2");

            sw.WriteLine(string.Join(",",
                c.OpenTime.ToString("yyyy-MM-dd"),
                c.OpenTime.ToString("HH:mm:ss"),
                Instrument.FullName,
                tf,
                c.OpenTime.ToString("yyyy-MM-dd HH:mm:ss"),
                c.Open.ToString("F2"),
                c.High.ToString("F2"),
                c.Low.ToString("F2"),
                c.Close.ToString("F2"),
                c.Range.ToString("F2"),
                c.Level50.ToString("F2"),
                c.Level25.ToString("F2"),
                ev.FirstTouchTime.ToString("yyyy-MM-dd HH:mm:ss"),
                ev.Outcome,
                outcomeTime,
                mins,
                maxP,
                minP,
                _sessionTemplate,
                "1-Minute"
            ));
            sw.Flush();
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Summary report
        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void WriteSummary()
        {
            string dir = Path.GetDirectoryName(_csv5mPath);
            string path = Path.Combine(dir,
                "Level50Study_SUMMARY_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");

            using (var sw = new StreamWriter(path, false))
            {
                sw.WriteLine("=================================================================");
                sw.WriteLine("  NQ LEVEL50 CONTINUATION STUDY --- SUMMARY REPORT");
                sw.WriteLine("  Generated : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sw.WriteLine("  Instrument: " + Instrument.FullName);
                sw.WriteLine("  Session   : " + _sessionTemplate);
                sw.WriteLine("  Resolution: 1-Minute bars");
                sw.WriteLine("=================================================================");
                sw.WriteLine();

                PrintStudyBlock(sw, "5-Minute",
                    _bull5m, _touch5m, _up5m, _dn5m, _amb5m, _unr5m,
                    _tUp5m, _tDn5m, _rng5m);

                sw.WriteLine();

                PrintStudyBlock(sw, "15-Minute",
                    _bull15m, _touch15m, _up15m, _dn15m, _amb15m, _unr15m,
                    _tUp15m, _tDn15m, _rng15m);

                sw.WriteLine();
                sw.WriteLine("=================================================================");
                sw.WriteLine("  FINAL COMPARISON TABLE");
                sw.WriteLine("=================================================================");
                sw.WriteLine();

                int v5  = _up5m  + _dn5m;
                int v15 = _up15m + _dn15m;
                double hfr5  = v5  > 0 ? (double)_up5m  / v5  : 0;
                double hfr15 = v15 > 0 ? (double)_up15m / v15 : 0;
                double lfr5  = v5  > 0 ? (double)_dn5m  / v5  : 0;
                double lfr15 = v15 > 0 ? (double)_dn15m / v15 : 0;
                double ar5   = _touch5m  > 0 ? (double)_amb5m  / _touch5m  : 0;
                double ar15  = _touch15m > 0 ? (double)_amb15m / _touch15m : 0;
                double ur5   = _touch5m  > 0 ? (double)_unr5m  / _touch5m  : 0;
                double ur15  = _touch15m > 0 ? (double)_unr15m / _touch15m : 0;

                string f = "{0,-40} {1,16} {2,16}";
                sw.WriteLine(string.Format(f, "Metric", "5-Minute", "15-Minute"));
                sw.WriteLine(new string('-', 74));
                sw.WriteLine(string.Format(f, "Bullish candle count",         _bull5m,            _bull15m));
                sw.WriteLine(string.Format(f, "Level50 pullback count",       _touch5m,           _touch15m));
                sw.WriteLine(string.Format(f, "High-first rate",              hfr5.ToString("P2"),  hfr15.ToString("P2")));
                sw.WriteLine(string.Format(f, "Level25-first rate",           lfr5.ToString("P2"),  lfr15.ToString("P2")));
                sw.WriteLine(string.Format(f, "Ambiguous-event rate",         ar5.ToString("P2"),   ar15.ToString("P2")));
                sw.WriteLine(string.Format(f, "Unresolved-event rate",        ur5.ToString("P2"),   ur15.ToString("P2")));
                sw.WriteLine(string.Format(f, "Median time to High break",
                    _tUp5m.Count  > 0 ? Median(_tUp5m).ToString("F1")  + " min" : "N/A",
                    _tUp15m.Count > 0 ? Median(_tUp15m).ToString("F1") + " min" : "N/A"));
                sw.WriteLine(string.Format(f, "Median time to Level25 touch",
                    _tDn5m.Count  > 0 ? Median(_tDn5m).ToString("F1")  + " min" : "N/A",
                    _tDn15m.Count > 0 ? Median(_tDn15m).ToString("F1") + " min" : "N/A"));
                sw.WriteLine(string.Format(f, "Median candle range",
                    _rng5m.Count  > 0 ? Median(_rng5m).ToString("F2")  + " pts" : "N/A",
                    _rng15m.Count > 0 ? Median(_rng15m).ToString("F2") + " pts" : "N/A"));
                sw.WriteLine();

                // Recommendation
                sw.WriteLine("=================================================================");
                sw.WriteLine("  DECISION GUIDANCE");
                sw.WriteLine("=================================================================");
                string verdict;
                if (hfr5 > hfr15 && v5 >= 30)
                    verdict = "5-Minute shows higher High-first rate with adequate sample.";
                else if (hfr15 > hfr5 && v15 >= 30)
                    verdict = "15-Minute shows higher High-first rate with adequate sample.";
                else if (v5 < 30 && v15 < 30)
                    verdict = "WARNING: Sample size too small (<30 valid events). Extend lookback.";
                else
                    verdict = "Rates are close. Apply all 5 selection criteria before deciding.";

                sw.WriteLine("  " + verdict);
                sw.WriteLine("  Reminder: 5 criteria = rate + sample + ambiguity + L25 rate + time.");
                sw.WriteLine("=================================================================");
            }

            Print("[Level50Study] Summary saved --- " + path);
        }

        private void PrintStudyBlock(
            StreamWriter sw, string label,
            int bull, int touch, int up, int dn, int amb, int unr,
            List<double> tUp, List<double> tDn, List<double> rngs)
        {
            int valid = up + dn;
            double hfr  = valid > 0 ? (double)up  / valid : 0;
            double lfr  = valid > 0 ? (double)dn  / valid : 0;
            double ar   = touch > 0 ? (double)amb / touch  : 0;
            double ur   = touch > 0 ? (double)unr / touch  : 0;

            sw.WriteLine("  ------ " + label + " Candle Study ------------------------------------------------------------------------------------------");
            sw.WriteLine("  Sample Statistics");
            sw.WriteLine("    Total bullish candles:           " + bull);
            sw.WriteLine("    Total Level50 pullbacks:         " + touch);
            sw.WriteLine("    UP count:                        " + up);
            sw.WriteLine("    DOWN count:                      " + dn);
            sw.WriteLine("    AMBIGUOUS count:                 " + amb);
            sw.WriteLine("    UNRESOLVED count:                " + unr);
            sw.WriteLine("    Valid resolved (UP + DOWN):      " + valid);
            sw.WriteLine();
            sw.WriteLine("  Primary Ratios");
            sw.WriteLine("    High-first rate (UP/valid):      " + hfr.ToString("P2"));
            sw.WriteLine("    Level25-first rate (DN/valid):   " + lfr.ToString("P2"));
            sw.WriteLine("    Ambiguous-event rate:            " + ar.ToString("P2"));
            sw.WriteLine("    Unresolved-event rate:           " + ur.ToString("P2"));
            sw.WriteLine();
            sw.WriteLine("  Timing Statistics");
            sw.WriteLine("    Median min --- High break:         " + (tUp.Count > 0  ? Median(tUp).ToString("F1")  : "N/A"));
            sw.WriteLine("    Average min --- High break:        " + (tUp.Count > 0  ? Avg(tUp).ToString("F1")     : "N/A"));
            sw.WriteLine("    Median min --- Level25 touch:      " + (tDn.Count > 0  ? Median(tDn).ToString("F1")  : "N/A"));
            sw.WriteLine("    Average min --- Level25 touch:     " + (tDn.Count > 0  ? Avg(tDn).ToString("F1")     : "N/A"));
            sw.WriteLine("    Median candle range (pts):       " + (rngs.Count > 0 ? Median(rngs).ToString("F2") : "N/A"));
            sw.WriteLine("    Average candle range (pts):      " + (rngs.Count > 0 ? Avg(rngs).ToString("F2")    : "N/A"));
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private double Median(List<double> d)
        {
            if (d.Count == 0) return 0;
            var s = new List<double>(d); s.Sort();
            int m = s.Count / 2;
            return s.Count % 2 == 0 ? (s[m-1] + s[m]) / 2.0 : s[m];
        }

        private double Avg(List<double> d)
        {
            if (d.Count == 0) return 0;
            double sum = 0; foreach (var v in d) sum += v; return sum / d.Count;
        }
    }
}
