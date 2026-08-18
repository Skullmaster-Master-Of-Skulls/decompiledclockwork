using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports.RunReportResults
{
	// Token: 0x02000234 RID: 564
	public class ReportExecutionPlan
	{
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x00017FE5 File Offset: 0x000161E5
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x00017FED File Offset: 0x000161ED
		public IList<ExecuteReportPlanItem> ExecutionSteps { get; set; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00017FF6 File Offset: 0x000161F6
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x00017FFE File Offset: 0x000161FE
		public int NumIterations { get; set; }
	}
}
