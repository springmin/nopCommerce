namespace Nop.Web.Areas.Admin.Models.Vendors;

/// <summary>
/// Represents a customer model to add to the vendor 
/// </summary>
public partial record AddCustomerToVendorModel
{
    #region Properties

    public long CustomerId { get; set; }

    #endregion
}
