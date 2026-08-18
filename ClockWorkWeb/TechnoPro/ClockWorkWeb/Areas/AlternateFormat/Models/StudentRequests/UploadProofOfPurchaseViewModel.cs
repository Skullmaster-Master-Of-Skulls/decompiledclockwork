using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.StudentRequests
{
	// Token: 0x0200017B RID: 379
	public class UploadProofOfPurchaseViewModel : AlternateFormatBaseViewModel
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00049539 File Offset: 0x00047739
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00049541 File Offset: 0x00047741
		public PersonBaseDTO Student { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0004954A File Offset: 0x0004774A
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00049552 File Offset: 0x00047752
		public string ReturnUrl { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0004955B File Offset: 0x0004775B
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x00049563 File Offset: 0x00047763
		public int MediaContentRequestedInfoId { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0004956C File Offset: 0x0004776C
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00049574 File Offset: 0x00047774
		public MediaContentWebView MediaContent { get; set; }
	}
}
