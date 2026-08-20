using Nop.Web.Framework.Models;

namespace Nop.Web.Models.Common;

public partial record LanguageSelectorModel : BaseNopModel
{
    public LanguageSelectorModel()
    {
        AvailableLanguages = new List<LanguageModel>();
    }

    public IList<LanguageModel> AvailableLanguages { get; set; }

    public long CurrentLanguageId { get; set; }

    public bool UseImages { get; set; }
}