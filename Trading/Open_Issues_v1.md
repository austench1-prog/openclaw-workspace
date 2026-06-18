# Trading Open Issues / 待解决事项 v1

**Created:** 2026-06-15 | **Maintained by:** Dragon
Tracks unresolved trading-system issues that need a decision or rule. Each item = problem + interim management + final fix direction.

---

## 【总纲】系统分工测试 (2026-06-15, Chairman 方向)

Chairman 的深层意图:不只是解决单个问题,而是借这些问题**测试系统的分工能力**。核心问题:
- 我们最初的设置里是不是已经把 **scale / agent** 做了拆分,可以完成不同任务?
- 每个问题 → 分类:【A】系统可自动解决 / 【B】必须 Chairman 参与 / 【C】Dragon 要在系统侧补的工作。
- 先记录，不急讨论;讨论时逐项定 A/B/C 与实用性。

【待讨论清单 — 每项待定 A/B/C】
- [ ] ISSUE-001 跨平台结构不一致 → 哪些能系统自查(合约/数据源比对)?
- [ ] ISSUE-002 账户信息更新/同步 → 能否系统自动拉取账户状态(API/截屏识别)?
- [ ] 原始设置里 agent/scale 拆分现状核查 → 现有能力边界是什么?
- [ ] 为解决这些,Dragon 在 side(系统侧)要补什么 → 逐条列出

---

## ISSUE-001 — 同产品跨平台:数据 + 结构不一致

**Raised:** 2026-06-15 (Chairman)
**Status:** OPEN — interim rule needed; final source-lock decision pending
**Layer:** Rule layer (影响所有判断的地基,非预演)
**Priority:** HIGH (会直接污染 OB/FVG/结构判断 → 影响交易决策)

### 问题描述
同一种产品(如 NQ),在两个平台上看 **数据不同 + 结构看上去也不同**。
Chairman 判断:**大概率两边用的不是同一个合约**(虽然同种产品)。

### 最可能根因(从高到低)
1. **合约不同** — 连续合约(NQ1!) vs 单月合约(NQM6);含/不含展期调整 → 结构整体变形(OB/FVG/摆动高低点位置全变)。← 当前最可能
2. **数据源/聚合不同** — tick 精度、成交量算法不同 → K线实体/影线变化(影响引线、OB 判定)。
3. **会话口径不同** — RTH vs ETH → 日开盘价、Midnight Open、亚洲区间错位(直接影响 gap-down 策略、Kill Zone、4H 锚点)。
4. **时区设置不同** — ET vs 本地 → 4H 蜡烛起点错位 → OB/FVG 全移。
5. **复权/展期方式不同** — Panama/比例/不复权 → 跨月结构跳变。

### ⚠️ 交易管理办法(出现这种情况时怎么做)— 这是本事项的重点
**核心原则:判断与执行必须同源。**

1. **锁定"判断基准源"** — 只在 **一个** 平台 + **一个** 合约口径上做所有结构判断(OB/FVG/摆动点/目标位)。
   - 建议:判断源 = 与执行链一致的平台(执行走 NinjaTrader/Tradovate,判断就在它上面),避免"在A平台画线、到B平台下单"的错位。
2. **另一平台仅作交叉验证**,不参与下单决策。两边结构若打架 → **以基准源为准**,不混用。
3. **每次交易前确认四项口径一致**(基准源 checklist):
   - 合约代码(连续 or 指定月,且与下单合约一致)
   - 时区(锁 ET)
   - 会话(ETH/RTH 选定并固定)
   - 4H 锚点(02/06/10/14/18/22 ET)
4. **若基准源与执行平台合约对不上 → 不交易**,先对齐再说(呼应纪律:口径不清 = 不下单)。

### 观察记录 (2026-06-15)
- 其中一平台 = **NinjaTrader**,显示合约 **NQ SEP26**(另有 GC AUG26 标签);出现过 "Data is delayed 10 min" / "Delayed" 标记。
- 另一平台 = **TradingView (TV)**。

### ⚠️ 原因未确认 — 以下解释已被 Chairman 证伪/存疑
- **“延迟10分钟”不成立(被证伪):** 实际差距是 **1-2 小时** —— TV 上 1-2 小时前已经突破,NT 上到现在都还没突破。量级完全对不上,10分钟延迟解释不了。
- **数据源澄清:** NinjaTrader 只是**显示终端**,我们用的数据**不是 NT 提供的**,是**考试账户的 Prop 公司(如 Apex/Tradovate 端)提供的**。所以问题可能出在那个数据源,不在 NT 本身。
- **结论:** 目前原因**不确定**。不再硬给答案。待逐项排查。

### 需排查的方向(重新)
- TV 看的是什么合约 / 数据源?(连续合约 NQ1!? 哪个交所源?) vs NT(Prop 公司源,SEP26)
- 两者“是否同一个合约月份”—— 可能才是 1-2 小时结构差的真原因(不同月份合约走势可以差很多)
- Prop 公司数据源本身是否有问题(源质量/卡顿)
- 为什么 NT 迟迟不突破而 TV 早已突破 — 是价格真不同,还是合约/源不同造成的视觉差异

### 交易管理含义(不受原因未确认影响)
- 既然两平台结构能差 1-2 小时,**判断与执行必须同源**这条临时铁律更加重要:只在一个源上判断,两边打架以基准源为准,口径不清 = 不下单。

### 待 Chairman 决策 / 待办
- [x] 确认两平台 = NinjaTrader(NQ SEP26, Prop 公司数据源) + TradingView
- [ ] 查清 TV 看的合约代码 + 数据源(连续/指定月,哪个交所源)
- [ ] 比对两者是否同一合约月份(最可疑 — 1-2小时结构差的主嫌)
- [ ] 排查 Prop 公司数据源质量
- [ ] 弄清各平台选合约的**自主性有多大**(能否手动指定单月合约 / 能否切连续合约)— Chairman 备注:目前未细想,需调研
- [ ] 拍板"判断基准源"= 哪个平台 + 哪种合约口径
- [ ] 定稿后:把"图表基准源锁定"升级为 Line 1 正式规则(Source 单一化的延伸),写入合规框架

### 关联
- 合规框架原则:Source = 单一可靠源(Tier 1)
- 影响策略:Gap-down 极静突破(依赖开盘价/会话口径)、4H 锚点、Kill Zone、所有 OB/FVG 判断

---

## ISSUE-002 — 考试账户信息更新 (现状核对 + 待确认)

**Raised:** 2026-06-15 (Chairman) | **Status:** OPEN — Dragon 读图完成,待 Chairman 确认后更新 MEMORY

### 读图记录 (2026-06-15 截图,待核实)

**平台 1 — Tradovate (Chrome) · 合约 MNQU6 · 价 ~30,849:**
TakeProfitTrader (TPT) 5 个 50K 账户,$2,000 trailing drawdown:
| 账户 | Cash | Dist to Drawdown |
|---|---|---|
| TAKEPROFITPRO704123103 | $48,579.50 | $363.00 (⚠️ 接近) |
| TAKEPROFIT973527220 | $50,000 | $2,000 |
| TAKEPROFIT152524137 | $50,000 | $2,000 |
| TAKEPROFIT718789812 | $50,000 | $2,000 |
| TAKEPROFIT800884314 | $50,000 | $2,000 |

**平台 2 — NinjaTrader:**
- Sim (虚拟 leader): Sim101 ($9.63M) / SimNQ / SimFF
- TD: ELTDER260603051540656386 → $48,445.30
- Tradeify 5 个 ~$49K 账户:
  - TDFYG50653786524 → $49,171.76
  - TDFYG50785559546 → $49,157.76
  - TDFYG50971738857 → $49,157.26
  - TDFYG50758269179 → $49,156.76
  - TDFYG50640402386 → $49,159.76

### 现状 vs 旧记录
- 新出现:**TakeProfitTrader (TPT) ×5** + **Tradeify ×5** + Sim101/SimNQ/SimFF + TD 账户
- MEMORY 旧记录:**Apex APEX-165583-123** + **MFF(已暂停)** — 两图里均未出现

### 待 Chairman 确认(确认后才写 MEMORY)
- [ ] TPT + Tradeify 是新开考试账户吗?Apex/MFF 是否停用 → 移入“历史账户”?
- [ ] 各账户具体规则(profit target / min days / consistency / DLL)是否有合同?还是先记账号、规则待补?
- [ ] 是否重写 MEMORY Accounts 段 = TPT + Tradeify + Sim,Apex/MFF 转历史

### 系统分工角度(待讨论 A/B/C)
- 账户状态拉取:Tradovate / Tradeify 是否有 API 可让系统自动读账户余额/drawdown?→ 可能【A 系统可解决】
- 合同规则录入:需 Chairman 提供合同 → 【B 必须 Chairman 参与】
- Dragon side 补充:账户台账(account registry)文档结构 → 【C Dragon 系统侧】

---

## ISSUE-003 — 6 考试账户 · 本周 test 规划(盈利目标) + 抢救计划

**Raised:** 2026-06-15 (Chairman) | **Status:** OPEN — 仅记录,待 review 后排 to-do
**适用范围:全部 6 个考试账户**(Tradeify ×5 + TradeDay ×1)。Chairman 2026-06-15 明确:"6 of all accounts"。

### A. 本周 test 盈利规划
- **允许的最大单日盈利 = $1,500** → 把单日最大盈利设为 **$1,500**。
- 交易始终以 **1:5 盈亏比** 目标设置。
- 大方向:本周还有 **4 个交易日**,完成 **$3,000 利润目标**的账户规划。
- 待办:查这 6 个账户的**到期日** + **还有多少可用交易日** → 做完整策略。

### B. 抢救计划(Chairman 思路 · 适用全 6 账户)
- 现状:6 个考试账户 trailing max drawdown 都极低(~$75-83),余额相当低 → **进入抢救状态**。
- Chairman 抢救思路:
  1. 用**目前余额的一半** + **5 倍(5x)设置**进行一笔交易。
  2. 5x 交易跑到**第一个有价值的点位**后 → 把交易**放回盈亏平衡(break even)**。
  3. 之后只关心能不能走到 **5 倍**。
  4. 能救回来就救,救不回就算了(已经太差)。
- 待办:查这 6 个账户的到期日 + 可用交易日。

### 账户口径更正 (Chairman 2026-06-15, 重读图后定稿)
- **两家公司,共 6 个考试账户** = 要记录/管理的。与图完全对上。
- **虚拟账户 Sim101 / SimFF / SimNQ 不理会**(百万级余额、$0 draw),记录时跳过。
- ⚠️ 更正:之前误把另一张 Tradovate 图的 TPT 5账户混进来了。本图(NinjaTrader)**没有 TPT 账户**。

### 读图记录 (2026-06-15, NinjaTrader) — 6 考试账户,虚拟账户已剔除
| 连接 | 账户 | Cash | Trailing max draw |
|---|---|---|---|
| (公司2 待补) | ELTDER260603051540656386 | $48,445.30 | $75.15 |
| Tradeify | TDFYG50640402386 | $49,159.76 | $83.56 |
| Tradeify | TDFYG50653786524 | $49,171.76 | $82.06 |
| Tradeify | TDFYG50758269179 | $49,156.76 | $82.56 |
| Tradeify | TDFYG50785559546 | $49,157.76 | $83.56 |
| Tradeify | TDFYG50971738857 | $49,157.26 | $83.06 |

*共 6 个考试账户:Tradeify ×5 + ELTDER… ×1。虚拟账户 Sim101/SimFF/SimNQ 不纳入。*

**两家公司(已自查推断):**
- 公司1 = **Tradeify**(5个 TDFYG…)
- 公司2 = **TradeDay (TD)** — 推断依据:NinjaTrader 连接名"TD" + Chairman 历史用过 TradeDay(memory/2026-03-31, 04-04);ELTDER 账号挂在 TD 连接下。置信度高但未 100% 确认,若不对请一言纠正。

### 总原则(Chairman)
- 所有这些先**记录为待办**,然后一起 review,一步一步排出完整 to-do list。

---

## ISSUE-004 — 系统需求更新(仅记录,等核心交易系统完成后再做)

**Raised:** 2026-06-15 (Chairman) | **Status:** PARKED(暂缓 · 不评估)
**Chairman 明确:核心交易系统未完成前,不做这些 — 别捡芝麻丢西瓜。先记概念,等核心完成后再展开。**

### A. 行政/后勤 Agent(账户管家)— 概念
- 设想:一个不参与决策的行政角色 = MATS 里 Prop Intelligence Agent + Dragon-B 的合体。
- 职责:新账户查公司合规/规则/价格;在管账户跟踪 renew / reset / 到期日 / 可用交易日。
- 已评估结论(存档):时机成熟、系统撑得住(有上限)、实际价值约 30-40% 但正中高频痛点 → 建议轻量上(台账+cron),但**暂缓**。

### B. 三类交易品种 + 平台(系统最终需覆盖)
| 品种 | 平台 | 操作方式 |
|---|---|---|
| Prop 期货(TPT 等) | NinjaTrader/Tradovate | TPT 无法并入自动 → 手动操作,但系统协助 + 风控纳入管理 |
| 期货现金账户 AMP | 只能在 TV 上跑 | 待定 |
| SPX 期权 | 平台待点明 | 待定 |

### C. 账户行政信息获取途径(待评估)
- 促销/规则/reset/到期/保证金变动等信息:email 能收到,官网也有。
- 待评估:email vs 官网,哪条路系统更容易监控。初判 email 更易(暂不展开)。

### 前置条件
- ⏸ 全部暂缓,直到**核心交易系统完成**后再启动。

---

## ISSUE-002 补充 — 新增 2 个 MFF 账户 (2026-06-15)

来源:My Funded Futures (MFF) 续费收据邮件(via Tradovate)。
| 账户 ID | Renewal Price | Renewal Date | Order |
|---|---|---|---|
| MFFUEVRPD122274045 | $97.0 | 2026-06-16 00:03:53 | ORD-fnOsZuTUAB |
| MFFUEVRPD122274046 | $97.0 | 2026-06-16 00:03:58 | ORD-PltKhWIkwa |

- 注:这是 **MFF**,与旧记录 MFFUEVRPD122274040(曾标暂停)同公司,账号后缀不同(045/046)。
- ⚠️ 待 Chairman 厘清:现在 MFF 到底有几个活跃账户?这两个新账户是否纳入「考试账户管理」总数?(此前说的「6 个考试账户」是否要更新?)

---

## ISSUE-004 更新 (2026-06-15, Chairman 重要修正)

### 1. SPX 期权平台 = IBKR(已连通)
- 更正之前「平台待定」:**IBKR 系统已设置成功,API 已成功连上。** 不再是问号。

### 2. 保证金/账户信息源(B)— 高优先级 + 承载力取舍
- Chairman:**这部分非常重要,能解决就行。** Email 权限没问题。
- **关键取舍(记死):若系统无法承受所有品类,TPT 可以完全从系统独立拿掉、完全独立出来。**
- 优先级:account/margin 信息的 email 监控 = 高;系统承载不足时,TPT 让路。

### 3. 账户口径 = 动态资产组合,不是固定数
- Chairman 修正:账户**不是固定几个**。这是 APM 主营商业模型的一部分。
- 逻辑:系统越成熟、越确认有盈利能力 → 账户应**越多越好**,不断寻找机会。
- 所以需要的不是「静态台账」,而是**一套能随时管理 / 更新 / 寻找好账户的方法与机制**。
- 「账户管家」角色定位升级:从「记录台账」→「动态资产(账户组合)管理 + 机会发现」。

### 仍受总前提约束
- ⏸ 这些都在 ISSUE-004「核心交易系统完成后再展开」的暂缓范围内,先记录概念,不立即建。

---

## ISSUE-005 — Tradovate 风控落地 (B 方案 + A 方案验证)

**Raised:** 2026-06-16 | **Status:** OPEN

### 背景
今早讨论确定：每日风控线（盈利上限 + 亏损上限）必须落地到系统自动执行，不能靠人手盘中监控。两种方案：

### A 方案 — Tradovate Risk Settings （最高目标）
- **原理：** Tradovate 是上游控制端，在 Tradovate 设好风控线，不管 NinjaTrader 那边怎操作，触动就强制平仓。
- **重要细节：** Tradovate 的「每日」以 **5:00 PM CT** 起算（不是日历日）。
- **待验证：** Tradovate Risk Settings 是账户级——能否覆盖经 Replikanto 跟单的 MFF 账户，需要现場登录验证。
- **阻塑：** 需要 MFF 官网登录凭据（Chairman 提供）。

### B 方案 — Dragon 信号系统 FLATTEN（备用层）
- **触发阈值：** 盈利 $1,600 或当日亏损 $200（默认；R-T3 动态调整）→ 发送 FLATTEN_ALL。
- **发信号的链路：已有**（Dragon → HTTP → Signal Server → DragonFileSig → Sim101 → Replikanto）——现成链路。
- **缺的是：** 自动监控 MFF PnL（Dragon 读不到 Tradovate 实时数据）——依赖 P2 打通。
- **现在可用：** 手动触发版（Chairman 告知已到阈值 → Dragon 立刻发 FLATTEN）。
- **伪代码已写入 Account_Registry，待全自动化落地。**

### 下一步
- [ ] Chairman 提供 MFF 官网登录凭据 → Dragon 登入验证 A 方案，并设好每日风控
- [ ] P2（温总数据连通）实现后再落地 B 方案

### ⚡ 2026-06-17 进展（今日任务）
- 凭据已有（MFF/TPT 均在凭据库）。正在落地 **6 个正常账户**的 A 方案：MFF×2→Tradovate Risk Settings；TPT×4(+PRO)→CQG/TPT 后台。
- 数字（Chairman 06-17 拍板）：盈利目标 **$1,500**；每日亏损上限 **$300**（满足 ≤剩余DD/2）；**例外 TPT PRO 704123103 剩余DD只$370.50 → 用 $150**。
- 状态：后台登录成功，设置入口待最后一步提交（待 Chairman 在线确认提交 / 或明日盘前完成）。

---

## ISSUE-006 — ⚠️ 新闻/高影响事件日无自动拦截（生死线）

**Raised:** 2026-06-17 (Chairman) | **Status:** OPEN · **Priority: CRITICAL**

### 问题描述
2026-06-17 = **FOMC 利率决议日**。Chairman 今天**漏判了这条**（Mis 掉）。幸好进场时间靠后（没在开会/公布那个时间点进场），**没因这条毁掉账户**——但这是**极大的风险**。

### 核心教训（Chairman）
这恒恒证明：**系统没工作起来时，我们的风险有多大。** 新闻限制是各平台的生死线（T1 News Events：FOMC/CPI/就业报告等），靠人记**必漏**。

### 三条叠加的“人来管”漏洞（今日同时发生）
1. 违反原则进场（行情不适合还做）— 目标压力下破纪律。
2. 风控没设（两平台端都没）— 无自动护栏兑底。
3. FOMC 日漏判 — 生死线靠人记必漏。
→ 三条全是“人来管”的必然漏洞，系统都能堵。今天没爆账是**运气**（进场靠后），不是风控。

### 修复方向
- [ ] 系统接入**经济日历/新闻事件 API**（FOMC/CPI/NFP 等）→ 事件窗口自动拦截进场 / 发警告。
- [ ] 合规检查清单加入“今日是否有 T1 News Event”一项（盘前必查）。
- [ ] 与【CRITICAL Pending】的“有效交易日/最短持仓/新闻限制”合并管理。

---

### C 补充 — 邮件 skill 已连通 (2026-06-15 留痕)
- `himalaya` v1.2.0 已装,已连 Gmail austench1@gmail.com(IMAP 读 ✅ / SMTP 发已配)。
- 密码存 macOS keychain(service: himalaya-austench1),config 不存明文,git 不同步密码。
- 读邮件命令:`himalaya envelope list -a gmail` / `himalaya message read <id> -a gmail`。
- 发邮件:已配但需 Chairman 批准后才发,绝不擅自。
- 状态:管道打通、验证可用。**监控功能仍暂缓,等核心交易系统完成后再展开。**

---

## ISSUE-007 — Tradovate Risk Settings 自动设置缺口

**Raised:** 2026-06-17 | **Status:** OPEN | **Priority:** HIGH

### 问题
Tradovate Risk Settings（每日盈亏自动平仓）无法可靠地自动设置：
- Browser 自动化：canvas UI，坐标盲点，不可靠，成本高
- API：Tradovate 不开放此功能给交易者端
- 结果：系统无法自主完成这个关键的风控配置步骤，违反"减少人为参与"原则

### 影响
每次需要更新风控参数（新账户、每日调整），都要人工登录操作。今天（6/17）因此导致：
- MFF 两个账户花了约 1 小时才设好（本应几分钟）
- TPT 5 个未完成
- 大量 API token 浪费在浏览器截图/重试

### 待解决
- [ ] 研究 Tradovate 是否有隐藏的 WebSocket/内部 API 可用
- [ ] 或通过 NinjaTrader 端设置同等风控（已有 FLATTEN 信号链）
- [ ] 或找到可靠的 browser automation 方案（headless + 稳定 selector）
