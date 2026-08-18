using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000600 RID: 1536
	internal struct MibTcpStats
	{
		// Token: 0x04002D6A RID: 11626
		internal uint reTransmissionAlgorithm;

		// Token: 0x04002D6B RID: 11627
		internal uint minimumRetransmissionTimeOut;

		// Token: 0x04002D6C RID: 11628
		internal uint maximumRetransmissionTimeOut;

		// Token: 0x04002D6D RID: 11629
		internal uint maximumConnections;

		// Token: 0x04002D6E RID: 11630
		internal uint activeOpens;

		// Token: 0x04002D6F RID: 11631
		internal uint passiveOpens;

		// Token: 0x04002D70 RID: 11632
		internal uint failedConnectionAttempts;

		// Token: 0x04002D71 RID: 11633
		internal uint resetConnections;

		// Token: 0x04002D72 RID: 11634
		internal uint currentConnections;

		// Token: 0x04002D73 RID: 11635
		internal uint segmentsReceived;

		// Token: 0x04002D74 RID: 11636
		internal uint segmentsSent;

		// Token: 0x04002D75 RID: 11637
		internal uint segmentsResent;

		// Token: 0x04002D76 RID: 11638
		internal uint errorsReceived;

		// Token: 0x04002D77 RID: 11639
		internal uint segmentsSentWithReset;

		// Token: 0x04002D78 RID: 11640
		internal uint cumulativeConnections;
	}
}
