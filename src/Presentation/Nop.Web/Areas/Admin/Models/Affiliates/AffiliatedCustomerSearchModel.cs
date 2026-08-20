using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Affiliates;

/// <summary>
/// Represents an affiliated customer search model
/// </summary>
public partial record AffiliatedCustomerSearchModel : BaseSearchModel
{
    #region Properties

    public long AffliateId { get; set; }

    #endregion
}