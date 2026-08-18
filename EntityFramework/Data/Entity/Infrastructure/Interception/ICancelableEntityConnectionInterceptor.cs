using System;
using System.Data.Entity.Core.EntityClient;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000185 RID: 389
	internal interface ICancelableEntityConnectionInterceptor : IDbInterceptor
	{
		// Token: 0x06000D5B RID: 3419
		bool ConnectionOpening(EntityConnection connection, DbInterceptionContext interceptionContext);
	}
}
