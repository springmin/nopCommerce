namespace Nop.Core.Domain.Security;

/// <summary>
/// Represents an ACL record
/// </summary>
public partial class AclRecord : BaseEntity
{
    /// <summary>
    /// Gets or sets the entity identifier
    /// </summary>
    public long EntityId { get; set; }

    /// <summary>
    /// Gets or sets the entity name
    /// </summary>
    public string EntityName { get; set; }

    /// <summary>
    /// Gets or sets the customer role identifier
    /// </summary>
    public long CustomerRoleId { get; set; }
}