using System;

namespace System.Net
{
	// Token: 0x020001E5 RID: 485
	internal class RequestLifetimeSetter
	{
		// Token: 0x060012E4 RID: 4836 RVA: 0x00063FE2 File Offset: 0x000621E2
		internal RequestLifetimeSetter(long requestStartTimestamp)
		{
			this.m_RequestStartTimestamp = requestStartTimestamp;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00063FF1 File Offset: 0x000621F1
		internal static void Report(RequestLifetimeSetter tracker)
		{
			if (tracker != null)
			{
				NetworkingPerfCounters.Instance.IncrementAverage(NetworkingPerfCounterName.HttpWebRequestAvgLifeTime, tracker.m_RequestStartTimestamp);
			}
		}

		// Token: 0x04001534 RID: 5428
		private long m_RequestStartTimestamp;
	}
}
