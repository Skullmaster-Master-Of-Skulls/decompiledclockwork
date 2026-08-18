using System;
using System.Collections.Generic;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000074 RID: 116
	public class PeopleCacheServiceManager : IPeopleCache, IService
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x00014730 File Offset: 0x00012930
		public void LoadAllUserObjectsIntoCache(LoadAllUserObjectsIntoCacheReq request)
		{
			CWLogger.Logger.Trace("PeopleCacheServiceManager:LoadAllUserObjectsIntoCache:Starting to load all user objects cache.");
			IPeopleManager peopleManager = new PeopleManager(request.GetOperationContext());
			List<PersonBase> list = peopleManager.LoadAllUserObjects(true);
			CWLogger.Logger.Trace("PeopleCacheServiceManager:LoadAllUserObjectsIntoCache::Completed loading all user objects cache.  Total count is {0}.", (list == null) ? "0" : list.Count.ToString());
		}
	}
}
