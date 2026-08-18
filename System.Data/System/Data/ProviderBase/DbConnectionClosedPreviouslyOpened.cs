using System;

namespace System.Data.ProviderBase
{
	// Token: 0x0200026C RID: 620
	internal sealed class DbConnectionClosedPreviouslyOpened : DbConnectionClosed
	{
		// Token: 0x060020FB RID: 8443 RVA: 0x00282C48 File Offset: 0x00282048
		private DbConnectionClosedPreviouslyOpened() : base(ConnectionState.Closed, true, true)
		{
		}

		// Token: 0x04001559 RID: 5465
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedPreviouslyOpened();
	}
}
