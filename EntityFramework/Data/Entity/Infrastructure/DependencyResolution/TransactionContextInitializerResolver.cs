using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000145 RID: 325
	internal class TransactionContextInitializerResolver : IDbDependencyResolver
	{
		// Token: 0x06000AB6 RID: 2742 RVA: 0x00036BD4 File Offset: 0x00034DD4
		public object GetService(Type type, object key)
		{
			Check.NotNull<Type>(type, "type");
			Type type2 = type.TryGetElementType(typeof(IDatabaseInitializer<>));
			if (type2 != null && typeof(TransactionContext).IsAssignableFrom(type2))
			{
				return this._initializers.GetOrAdd(type2, new Func<Type, object>(this.CreateInitializerInstance));
			}
			return null;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00036C34 File Offset: 0x00034E34
		private object CreateInitializerInstance(Type type)
		{
			Type typeFromHandle = typeof(TransactionContextInitializer<>);
			Type type2 = typeFromHandle.MakeGenericType(new Type[]
			{
				type
			});
			return Activator.CreateInstance(type2);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00036C65 File Offset: 0x00034E65
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}

		// Token: 0x040002DE RID: 734
		private readonly ConcurrentDictionary<Type, object> _initializers = new ConcurrentDictionary<Type, object>();
	}
}
