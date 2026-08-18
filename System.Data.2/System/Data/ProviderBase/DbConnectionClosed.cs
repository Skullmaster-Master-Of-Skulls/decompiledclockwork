using System;
using System.Data.Common;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x020002B8 RID: 696
	internal abstract class DbConnectionClosed : DbConnectionInternal
	{
		// Token: 0x06002A34 RID: 10804 RVA: 0x00116D28 File Offset: 0x00116128
		protected DbConnectionClosed(ConnectionState state, bool hidePassword, bool allowSetConnectionString) : base(state, hidePassword, allowSetConnectionString)
		{
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002A35 RID: 10805 RVA: 0x00116D40 File Offset: 0x00116140
		public override string ServerVersion
		{
			get
			{
				throw ADP.ClosedConnectionError();
			}
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x00116D54 File Offset: 0x00116154
		protected override void Activate(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x00116D68 File Offset: 0x00116168
		public override DbTransaction BeginTransaction(IsolationLevel il)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x00116D7C File Offset: 0x0011617C
		public override void ChangeDatabase(string database)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x00116D90 File Offset: 0x00116190
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x00116DA0 File Offset: 0x001161A0
		protected override void Deactivate()
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x00116DB4 File Offset: 0x001161B4
		public override void EnlistTransaction(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x00116DC8 File Offset: 0x001161C8
		protected internal override DataTable GetSchema(DbConnectionFactory factory, DbConnectionPoolGroup poolGroup, DbConnection outerConnection, string collectionName, string[] restrictions)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x00116DDC File Offset: 0x001161DC
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x00116DF0 File Offset: 0x001161F0
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return base.TryOpenConnectionInternal(outerConnection, connectionFactory, retry, userOptions);
		}
	}
}
