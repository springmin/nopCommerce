using Nop.Core;

namespace Nop.Plugin.Misc.Polls.Domain;

/// <summary>
/// Represents a poll voting record
/// </summary>
public class PollVotingRecord : BaseEntity
{
    /// <summary>
    /// Gets or sets the poll answer identifier
    /// </summary>
    public long PollAnswerId { get; set; }

    /// <summary>
    /// Gets or sets the customer identifier
    /// </summary>
    public long CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the date and time of instance creation
    /// </summary>
    public DateTime CreatedOnUtc { get; set; }
}