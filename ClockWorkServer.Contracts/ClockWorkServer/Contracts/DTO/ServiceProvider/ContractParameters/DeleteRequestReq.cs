using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002C7 RID: 711
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteRequestReq : BaseMessageReq
	{
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x000078CB File Offset: 0x00005ACB
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x000078D3 File Offset: 0x00005AD3
		[DataMember]
		public int SPRequestId { get; set; }
	}
}
