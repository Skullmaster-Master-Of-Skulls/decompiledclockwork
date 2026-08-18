using System;
using System.Threading;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000DE RID: 222
	internal class PoolPopulationOption
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x0005E13C File Offset: 0x0005C33C
		public PoolPopulationOption(int requestCount, int targetCount, Semaphore semPoolPopulation, bool ignoreIncrPoolSize, string connectionClass = null)
		{
			this.m_requestCount = requestCount;
			this.m_targetCount = targetCount;
			this.m_semPoolPopulation = semPoolPopulation;
			this.m_ignoreIncrPoolSize = ignoreIncrPoolSize;
			this.m_connectionClass = connectionClass;
		}

		// Token: 0x04000BAC RID: 2988
		public int m_requestCount;

		// Token: 0x04000BAD RID: 2989
		public int m_targetCount;

		// Token: 0x04000BAE RID: 2990
		public Semaphore m_semPoolPopulation;

		// Token: 0x04000BAF RID: 2991
		public string m_connectionClass;

		// Token: 0x04000BB0 RID: 2992
		public bool m_ignoreIncrPoolSize;
	}
}
