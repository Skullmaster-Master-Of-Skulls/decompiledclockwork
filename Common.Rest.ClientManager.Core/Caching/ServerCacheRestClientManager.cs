using System;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Caching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Caching;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Caching
{
	// Token: 0x02000067 RID: 103
	public class ServerCacheRestClientManager : BearerTokenRestProxy<IServerCacheClientManager>, IServerCacheClientManager, IWebService
	{
		// Token: 0x060003DE RID: 990 RVA: 0x0000B97C File Offset: 0x00009B7C
		public ServerCacheRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000B986 File Offset: 0x00009B86
		public ServerCacheRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000B994 File Offset: 0x00009B94
		public void ClearServerCache(params eServerCacheItemType[] keys)
		{
			ClearServerCacheReq clearServerCacheReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearServerCacheReq>();
			clearServerCacheReq.Keys = (from k in keys
			select new ServerCacheItemDTO
			{
				ServerCacheItemType = k
			}).ToList<ServerCacheItemDTO>();
			base.Post<ClearServerCacheReq>(clearServerCacheReq, "servercache/clear");
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		public void ClearAllUsersCache(params eServerCacheItemType[] keys)
		{
			ClearAllUsersCacheReq clearAllUsersCacheReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearAllUsersCacheReq>();
			clearAllUsersCacheReq.Keys = keys;
			base.Post<ClearAllUsersCacheReq>(clearAllUsersCacheReq, "servercache/clearallusers");
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000BA14 File Offset: 0x00009C14
		public void ClearServerCache(params ServerCacheItemDTO[] items)
		{
			ClearServerCacheReq clearServerCacheReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearServerCacheReq>();
			clearServerCacheReq.Keys = items;
			base.Post<ClearServerCacheReq>(clearServerCacheReq, "servercache/clear");
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000BA40 File Offset: 0x00009C40
		public void ClearServerCacheItems(params string[] keys)
		{
			ClearCacheItemsReq clearCacheItemsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearCacheItemsReq>();
			clearCacheItemsReq.Keys = keys;
			base.Post<ClearCacheItemsReq>(clearCacheItemsReq, "servercache/clearitems");
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000BA6C File Offset: 0x00009C6C
		public void ClearServerCacheAllSubItems(params eServerCacheItemType[] keys)
		{
			ClearServerCacheAllSubItemsReq clearServerCacheAllSubItemsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearServerCacheAllSubItemsReq>();
			clearServerCacheAllSubItemsReq.Keys = keys;
			base.Post<ClearServerCacheAllSubItemsReq>(clearServerCacheAllSubItemsReq, "servercache/clearallsubtitems");
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000BA97 File Offset: 0x00009C97
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

		// Token: 0x060003E6 RID: 998 RVA: 0x0000BABC File Offset: 0x00009CBC
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

		// Token: 0x060003E7 RID: 999 RVA: 0x0000BB19 File Offset: 0x00009D19
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

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000BB3B File Offset: 0x00009D3B
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

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000BB5D File Offset: 0x00009D5D
		public void ClearServerCacheForGroups()
		{
			this.ClearServerCache(new eServerCacheItemType[]
			{
				eServerCacheItemType.allGroups
			});
		}
	}
}
