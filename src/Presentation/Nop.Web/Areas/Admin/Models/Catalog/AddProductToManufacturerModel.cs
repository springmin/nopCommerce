using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a product model to add to the manufacturer
/// </summary>
public partial record AddProductToManufacturerModel : BaseNopModel
{
    #region Ctor

    public AddProductToManufacturerModel()
    {
        SelectedProductIds = new List<long>();
    }
    #endregion

    #region Properties

    public long ManufacturerId { get; set; }

    public IList<long> SelectedProductIds { get; set; }

    #endregion
}