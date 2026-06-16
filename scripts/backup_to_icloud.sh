#!/bin/bash
# Dragon Backup Script - iCloud + DragonVault (local)
# Runs automatically via cron daily at 3am
# Created: 2026-04-10 | Updated: 2026-06-16

WORKSPACE="/Users/austinai/.openclaw/workspace"
ICLOUD="$HOME/Library/Mobile Documents/com~apple~CloudDocs/Dragon_Backup"
VAULT="/Volumes/DragonVault/Dragon_Backup"
DATE=$(date +%Y-%m-%d)
LOG="$WORKSPACE/scripts/backup.log"

echo "[$DATE $(date +%H:%M)] Starting backup..." >> "$LOG"

# Critical files list
FILES=(
    "MEMORY.md"
    "AGENTS.md"
    "TOOLS.md"
    "Multi_Agent_Trading_System_v3.0.md"
    "Dragon_ToDo_v1.md"
    "Agent_Prompts/compliance_framework_v1.md"
    "Agent_Prompts/gatekeeper_v1.md"
    "Agent_Prompts/daily_checklist_v1.md"
    "Agent_Prompts/ninja_startup_sop_v1.md"
    "Agent_Prompts/tradovate_daily_risk_sop_v1.md"
    "memory/active_work.md"
)

# --- iCloud Backup ---
mkdir -p "$ICLOUD"
for f in "${FILES[@]}"; do
    if [ -f "$WORKSPACE/$f" ]; then
        cp "$WORKSPACE/$f" "$ICLOUD/$(basename $f)" && echo "[$DATE] iCloud: $f ✅" >> "$LOG"
    fi
done

# --- DragonVault Backup (local) ---
if [ -d "/Volumes/DragonVault" ]; then
    mkdir -p "$VAULT"
    for f in "${FILES[@]}"; do
        if [ -f "$WORKSPACE/$f" ]; then
            cp "$WORKSPACE/$f" "$VAULT/$(basename $f)" && echo "[$DATE] Vault: $f ✅" >> "$LOG"
        fi
    done
    # Full workspace snapshot to DragonVault
    rsync -av --delete "$WORKSPACE/" "$VAULT/workspace_full/" >> "$LOG" 2>&1
    echo "[$DATE] Vault: Full workspace snapshot ✅" >> "$LOG"
else
    echo "[$DATE] DragonVault not mounted - skipping local backup" >> "$LOG"
fi

# Trading directory (rsync to both destinations)
if [ -d "$ICLOUD" ]; then
    rsync -a --delete "$WORKSPACE/Trading/" "$ICLOUD/Trading/" && echo "[$DATE] iCloud: Trading/ ✅" >> "$LOG"
fi
if [ -d "/Volumes/DragonVault" ]; then
    rsync -a --delete "$WORKSPACE/Trading/" "$VAULT/Trading/" && echo "[$DATE] Vault: Trading/ ✅" >> "$LOG"
fi

# Today's memory file
MEMORY_FILE="$WORKSPACE/memory/$(date +%Y-%m-%d).md"
if [ -f "$MEMORY_FILE" ]; then
    cp "$MEMORY_FILE" "$ICLOUD/memory_$(date +%Y-%m-%d).md"
    [ -d "/Volumes/DragonVault" ] && cp "$MEMORY_FILE" "$VAULT/memory_$(date +%Y-%m-%d).md"
    echo "[$DATE] Today's memory ✅" >> "$LOG"
fi

echo "[$DATE $(date +%H:%M)] Backup complete." >> "$LOG"
echo "Dragon backup complete: $DATE (iCloud + DragonVault)"
