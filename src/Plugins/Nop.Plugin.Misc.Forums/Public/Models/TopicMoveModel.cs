using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.Forums.Public.Models;

public record TopicMoveModel : BaseNopEntityModel
{
    #region Properties

    public long ForumSelected { get; set; }
    public string TopicSeName { get; set; }

    public List<SelectListItem> ForumList { get; set; } = new();

    #endregion
}