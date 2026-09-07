using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Customers;

/// <summary>
/// Represents a customer orders search model
/// </summary>
public partial record CustomerOrderSearchModel : BaseSearchModel
{
    #region Properties

    public long CustomerId { get; set; }

    #endregion
}