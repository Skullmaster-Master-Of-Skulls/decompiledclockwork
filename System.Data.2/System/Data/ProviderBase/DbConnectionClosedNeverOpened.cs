using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020002BD RID: 701
	internal sealed class DbConnectionClosedNeverOpened : DbConnectionClosed
	{
		// Token: 0x06002A4A RID: 10826 RVA: 0x00116F40 File Offset: 0x00116340
		private DbConnectionClosedNeverOpened() : base(ConnectionState.Closed, false, true)
		{
		}

		// Token: 0x04001B1A RID: 6938
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedNeverOpened();
	}
}
