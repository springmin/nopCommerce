using Nop.Web.Areas.Admin.Models.Directory;
using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Shipping;

/// <summary>
/// Represents a shipping method restriction model
/// </summary>
public partial record ShippingMethodRestrictionModel : BaseNopModel
{
    #region Ctor

    public ShippingMethodRestrictionModel()
    {
        AvailableShippingMethods = new List<ShippingMethodModel>();
        AvailableCountries = new List<CountryModel>();
        Restricted = new Dictionary<long, IDictionary<long, bool>>();
    }

    #endregion

    #region Properties

    public IList<ShippingMethodModel> AvailableShippingMethods { get; set; }

    public IList<CountryModel> AvailableCountries { get; set; }

    //[country id] / [shipping method id] / [restricted]
    public IDictionary<long, IDictionary<long, bool>> Restricted { get; set; }

    #endregion
}