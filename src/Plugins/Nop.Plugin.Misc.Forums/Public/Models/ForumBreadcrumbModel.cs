using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.Forums.Public.Models;

public record ForumBreadcrumbModel : BaseNopModel
{
    #region Properties

    public long ForumGroupId { get; set; }
    public string ForumGroupName { get; set; }
    public string ForumGroupSeName { get; set; }
    public long ForumId { get; set; }
    public string ForumName { get; set; }
    public string ForumSeName { get; set; }
    public long ForumTopicId { get; set; }
    public string ForumTopicSubject { get; set; }
    public string ForumTopicSeName { get; set; }

    #endregion
}