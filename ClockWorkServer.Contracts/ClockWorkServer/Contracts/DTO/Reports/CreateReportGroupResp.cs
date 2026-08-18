using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000349 RID: 841
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateReportGroupResp
	{
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x00009004 File Offset: 0x00007204
		// (set) Token: 0x06001340 RID: 4928 RVA: 0x0000900C File Offset: 0x0000720C
		[DataMember]
		public int ReportGroupId { get; set; }
	}
}
