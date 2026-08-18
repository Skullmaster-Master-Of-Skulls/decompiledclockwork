using System;

namespace TechnoPro.Common.UI.Web.Entity.AlternateFormat
{
	// Token: 0x02000051 RID: 81
	public class MediaContentRequestedToolStripViewModel
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00005258 File Offset: 0x00003458
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00005260 File Offset: 0x00003460
		public int MediaContentRequestedInfoID { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00005269 File Offset: 0x00003469
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00005271 File Offset: 0x00003471
		public int MediaContentPerFormatId { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000527A File Offset: 0x0000347A
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00005282 File Offset: 0x00003482
		public bool IsCancellable { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000528B File Offset: 0x0000348B
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00005293 File Offset: 0x00003493
		public bool NeedForProofOfPurchaseUpload { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000529C File Offset: 0x0000349C
		// (set) Token: 0x06000262 RID: 610 RVA: 0x000052A4 File Offset: 0x000034A4
		public bool ReadyToDownload { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000052AD File Offset: 0x000034AD
		// (set) Token: 0x06000264 RID: 612 RVA: 0x000052B5 File Offset: 0x000034B5
		public int FileSize { get; set; }
	}
}
