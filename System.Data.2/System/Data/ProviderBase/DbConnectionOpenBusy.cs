using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020002BB RID: 699
	internal sealed class DbConnectionOpenBusy : DbConnectionBusy
	{
		// Token: 0x06002A43 RID: 10819 RVA: 0x00116E64 File Offset: 0x00116264
		private DbConnectionOpenBusy() : base(ConnectionState.Open)
		{
		}

		// Token: 0x04001B18 RID: 6936
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionOpenBusy();
	}
}
