using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000092 RID: 146
	internal sealed class DbConnectionClosedPreviouslyOpened : DbConnectionClosed
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x00076804 File Offset: 0x00075C04
		private DbConnectionClosedPreviouslyOpened() : base(ConnectionState.Closed, true, true)
		{
		}

		// Token: 0x04000512 RID: 1298
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedPreviouslyOpened();
	}
}
