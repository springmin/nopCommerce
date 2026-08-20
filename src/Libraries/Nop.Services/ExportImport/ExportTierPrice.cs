namespace Nop.Services.ExportImport;

public partial class ExportTierPrice
{
    /// <summary>
    /// Gets or sets the tier price identifier
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the store identifier (0 - all stores)
    /// </summary>
    public long StoreId { get; set; }

    /// <summary>
    /// Gets or sets the customer role identifier
    /// </summary>
    public long? CustomerRoleId { get; set; }

    /// <summary>
    /// Gets or sets the quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the start date and time in UTC
    /// </summary>
    public DateTime? StartDateTimeUtc { get; set; }

    /// <summary>
    /// Gets or sets the end date and time in UTC
    /// </summary>
    public DateTime? EndDateTimeUtc { get; set; }
}