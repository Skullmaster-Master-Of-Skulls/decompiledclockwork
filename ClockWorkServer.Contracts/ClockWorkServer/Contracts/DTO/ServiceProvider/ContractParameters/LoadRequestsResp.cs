using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002C0 RID: 704
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestsResp
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x000077FF File Offset: 0x000059FF
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x00007807 File Offset: 0x00005A07
		[DataMember]
		public IList<SPRequestDTO> Requests { get; set; }
	}
}
