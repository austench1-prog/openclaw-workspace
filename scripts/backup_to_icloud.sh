#!/bin/bash
# Dragon Memory Backup to iCloud
# Runs automatically via cron or manually
# Created: 2026-04-10

WORKSPACE="/Users/austinai/.openclaw/workspace"
ICLOUD="$HOME/Library/Mobile Documents/com~apple~CloudDocs/Dragon_Backup"
DATE=$(date +%Y-%m-%d)
LOG="$WORKSPACE/scripts/backup.log"

echo "[$DATE $(date +%H:%M)] Starting backup..." >> "$LOG"

# Create iCloud backup folder if not exists
mkdir -p "$ICLOUD"

# Backup critical files
cp "$WORKSPACE/MEMORY.md" "$ICLOUD/MEMORY.md" && echo "[$DATE] MEMORY.md ✅" >> "$LOG"
cp "$WORKSPACE/Multi_Agent_Trading_System_v3.0.md" "$ICLOUD/MATS_v3.0.md" && echo "[$DATE] MATS v3.0 ✅" >> "$LOG"
cp "$WORKSPACE/Agent_Prompts/compliance_framework_v1.md" "$ICLOUD/compliance_framework_v1.md" && echo "[$DATE] Compliance Framework ✅" >> "$LOG"
cp "$WORKSPACE/Agent_Prompts/gatekeeper_v1.md" "$ICLOUD/gatekeeper_v1.md" && echo "[$DATE] Gatekeeper ✅" >> "$LOG"
cp "$WORKSPACE/Agent_Prompts/daily_checklist_v1.md" "$ICLOUD/daily_checklist_v1.md" && echo "[$DATE] Daily Checklist ✅" >> "$LOG"
cp "$WORKSPACE/Dragon_ToDo_v1.md" "$ICLOUD/Dragon_ToDo_v1.md" && echo "[$DATE] ToDo List ✅" >> "$LOG"

# Backup today's memory file
MEMORY_FILE="$WORKSPACE/memory/$(date +%Y-%m-%d).md"
if [ -f "$MEMORY_FILE" ]; then
    cp "$MEMORY_FILE" "$ICLOUD/memory_$(date +%Y-%m-%d).md" && echo "[$DATE] Today's memory ✅" >> "$LOG"
fi

echo "[$DATE $(date +%H:%M)] Backup complete." >> "$LOG"
echo "Dragon backup to iCloud complete: $DATE"
