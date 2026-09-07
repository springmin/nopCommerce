using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a product order search model
/// </summary>
public partial record ProductOrderSearchModel : BaseSearchModel
{
    #region Properties

    public long ProductId { get; set; }

    #endregion
}