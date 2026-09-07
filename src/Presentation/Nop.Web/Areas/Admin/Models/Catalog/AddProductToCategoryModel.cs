using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a product model to add to the category
/// </summary>
public partial record AddProductToCategoryModel : BaseNopModel
{
    #region Ctor

    public AddProductToCategoryModel()
    {
        SelectedProductIds = new List<long>();
    }
    #endregion

    #region Properties

    public long CategoryId { get; set; }

    public IList<long> SelectedProductIds { get; set; }

    #endregion
}