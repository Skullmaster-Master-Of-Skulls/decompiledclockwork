using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200034C RID: 844
	[DataContract(Namespace = "http://tpro.ca")]
	public class ImportUpdateStudentResp
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00009037 File Offset: 0x00007237
		// (set) Token: 0x06001349 RID: 4937 RVA: 0x0000903F File Offset: 0x0000723F
		[DataMember]
		public ExecuteReportResultDTO ReportResult { get; set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x00009048 File Offset: 0x00007248
		// (set) Token: 0x0600134B RID: 4939 RVA: 0x00009050 File Offset: 0x00007250
		[DataMember]
		public int PersonId { get; set; }
	}
}
