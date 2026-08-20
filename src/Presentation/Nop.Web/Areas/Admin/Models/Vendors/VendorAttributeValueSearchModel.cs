using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Vendors;

/// <summary>
/// Represents a vendor attribute value search model
/// </summary>
public partial record VendorAttributeValueSearchModel : BaseSearchModel
{
    #region Properties

    public long VendorAttributeId { get; set; }

    #endregion
}