using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000152 RID: 338
	internal class CachingDependencyResolver : IDbDependencyResolver
	{
		// Token: 0x06000B14 RID: 2836 RVA: 0x00037D3F File Offset: 0x00035F3F
		public CachingDependencyResolver(IDbDependencyResolver underlyingResolver)
		{
			this._underlyingResolver = underlyingResolver;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00037D8C File Offset: 0x00035F8C
		public virtual object GetService(Type type, object key)
		{
			return this._resolvedDependencies.GetOrAdd(Tuple.Create<Type, object>(type, key), (Tuple<Type, object> k) => this._underlyingResolver.GetService(type, key));
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00037E04 File Offset: 0x00036004
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this._resolvedAllDependencies.GetOrAdd(Tuple.Create<Type, object>(type, key), (Tuple<Type, object> k) => this._underlyingResolver.GetServices(type, key));
		}

		// Token: 0x04000307 RID: 775
		private readonly IDbDependencyResolver _underlyingResolver;

		// Token: 0x04000308 RID: 776
		private readonly ConcurrentDictionary<Tuple<Type, object>, object> _resolvedDependencies = new ConcurrentDictionary<Tuple<Type, object>, object>();

		// Token: 0x04000309 RID: 777
		private readonly ConcurrentDictionary<Tuple<Type, object>, IEnumerable<object>> _resolvedAllDependencies = new ConcurrentDictionary<Tuple<Type, object>, IEnumerable<object>>();
	}
}
