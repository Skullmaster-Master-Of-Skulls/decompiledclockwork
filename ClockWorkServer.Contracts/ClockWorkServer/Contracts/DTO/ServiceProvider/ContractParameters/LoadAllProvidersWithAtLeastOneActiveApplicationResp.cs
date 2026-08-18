using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002AE RID: 686
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllProvidersWithAtLeastOneActiveApplicationResp
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x000076BC File Offset: 0x000058BC
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x000076C4 File Offset: 0x000058C4
		[DataMember]
		public IList<SPProviderDTO> Providers { get; set; }
	}
}
