using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000167 RID: 359
	internal class WrappingDependencyResolver<TService> : IDbDependencyResolver
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x00039761 File Offset: 0x00037961
		public WrappingDependencyResolver(IDbDependencyResolver snapshot, Func<TService, object, TService> serviceWrapper)
		{
			this._snapshot = snapshot;
			this._serviceWrapper = serviceWrapper;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00039777 File Offset: 0x00037977
		public object GetService(Type type, object key)
		{
			if (!(type == typeof(TService)))
			{
				return null;
			}
			return this._serviceWrapper(this._snapshot.GetService(key), key);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000397CC File Offset: 0x000379CC
		public IEnumerable<object> GetServices(Type type, object key)
		{
			if (!(type == typeof(TService)))
			{
				return Enumerable.Empty<object>();
			}
			return (IEnumerable<object>)(from s in this._snapshot.GetServices(key)
			select this._serviceWrapper(s, key));
		}

		// Token: 0x0400033A RID: 826
		private readonly IDbDependencyResolver _snapshot;

		// Token: 0x0400033B RID: 827
		private readonly Func<TService, object, TService> _serviceWrapper;
	}
}
