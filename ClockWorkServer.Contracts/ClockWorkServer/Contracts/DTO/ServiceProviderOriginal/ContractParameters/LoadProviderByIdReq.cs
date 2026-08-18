using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters
{
	// Token: 0x020002E8 RID: 744
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderByIdReq : BaseMessageReq
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00007F57 File Offset: 0x00006157
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x00007F5F File Offset: 0x0000615F
		[DataMember]
		public int ServiceProviderId { get; set; }
	}
}
