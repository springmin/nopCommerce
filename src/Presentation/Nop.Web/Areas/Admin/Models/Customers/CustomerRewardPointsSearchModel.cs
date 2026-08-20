using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Customers;

/// <summary>
/// Represents a reward points search model
/// </summary>
public partial record CustomerRewardPointsSearchModel : BaseSearchModel
{
    #region Properties

    public long CustomerId { get; set; }

    #endregion
}