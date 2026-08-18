using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200029A RID: 666
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderByIdResp
	{
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0000759B File Offset: 0x0000579B
		// (set) Token: 0x06000FAB RID: 4011 RVA: 0x000075A3 File Offset: 0x000057A3
		[DataMember]
		public SPProviderDTO Provider { get; set; }
	}
}
