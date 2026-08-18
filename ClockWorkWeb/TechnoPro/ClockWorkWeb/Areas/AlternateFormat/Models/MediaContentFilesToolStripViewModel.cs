using System;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models
{
	// Token: 0x02000172 RID: 370
	public class MediaContentFilesToolStripViewModel
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00049205 File Offset: 0x00047405
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x0004920D File Offset: 0x0004740D
		public MediaContentWebView MediaContent { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00049216 File Offset: 0x00047416
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x0004921E File Offset: 0x0004741E
		public int ProofOfPurchaseId { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00049227 File Offset: 0x00047427
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x0004922F File Offset: 0x0004742F
		public eStudentMediaContentFileStatus? FileStatus { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00049238 File Offset: 0x00047438
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x00049240 File Offset: 0x00047440
		public int MediaContentFilesCount { get; set; }
	}
}
