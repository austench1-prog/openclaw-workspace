// =============================================================================
// Level50ContinuationStudy.cs
// NQ Level50 Continuation Event Study
// Version: 2.0 | 2026-06-25 | Dragon
//
// DESIGN (v2):
//   Primary series = 1-Minute (BarsArray[0]).
//   5m and 15m candle boundaries are detected inside OnBarUpdate(BarsInProgress==0)
//   by tracking when BarsArray[1] / BarsArray[2] bar count increments.
//   This is compatible with both live-chart mode and Strategy Analyzer backtest mode.
//
//   NO orders are placed. This is a pure observation study.
//
// INSTALL:
//   NT8 -> Tools -> Edit NinjaScript -> Strategy
//   Open Level50ContinuationStudy, paste entire file, F5
//
// RUN (Strategy Analyzer):
//   Instrument : NQ SEP26
//   Type       : Minute  Value: 1
//   Strategy   : Level50ContinuationStudy
//   Bars back  : Infinite
//   Date range : 01/01/2026 - today
//   Click Run
//
// OUTPUT:
//   Documents\NinjaTrader 8\csv\Level50Study_5m_*.csv
//   Documents\NinjaTrader 8\csv\Level50Study_15m_*.csv
//   Documents\NinjaTrader 8\csv\Level50Study_SUMMARY_*.txt
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
        // ---- CSV writers -------------------------------------------------------
        private StreamWriter _sw5m;
        private StreamWriter _sw15m;
        private string _csv5mPath;
        private string _csv15mPath;
        private string _sessionName;

        // ---- HTF bar tracking --------------------------------------------------
        // We detect a new HTF bar by watching when BarsArray[n].Count increases.
        private int _prev5mCount  = 0;
        private int _prev15mCount = 0;

        // ---- Active monitoring event -------------------------------------------
        private class Event
        {
            // Source HTF candle
            public DateTime CandleOpen;
            public double   Open, High, Low, Close, Range;
            public double   Level50, Level25;

            // Touch state
            public bool     Touched;
            public DateTime TouchTime;
            public double   MaxAfter = double.MinValue;
            public double   MinAfter = double.MaxValue;

            // Result
            public bool     Done;
            public string   Outcome;   // UP / DOWN / AMBIGUOUS / UNRESOLVED
            public DateTime OutcomeTime;
            public int      Minutes;
        }

        private Event _e5m  = null;
        private Event _e15m = null;

        // ---- Counters ----------------------------------------------------------
        private int _bull5m,  _bull15m;
        private int _touch5m, _touch15m;
        private int _up5m,    _dn5m,    _amb5m,  _unr5m;
        private int _up15m,   _dn15m,   _amb15m, _unr15m;

        private List<double> _tUp5m  = new List<double>();
        private List<double> _tDn5m  = new List<double>();
        private List<double> _tUp15m = new List<double>();
        private List<double> _tDn15m = new List<double>();
        private List<double> _rng5m  = new List<double>();
        private List<double> _rng15m = new List<double>();

        // ========================================================================
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description  = "NQ Level50 Continuation Event Study v2 - observation only";
                Name         = "Level50ContinuationStudy";
                Calculate    = Calculate.OnBarClose;   // use OnBarClose for SA compatibility
                IsOverlay    = false;
                IsAutoScale  = false;
                EntriesPerDirection        = 1;
                EntryHandling              = EntryHandling.AllEntries;
                IsExitOnSessionCloseStrategy = false;
                BarsRequiredToTrade        = 5;
            }
            else if (State == State.Configure)
            {
                AddDataSeries(BarsPeriodType.Minute, 5);   // BarsArray[1]
                AddDataSeries(BarsPeriodType.Minute, 15);  // BarsArray[2]
            }
            else if (State == State.DataLoaded)
            {
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

                try   { _sessionName = TradingHours.Name; }
                catch { _sessionName = "Default"; }

                Print("[Level50v2] Ready. Output: " + csvDir);
            }
            else if (State == State.Terminated)
            {
                // Close any open events as UNRESOLVED
                if (_e5m  != null && !_e5m.Done  && _e5m.Touched)
                    Finalize(_e5m,  "5m",  _sw5m);
                if (_e15m != null && !_e15m.Done && _e15m.Touched)
                    Finalize(_e15m, "15m", _sw15m);

                WriteSummary();

                if (_sw5m  != null) { _sw5m.Flush();  _sw5m.Close();  }
                if (_sw15m != null) { _sw15m.Flush(); _sw15m.Close(); }

                Print("[Level50v2] Done.");
            }
        }

        // ========================================================================
        protected override void OnBarUpdate()
        {
            // Only process on the 1-minute primary series
            if (BarsInProgress != 0) return;
            if (CurrentBars[0] < 2)  return;
            if (CurrentBars[1] < 2)  return;
            if (CurrentBars[2] < 2)  return;

            DateTime barTime = Time[0];
            double   barHigh = High[0];
            double   barLow  = Low[0];

            // ---- Detect 5m bar close -------------------------------------------
            // BarsArray[1].Count increases by 1 each time a new 5m bar opens,
            // meaning the previous 5m bar just closed.
            int cur5m = BarsArray[1].Count;
            if (cur5m > _prev5mCount && _prev5mCount > 0)
            {
                // The bar at index 1 on series 1 is the just-completed 5m bar
                // (index 0 = current forming bar, index 1 = last closed bar)
                int idx = 1;
                double o = BarsArray[1].GetOpen(BarsArray[1].Count - 1 - idx);
                double h = BarsArray[1].GetHigh(BarsArray[1].Count - 1 - idx);
                double l = BarsArray[1].GetLow( BarsArray[1].Count - 1 - idx);
                double c = BarsArray[1].GetClose(BarsArray[1].Count - 1 - idx);
                DateTime ot = BarsArray[1].GetTime(BarsArray[1].Count - 1 - idx);

                if (c > o && (h - l) > 0)
                {
                    _bull5m++;
                    double range = h - l;
                    _rng5m.Add(range);

                    // Close previous event if still open
                    if (_e5m != null && !_e5m.Done && _e5m.Touched)
                        Finalize(_e5m, "5m", _sw5m);

                    _e5m = new Event
                    {
                        CandleOpen = ot,
                        Open = o, High = h, Low = l, Close = c,
                        Range   = range,
                        Level50 = l + 0.50 * range,
                        Level25 = l + 0.25 * range
                    };
                }
            }
            _prev5mCount = cur5m;

            // ---- Detect 15m bar close ------------------------------------------
            int cur15m = BarsArray[2].Count;
            if (cur15m > _prev15mCount && _prev15mCount > 0)
            {
                int idx = 1;
                double o = BarsArray[2].GetOpen(BarsArray[2].Count - 1 - idx);
                double h = BarsArray[2].GetHigh(BarsArray[2].Count - 1 - idx);
                double l = BarsArray[2].GetLow( BarsArray[2].Count - 1 - idx);
                double c = BarsArray[2].GetClose(BarsArray[2].Count - 1 - idx);
                DateTime ot = BarsArray[2].GetTime(BarsArray[2].Count - 1 - idx);

                if (c > o && (h - l) > 0)
                {
                    _bull15m++;
                    double range = h - l;
                    _rng15m.Add(range);

                    if (_e15m != null && !_e15m.Done && _e15m.Touched)
                        Finalize(_e15m, "15m", _sw15m);

                    _e15m = new Event
                    {
                        CandleOpen = ot,
                        Open = o, High = h, Low = l, Close = c,
                        Range   = range,
                        Level50 = l + 0.50 * range,
                        Level25 = l + 0.25 * range
                    };
                }
            }
            _prev15mCount = cur15m;

            // ---- Monitor active events on this 1m bar --------------------------
            Monitor(ref _e5m,  "5m",  barHigh, barLow, barTime);
            Monitor(ref _e15m, "15m", barHigh, barLow, barTime);
        }

        // ========================================================================
        private void Monitor(ref Event ev, string tf,
            double barHigh, double barLow, DateTime barTime)
        {
            if (ev == null || ev.Done) return;

            if (!ev.Touched)
            {
                // Wait for price to retrace to Level50
                // Touch = this 1m bar's low is at or below Level50
                if (barLow <= ev.Level50)
                {
                    ev.Touched   = true;
                    ev.TouchTime = barTime;
                    ev.MaxAfter  = barHigh;
                    ev.MinAfter  = barLow;

                    if (tf == "5m") _touch5m++;
                    else            _touch15m++;

                    // Check outcome immediately on touch bar
                    Evaluate(ref ev, tf, barHigh, barLow, barTime);
                }
                return;
            }

            // Update extremes
            if (barHigh > ev.MaxAfter) ev.MaxAfter = barHigh;
            if (barLow  < ev.MinAfter) ev.MinAfter = barLow;

            Evaluate(ref ev, tf, barHigh, barLow, barTime);
        }

        private void Evaluate(ref Event ev, string tf,
            double barHigh, double barLow, DateTime barTime)
        {
            if (ev == null || ev.Done) return;

            bool hitHigh = barHigh > ev.High;
            bool hitL25  = barLow  <= ev.Level25;

            string outcome = null;
            if      (hitHigh && hitL25) outcome = "AMBIGUOUS";
            else if (hitHigh)           outcome = "UP";
            else if (hitL25)            outcome = "DOWN";

            if (outcome == null) return;

            ev.Done        = true;
            ev.Outcome     = outcome;
            ev.OutcomeTime = barTime;
            ev.Minutes     = (int)Math.Round((barTime - ev.TouchTime).TotalMinutes);

            Tally(tf, outcome, ev);
            WriteRow(tf == "5m" ? _sw5m : _sw15m, ev, tf);
            ev = null;
        }

        private void Finalize(Event ev, string tf, StreamWriter sw)
        {
            ev.Done        = true;
            ev.Outcome     = "UNRESOLVED";
            ev.OutcomeTime = DateTime.MinValue;
            ev.Minutes     = -1;
            Tally(tf, "UNRESOLVED", ev);
            WriteRow(sw, ev, tf);
        }

        // ========================================================================
        private void Tally(string tf, string outcome, Event ev)
        {
            if (tf == "5m")
            {
                switch (outcome)
                {
                    case "UP":         _up5m++;  if (ev.Minutes >= 0) _tUp5m.Add(ev.Minutes); break;
                    case "DOWN":       _dn5m++;  if (ev.Minutes >= 0) _tDn5m.Add(ev.Minutes); break;
                    case "AMBIGUOUS":  _amb5m++; break;
                    case "UNRESOLVED": _unr5m++; break;
                }
            }
            else
            {
                switch (outcome)
                {
                    case "UP":         _up15m++;  if (ev.Minutes >= 0) _tUp15m.Add(ev.Minutes); break;
                    case "DOWN":       _dn15m++;  if (ev.Minutes >= 0) _tDn15m.Add(ev.Minutes); break;
                    case "AMBIGUOUS":  _amb15m++; break;
                    case "UNRESOLVED": _unr15m++; break;
                }
            }
        }

        private void WriteRow(StreamWriter sw, Event ev, string tf)
        {
            if (sw == null || ev == null || !ev.Touched) return;

            string outTime = ev.OutcomeTime == DateTime.MinValue ? ""
                : ev.OutcomeTime.ToString("yyyy-MM-dd HH:mm:ss");
            string mins = ev.Minutes < 0 ? "" : ev.Minutes.ToString();
            string maxP = ev.MaxAfter == double.MinValue ? "" : ev.MaxAfter.ToString("F2");
            string minP = ev.MinAfter == double.MaxValue ? "" : ev.MinAfter.ToString("F2");

            sw.WriteLine(string.Join(",",
                ev.CandleOpen.ToString("yyyy-MM-dd"),
                ev.CandleOpen.ToString("HH:mm:ss"),
                Instrument.FullName,
                tf,
                ev.CandleOpen.ToString("yyyy-MM-dd HH:mm:ss"),
                ev.Open.ToString("F2"),
                ev.High.ToString("F2"),
                ev.Low.ToString("F2"),
                ev.Close.ToString("F2"),
                ev.Range.ToString("F2"),
                ev.Level50.ToString("F2"),
                ev.Level25.ToString("F2"),
                ev.TouchTime.ToString("yyyy-MM-dd HH:mm:ss"),
                ev.Outcome,
                outTime,
                mins,
                maxP,
                minP,
                _sessionName,
                "1-Minute"
            ));
            sw.Flush();
        }

        // ========================================================================
        private void WriteSummary()
        {
            string dir  = Path.GetDirectoryName(_csv5mPath);
            string path = Path.Combine(dir,
                "Level50Study_SUMMARY_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");

            using (var sw = new StreamWriter(path, false))
            {
                sw.WriteLine("=================================================================");
                sw.WriteLine("  NQ LEVEL50 CONTINUATION STUDY v2 -- SUMMARY REPORT");
                sw.WriteLine("  Generated : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sw.WriteLine("  Instrument: " + Instrument.FullName);
                sw.WriteLine("  Session   : " + _sessionName);
                sw.WriteLine("  Resolution: 1-Minute bars");
                sw.WriteLine("=================================================================");
                sw.WriteLine();

                Block(sw, "5-Minute",
                    _bull5m, _touch5m, _up5m, _dn5m, _amb5m, _unr5m,
                    _tUp5m, _tDn5m, _rng5m);
                sw.WriteLine();
                Block(sw, "15-Minute",
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

                string fmt = "{0,-40} {1,16} {2,16}";
                sw.WriteLine(string.Format(fmt, "Metric", "5-Minute", "15-Minute"));
                sw.WriteLine(new string('-', 74));
                sw.WriteLine(string.Format(fmt, "Bullish candle count",      _bull5m,  _bull15m));
                sw.WriteLine(string.Format(fmt, "Level50 pullback count",    _touch5m, _touch15m));
                sw.WriteLine(string.Format(fmt, "High-first rate",           hfr5.ToString("P2"),  hfr15.ToString("P2")));
                sw.WriteLine(string.Format(fmt, "Level25-first rate",        lfr5.ToString("P2"),  lfr15.ToString("P2")));
                sw.WriteLine(string.Format(fmt, "Ambiguous-event rate",      ar5.ToString("P2"),   ar15.ToString("P2")));
                sw.WriteLine(string.Format(fmt, "Unresolved-event rate",     ur5.ToString("P2"),   ur15.ToString("P2")));
                sw.WriteLine(string.Format(fmt, "Median time to High break",
                    _tUp5m.Count  > 0 ? Med(_tUp5m).ToString("F1")  + " min" : "N/A",
                    _tUp15m.Count > 0 ? Med(_tUp15m).ToString("F1") + " min" : "N/A"));
                sw.WriteLine(string.Format(fmt, "Median time to Level25 touch",
                    _tDn5m.Count  > 0 ? Med(_tDn5m).ToString("F1")  + " min" : "N/A",
                    _tDn15m.Count > 0 ? Med(_tDn15m).ToString("F1") + " min" : "N/A"));
                sw.WriteLine(string.Format(fmt, "Median candle range",
                    _rng5m.Count  > 0 ? Med(_rng5m).ToString("F2")  + " pts" : "N/A",
                    _rng15m.Count > 0 ? Med(_rng15m).ToString("F2") + " pts" : "N/A"));

                sw.WriteLine();
                sw.WriteLine("=================================================================");
                sw.WriteLine("  DECISION GUIDANCE");
                sw.WriteLine("=================================================================");
                string verdict;
                if      (hfr5 > hfr15 && v5  >= 30) verdict = "5-Minute shows higher High-first rate with adequate sample.";
                else if (hfr15 > hfr5 && v15 >= 30) verdict = "15-Minute shows higher High-first rate with adequate sample.";
                else if (v5 < 30 && v15 < 30)        verdict = "WARNING: Sample too small (<30). Extend date range.";
                else                                  verdict = "Rates are close. Apply all 5 criteria before deciding.";
                sw.WriteLine("  " + verdict);
                sw.WriteLine("  5 criteria: rate + sample + ambiguity + L25 rate + time-to-resolution");
                sw.WriteLine("=================================================================");
            }
            Print("[Level50v2] Summary saved.");
        }

        private void Block(StreamWriter sw, string label,
            int bull, int touch, int up, int dn, int amb, int unr,
            List<double> tUp, List<double> tDn, List<double> rngs)
        {
            int valid = up + dn;
            double hfr = valid > 0 ? (double)up  / valid : 0;
            double lfr = valid > 0 ? (double)dn  / valid : 0;
            double ar  = touch > 0 ? (double)amb / touch  : 0;
            double ur  = touch > 0 ? (double)unr / touch  : 0;

            sw.WriteLine("  -- " + label + " Candle Study --");
            sw.WriteLine("    Total bullish candles:        " + bull);
            sw.WriteLine("    Total Level50 pullbacks:      " + touch);
            sw.WriteLine("    UP:                           " + up);
            sw.WriteLine("    DOWN:                         " + dn);
            sw.WriteLine("    AMBIGUOUS:                    " + amb);
            sw.WriteLine("    UNRESOLVED:                   " + unr);
            sw.WriteLine("    Valid (UP+DOWN):              " + valid);
            sw.WriteLine("    High-first rate:              " + hfr.ToString("P2"));
            sw.WriteLine("    Level25-first rate:           " + lfr.ToString("P2"));
            sw.WriteLine("    Ambiguous rate:               " + ar.ToString("P2"));
            sw.WriteLine("    Unresolved rate:              " + ur.ToString("P2"));
            sw.WriteLine("    Median min to High break:     " + (tUp.Count  > 0 ? Med(tUp).ToString("F1")  : "N/A"));
            sw.WriteLine("    Avg min to High break:        " + (tUp.Count  > 0 ? Avg(tUp).ToString("F1")  : "N/A"));
            sw.WriteLine("    Median min to Level25 touch:  " + (tDn.Count  > 0 ? Med(tDn).ToString("F1")  : "N/A"));
            sw.WriteLine("    Avg min to Level25 touch:     " + (tDn.Count  > 0 ? Avg(tDn).ToString("F1")  : "N/A"));
            sw.WriteLine("    Median candle range (pts):    " + (rngs.Count > 0 ? Med(rngs).ToString("F2") : "N/A"));
            sw.WriteLine("    Avg candle range (pts):       " + (rngs.Count > 0 ? Avg(rngs).ToString("F2") : "N/A"));
        }

        private double Med(List<double> d)
        {
            if (d.Count == 0) return 0;
            var s = new List<double>(d); s.Sort();
            int m = s.Count / 2;
            return s.Count % 2 == 0 ? (s[m-1] + s[m]) / 2.0 : s[m];
        }
        private double Avg(List<double> d)
        {
            if (d.Count == 0) return 0;
            double t = 0; foreach (var v in d) t += v; return t / d.Count;
        }
    }
}
