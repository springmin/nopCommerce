using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Menus;

/// <summary>
/// Represents a select entity model
/// </summary>
public partial record SelectMenuItemEntityModel : BaseNopModel
{
    #region Properties

    public long MenuItemId { get; set; }

    public long EntityId { get; set; }

    #endregion
}
