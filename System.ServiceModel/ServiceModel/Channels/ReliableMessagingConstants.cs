using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200093E RID: 2366
	internal static class ReliableMessagingConstants
	{
		// Token: 0x040036C5 RID: 14021
		public static TimeSpan UnknownInitiationTime = TimeSpan.FromSeconds(2.0);

		// Token: 0x040036C6 RID: 14022
		public static TimeSpan RequestorIterationTime = TimeSpan.FromSeconds(10.0);

		// Token: 0x040036C7 RID: 14023
		public static TimeSpan RequestorReceiveTime = TimeSpan.FromSeconds(10.0);

		// Token: 0x040036C8 RID: 14024
		public static int MaxSequenceRanges = 128;
	}
}
