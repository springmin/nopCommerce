using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Discounts;

/// <summary>
/// Represents a discount manufacturer model
/// </summary>
public partial record DiscountManufacturerModel : BaseNopEntityModel
{
    #region Properties

    public long ManufacturerId { get; set; }

    public string ManufacturerName { get; set; }

    #endregion
}