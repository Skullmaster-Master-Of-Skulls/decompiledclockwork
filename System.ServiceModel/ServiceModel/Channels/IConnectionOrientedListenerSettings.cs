using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200077A RID: 1914
	internal interface IConnectionOrientedListenerSettings : IConnectionOrientedConnectionSettings
	{
		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06004916 RID: 18710
		TimeSpan ChannelInitializationTimeout { get; }

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06004917 RID: 18711
		int MaxPendingConnections { get; }

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06004918 RID: 18712
		int MaxPendingAccepts { get; }

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06004919 RID: 18713
		int MaxPooledConnections { get; }
	}
}
