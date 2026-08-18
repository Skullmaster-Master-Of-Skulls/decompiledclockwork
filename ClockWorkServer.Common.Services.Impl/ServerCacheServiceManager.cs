using System;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Caching;
using TechnoPro.Common.Core;
using TechnoPro.Common.Core.Mappers.Caching;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000027 RID: 39
	public class ServerCacheServiceManager : IServerCache, IService
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00008C78 File Offset: 0x00006E78
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008C8C File Offset: 0x00006E8C
		public void ClearServerCache(ClearServerCacheReq Request)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			foreach (ServerCacheItemDTO serverCacheItemDTO in Request.Keys)
			{
				bool flag = serverCacheItemDTO.SubItemId > 0;
				if (flag)
				{
					cacheStorageManager.Remove(serverCacheItemDTO.ToDomainObject());
				}
				else
				{
					string name = Enum.GetName(typeof(eServerCacheItemType), serverCacheItemDTO.ServerCacheItemType);
					cacheStorageManager.Remove(name);
				}
				CWLogger.Logger.Trace("ServerCacheServiceManager:ClearServerCache:ClearedKey={0}", serverCacheItemDTO.ServerCacheItemType.ToString());
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008D48 File Offset: 0x00006F48
		public void ClearAllUsersCache(ClearAllUsersCacheReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			UserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((operationContext != null) ? operationContext.TenantId : null);
			foreach (eServerCacheItemType eServerCacheItemType in Request.Keys)
			{
				userDatabaseCacheStorageManager.Clear(eServerCacheItemType);
				CWLogger.Logger.Trace("ServerCacheServiceManager:ClearServerCache:ClearedKey={0}", eServerCacheItemType.ToString());
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public void ClearServerCacheAllSubItems(ClearServerCacheAllSubItemsReq Request)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			foreach (eServerCacheItemType key in Request.Keys)
			{
				cacheStorageManager.RemoveAllSubItems(key);
				CWLogger.Logger.Trace("ServerCacheServiceManager:ClearServerCacheAllSubItems:ClearedKey={0}", key.ToString());
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008E50 File Offset: 0x00007050
		public void ClearCacheItems(ClearCacheItemsReq Request)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			foreach (string key in Request.Keys)
			{
				cacheStorageManager.Remove(key);
			}
		}
	}
}
