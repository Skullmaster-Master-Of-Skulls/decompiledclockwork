using System;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x02000267 RID: 615
	internal abstract class DbConnectionBusy : DbConnectionClosed
	{
		// Token: 0x060020F1 RID: 8433 RVA: 0x00282B08 File Offset: 0x00281F08
		protected DbConnectionBusy(ConnectionState state) : base(state, true, false)
		{
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00282B28 File Offset: 0x00281F28
		internal override void OpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory)
		{
			throw ADP.ConnectionAlreadyOpen(base.State);
		}
	}
}
