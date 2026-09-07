using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.Forums.Admin.Models;

/// <summary>
/// Represents a forum search model
/// </summary>
public record ForumSearchModel : BaseSearchModel
{
    #region Properties

    public long ForumGroupId { get; set; }

    #endregion
}