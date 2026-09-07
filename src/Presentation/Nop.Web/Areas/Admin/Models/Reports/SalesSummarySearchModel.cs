using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Reports;

public partial record SalesSummarySearchModel : BaseSearchModel
{
    #region Ctor

    public SalesSummarySearchModel()
    {
        AvailableStores = new List<SelectListItem>();
        AvailableOrderStatuses = new List<SelectListItem>();
        AvailablePaymentStatuses = new List<SelectListItem>();
        AvailableCategories = new List<SelectListItem>();
        AvailableManufacturers = new List<SelectListItem>();
        AvailableCountries = new List<SelectListItem>();
        OrderStatusIds = new List<long>();
        PaymentStatusIds = new List<long>();
        GroupByOptions = new List<SelectListItem>();
        AvailableVendors = new List<SelectListItem>();
    }

    #endregion

    #region Properties

    [NopResourceDisplayName("Admin.Reports.SalesSummary.StartDate")]
    [UIHint("DateNullable")]
    public DateTime? StartDate { get; set; }

    [NopResourceDisplayName("Admin.Reports.SalesSummary.EndDate")]
    [UIHint("DateNullable")]
    public DateTime? EndDate { get; set; }

    [NopResourceDisplayName("Admin.Reports.SalesSummary.Store")]
    public long StoreId { get; set; }

    [NopResourceDisplayName("Admin.Reports.SalesSummary.OrderStatus")]
    public IList<long> OrderStatusIds { get; set; }

    [NopResourceDisplayName("Admin.Reports.SalesSummary.PaymentStatus")]
    public IList<long> PaymentStatusIds { get; set; }

    [NopResourceDisplayName("Admin.Reports.SalesSummary.Category")]
    public long CategoryId { get; set; }

    [NopResourceDisplayName("Admin.Reports.SalesSummary.Manufacturer")]
    public long ManufacturerId { get; set; }

    [NopResourceDisplayName("Admin.Reports.SalesSummary.BillingCountry")]
    public long BillingCountryId { get; set; }

    [NopResourceDisplayName("Admin.Reports.SalesSummary.Product")]
    public long ProductId { get; set; }

    [NopResourceDisplayName("Admin.Reports.SalesSummary.GroupBy")]
    public long SearchGroupId { get; set; }

    [NopResourceDisplayName("Admin.Reports.SalesSummary.Vendor")]
    public long VendorId { get; set; }

    public IList<SelectListItem> AvailableStores { get; set; }

    public IList<SelectListItem> AvailableOrderStatuses { get; set; }

    public IList<SelectListItem> AvailablePaymentStatuses { get; set; }

    public IList<SelectListItem> AvailableCategories { get; set; }

    public IList<SelectListItem> AvailableManufacturers { get; set; }

    public IList<SelectListItem> AvailableCountries { get; set; }

    public IList<SelectListItem> GroupByOptions { get; set; }

    public IList<SelectListItem> AvailableVendors { get; set; }

    public bool IsLoggedInAsVendor { get; set; }

    #endregion
}