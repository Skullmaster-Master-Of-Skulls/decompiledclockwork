using System;

namespace System.Data.ProviderBase
{
	// Token: 0x0200026A RID: 618
	internal sealed class DbConnectionClosedConnecting : DbConnectionBusy
	{
		// Token: 0x060020F7 RID: 8439 RVA: 0x00282BC8 File Offset: 0x00281FC8
		private DbConnectionClosedConnecting() : base(ConnectionState.Connecting)
		{
		}

		// Token: 0x04001557 RID: 5463
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedConnecting();
	}
}
