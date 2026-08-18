using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009EE RID: 2542
	internal enum PeerNeighborState
	{
		// Token: 0x040039E0 RID: 14816
		Created,
		// Token: 0x040039E1 RID: 14817
		Opened,
		// Token: 0x040039E2 RID: 14818
		Authenticated,
		// Token: 0x040039E3 RID: 14819
		Connecting,
		// Token: 0x040039E4 RID: 14820
		Connected,
		// Token: 0x040039E5 RID: 14821
		Disconnecting,
		// Token: 0x040039E6 RID: 14822
		Disconnected,
		// Token: 0x040039E7 RID: 14823
		Faulted,
		// Token: 0x040039E8 RID: 14824
		Closed
	}
}
