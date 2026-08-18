using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000164 RID: 356
	internal class ResolverChain : IDbDependencyResolver
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x00039251 File Offset: 0x00037451
		public virtual void Add(IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			this._resolvers.Push(resolver);
			this._resolversSnapshot = this._resolvers.ToArray();
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0003927E File Offset: 0x0003747E
		public virtual IEnumerable<IDbDependencyResolver> Resolvers
		{
			get
			{
				return this._resolversSnapshot.Reverse<IDbDependencyResolver>();
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000392B4 File Offset: 0x000374B4
		public virtual object GetService(Type type, object key)
		{
			return (from r in this._resolversSnapshot
			select r.GetService(type, key)).FirstOrDefault((object s) => s != null);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0003932C File Offset: 0x0003752C
		public virtual IEnumerable<object> GetServices(Type type, object key)
		{
			return this._resolversSnapshot.SelectMany((IDbDependencyResolver r) => r.GetServices(type, key));
		}

		// Token: 0x0400032C RID: 812
		private readonly ConcurrentStack<IDbDependencyResolver> _resolvers = new ConcurrentStack<IDbDependencyResolver>();

		// Token: 0x0400032D RID: 813
		private volatile IDbDependencyResolver[] _resolversSnapshot = new IDbDependencyResolver[0];
	}
}
