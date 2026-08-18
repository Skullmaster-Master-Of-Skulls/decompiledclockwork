using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002B0 RID: 688
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderTypeByIdResp
	{
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x000076EF File Offset: 0x000058EF
		// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x000076F7 File Offset: 0x000058F7
		[DataMember]
		public SPProviderTypeDTO ProviderType { get; set; }
	}
}
