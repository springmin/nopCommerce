using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a related product model to add to the product
/// </summary>
public partial record AddRelatedProductModel : BaseNopModel
{
    #region Ctor

    public AddRelatedProductModel()
    {
        SelectedProductIds = new List<long>();
    }
    #endregion

    #region Properties

    public long ProductId { get; set; }

    public IList<long> SelectedProductIds { get; set; }

    #endregion
}