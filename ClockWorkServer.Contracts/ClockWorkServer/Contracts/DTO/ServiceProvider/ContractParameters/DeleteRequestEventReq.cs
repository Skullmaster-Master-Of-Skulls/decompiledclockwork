using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002D1 RID: 721
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteRequestEventReq : BaseMessageReq
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00007964 File Offset: 0x00005B64
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x0000796C File Offset: 0x00005B6C
		[DataMember]
		public int SPRequestEventId { get; set; }
	}
}
