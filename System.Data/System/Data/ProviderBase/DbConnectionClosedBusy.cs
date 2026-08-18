using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000268 RID: 616
	internal sealed class DbConnectionClosedBusy : DbConnectionBusy
	{
		// Token: 0x060020F3 RID: 8435 RVA: 0x00282B48 File Offset: 0x00281F48
		private DbConnectionClosedBusy() : base(ConnectionState.Closed)
		{
		}

		// Token: 0x04001555 RID: 5461
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedBusy();
	}
}
