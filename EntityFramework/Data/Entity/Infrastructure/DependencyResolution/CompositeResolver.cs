using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000154 RID: 340
	internal class CompositeResolver<TFirst, TSecond> : IDbDependencyResolver where TFirst : class, IDbDependencyResolver where TSecond : class, IDbDependencyResolver
	{
		// Token: 0x06000B1A RID: 2842 RVA: 0x00037EB8 File Offset: 0x000360B8
		public CompositeResolver(TFirst firstResolver, TSecond secondResolver)
		{
			this._firstResolver = firstResolver;
			this._secondResolver = secondResolver;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00037ECE File Offset: 0x000360CE
		public TFirst First
		{
			get
			{
				return this._firstResolver;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00037ED6 File Offset: 0x000360D6
		public TSecond Second
		{
			get
			{
				return this._secondResolver;
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00037EE0 File Offset: 0x000360E0
		public virtual object GetService(Type type, object key)
		{
			TFirst firstResolver = this._firstResolver;
			object service;
			if ((service = firstResolver.GetService(type, key)) == null)
			{
				TSecond secondResolver = this._secondResolver;
				service = secondResolver.GetService(type, key);
			}
			return service;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00037F20 File Offset: 0x00036120
		public IEnumerable<object> GetServices(Type type, object key)
		{
			TFirst firstResolver = this._firstResolver;
			IEnumerable<object> services = firstResolver.GetServices(type, key);
			TSecond secondResolver = this._secondResolver;
			return services.Concat(secondResolver.GetServices(type, key));
		}

		// Token: 0x0400030A RID: 778
		private readonly TFirst _firstResolver;

		// Token: 0x0400030B RID: 779
		private readonly TSecond _secondResolver;
	}
}
