using System;
using System.Data.Common;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x0200008C RID: 140
	internal abstract class DbConnectionClosed : DbConnectionInternal
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x00076534 File Offset: 0x00075934
		protected DbConnectionClosed(ConnectionState state, bool hidePassword, bool allowSetConnectionString) : base(state, hidePassword, allowSetConnectionString)
		{
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00076554 File Offset: 0x00075954
		public override string ServerVersion
		{
			get
			{
				throw ADP.ClosedConnectionError();
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00076574 File Offset: 0x00075974
		protected override void Activate(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00076594 File Offset: 0x00075994
		public override DbTransaction BeginTransaction(IsolationLevel il)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000765B4 File Offset: 0x000759B4
		public override void ChangeDatabase(string database)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000765D4 File Offset: 0x000759D4
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000765E4 File Offset: 0x000759E4
		protected override void Deactivate()
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00076604 File Offset: 0x00075A04
		public override void EnlistTransaction(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00076624 File Offset: 0x00075A24
		protected internal override DataTable GetSchema(DbConnectionFactory factory, DbConnectionPoolGroup poolGroup, DbConnection outerConnection, string collectionName, string[] restrictions)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00076644 File Offset: 0x00075A44
		internal override void OpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory)
		{
			if (connectionFactory.SetInnerConnectionFrom(outerConnection, DbConnectionClosedConnecting.SingletonInstance, this))
			{
				DbConnectionInternal dbConnectionInternal = null;
				try
				{
					connectionFactory.PermissionDemand(outerConnection);
					dbConnectionInternal = connectionFactory.GetConnection(outerConnection);
				}
				catch
				{
					connectionFactory.SetInnerConnectionTo(outerConnection, this);
					throw;
				}
				if (dbConnectionInternal == null)
				{
					connectionFactory.SetInnerConnectionTo(outerConnection, this);
					throw ADP.InternalConnectionError(ADP.ConnectionError.GetConnectionReturnsNull);
				}
				connectionFactory.SetInnerConnectionEvent(outerConnection, dbConnectionInternal);
			}
		}
	}
}
