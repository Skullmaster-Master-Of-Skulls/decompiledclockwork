using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200028C RID: 652
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadApplicationsBySPProviderResp
	{
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0000747A File Offset: 0x0000567A
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x00007482 File Offset: 0x00005682
		[DataMember]
		public IList<SPApplicationDTO> Applications { get; set; }
	}
}
