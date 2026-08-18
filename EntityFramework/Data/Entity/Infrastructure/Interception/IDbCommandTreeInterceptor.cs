using System;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000186 RID: 390
	public interface IDbCommandTreeInterceptor : IDbInterceptor
	{
		// Token: 0x06000D5C RID: 3420
		void TreeCreated(DbCommandTreeInterceptionContext interceptionContext);
	}
}
