using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Customers;

/// <summary>
/// Represents a product model to add to the customer role 
/// </summary>
public partial record AddProductToCustomerRoleModel : BaseNopEntityModel
{
    #region Properties

    public long AssociatedToProductId { get; set; }

    #endregion
}