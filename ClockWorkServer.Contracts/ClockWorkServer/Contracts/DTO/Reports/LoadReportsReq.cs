using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000312 RID: 786
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportsReq : BaseReportMessageReq
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x000086A9 File Offset: 0x000068A9
		// (set) Token: 0x060011FE RID: 4606 RVA: 0x000086B1 File Offset: 0x000068B1
		[DataMember]
		public ReportContextDTO ReportContext { get; set; }
	}
}
