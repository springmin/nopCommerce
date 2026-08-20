using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Affiliates;

/// <summary>
/// Represents an affiliated order model
/// </summary>
public partial record AffiliatedOrderModel : BaseNopEntityModel
{
    #region Properties

    public long Id { get; set; }

    [NopResourceDisplayName("Admin.Affiliates.Orders.CustomOrderNumber")]
    public string CustomOrderNumber { get; set; }

    [NopResourceDisplayName("Admin.Affiliates.Orders.OrderStatus")]
    public string OrderStatus { get; set; }
    [NopResourceDisplayName("Admin.Affiliates.Orders.OrderStatus")]
    public long OrderStatusId { get; set; }

    [NopResourceDisplayName("Admin.Affiliates.Orders.PaymentStatus")]
    public string PaymentStatus { get; set; }

    [NopResourceDisplayName("Admin.Affiliates.Orders.ShippingStatus")]
    public string ShippingStatus { get; set; }

    [NopResourceDisplayName("Admin.Affiliates.Orders.OrderTotal")]
    public string OrderTotal { get; set; }

    [NopResourceDisplayName("Admin.Affiliates.Orders.CreatedOn")]
    public DateTime CreatedOn { get; set; }

    #endregion
}