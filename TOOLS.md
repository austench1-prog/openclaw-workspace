# TOOLS.md - Local Notes

## System Language Policy
- All workspace files, documents, SOPs, agent prompts, code, and operational records: **English only**
- Chat conversation with President: Chinese is fine
- No Chinese-origin software or services in this system
- This is a US-based operation (APM LLC, Meritpoint Logic LLC)
- All platforms are English-native: NinjaTrader, Apex, Replikanto, OpenClaw, NotebookLM

Skills define _how_ tools work. This file is for _your_ specifics — the stuff that's unique to your setup.

## What Goes Here

Things like:

- Camera names and locations
- SSH hosts and aliases
- Preferred voices for TTS
- Speaker/room names
- Device nicknames
- Anything environment-specific

## Examples

```markdown
### Cameras

- living-room → Main area, 180° wide angle
- front-door → Entrance, motion-triggered

### SSH

- home-server → 192.168.1.100, user: admin

### TTS

- Preferred voice: "Nova" (warm, slightly British)
- Default speaker: Kitchen HomePod
```

## Why Separate?

Skills are shared. Your setup is yours. Keeping them apart means you can update skills without losing your notes, and share skills without leaking your infrastructure.

---

Add whatever helps you do your job. This is your cheat sheet.

## Telegram Desktop 版本（复制按钮问题，2026-06-21 解决）

- **必须用 Telegram Desktop（官方牌面版，从 GitHub release 下），版本 6.9.3 验证可用。**
- ❗ **App Store 版 / Telegram Lite（FZ-LLC）不行** — inline code 没有复制按钮。App ID 747648890 也解决不了。
- 下载：https://github.com/telegramdesktop/tdesktop 的 releases，或官网 desktop.telegram.org。
- 小塔已装 6.9.3，复制按钮正常。

## VSCode Workspace Paths

### 小塔 (Mac mini) - austinai
- Workspace: `/Users/austinai/.openclaw/workspace`
- Open: `code ~/.openclaw/workspace`

### 小白 (MacBook Air) - austinchien
- Workspace: `/Users/austinchien/.openclaw/workspace`
- Open: `code ~/.openclaw/workspace`
- NOT Google Drive, NOT iCloud Drive

### All Pine/ThinkScript files are in workspace root:
- `camarilla-weekly-thinkscript-v4.txt` ← current latest
- `camarilla-weekly-thinkscript-v3.txt`
- `camarilla-weekly-thinkscript.txt` ← original v1

## Indicator Development SOP

**Before writing any indicator code:**
1. Ask President to confirm the logic in plain text
2. Save the confirmed logic as a .md file in workspace
3. Only then write the code

File naming: `[indicator-name]-spec.md`
Example: `camarilla-weekly-spec.md`

This prevents losing the original requirement and avoids confusion during iterations.

## TradingView DragonLab
- URL: https://www.tradingview.com/chart/d1zYpNgC/
- Purpose: Dragon's dedicated lab for indicator development (no other indicators)
- Symbol: NQ1! (NASDAQ 100 E-mini Futures)
- Account: austench1@gmail.com (TV paid account)
