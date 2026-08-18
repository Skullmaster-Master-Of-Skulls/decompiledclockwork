using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200030F RID: 783
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportResp
	{
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00008676 File Offset: 0x00006876
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x0000867E File Offset: 0x0000687E
		[DataMember]
		public ReportDTO Report { get; set; }
	}
}
