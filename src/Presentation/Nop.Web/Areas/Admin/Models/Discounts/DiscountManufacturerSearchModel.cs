using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Discounts;

/// <summary>
/// Represents a discount manufacturer search model
/// </summary>
public partial record DiscountManufacturerSearchModel : BaseSearchModel
{
    #region Properties

    public long DiscountId { get; set; }

    #endregion
}