using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a product picture search model
/// </summary>
public partial record ProductPictureSearchModel : BaseSearchModel
{
    #region Properties

    public long ProductId { get; set; }

    #endregion
}