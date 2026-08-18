using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000284 RID: 644
	public interface IManifestTokenResolver
	{
		// Token: 0x060016A9 RID: 5801
		string ResolveManifestToken(DbConnection connection);
	}
}
