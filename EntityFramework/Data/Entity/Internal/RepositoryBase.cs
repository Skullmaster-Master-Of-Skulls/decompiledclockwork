using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace System.Data.Entity.Internal
{
	// Token: 0x020006C2 RID: 1730
	internal abstract class RepositoryBase
	{
		// Token: 0x060044BE RID: 17598 RVA: 0x001449B0 File Offset: 0x00142BB0
		protected RepositoryBase(InternalContext usersContext, string connectionString, DbProviderFactory providerFactory)
		{
			DbConnection connection = usersContext.Connection;
			if (connection != null && connection.State == ConnectionState.Open)
			{
				this._existingConnection = connection;
			}
			this._connectionString = connectionString;
			this._providerFactory = providerFactory;
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x001449EC File Offset: 0x00142BEC
		protected DbConnection CreateConnection()
		{
			if (this._existingConnection != null)
			{
				return this._existingConnection;
			}
			DbConnection dbConnection = this._providerFactory.CreateConnection();
			DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(this._connectionString));
			return dbConnection;
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x00144A35 File Offset: 0x00142C35
		protected void DisposeConnection(DbConnection connection)
		{
			if (connection != null && this._existingConnection == null)
			{
				DbInterception.Dispatch.Connection.Dispose(connection, new DbInterceptionContext());
			}
		}

		// Token: 0x04001952 RID: 6482
		private readonly DbConnection _existingConnection;

		// Token: 0x04001953 RID: 6483
		private readonly string _connectionString;

		// Token: 0x04001954 RID: 6484
		private readonly DbProviderFactory _providerFactory;
	}
}
