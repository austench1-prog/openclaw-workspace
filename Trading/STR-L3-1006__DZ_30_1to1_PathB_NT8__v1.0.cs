// ============================================================
// STR-L3-1006 — DZ_30_1to1 Path B
// NinjaScript Strategy v1.0
// ============================================================
// Document ID   : STR-L3-1006
// Strategy ID   : DZ_30_1to1_PathB
// Version       : v1.0
// Based on spec : STR-L3-1006__DZ_30_1to1_PathB_NT8__v0.1.md
// Commit ref    : 31978ddabbb8060752f06a58d08c3b8a45d6df5e
// Author        : Dragon (OC)
// Date          : 2026-06-24
// Status        : Draft — Pending President Review
//
// PARAMETERS (locked by President 2026-06-24):
//   Touch Band      = 10 ticks  (Level50 ± 10 ticks = "touched")
//   Reclaim Offset  = 10 ticks  (Level50 + 10 ticks = reclaim confirmed)
//   Lower Excursion = any tick below Level50 (per spec §8 Step 3: "makes a qualifying lower excursion below Level50")
//   Immediate Rev   = touched Level50 (in band), never went below Level50, still above Level25
//   Tick evaluation = tick-by-tick (Calculate.OnEachTick)
//   Stop distance   = 30 points (code-internal, not ATM-dependent)
//   TP A distance   = 30 points (code-internal)
//   Level input     = SETUP|NQ|Level50|Level25 signal via signal.txt
//
// SIGNAL PROTOCOL (M2):
//   SETUP|NQ|<Level50>|<Level25>   — arms the strategy
//   FLATTEN_ALL                     — emergency close all
//
// THREE EXECUTION PATHS (per spec §9):
//   Scenario A — Invalid  : touch Level50 → Level25 hit → Path B dead → do nothing further
//   Scenario B — Standard : touch Level50 → excursion below Level50 (above Level25) → reclaim → add 3 → total 4 → TP A sell 2
//   Scenario C — Imm Rev  : touch Level50 → never below Level50 → still above Level25 → add 1 → total 2 → TP A sell all 2
//
// AUTHORIZATION BOUNDARY:
//   - Sim101 (virtual/sim account) ONLY.
//   - No live account. No Replikanto in this version.
//   - Do not enable on real account without separate President authorization.
// ============================================================

#region Using declarations
using System;
using System.IO;
using System.Timers;
using NinjaTrader.Cbi;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.Strategies;
#endregion

namespace NinjaTrader.NinjaScript.Strategies
{
    public class DZ_30_1to1_PathB : Strategy
    {
        // ── Signal file paths ──────────────────────────────────
        private const string SIGNAL_FOLDER   = @"C:\DragonSignals\";
        private const string SIGNAL_FILE     = "signal.txt";
        private const string DONE_FILE       = "signal_done.txt";
        private const int    POLL_MS         = 500;

        // ── Fixed strategy parameters ──────────────────────────
        private const double TICK_SIZE       = 0.25;   // NQ/MNQ: 1 point = 4 ticks
        private const double STOP_POINTS     = 30.0;
        private const double TPA_POINTS      = 30.0;
        private const int    TOUCH_BAND_TICKS  = 10;  // Level50 ± 10 ticks = "touched"
        private const int    RECLAIM_TICKS     = 10;  // Level50 + 10 ticks = reclaim confirmed
        private const int    INITIAL_QTY       = 1;
        private const int    STANDARD_ADD_QTY  = 3;
        private const int    IMM_REV_ADD_QTY   = 1;
        private const int    STANDARD_TPA_EXIT = 2;
        private const int    IMM_REV_TPA_EXIT  = 2;   // = all 2 lots

        // ── State machine ──────────────────────────────────────
        private enum PathBState
        {
            Idle,               // no SETUP received
            Armed,              // SETUP received, waiting for Level50 touch
            Entered,            // E1 fill done, watching for branch determination
            Standard_Expanding, // below Level50 happened, watching for reclaim
            Branch_Determined,  // in trade (standard or imm_rev), watching for TP A
            Invalid,            // Level25 hit — Path B dead this setup
            Complete            // TP A hit — Path B done
        }

        private PathBState _state        = PathBState.Idle;
        private double     _level50      = 0;
        private double     _level25      = 0;
        private double     _e1           = 0;
        private double     _stopPrice    = 0;
        private double     _tpAPrice     = 0;
        private bool       _wentBelowL50 = false;  // true once price ticked below Level50
        private bool       _isStandard   = false;   // true = standard 1+3, false = imm rev 1+1
        private int        _totalPosition = 0;
        private string     _lastSignal   = "";

        private Timer _timer;

        // ── Helpers ────────────────────────────────────────────
        private double TouchBandLow  => _level50 - TOUCH_BAND_TICKS * TICK_SIZE;
        private double TouchBandHigh => _level50 + TOUCH_BAND_TICKS * TICK_SIZE;
        private double ReclaimPrice  => _level50 + RECLAIM_TICKS * TICK_SIZE;

        // ── OnStateChange ──────────────────────────────────────
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description = "STR-L3-1006 DZ_30_1to1 Path B — Dragon v1.0";
                Name        = "DZ_30_1to1_PathB";
                Calculate   = Calculate.OnEachTick;
                IsOverlay   = false;
                BarsRequiredToTrade = 1;
            }
            else if (State == State.Configure)
            {
                if (!Directory.Exists(SIGNAL_FOLDER))
                    Directory.CreateDirectory(SIGNAL_FOLDER);
                Print("[PathB] Configured. Waiting for SETUP signal.");
            }
            else if (State == State.DataLoaded)
            {
                _timer          = new Timer(POLL_MS);
                _timer.Elapsed += OnTimerElapsed;
                _timer.AutoReset = true;
                _timer.Start();
                Print("[PathB] Timer started. Polling " + SIGNAL_FILE);
            }
            else if (State == State.Terminated)
            {
                if (_timer != null) { _timer.Stop(); _timer.Dispose(); }
                Print("[PathB] Terminated.");
            }
        }

        // ── Timer: poll signal file ─────────────────────────────
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                string sigPath  = SIGNAL_FOLDER + SIGNAL_FILE;
                string donePath = SIGNAL_FOLDER + DONE_FILE;

                if (!File.Exists(sigPath)) return;
                if (File.Exists(donePath)) return; // already handled

                string raw = File.ReadAllText(sigPath).Trim();
                if (raw == _lastSignal) return;
                _lastSignal = raw;

                // FLATTEN_ALL — emergency
                if (raw.ToUpper() == "FLATTEN_ALL")
                {
                    Print("[PathB] FLATTEN_ALL received — flattening.");
                    ResetState();
                    // NT8 flatten via Position close
                    if (Position.MarketPosition != MarketPosition.Flat)
                        ExitLong("FLATTEN_ALL", "", Position.Quantity);
                    File.WriteAllText(donePath, "FLATTEN_ALL");
                    return;
                }

                // SETUP|NQ|Level50|Level25
                var parts = raw.Split('|');
                if (parts.Length >= 4 &&
                    parts[0].ToUpper() == "SETUP" &&
                    parts[1].ToUpper() == "NQ")
                {
                    double l50, l25;
                    if (double.TryParse(parts[2], out l50) &&
                        double.TryParse(parts[3], out l25))
                    {
                        _level50 = l50;
                        _level25 = l25;
                        ResetState();
                        _state = PathBState.Armed;
                        Print(string.Format("[PathB] Armed. Level50={0} Level25={1} TouchBand=[{2},{3}] ReclaimAt={4}",
                            _level50, _level25, TouchBandLow, TouchBandHigh, ReclaimPrice));
                        File.WriteAllText(donePath, raw);
                    }
                }
            }
            catch (Exception ex)
            {
                Print("[PathB] Timer error: " + ex.Message);
            }
        }

        // ── OnBarUpdate (tick-by-tick) ──────────────────────────
        protected override void OnBarUpdate()
        {
            if (CurrentBar < BarsRequiredToTrade) return;

            double price = Close[0]; // tick-by-tick: Close[0] = current tick price

            switch (_state)
            {
                // ── ARMED: watch for Level50 touch ──────────────
                case PathBState.Armed:
                    // Level25 already breached before entry — path dead
                    if (price <= _level25)
                    {
                        Print(string.Format("[PathB] INVALID before entry — price {0} hit Level25 {1} while armed.", price, _level25));
                        _state = PathBState.Invalid;
                        break;
                    }
                    // Touch Level50 band
                    if (price >= TouchBandLow && price <= TouchBandHigh)
                    {
                        Print(string.Format("[PathB] Level50 touched at {0}. Entering 1 lot.", price));
                        EnterLong(INITIAL_QTY, "E1");
                        // E1 will be recorded in OnExecutionUpdate
                        _state = PathBState.Entered;
                    }
                    break;

                // ── ENTERED: wait for fill, then watch branch ───
                case PathBState.Entered:
                    // E1 set in OnExecutionUpdate; guard in case fill not yet confirmed
                    if (_e1 == 0) break;

                    // Step 2 Validity Gate
                    if (price <= _level25)
                    {
                        Print(string.Format("[PathB] INVALID — price {0} hit Level25 {1} after entry.", price, _level25));
                        _state = PathBState.Invalid;
                        // Keep position open — caller/President decides exit; or auto-exit on stop
                        break;
                    }

                    // Track whether price has gone below Level50 (qualifying excursion)
                    if (!_wentBelowL50 && price < _level50)
                    {
                        _wentBelowL50 = true;
                        Print(string.Format("[PathB] Lower excursion detected at {0}. Watching for reclaim at {1}.", price, ReclaimPrice));
                        _state = PathBState.Standard_Expanding;
                    }
                    else if (!_wentBelowL50)
                    {
                        // No excursion yet — could still be Immediate Reversal if price starts going up
                        // Check: if price has risen above ReclaimPrice WITHOUT ever going below Level50
                        if (price >= ReclaimPrice)
                        {
                            Print(string.Format("[PathB] Immediate Reversal confirmed at {0}. Adding 1 lot.", price));
                            EnterLong(IMM_REV_ADD_QTY, "E2_ImmRev");
                            _isStandard  = false;
                            _totalPosition = INITIAL_QTY + IMM_REV_ADD_QTY; // = 2
                            _state = PathBState.Branch_Determined;
                            SetStopAndTP();
                        }
                    }
                    break;

                // ── STANDARD EXPANDING: watch for reclaim ───────
                case PathBState.Standard_Expanding:
                    // Validity gate still active
                    if (price <= _level25)
                    {
                        Print(string.Format("[PathB] INVALID during excursion — price {0} hit Level25 {1}.", price, _level25));
                        _state = PathBState.Invalid;
                        break;
                    }
                    // Reclaim confirmed
                    if (price >= ReclaimPrice)
                    {
                        Print(string.Format("[PathB] Reclaim confirmed at {0}. Adding 3 lots.", price));
                        EnterLong(STANDARD_ADD_QTY, "E2_Standard");
                        _isStandard   = true;
                        _totalPosition = INITIAL_QTY + STANDARD_ADD_QTY; // = 4
                        _state = PathBState.Branch_Determined;
                        SetStopAndTP();
                    }
                    break;

                // ── BRANCH DETERMINED: watch TP A ───────────────
                case PathBState.Branch_Determined:
                    // Stop is managed by SetStopLoss below; TP A: manual watch
                    if (price >= _tpAPrice)
                    {
                        int exitQty = _isStandard ? STANDARD_TPA_EXIT : IMM_REV_TPA_EXIT;
                        Print(string.Format("[PathB] TP A reached at {0}. Exiting {1} lots.", price, exitQty));
                        ExitLong("TPA_Exit", "", exitQty);
                        _state = PathBState.Complete;
                        Print("[PathB] Path B complete. Remaining position leaves Path B scope.");
                    }
                    break;

                // ── INVALID / COMPLETE: do nothing ──────────────
                case PathBState.Invalid:
                case PathBState.Complete:
                case PathBState.Idle:
                    break;
            }
        }

        // ── Record E1 fill price ────────────────────────────────
        protected override void OnExecutionUpdate(
            Execution execution, string executionId, double price,
            int quantity, MarketPosition marketPosition,
            string orderId, DateTime time)
        {
            if (execution.Name == "E1" && _e1 == 0)
            {
                _e1         = execution.Price;
                _stopPrice  = _e1 - STOP_POINTS;
                _tpAPrice   = _e1 + TPA_POINTS;
                Print(string.Format("[PathB] E1 filled at {0}. Stop={1} TP_A={2}", _e1, _stopPrice, _tpAPrice));
                // Set initial stop loss on the 1-lot position
                SetStopLoss("E1", CalculationMode.Price, _stopPrice, false);
            }
        }

        // ── Apply Stop and TP to full position after add-on ─────
        private void SetStopAndTP()
        {
            // Apply stop to all open entries
            SetStopLoss(CalculationMode.Price, _stopPrice);
            // TP A is watched manually in OnBarUpdate (price >= _tpAPrice)
            // We do NOT set a profit target here — manual watch only,
            // because we exit partial qty (not all), which NT8 SetProfitTarget does not support natively.
            Print(string.Format("[PathB] Stop set at {0}, TP A at {1} (manual watch). Position={2} lots.",
                _stopPrice, _tpAPrice, _totalPosition));
        }

        // ── Reset all Path B state ──────────────────────────────
        private void ResetState()
        {
            _state          = PathBState.Idle;
            _level50        = 0;
            _level25        = 0;
            _e1             = 0;
            _stopPrice      = 0;
            _tpAPrice       = 0;
            _wentBelowL50   = false;
            _isStandard     = false;
            _totalPosition  = 0;
        }
    }
}
