using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A04 RID: 2564
	internal static class PeerTransportConstants
	{
		// Token: 0x04003A81 RID: 14977
		public const int ConnectTimeout = 60000;

		// Token: 0x04003A82 RID: 14978
		public const ulong InvalidNodeId = 0UL;

		// Token: 0x04003A83 RID: 14979
		public const int MinNeighbors = 2;

		// Token: 0x04003A84 RID: 14980
		public const int IdealNeighbors = 3;

		// Token: 0x04003A85 RID: 14981
		public const int MaxResolveAddresses = 3;

		// Token: 0x04003A86 RID: 14982
		public const int MaxNeighbors = 7;

		// Token: 0x04003A87 RID: 14983
		public const int MaxReferrals = 10;

		// Token: 0x04003A88 RID: 14984
		public const int MaxReferralCacheSize = 50;

		// Token: 0x04003A89 RID: 14985
		public const int MaintainerInterval = 300000;

		// Token: 0x04003A8A RID: 14986
		public const int MaintainerRetryInterval = 10000;

		// Token: 0x04003A8B RID: 14987
		public const int MaintainerTimeout = 120000;

		// Token: 0x04003A8C RID: 14988
		public const int UnregisterTimeout = 120000;

		// Token: 0x04003A8D RID: 14989
		public const int AckTimeout = 30000;

		// Token: 0x04003A8E RID: 14990
		public const uint AckWindow = 32U;

		// Token: 0x04003A8F RID: 14991
		public const long MinMessageSize = 16384L;

		// Token: 0x04003A90 RID: 14992
		public const int MinPort = 0;

		// Token: 0x04003A91 RID: 14993
		public const int MaxPort = 65535;

		// Token: 0x04003A92 RID: 14994
		public const ulong MaxHopCount = 18446744073709551615UL;

		// Token: 0x04003A93 RID: 14995
		public static TimeSpan ForwardInterval = TimeSpan.FromSeconds(10.0);

		// Token: 0x04003A94 RID: 14996
		public static TimeSpan ForwardTimeout = TimeSpan.FromSeconds(60.0);

		// Token: 0x04003A95 RID: 14997
		public static int MaxOutgoingMessages = 128;

		// Token: 0x04003A96 RID: 14998
		public const int MessageThreshold = 32;
	}
}
