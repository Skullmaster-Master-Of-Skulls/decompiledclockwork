using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000308 RID: 776
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportsInAGroupByGroupIdReq : BaseReportMessageReq
	{
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x000085EE File Offset: 0x000067EE
		// (set) Token: 0x060011DE RID: 4574 RVA: 0x000085F6 File Offset: 0x000067F6
		[DataMember]
		public int GroupId { get; set; }
	}
}
