using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200016F RID: 367
	public interface IDbCommandInterceptor : IDbInterceptor
	{
		// Token: 0x06000BD1 RID: 3025
		void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext);

		// Token: 0x06000BD2 RID: 3026
		void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext);

		// Token: 0x06000BD3 RID: 3027
		void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext);

		// Token: 0x06000BD4 RID: 3028
		void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext);

		// Token: 0x06000BD5 RID: 3029
		void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext);

		// Token: 0x06000BD6 RID: 3030
		void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext);
	}
}
