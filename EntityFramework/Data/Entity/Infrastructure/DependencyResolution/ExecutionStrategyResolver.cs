using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x0200015E RID: 350
	public class ExecutionStrategyResolver<T> : IDbDependencyResolver where T : IDbExecutionStrategy
	{
		// Token: 0x06000B57 RID: 2903 RVA: 0x00038BD3 File Offset: 0x00036DD3
		public ExecutionStrategyResolver(string providerInvariantName, string serverName, Func<T> getExecutionStrategy)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<Func<T>>(getExecutionStrategy, "getExecutionStrategy");
			this._providerInvariantName = providerInvariantName;
			this._serverName = serverName;
			this._getExecutionStrategy = getExecutionStrategy;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00038C08 File Offset: 0x00036E08
		public object GetService(Type type, object key)
		{
			if (!(type == typeof(Func<IDbExecutionStrategy>)))
			{
				return null;
			}
			ExecutionStrategyKey executionStrategyKey = key as ExecutionStrategyKey;
			if (executionStrategyKey == null)
			{
				throw new ArgumentException(Strings.DbDependencyResolver_InvalidKey(typeof(ExecutionStrategyKey).Name, "Func<IExecutionStrategy>"));
			}
			if (!executionStrategyKey.ProviderInvariantName.Equals(this._providerInvariantName, StringComparison.Ordinal))
			{
				return null;
			}
			if (this._serverName != null && !this._serverName.Equals(executionStrategyKey.ServerName, StringComparison.Ordinal))
			{
				return null;
			}
			return this._getExecutionStrategy;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00038C8C File Offset: 0x00036E8C
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}

		// Token: 0x0400031F RID: 799
		private readonly Func<T> _getExecutionStrategy;

		// Token: 0x04000320 RID: 800
		private readonly string _providerInvariantName;

		// Token: 0x04000321 RID: 801
		private readonly string _serverName;
	}
}
