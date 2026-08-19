using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.MySql;
using LinqToDB.DataProvider.Oracle;
using LinqToDB.DataProvider.PostgreSQL;
using LinqToDB.DataProvider.SQLite;
using LinqToDB.DataProvider.SqlServer;
using Nop.Core;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;

namespace Nop.Data.DataProviders;

/// <summary>
/// Represents a Tinyid-style (https://github.com/didi/tinyid) segment id generator:
/// identifiers are pre-assigned from a database backed number segment allocator.
/// </summary>
/// <remarks>
/// The generator keeps a local segment (segment size = <see cref="IdGenerationConfig.TinyidStep"/>)
/// and only hits the database when the segment is exhausted, so it is fast and works
/// across multiple instances (each instance allocates a disjoint segment).
/// Ids are sequential Int32 values starting from <see cref="IdGenerationConfig.TinyidStartId"/> + 1.
/// </remarks>
public partial class TinyidIdGenerator : IEntityIdGenerator
{
    #region Fields

    private const string BIZ_TYPE = "nop";
    private const string TABLE_NAME = "nop_tiny_id_info";
    private const int MAX_RETRIES = 10;

    private readonly IdGenerationConfig _config;
    private readonly object _locker = new();
    private long _currentId;
    private long _segmentEnd;

    #endregion

    #region Ctor

    public TinyidIdGenerator(IdGenerationConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        _config = config;
        _currentId = 0;
        _segmentEnd = 0;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets a value indicating whether identifiers are pre-assigned before insert
    /// </summary>
    public bool PreGenerateIds => true;

    #endregion

    #region Methods

    /// <summary>
    /// Generates the next identifier
    /// </summary>
    /// <returns>The generated identifier</returns>
    public int NextId()
    {
        lock (_locker)
        {
            if (_currentId >= _segmentEnd)
                FetchSegment();

            var id = ++_currentId;
            if (id > int.MaxValue)
                throw new NopException(
                    $"Tinyid generated id ({id}) exceeds the Int32 range supported by nopCommerce entities. " +
                    "All ids are allocated from the nop_tiny_id_info segment table; once Int32 is exhausted " +
                    "the segment start must be raised (or ids migrated to Int64).");

            return (int)id;
        }
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Fetches a new id segment from the database. Concurrency safe: the segment
    /// row is bumped with a conditional update, so concurrent instances get disjoint
    /// ranges (a conflicting update affects 0 rows and the fetch is retried).
    /// </summary>
    protected virtual void FetchSegment()
    {
        var step = _config.TinyidStep > 0 ? _config.TinyidStep : 10000;
        var startId = _config.TinyidStartId > 0 ? _config.TinyidStartId : 0;

        using var dataContext = CreateDataConnection();

        for (var i = 0; i < MAX_RETRIES; i++)
        {
            //read the current segment end
            var oldMax = dataContext.Query<long?>($"SELECT max_id FROM {TABLE_NAME} WHERE biz_type = '{BIZ_TYPE}'")
                .FirstOrDefault();

            if (!oldMax.HasValue)
            {
                //no segment row yet: try to initialize it (a concurrent insert on the
                //primary key simply fails and is ignored, the next read sees the row)
                try
                {
                    dataContext.Execute(
                        $"INSERT INTO {TABLE_NAME} (biz_type, max_id, step) VALUES ('{BIZ_TYPE}', {startId}, {step})");
                }
                catch
                {
                    //ignore: another instance created the row concurrently
                }

                continue;
            }

            //conditional bump: only wins if no other instance changed the row
            var affected = dataContext.Execute(
                $"UPDATE {TABLE_NAME} SET max_id = {oldMax.Value + step} WHERE biz_type = '{BIZ_TYPE}' AND max_id = {oldMax.Value}");

            if (affected == 1)
            {
                _currentId = oldMax.Value;
                _segmentEnd = oldMax.Value + step;
                return;
            }
        }

        throw new NopException($"Failed to allocate a Tinyid segment after {MAX_RETRIES} retries");
    }

    /// <summary>
    /// Creates a LinqToDB data connection for the currently configured data provider
    /// </summary>
    protected virtual DataConnection CreateDataConnection()
    {
        var dataSettings = DataSettingsManager.LoadSettings();

        var dataProvider = dataSettings.DataProvider switch
        {
            DataProviderType.SqlServer => SqlServerTools.GetDataProvider(),
            DataProviderType.MySql or DataProviderType.Tidb => MySqlTools.GetDataProvider(MySqlVersion.MySql80),
            DataProviderType.PostgreSQL or DataProviderType.OpenGauss or DataProviderType.GaussDB => PostgreSQLTools.GetDataProvider(),
            DataProviderType.Sqlite or DataProviderType.Unknown => SQLiteTools.GetDataProvider(SQLiteProvider.Microsoft),
            DataProviderType.Oracle => OracleTools.GetDataProvider(OracleVersion.v12),
            _ => throw new NopException($"Unsupported data provider: {dataSettings.DataProvider}")
        } as IDataProvider;

        return new DataConnection(new DataOptions().UseConnectionString(dataProvider, dataSettings.ConnectionString));
    }

    #endregion
}
