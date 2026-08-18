using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000170 RID: 368
	public interface IDbConnectionInterceptor : IDbInterceptor
	{
		// Token: 0x06000BD7 RID: 3031
		void BeginningTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext);

		// Token: 0x06000BD8 RID: 3032
		void BeganTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext);

		// Token: 0x06000BD9 RID: 3033
		void Closing(DbConnection connection, DbConnectionInterceptionContext interceptionContext);

		// Token: 0x06000BDA RID: 3034
		void Closed(DbConnection connection, DbConnectionInterceptionContext interceptionContext);

		// Token: 0x06000BDB RID: 3035
		void ConnectionStringGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x06000BDC RID: 3036
		void ConnectionStringGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x06000BDD RID: 3037
		void ConnectionStringSetting(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext);

		// Token: 0x06000BDE RID: 3038
		void ConnectionStringSet(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext);

		// Token: 0x06000BDF RID: 3039
		void ConnectionTimeoutGetting(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext);

		// Token: 0x06000BE0 RID: 3040
		void ConnectionTimeoutGot(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext);

		// Token: 0x06000BE1 RID: 3041
		void DatabaseGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x06000BE2 RID: 3042
		void DatabaseGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x06000BE3 RID: 3043
		void DataSourceGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x06000BE4 RID: 3044
		void DataSourceGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x06000BE5 RID: 3045
		void Disposing(DbConnection connection, DbConnectionInterceptionContext interceptionContext);

		// Token: 0x06000BE6 RID: 3046
		void Disposed(DbConnection connection, DbConnectionInterceptionContext interceptionContext);

		// Token: 0x06000BE7 RID: 3047
		void EnlistingTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext);

		// Token: 0x06000BE8 RID: 3048
		void EnlistedTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext);

		// Token: 0x06000BE9 RID: 3049
		void Opening(DbConnection connection, DbConnectionInterceptionContext interceptionContext);

		// Token: 0x06000BEA RID: 3050
		void Opened(DbConnection connection, DbConnectionInterceptionContext interceptionContext);

		// Token: 0x06000BEB RID: 3051
		void ServerVersionGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x06000BEC RID: 3052
		void ServerVersionGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x06000BED RID: 3053
		void StateGetting(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext);

		// Token: 0x06000BEE RID: 3054
		void StateGot(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext);
	}
}
