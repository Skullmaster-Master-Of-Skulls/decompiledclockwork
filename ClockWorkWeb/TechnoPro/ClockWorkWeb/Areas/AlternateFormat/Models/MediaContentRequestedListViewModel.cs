using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models
{
	// Token: 0x02000174 RID: 372
	public class MediaContentRequestedListViewModel
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0004925A File Offset: 0x0004745A
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x00049262 File Offset: 0x00047462
		public MediaContentDetailDTO MediaContentDetail { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0004926B File Offset: 0x0004746B
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x00049273 File Offset: 0x00047473
		public IList<StudentRequestWebView> StudentRequestList { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0004927C File Offset: 0x0004747C
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x00049284 File Offset: 0x00047484
		public int ProofOfPurchaseId { get; set; }
	}
}
