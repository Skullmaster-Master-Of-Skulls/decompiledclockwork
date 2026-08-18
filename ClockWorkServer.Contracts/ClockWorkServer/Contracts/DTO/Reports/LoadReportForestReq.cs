using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000310 RID: 784
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportForestReq : BaseReportMessageReq
	{
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00008687 File Offset: 0x00006887
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x0000868F File Offset: 0x0000688F
		[DataMember]
		public ReportContextDTO ReportContext { get; set; }
	}
}
