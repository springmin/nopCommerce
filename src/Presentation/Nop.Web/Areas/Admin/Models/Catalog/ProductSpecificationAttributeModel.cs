using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a product specification attribute model
/// </summary>
public partial record ProductSpecificationAttributeModel : BaseNopEntityModel
{
    #region Properties

    public long AttributeTypeId { get; set; }

    public string AttributeTypeName { get; set; }

    public long AttributeId { get; set; }

    public string AttributeName { get; set; }

    public string ValueRaw { get; set; }

    public bool AllowFiltering { get; set; }

    public bool ShowOnProductPage { get; set; }

    public int DisplayOrder { get; set; }

    public long SpecificationAttributeOptionId { get; set; }

    #endregion
}