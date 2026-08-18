using System;
using System.Data.Common;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000283 RID: 643
	internal class DefaultDbProviderFactoryResolver : IDbProviderFactoryResolver
	{
		// Token: 0x060016A7 RID: 5799 RVA: 0x0006EDC8 File Offset: 0x0006CFC8
		public DbProviderFactory ResolveProviderFactory(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			return DbProviderFactories.GetFactory(connection);
		}
	}
}
