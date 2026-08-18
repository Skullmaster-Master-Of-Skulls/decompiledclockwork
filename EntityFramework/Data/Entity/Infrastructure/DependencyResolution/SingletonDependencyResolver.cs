using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000166 RID: 358
	public class SingletonDependencyResolver<T> : IDbDependencyResolver where T : class
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x0003966C File Offset: 0x0003786C
		public SingletonDependencyResolver(T singletonInstance) : this(singletonInstance, null)
		{
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00039698 File Offset: 0x00037898
		public SingletonDependencyResolver(T singletonInstance, object key)
		{
			Check.NotNull<T>(singletonInstance, "singletonInstance");
			this._singletonInstance = singletonInstance;
			this._keyPredicate = ((object k) => key == null || object.Equals(key, k));
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000396E4 File Offset: 0x000378E4
		public SingletonDependencyResolver(T singletonInstance, Func<object, bool> keyPredicate)
		{
			Check.NotNull<T>(singletonInstance, "singletonInstance");
			Check.NotNull<Func<object, bool>>(keyPredicate, "keyPredicate");
			this._singletonInstance = singletonInstance;
			this._keyPredicate = keyPredicate;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00039714 File Offset: 0x00037914
		public object GetService(Type type, object key)
		{
			return (type == typeof(T) && this._keyPredicate(key)) ? this._singletonInstance : default(T);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00039757 File Offset: 0x00037957
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}

		// Token: 0x04000338 RID: 824
		private readonly T _singletonInstance;

		// Token: 0x04000339 RID: 825
		private readonly Func<object, bool> _keyPredicate;
	}
}
