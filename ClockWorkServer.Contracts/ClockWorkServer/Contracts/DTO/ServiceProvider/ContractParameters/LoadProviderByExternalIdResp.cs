using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002A0 RID: 672
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderByExternalIdResp
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00007601 File Offset: 0x00005801
		// (set) Token: 0x06000FBD RID: 4029 RVA: 0x00007609 File Offset: 0x00005809
		[DataMember]
		public SPProviderDTO Provider { get; set; }
	}
}
