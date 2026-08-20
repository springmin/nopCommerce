
namespace Nop.Web.Framework.Models;

/// <summary>
/// Represents localized locale model
/// </summary>
public partial interface ILocalizedLocaleModel
{
    /// <summary>
    /// Gets or sets the language identifier
    /// </summary>
    long LanguageId { get; set; }
}