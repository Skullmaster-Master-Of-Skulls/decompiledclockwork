using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000797 RID: 1943
	internal static class OneWayDefaults
	{
		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x060049B8 RID: 18872 RVA: 0x0010ED1D File Offset: 0x0010CF1D
		internal static TimeSpan IdleTimeout
		{
			get
			{
				return TimeSpanHelper.FromMinutes(2, "00:02:00");
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x060049B9 RID: 18873 RVA: 0x0010ED2A File Offset: 0x0010CF2A
		internal static TimeSpan LeaseTimeout
		{
			get
			{
				return TimeSpanHelper.FromMinutes(10, "00:10:00");
			}
		}

		// Token: 0x04002EAE RID: 11950
		internal const string IdleTimeoutString = "00:02:00";

		// Token: 0x04002EAF RID: 11951
		internal const int MaxOutboundChannelsPerEndpoint = 10;

		// Token: 0x04002EB0 RID: 11952
		internal const string LeaseTimeoutString = "00:10:00";

		// Token: 0x04002EB1 RID: 11953
		internal const int MaxAcceptedChannels = 10;

		// Token: 0x04002EB2 RID: 11954
		internal const bool PacketRoutable = false;
	}
}
