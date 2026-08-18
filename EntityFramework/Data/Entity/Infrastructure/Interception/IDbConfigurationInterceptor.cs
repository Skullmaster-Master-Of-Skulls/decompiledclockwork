using System;
using System.Data.Entity.Infrastructure.DependencyResolution;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000149 RID: 329
	public interface IDbConfigurationInterceptor : IDbInterceptor
	{
		// Token: 0x06000AC2 RID: 2754
		void Loaded(DbConfigurationLoadedEventArgs loadedEventArgs, DbConfigurationInterceptionContext interceptionContext);
	}
}
