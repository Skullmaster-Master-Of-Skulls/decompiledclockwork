using System;
using System.Data.Common;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x02000266 RID: 614
	internal abstract class DbConnectionClosed : DbConnectionInternal
	{
		// Token: 0x060020E7 RID: 8423 RVA: 0x00282978 File Offset: 0x00281D78
		protected DbConnectionClosed(ConnectionState state, bool hidePassword, bool allowSetConnectionString) : base(state, hidePassword, allowSetConnectionString)
		{
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x00282998 File Offset: 0x00281D98
		public override string ServerVersion
		{
			get
			{
				throw ADP.ClosedConnectionError();
			}
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x002829B8 File Offset: 0x00281DB8
		protected override void Activate(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x002829D8 File Offset: 0x00281DD8
		public override DbTransaction BeginTransaction(IsolationLevel il)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x002829F8 File Offset: 0x00281DF8
		public override void ChangeDatabase(string database)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00282A18 File Offset: 0x00281E18
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x00282A28 File Offset: 0x00281E28
		protected override void Deactivate()
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00282A48 File Offset: 0x00281E48
		public override void EnlistTransaction(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x00282A68 File Offset: 0x00281E68
		protected internal override DataTable GetSchema(DbConnectionFactory factory, DbConnectionPoolGroup poolGroup, DbConnection outerConnection, string collectionName, string[] restrictions)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00282A88 File Offset: 0x00281E88
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
