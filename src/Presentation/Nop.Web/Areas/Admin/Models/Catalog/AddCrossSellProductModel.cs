using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a cross-sell product model to add to the product
/// </summary>
public partial record AddCrossSellProductModel : BaseNopModel
{
    #region Ctor

    public AddCrossSellProductModel()
    {
        SelectedProductIds = new List<long>();
    }
    #endregion

    #region Properties

    public long ProductId { get; set; }

    public IList<long> SelectedProductIds { get; set; }

    #endregion
}