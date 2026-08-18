using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Caching;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Caching
{
	// Token: 0x020008AD RID: 2221
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearAllUsersCacheReq : BaseMessageReq
	{
		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x00015428 File Offset: 0x00013628
		// (set) Token: 0x06002CF0 RID: 11504 RVA: 0x00015430 File Offset: 0x00013630
		[DataMember]
		public IList<eServerCacheItemType> Keys { get; set; }
	}
}
