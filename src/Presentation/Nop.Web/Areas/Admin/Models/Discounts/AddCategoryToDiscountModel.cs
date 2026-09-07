using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Discounts;

/// <summary>
/// Represents a category model to add to the discount
/// </summary>
public partial record AddCategoryToDiscountModel : BaseNopModel
{
    #region Ctor

    public AddCategoryToDiscountModel()
    {
        SelectedCategoryIds = new List<long>();
    }
    #endregion

    #region Properties

    public long DiscountId { get; set; }

    public IList<long> SelectedCategoryIds { get; set; }

    #endregion
}