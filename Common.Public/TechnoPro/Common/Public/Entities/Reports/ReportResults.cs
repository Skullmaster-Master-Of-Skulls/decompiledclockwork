using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000230 RID: 560
	public class ReportResults : List<ReportResult>
	{
		// Token: 0x06001121 RID: 4385 RVA: 0x00017F58 File Offset: 0x00016158
		public ReportResults()
		{
			this.Args = new Dictionary<string, object>();
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00017F6E File Offset: 0x0001616E
		// (set) Token: 0x06001123 RID: 4387 RVA: 0x00017F76 File Offset: 0x00016176
		public Report Report { get; set; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00017F7F File Offset: 0x0001617F
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x00017F87 File Offset: 0x00016187
		public Dictionary<string, object> Args { get; set; }
	}
}
