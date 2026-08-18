using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200027D RID: 637
	public interface IDbProviderFactoryResolver
	{
		// Token: 0x06001665 RID: 5733
		DbProviderFactory ResolveProviderFactory(DbConnection connection);
	}
}
