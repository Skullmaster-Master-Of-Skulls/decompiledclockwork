using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x0200024B RID: 587
	public class LoadDynamicDataOptions
	{
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00018599 File Offset: 0x00016799
		// (set) Token: 0x060011D1 RID: 4561 RVA: 0x000185A1 File Offset: 0x000167A1
		public string SqlQuery { get; set; }

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x000185AA File Offset: 0x000167AA
		// (set) Token: 0x060011D3 RID: 4563 RVA: 0x000185B2 File Offset: 0x000167B2
		public int ScreenNum { get; set; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x000185BB File Offset: 0x000167BB
		// (set) Token: 0x060011D5 RID: 4565 RVA: 0x000185C3 File Offset: 0x000167C3
		public IList<int> ControlIds { get; set; }
	}
}
