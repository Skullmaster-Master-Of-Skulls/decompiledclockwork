using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Startup;

namespace TechnoPro.Common.ICore.Startup
{
	// Token: 0x02000035 RID: 53
	public interface IStartupManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000161 RID: 353
		CacheClusterFull LoadCacheClusterFull();
	}
}
