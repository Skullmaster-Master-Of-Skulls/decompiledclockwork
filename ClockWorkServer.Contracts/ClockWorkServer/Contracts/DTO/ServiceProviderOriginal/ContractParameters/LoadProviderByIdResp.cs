using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters
{
	// Token: 0x020002E7 RID: 743
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderByIdResp
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00007F46 File Offset: 0x00006146
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x00007F4E File Offset: 0x0000614E
		[DataMember]
		public ServiceProviderDTO Provider { get; set; }
	}
}
