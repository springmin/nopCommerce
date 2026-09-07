using Nop.Core;
using Nop.Core.Configuration;
using Yitter.IdGenerator;

namespace Nop.Core.Infrastructure;

/// <summary>
/// Represents the Yitter (https://github.com/yitter/IdGenerator) high-performance
/// id generator: identifiers are pre-assigned before insert.
/// </summary>
/// <remarks>
/// yitter generates 64-bit snowflake ids; nopCommerce entities now use Int64 ids, so
/// the full range is available. To keep ids exact in JavaScript (safe integer range
/// 2^53), WorkerIdBitLength + SeqBitLength must be <= 11 (validated in the ctor).
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

        var workerIdBitLength = (byte)Math.Clamp(config.YitterWorkerIdBitLength, 1, 63);
        var seqBitLength = (byte)Math.Clamp(config.YitterSeqBitLength, 1, 63);

        //keep generated ids within the JavaScript safe integer range (2^53): the
        //timestamp component is 41 bits, so worker + seq bits must be <= 11
        if (workerIdBitLength + seqBitLength > 11)
            throw new NopException($"YitterIdGenerator: WorkerIdBitLength ({workerIdBitLength}) + SeqBitLength ({seqBitLength}) must be <= 11 to keep ids within the JavaScript safe integer range (2^53).");

        var options = new IdGeneratorOptions
        {
            WorkerIdBitLength = workerIdBitLength,
            WorkerId = (ushort)Math.Max(0, Math.Min(config.YitterWorkerId, ushort.MaxValue)),
            SeqBitLength = seqBitLength
        };

        //the base time defaults to the yitter default (2020-01-01); allow the
        //administrator to configure it
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
    public long NextId()
    {
        lock (_locker)
        {
            return YitIdHelper.NextId();
        }
    }

    #endregion
}
