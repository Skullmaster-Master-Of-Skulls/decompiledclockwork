using System;
using System.Web;
using TechnoPro.ClockWorkWeb.Common.ValidationAttributes;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.StudentRequests
{
	// Token: 0x0200017C RID: 380
	public class UploadProofOfPurchaseFileViewModel : UploadProofOfPurchaseViewModel
	{
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0004957D File Offset: 0x0004777D
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x00049585 File Offset: 0x00047785
		[FileSize(524288000)]
		[FileTypes("jpg,jpeg,png")]
		public HttpPostedFileBase File { get; set; }

		// Token: 0x04000854 RID: 2132
		private const int MaxUploadFileSizeInBytes = 524288000;

		// Token: 0x04000855 RID: 2133
		private const string SupportedUploadImageFileTypes = "jpg,jpeg,png";
	}
}
