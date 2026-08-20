using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Blogs;
using Nop.Web.Framework.Components;

namespace Nop.Web.Components;

public partial class BlogRssHeaderLinkViewComponent : NopViewComponent
{
    protected readonly BlogSettings _blogSettings;

    public BlogRssHeaderLinkViewComponent(BlogSettings blogSettings)
    {
        _blogSettings = blogSettings;
    }

    public async Task<IViewComponentResult> InvokeAsync(long currentCategoryId, long currentProductId)
    {
        if (!_blogSettings.Enabled || !_blogSettings.ShowHeaderRssUrl)
            return Content("");

        return await ViewAsync();
    }
}