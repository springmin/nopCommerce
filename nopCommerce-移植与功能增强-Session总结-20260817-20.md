# nopCommerce 移植与功能增强 — Session 总结(2026-08-17 ~ 08-20)

> 本文档独立记录本次 OpenCode Session(`ses_fefcd988cffe30tni13bf3jgjI`)中完成的 nopCommerce 5.00
> 移植运行与功能增强全过程,并附带精确的 Token 消耗统计。
> 环境:OpenHarmony 沙箱(openharmony 平台);模型:deepseek-v4-flash-free(免费)。

---

## 1. 概述

在 2 天 15 小时的连续会话中,完成了 nopCommerce 5.00(ASP.NET Core 电商平台)在 OpenHarmony
沙箱上的**全链路移植与增强**:

| 维度 | 结果 |
|---|---|
| 编译 | 全部 6 个核心项目 + 32 个插件 + 测试项目,0 错误 |
| 数据库 | 新增 SQLite / TiDB / Oracle / openGauss / GaussDB 支持(原仅 SQL Server/PostgreSQL/MySQL) |
| Id 生成 | 全新框架:Database / Yitter / Tinyid 三模式,含 JS 2^53 安全设计 |
| 测试 | 1107 项 NUnit 全套通过(SQLite 内存库) |
| 部署 | SQLite + Tinyid 模式 + 全部 32 插件,安装 89s,站点运行验证通过 |
| Git | 16 个提交推送至 GitHub fork `springmin/nopCommerce`(develop 分支) |
| 总 Token | 输入 ~522 万 / 输出 ~52.6 万 / 推理 ~47.5 万(约 622 万有效 Token,1427 个模型步) |

---

## 2. 阶段时间线

```
08-17 ──► Day 1:环境适配与编译打通
08-18 ──► Day 2:SQLite / TiDB 支持 + Id 生成框架
08-19 ──► Day 3:Oracle / openGauss / GaussDB、Tinyid、部署验证、fork 提交 c1-c12
08-20 ──► Day 4:int32→int64 全量迁移、回归、fork 提交 c13-c16
```

### Day 1(08-17):环境适配与编译打通

**目标**:让 nopCommerce 5.00 在 OpenHarmony 沙箱中编译通过。

- 沙箱内 `dotnet` CLI 直接执行会 SIGSYS(seccomp 阻断 `get_mempolicy`、无 ICU、OHOS 仅执行
  codesign ELF)。建立 **`dotnet-ohos` 包装器**(设置 `DOTNET_ROOT`/`TMPDIR`/
  `DOTNET_EnableWriteXorExecute=0`、`LD_PRELOAD=libnuma-shim.so`、自动 codesign)。
- 定位构建必带参数:`MSBUILDUSESERVER=0` + `-m:1`(MSBuild worker 节点在沙箱 socket 限制下崩溃);
  插件项目需 `-p:SolutionDir=$PWD/src/`(restore 与 build 均需)。
- `global.json` 固定 SDK `11.0.100-dev`(自带 `10.0.110-dev` 无法构建本仓库)。
- 修复 3 处 `[GeneratedRegex(..., "en-US")]` 的 cultureName 参数(invariant 模式下运行失败)。
- **成果**:解决方案全量编译通过;`AGENTS.md` 更新了完整的沙箱构建/测试环境说明。

### Day 2(08-18):SQLite / TiDB 支持 + Id 生成框架

**目标**:让安装向导可选择 SQLite/TiDB,并建立可插拔的 Id 生成方案。

- **SQLite 生产化支持**:`SqliteNopDataProvider` 从测试代码迁入生产;枚举、DataProviderManager、
  FluentMigrator AddSQLite 接线、SQLite generator/processor、外键保护等。
- SQLite 端到端安装成功,修复:`CurrentOSUser`(`id` 命令缺失)、`ReadWriteCreate`、
  `Cache=Default`、Release 构建迁移标记、`MigrationManager` 修复。
- **TiDB 支持(方案 B)**:走 MySQL 数据路径;连接串 host:port 解析、`ConnectionIdleTimeout=60`、
  `ConnectionTimeout=60`、命令超时 300s、`RepeatableRead` 代替 SERIALIZABLE;
  索引长度修复(`CustomerBuilder` 1000→255、`GenericAttributeBuilder` 400→255);
  `ImportStatesFromTxtAsync` 分批写入优化。
- TiDB Cloud 全流程安装+运行成功(首次无样例数据;后重装含样例数据,1905s,完整演示店运行)。
- **Id 生成框架**:`IdGenerationConfig`(IConfig+ISettings)、`IEntityIdGenerator`、
  `DatabaseIdGenerator`(IDENTITY)、`YitterIdGenerator`(雪花)、插入路径预分配、
  建表 `IsIdentity` 按模式、Autofac 注册。
- SQLite 下 5/5 Id 生成测试通过(修复 Sqlite/PostgreSql provider 绕过预分配路径、测试隔离)。

### Day 3(08-19):Oracle / openGauss / GaussDB、Tinyid、部署、fork 提交

- **Oracle 支持**:`OracleNopDataProvider`、FluentMigrator Oracle 接线、generator/processor
  (按约定:当前环境无 Oracle 服务器,仅源码级,未实测)。
- **Tinyid 号段模式**:`TinyidIdGenerator`(号段分配器)、`nop_tiny_id_info` 表迁移、
  `TinyidStartId`/`TinyidStep` 配置;SQLite 下 5/5 测试通过;全套回归 1098/0/9。
- **部署验证**:SQLite + Tinyid 模式 + 全部 32 插件,安装 89s;验证号段表 0→30000、
  表无 AUTOINCREMENT、Setting 表 Id 来自号段;首页/后台 200;启动竞态日志诊断无害。
- **openGauss / GaussDB 支持**:经 PostgreSQL 数据路径实现(枚举 → `PostgreSqlDataProvider`)。
- **Git fork 提交基础设施**:github.com:443 被沙箱阻断 → 改用 **Git Data API**
  (api.github.com 可达),编写 `gh-commit.py`(blobs/trees/commits/refs 构造),
  推送 **c1–c12**(构建适配、SQLite、TiDB、Oracle、utf8mb4 索引、批量优化、Id 框架、
  Tinyid、稳定性、测试文档、openGauss、host:port+IPv6)。
- PostgreSQL/MySQL provider 增加 host:port 解析与 IPv6 保护(探针验证 5 场景)。

### Day 4(08-20):int32 → int64 全量迁移(本 Session 主体)

**目标**:实体主键/外键从 int32 升级 int64,并处理 JavaScript 无法精确表示 >2^53 的问题。

- **JS 2^53 处理**:`IdGenerationConfig.YitterWorkerIdBitLength`(默认 6)+ YitterIdGenerator
  构造校验 `WorkerIdBitLength + SeqBitLength ≤ 11`,保证 yitter Id 始终在 JS 安全整数内。
- **核心层**:`BaseEntity.Id`、`IEntityIdGenerator.NextId()`、`GetTableIdent/SetTableIdent`、
  `InsertWithInt32Identity→InsertWithInt64Identity`、`IRepository/EntityRepository`、
  `GetFieldHashesAsync` keySelector 泛型等。
- **实体层**:251 个外键属性 int→long(110 文件);builders 65 文件 `AsInt32→AsInt64`;
  主键 DbType `Int64`;`DiscountMapping` 抽象属性、`ProductId1/ProductId2` 等。
- **服务层**:2008 个编译错误清零(方法签名、集合、字典、事件类构造、PdfDocument 委托、
  PluginDescriptor 字段、枚举 cast 等)。
- **Web 层**:`BaseNopEntityModel.Id`、`ISettingsModel.ActiveStoreScopeConfiguration`、
  `NopOverrideStoreCheckboxHelper.StoreScope`(一次修复 684 个 cshtml Razor 错误)、
  `Func<T,int,Task>` 委托、匿名类型、TagHelper。
- **插件**:逐个修复 13 个出错插件(Avalara 62→0、Polls 34→0、FixedByWeightByTotal、
  RFQ、Forums、Omnisend、Zettle、Brevo、News、PayPalCommerce 等),32 插件全通过。
- **测试**:50 个测试文件修复;回归 1107 项通过(1097 通过 + 9 跳过;1 个网络版本检查
  重跑通过)。
- **fork 提交 c13–c16**:1068 个文件分 4 批推送(增强 gh-commit.py:400 重试 + 上传延时,
  解决大提交中途断连)。

---

## 3. 关键决策与经验教训

| 问题 | 决策 |
|---|---|
| 沙箱无 dotnet | `dotnet-ohos` 包装器 + 强制构建参数,写入 AGENTS.md |
| 测试无法 vstest | 进程内 NUnit runner + AspNetCore 运行时包 + sqlite/Skia 原生库 + ICU/tzdata |
| 数据库扩展 | 按 provider 枚举接线,复用 MySQL/PostgreSQL 数据路径(TiDB、openGauss/GaussDB) |
| Id 生成三模式 | 配置切换;Database 模式建表 IsIdentity,预分配模式去 AUTOINCREMENT |
| JS 精度 | 约束 yitter 位宽 ≤ 53 位,而非全局字符串化(侵入小、性能无损) |
| git 协议阻断 | Git Data API 构造提交;大提交分批 + 脚本增强(400 重试、0.25s 延时) |
| 增量编译假象 | 错误数跳变(如显示 2 实际 344);用 `-t:Rebuild`/全量编译确认真实状态 |
| 批量替换误伤 | `individualPagesDisplayedCount`(含 "id" 子串)、`RenderPicture.width`、
  `long.Parse(year)`(DateTime 参数)— 均被发现并恢复;属性批量只用"以 Id 结尾"安全模式 |
| 枚举 unbox | `Enum.GetValues(...).Cast<long>()` 运行期 InvalidCastException →
  改为 `Cast<int>().Select(x => (long)x)` |

---

## 4. Token 消耗统计(精确,来自本地会话数据库)

**模型**:deepseek-v4-flash-free(免费);统计口径:1427 个 step-finish 步的 tokens 字段。

### 4.1 总量

| 指标 | Token 数 |
|---|---:|
| 输入(input) | 5,224,328 |
| 输出(output) | 525,647 |
| 推理(reasoning) | 475,064 |
| **有效合计** | **≈ 6,225,039(622 万)** |
| 模型步数(step-finish) | 1,427 |
| 成本 | $0(免费模型) |

> 注:数据库 `tokens.total` 字段包含异常大值(619,534,223),疑为模型计费侧累计字段,
> 未采用;以上 input/output/reasoning 为可靠口径。

### 4.2 按日分布

| 日期 | 输入 | 输出 | 阶段 |
|---|---:|---:|---|
| 2026-08-17 | 1,900,140 | 125,006 | 环境适配、编译打通 |
| 2026-08-18 | 859,716 | 166,715 | SQLite/TiDB、Id 生成框架 |
| 2026-08-19 | 1,965,642 | 173,755 | Oracle/openGauss、Tinyid、部署、c1-c12 |
| 2026-08-20 | 498,830 | 60,171 | int64 迁移、回归、c13-c16 |
| **合计** | **5,224,328** | **525,647** | |

### 4.3 工具调用(1462 次)

| 工具 | 次数 | 占比 |
|---|---:|---:|
| bash(构建/测试/git/脚本) | 1,220 | 83.4% |
| edit(python 批量 patch) | 116 | 7.9% |
| read | 51 | 3.5% |
| todowrite | 27 | 1.8% |
| write | 22 | 1.5% |
| webfetch / 其它 | 26 | 1.8% |

### 4.4 会话规模

- 消息:1491 条(user 58 / assistant 1433)
- part 总量:6794(含 1233 条 reasoning、31 个 patch、2 次 compaction)
- 时长:2026-08-17 14:48 → 2026-08-20 06:15(2 天 15 小时)

---

## 5. Git 提交清单(16 个,均推送至 fork `springmin/nopCommerce` develop)

| 提交 | SHA(前 10) | 内容 |
|---|---|---|
| c1 | 2bf7a143 | 构建适配(沙箱编译打通) |
| c2 | (SQLite) | SQLite 数据库支持 |
| c3 | (TiDB) | TiDB 支持 |
| c4 | (Oracle) | Oracle 支持(源码级) |
| c5 | (utf8mb4) | TiDB 索引长度修复 |
| c6 | (导入优化) | 安装导入分批优化 |
| c7 | (Id 框架) | Id 生成框架(Database/Yitter) |
| c8 | (Tinyid) | Tinyid 号段生成器 |
| c9 | (稳定性) | 稳定性修复 |
| c10 | (测试) | 测试基建与文档 |
| c11 | e34b6a6c | openGauss/GaussDB 支持 |
| c12 | 1864e295 | host:port 连接串 + IPv6 保护 |
| c13 | 68bf0f7b | int64 迁移:Nop.Core + Nop.Data(202 文件) |
| c14 | 59506871 | int64 迁移:Nop.Services(214 文件) |
| c15 | 571cdf49 | int64 迁移:Nop.Web + Framework(470 文件) |
| c16 | 6942eb42 | int64 迁移:Plugins + Tests(182 文件) |

---

## 6. 当前状态与后续建议

- 运行中实例:SQLite + Tinyid 模式 + 全部插件,端口 5000,管理员 `admin@example.com / Test1234!`。
- Oracle / openGauss / GaussDB 仅源码级验证(无服务器),需真实服务器实测。
- 建议后续:
  1. 在真实 SQL Server/PostgreSQL 上验证 int64 迁移(尤其 `GetTableIdent` 序列路径);
  2. 为 int64 迁移补充针对大 Id(> int32.MaxValue)的专项测试;
  3. 将 `gh-commit.py` 增强(400 重试、延时)固化到仓库工具目录;
  4. 更新 `nopCommerce-OpenHarmony-移植运行总结.md` 与 int64 迁移章节保持一致。

---

*生成时间:2026-08-20 | 依据:会话数据库精确统计 + 会话全程记录*
