using Nop.Core;
using Nop.Core.Configuration;
using Yitter.IdGenerator;

namespace Nop.Core.Infrastructure;

/// <summary>
/// Represents the Yitter (https://github.com/yitter/IdGenerator) high-performance
/// id generator: identifiers are pre-assigned before insert.
/// </summary>
/// <remarks>
/// yitter generates 64-bit snowflake ids. nopCommerce entities use Int32 ids, so the
/// generated id must fit into Int32. With the default options the timestamp component
/// quickly exceeds Int32; to use this generator either:
///   - set <see cref="IdGeneratorOptions.BaseTime"/> close to the current time so the
///     timestamp component stays small, or
///   - migrate nopCommerce entity ids to Int64 (not yet implemented).
/// When an id exceeds Int32.MaxValue a NopException is thrown.
/// </remarks>
public partial class YitterIdGenerator : IEntityIdGenerator
{
    #region Fields

    private readonly object _locker = new();

    #endregion

    #region Ctor

    public YitterIdGenerator(IdGenerationConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        var options = new IdGeneratorOptions
        {
            WorkerId = (ushort)Math.Max(0, Math.Min(config.YitterWorkerId, ushort.MaxValue)),
            SeqBitLength = (byte)Math.Clamp(config.YitterSeqBitLength, 1, 63)
        };

        //the base time must be close to the current time so generated ids fit into
        //Int32 (see remarks); allow the administrator to configure it
        if (DateTime.TryParse(config.YitterBaseTime, out var baseTime))
            options.BaseTime = baseTime;

        YitIdHelper.SetIdGenerator(options);
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
            var id = YitIdHelper.NextId();

            if (id > int.MaxValue)
                throw new NopException(
                    $"The generated id ({id}) exceeds the Int32 range supported by nopCommerce entities. " +
                    "Set IdGeneratorOptions.BaseTime close to the current time or migrate entity ids to Int64.");

            return (int)id;
        }
    }

    #endregion
}
