// NinjaTrader 8 - Dragon File Signal Strategy v2
// 龙信 v2 — 内建止损/止盈，不依赖 ATM 模板
// Source: Dragon | Date: 2026-06-23
//
// 新增功能（vs v1）：
// - 信号格式支持 SL=点数 + TP=点数（相对点数，龙信自动换算绝对价格）
// - 进场后自动 SetStopLoss + SetProfitTarget
// - 支持4套参数：SL80/TP400(1:5), SL80/TP80(1:1), SL30/TP150(1:5), SL30/TP30(1:1)
//
// 信号格式：ACTION|SYMBOL|QTY|SL=点数|TP=点数
// 例：BUY|MNQ|4|SL=80|TP=400
//     SELL|MNQ|4|SL=30|TP=150
//     FLATTEN_ALL
//     CLOSE|MNQ|0
//
// 止损/TP 单位 = 点数（points）。MNQ: 1 point = 0.25 ticks × 4 = 1 pt
// 代码自动换算：points → NinjaTrader CalculationMode.Ticks

#region Using declarations
using System;
using System.IO;
using NinjaTrader.Cbi;
using NinjaTrader.NinjaScript.Strategies;
#endregion

namespace NinjaTrader.NinjaScript.Strategies
{
    public class DragonFileSignal : Strategy
    {
        private const string SIGNAL_FOLDER   = @"C:\DragonSignals\";
        private const string SIGNAL_FILE     = "signal.txt";
        private const string DONE_FILE       = "signal_done.txt";
        private const int    CHECK_INTERVAL_MS = 500;

        // MNQ tick size = 0.25 pt → 1 point = 4 ticks
        private const double TICK_SIZE       = 0.25;

        private System.Timers.Timer _timer;
        private string _lastProcessedSignal  = "";

        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description = "龙信 v2 — Dragon File Signal with built-in SL/TP (no ATM)";
                Name        = "DragonFileSignal";
                Calculate   = Calculate.OnBarClose;
                IsOverlay   = false;
            }
            else if (State == State.Configure)
            {
                if (!Directory.Exists(SIGNAL_FOLDER))
                    Directory.CreateDirectory(SIGNAL_FOLDER);
                Print($"龙信: Monitoring {SIGNAL_FOLDER}{SIGNAL_FILE}");
            }
            else if (State == State.DataLoaded)
            {
                _timer          = new System.Timers.Timer(CHECK_INTERVAL_MS);
                _timer.Elapsed += OnTimerElapsed;
                _timer.Start();
                Print("龙信: Timer started ✅");
            }
            else if (State == State.Terminated)
            {
                _timer?.Stop();
                _timer?.Dispose();
                Print("龙信: Stopped");
            }
        }

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string signalPath = Path.Combine(SIGNAL_FOLDER, SIGNAL_FILE);
            if (!File.Exists(signalPath)) return;

            try
            {
                string signal = File.ReadAllText(signalPath).Trim();
                if (signal == _lastProcessedSignal) return;

                Print($"龙信: Signal received → {signal}");

                // FLATTEN_ALL — 紧急平仓
                if (signal.ToUpper() == "FLATTEN_ALL")
                {
                    TriggerCustomEvent(s => { FlattenAll(); }, null);
                    ArchiveSignal(signalPath, signal);
                    return;
                }

                var parts = signal.ToUpper().Split('|');
                if (parts.Length < 3) { Print("龙信: Invalid signal format"); return; }

                string action = parts[0];
                string symbol = parts[1];
                int    qty    = int.Parse(parts[2]);

                double slPoints = 0;
                double tpPoints = 0;
                for (int i = 3; i < parts.Length; i++)
                {
                    if (parts[i].StartsWith("SL="))
                        double.TryParse(parts[i].Substring(3), out slPoints);
                    if (parts[i].StartsWith("TP="))
                        double.TryParse(parts[i].Substring(3), out tpPoints);
                }

                double sl = slPoints;
                double tp = tpPoints;

                TriggerCustomEvent(state =>
                {
                    ExecuteSignal(action, symbol, qty, sl, tp);
                }, null);

                ArchiveSignal(signalPath, signal);
            }
            catch (Exception ex)
            {
                Print($"龙信 ERROR: {ex.Message}");
            }
        }

        private void ExecuteSignal(string action, string symbol, int qty,
                                   double slPoints, double tpPoints)
        {
            Print($"龙信: Execute {action} {qty} {symbol} SL={slPoints}pt TP={tpPoints}pt");

            // 换算 points → ticks（MNQ 1pt = 4 ticks）
            double slTicks = slPoints / TICK_SIZE;
            double tpTicks = tpPoints / TICK_SIZE;

            switch (action)
            {
                case "BUY":
                    EnterLong(qty, "Dragon_B");
                    if (slTicks > 0)
                        SetStopLoss("Dragon_B", CalculationMode.Ticks, slTicks, false);
                    if (tpTicks > 0)
                        SetProfitTarget("Dragon_B", CalculationMode.Ticks, tpTicks);
                    Print($"龙信: LONG {qty}手 | SL {slPoints}pt | TP {tpPoints}pt ✅");
                    break;

                case "SELL":
                    EnterShort(qty, "Dragon_S");
                    if (slTicks > 0)
                        SetStopLoss("Dragon_S", CalculationMode.Ticks, slTicks, false);
                    if (tpTicks > 0)
                        SetProfitTarget("Dragon_S", CalculationMode.Ticks, tpTicks);
                    Print($"龙信: SHORT {qty}手 | SL {slPoints}pt | TP {tpPoints}pt ✅");
                    break;

                case "CLOSE":
                    if (Position.MarketPosition == MarketPosition.Long)
                    {
                        ExitLong("Dragon_Close");
                        Print("龙信: EXIT LONG ✅");
                    }
                    else if (Position.MarketPosition == MarketPosition.Short)
                    {
                        ExitShort("Dragon_Close");
                        Print("龙信: EXIT SHORT ✅");
                    }
                    else
                        Print("龙信: No position to close");
                    break;

                default:
                    Print($"龙信: Unknown action → {action}");
                    break;
            }
        }

        private void FlattenAll()
        {
            if (Position.MarketPosition == MarketPosition.Long)
                ExitLong("Dragon_Flatten");
            else if (Position.MarketPosition == MarketPosition.Short)
                ExitShort("Dragon_Flatten");
            Print("龙信: FLATTEN_ALL executed ✅");
        }

        private void ArchiveSignal(string signalPath, string signal)
        {
            _lastProcessedSignal = signal;
            string donePath = Path.Combine(SIGNAL_FOLDER, DONE_FILE);
            if (File.Exists(donePath)) File.Delete(donePath);
            File.Move(signalPath, donePath);
            Print("龙信: Signal archived ✅");
        }

        protected override void OnBarUpdate()
        {
            // 信号来自文件，此处不处理
        }
    }
}
