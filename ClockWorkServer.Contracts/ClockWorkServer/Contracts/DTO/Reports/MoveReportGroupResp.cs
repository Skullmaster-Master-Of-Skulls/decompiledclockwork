using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000303 RID: 771
	[DataContract(Namespace = "http://tpro.ca")]
	public class MoveReportGroupResp
	{
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00008500 File Offset: 0x00006700
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x00008508 File Offset: 0x00006708
		[DataMember]
		public ReportGroupDTO ReportGroup { get; set; }
	}
}
