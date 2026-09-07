using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Tax.FixedOrByCountryStateZip.Models;

public record FixedTaxRateModel : BaseNopModel
{
    public long TaxCategoryId { get; set; }

    [NopResourceDisplayName("Plugins.Tax.FixedOrByCountryStateZip.Fields.TaxCategoryName")]
    public string TaxCategoryName { get; set; }

    [NopResourceDisplayName("Plugins.Tax.FixedOrByCountryStateZip.Fields.Rate")]
    public decimal Rate { get; set; }
}