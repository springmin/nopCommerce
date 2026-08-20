using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a tier price search model
/// </summary>
public partial record TierPriceSearchModel : BaseSearchModel
{
    #region Properties

    public long ProductId { get; set; }

    #endregion
}