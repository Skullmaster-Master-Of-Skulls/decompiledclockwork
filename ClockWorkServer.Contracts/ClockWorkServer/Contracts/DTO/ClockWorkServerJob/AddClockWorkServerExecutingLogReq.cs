using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200086B RID: 2155
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddClockWorkServerExecutingLogReq : BaseMessageReq
	{
		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06002BCF RID: 11215 RVA: 0x00014C34 File Offset: 0x00012E34
		// (set) Token: 0x06002BD0 RID: 11216 RVA: 0x00014C3C File Offset: 0x00012E3C
		[DataMember]
		public ClockWorkServerJobExecutionLogDTO ClockWorkServerJobExecutionLog { get; set; }
	}
}
