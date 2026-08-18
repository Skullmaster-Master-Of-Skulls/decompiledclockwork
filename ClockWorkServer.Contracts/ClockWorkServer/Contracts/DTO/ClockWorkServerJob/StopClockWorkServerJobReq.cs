using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200086F RID: 2159
	[DataContract(Namespace = "http://tpro.ca")]
	public class StopClockWorkServerJobReq : BaseMessageReq
	{
		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x00014C67 File Offset: 0x00012E67
		// (set) Token: 0x06002BDA RID: 11226 RVA: 0x00014C6F File Offset: 0x00012E6F
		[DataMember]
		public int JobId { get; set; }
	}
}
