using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200017A RID: 378
	public class DbCommandInterceptor : IDbCommandInterceptor, IDbInterceptor
	{
		// Token: 0x06000CB2 RID: 3250 RVA: 0x0003B014 File Offset: 0x00039214
		public virtual void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0003B016 File Offset: 0x00039216
		public virtual void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0003B018 File Offset: 0x00039218
		public virtual void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0003B01A File Offset: 0x0003921A
		public virtual void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0003B01C File Offset: 0x0003921C
		public virtual void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0003B01E File Offset: 0x0003921E
		public virtual void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
		}
	}
}
