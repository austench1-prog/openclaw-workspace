# 数据安全与 API 隐私防护
> 建立日期：2026-04-04

---

# 第一部分：笔记（原始内容）

## 核心前提
本地模型（Local LLM）暂不在选项内。
在"使用商业 API"和"确保数据主权"之间建立坚固的防火墙。

---

## 1. API 的"不学习"特性

| 方式 | 数据处理 |
|---|---|
| 网页端（ChatGPT/Claude.ai）| ✅ 数据默认用于模型训练 |
| API Key 调用（你现在的方式）| ✅ 不被用于训练，不被存储改进服务 |

**操作建议：** 所有核心交易策略和笔记内容，通过 Mac mini 上的 API 调用处理，不在网页端输入。

---

## 2. Obsidian 文件夹隔离策略

- 建立 `Personal_Private/` 文件夹
- AI 插件（如 Smart Connections）设置 **Excluded Folders**，把私密文件夹填进去
- 结果：插件物理跳过这些内容，AI 根本看不见

---

## 3. 防火墙监控（Mac 推荐工具）

| 工具 | 特点 |
|---|---|
| **LuLu** | 免费，开源 |
| **Little Snitch** | 付费，功能更强 |

**作用：** 实时提醒异常连接，只允许官方 API 地址（OpenAI/Anthropic），拦截其他所有未知请求。

---

## 4. 传输脱敏程序（高级选项）

- 发送给 LLM 之前：关键词替换（真实资金量 → 代号，账户名 → Account_01）
- 收到结果后：本地还原
- 工具：Python 脚本或 Obsidian 插件

---

## 5. 安全配置清单

| 项目 | 建议 |
|---|---|
| API 调用 | 继续使用 API，不用网页端处理敏感内容 |
| 文件夹隔离 | Obsidian 插件排除私密路径 |
| 云同步 | 确认 Obsidian 无第三方云盘同步 |
| 日志清理 | 定期检查 OpenClaw 运行日志，避免明文长期存在 |
| 流量监控 | 安装 LuLu 或 Little Snitch |

**核心原则：**
> 不入训练集 + 本地物理隔离 + 流量监控 = 对外界仍是黑盒

---

# 第二部分：龙大哥应对策略

## 现状评估

你目前的架构：

| 项目 | 现状 | 风险等级 |
|---|---|---|
| API 调用方式 | OpenClaw API Key（Anthropic Tier 3+）| ✅ 低风险 |
| 数据存储 | 本地 `~/.openclaw/workspace` | ✅ 低风险 |
| 敏感文件 | 已隔离进 iCloud / 加密磁盘 | ✅ 低风险 |
| OpenClaw 日志 | 明文存在 `~/.openclaw/logs/` | ⚠️ 需关注 |
| Obsidian 同步 | 两台电脑用不同 Apple ID，无自动同步 | ✅ 低风险 |
| 流量监控 | 尚未安装防火墙工具 | ⚠️ 待补强 |

---

## 行动计划（按优先级）

### 优先级 1：立即可做

**检查 OpenClaw 日志**

日志位置：
```
~/.openclaw/logs/
```

检查是否有敏感内容（EIN、账户信息等）以明文形式存在。

---

### 优先级 2：本周内完成

**安装 LuLu（免费防火墙）**

下载地址：https://objective-see.org/products/lulu.html

配置原则：
- 允许：`api.anthropic.com`
- 允许：`api.openai.com`
- 允许：`icloud.com`
- 其他未知连接：询问后决定

---

### 优先级 3：Obsidian 插件排除配置

如果你以后安装 Obsidian AI 插件（如 Smart Connections），在插件设置里加入排除路径：

```
Business_OS/
Personal_Private/（如果建立的话）
memory/
```

---

### 优先级 4：日志清理规则（长期）

建议每月定期清理 OpenClaw 日志：

```bash
# 查看日志大小
du -sh ~/.openclaw/logs/

# 清理30天以前的日志（可选）
find ~/.openclaw/logs/ -name "*.log" -mtime +30 -delete
```

---

## 不需要做的事

- ❌ 不需要本地部署 LLM（成本高、暂无必要）
- ❌ 不需要脱敏程序（你的 API 调用方式已经足够安全）
- ❌ 不需要关闭 iCloud（两个 Apple ID 互不相通，已隔离）

---

## 一句话总结

> 你现在的架构已经处于商业 API 用户中的安全上游。
> 补两件事就完整：**LuLu 防火墙** + **定期日志清理**。

---

*文件路径：Business_OS/00_Dashboard/Security_API_and_Data_Privacy.md*
