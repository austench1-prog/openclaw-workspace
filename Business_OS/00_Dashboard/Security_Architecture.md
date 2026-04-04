# 安全架构说明（Security Architecture）
> 建立日期：2026-04-03 | 核心原则文件，不得删除

---

## 核心原则

> **物理隔离，逻辑关联。**
> 龙大哥（OpenClaw）是 AI 代理，连接互联网，不能接触敏感原件。
> Obsidian 是本地管理工具，只存流程和索引，不存数据原件。

---

## 三层存储架构

### 第一层：核心机密（加密存储）
**存放位置：** macOS 加密磁盘 / 1Password / Bitwarden
**存放内容：**
- EIN 确认信（CP575）
- 银行开户文件原件
- 公司注册证书
- 纳税申报表
- 政府 ID
- 银行账号/密码
- 券商/税务/州政府平台登录信息

**访问方式：** 只有本人手动打开，AI 不接触

---

### 第二层：管理流程（Obsidian）
**存放位置：** `~/.openclaw/workspace/Business_OS/`（本地，非公开）
**存放内容：**
- 银行开户进度记录
- LLC 年度申报时间表
- 合规要求清单
- SOP 和流程说明
- 文件索引（只写位置，不贴内容）
- 决策记录

**安全边界：** 只写"去哪找"，不写"是什么"

---

### 第三层：执行模板（OpenClaw workspace）
**存放位置：** `~/.openclaw/workspace/Business_OS/`
**存放内容：**
- 商务信函模板（脱敏版）
- 银行 KYC 话术框架
- LLC 结构说明（不含账号）
- 交易风险管理规则描述

**使用方式：** 需要写信时告诉龙大哥「参考模板」，龙大哥协助起草，不接触原始数据

---

## 文件索引示例（正确写法）

```markdown
### APM LLC 文件位置
- Articles of Organization → 本地加密盘：Documents/Corporate/APM_LLC/01_Formation/
- EIN Letter → 本地加密盘：Documents/Corporate/APM_LLC/02_IRS_Tax/
- 银行开户包 → 云盘：Business/APM_LLC/03_Banking/
```

---

## 为什么龙大哥（OpenClaw）不能存敏感文件

- OpenClaw 是连接互联网的 AI 代理
- 频繁调用外部 API
- 如果 API Key 泄露，工作目录内容存在风险
- EIN、银行账号、政府 ID 属于最高级敏感信息
- 一旦泄露影响法律实体安全

---

## 合规做法总结

| 操作 | 正确做法 |
|---|---|
| 存 EIN | 加密磁盘 / 1Password |
| 存银行文件 | 云盘加密文件夹 |
| 查进度和待办 | Obsidian |
| 写商务信函 | 告诉龙大哥「参考模板」，提供脱敏背景 |
| 存登录信息 | 1Password / Bitwarden，绝不存笔记 |
