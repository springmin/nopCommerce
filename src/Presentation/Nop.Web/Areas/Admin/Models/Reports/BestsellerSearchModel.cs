using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Reports;

/// <summary>
/// Represents a bestseller search model
/// </summary>
public partial record BestsellerSearchModel : BaseSearchModel
{
    #region Ctor

    public BestsellerSearchModel()
    {
        AvailableStores = new List<SelectListItem>();
        AvailableOrderStatuses = new List<SelectListItem>();
        AvailablePaymentStatuses = new List<SelectListItem>();
        AvailableCategories = new List<SelectListItem>();
        AvailableManufacturers = new List<SelectListItem>();
        AvailableCountries = new List<SelectListItem>();
        AvailableVendors = new List<SelectListItem>();
    }

    #endregion

    #region Properties

    [NopResourceDisplayName("Admin.Reports.Sales.Bestsellers.StartDate")]
    [UIHint("DateNullable")]
    public DateTime? StartDate { get; set; }

    [NopResourceDisplayName("Admin.Reports.Sales.Bestsellers.EndDate")]
    [UIHint("DateNullable")]
    public DateTime? EndDate { get; set; }

    [NopResourceDisplayName("Admin.Reports.Sales.Bestsellers.Store")]
    public long StoreId { get; set; }

    [NopResourceDisplayName("Admin.Reports.Sales.Bestsellers.OrderStatus")]
    public long OrderStatusId { get; set; }

    [NopResourceDisplayName("Admin.Reports.Sales.Bestsellers.PaymentStatus")]
    public long PaymentStatusId { get; set; }

    [NopResourceDisplayName("Admin.Reports.Sales.Bestsellers.Category")]
    public long CategoryId { get; set; }

    [NopResourceDisplayName("Admin.Reports.Sales.Bestsellers.Manufacturer")]
    public long ManufacturerId { get; set; }

    [NopResourceDisplayName("Admin.Reports.Sales.Bestsellers.BillingCountry")]
    public long BillingCountryId { get; set; }

    [NopResourceDisplayName("Admin.Reports.Sales.Bestsellers.Vendor")]
    public long VendorId { get; set; }

    public IList<SelectListItem> AvailableStores { get; set; }

    public IList<SelectListItem> AvailableOrderStatuses { get; set; }

    public IList<SelectListItem> AvailablePaymentStatuses { get; set; }

    public IList<SelectListItem> AvailableCategories { get; set; }

    public IList<SelectListItem> AvailableManufacturers { get; set; }

    public IList<SelectListItem> AvailableCountries { get; set; }

    public IList<SelectListItem> AvailableVendors { get; set; }

    public bool IsLoggedInAsVendor { get; set; }

    #endregion
}