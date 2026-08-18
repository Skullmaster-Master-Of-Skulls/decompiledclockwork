using System;
using System.Collections.Generic;

namespace TechnoPro.ClockWorkWeb.Models.Home
{
	// Token: 0x02000110 RID: 272
	public class ClockWorkHomeModel
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0003A6B3 File Offset: 0x000388B3
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x0003A6BB File Offset: 0x000388BB
		public string PageTitle { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0003A6C4 File Offset: 0x000388C4
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x0003A6CC File Offset: 0x000388CC
		public string PageDescription { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0003A6D5 File Offset: 0x000388D5
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x0003A6DD File Offset: 0x000388DD
		public IList<ClockWorkGroupLinkModel> LinkGroups { get; set; }
	}
}
