using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Customers;

/// <summary>
/// Represents a customer address search model
/// </summary>
public partial record CustomerAddressSearchModel : BaseSearchModel
{
    #region Properties

    public long CustomerId { get; set; }

    #endregion
}