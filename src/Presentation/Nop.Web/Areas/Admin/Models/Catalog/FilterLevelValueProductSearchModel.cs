using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a filter level value product search model
/// </summary>
public partial record FilterLevelValueProductSearchModel : BaseSearchModel
{
    #region Properties

    public long FilterLevelValueId { get; set; }

    #endregion
}