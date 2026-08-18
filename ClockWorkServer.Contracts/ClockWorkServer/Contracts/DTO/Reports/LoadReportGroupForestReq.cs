using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000304 RID: 772
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportGroupForestReq : BaseReportMessageReq
	{
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00008511 File Offset: 0x00006711
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x00008519 File Offset: 0x00006719
		[DataMember]
		public ReportContextDTO ReportContext { get; set; }
	}
}
