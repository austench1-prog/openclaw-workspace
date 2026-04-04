# 🐉 Business OS — 主执行说明书
> 版本：v1.0 | 建立日期：2026-04-03
> 负责人：龙大哥（OpenClaw）
> 触发词：「记笔记」/「更新 SOP」/「记录节点」/「加入军械库」

---

## 一、系统定位

本文件是两家公司运营的**最终执行说明书**。所有重要决策、流程、安全规则均以本文件为准。

---

## 二、两家公司基本信息

| 项目 | APM LLC | Meritpoint Logic LLC |
|---|---|---|
| 全名 | ⚠️ 待填写（新 APM LLC）| MERITPOINT LOGIC LLC |
| 成立州 | 待填写 | Nevada |
| Entity No. | 待填写 | 待填写 |
| EIN | 待填写（存加密磁盘）| 待填写（存加密磁盘）|
| 税务身份 | 待填写 | Disregarded entity |
| 用途 | 待填写 | 自营交易实体 |

---

## 三、安全架构（不可更改原则）

### 三层存储法

| 层级 | 工具 | 存放内容 |
|---|---|---|
| **第一层：核心机密** | macOS 加密磁盘 / 1Password / Bitwarden | EIN / 银行文件 / 注册证书 / 税务回函 / 政府 ID |
| **第二层：管理流程** | Obsidian（本地，非公开） | SOP / 清单 / 进度 / 索引 / 决策记录 |
| **第三层：执行模板** | OpenClaw workspace（龙大哥） | 脱敏话术 / 信函模板 / 逻辑说明 |

### 龙大哥（OpenClaw）绝对不存
- ❌ EIN 原件
- ❌ 银行账号/密码
- ❌ 政府 ID
- ❌ 税务原件
- ❌ 签字版 PDF

### 正确的文件索引写法
> 只写位置，不贴内容
```
EIN Letter → 本地加密磁盘：Documents/Corporate/APM_LLC/02_IRS_Tax/
```

---

## 四、软件分工

| 工具 | 存放内容 |
|---|---|
| **macOS 加密磁盘 / 1Password** | 核心机密原件、所有登录信息 |
| **iCloud Drive / Google Drive** | 正式 PDF / 签字文件 / 银行材料 / IRS 回函 |
| **Obsidian（本文件所在）** | SOP / 流程 / 清单 / 索引 / 决策记录 |
| **OpenClaw workspace** | 脱敏模板 / 话术框架 / 逻辑说明 |
| **Excel / Google Sheets** | 收入支出 / 分类账 / 月度对账 |
| **1Password / Bitwarden** | 银行/券商/税务/州政府登录 |

---

## 五、云盘文件夹结构

```
Business/
├── APM_LLC/
│   ├── 01_Formation/          ← Articles of Organization, Operating Agreement
│   ├── 02_IRS_Tax/            ← EIN Letter, 2553, 税务回函
│   ├── 03_Banking/            ← 银行开户包, 银行回邮
│   ├── 04_Accounting/         ← 月结单, 分类账
│   ├── 05_Compliance/         ← 年报, BOI, 合规文件
│   ├── 06_Payroll/            ← Payroll records
│   ├── 07_Contracts_Letters/  ← 商务信函, 合同
│   └── 08_Archived/
│
├── Meritpoint_Logic_LLC/
│   ├── 01_Formation/
│   ├── 02_IRS_Tax/
│   ├── 03_Banking/
│   ├── 04_Trading_Brokerage/  ← 券商开户文件, trading records
│   ├── 05_Accounting/
│   ├── 06_Compliance/
│   ├── 07_Letters_Internal/
│   └── 08_Archived/
│
└── Shared/
    ├── Business_Structure_Overview.pdf
    ├── Bank_Explanation_Letters/
    ├── Risk_Management_Policy.pdf
    └── Group_SOPs/
```

---

## 六、Obsidian Business OS 目录结构

```
Business_OS/
├── 00_Dashboard/
│   ├── README.md                    ← 指挥中心入口
│   ├── MASTER_OPERATING_MANUAL.md  ← 本文件
│   └── Security_Architecture.md    ← 安全架构说明
├── 01_APM_LLC/
│   └── Company_Profile.md           ← 公司主档案 + 文件索引
├── 02_Meritpoint_Logic_LLC/
│   └── Company_Profile.md
├── 03_Shared_Structure/
│   └── Group_Structure.md           ← 集团架构 + 银行话术
├── 04_SOPs/
│   └── SOP_Index.md                 ← SOP 总目录
├── 05_Banking/
│   └── Banking_Overview.md          ← 银行状态 + 开户清单
├── 06_Compliance_Calendar/
│   └── Annual_Compliance.md         ← 年度合规日历
├── 07_Finance_Admin/
│   └── Finance_Rules.md             ← 财务管理规则
├── 08_Templates/                    ← 商务信函等模板（待建立）
└── 09_Decisions/
    └── Decision_Log.md              ← 重大决策记录
```

---

## 七、合规日历（待完善）

| 事项 | 公司 | 截止日期 | 状态 |
|---|---|---|---|
| CA Statement of Information | APM LLC | 待填写 | 待填写 |
| CA Franchise Tax ($800) | APM LLC | 待填写 | 待填写 |
| NV Annual Report | Meritpoint | 待填写 | 待填写 |
| BOI Filing | 两家 | 待确认 | 待填写 |
| Federal Tax Return | 两家 | 待填写 | 待填写 |

---

## 八、触发词规则（龙大哥执行）

| 触发词 | 动作 | 目标文件 |
|---|---|---|
| 「记笔记」/ 「更新 SOP」 | 汇总刚才聊的核心内容 | `龙大哥系统笔记_01.md` |
| 「记录节点」 | 追加新决策节点 | `01_Decision_Log.md` |
| 「这个很重要」/ 「加入军械库」 | 追加新资产条目 | `02_Assets_Library.md` |

---

## 九、待办事项

### 立即
- [ ] Meritpoint Logic LLC EIN 填入档案
- [ ] 两家公司银行账户状态更新
- [ ] Nevada 年报截止日确认
- [ ] APM LLC S-Corp election 状态确认

### 近期
- [ ] 建立银行 KYC 话术（英文版）放入 `03_Shared_Structure/`
- [ ] 建立商务信函模板库 `08_Templates/`
- [ ] 完善合规日历截止日期
- [ ] 确认 APM LLC 注销还是保留

---

*本文件是 Business OS 的最终执行依据。有变动由龙大哥负责同步更新。*
