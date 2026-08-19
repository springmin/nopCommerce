using System.Runtime.Serialization;

namespace Nop.Data;

/// <summary>
/// Represents data provider type enumeration
/// </summary>
public enum DataProviderType
{
    /// <summary>
    /// Unknown
    /// </summary>
    [EnumMember(Value = "")]
    Unknown,

    /// <summary>
    /// MS SQL Server
    /// </summary>
    [EnumMember(Value = "sqlserver")]
    SqlServer,

    /// <summary>
    /// MySQL
    /// </summary>
    [EnumMember(Value = "mysql")]
    MySql,

    /// <summary>
    /// PostgreSQL
    /// </summary>
    [EnumMember(Value = "postgresql")]
    PostgreSQL,

    /// <summary>
    /// SQLite
    /// </summary>
    [EnumMember(Value = "sqlite")]
    Sqlite,

    /// <summary>
    /// TiDB (MySQL protocol compatible)
    /// </summary>
    [EnumMember(Value = "tidb")]
    Tidb,

    /// <summary>
    /// Oracle
    /// </summary>
    [EnumMember(Value = "oracle")]
    Oracle,

    /// <summary>
    /// openGauss (PostgreSQL protocol compatible, derived from PostgreSQL)
    /// </summary>
    [EnumMember(Value = "opengauss")]
    OpenGauss,

    /// <summary>
    /// Huawei Cloud GaussDB (PostgreSQL-compatible edition)
    /// </summary>
    [EnumMember(Value = "gaussdb")]
    GaussDB
}