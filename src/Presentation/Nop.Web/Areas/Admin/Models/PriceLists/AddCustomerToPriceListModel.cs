using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.PriceLists;

/// <summary>
/// Represents a customer model to add to the price list
/// </summary>
public partial record AddCustomerToPriceListModel : BaseNopModel
{
    #region Ctor

    public AddCustomerToPriceListModel()
    {
        SelectedCustomerIds = new List<long>();
    }
    #endregion

    #region Properties

    public long PriceListId { get; set; }

    public IList<long> SelectedCustomerIds { get; set; }

    #endregion
}
