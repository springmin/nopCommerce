namespace Nop.Core.Domain.Customers;

/// <summary>
/// Represents a customer-address mapping class
/// </summary>
public partial class CustomerAddressMapping : BaseEntity
{
    /// <summary>
    /// Gets or sets the customer identifier
    /// </summary>
    public long CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the address identifier
    /// </summary>
    public long AddressId { get; set; }
}