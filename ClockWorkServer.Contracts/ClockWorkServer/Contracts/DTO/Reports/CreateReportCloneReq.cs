using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200032A RID: 810
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateReportCloneReq : BaseReportMessageReq
	{
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x000088C9 File Offset: 0x00006AC9
		// (set) Token: 0x06001256 RID: 4694 RVA: 0x000088D1 File Offset: 0x00006AD1
		[DataMember]
		public int ReportId { get; set; }
	}
}
