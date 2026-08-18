using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x0200015A RID: 346
	internal class DefaultExecutionStrategyResolver : IDbDependencyResolver
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x00038960 File Offset: 0x00036B60
		public object GetService(Type type, object key)
		{
			if (!(type == typeof(Func<IDbExecutionStrategy>)))
			{
				return null;
			}
			Check.NotNull<object>(key, "key");
			if (!(key is ExecutionStrategyKey))
			{
				throw new ArgumentException(Strings.DbDependencyResolver_InvalidKey(typeof(ExecutionStrategyKey).Name, "Func<IExecutionStrategy>"));
			}
			return new Func<IDbExecutionStrategy>(() => new DefaultExecutionStrategy());
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000389D3 File Offset: 0x00036BD3
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}
	}
}
