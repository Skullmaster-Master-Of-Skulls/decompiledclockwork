using System;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x0200008D RID: 141
	internal abstract class DbConnectionBusy : DbConnectionClosed
	{
		// Token: 0x060007F2 RID: 2034 RVA: 0x000766C4 File Offset: 0x00075AC4
		protected DbConnectionBusy(ConnectionState state) : base(state, true, false)
		{
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000766E4 File Offset: 0x00075AE4
		internal override void OpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory)
		{
			throw ADP.ConnectionAlreadyOpen(base.State);
		}
	}
}
