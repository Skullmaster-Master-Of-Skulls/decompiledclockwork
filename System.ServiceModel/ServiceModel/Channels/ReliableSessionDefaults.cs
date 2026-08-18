using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000798 RID: 1944
	internal static class ReliableSessionDefaults
	{
		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x060049BA RID: 18874 RVA: 0x0010ED38 File Offset: 0x0010CF38
		internal static TimeSpan AcknowledgementInterval
		{
			get
			{
				return TimeSpanHelper.FromMilliseconds(200, "00:00:00.2");
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x0010ED49 File Offset: 0x0010CF49
		internal static TimeSpan InactivityTimeout
		{
			get
			{
				return TimeSpanHelper.FromMinutes(10, "00:10:00");
			}
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x060049BC RID: 18876 RVA: 0x0010ED57 File Offset: 0x0010CF57
		internal static ReliableMessagingVersion ReliableMessagingVersion
		{
			get
			{
				return ReliableMessagingVersion.WSReliableMessagingFebruary2005;
			}
		}

		// Token: 0x04002EB3 RID: 11955
		internal const string AcknowledgementIntervalString = "00:00:00.2";

		// Token: 0x04002EB4 RID: 11956
		internal const bool Enabled = false;

		// Token: 0x04002EB5 RID: 11957
		internal const bool FlowControlEnabled = true;

		// Token: 0x04002EB6 RID: 11958
		internal const string InactivityTimeoutString = "00:10:00";

		// Token: 0x04002EB7 RID: 11959
		internal const int MaxPendingChannels = 4;

		// Token: 0x04002EB8 RID: 11960
		internal const int MaxRetryCount = 8;

		// Token: 0x04002EB9 RID: 11961
		internal const int MaxTransferWindowSize = 8;

		// Token: 0x04002EBA RID: 11962
		internal const bool Ordered = true;

		// Token: 0x04002EBB RID: 11963
		internal const string ReliableMessagingVersionString = "WSReliableMessagingFebruary2005";
	}
}
