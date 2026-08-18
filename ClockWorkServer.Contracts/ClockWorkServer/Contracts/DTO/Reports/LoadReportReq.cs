using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200030E RID: 782
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportReq : BaseReportMessageReq
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00008665 File Offset: 0x00006865
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x0000866D File Offset: 0x0000686D
		[DataMember]
		public int ReportId { get; set; }
	}
}
