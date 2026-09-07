using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents tagged products search model
/// </summary>
public partial record ProductTagProductSearchModel : BaseSearchModel
{
    #region Properties

    public long ProductTagId { get; set; }

    #endregion
}
