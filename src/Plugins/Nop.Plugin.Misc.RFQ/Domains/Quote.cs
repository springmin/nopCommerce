using Nop.Core;

namespace Nop.Plugin.Misc.RFQ.Domains;

/// <summary>
/// Represent the quote entity
/// </summary>
public class Quote : BaseEntity, IAdminNote
{
    /// <summary>
    /// Gets or sets the customer identifier
    /// </summary>
    public long CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the created date and time
    /// </summary>
    public DateTime CreatedOnUtc { get; set; }

    /// <summary>
    /// Gets or sets the quote expiration date and time
    /// </summary>
    public DateTime? ExpirationDateUtc { get; set; }

    /// <summary>
    /// Gets or sets the quote status identifier
    /// </summary>
    public long StatusId { get; set; }

    /// <summary>
    /// Gets or sets the request status
    /// </summary>
    public QuoteStatus Status
    {
        get => (QuoteStatus)StatusId;
        set => StatusId = (int)value;
    }

    /// <summary>
    /// Gets or sets the request a quote identifier
    /// </summary>
    public long? RequestQuoteId { get; set; }

    /// <summary>
    /// Gets or sets the administration notes
    /// </summary>
    public string AdminNotes { get; set; }

    /// <summary>
    /// Gets or sets the order id
    /// </summary>
    public long? OrderId { get; set; }
}