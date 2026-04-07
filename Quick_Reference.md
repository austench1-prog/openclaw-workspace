# Quick Reference - Commands & Shortcuts
# Source: Dragon
# Version: 1.0 | Date: 2026-04-05

---

## 1. SYNC (Most Important)

### 小塔（Mac mini / 龙哥操作）→ 直接 push，不需要先 pull
```bash
cd ~/.openclaw/workspace && git add -A && git commit -m "说明" && git push
```

### 小白（MacBook Air / 总裁操作）→ 必须先 pull 再 push
```bash
cd ~/.openclaw/workspace && git add -A && git commit -m "说明"
git pull --rebase && git push
```

### 只拉取最新（小白同步小塔的更新）
```bash
cd ~/.openclaw/workspace && git pull
```

---

## 2. TERMINAL - Daily Use

### Go to workspace
```bash
cd ~/.openclaw/workspace
```

### List all files
```bash
ls
```

### See git status
```bash
git status
```

### See recent commits
```bash
git log --oneline -5
```

---

## 3. MAC - System Shortcuts

| Action | Shortcut |
|---|---|
| Spotlight search | ⌘ + Space |
| Switch apps | ⌘ + Tab |
| Screenshot | ⌘ + Shift + 4 |
| New Terminal window | ⌘ + N (in Terminal) |
| Force quit | ⌘ + Option + Esc |
| Lock screen | ⌘ + Control + Q |

---

## 4. VS CODE - Daily Use

| Action | Shortcut |
|---|---|
| Open workspace | `code ~/.openclaw/workspace` (Terminal) |
| Open file | ⌘ + P → type filename |
| Find in all files | ⌘ + Shift + F |
| Open terminal inside VS Code | ⌘ + ` (backtick) |
| Preview Markdown | ⌘ + Shift + V |
| Select all | ⌘ + A |
| Copy | ⌘ + C |
| Save | ⌘ + S |
| Close file | ⌘ + W |
| Reload window | ⌘ + Shift + P → type "Reload Window" |

---

## 5. OBSIDIAN - Daily Use

| Action | Shortcut |
|---|---|
| Open workspace | Open Obsidian → vault is `~/.openclaw/workspace` |
| Refresh / reload | ⌘ + R |
| Search files | ⌘ + O |
| Search text | ⌘ + Shift + F |
| Toggle reading view | ⌘ + E |
| New note | ⌘ + N |
| Close tab | ⌘ + W |

---

## 6. OPENCLAW - Telegram Commands

| Command | Action |
|---|---|
| Just chat normally | Ask anything |
| `记笔记` | Dragon saves notes to SOP file |
| `更新 SOP` | Dragon updates system SOP |
| `记录节点` | Dragon logs decision to Decision Log |
| `这个很重要` | Dragon adds item to Assets Library |
| `加入军械库` | Same as above |
| `/status` | Show session status |
| `/reasoning` | Toggle reasoning mode |

---

## 7. COMMON WORKFLOWS

### After Dragon pushes changes (new computer sync)
```bash
cd ~/.openclaw/workspace && git pull
```
Then in Obsidian: press **⌘ + R**

### Want to tell Dragon to change a file
Just message: "Change [filename]: [what to change]"
Dragon edits on Mac mini and pushes automatically.

### Open everything at start of day
```bash
# Terminal
cd ~/.openclaw/workspace

# VS Code (paste in Terminal)
code ~/.openclaw/workspace

# Obsidian - just open the app
```

### Check if sync is up to date
```bash
cd ~/.openclaw/workspace && git log --oneline -3
```
Compare top commit with what Dragon last reported.

---

## 8. 紧急拍拍 - 小白唤醒小塔

**触发时机：** 小塔失联 / 龙哥没有回应

**在小白 Terminal 执行：**
```bash
python3 ~/wake_mini.py
```

**执行动作：**
1. 发送 Magic Packet 唤醒小塔
2. 等待小塔上线（最长90秒）
3. SSH 重载龙哥
4. 输出：`✅ 小塔已唤醒，龙哥已重载。`

**注意：** 小塔必须接网线或保持 WiFi，且 Wake on LAN 已开启（已设置）

---

## 9. PROP FIRM - Daily Check (Manual for now)

Before trading, answer these:
- What is today's account balance?
- What is the drawdown floor?
- How much room do I have today?
- Any Tier 1 news events today? (TradeDay: no trading)

---

*Keep this file open in Obsidian as a pinned reference tab.*
