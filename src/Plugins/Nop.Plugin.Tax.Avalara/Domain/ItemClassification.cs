using Nop.Core;

namespace Nop.Plugin.Tax.Avalara.Domain;

/// <summary>
/// Represents a item classification record
/// </summary>
public class ItemClassification : BaseEntity
{
    /// <summary>
    /// Gets or sets the product identifier
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// Gets or sets the HS classification request identifier
    /// </summary>
    public string HSClassificationRequestId { get; set; }

    /// <summary>
    /// Gets or sets the country identifier
    /// </summary>
    public long CountryId { get; set; }

    /// <summary>
    /// Gets or sets the HS (harmonized system) code
    /// </summary>
    public string HSCode { get; set; }

    /// <summary>
    /// Gets or sets the date and time of updation
    /// </summary>
    public DateTime UpdatedOnUtc { get; set; }
}