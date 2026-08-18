using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Caching;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Caching;

namespace TechnoPro.Common.ClientManager.ICore.Caching
{
	// Token: 0x02000075 RID: 117
	[Obsolete("Avoid using this class. In use now in some places for legacy code reasons.")]
	public interface IServerCacheClientManager : IWebService
	{
		// Token: 0x06000361 RID: 865
		void ClearServerCache(params eServerCacheItemType[] keys);

		// Token: 0x06000362 RID: 866
		void ClearAllUsersCache(params eServerCacheItemType[] keys);

		// Token: 0x06000363 RID: 867
		void ClearServerCache(params ServerCacheItemDTO[] items);

		// Token: 0x06000364 RID: 868
		void ClearServerCacheItems(params string[] keys);

		// Token: 0x06000365 RID: 869
		void ClearServerCacheAllSubItems(params eServerCacheItemType[] keys);

		// Token: 0x06000366 RID: 870
		void ClearServerCacheForStudents();

		// Token: 0x06000367 RID: 871
		void ClearServerCacheForAllUsers();

		// Token: 0x06000368 RID: 872
		void ClearServerCacheForRooms();

		// Token: 0x06000369 RID: 873
		void ClearServerCacheForResources();

		// Token: 0x0600036A RID: 874
		void ClearServerCacheForGroups();
	}
}
