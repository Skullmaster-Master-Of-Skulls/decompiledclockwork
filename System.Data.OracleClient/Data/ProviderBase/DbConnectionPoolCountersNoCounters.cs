using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000099 RID: 153
	internal sealed class DbConnectionPoolCountersNoCounters : DbConnectionPoolCounters
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x00078504 File Offset: 0x00077904
		private DbConnectionPoolCountersNoCounters()
		{
		}

		// Token: 0x04000547 RID: 1351
		public static readonly DbConnectionPoolCountersNoCounters SingletonInstance = new DbConnectionPoolCountersNoCounters();
	}
}
