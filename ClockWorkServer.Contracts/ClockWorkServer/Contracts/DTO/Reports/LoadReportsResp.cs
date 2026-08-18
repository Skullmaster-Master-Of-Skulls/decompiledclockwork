using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000313 RID: 787
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportsResp
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x000086BA File Offset: 0x000068BA
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x000086C2 File Offset: 0x000068C2
		[DataMember]
		public ReportCollectionDTO Reports { get; set; }
	}
}
