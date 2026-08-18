using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000307 RID: 775
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteReportResp
	{
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x000085CC File Offset: 0x000067CC
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x000085D4 File Offset: 0x000067D4
		[DataMember]
		public RunReportResultDTO ReportResult { get; set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x000085DD File Offset: 0x000067DD
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x000085E5 File Offset: 0x000067E5
		[DataMember]
		public ReportExecutionPlanDTO ExecutionPlan { get; set; }
	}
}
