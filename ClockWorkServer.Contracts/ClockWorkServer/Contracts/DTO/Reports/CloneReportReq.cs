using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000332 RID: 818
	[DataContract(Namespace = "http://tpro.ca")]
	public class CloneReportReq : BaseReportMessageReq
	{
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00008951 File Offset: 0x00006B51
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x00008959 File Offset: 0x00006B59
		[DataMember]
		public int ReportId { get; set; }
	}
}
