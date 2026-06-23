// NinjaTrader 8 - Dragon File Signal Strategy v3
// 法师 v3 — 实现 DZ_80_1to5 折扣区头寸策略
// Source: Dragon | Date: 2026-06-23
//
// 策略逻辑（总裁原创）：
// 1. 收到信号，挂限价单在50%线等回踩（第1手）
// 2. 第1手成交后，监测价格：
//    - 如果价格继续下探碰了25%线 → 加4手（路径A）
//    - 如果价格没碰25%直接回头 → 加3手（路径B）
// 3. 到1:1位置（80点盈利）→ 自动平一半仓位
// 4. 剩余仓位冲400点（1:5）→ 全部出场
// 止损：80点，全程固定
//
// 信号格式：
//   SETUP|NQ|50%价格|25%价格
//   例：SETUP|NQ|21000|20950
//   FLATTEN_ALL — 紧急平仓

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
        private const string SIGNAL_FOLDER     = @"C:\DragonSignals\";
        private const string SIGNAL_FILE       = "signal.txt";
        private const string DONE_FILE         = "signal_done.txt";
        private const int    CHECK_INTERVAL_MS = 500;

        // MNQ: 1 point = 4 ticks (tick size = 0.25)
        private const double TICK_SIZE  = 0.25;
        private const double SL_POINTS  = 80.0;
        private const double TP_POINTS  = 400.0;   // 1:5
        private const double MID_POINTS = 80.0;    // 1:1 卖一半位置

        // 策略状态
        private enum SetupState { Idle, WaitingEntry, InTrade_Phase1, InTrade_Phase2 }
        private SetupState _state = SetupState.Idle;

        private double _fiftyPct   = 0;  // 50%线价格（进场价）
        private double _twentyFive = 0;  // 25%线价格
        private double _slPrice    = 0;  // 止损绝对价格
        private double _tpPrice    = 0;  // TP绝对价格（400点）
        private double _midPrice   = 0;  // 中场价格（80点，卖一半）
        private bool   _hit25      = false; // 有没有碰过25%
        private bool   _midDone    = false; // 有没有卖过一半
        private int    _phase1Qty  = 0;  // 第1手成交数量

        private System.Timers.Timer _timer;
        private string _lastProcessedSignal = "";

        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description = "法师 v3 — DZ_80_1to5 折扣区头寸策略（1+3加仓，1:1卖一半，1:5全出）";
                Name        = "DragonFileSignal";
                Calculate   = Calculate.OnEachTick; // 需要逐tick监测价格
                IsOverlay   = false;
            }
            else if (State == State.Configure)
            {
                if (!Directory.Exists(SIGNAL_FOLDER))
                    Directory.CreateDirectory(SIGNAL_FOLDER);
                Print("法师v3: 初始化完成，等待 SETUP 信号...");
            }
            else if (State == State.DataLoaded)
            {
                _timer          = new System.Timers.Timer(CHECK_INTERVAL_MS);
                _timer.Elapsed += OnTimerElapsed;
                _timer.Start();
                Print("法师v3: 启动 ✅");
            }
            else if (State == State.Terminated)
            {
                _timer?.Stop();
                _timer?.Dispose();
                Print("法师v3: 停止");
            }
        }

        // ─── 文件监听 ────────────────────────────────────────────────
        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string signalPath = Path.Combine(SIGNAL_FOLDER, SIGNAL_FILE);
            if (!File.Exists(signalPath)) return;

            try
            {
                string signal = File.ReadAllText(signalPath).Trim();
                if (signal == _lastProcessedSignal) return;

                Print($"法师v3: 收到信号 → {signal}");

                if (signal.ToUpper() == "FLATTEN_ALL")
                {
                    TriggerCustomEvent(s => { DoFlatten(); }, null);
                    ArchiveSignal(signalPath, signal);
                    return;
                }

                var parts = signal.ToUpper().Split('|');
                if (parts.Length >= 3 && parts[0] == "SETUP")
                {
                    double fifty = 0, twenty5 = 0;
                    double.TryParse(parts[2], out fifty);
                    if (parts.Length >= 4)
                        double.TryParse(parts[3], out twenty5);

                    double f = fifty;
                    double t = twenty5;
                    TriggerCustomEvent(s => { StartSetup(f, t); }, null);
                    ArchiveSignal(signalPath, signal);
                    return;
                }

                Print($"法师v3: 未知信号格式 → {signal}");
                ArchiveSignal(signalPath, signal);
            }
            catch (Exception ex)
            {
                Print($"法师v3 ERROR: {ex.Message}");
            }
        }

        // ─── 开始 Setup ───────────────────────────────────────────────
        private void StartSetup(double fiftyPrice, double twentyFivePrice)
        {
            if (_state != SetupState.Idle)
            {
                Print("法师v3: 已有进行中的 Setup，忽略新信号");
                return;
            }

            _fiftyPct   = fiftyPrice;
            _twentyFive = twentyFivePrice;
            _hit25      = false;
            _midDone    = false;
            _phase1Qty  = 0;

            // 挂限价单在50%线（价格回踩才成交）
            EnterLongLimit(1, _fiftyPct, "DZ_Entry1");
            _state = SetupState.WaitingEntry;

            Print($"法师v3: Setup 开始 | 50%={_fiftyPct} | 25%={_twentyFive}");
            Print($"法师v3: 限价单已挂在 {_fiftyPct}，等待回踩...");
        }

        // ─── 逐tick监测：策略核心逻辑 ────────────────────────────────
        protected override void OnBarUpdate()
        {
            if (_state == SetupState.Idle) return;

            double price = Close[0];

            // 第1手成交后进入 Phase1
            if (_state == SetupState.WaitingEntry)
            {
                // 由 OnExecutionUpdate 处理，这里只监测25%
                return;
            }

            if (_state == SetupState.InTrade_Phase1)
            {
                // 监测有没有碰25%
                if (!_hit25 && _twentyFive > 0 && price <= _twentyFive)
                {
                    _hit25 = true;
                    Print($"法师v3: 碰到25%线 ({_twentyFive})！路径A — 加4手");
                    EnterLong(4, "DZ_Add4");

                    // 设置统一止损和TP
                    SetStopLoss("DZ_Add4", CalculationMode.Price, _slPrice, false);
                    SetProfitTarget("DZ_Add4", CalculationMode.Price, _tpPrice);
                }

                // 监测1:1中场点（卖一半）
                if (!_midDone && price >= _midPrice)
                {
                    _midDone = true;
                    int halfQty = Position.Quantity / 2;
                    if (halfQty > 0)
                    {
                        ExitLong(halfQty, "DZ_HalfOut", "");
                        Print($"法师v3: 到达1:1({_midPrice})，卖出{halfQty}手 ✅");
                        _state = SetupState.InTrade_Phase2;
                    }
                }
            }

            if (_state == SetupState.InTrade_Phase2)
            {
                // 剩余仓位等400点TP，由SetProfitTarget自动处理
                // 监测仓位是否已清零
                if (Position.MarketPosition == MarketPosition.Flat)
                {
                    Print("法师v3: 全部出场，Setup 完成 ✅");
                    ResetState();
                }
            }
        }

        // ─── 成交回调：第1手成交后设SL/TP，判断路径 ──────────────────
        protected override void OnExecutionUpdate(Execution execution,
            string executionId, double price, int quantity,
            MarketPosition marketPosition, string orderId, DateTime time)
        {
            if (execution.Name == "DZ_Entry1" && marketPosition == MarketPosition.Long)
            {
                _phase1Qty = quantity;
                _slPrice   = price - SL_POINTS;
                _tpPrice   = price + TP_POINTS;
                _midPrice  = price + MID_POINTS;

                // 给第1手设止损和TP
                SetStopLoss("DZ_Entry1", CalculationMode.Price, _slPrice, false);
                SetProfitTarget("DZ_Entry1", CalculationMode.Price, _tpPrice);

                _state = SetupState.InTrade_Phase1;

                Print($"法师v3: 第1手成交 @ {price} ✅");
                Print($"法师v3: SL={_slPrice} | MID={_midPrice} | TP={_tpPrice}");
                Print($"法师v3: 等待市场决定路径A(碰{_twentyFive})或路径B(直接回头)...");

                // 路径B：没碰25%直接回头 → 价格回到入场价以上时加3手
                // 用一个高于入场价的限价买单触发
                double pathBTrigger = price + (SL_POINTS * 0.25); // 入场价+20点确认回头
                EnterLongLimit(3, pathBTrigger, "DZ_PathB_Add3");
                Print($"法师v3: 路径B触发价挂在 {pathBTrigger}（未碰25%时回头确认）");
            }

            // 路径B加3手成交
            if (execution.Name == "DZ_PathB_Add3" && !_hit25)
            {
                SetStopLoss("DZ_PathB_Add3", CalculationMode.Price, _slPrice, false);
                SetProfitTarget("DZ_PathB_Add3", CalculationMode.Price, _tpPrice);
                Print($"法师v3: 路径B — 加3手成交 @ {price}，手上共{Position.Quantity}手 ✅");
            }
        }

        // ─── 紧急平仓 ─────────────────────────────────────────────────
        private void DoFlatten()
        {
            if (Position.MarketPosition == MarketPosition.Long)
                ExitLong("DZ_Flatten");
            else if (Position.MarketPosition == MarketPosition.Short)
                ExitShort("DZ_Flatten");

            // 取消所有挂单
            CancelOrder(GetOrderByName("DZ_Entry1"));
            CancelOrder(GetOrderByName("DZ_PathB_Add3"));

            ResetState();
            Print("法师v3: FLATTEN_ALL 执行 ✅");
        }

        private void ResetState()
        {
            _state      = SetupState.Idle;
            _fiftyPct   = 0;
            _twentyFive = 0;
            _slPrice    = 0;
            _tpPrice    = 0;
            _midPrice   = 0;
            _hit25      = false;
            _midDone    = false;
            _phase1Qty  = 0;
        }

        private void ArchiveSignal(string signalPath, string signal)
        {
            _lastProcessedSignal = signal;
            string donePath = Path.Combine(SIGNAL_FOLDER, DONE_FILE);
            if (File.Exists(donePath)) File.Delete(donePath);
            File.Move(signalPath, donePath);
        }

        // GetOrderByName 辅助（NT8没有内置，用try/catch保护）
        private Order GetOrderByName(string name)
        {
            try
            {
                foreach (var o in Orders)
                    if (o.Name == name) return o;
            }
            catch { }
            return null;
        }
    }
}
