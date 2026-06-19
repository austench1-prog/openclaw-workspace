# Mac mini 迁移 SOP v1.0
# Created: 2026-06-18 | Based on: M4 Mac mini actual migration

---

## 概述

本文档记录小塔（Mac mini）迁移到新机器的完整操作流程，
基于 2026-06-18 实际迁移经验整理。

---

## 迁移前准备（旧机器上做）

- [ ] 确认 iCloud 备份最新（当天）
- [ ] 确认 DragonVault 备份最新（当天手动跑一次备份脚本）
- [ ] 记录旧机器 IP（192.168.0.x）
- [ ] 确认温总信号服务器地址不变（192.168.0.226:5000）
- [ ] GitHub 推送最新 workspace

---

## 迁移后检查清单（新机器上做）

### 1. OpenClaw 服务
- [ ] `openclaw gateway status` → 确认 running，单实例
- [ ] 确认 Telegram bot (@TT986_Bot) 正常接收消息
- [ ] 发一条测试消息确认回复正常

### 2. DragonVault（Samsung SSD）

**⚠️ 已知问题：卷名含换行符**

DragonVault APFS 卷名包含换行符，导致路径字符串拼接失败。

**症状：** `ls /Volumes/DragonVault` 报 "No such file or directory"，但 `mount` 显示已挂载。

**解法：** 所有脚本一律用 glob 方式访问：
```bash
cd /Volumes/Dragon* && pwd    # 验证可访问
```

**首次挂载步骤：**
```bash
# 1. 插上盘，等系统识别
diskutil list | grep DragonVault

# 2. 若未自动挂载
diskutil mount /dev/disk5s1   # disk 编号可能不同，看 diskutil list

# 3. 禁用 ownership（新机器需要做一次，之后持久）
# 在终端输入（需要密码）：
sudo diskutil disableOwnership /dev/disk5s1

# 4. 重挂载使权限生效
diskutil unmount /dev/disk5s1
diskutil mount /dev/disk5s1

# 5. 验证写入
cd /Volumes/Dragon* && touch test_write && rm test_write && echo "✅ 写入正常"
```

**手动跑备份验证：**
```bash
bash /Users/austinai/.openclaw/workspace/scripts/backup_to_icloud.sh
```
看 backup.log 最后几行确认 `Vault: Full workspace snapshot ✅`

### 3. 信号链验证（温总 PC 开机后）
```bash
# 测试信号服务器连通
curl -s -m 5 http://192.168.0.226:5000/status
# 期望输出：Dragon Signal Server v2 is running
```

### 4. Telegram 客户端
- [ ] 小塔上的 Telegram Desktop 更新到最新版（https://desktop.telegram.org）
- [ ] 旧版本会导致富文本消息显示为「This message is not supported」
- [ ] 温总 PC 同样需要更新

### 5. 安全设置
- [ ] 防火墙开启：System Settings → Network → Firewall → ON
- [ ] 隐身模式开启：Firewall → Options → Enable stealth mode ✅
- [ ] Discord 插件禁用：`openclaw plugins disable discord && openclaw gateway restart`
- [ ] 检查 Firewall 允许列表，移除不需要的：
  - smbd（Samba）：不用 Windows 共享就关
  - sshd-keygen-wrapper：不用 SSH 就关

### 6. Apple ID 2FA
- [ ] 系统设置会弹出 Apple ID 验证弹窗
- [ ] 如果没看到代码通知：点「Didn't get a code?」→ 发到 iPhone
- [ ] 在 iPhone 上看到 6 位码 → 输入到小塔弹窗

### 7. cron 任务验证
```bash
crontab -l    # 确认 3am 备份 cron 在
```
期望看到：
```
0 3 * * * /Users/austinai/.openclaw/workspace/scripts/backup_to_icloud.sh
0 * * * * /Users/austinai/.openclaw/workspace/scripts/system_monitor.py ...
```

---

## 已知问题与解法汇总

| 问题 | 根因 | 解法 |
|---|---|---|
| DragonVault `ls` 报错 | 卷名含换行符 | 用 `cd /Volumes/Dragon*` glob 访问 |
| DragonVault 写入 Permission denied | 新机器 ownership 未禁用 | `sudo diskutil disableOwnership /dev/disk5s1` |
| Telegram 显示「不支持此消息」 | 客户端版本太老 | 更新到最新版 |
| Gateway 重启时 Telegram 回复丢失 | 双实例抢 polling | 确认单实例：`ps aux | grep gateway` |
| OpenClaw 启动时 Discord 自动加载 | plugins.allow 为空 | `openclaw plugins disable discord` |

---

## 迁移完成确认

全部完成后在 HEARTBEAT.md 更新：
```
### ✅ 小塔 M4 迁移完成 — YYYY-MM-DD
- 新机：Mac mini M4 (16GB/512GB)，已迁移并验证
- 信号链：✅
- DragonVault：✅
- 备份脚本：✅
- 安全设置：✅
```

---

*文档作者：Dragon | 基于 2026-06-18 实际迁移经验*
