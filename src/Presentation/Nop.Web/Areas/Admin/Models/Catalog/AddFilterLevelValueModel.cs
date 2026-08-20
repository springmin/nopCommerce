using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a filter level value model to add to the product
/// </summary>
public partial record AddFilterLevelValueModel : BaseNopModel
{
    #region Ctor

    public AddFilterLevelValueModel()
    {
        SelectedFilterLevelValueIds = new List<long>();
    }
    #endregion

    #region Properties

    public long ProductId { get; set; }

    public IList<long> SelectedFilterLevelValueIds { get; set; }

    #endregion
}