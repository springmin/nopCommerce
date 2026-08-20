using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.PriceLists;

/// <summary>
/// Represents a product model to add to the price list
/// </summary>
public partial record AddProductToPriceListModel : BaseNopModel
{
    #region Ctor

    public AddProductToPriceListModel()
    {
        SelectedProductIds = new List<long>();
    }
    #endregion

    #region Properties

    public long PriceListId { get; set; }

    public IList<long> SelectedProductIds { get; set; }

    #endregion
}
