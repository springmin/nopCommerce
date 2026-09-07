using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.News.Public.Models;

/// <summary>
/// Represents the news item list model
/// </summary>
public record NewsItemListModel : BaseNopModel
{
    #region Properties

    public long WorkingLanguageId { get; set; }
    public NewsPagingFilteringModel PagingFilteringContext { get; set; } = new();
    public List<NewsItemModel> NewsItems { get; set; } = [];

    #endregion
}