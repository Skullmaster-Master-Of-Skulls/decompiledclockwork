using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020002BA RID: 698
	internal sealed class DbConnectionClosedBusy : DbConnectionBusy
	{
		// Token: 0x06002A41 RID: 10817 RVA: 0x00116E38 File Offset: 0x00116238
		private DbConnectionClosedBusy() : base(ConnectionState.Closed)
		{
		}

		// Token: 0x04001B17 RID: 6935
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedBusy();
	}
}
