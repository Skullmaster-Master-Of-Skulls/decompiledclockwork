using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Caching;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Caching
{
	// Token: 0x020008AE RID: 2222
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearServerCacheAllSubItemsReq : BaseMessageReq
	{
		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x00015439 File Offset: 0x00013639
		// (set) Token: 0x06002CF3 RID: 11507 RVA: 0x00015441 File Offset: 0x00013641
		[DataMember]
		public IList<eServerCacheItemType> Keys { get; set; }
	}
}
