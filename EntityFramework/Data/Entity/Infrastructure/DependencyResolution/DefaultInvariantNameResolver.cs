using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x0200015B RID: 347
	internal class DefaultInvariantNameResolver : IDbDependencyResolver
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x000389E8 File Offset: 0x00036BE8
		public virtual object GetService(Type type, object key)
		{
			if (!(type == typeof(IProviderInvariantName)))
			{
				return null;
			}
			DbProviderFactory dbProviderFactory = key as DbProviderFactory;
			if (dbProviderFactory == null)
			{
				throw new ArgumentException(Strings.DbDependencyResolver_InvalidKey(typeof(DbProviderFactory).Name, typeof(IProviderInvariantName)));
			}
			return new ProviderInvariantName(dbProviderFactory.GetProviderInvariantName());
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00038A42 File Offset: 0x00036C42
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}
	}
}
