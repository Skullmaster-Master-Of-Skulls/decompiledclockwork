using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000146 RID: 326
	public class TransactionHandlerResolver : IDbDependencyResolver
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x00036C82 File Offset: 0x00034E82
		public TransactionHandlerResolver(Func<TransactionHandler> transactionHandlerFactory, string providerInvariantName, string serverName)
		{
			Check.NotNull<Func<TransactionHandler>>(transactionHandlerFactory, "transactionHandlerFactory");
			this._providerInvariantName = providerInvariantName;
			this._serverName = serverName;
			this._transactionHandlerFactory = transactionHandlerFactory;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00036CAC File Offset: 0x00034EAC
		public object GetService(Type type, object key)
		{
			if (!(type == typeof(Func<TransactionHandler>)))
			{
				return null;
			}
			ExecutionStrategyKey executionStrategyKey = key as ExecutionStrategyKey;
			if (executionStrategyKey == null)
			{
				throw new ArgumentException(Strings.DbDependencyResolver_InvalidKey(typeof(ExecutionStrategyKey).Name, "Func<TransactionHandler>"));
			}
			if (this._providerInvariantName != null && !executionStrategyKey.ProviderInvariantName.Equals(this._providerInvariantName, StringComparison.Ordinal))
			{
				return null;
			}
			if (this._serverName != null && !this._serverName.Equals(executionStrategyKey.ServerName, StringComparison.Ordinal))
			{
				return null;
			}
			return this._transactionHandlerFactory;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00036D38 File Offset: 0x00034F38
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00036D44 File Offset: 0x00034F44
		public override bool Equals(object obj)
		{
			TransactionHandlerResolver transactionHandlerResolver = obj as TransactionHandlerResolver;
			return transactionHandlerResolver != null && (this._transactionHandlerFactory == transactionHandlerResolver._transactionHandlerFactory && this._providerInvariantName == transactionHandlerResolver._providerInvariantName) && this._serverName == transactionHandlerResolver._serverName;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00036D98 File Offset: 0x00034F98
		public override int GetHashCode()
		{
			int num = this._transactionHandlerFactory.GetHashCode();
			if (this._providerInvariantName != null)
			{
				num = num * 41 + this._providerInvariantName.GetHashCode();
			}
			if (this._serverName != null)
			{
				num = num * 41 + this._serverName.GetHashCode();
			}
			return num;
		}

		// Token: 0x040002DF RID: 735
		private readonly Func<TransactionHandler> _transactionHandlerFactory;

		// Token: 0x040002E0 RID: 736
		private readonly string _providerInvariantName;

		// Token: 0x040002E1 RID: 737
		private readonly string _serverName;
	}
}
