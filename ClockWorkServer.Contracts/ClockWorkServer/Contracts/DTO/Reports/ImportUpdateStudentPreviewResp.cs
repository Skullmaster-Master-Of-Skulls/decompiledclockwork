using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200034E RID: 846
	[DataContract(Namespace = "http://tpro.ca")]
	public class ImportUpdateStudentPreviewResp
	{
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x000090F2 File Offset: 0x000072F2
		// (set) Token: 0x06001361 RID: 4961 RVA: 0x000090FA File Offset: 0x000072FA
		[DataMember]
		public ExecuteReportResultDTO ReportResult { get; set; }
	}
}
