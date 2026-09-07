# nopCommerce 在 OpenHarmony 沙箱的移植、测试与运行总结

> 记录在 OpenHarmony 设备上完成 nopCommerce 5.00 的编译、1102 项测试、SQLite 与 TiDB 数据库支持及网站运行的全过程。
> 日期:2026-08-18 · 工作目录:`/storage/Users/currentUser/springsources/nopCommerce`

---

## 1. 概述

本环境为 OpenHarmony 沙箱,与标准 Linux 有大量差异。整个工作围绕 **让 nopCommerce 5.00 在这台设备上"能编译、能测试、能运行"** 展开,最终达成:

| 目标 | 结果 |
|---|---|
| 编译 | ✅ 42 个项目(6 核心库 + 30 插件 + Web + Tests)全部成功 |
| 测试 | ✅ 1102 项:NUnit 套件 **1093 通过 / 0 失败 / 9 跳过(Ignored)** |
| SQLite 支持 | ✅ 新增数据库支持(官方仅测试项目内置 provider),网站完整运行 |
| TiDB 支持 | ✅ 方案 B 实施,TiDB Cloud Serverless 上完整安装 + 网站运行 |

---

## 2. 环境背景与平台限制

| 限制 | 说明 | 应对 |
|---|---|---|
| `dotnet` 直接崩溃(SIGSYS) | 沙箱 seccomp 阻止 `get_mempolicy` 等系统调用 | `dotnet-ohos` wrapper(设 `DOTNET_ROOT`/`TMPDIR`/`DOTNET_EnableWriteXorExecute=0`/`LD_PRELOAD=libnuma-shim.so` + 自动 codesign) |
| ELF 需 codesign | OHOS 只执行带 `.codesign` 段的 ELF | `binary-sign-tool` 签名所有原生二进制 |
| MSBuild 多进程崩溃 | named-pipe socket 被禁,worker 节点全挂 | `MSBUILDUSESERVER=0` + `-m:1`(单进程) |
| SDK 残缺 | 10.0.110-dev 缺 workload locator,无法构建 | `global.json` 改用 **11.0.100-dev**(输出仍 net10.0) |
| 无 ICU | 系统无 libicu | **发现 harmonybrew 自带 ICU 78** → 取消 invariant 模式,`LD_LIBRARY_PATH` 指向 |
| 无 tzdata | 无 `/usr/share/zoneinfo`(只读) | PyPI `tzdata` wheel → `TZDIR=~/.dotnet/test-tools/zoneinfo` |
| 无 `id` 命令 | 安装向导权限检查崩溃 | toybox `id` shim → `~/.dotnet/tools/id` |
| 路径翻译 | `/proc/self/exe` 返回沙箱外真实路径 | vstest 无法用 → 自写进程内 NUnit runner |
| OHOS 无 aspnetcore | `linux-ohos-arm64` 渠道不发布 AspNetCore.App | 托管 dll 取自 `linux-musl-arm64` runtime pack,组装共享框架 |

---

## 3. 任务一:编译

### 3.1 关键问题

- **SDK 选择**:10.0.110-dev 是残缺构建(缺 `WorkloadAutoImportPropsLocator` 等,官方 tarball 本身残缺),11.0.100-dev 正常。
- **插件构建**:插件 csproj 的 `OutputPath` 与引用依赖 `$(SolutionDir)`,单独构建时为空 → restore 与 build 都必须传 `/p:SolutionDir=/storage/Users/currentUser/springsources/nopCommerce/src/`。
- **GeneratedRegex**:`DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1` 下 `[GeneratedRegex(..., "en-US")]` 报 SYSLIB1042 → 移除 cultureName 参数(纯 ASCII 正则,行为不变)。

### 3.2 修改的源文件

| 文件 | 改动 |
|---|---|
| `global.json` | SDK 固定为 `11.0.100-dev`(`latestPatch` + `allowPrerelease`) |
| `src/Libraries/Nop.Core/CommonHelper.cs` | GeneratedRegex 移除 cultureName |
| `src/Plugins/Nop.Plugin.Misc.Forums/Services/BBCodeHelper.cs` | 同上(10 处) |
| `src/Plugins/Nop.Plugin.Misc.Forums/Services/CSharpFormat.cs` | 同上 |

### 3.3 结果

42 个项目全部 `Build succeeded`,30 个插件产物部署到 `Nop.Web/Plugins/<SystemName>/`(含 `plugin.json`)。

---

## 4. 任务二:测试套件(1102 项)

### 4.1 vstest 不可用的根因

vstest 的 `DotnetTestHostManager` 通过 `Process.MainModule`(`/proc/self/exe`)解析 dotnet 路径,沙箱返回**未翻译的真实路径**(`/data/service/el2/...`),该路径在沙箱内不可见 → testhost 无法启动。尝试过的方案:

- ❌ `LD_PRELOAD` readlink shim(.NET 绕过 libc)
- ❌ `--arch x64` 强制走 DOTNET_ROOT 分支(架构校验拒绝 arm64)
- ❌ `DotnetHostPath` runsettings(该版本 vstest 不支持)

### 4.2 解决方案:进程内 NUnit Runner

自写 `nunit-runner`(`~/.dotnet/test-tools/nunit-runner.dll`,源码 `nunit-runner-src.cs`),基于 NUnit 4.5.1 的 `NUnitTestAssemblyRunner`,进程内运行,完全绕过 vstest。

### 4.3 补齐的运行时缺口

| 缺口 | 来源 |
|---|---|
| AspNetCore.App 托管 dll(143 个) | `Microsoft.AspNetCore.App.Runtime.linux-musl-arm64/10.0.10` NuGet 包,runner 的 `AssemblyResolve` 兜底 |
| `e_sqlite3.so` / `libe_sqlite3.so` | harmonybrew 的 musl `libsqlite3.so` 复制(已 codesign) |
| `libSkiaSharp.so` | `SkiaSharp.NativeAssets.Linux.NoDependencies` 的 `linux-musl-arm64` 版 + 重新签名 |
| ICU | `LD_LIBRARY_PATH=~/.harmonybrew/lib`,**取消** `DOTNET_SYSTEM_GLOBALIZATION_INVARIANT` |
| tzdata | `TZDIR=~/.dotnet/test-tools/zoneinfo`(PyPI `tzdata` wheel) |

### 4.4 运行方式

```sh
cd src/Tests/Nop.Tests/bin/Debug/net10.0
export DOTNET_ROOT=~/.dotnet TMPDIR=/data/storage/el2/base/tmp DOTNET_EnableWriteXorExecute=0
export LD_LIBRARY_PATH=~/.harmonybrew/lib LD_PRELOAD=$DOTNET_ROOT/libnuma-shim.so
export TZDIR=~/.dotnet/test-tools/zoneinfo
unset DOTNET_SYSTEM_GLOBALIZATION_INVARIANT
dotnet exec ~/.dotnet/test-tools/nunit-runner.dll ./Nop.Tests.dll            # 全部
dotnet exec ~/.dotnet/test-tools/nunit-runner.dll ./Nop.Tests.dll OrderServiceTests  # 单个夹具
```

### 4.5 结果

```
total=1102 passed=1093 failed=0 skipped=9 (Ignored)
```

---

## 5. 任务三:SQLite 数据库支持 + 网站运行

### 5.1 分析结论

nopCommerce 5.00 的 SQLite provider(`SqLiteNopDataProvider`,447 行完整实现)**只存在于测试项目**,生产代码未接线。安装向导下拉由 `Enum.GetValues(typeof(DataProviderType))` 自动生成,加枚举值即可出现选项。

### 5.2 代码改动

| 文件 | 改动 |
|---|---|
| `DataProviderType.cs` | 枚举加 `[EnumMember(Value="sqlite")] Sqlite` |
| `DataProviders/SqliteNopDataProvider.cs` | **新增**(自测试迁入生产,命名空间调整) |
| `DataProviderManager.cs` | switch 加 `Sqlite => new SqliteNopDataProvider()` |
| `FluentMigratorExtensions.cs` | `AddNopDbEngines` 加 `AddSQLite()`(安装前/后两分支) |
| `NopGeneratorAccessor.cs` / `NopProcessorAccessor.cs` | 加 `GeneratorIdConstants.SQLite` / `ProcessorIdConstants.SQLite` |
| `AddOrderRewardPointsHistoryFK.cs` | 外键守卫扩展为 `Unknown or Sqlite`(SQLite 不支持 ALTER ADD FK) |
| `Nop.Data.csproj` | 加 `FluentMigrator.Runner.SQLite` + `Microsoft.Data.Sqlite` |
| `NopDataProviderTests.cs` | 连接串断言更新 |

### 5.3 端到端验证中修复的生产问题

| 问题 | 修复 |
|---|---|
| `CurrentOSUser.PopulateLinuxUser()` 无 `id` 命令 → IndexOutOfRange | 防御性解析 + toybox `id` shim(`~/.dotnet/tools/id`) |
| SQLite `Mode=ReadWrite` 无法打开不存在的文件 | `ReadWriteCreate` |
| `Cache=Shared` 多连接并发写 → `SQLITE_LOCKED` | `Cache=Default` + `DefaultTimeout=60` |
| Debug 构建下当前版本 Update 迁移不标记 → 启动重跑 → 死锁 | **用 Release 构建运行**(官方设计:Release 下 `#if DEBUG` 不编译) |
| `MigrationManager.GetMigrations` 过滤遗漏 NoMatter 迁移 | 安装标记时放行 NoMatter |

### 5.4 结果

SQLite 完整安装成功(建库 `App_Data/nopshop.sqlite` 13.6MB),网站运行,首页 200。

---

## 6. 任务四:TiDB 数据库支持(方案 B)+ 网站运行

### 6.1 可行性分析

TiDB 兼容 MySQL 协议与 MySQL 8.0 语法 → 复用 nopCommerce 的 MySQL 全链路(MySqlConnector + LinqToDB MySql80 + FluentMigrator MySql8),无需新 provider。方案 B = 枚举 + 映射(约 10 处小改动)。

### 6.2 代码改动

| 文件 | 改动 |
|---|---|
| `DataProviderType.cs` | 枚举加 `[EnumMember(Value="tidb")] Tidb`(安装向导选项 5) |
| `DataProviderManager.cs` | `Tidb => new MySqlNopDataProvider()` |
| `FluentMigratorExtensions.cs` / `NopGeneratorAccessor.cs` / `NopProcessorAccessor.cs` | `Tidb => AddMySql8()` / MySql8 generator/processor |
| `NopMySql8TypeMap.cs` | 守卫改为 `is not (MySql or Tidb)` |
| `MySqlDataProvider.cs` | `host:port` 解析(MySqlConnector 不解析该语法);`ConnectionIdleTimeout=60`;`ConnectionTimeout=60`(Serverless 冷启动);命令超时兜底 300s;**`CreateTransactionScope` 用 REPEATABLE READ**(TiDB 不支持 SERIALIZABLE) |
| `CustomerBuilder.cs` | `Username/Email/EmailToRevalidate`:VARCHAR(1000)→**255**(utf8mb4 索引 3072 字节限制) |
| `GenericAttributeBuilder.cs` | `KeyGroup/Key`:400→**255**(组合索引 3204→2044 字节) |
| `InstallRequiredData.cs` | `ImportStatesFromTxtAsync` **批量化**:601 行 × 3 次往返 → 2 次查询 + 2 次批量写(严格等价,回归验证) |
| `NopDataProviderTests.cs` / `TestDataProviderManager.cs` | 连接串断言更新 + Tidb 分支 |

### 6.3 真实 TiDB Cloud Serverless 验证中克服的问题

| 问题 | 修复 | 效果 |
|---|---|---|
| 索引超长(4000/3204 字节 > 3072) | 列长度修复 | 128 表 + 索引 + 外键迁移成功 |
| SERIALIZABLE 不支持 | 隔离级别改 REPEATABLE READ | 启动不再崩溃 |
| **~20 分钟会话中断**(Serverless 基础设施限制) | 安装侧:`ImportStatesFromTxtAsync` 批量化;启动侧:32 个插件默认禁用(`DisabledPlugins`) | 安装从 20+ 分钟(必断)→ **291 秒**;启动 <2 分钟 |
| 340 秒空闲断连、冷启动慢 | 连接池参数适配 | 连接稳定 |

### 6.4 连接串说明

用户提供的 TiDB Cloud 连接串:
```
mysql://33tbZWiTMRxa4RE.root:08PeeyXHZwWNjnwV@gateway01.ap-southeast-1.prod.alicloud.tidbcloud.com:4000/sys
```
安装时:安装向导选 **Tidb**,ServerName 填 `gateway01...tidbcloud.com:4000`,DatabaseName 填新库名(如 `nopcommerce`),CharacterSet=`utf8mb4`,Collation=`utf8mb4_general_ci`(规避 TiDB 默认 `utf8mb4_bin` 大小写敏感)。

### 6.5 结果

```
✅ 安装:POST /install 291s(128 表 + 索引 + 外键 + 必需数据)
✅ 启动:Application started
✅ 首页:HTTP 200 "Your store. Home page title"
✅ 后台:HTTP 200 (/admin)
✅ 数据:PermissionRecord 126、Setting 910、LocaleStringResource 7047、StateProvince 1956
✅ 管理员:admin@example.com (Test1234!) active=True
✅ 测试回归:1093/0/9 全绿
```

---

## 7. 环境工具与脚本

| 工具 | 位置 | 说明 |
|---|---|---|
| `dotnet-ohos` | `~/.dotnet/dotnet-ohos` | dotnet wrapper(env + shim + codesign) |
| `install-dotnet-ohos.sh` | `~/Download/install-dotnet-ohos.sh` | Runtime/SDK 一键安装(多包、类型识别、SDK 冒烟测试检出残缺包) |
| `nunit-runner.dll` | `~/.dotnet/test-tools/` | 进程内 NUnit runner(源码 `nunit-runner-src.cs`) |
| `id` shim | `~/.dotnet/tools/id` | toybox id 包装 |
| `zoneinfo/` | `~/.dotnet/test-tools/zoneinfo` | tzdata(PyPI wheel 解压) |

---

## 8. 代码改动总清单(~20 个文件)

**核心功能(SQLite + TiDB)**:`DataProviderType.cs`、`SqliteNopDataProvider.cs`(新)、`DataProviderManager.cs`、`FluentMigratorExtensions.cs`、`NopGeneratorAccessor.cs`、`NopProcessorAccessor.cs`、`NopMySql8TypeMap.cs`、`AddOrderRewardPointsHistoryFK.cs`、`Nop.Data.csproj`、`MySqlDataProvider.cs`、`InstallRequiredData.cs`

**数据层适配**:`CustomerBuilder.cs`、`GenericAttributeBuilder.cs`、`MigrationManager.cs`、`CurrentOSUser.cs`

**构建/全局**:`global.json`

**测试**:`NopDataProviderTests.cs`、`TestDataProviderManager.cs`、`SqLiteNopDataProvider.cs`(删除,迁入生产)

**正则适配**:`CommonHelper.cs`、`BBCodeHelper.cs`、`CSharpFormat.cs`

---

## 9. 当前运行状态

| 项 | 状态 |
|---|---|
| 网站(TiDB) | ✅ 运行中:`http://127.0.0.1:5000`(TiDB Cloud,库 `nopcommerce`) |
| 管理员 | `admin@example.com` / `Test1234!` |
| 插件 | 默认全部禁用(避免启动迁移超时;后台可逐个启用) |
| 历史 SQLite 站点 | 已被 TiDB 实例取代(配置指向 TiDB) |

**网站启动命令**:

```sh
cd src/Presentation/Nop.Web
export DOTNET_ROOT=~/.dotnet TMPDIR=/data/storage/el2/base/tmp DOTNET_EnableWriteXorExecute=0
export LD_LIBRARY_PATH=~/.harmonybrew/lib LD_PRELOAD=$DOTNET_ROOT/libnuma-shim.so
export TZDIR=~/.dotnet/test-tools/zoneinfo
unset DOTNET_SYSTEM_GLOBALIZATION_INVARIANT
dotnet bin/Release/net10.0/Nop.Web.dll
```

---

## 10. 已知限制与后续建议

1. **TiDB Cloud Serverless 会话中断(~20 分钟)**:安装/启动已通过优化规避;但后台启用插件时,插件迁移需在窗口内完成,建议逐个启用。
2. **插件默认禁用**:核心功能完整;如需插件,在 `App_Data/appsettings.json` 的 `InstallationConfig.DisabledPlugins` 中移除对应项后重启。
3. **SQLite 限制**:不支持数据库备份/恢复、存储过程。
4. **TiDB 限制**:后台"重建索引"(`OPTIMIZE TABLE`)不可用;排序规则需显式指定 `utf8mb4_general_ci` 等(安装向导 Collation 字段)。
5. **自托管 TiDB / 付费版**:无 Serverless 会话中断限制,体验更稳定。
6. **Debug 构建**:网站运行请用 **Release**(Debug 下当前版本 Update 迁移会在每次启动重跑,SQLite 上会死锁)。
7. **`dotnet test`/vstest**:本环境不可用,测试请用 `nunit-runner`(见 AGENTS.md)。
