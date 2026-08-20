using Nop.Core;

namespace Nop.Plugin.Misc.RFQ.Domains;

/// <summary>
/// Represents a quote item entity
/// </summary>
public class QuoteItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the quote identifier
    /// </summary>
    public long QuoteId { get; set; }

    /// <summary>
    /// Gets or sets the product identifier
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// Gets or sets the requested quantity
    /// </summary>
    public int? RequestedQty { get; set; }

    /// <summary>
    /// Gets or sets the requested unit price in primary store currency (include tax)
    /// </summary>
    public decimal? RequestedUnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the quantity
    /// </summary>
    public int OfferedQty { get; set; }

    /// <summary>
    /// Gets or sets the unit price in primary store currency (include tax)
    /// </summary>
    public decimal OfferedUnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the product attributes in XML format
    /// </summary>
    public string AttributesXml { get; set; }

    /// <summary>
    /// Request a quote identifier
    /// </summary>
    public long? RequestQuoteId { get; set; }

    /// <summary>
    /// Shopping cart item identifier
    /// </summary>
    public long? ShoppingCartItemId { get; set; }
}