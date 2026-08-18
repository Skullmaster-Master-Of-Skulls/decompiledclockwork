using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000155 RID: 341
	internal class DatabaseInitializerResolver : IDbDependencyResolver
	{
		// Token: 0x06000B1F RID: 2847 RVA: 0x00037F60 File Offset: 0x00036160
		public virtual object GetService(Type type, object key)
		{
			Type type2 = type.TryGetElementType(typeof(IDatabaseInitializer<>));
			object result;
			if (type2 != null && this._initializers.TryGetValue(type2, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00037FAC File Offset: 0x000361AC
		public virtual void SetInitializer(Type contextType, object initializer)
		{
			this._initializers.AddOrUpdate(contextType, initializer, (Type c, object i) => initializer);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00037FE5 File Offset: 0x000361E5
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}

		// Token: 0x0400030C RID: 780
		private readonly ConcurrentDictionary<Type, object> _initializers = new ConcurrentDictionary<Type, object>();
	}
}
