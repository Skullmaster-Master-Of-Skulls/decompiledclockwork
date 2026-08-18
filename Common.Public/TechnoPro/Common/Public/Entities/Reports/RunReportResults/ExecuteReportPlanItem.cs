using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports.RunReportResults
{
	// Token: 0x02000233 RID: 563
	public class ExecuteReportPlanItem
	{
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00017FB2 File Offset: 0x000161B2
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x00017FBA File Offset: 0x000161BA
		public IList<int> ReportFunctionIdsToRun { get; set; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00017FC3 File Offset: 0x000161C3
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x00017FCB File Offset: 0x000161CB
		public bool HasCompleted { get; set; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00017FD4 File Offset: 0x000161D4
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x00017FDC File Offset: 0x000161DC
		public bool RunOnClient { get; set; }
	}
}
