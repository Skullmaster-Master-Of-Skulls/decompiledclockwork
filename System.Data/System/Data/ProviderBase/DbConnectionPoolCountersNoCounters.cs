using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000276 RID: 630
	internal sealed class DbConnectionPoolCountersNoCounters : DbConnectionPoolCounters
	{
		// Token: 0x06002148 RID: 8520 RVA: 0x00285038 File Offset: 0x00284438
		private DbConnectionPoolCountersNoCounters()
		{
		}

		// Token: 0x040015AB RID: 5547
		public static readonly DbConnectionPoolCountersNoCounters SingletonInstance = new DbConnectionPoolCountersNoCounters();
	}
}
