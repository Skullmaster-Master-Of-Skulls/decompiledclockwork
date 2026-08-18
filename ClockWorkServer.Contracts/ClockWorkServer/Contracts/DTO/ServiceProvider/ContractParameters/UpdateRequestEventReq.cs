using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002D3 RID: 723
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRequestEventReq : BaseMessageReq
	{
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x00007975 File Offset: 0x00005B75
		// (set) Token: 0x06001058 RID: 4184 RVA: 0x0000797D File Offset: 0x00005B7D
		[DataMember]
		public SPRequestEventDTO RequestEvent { get; set; }
	}
}
