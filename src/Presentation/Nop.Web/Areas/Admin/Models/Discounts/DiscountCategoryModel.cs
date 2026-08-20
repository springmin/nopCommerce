using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Discounts;

/// <summary>
/// Represents a discount category model
/// </summary>
public partial record DiscountCategoryModel : BaseNopEntityModel
{
    #region Properties

    public long CategoryId { get; set; }

    public string CategoryName { get; set; }

    #endregion
}