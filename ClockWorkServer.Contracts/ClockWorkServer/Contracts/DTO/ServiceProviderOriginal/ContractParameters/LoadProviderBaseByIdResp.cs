using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters
{
	// Token: 0x020002E9 RID: 745
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderBaseByIdResp
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x00007F68 File Offset: 0x00006168
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x00007F70 File Offset: 0x00006170
		[DataMember]
		public ServiceProviderBaseDTO ProviderBase { get; set; }
	}
}
