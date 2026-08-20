using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Menus;

/// <summary>
/// Represents menu model
/// </summary>
public partial record MenuModel : BaseNopEntityModel, IAclSupportedModel, IStoreMappingSupportedModel, ILocalizedModel<MenuLocalizedModel>
{
    #region Ctor

    public MenuModel()
    {
        AvailableMenuTypes = new List<SelectListItem>();

        SelectedStoreIds = new List<long>();
        AvailableStores = new List<SelectListItem>();

        SelectedCustomerRoleIds = new List<long>();
        AvailableCustomerRoles = new List<SelectListItem>();

        MenuItemSearchModel = new();
        Locales = new List<MenuLocalizedModel>();
    }

    #endregion

    #region Properties

    [NopResourceDisplayName("Admin.ContentManagement.Menus.Fields.Name")]
    public string Name { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.Fields.MenuType")]
    public long MenuTypeId { get; set; }
    public string MenuTypeName { get; set; }
    public IList<SelectListItem> AvailableMenuTypes { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.Fields.DisplayAllCategories")]
    public bool DisplayAllCategories { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.Fields.CssClass")]
    public string CssClass { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.Fields.DisplayOrder")]
    public int DisplayOrder { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.Fields.Published")]
    public bool Published { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.Fields.LimitedToStores")]
    public IList<long> SelectedStoreIds { get; set; }
    public IList<SelectListItem> AvailableStores { get; set; }

    public IList<long> SelectedCustomerRoleIds { get; set; }
    public IList<SelectListItem> AvailableCustomerRoles { get; set; }

    public MenuItemSearchModel MenuItemSearchModel { get; set; }

    public IList<MenuLocalizedModel> Locales { get; set; }

    #endregion
}

public partial record MenuLocalizedModel : ILocalizedLocaleModel
{
    public long LanguageId { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.Fields.Name")]
    public string Name { get; set; }
}