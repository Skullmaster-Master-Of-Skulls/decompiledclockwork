using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002B2 RID: 690
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderTypeByBehaviourCodeResp
	{
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00007711 File Offset: 0x00005911
		// (set) Token: 0x06000FEF RID: 4079 RVA: 0x00007719 File Offset: 0x00005919
		[DataMember]
		public IList<SPProviderTypeDTO> ProviderTypes { get; set; }
	}
}
