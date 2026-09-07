using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.DiscountRules.CustomerRoles.Models;

public class RequirementModel
{
    public RequirementModel()
    {
        AvailableCustomerRoles = new List<SelectListItem>();
    }

    [NopResourceDisplayName("Plugins.DiscountRules.CustomerRoles.Fields.CustomerRole")]
    public long CustomerRoleId { get; set; }

    public long DiscountId { get; set; }

    public long RequirementId { get; set; }

    public IList<SelectListItem> AvailableCustomerRoles { get; set; }
}