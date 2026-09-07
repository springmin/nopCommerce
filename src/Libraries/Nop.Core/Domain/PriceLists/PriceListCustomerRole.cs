namespace Nop.Core.Domain.PriceLists;

/// <summary>
/// Represents a price list customer role mapping
/// </summary>
public partial class PriceListCustomerRole : BaseEntity
{
    /// <summary>
    ///  Gets or sets the price list identifier
    /// </summary>
    public long PriceListId { get; set; }

    /// <summary>
    ///  Gets or sets the customer role identifier
    /// </summary>
    public long CustomerRoleId { get; set; }
}
