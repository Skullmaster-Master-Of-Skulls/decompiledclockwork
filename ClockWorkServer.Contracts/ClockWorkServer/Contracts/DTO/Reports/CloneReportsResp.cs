using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000335 RID: 821
	[DataContract(Namespace = "http://tpro.ca")]
	public class CloneReportsResp
	{
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00008984 File Offset: 0x00006B84
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x0000898C File Offset: 0x00006B8C
		[DataMember]
		public IDictionary<int, int> OldAndNewReportIds { get; set; }
	}
}
