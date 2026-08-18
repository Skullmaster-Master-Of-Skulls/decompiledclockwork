using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults
{
	// Token: 0x02000354 RID: 852
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteReportPlanItemDTO
	{
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0000919C File Offset: 0x0000739C
		// (set) Token: 0x0600137A RID: 4986 RVA: 0x000091A4 File Offset: 0x000073A4
		[DataMember]
		public IList<int> ReportFunctionIdsToRun { get; set; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x000091AD File Offset: 0x000073AD
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x000091B5 File Offset: 0x000073B5
		[DataMember]
		public bool HasCompleted { get; set; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x000091BE File Offset: 0x000073BE
		// (set) Token: 0x0600137E RID: 4990 RVA: 0x000091C6 File Offset: 0x000073C6
		[DataMember]
		public bool RunOnClient { get; set; }
	}
}
