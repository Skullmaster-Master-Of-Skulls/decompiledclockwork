using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000871 RID: 2161
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunClockWorkServerJobNowReq : BaseMessageReq
	{
		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x00014C78 File Offset: 0x00012E78
		// (set) Token: 0x06002BDE RID: 11230 RVA: 0x00014C80 File Offset: 0x00012E80
		[DataMember]
		public int JobId { get; set; }
	}
}
