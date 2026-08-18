using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005FF RID: 1535
	internal struct MibUdpStats
	{
		// Token: 0x04002D65 RID: 11621
		internal uint datagramsReceived;

		// Token: 0x04002D66 RID: 11622
		internal uint incomingDatagramsDiscarded;

		// Token: 0x04002D67 RID: 11623
		internal uint incomingDatagramsWithErrors;

		// Token: 0x04002D68 RID: 11624
		internal uint datagramsSent;

		// Token: 0x04002D69 RID: 11625
		internal uint udpListeners;
	}
}
