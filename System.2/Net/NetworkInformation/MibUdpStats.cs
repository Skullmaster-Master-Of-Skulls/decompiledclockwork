using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C3 RID: 707
	internal struct MibUdpStats
	{
		// Token: 0x040019C3 RID: 6595
		internal uint datagramsReceived;

		// Token: 0x040019C4 RID: 6596
		internal uint incomingDatagramsDiscarded;

		// Token: 0x040019C5 RID: 6597
		internal uint incomingDatagramsWithErrors;

		// Token: 0x040019C6 RID: 6598
		internal uint datagramsSent;

		// Token: 0x040019C7 RID: 6599
		internal uint udpListeners;
	}
}
