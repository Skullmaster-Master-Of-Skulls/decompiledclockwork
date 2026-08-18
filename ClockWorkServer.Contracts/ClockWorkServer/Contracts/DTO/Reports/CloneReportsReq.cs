using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000334 RID: 820
	[DataContract(Namespace = "http://tpro.ca")]
	public class CloneReportsReq : BaseReportMessageReq
	{
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x00008973 File Offset: 0x00006B73
		// (set) Token: 0x06001274 RID: 4724 RVA: 0x0000897B File Offset: 0x00006B7B
		[DataMember]
		public IList<int> ReportIds { get; set; }
	}
}
