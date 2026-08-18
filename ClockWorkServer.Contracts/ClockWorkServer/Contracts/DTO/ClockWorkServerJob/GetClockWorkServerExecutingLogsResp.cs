using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200086A RID: 2154
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerExecutingLogsResp
	{
		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x00014C23 File Offset: 0x00012E23
		// (set) Token: 0x06002BCD RID: 11213 RVA: 0x00014C2B File Offset: 0x00012E2B
		[DataMember]
		public IList<ClockWorkServerJobExecutionLogDTO> ClockWorkServerJobExecutionLogList { get; set; }
	}
}
