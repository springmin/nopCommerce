using Nop.Web.Framework.Models;

namespace Nop.Web.Models.Profile;

public partial record ProfileIndexModel : BaseNopModel
{
    public long CustomerProfileId { get; set; }
    public string ProfileTitle { get; set; }
    public int PostsPage { get; set; }
    public bool PagingPosts { get; set; }
}