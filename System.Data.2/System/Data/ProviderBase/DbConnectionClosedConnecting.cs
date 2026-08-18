using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.ProviderBase
{
	// Token: 0x020002BC RID: 700
	internal sealed class DbConnectionClosedConnecting : DbConnectionBusy
	{
		// Token: 0x06002A45 RID: 10821 RVA: 0x00116E90 File Offset: 0x00116290
		private DbConnectionClosedConnecting() : base(ConnectionState.Connecting)
		{
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x00116EA4 File Offset: 0x001162A4
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
			connectionFactory.SetInnerConnectionTo(owningObject, DbConnectionClosedPreviouslyOpened.SingletonInstance);
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x00116EC0 File Offset: 0x001162C0
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return this.TryOpenConnection(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x00116ED8 File Offset: 0x001162D8
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			if (retry == null || !retry.Task.IsCompleted)
			{
				throw ADP.ConnectionAlreadyOpen(base.State);
			}
			DbConnectionInternal result = retry.Task.Result;
			if (result == null)
			{
				connectionFactory.SetInnerConnectionTo(outerConnection, this);
				throw ADP.InternalConnectionError(ADP.ConnectionError.GetConnectionReturnsNull);
			}
			connectionFactory.SetInnerConnectionEvent(outerConnection, result);
			return true;
		}

		// Token: 0x04001B19 RID: 6937
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedConnecting();
	}
}
