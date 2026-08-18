using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000301 RID: 769
	[DataContract(Namespace = "http://tpro.ca")]
	public class MoveReportResp
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x000084AB File Offset: 0x000066AB
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x000084B3 File Offset: 0x000066B3
		[DataMember]
		public ReportDTO Report { get; set; }
	}
}
