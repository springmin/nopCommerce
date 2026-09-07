using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Orders;

/// <summary>
/// Represents an upload license model
/// </summary>
public partial record UploadLicenseModel : BaseNopModel
{
    #region Properties

    public long OrderId { get; set; }

    public long OrderItemId { get; set; }

    [UIHint("Download")]
    public long LicenseDownloadId { get; set; }

    #endregion
}