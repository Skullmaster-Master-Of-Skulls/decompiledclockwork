using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200032B RID: 811
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateReportCloneResp
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x000088DA File Offset: 0x00006ADA
		// (set) Token: 0x06001259 RID: 4697 RVA: 0x000088E2 File Offset: 0x00006AE2
		[DataMember]
		public ReportDTO Report { get; set; }
	}
}
