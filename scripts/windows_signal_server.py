# Dragon Signal Server - runs on Windows PC
# Source: Dragon
# Version: 1.0 | Date: 2026-04-05
#
# Run this on Windows: python windows_signal_server.py
# Mac mini sends signals to this server
# Server writes signal file to C:\DragonSignals\ for NinjaTrader to pick up

from http.server import HTTPServer, BaseHTTPRequestHandler
import os
import json
from datetime import datetime

SIGNAL_FOLDER = r"C:\DragonSignals"
SIGNAL_FILE = os.path.join(SIGNAL_FOLDER, "signal.txt")
PORT = 5000

class SignalHandler(BaseHTTPRequestHandler):
    def do_POST(self):
        content_length = int(self.headers.get('Content-Length', 0))
        body = self.rfile.read(content_length).decode('utf-8').strip()
        
        timestamp = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        print(f"[{timestamp}] Received: {body}")
        
        # Validate signal format: ACTION|SYMBOL|QTY
        parts = body.upper().split('|')
        if len(parts) >= 3 and parts[0] in ['BUY', 'SELL', 'CLOSE']:
            # Write to signal file for NinjaTrader
            os.makedirs(SIGNAL_FOLDER, exist_ok=True)
            with open(SIGNAL_FILE, 'w') as f:
                f.write(body)
            
            response = f"OK: {body}"
            print(f"[{timestamp}] Signal written to {SIGNAL_FILE}")
        else:
            response = f"ERROR: Invalid signal format. Use ACTION|SYMBOL|QTY"
            print(f"[{timestamp}] Invalid signal: {body}")
        
        self.send_response(200)
        self.send_header('Content-Type', 'text/plain')
        self.end_headers()
        self.wfile.write(response.encode())
    
    def do_GET(self):
        self.send_response(200)
        self.send_header('Content-Type', 'text/plain')
        self.end_headers()
        self.wfile.write(b"Dragon Signal Server is running")
    
    def log_message(self, format, *args):
        # Suppress default logging, we handle it ourselves
        pass

if __name__ == "__main__":
    os.makedirs(SIGNAL_FOLDER, exist_ok=True)
    print(f"Dragon Signal Server starting on port {PORT}")
    print(f"Signal folder: {SIGNAL_FOLDER}")
    print(f"Waiting for signals from Mac mini...")
    print("Press Ctrl+C to stop")
    print("-" * 40)
    
    server = HTTPServer(('0.0.0.0', PORT), SignalHandler)
    server.serve_forever()
