using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200017B RID: 379
	internal class DbCommandTreeDispatcher
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0003B028 File Offset: 0x00039228
		public InternalDispatcher<IDbCommandTreeInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0003B039 File Offset: 0x00039239
		public virtual DbCommandTree Created(DbCommandTree commandTree, DbInterceptionContext interceptionContext)
		{
			return this._internalDispatcher.Dispatch<DbCommandTreeInterceptionContext, DbCommandTree>(commandTree, new DbCommandTreeInterceptionContext(interceptionContext), delegate(IDbCommandTreeInterceptor i, DbCommandTreeInterceptionContext c)
			{
				i.TreeCreated(c);
			});
		}

		// Token: 0x0400035B RID: 859
		private readonly InternalDispatcher<IDbCommandTreeInterceptor> _internalDispatcher = new InternalDispatcher<IDbCommandTreeInterceptor>();
	}
}
