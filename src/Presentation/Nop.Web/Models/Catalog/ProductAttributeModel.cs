using Nop.Web.Framework.Models;

namespace Nop.Web.Models.Catalog;

/// <summary>
/// Represents a product attribute model
/// </summary>
public partial record ProductAttributeModel : BaseNopModel
{
    #region Properties

    /// <summary>
    /// Gets or sets the attribute id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the value IDs of the attribute
    /// </summary>
    public IList<long> ValueIds { get; set; }

    #endregion

    #region Ctor

    public ProductAttributeModel()
    {
        ValueIds = new List<long>();
    }

    #endregion
}