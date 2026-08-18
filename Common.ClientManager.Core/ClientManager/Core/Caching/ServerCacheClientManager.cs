using System;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Caching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Caching;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Caching
{
	// Token: 0x0200007C RID: 124
	[Obsolete("Avoid using this class. In use now in some places for legacy code reasons.")]
	public class ServerCacheClientManager : IServerCacheClientManager, IWebService
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00014938 File Offset: 0x00012B38
		public void ClearServerCache(params eServerCacheItemType[] keys)
		{
			ClearServerCacheReq clearServerCacheReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearServerCacheReq>();
			clearServerCacheReq.Keys = (from k in keys
			select new ServerCacheItemDTO
			{
				ServerCacheItemType = k
			}).ToList<ServerCacheItemDTO>();
			ClientServiceFactory.GetClientInstance<IServerCache>().ClearServerCache(clearServerCacheReq);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00014990 File Offset: 0x00012B90
		public void ClearServerCache(params ServerCacheItemDTO[] items)
		{
			ClearServerCacheReq clearServerCacheReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearServerCacheReq>();
			clearServerCacheReq.Keys = items;
			ClientServiceFactory.GetClientInstance<IServerCache>().ClearServerCache(clearServerCacheReq);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000149C0 File Offset: 0x00012BC0
		public void ClearAllUsersCache(params eServerCacheItemType[] keys)
		{
			ClearAllUsersCacheReq clearAllUsersCacheReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearAllUsersCacheReq>();
			clearAllUsersCacheReq.Keys = keys;
			ClientServiceFactory.GetClientInstance<IServerCache>(true, false).ClearAllUsersCache(clearAllUsersCacheReq);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000149EF File Offset: 0x00012BEF
		public void ClearServerCacheForStudents()
		{
			this.ClearServerCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.allUserObjects
			});
			this.ClearAllUsersCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.uAllowedStudentPids
			});
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00014A14 File Offset: 0x00012C14
		public void ClearServerCacheForAllUsers()
		{
			this.ClearServerCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.allUserObjects
			});
			this.ClearAllUsersCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.uAllowedStudentPids
			});
			this.ClearAllUsersCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.uAllowedStaffPids
			});
			this.ClearAllUsersCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.uAllowedRoomPids
			});
			this.ClearAllUsersCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.uAllowedResourcePids
			});
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00014A77 File Offset: 0x00012C77
		public void ClearServerCacheForRooms()
		{
			this.ClearServerCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.allUserObjects
			});
			this.ClearAllUsersCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.uAllowedRoomPids
			});
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00014A9C File Offset: 0x00012C9C
		public void ClearServerCacheForResources()
		{
			this.ClearServerCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.allUserObjects
			});
			this.ClearAllUsersCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.uAllowedResourcePids
			});
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00014AC1 File Offset: 0x00012CC1
		public void ClearServerCacheForGroups()
		{
			this.ClearServerCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.allGroups
			});
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00014AD8 File Offset: 0x00012CD8
		public void ClearServerCacheAllSubItems(params eServerCacheItemType[] keys)
		{
			ClearServerCacheAllSubItemsReq clearServerCacheAllSubItemsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearServerCacheAllSubItemsReq>();
			clearServerCacheAllSubItemsReq.Keys = keys;
			ClientServiceFactory.GetClientInstance<IServerCache>().ClearServerCacheAllSubItems(clearServerCacheAllSubItemsReq);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00014B08 File Offset: 0x00012D08
		public void ClearServerCacheItems(params string[] keys)
		{
			ClearCacheItemsReq clearCacheItemsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearCacheItemsReq>();
			clearCacheItemsReq.Keys = keys;
			ClientServiceFactory.GetClientInstance<IServerCache>().ClearCacheItems(clearCacheItemsReq);
		}
	}
}
