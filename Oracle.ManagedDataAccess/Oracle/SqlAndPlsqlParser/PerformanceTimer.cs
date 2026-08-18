using System;
using System.Diagnostics;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000284 RID: 644
	internal class PerformanceTimer : IDisposable
	{
		// Token: 0x06001928 RID: 6440 RVA: 0x00108290 File Offset: 0x00106490
		public PerformanceTimer(string opText)
		{
			this.m_vOperationText = opText;
			this.m_vStopWatch = new Stopwatch();
			this.m_vStopWatch.Start();
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x001082B8 File Offset: 0x001064B8
		public void Dispose()
		{
			this.m_vStopWatch.Stop();
			long elapsedMilliseconds = this.m_vStopWatch.ElapsedMilliseconds;
			string.Format("Operation {0} took {1} s. and {2} ms.", this.m_vOperationText, elapsedMilliseconds / 1000L, elapsedMilliseconds % 1000L);
		}

		// Token: 0x04001B79 RID: 7033
		private Stopwatch m_vStopWatch;

		// Token: 0x04001B7A RID: 7034
		private string m_vOperationText;
	}
}
