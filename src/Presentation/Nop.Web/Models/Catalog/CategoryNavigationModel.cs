using Nop.Web.Framework.Models;

namespace Nop.Web.Models.Catalog;

public partial record CategoryNavigationModel : BaseNopModel
{
    public CategoryNavigationModel()
    {
        Categories = new List<CategorySimpleModel>();
    }

    public long CurrentCategoryId { get; set; }
    public List<CategorySimpleModel> Categories { get; set; }

    #region Nested classes

    public partial record CategoryLineModel : BaseNopModel
    {
        public long CurrentCategoryId { get; set; }
        public CategorySimpleModel Category { get; set; }
    }

    #endregion
}