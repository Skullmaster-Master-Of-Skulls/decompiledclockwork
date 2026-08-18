using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002B4 RID: 692
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllProviderTypesResp
	{
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x00007733 File Offset: 0x00005933
		// (set) Token: 0x06000FF5 RID: 4085 RVA: 0x0000773B File Offset: 0x0000593B
		[DataMember]
		public IList<SPProviderTypeDTO> ProviderTypes { get; set; }
	}
}
