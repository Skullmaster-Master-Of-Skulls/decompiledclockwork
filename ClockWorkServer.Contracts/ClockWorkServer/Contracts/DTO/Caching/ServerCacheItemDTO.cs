using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Caching;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Caching
{
	// Token: 0x020008AB RID: 2219
	[DataContract(Namespace = "http://tpro.ca")]
	public class ServerCacheItemDTO
	{
		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x000153F5 File Offset: 0x000135F5
		// (set) Token: 0x06002CE8 RID: 11496 RVA: 0x000153FD File Offset: 0x000135FD
		[DataMember]
		public eServerCacheItemType ServerCacheItemType { get; set; }

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x00015406 File Offset: 0x00013606
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x0001540E File Offset: 0x0001360E
		[DataMember]
		public int SubItemId { get; set; }
	}
}
