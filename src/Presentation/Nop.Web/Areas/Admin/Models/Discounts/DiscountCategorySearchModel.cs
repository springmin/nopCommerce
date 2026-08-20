using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Discounts;

/// <summary>
/// Represents a discount category search model
/// </summary>
public partial record DiscountCategorySearchModel : BaseSearchModel
{
    #region Properties

    public long DiscountId { get; set; }

    #endregion
}