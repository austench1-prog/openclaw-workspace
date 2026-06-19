# Mac mini M4 迁移 SOP
_目标机: Mac mini M4 16GB/512GB Open Box — ETA 2026-06-19 (Fri)_
_起草: Dragon @ 2026-06-18 22:50 PDT_

---

## 前置：到货前在小塔上做

- [ ] 确认本 SOP 无遗漏
- [ ] `cp -p ~/.openclaw/workspace/secrets/credentials.json /tmp/creds_backup.json`（临时备）
- [ ] 导出当前 cron 列表截图/文字（用 openclaw cron list 或 UI）
- [ ] 记下备份脚本路径（`which backup` 或 locate）
- [ ] 记下信号链配置（Replikanto 目标：温总PC 5000）

---

## 第一步：硬件准备

1. 开箱，接电源、显示器、键鼠（USB-A/C hub 按需）
2. 开机，**不要** 从 Time Machine 恢复（全新装，避免拖旧病）
3. 设置 macOS：
   - Apple ID：austinai（同 App Store 账号）
   - 账户名：`austinai`（必须与小塔一致，避免路径变动）
   - 时区：America/Los_Angeles
   - 关闭 iCloud Drive（workspace 不上云）
   - FileVault：**开启**（磁盘加密）

---

## 第二步：基础软件

```bash
# Homebrew
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

# Node.js (LTS)
brew install node

# OpenClaw
npm install -g openclaw   # 或按官方安装命令

# 其他工具（按需）
brew install git gh trash
```

---

## 第三步：Workspace 迁移

```bash
# 在小塔上打包（排除 .git 大 blob 可加 --exclude）
tar czf ~/workspace-backup-$(date +%Y%m%d).tar.gz -C ~/.openclaw workspace

# 传输到 M4（同 LAN 下）
scp ~/workspace-backup-*.tar.gz austinai@[M4的IP]:~/

# 在 M4 上解压
mkdir -p ~/.openclaw
cd ~/.openclaw
tar xzf ~/workspace-backup-*.tar.gz
```

---

## 第四步：Secrets & 权限

```bash
# secrets 文件夹权限必须 600（含子文件）
chmod 700 ~/.openclaw/workspace/secrets
chmod 600 ~/.openclaw/workspace/secrets/credentials.json
# 验证
ls -la ~/.openclaw/workspace/secrets/
```

---

## 第五步：OpenClaw 配置

```bash
# 启动 openclaw，检查 gateway 状态
openclaw gateway status

# 验证 workspace 路径正确
# 检查 AGENTS.md / SOUL.md / MEMORY.md 是否在位
ls ~/.openclaw/workspace/
```

---

## 第六步：Cron Jobs 重建

1. 在小塔上导出 cron 列表（备用截图）
2. 在 M4 上用 `openclaw cron` 或 UI 逐一重建
3. 重点确认：
   - 心跳 cron（heartbeat poll）
   - 任何定期提醒/任务

---

## 第七步：信号链验证（Replikanto → 温总PC 5000）

1. 安装 NinjaTrader 8（如需）
2. 配置 Replikanto Leader（源：M4 账户）
3. 验证信号到达温总PC 5000（Follower 端收单）
4. 测试单：小手数 MNQ 1手，确认链路通

---

## 第八步：备份脚本

```bash
# 迁移旧备份脚本（路径示例，按实际调整）
cp ~/backup-script.sh ~/.openclaw/workspace/scripts/
chmod +x ~/.openclaw/workspace/scripts/backup-script.sh

# 设定定期执行（cron 或 launchd）
```

---

## 第九步：验收清单

- [ ] `openclaw gateway status` 显示 running
- [ ] Telegram → OpenClaw 消息通道正常（发条测试消息）
- [ ] MEMORY.md / SOUL.md / USER.md 内容完整
- [ ] credentials.json 权限 600 ✅
- [ ] Cron jobs 全部重建 ✅
- [ ] 信号链测试单通过 ✅
- [ ] 备份脚本运行正常 ✅

---

## 第十步：小塔退役

- 确认 M4 完全稳定后（建议运行 1-2 天）
- 小塔关机，标记"冷备"
- 保留原 workspace 备份不删

---

## 注意事项

- `accounts` 账户名 **必须** 用 `austinai`（与 VSCode workspace 路径一致）
- FileVault 密钥备份到安全地方（不存 iCloud）
- 迁移当天不做实盘交易（信号链稳定前）
