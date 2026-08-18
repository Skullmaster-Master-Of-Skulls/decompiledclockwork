using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000171 RID: 369
	public interface IDbTransactionInterceptor : IDbInterceptor
	{
		// Token: 0x06000BEF RID: 3055
		void ConnectionGetting(DbTransaction transaction, DbTransactionInterceptionContext<DbConnection> interceptionContext);

		// Token: 0x06000BF0 RID: 3056
		void ConnectionGot(DbTransaction transaction, DbTransactionInterceptionContext<DbConnection> interceptionContext);

		// Token: 0x06000BF1 RID: 3057
		void IsolationLevelGetting(DbTransaction transaction, DbTransactionInterceptionContext<IsolationLevel> interceptionContext);

		// Token: 0x06000BF2 RID: 3058
		void IsolationLevelGot(DbTransaction transaction, DbTransactionInterceptionContext<IsolationLevel> interceptionContext);

		// Token: 0x06000BF3 RID: 3059
		void Committing(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext);

		// Token: 0x06000BF4 RID: 3060
		void Committed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext);

		// Token: 0x06000BF5 RID: 3061
		void Disposing(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext);

		// Token: 0x06000BF6 RID: 3062
		void Disposed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext);

		// Token: 0x06000BF7 RID: 3063
		void RollingBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext);

		// Token: 0x06000BF8 RID: 3064
		void RolledBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext);
	}
}
