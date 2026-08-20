namespace Nop.Core.Domain.Blogs;

/// <summary>
/// Represents a blog comment
/// </summary>
public partial class BlogComment : BaseEntity
{
    /// <summary>
    /// Gets or sets the customer identifier
    /// </summary>
    public long CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the comment text
    /// </summary>
    public string CommentText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the comment is approved
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Gets or sets the store identifier
    /// </summary>
    public long StoreId { get; set; }

    /// <summary>
    /// Gets or sets the blog post identifier
    /// </summary>
    public long BlogPostId { get; set; }

    /// <summary>
    /// Gets or sets the date and time of instance creation
    /// </summary>
    public DateTime CreatedOnUtc { get; set; }
}