using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000285 RID: 645
	public class DefaultManifestTokenResolver : IManifestTokenResolver
	{
		// Token: 0x060016AA RID: 5802 RVA: 0x0006EE04 File Offset: 0x0006D004
		public string ResolveManifestToken(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			DbInterceptionContext interceptionContext = new DbInterceptionContext();
			Tuple<Type, string, string> key = Tuple.Create<Type, string, string>(connection.GetType(), DbInterception.Dispatch.Connection.GetDataSource(connection, interceptionContext), DbInterception.Dispatch.Connection.GetDatabase(connection, interceptionContext));
			return this._cachedTokens.GetOrAdd(key, (Tuple<Type, string, string> k) => DbProviderServices.GetProviderServices(connection).GetProviderManifestTokenChecked(connection));
		}

		// Token: 0x0400080F RID: 2063
		private readonly ConcurrentDictionary<Tuple<Type, string, string>, string> _cachedTokens = new ConcurrentDictionary<Tuple<Type, string, string>, string>();
	}
}
