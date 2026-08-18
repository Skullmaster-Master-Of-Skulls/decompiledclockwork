using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000350 RID: 848
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportsByGroupReq
	{
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x00009169 File Offset: 0x00007369
		// (set) Token: 0x06001371 RID: 4977 RVA: 0x00009171 File Offset: 0x00007371
		[DataMember]
		public IList<string> ReportGroupNames { get; set; }
	}
}
