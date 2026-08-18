using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020002C5 RID: 709
	internal sealed class DbConnectionPoolCountersNoCounters : DbConnectionPoolCounters
	{
		// Token: 0x06002AF7 RID: 10999 RVA: 0x0011A770 File Offset: 0x00119B70
		private DbConnectionPoolCountersNoCounters()
		{
		}

		// Token: 0x04001B76 RID: 7030
		public static readonly DbConnectionPoolCountersNoCounters SingletonInstance = new DbConnectionPoolCountersNoCounters();
	}
}
