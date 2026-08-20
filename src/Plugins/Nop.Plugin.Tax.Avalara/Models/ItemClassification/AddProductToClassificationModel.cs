using Nop.Web.Framework.Models;

namespace Nop.Plugin.Tax.Avalara.Models.ItemClassification;

/// <summary>
/// Represents a product model to add for classification
/// </summary>
public record AddProductToClassificationModel : BaseNopModel
{
    #region Ctor

    public AddProductToClassificationModel()
    {
        SelectedProductIds = new List<long>();
    }

    #endregion

    #region Properties

    public IList<long> SelectedProductIds { get; set; }

    #endregion
}