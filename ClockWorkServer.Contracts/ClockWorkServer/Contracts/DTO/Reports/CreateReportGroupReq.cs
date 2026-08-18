using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000348 RID: 840
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateReportGroupReq : BaseReportMessageReq
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x00008FF3 File Offset: 0x000071F3
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x00008FFB File Offset: 0x000071FB
		[DataMember]
		public ReportGroupDTO Group { get; set; }
	}
}
