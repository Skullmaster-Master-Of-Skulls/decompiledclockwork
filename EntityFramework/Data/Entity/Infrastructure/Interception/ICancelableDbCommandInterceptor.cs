using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000184 RID: 388
	internal interface ICancelableDbCommandInterceptor : IDbInterceptor
	{
		// Token: 0x06000D5A RID: 3418
		bool CommandExecuting(DbCommand command, DbInterceptionContext interceptionContext);
	}
}
