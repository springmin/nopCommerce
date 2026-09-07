using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Menus;

/// <summary>
/// Represents menu item model
/// </summary>
public partial record MenuItemModel : BaseNopEntityModel, IAclSupportedModel, IStoreMappingSupportedModel, ILocalizedModel<MenuItemLocalizedModel>
{
    #region Ctor

    public MenuItemModel()
    {
        AvailableMenuItemTypes = new List<SelectListItem>();
        AvailableMenuItems = new List<SelectListItem>();

        SelectedStoreIds = new List<long>();
        AvailableStores = new List<SelectListItem>();

        SelectedCustomerRoleIds = new List<long>();
        AvailableCustomerRoles = new List<SelectListItem>();

        AvailableMenuItemTemplates = new List<SelectListItem>();
        AvailableStandardRoutes = new List<SelectListItem>();
        AvailableCategories = new List<SelectListItem>();
        AvailableVendors = new List<SelectListItem>();
        AvailableManufacturers = new List<SelectListItem>();
        AvailableTopics = new List<SelectListItem>();

        Locales = new List<MenuItemLocalizedModel>();
    }

    #endregion

    #region Properties

    public long MenuId { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.Title")]
    public string Title { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.Breadcrumb")]
    public string Breadcrumb { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.Url")]
    public string Url { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.Category")]
    public long CategoryId { get; set; }
    public IList<SelectListItem> AvailableCategories { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.Vendor")]
    public long VendorId { get; set; }
    public IList<SelectListItem> AvailableVendors { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.Manufacturer")]
    public long ManufacturerId { get; set; }
    public IList<SelectListItem> AvailableManufacturers { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.Topic")]
    public long TopicId { get; set; }
    public IList<SelectListItem> AvailableTopics { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.Product")]
    public long? ProductId { get; set; }
    public string ProductName { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.MenuItemType")]
    public long MenuItemTypeId { get; set; }
    public string MenuItemTypeName { get; set; }
    public IList<SelectListItem> AvailableMenuItemTypes { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.RouteName")]
    public string RouteName { get; set; }
    public IList<SelectListItem> AvailableStandardRoutes { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.Template")]
    public long TemplateId { get; set; }
    public IList<SelectListItem> AvailableMenuItemTemplates { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.Published")]
    public bool Published { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.DisplayOrder")]
    public int DisplayOrder { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.CssClass")]
    public string CssClass { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.Parent")]
    public long? ParentId { get; set; }
    public IList<SelectListItem> AvailableMenuItems { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.NumberOfSubItemsPerGridElement")]
    public int? NumberOfSubItemsPerGridElement { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.NumberOfItemsPerGridRow")]
    public int? NumberOfItemsPerGridRow { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.MaximumNumberEntities")]
    public int? MaximumNumberEntities { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.LimitedToStores")]
    public IList<long> SelectedStoreIds { get; set; }
    public IList<SelectListItem> AvailableStores { get; set; }

    public IList<long> SelectedCustomerRoleIds { get; set; }
    public IList<SelectListItem> AvailableCustomerRoles { get; set; }

    public IList<MenuItemLocalizedModel> Locales { get; set; }

    #endregion
}

public partial record MenuItemLocalizedModel : ILocalizedLocaleModel
{
    public long LanguageId { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Menus.MenuItem.Fields.Title")]
    public string Title { get; set; }
}
