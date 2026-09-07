using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a product attribute combination search model
/// </summary>
public partial record ProductAttributeCombinationSearchModel : BaseSearchModel
{
    #region Properties

    public long ProductId { get; set; }

    #endregion
}