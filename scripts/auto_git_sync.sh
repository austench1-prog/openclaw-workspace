#!/bin/bash
# Auto Git Sync - commit + push workspace changes to GitHub
# So 小白 (and any device) always pulls the latest.
# Runs via cron. Created 2026-06-15.

WORKSPACE="/Users/austinai/.openclaw/workspace"
LOG="$WORKSPACE/scripts/git_sync.log"
DATE=$(date "+%Y-%m-%d %H:%M")

cd "$WORKSPACE" || exit 1

# Only act if there are changes (tracked or untracked)
if [ -z "$(git status --porcelain)" ]; then
    echo "[$DATE] No changes, skip." >> "$LOG"
    exit 0
fi

git add -A >> "$LOG" 2>&1
git commit -m "auto-sync $DATE" >> "$LOG" 2>&1
if git push origin main >> "$LOG" 2>&1; then
    echo "[$DATE] Pushed ✅" >> "$LOG"
else
    echo "[$DATE] PUSH FAILED ⚠️" >> "$LOG"
fi
