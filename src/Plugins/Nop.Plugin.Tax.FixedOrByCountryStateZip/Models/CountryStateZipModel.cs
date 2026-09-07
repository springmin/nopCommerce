using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Tax.FixedOrByCountryStateZip.Models;

public record CountryStateZipModel : BaseNopEntityModel
{
    [NopResourceDisplayName("Plugins.Tax.FixedOrByCountryStateZip.Fields.Store")]
    public long StoreId { get; set; }
    [NopResourceDisplayName("Plugins.Tax.FixedOrByCountryStateZip.Fields.Store")]
    public string StoreName { get; set; }

    [NopResourceDisplayName("Plugins.Tax.FixedOrByCountryStateZip.Fields.TaxCategory")]
    public long TaxCategoryId { get; set; }
    [NopResourceDisplayName("Plugins.Tax.FixedOrByCountryStateZip.Fields.TaxCategory")]
    public string TaxCategoryName { get; set; }

    [NopResourceDisplayName("Plugins.Tax.FixedOrByCountryStateZip.Fields.Country")]
    public long CountryId { get; set; }
    [NopResourceDisplayName("Plugins.Tax.FixedOrByCountryStateZip.Fields.Country")]
    public string CountryName { get; set; }

    [NopResourceDisplayName("Plugins.Tax.FixedOrByCountryStateZip.Fields.StateProvince")]
    public long StateProvinceId { get; set; }
    [NopResourceDisplayName("Plugins.Tax.FixedOrByCountryStateZip.Fields.StateProvince")]
    public string StateProvinceName { get; set; }

    [NopResourceDisplayName("Plugins.Tax.FixedOrByCountryStateZip.Fields.Zip")]
    public string Zip { get; set; }

    [NopResourceDisplayName("Plugins.Tax.FixedOrByCountryStateZip.Fields.Percentage")]
    public decimal Percentage { get; set; }
}