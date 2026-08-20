namespace Nop.Core.Domain.Discounts;

/// <summary>
/// Represents a discount usage history entry
/// </summary>
public partial class DiscountUsageHistory : BaseEntity
{
    /// <summary>
    /// Gets or sets the discount identifier
    /// </summary>
    public long DiscountId { get; set; }

    /// <summary>
    /// Gets or sets the order identifier
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// Gets or sets the date and time of instance creation
    /// </summary>
    public DateTime CreatedOnUtc { get; set; }
}