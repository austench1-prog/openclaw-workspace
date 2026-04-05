// NinjaTrader 8 HTTP Signal Receiver
// Source: Dragon
// Version: 1.0 | Date: 2026-04-05
//
// INSTALL INSTRUCTIONS:
// 1. Open NinjaTrader 8
// 2. Tools → Edit NinjaScript → Add-On
// 3. Paste this code, compile
// 4. The HTTP listener will start automatically
//
// This listens on port 5000 for signals from Mac mini
// Supported actions: BUY, SELL, CLOSE
// Supported instruments: NQ, MNQ, ES, MES, GC, MGC

using System;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using NinjaTrader.Cbi;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.NinjaScript.AddOns
{
    public class DragonSignalReceiver : NinjaTrader.NinjaScript.AddOnBase
    {
        private HttpListener _listener;
        private Thread _listenerThread;
        private const int PORT = 5000;
        
        // Account to use for orders
        private const string ACCOUNT_NAME = "Sim101"; // Change to your account name
        
        // Instrument map
        private Dictionary<string, string> _instruments = new Dictionary<string, string>
        {
            {"NQ",  "NQ 06-25"},
            {"MNQ", "MNQ 06-25"},
            {"ES",  "ES 06-25"},
            {"MES", "MES 06-25"},
            {"GC",  "GC 06-25"},
            {"MGC", "MGC 06-25"}
        };

        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description = "Dragon Signal Receiver - HTTP listener for AI trading signals";
                Name = "DragonSignalReceiver";
            }
            else if (State == State.Active)
            {
                StartListener();
                Print("DragonSignalReceiver: Started on port " + PORT);
            }
            else if (State == State.Terminated)
            {
                StopListener();
            }
        }

        private void StartListener()
        {
            try
            {
                _listener = new HttpListener();
                _listener.Prefixes.Add($"http://localhost:{PORT}/");
                _listener.Prefixes.Add($"http://+:{PORT}/");
                _listener.Start();
                
                _listenerThread = new Thread(ListenForRequests);
                _listenerThread.IsBackground = true;
                _listenerThread.Start();
            }
            catch (Exception ex)
            {
                Print("DragonSignalReceiver ERROR starting: " + ex.Message);
            }
        }

        private void StopListener()
        {
            try
            {
                _listener?.Stop();
                _listenerThread?.Abort();
            }
            catch { }
        }

        private void ListenForRequests()
        {
            while (_listener != null && _listener.IsListening)
            {
                try
                {
                    var context = _listener.GetContext();
                    ThreadPool.QueueUserWorkItem(ProcessRequest, context);
                }
                catch (Exception ex)
                {
                    if (_listener != null && _listener.IsListening)
                        Print("DragonSignalReceiver ERROR: " + ex.Message);
                }
            }
        }

        private void ProcessRequest(object obj)
        {
            var context = (HttpListenerContext)obj;
            string response = "ERROR";
            
            try
            {
                // Read request body
                string body = "";
                using (var reader = new StreamReader(context.Request.InputStream))
                    body = reader.ReadToEnd();
                
                Print($"DragonSignalReceiver: Received signal: {body}");
                
                // Parse signal: FORMAT = ACTION|SYMBOL|QTY
                // Example: BUY|NQ|1  or  SELL|MNQ|2  or  CLOSE|ES|0
                var parts = body.Trim().ToUpper().Split('|');
                
                if (parts.Length >= 3)
                {
                    string action = parts[0];   // BUY / SELL / CLOSE
                    string symbol = parts[1];   // NQ / MNQ / ES etc
                    int qty = int.Parse(parts[2]);
                    
                    // Validate
                    if (!_instruments.ContainsKey(symbol))
                    {
                        response = $"ERROR: Unknown symbol {symbol}";
                        Print(response);
                    }
                    else
                    {
                        string fullSymbol = _instruments[symbol];
                        ExecuteOrder(action, fullSymbol, qty);
                        response = $"OK: {action} {qty} {symbol}";
                        Print($"DragonSignalReceiver: Executed {response}");
                    }
                }
                else
                {
                    response = "ERROR: Invalid format. Use ACTION|SYMBOL|QTY";
                }
            }
            catch (Exception ex)
            {
                response = "ERROR: " + ex.Message;
                Print("DragonSignalReceiver ERROR processing: " + ex.Message);
            }
            
            // Send response
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(response);
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
        }

        private void ExecuteOrder(string action, string fullSymbol, int qty)
        {
            // Get account
            Account account = null;
            foreach (var acc in Account.All)
            {
                if (acc.Name == ACCOUNT_NAME)
                {
                    account = acc;
                    break;
                }
            }
            
            if (account == null)
            {
                Print($"DragonSignalReceiver: Account {ACCOUNT_NAME} not found!");
                return;
            }
            
            // Get instrument
            Instrument instrument = Instrument.GetInstrument(fullSymbol);
            if (instrument == null)
            {
                Print($"DragonSignalReceiver: Instrument {fullSymbol} not found!");
                return;
            }
            
            OrderAction orderAction;
            switch (action)
            {
                case "BUY":
                    orderAction = OrderAction.Buy;
                    break;
                case "SELL":
                    orderAction = OrderAction.Sell;
                    break;
                case "CLOSE":
                    // Close all positions
                    account.Flatten(new[] { instrument }, OrderType.Market, 0, 0, "", "Dragon_Close");
                    Print($"DragonSignalReceiver: Closed all {fullSymbol} positions");
                    return;
                default:
                    Print($"DragonSignalReceiver: Unknown action {action}");
                    return;
            }
            
            // Place market order
            account.CreateOrder(
                instrument,
                orderAction,
                OrderType.Market,
                OrderEntry.Manual,
                TimeInForce.Day,
                qty,
                0, 0,
                null,
                "Dragon_Signal",
                DateTime.Now,
                null
            );
        }
    }
}
