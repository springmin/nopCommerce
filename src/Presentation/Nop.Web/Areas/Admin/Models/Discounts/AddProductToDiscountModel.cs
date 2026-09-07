using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Discounts;

/// <summary>
/// Represents a product model to add to the discount
/// </summary>
public partial record AddProductToDiscountModel : BaseNopModel
{
    #region Ctor

    public AddProductToDiscountModel()
    {
        SelectedProductIds = new List<long>();
    }
    #endregion

    #region Properties

    public long DiscountId { get; set; }

    public IList<long> SelectedProductIds { get; set; }

    #endregion
}