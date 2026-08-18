using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000309 RID: 777
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportsInAGroupByGroupIdResp
	{
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x000085FF File Offset: 0x000067FF
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x00008607 File Offset: 0x00006807
		[DataMember]
		public ReportCollectionDTO ReportCollection { get; set; }
	}
}
