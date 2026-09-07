using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents an associated product model to add to the product
/// </summary>
public partial record AddAssociatedProductModel : BaseNopModel
{
    #region Ctor

    public AddAssociatedProductModel()
    {
        SelectedProductIds = new List<long>();
    }
    #endregion

    #region Properties

    public long ProductId { get; set; }

    public IList<long> SelectedProductIds { get; set; }

    #endregion
}