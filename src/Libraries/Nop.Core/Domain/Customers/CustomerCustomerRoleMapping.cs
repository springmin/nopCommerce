namespace Nop.Core.Domain.Customers;

/// <summary>
/// Represents a customer-customer role mapping class
/// </summary>
public partial class CustomerCustomerRoleMapping : BaseEntity
{
    /// <summary>
    /// Gets or sets the customer identifier
    /// </summary>
    public long CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the customer role identifier
    /// </summary>
    public long CustomerRoleId { get; set; }
}