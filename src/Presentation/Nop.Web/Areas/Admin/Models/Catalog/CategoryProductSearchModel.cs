using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a category product search model
/// </summary>
public partial record CategoryProductSearchModel : BaseSearchModel
{
    #region Properties

    public long CategoryId { get; set; }

    #endregion
}