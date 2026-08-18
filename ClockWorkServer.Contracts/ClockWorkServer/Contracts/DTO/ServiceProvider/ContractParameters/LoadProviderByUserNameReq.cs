using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200029F RID: 671
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderByUserNameReq : BaseMessageReq
	{
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x000075F0 File Offset: 0x000057F0
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x000075F8 File Offset: 0x000057F8
		[DataMember]
		public string UserName { get; set; }
	}
}
