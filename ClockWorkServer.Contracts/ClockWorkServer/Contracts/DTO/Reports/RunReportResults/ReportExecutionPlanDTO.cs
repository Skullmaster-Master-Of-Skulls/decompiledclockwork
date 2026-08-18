using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults
{
	// Token: 0x02000355 RID: 853
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReportExecutionPlanDTO
	{
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x000091CF File Offset: 0x000073CF
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x000091D7 File Offset: 0x000073D7
		[DataMember]
		public IList<ExecuteReportPlanItemDTO> ExecutionSteps { get; set; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x000091E0 File Offset: 0x000073E0
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x000091E8 File Offset: 0x000073E8
		[DataMember]
		public int NumIterations { get; set; }
	}
}
