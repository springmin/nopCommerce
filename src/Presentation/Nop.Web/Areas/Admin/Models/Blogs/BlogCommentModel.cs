using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Blogs;

/// <summary>
/// Represents a blog comment model
/// </summary>
public partial record BlogCommentModel : BaseNopEntityModel
{
    #region Properties

    [NopResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.BlogPost")]
    public long BlogPostId { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.BlogPost")]
    public string BlogPostTitle { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.Customer")]
    public long CustomerId { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.Customer")]
    public string CustomerInfo { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.Comment")]
    public string Comment { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.IsApproved")]
    public bool IsApproved { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.StoreName")]
    public long StoreId { get; set; }
    public string StoreName { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.CreatedOn")]
    public DateTime CreatedOn { get; set; }

    #endregion
}