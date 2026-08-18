using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Caching
{
	// Token: 0x020008AC RID: 2220
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearServerCacheReq : BaseMessageReq
	{
		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x00015417 File Offset: 0x00013617
		// (set) Token: 0x06002CED RID: 11501 RVA: 0x0001541F File Offset: 0x0001361F
		[DataMember]
		public IList<ServerCacheItemDTO> Keys { get; set; }
	}
}
