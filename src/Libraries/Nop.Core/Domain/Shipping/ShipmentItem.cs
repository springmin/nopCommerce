namespace Nop.Core.Domain.Shipping;

/// <summary>
/// Represents a shipment item
/// </summary>
public partial class ShipmentItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the shipment identifier
    /// </summary>
    public long ShipmentId { get; set; }

    /// <summary>
    /// Gets or sets the order item identifier
    /// </summary>
    public long OrderItemId { get; set; }

    /// <summary>
    /// Gets or sets the quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the warehouse identifier
    /// </summary>
    public long WarehouseId { get; set; }
}