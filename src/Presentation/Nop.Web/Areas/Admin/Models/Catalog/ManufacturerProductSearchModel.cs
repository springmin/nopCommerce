using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a manufacturer product search model
/// </summary>
public partial record ManufacturerProductSearchModel : BaseSearchModel
{
    #region Properties

    public long ManufacturerId { get; set; }

    #endregion
}