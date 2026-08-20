using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Common;

/// <summary>
/// Represents a contact form attribute value search model
/// </summary>
public partial record ContactFormAttributeValueSearchModel : BaseSearchModel
{
    #region Properties

    public long ContactFormAttributeId { get; set; }

    #endregion
}