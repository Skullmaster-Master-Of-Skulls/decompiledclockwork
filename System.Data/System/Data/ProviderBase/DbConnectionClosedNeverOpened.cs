using System;

namespace System.Data.ProviderBase
{
	// Token: 0x0200026B RID: 619
	internal sealed class DbConnectionClosedNeverOpened : DbConnectionClosed
	{
		// Token: 0x060020F9 RID: 8441 RVA: 0x00282C08 File Offset: 0x00282008
		private DbConnectionClosedNeverOpened() : base(ConnectionState.Closed, false, true)
		{
		}

		// Token: 0x04001558 RID: 5464
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedNeverOpened();
	}
}
