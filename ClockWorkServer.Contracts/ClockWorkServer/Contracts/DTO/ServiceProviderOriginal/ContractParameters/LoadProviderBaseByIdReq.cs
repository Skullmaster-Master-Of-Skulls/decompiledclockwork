using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters
{
	// Token: 0x020002EA RID: 746
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderBaseByIdReq : BaseMessageReq
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00007F79 File Offset: 0x00006179
		// (set) Token: 0x06001121 RID: 4385 RVA: 0x00007F81 File Offset: 0x00006181
		[DataMember]
		public int ServiceProviderId { get; set; }
	}
}
