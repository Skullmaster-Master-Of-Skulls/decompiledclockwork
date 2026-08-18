using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002CF RID: 719
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateRequestEventReq : BaseMessageReq
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x00007942 File Offset: 0x00005B42
		// (set) Token: 0x0600104E RID: 4174 RVA: 0x0000794A File Offset: 0x00005B4A
		[DataMember]
		public int SPRequestId { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x00007953 File Offset: 0x00005B53
		// (set) Token: 0x06001050 RID: 4176 RVA: 0x0000795B File Offset: 0x00005B5B
		[DataMember]
		public SPRequestEventDTO RequestEvent { get; set; }
	}
}
