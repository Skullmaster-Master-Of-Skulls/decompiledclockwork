using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200034A RID: 842
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateReportReq : BaseReportMessageReq
	{
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x00009015 File Offset: 0x00007215
		// (set) Token: 0x06001343 RID: 4931 RVA: 0x0000901D File Offset: 0x0000721D
		[DataMember]
		public ReportDTO Report { get; set; }
	}
}
