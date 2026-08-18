using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Caching
{
	// Token: 0x020008AF RID: 2223
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearCacheItemsReq : BaseMessageReq
	{
		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x0001544A File Offset: 0x0001364A
		// (set) Token: 0x06002CF6 RID: 11510 RVA: 0x00015452 File Offset: 0x00013652
		[DataMember]
		public IList<string> Keys { get; set; }
	}
}
