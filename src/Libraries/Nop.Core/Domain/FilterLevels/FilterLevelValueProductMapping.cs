namespace Nop.Core.Domain.FilterLevels;

/// <summary>
/// Represents a filter level value and product mapping
/// </summary>
public partial class FilterLevelValueProductMapping : BaseEntity
{
    /// <summary>
    /// Gets or sets the filter level value identifier
    /// </summary>
    public long FilterLevelValueId { get; set; }
    
    /// <summary>
    /// Gets or sets the product identifier
    /// </summary>
    public long ProductId { get; set; }
    
}
