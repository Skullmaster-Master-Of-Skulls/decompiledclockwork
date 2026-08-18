using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkWeb.Models;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models
{
	// Token: 0x0200016E RID: 366
	public class SearchingContentResultsViewModel
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0004914A File Offset: 0x0004734A
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x00049152 File Offset: 0x00047352
		public string SearchText { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0004915B File Offset: 0x0004735B
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x00049163 File Offset: 0x00047363
		public IList<MediaContentWebView> MediaContentList { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0004916C File Offset: 0x0004736C
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00049174 File Offset: 0x00047374
		public PagingInfo PagingInfo { get; set; }
	}
}
