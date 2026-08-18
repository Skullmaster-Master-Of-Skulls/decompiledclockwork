using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.ProviderBase
{
	// Token: 0x020002B9 RID: 697
	internal abstract class DbConnectionBusy : DbConnectionClosed
	{
		// Token: 0x06002A3F RID: 10815 RVA: 0x00116E08 File Offset: 0x00116208
		protected DbConnectionBusy(ConnectionState state) : base(state, true, false)
		{
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x00116E20 File Offset: 0x00116220
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.ConnectionAlreadyOpen(base.State);
		}
	}
}
