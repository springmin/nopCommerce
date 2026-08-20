using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Discounts;

/// <summary>
/// Represents a manufacturer model to add to the discount
/// </summary>
public partial record AddManufacturerToDiscountModel : BaseNopModel
{
    #region Ctor

    public AddManufacturerToDiscountModel()
    {
        SelectedManufacturerIds = new List<long>();
    }
    #endregion

    #region Properties

    public long DiscountId { get; set; }

    public IList<long> SelectedManufacturerIds { get; set; }

    #endregion
}