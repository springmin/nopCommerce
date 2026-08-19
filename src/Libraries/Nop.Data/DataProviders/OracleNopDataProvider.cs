using System.Data;
using System.Data.Common;
using System.Text;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.Oracle;
using Nop.Core;
using Nop.Data.Mapping;
using Oracle.ManagedDataAccess.Client;

namespace Nop.Data.DataProviders;

/// <summary>
/// Represents the Oracle data provider
/// </summary>
/// <remarks>
/// Oracle support requires an Oracle 12c or newer database (uses identity columns).
/// NOTE: identifiers are handled case-insensitively by Oracle (unquoted identifiers
/// are folded to UPPER case); nopCommerce maps tables/columns in lower case, so this
/// provider relies on FluentMigrator/linq2db generating quoted identifiers. This
/// implementation has been compiled and unit-tested, but NOT yet validated against a
/// real Oracle instance.
/// </remarks>
public partial class OracleNopDataProvider : BaseDataProvider, INopDataProvider
{
    #region Fields

    //it's quite fast hash (to cheaply distinguish between objects)
    protected const string HASH_ALGORITHM = "SHA1";

    #endregion

    #region Methods

    /// <summary>
    /// Gets a connection to the database for a current data provider
    /// </summary>
    /// <param name="connectionString">Connection string</param>
    /// <returns>Connection to a database</returns>
    protected override DbConnection GetInternalDbConnection(string connectionString)
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        return new OracleConnection(connectionString);
    }

    /// <summary>
    /// Creates the database by using the loaded connection string
    /// </summary>
    /// <param name="triesToConnect"></param>
    public virtual void CreateDatabase(int triesToConnect = 10)
    {
        //Oracle databases are instance/schema level objects: a connection string
        //points to an existing service/schema, there is no CREATE DATABASE equivalent
    }

    /// <summary>
    /// Checks if the specified database exists, returns true if database exists
    /// </summary>
    /// <returns>Returns true if the database exists.</returns>
    public virtual bool DatabaseExists()
    {
        try
        {
            using var connection = CreateDbConnection();
            //just try to connect
            connection.Open();

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the specified database exists, returns true if database exists
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the returns true if the database exists.
    /// </returns>
    public virtual async Task<bool> DatabaseExistsAsync()
    {
        try
        {
            await using var connection = GetInternalDbConnection(DataSettings.ConnectionString);

            //just try to connect
            await connection.OpenAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Get the current identity value
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the integer identity; null if cannot get the result
    /// </returns>
    public virtual Task<int?> GetTableIdentAsync<TEntity>() where TEntity : BaseEntity
    {
        using var currentConnection = CreateDataConnection();
        var tableName = NopMappingSchema.GetEntityDescriptor(typeof(TEntity)).EntityName;

        //Oracle 12c+ identity columns have an implicit sequence; USER_TAB_IDENTITY_COLS
        //maps table -> sequence and USER_SEQUENCES exposes its current value
        var result = currentConnection.Query<int?>(
                $@"SELECT s.LAST_NUMBER FROM USER_SEQUENCES s
                   JOIN USER_TAB_IDENTITY_COLS i ON s.SEQUENCE_NAME = i.SEQUENCE_NAME
                   WHERE UPPER(i.TABLE_NAME) = UPPER('{tableName}')")
            .FirstOrDefault();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Set table identity (is supported)
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <param name="ident">Identity value</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public virtual async Task SetTableIdentAsync<TEntity>(int ident) where TEntity : BaseEntity
    {
        var currentIdent = await GetTableIdentAsync<TEntity>();
        if (!currentIdent.HasValue || ident <= currentIdent.Value)
            return;

        using var currentConnection = CreateDataConnection();
        var tableName = NopMappingSchema.GetEntityDescriptor(typeof(TEntity)).EntityName;

        //Oracle 18c+ allows restarting the identity sequence via ALTER TABLE MODIFY
        await currentConnection.ExecuteAsync(
            $"ALTER TABLE \"{tableName}\" MODIFY (\"Id\" GENERATED BY DEFAULT AS IDENTITY (START WITH {ident}))");
    }

    /// <summary>
    /// Creates a backup of the database
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public virtual Task BackupDatabaseAsync(string fileName)
    {
        throw new DataException("This database provider does not support backup");
    }

    /// <summary>
    /// Restores the database from a backup
    /// </summary>
    /// <param name="backupFileName">The name of the backup file</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public virtual Task RestoreDatabaseAsync(string backupFileName)
    {
        throw new DataException("This database provider does not support backup");
    }

    /// <summary>
    /// Re-index database tables
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public virtual async Task ReIndexTablesAsync()
    {
        using var currentConnection = CreateDataConnection();
        var tables = currentConnection.Query<string>(
                "SELECT TABLE_NAME FROM USER_TABLES ORDER BY TABLE_NAME")
            .ToList();

        foreach (var table in tables)
            await currentConnection.ExecuteAsync($"ALTER TABLE \"{table}\" ENABLE ROW MOVEMENT");
    }

    /// <summary>
    /// Shrinks database
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public virtual Task ShrinkDatabaseAsync()
    {
        throw new DataException("This database provider does not support database shrinking");
    }

    /// <summary>
    /// Gets the database size in Kb
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the database size
    /// </returns>
    public virtual async Task<long> GetDatabaseSizeAsync()
    {
        using var currentConnection = CreateDataConnection();
        var result = await currentConnection.QueryToListAsync<long>(
            "SELECT NVL(SUM(bytes) / 1024, 0) FROM USER_SEGMENTS");

        return result.FirstOrDefault();
    }

    /// <summary>
    /// Build the connection string
    /// </summary>
    /// <param name="nopConnectionString">Connection string info</param>
    /// <returns>Connection string</returns>
    public virtual string BuildConnectionString(INopConnectionStringInfo nopConnectionString)
    {
        ArgumentNullException.ThrowIfNull(nopConnectionString);

        if (nopConnectionString.IntegratedSecurity)
            throw new NopException("Data provider supports connection only with login and password");

        var builder = new OracleConnectionStringBuilder
        {
            DataSource = nopConnectionString.ServerName,
            UserID = nopConnectionString.Username,
            Password = nopConnectionString.Password
        };

        return builder.ConnectionString;
    }

    /// <summary>
    /// Gets the name of a foreign key
    /// </summary>
    /// <param name="foreignTable">Foreign key table</param>
    /// <param name="foreignColumn">Foreign key column name</param>
    /// <param name="primaryTable">Primary table</param>
    /// <param name="primaryColumn">Primary key column name</param>
    /// <returns>Name of a foreign key</returns>
    public virtual string CreateForeignKeyName(string foreignTable, string foreignColumn, string primaryTable, string primaryColumn)
    {
        //Oracle supports identifiers up to 128 bytes (12c+), so the readable
        //name can be used directly (unlike MySQL's 64 char limit)
        return $"FK_{foreignTable}_{foreignColumn}_{primaryTable}_{primaryColumn}";
    }

    /// <summary>
    /// Gets the name of an index
    /// </summary>
    /// <param name="targetTable">Target table name</param>
    /// <param name="targetColumn">Target column name</param>
    /// <returns>Name of an index</returns>
    public virtual string GetIndexName(string targetTable, string targetColumn)
    {
        return $"IX_{targetTable}_{targetColumn}";
    }

    /// <summary>
    /// Gets the name of the database collation
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the database collation
    /// </returns>
    public virtual Task<string> GetDataBaseCollationAsync()
    {
        using var currentConnection = CreateDataConnection();
        var result = currentConnection.Query<string>(
                "SELECT VALUE FROM NLS_DATABASE_PARAMETERS WHERE PARAMETER = 'NLS_SORT'")
            .FirstOrDefault();

        return Task.FromResult(result);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Linq2Db data provider
    /// </summary>
    protected override IDataProvider LinqToDbDataProvider => OracleTools.GetDataProvider(OracleVersion.v12);

    /// <summary>
    /// Gets allowed a limit input value of the data for hashing functions, returns 0 if not limited
    /// </summary>
    public int SupportedLengthOfBinaryHash { get; } = 0;

    /// <summary>
    /// Gets a value indicating whether this data provider supports backup
    /// </summary>
    public virtual bool BackupSupported => false;

    #endregion
}
