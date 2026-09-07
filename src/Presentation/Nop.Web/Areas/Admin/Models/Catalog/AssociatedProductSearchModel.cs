using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents an associated product search model
/// </summary>
public partial record AssociatedProductSearchModel : BaseSearchModel
{
    #region Properties

    public long ProductId { get; set; }

    #endregion
}