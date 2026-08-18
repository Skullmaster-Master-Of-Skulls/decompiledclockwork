using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200029E RID: 670
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderByUserNameResp
	{
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x000075DF File Offset: 0x000057DF
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x000075E7 File Offset: 0x000057E7
		[DataMember]
		public SPProviderDTO Provider { get; set; }
	}
}
