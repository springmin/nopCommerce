namespace Nop.Core.Domain.Catalog;

/// <summary>
/// Represents a product picture mapping
/// </summary>
public partial class ProductPicture : BaseEntity
{
    /// <summary>
    /// Gets or sets the product identifier
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// Gets or sets the picture identifier
    /// </summary>
    public long PictureId { get; set; }

    /// <summary>
    /// Gets or sets the display order
    /// </summary>
    public int DisplayOrder { get; set; }
}