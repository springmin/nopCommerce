using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Discounts;

/// <summary>
/// Represents a discount usage history search model
/// </summary>
public partial record DiscountUsageHistorySearchModel : BaseSearchModel
{
    #region Properties

    public long DiscountId { get; set; }

    #endregion
}