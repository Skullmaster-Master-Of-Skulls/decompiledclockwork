using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkWeb.Models;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models
{
	// Token: 0x02000175 RID: 373
	public class StudentRequestsByTermViewModel
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0004928D File Offset: 0x0004748D
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00049295 File Offset: 0x00047495
		public string SelectedTermId { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0004929E File Offset: 0x0004749E
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x000492A6 File Offset: 0x000474A6
		public IList<MediaContentRequestedListViewModel> StudentRequestList { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x000492AF File Offset: 0x000474AF
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x000492B7 File Offset: 0x000474B7
		public PagingInfo PagingInfo { get; set; }
	}
}
