using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200029C RID: 668
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderByStudent_noResp
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x000075BD File Offset: 0x000057BD
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x000075C5 File Offset: 0x000057C5
		[DataMember]
		public SPProviderDTO Provider { get; set; }
	}
}
