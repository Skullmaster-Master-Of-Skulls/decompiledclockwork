using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000868 RID: 2152
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerExecutingLogsByJobResp
	{
		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x00014BF0 File Offset: 0x00012DF0
		// (set) Token: 0x06002BC5 RID: 11205 RVA: 0x00014BF8 File Offset: 0x00012DF8
		[DataMember]
		public IList<ClockWorkServerJobExecutionLogDTO> ClockWorkServerJobExecutionLogList { get; set; }
	}
}
