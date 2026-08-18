using System;
using System.Data.Entity.Infrastructure.DependencyResolution;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200014B RID: 331
	internal class DbConfigurationDispatcher
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00037014 File Offset: 0x00035214
		public InternalDispatcher<IDbConfigurationInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00037038 File Offset: 0x00035238
		public virtual void Loaded(DbConfigurationLoadedEventArgs loadedEventArgs, DbInterceptionContext interceptionContext)
		{
			DbConfigurationInterceptionContext clonedInterceptionContext = new DbConfigurationInterceptionContext(interceptionContext);
			this._internalDispatcher.Dispatch(delegate(IDbConfigurationInterceptor i)
			{
				i.Loaded(loadedEventArgs, clonedInterceptionContext);
			});
		}

		// Token: 0x040002E7 RID: 743
		private readonly InternalDispatcher<IDbConfigurationInterceptor> _internalDispatcher = new InternalDispatcher<IDbConfigurationInterceptor>();
	}
}
