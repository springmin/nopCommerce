using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Customers;

/// <summary>
/// Represents a customer attribute value search model
/// </summary>
public partial record CustomerAttributeValueSearchModel : BaseSearchModel
{
    #region Properties

    public long CustomerAttributeId { get; set; }

    #endregion
}