using System;

namespace TechnoPro.Common.Public.Entities.Reports.RunReportResults
{
	// Token: 0x02000237 RID: 567
	public class RunFunctionResult
	{
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x000180FF File Offset: 0x000162FF
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x00018107 File Offset: 0x00016307
		public ReportFunction Function { get; set; }

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00018110 File Offset: 0x00016310
		// (set) Token: 0x06001146 RID: 4422 RVA: 0x00018118 File Offset: 0x00016318
		public RunStatus Status { get; set; }
	}
}
