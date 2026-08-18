using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C4 RID: 708
	internal struct MibTcpStats
	{
		// Token: 0x040019C8 RID: 6600
		internal uint reTransmissionAlgorithm;

		// Token: 0x040019C9 RID: 6601
		internal uint minimumRetransmissionTimeOut;

		// Token: 0x040019CA RID: 6602
		internal uint maximumRetransmissionTimeOut;

		// Token: 0x040019CB RID: 6603
		internal uint maximumConnections;

		// Token: 0x040019CC RID: 6604
		internal uint activeOpens;

		// Token: 0x040019CD RID: 6605
		internal uint passiveOpens;

		// Token: 0x040019CE RID: 6606
		internal uint failedConnectionAttempts;

		// Token: 0x040019CF RID: 6607
		internal uint resetConnections;

		// Token: 0x040019D0 RID: 6608
		internal uint currentConnections;

		// Token: 0x040019D1 RID: 6609
		internal uint segmentsReceived;

		// Token: 0x040019D2 RID: 6610
		internal uint segmentsSent;

		// Token: 0x040019D3 RID: 6611
		internal uint segmentsResent;

		// Token: 0x040019D4 RID: 6612
		internal uint errorsReceived;

		// Token: 0x040019D5 RID: 6613
		internal uint segmentsSentWithReset;

		// Token: 0x040019D6 RID: 6614
		internal uint cumulativeConnections;
	}
}
