# Dragon-A Operations Watchlist v1
# Role: System Engineer (external to trading decision chain)
# Version: 1.0 | Date: 2026-04-08

---

## Daily Checks (Dragon-A responsibility)

### Morning Pre-Market (before 09:00 ET)

- [ ] SSH connectivity: Mac mini → Win PC (温总)
- [ ] Signal Server running on Win PC (port 5000)
- [ ] NinjaTrader running on Win PC
- [ ] DragonFileSig 1 Minute = GREEN (enabled)
- [ ] DragonFileSig 5 Minute = WHITE (disabled)
- [ ] Replikanto: On = GREEN, Cross Order = GREEN
- [ ] Leader Account = Sim101
- [ ] Apex account connection active in NinjaTrader

### Continuous Monitoring

- [ ] Win PC network latency < 200ms
- [ ] Signal Server responding to PING
- [ ] Mac mini memory and CPU normal
- [ ] GitHub sync up to date

### Post-Market

- [ ] All positions closed (no overnight)
- [ ] Execution log updated
- [ ] Daily net value recorded

---

## Alert Conditions (immediate Telegram notification)

| Condition | Action |
|---|---|
| SSH connection lost to Win PC | Alert + attempt reconnect |
| Signal Server not responding | Alert + attempt restart via SSH |
| NinjaTrader not running | Alert Chairman |
| DragonFileSig status changed | Alert Chairman |
| Win PC latency > 500ms | Alert + log |
| Mac mini CPU > 90% sustained | Alert + investigate |

---

## WOL Status

- Win PC MAC: C8:53:09:F1:1A:C3
- Win PC IP: 192.168.0.226
- Fast Startup: DISABLED (fixed 2026-04-08)
- BIOS WOL: Pending (needs one-time BIOS config by Chairman)
- Current workaround: manual power button

---

## Dragon-A vs Dragon-B Boundary

| Dragon-A (System Engineer) | Dragon-B (Business Assistant) |
|---|---|
| Hardware monitoring | Rule queries |
| SSH / API / GitHub | Pre-trade checklist |
| Log review | Account summary |
| Compile / debug | Information relay |
| Does NOT make trading decisions | Does NOT verify rules directly |

---

*v1 | 2026-04-08*
