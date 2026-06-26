// =============================================================================
// Level50ContinuationStudy.cs
// NQ Level50 Continuation Event Study
// Version: 3.0 | 2026-06-25 | Dragon
//
// FIX v3: Use Closes[1][0]/Opens[1][0] etc. (NT8 official multi-series access)
//         and CurrentBars[1] increment to detect HTF bar close.
//         Compatible with both Strategy Analyzer and live chart.
//         NO orders. Observation only.
//
// INSTALL: NT8 Tools -> Edit NinjaScript -> Strategy -> paste -> F5/Compile
// RUN:     Strategy Analyzer, NQ SEP26, 1 Min, Infinite, 01/01/2026-06/24/2026
// OUTPUT:  Documents\NinjaTrader 8\csv\Level50Study_*.csv
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
        // ---- CSV output -------------------------------------------------------
        private StreamWriter _sw5m;
        private StreamWriter _sw15m;
        private string _csv5mPath;
        private string _csv15mPath;
        private string _session;

        // ---- HTF bar close detection -----------------------------------------
        // Track previous CurrentBars[1] and CurrentBars[2]
        // When they increment, the previous HTF bar just closed
        private int _prev5mBar  = -1;
        private int _prev15mBar = -1;

        // ---- Active monitoring event -----------------------------------------
        private class Evt
        {
            public DateTime CandleOpen;
            public double   O, H, L, C, Range, L50, L25;
            public bool     Touched;
            public DateTime TouchTime;
            public double   MaxAfter = double.MinValue;
            public double   MinAfter = double.MaxValue;
            public bool     Done;
            public string   Outcome;
            public DateTime OutcomeTime;
            public int      Mins;
        }

        private Evt _e5m  = null;
        private Evt _e15m = null;

        // ---- Counters --------------------------------------------------------
        private int _bull5, _bull15, _touch5, _touch15;
        private int _up5, _dn5, _amb5, _unr5;
        private int _up15, _dn15, _amb15, _unr15;
        private List<double> _tUp5 = new List<double>(), _tDn5 = new List<double>();
        private List<double> _tUp15 = new List<double>(), _tDn15 = new List<double>();
        private List<double> _rng5 = new List<double>(), _rng15 = new List<double>();

        // =====================================================================
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description  = "NQ Level50 Continuation Study v3 - no orders";
                Name         = "Level50ContinuationStudy";
                Calculate    = Calculate.OnBarClose;
                IsOverlay    = false;
                IsAutoScale  = false;
                EntriesPerDirection          = 1;
                EntryHandling                = EntryHandling.AllEntries;
                IsExitOnSessionCloseStrategy = false;
                BarsRequiredToTrade          = 5;
            }
            else if (State == State.Configure)
            {
                // BarsArray[1] = 5-min, BarsArray[2] = 15-min
                AddDataSeries(BarsPeriodType.Minute, 5);
                AddDataSeries(BarsPeriodType.Minute, 15);
            }
            else if (State == State.DataLoaded)
            {
                string dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "NinjaTrader 8", "csv");
                Directory.CreateDirectory(dir);

                string ts   = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                _csv5mPath  = Path.Combine(dir, "Level50Study_5m_"  + ts + ".csv");
                _csv15mPath = Path.Combine(dir, "Level50Study_15m_" + ts + ".csv");

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

                try   { _session = TradingHours.Name; }
                catch { _session = "Default"; }

                Print("[L50v3] Ready. Dir=" + dir);
            }
            else if (State == State.Terminated)
            {
                if (_e5m  != null && !_e5m.Done  && _e5m.Touched)  Close(_e5m,  "5m",  _sw5m);
                if (_e15m != null && !_e15m.Done && _e15m.Touched) Close(_e15m, "15m", _sw15m);
                WriteSummary();
                if (_sw5m  != null) { _sw5m.Flush();  _sw5m.Close();  }
                if (_sw15m != null) { _sw15m.Flush(); _sw15m.Close(); }
                Print("[L50v3] Done.");
            }
        }

        // =====================================================================
        protected override void OnBarUpdate()
        {
            // Only process primary 1-min series
            if (BarsInProgress != 0) return;
            if (CurrentBars[0] < 2) return;
            if (CurrentBars[1] < 2) return;
            if (CurrentBars[2] < 2) return;

            // ---- Detect 5m bar close ----------------------------------------
            // When CurrentBars[1] increases, the HTF bar at index [1][1] just closed.
            // NT8 multi-series access: Opens[1][n], Highs[1][n], etc.
            // Index [1][0] = current (forming) HTF bar
            // Index [1][1] = last CLOSED HTF bar
            int cur5  = CurrentBars[1];
            int cur15 = CurrentBars[2];

            if (cur5 != _prev5mBar && _prev5mBar >= 0)
            {
                double o = Opens[1][1];
                double h = Highs[1][1];
                double l = Lows[1][1];
                double c = Closes[1][1];
                DateTime ot = Times[1][1];

                if (c > o && (h - l) > 0)
                {
                    _bull5++;
                    double rng = h - l;
                    _rng5.Add(rng);

                    if (_e5m != null && !_e5m.Done && _e5m.Touched)
                        Close(_e5m, "5m", _sw5m);

                    _e5m = new Evt
                    {
                        CandleOpen = ot,
                        O = o, H = h, L = l, C = c,
                        Range = rng,
                        L50   = l + 0.50 * rng,
                        L25   = l + 0.25 * rng
                    };
                }
            }
            _prev5mBar = cur5;

            if (cur15 != _prev15mBar && _prev15mBar >= 0)
            {
                double o = Opens[2][1];
                double h = Highs[2][1];
                double l = Lows[2][1];
                double c = Closes[2][1];
                DateTime ot = Times[2][1];

                if (c > o && (h - l) > 0)
                {
                    _bull15++;
                    double rng = h - l;
                    _rng15.Add(rng);

                    if (_e15m != null && !_e15m.Done && _e15m.Touched)
                        Close(_e15m, "15m", _sw15m);

                    _e15m = new Evt
                    {
                        CandleOpen = ot,
                        O = o, H = h, L = l, C = c,
                        Range = rng,
                        L50   = l + 0.50 * rng,
                        L25   = l + 0.25 * rng
                    };
                }
            }
            _prev15mBar = cur15;

            // ---- Monitor active events on this 1m bar -----------------------
            double bH = High[0];
            double bL = Low[0];
            DateTime bT = Time[0];

            Monitor(ref _e5m,  "5m",  bH, bL, bT);
            Monitor(ref _e15m, "15m", bH, bL, bT);
        }

        // =====================================================================
        private void Monitor(ref Evt ev, string tf, double bH, double bL, DateTime bT)
        {
            if (ev == null || ev.Done) return;

            if (!ev.Touched)
            {
                if (bL <= ev.L50)
                {
                    ev.Touched   = true;
                    ev.TouchTime = bT;
                    ev.MaxAfter  = bH;
                    ev.MinAfter  = bL;
                    if (tf == "5m") _touch5++; else _touch15++;
                    Check(ref ev, tf, bH, bL, bT);
                }
                return;
            }

            if (bH > ev.MaxAfter) ev.MaxAfter = bH;
            if (bL < ev.MinAfter) ev.MinAfter = bL;
            Check(ref ev, tf, bH, bL, bT);
        }

        private void Check(ref Evt ev, string tf, double bH, double bL, DateTime bT)
        {
            if (ev == null || ev.Done) return;

            bool hitH  = bH > ev.H;
            bool hitL25 = bL <= ev.L25;

            string outcome = null;
            if      (hitH && hitL25) outcome = "AMBIGUOUS";
            else if (hitH)           outcome = "UP";
            else if (hitL25)         outcome = "DOWN";

            if (outcome == null) return;

            ev.Done        = true;
            ev.Outcome     = outcome;
            ev.OutcomeTime = bT;
            ev.Mins        = (int)Math.Round((bT - ev.TouchTime).TotalMinutes);

            Tally(tf, outcome, ev);
            WriteRow(tf == "5m" ? _sw5m : _sw15m, ev, tf);
            ev = null;
        }

        private void Close(Evt ev, string tf, StreamWriter sw)
        {
            ev.Done = true; ev.Outcome = "UNRESOLVED";
            ev.OutcomeTime = DateTime.MinValue; ev.Mins = -1;
            Tally(tf, "UNRESOLVED", ev);
            WriteRow(sw, ev, tf);
        }

        private void Tally(string tf, string outcome, Evt ev)
        {
            if (tf == "5m") {
                switch (outcome) {
                    case "UP":         _up5++;  if (ev.Mins>=0) _tUp5.Add(ev.Mins);  break;
                    case "DOWN":       _dn5++;  if (ev.Mins>=0) _tDn5.Add(ev.Mins);  break;
                    case "AMBIGUOUS":  _amb5++; break;
                    case "UNRESOLVED": _unr5++; break;
                }
            } else {
                switch (outcome) {
                    case "UP":         _up15++;  if (ev.Mins>=0) _tUp15.Add(ev.Mins);  break;
                    case "DOWN":       _dn15++;  if (ev.Mins>=0) _tDn15.Add(ev.Mins);  break;
                    case "AMBIGUOUS":  _amb15++; break;
                    case "UNRESOLVED": _unr15++; break;
                }
            }
        }

        private void WriteRow(StreamWriter sw, Evt ev, string tf)
        {
            if (sw == null || ev == null || !ev.Touched) return;
            string ot  = ev.OutcomeTime == DateTime.MinValue ? "" : ev.OutcomeTime.ToString("yyyy-MM-dd HH:mm:ss");
            string min = ev.Mins < 0 ? "" : ev.Mins.ToString();
            string mx  = ev.MaxAfter == double.MinValue ? "" : ev.MaxAfter.ToString("F2");
            string mn  = ev.MinAfter == double.MaxValue ? "" : ev.MinAfter.ToString("F2");
            sw.WriteLine(string.Join(",",
                ev.CandleOpen.ToString("yyyy-MM-dd"), ev.CandleOpen.ToString("HH:mm:ss"),
                Instrument.FullName, tf,
                ev.CandleOpen.ToString("yyyy-MM-dd HH:mm:ss"),
                ev.O.ToString("F2"), ev.H.ToString("F2"), ev.L.ToString("F2"),
                ev.C.ToString("F2"), ev.Range.ToString("F2"),
                ev.L50.ToString("F2"), ev.L25.ToString("F2"),
                ev.Touched ? ev.TouchTime.ToString("yyyy-MM-dd HH:mm:ss") : "",
                ev.Outcome, ot, min, mx, mn, _session, "1-Minute"));
            sw.Flush();
        }

        // =====================================================================
        private void WriteSummary()
        {
            string dir  = Path.GetDirectoryName(_csv5mPath);
            string path = Path.Combine(dir, "Level50Study_SUMMARY_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");
            using (var sw = new StreamWriter(path, false))
            {
                sw.WriteLine("=================================================================");
                sw.WriteLine("  NQ LEVEL50 CONTINUATION STUDY v3 -- SUMMARY");
                sw.WriteLine("  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sw.WriteLine("  " + Instrument.FullName + " | " + _session);
                sw.WriteLine("=================================================================");
                sw.WriteLine();
                Block(sw, "5-Minute",  _bull5,  _touch5,  _up5,  _dn5,  _amb5,  _unr5,  _tUp5,  _tDn5,  _rng5);
                sw.WriteLine();
                Block(sw, "15-Minute", _bull15, _touch15, _up15, _dn15, _amb15, _unr15, _tUp15, _tDn15, _rng15);

                int v5  = _up5 + _dn5, v15 = _up15 + _dn15;
                double hfr5  = v5  > 0 ? (double)_up5  / v5  : 0;
                double hfr15 = v15 > 0 ? (double)_up15 / v15 : 0;
                double lfr5  = v5  > 0 ? (double)_dn5  / v5  : 0;
                double lfr15 = v15 > 0 ? (double)_dn15 / v15 : 0;
                double ar5   = _touch5  > 0 ? (double)_amb5  / _touch5  : 0;
                double ar15  = _touch15 > 0 ? (double)_amb15 / _touch15 : 0;
                double ur5   = _touch5  > 0 ? (double)_unr5  / _touch5  : 0;
                double ur15  = _touch15 > 0 ? (double)_unr15 / _touch15 : 0;

                sw.WriteLine();
                sw.WriteLine("=================================================================");
                sw.WriteLine("  COMPARISON TABLE");
                sw.WriteLine("=================================================================");
                string f = "{0,-42} {1,14} {2,14}";
                sw.WriteLine(string.Format(f, "Metric", "5-Minute", "15-Minute"));
                sw.WriteLine(new string('-', 72));
                sw.WriteLine(string.Format(f, "Bullish candles",          _bull5,              _bull15));
                sw.WriteLine(string.Format(f, "Level50 pullbacks",        _touch5,             _touch15));
                sw.WriteLine(string.Format(f, "High-first rate",          hfr5.ToString("P2"), hfr15.ToString("P2")));
                sw.WriteLine(string.Format(f, "Level25-first rate",       lfr5.ToString("P2"), lfr15.ToString("P2")));
                sw.WriteLine(string.Format(f, "Ambiguous rate",           ar5.ToString("P2"),  ar15.ToString("P2")));
                sw.WriteLine(string.Format(f, "Unresolved rate",          ur5.ToString("P2"),  ur15.ToString("P2")));
                sw.WriteLine(string.Format(f, "Median min to High break",
                    _tUp5.Count  > 0 ? Med(_tUp5).ToString("F1")  + " min" : "N/A",
                    _tUp15.Count > 0 ? Med(_tUp15).ToString("F1") + " min" : "N/A"));
                sw.WriteLine(string.Format(f, "Median min to L25 touch",
                    _tDn5.Count  > 0 ? Med(_tDn5).ToString("F1")  + " min" : "N/A",
                    _tDn15.Count > 0 ? Med(_tDn15).ToString("F1") + " min" : "N/A"));
                sw.WriteLine(string.Format(f, "Median candle range",
                    _rng5.Count  > 0 ? Med(_rng5).ToString("F2")  + " pts" : "N/A",
                    _rng15.Count > 0 ? Med(_rng15).ToString("F2") + " pts" : "N/A"));
                sw.WriteLine();
                sw.WriteLine("=================================================================");
                string verdict;
                if      (hfr5 > hfr15 && v5  >= 30) verdict = "5-Minute shows higher High-first rate with adequate sample.";
                else if (hfr15 > hfr5 && v15 >= 30) verdict = "15-Minute shows higher High-first rate with adequate sample.";
                else if (v5 < 30 && v15 < 30)        verdict = "WARNING: Sample < 30. Extend date range.";
                else                                  verdict = "Rates close. Apply all 5 criteria before selecting timeframe.";
                sw.WriteLine("  " + verdict);
                sw.WriteLine("=================================================================");
            }
            Print("[L50v3] Summary saved.");
        }

        private void Block(StreamWriter sw, string lbl,
            int bull, int touch, int up, int dn, int amb, int unr,
            List<double> tUp, List<double> tDn, List<double> rngs)
        {
            int v = up + dn;
            sw.WriteLine("  -- " + lbl + " --");
            sw.WriteLine("    Bullish candles:     " + bull);
            sw.WriteLine("    Level50 pullbacks:   " + touch);
            sw.WriteLine("    UP:                  " + up);
            sw.WriteLine("    DOWN:                " + dn);
            sw.WriteLine("    AMBIGUOUS:           " + amb);
            sw.WriteLine("    UNRESOLVED:          " + unr);
            sw.WriteLine("    Valid (UP+DOWN):      " + v);
            sw.WriteLine("    High-first rate:     " + (v > 0 ? ((double)up/v).ToString("P2") : "N/A"));
            sw.WriteLine("    L25-first rate:      " + (v > 0 ? ((double)dn/v).ToString("P2") : "N/A"));
            sw.WriteLine("    Median min to High:  " + (tUp.Count > 0 ? Med(tUp).ToString("F1") : "N/A"));
            sw.WriteLine("    Median min to L25:   " + (tDn.Count > 0 ? Med(tDn).ToString("F1") : "N/A"));
            sw.WriteLine("    Median range (pts):  " + (rngs.Count > 0 ? Med(rngs).ToString("F2") : "N/A"));
        }

        private double Med(List<double> d)
        {
            if (d.Count == 0) return 0;
            var s = new List<double>(d); s.Sort();
            int m = s.Count / 2;
            return s.Count % 2 == 0 ? (s[m-1]+s[m])/2.0 : s[m];
        }
    }
}
