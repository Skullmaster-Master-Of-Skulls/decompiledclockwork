using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000269 RID: 617
	internal sealed class DbConnectionOpenBusy : DbConnectionBusy
	{
		// Token: 0x060020F5 RID: 8437 RVA: 0x00282B88 File Offset: 0x00281F88
		private DbConnectionOpenBusy() : base(ConnectionState.Open)
		{
		}

		// Token: 0x04001556 RID: 5462
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionOpenBusy();
	}
}
