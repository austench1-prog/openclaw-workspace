// NinjaTrader 8 - File Signal Strategy
// Source: Dragon
// Version: 1.0 | Date: 2026-04-05
//
// INSTALL: NinjaScript Editor → Strategies → New Strategy → paste this code
// This strategy monitors a folder for signal files from Mac mini
// Signal file format: ACTION|SYMBOL|QTY  (e.g. BUY|NQ|1)
// File name: signal.txt (overwritten each time)
//
// HOW IT WORKS:
// 1. Mac mini writes "BUY|MNQ|1" to the shared folder signal.txt
// 2. This strategy detects the new file, reads it, executes the order
// 3. File is renamed to signal_done.txt after execution

#region Using declarations
using System;
using System.IO;
using System.Threading;
using NinjaTrader.Cbi;
using NinjaTrader.NinjaScript.Strategies;
#endregion

namespace NinjaTrader.NinjaScript.Strategies
{
    public class DragonFileSignal : Strategy
    {
        // *** UPDATE THIS PATH to your shared folder ***
        private const string SIGNAL_FOLDER = @"C:\DragonSignals\";
        private const string SIGNAL_FILE = "signal.txt";
        private const string DONE_FILE = "signal_done.txt";
        private const int CHECK_INTERVAL_MS = 500; // check every 500ms

        private System.Timers.Timer _timer;
        private string _lastProcessedSignal = "";

        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description = "Dragon File Signal Strategy - receives signals from Mac mini via shared folder";
                Name = "DragonFileSignal";
                Calculate = Calculate.OnBarClose;
                IsOverlay = false;
            }
            else if (State == State.Configure)
            {
                // Ensure signal folder exists
                if (!Directory.Exists(SIGNAL_FOLDER))
                {
                    Directory.CreateDirectory(SIGNAL_FOLDER);
                    Print($"DragonFileSignal: Created folder {SIGNAL_FOLDER}");
                }
                Print($"DragonFileSignal: Monitoring {SIGNAL_FOLDER}{SIGNAL_FILE}");
            }
            else if (State == State.DataLoaded)
            {
                // Start file monitoring timer
                _timer = new System.Timers.Timer(CHECK_INTERVAL_MS);
                _timer.Elapsed += OnTimerElapsed;
                _timer.Start();
                Print("DragonFileSignal: Timer started, watching for signals...");
            }
            else if (State == State.Terminated)
            {
                _timer?.Stop();
                _timer?.Dispose();
                Print("DragonFileSignal: Stopped");
            }
        }

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string signalPath = Path.Combine(SIGNAL_FOLDER, SIGNAL_FILE);
            
            if (!File.Exists(signalPath)) return;
            
            try
            {
                string signal = File.ReadAllText(signalPath).Trim();
                
                // Skip if already processed
                if (signal == _lastProcessedSignal) return;
                
                Print($"DragonFileSignal: New signal received: {signal}");
                
                // Parse signal: ACTION|SYMBOL|QTY
                var parts = signal.ToUpper().Split('|');
                if (parts.Length >= 3)
                {
                    string action = parts[0];
                    string symbol = parts[1];
                    int qty = int.Parse(parts[2]);
                    
                    // Execute on main thread
                    TriggerCustomEvent(state =>
                    {
                        ExecuteSignal(action, symbol, qty);
                    }, null);
                    
                    _lastProcessedSignal = signal;
                    
                    // Rename file to mark as done
                    string donePath = Path.Combine(SIGNAL_FOLDER, DONE_FILE);
                    if (File.Exists(donePath)) File.Delete(donePath);
                    File.Move(signalPath, donePath);
                    
                    Print($"DragonFileSignal: Signal processed and archived");
                }
            }
            catch (Exception ex)
            {
                Print($"DragonFileSignal ERROR: {ex.Message}");
            }
        }

        private void ExecuteSignal(string action, string symbol, int qty)
        {
            Print($"DragonFileSignal: Executing {action} {qty} {symbol}");
            
            switch (action)
            {
                case "BUY":
                    EnterLong(qty, "Dragon_Buy");
                    break;
                case "SELL":
                    EnterShort(qty, "Dragon_Sell");
                    break;
                case "CLOSE":
                    if (Position.MarketPosition == MarketPosition.Long)
                        ExitLong("Dragon_Close");
                    else if (Position.MarketPosition == MarketPosition.Short)
                        ExitShort("Dragon_Close");
                    break;
                default:
                    Print($"DragonFileSignal: Unknown action {action}");
                    break;
            }
        }

        protected override void OnBarUpdate()
        {
            // Required but not used - signals come from file watcher
        }
    }
}
