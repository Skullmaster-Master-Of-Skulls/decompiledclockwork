using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200028A RID: 650
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadApplicationsBySPProviderTypeResp
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x00007425 File Offset: 0x00005625
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x0000742D File Offset: 0x0000562D
		[DataMember]
		public IList<SPApplicationDTO> Applications { get; set; }
	}
}
