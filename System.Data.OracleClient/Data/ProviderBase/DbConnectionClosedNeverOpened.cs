using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000091 RID: 145
	internal sealed class DbConnectionClosedNeverOpened : DbConnectionClosed
	{
		// Token: 0x060007FA RID: 2042 RVA: 0x000767C4 File Offset: 0x00075BC4
		private DbConnectionClosedNeverOpened() : base(ConnectionState.Closed, false, true)
		{
		}

		// Token: 0x04000511 RID: 1297
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedNeverOpened();
	}
}
