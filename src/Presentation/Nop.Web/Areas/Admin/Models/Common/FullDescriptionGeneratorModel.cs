using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Common;

/// <summary>
/// Represents a full description generator model
/// </summary>
public partial record FullDescriptionGeneratorModel : BaseNopModel
{
    public virtual string ProductNameElementId { get; set; }
    public long LanguageId { get; set; }
}