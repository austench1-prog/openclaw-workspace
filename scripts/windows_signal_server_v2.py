# Dragon Signal Server v2 - Windows PC
# Source: Dragon
# Version: 2.0 | Date: 2026-04-06
# Accepts: trading signals AND setup commands

from http.server import HTTPServer, BaseHTTPRequestHandler
import os
import subprocess
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
        print(f"[{timestamp}] Received: {body[:100]}")

        response = "ERROR"

        try:
            # CMD: run a system command
            if body.startswith("CMD:"):
                cmd = body[4:].strip()
                print(f"[{timestamp}] Executing CMD: {cmd}")
                result = subprocess.run(cmd, shell=True, capture_output=True, text=True, timeout=30)
                response = f"CMD_OK: {result.stdout.strip()[:200]}"
                if result.returncode != 0:
                    response = f"CMD_ERR: {result.stderr.strip()[:200]}"

            # PS: run PowerShell
            elif body.startswith("PS:"):
                ps_cmd = body[3:].strip()
                print(f"[{timestamp}] Executing PS: {ps_cmd[:50]}")
                result = subprocess.run(
                    ["powershell", "-Command", ps_cmd],
                    capture_output=True, text=True, timeout=30
                )
                response = f"PS_OK: {result.stdout.strip()[:200]}"
                if result.returncode != 0:
                    response = f"PS_ERR: {result.stderr.strip()[:200]}"

            # Trading signal: ACTION|SYMBOL|QTY
            elif "|" in body:
                parts = body.upper().split('|')
                if len(parts) >= 3 and parts[0] in ['BUY', 'SELL', 'CLOSE']:
                    os.makedirs(SIGNAL_FOLDER, exist_ok=True)
                    with open(SIGNAL_FILE, 'w') as f:
                        f.write(body)
                    response = f"OK: {body}"
                    print(f"[{timestamp}] Signal written")
                else:
                    response = "ERROR: Invalid signal format"

            # Ping
            else:
                response = "PONG"

        except subprocess.TimeoutExpired:
            response = "ERROR: Command timed out"
        except Exception as e:
            response = f"ERROR: {str(e)}"
            print(f"[{timestamp}] Error: {e}")

        self.send_response(200)
        self.send_header('Content-Type', 'text/plain')
        self.end_headers()
        self.wfile.write(response.encode())

    def do_GET(self):
        self.send_response(200)
        self.send_header('Content-Type', 'text/plain')
        self.end_headers()
        self.wfile.write(b"Dragon Signal Server v2 is running")

    def log_message(self, format, *args):
        pass

if __name__ == "__main__":
    os.makedirs(SIGNAL_FOLDER, exist_ok=True)
    print(f"Dragon Signal Server v2 on port {PORT}")
    print(f"Accepts: trading signals + CMD: + PS: commands")
    print("Ready.")
    server = HTTPServer(('0.0.0.0', PORT), SignalHandler)
    server.serve_forever()
