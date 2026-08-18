using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000601 RID: 1537
	internal struct MibIpStats
	{
		// Token: 0x04002D79 RID: 11641
		internal bool forwardingEnabled;

		// Token: 0x04002D7A RID: 11642
		internal uint defaultTtl;

		// Token: 0x04002D7B RID: 11643
		internal uint packetsReceived;

		// Token: 0x04002D7C RID: 11644
		internal uint receivedPacketsWithHeaderErrors;

		// Token: 0x04002D7D RID: 11645
		internal uint receivedPacketsWithAddressErrors;

		// Token: 0x04002D7E RID: 11646
		internal uint packetsForwarded;

		// Token: 0x04002D7F RID: 11647
		internal uint receivedPacketsWithUnknownProtocols;

		// Token: 0x04002D80 RID: 11648
		internal uint receivedPacketsDiscarded;

		// Token: 0x04002D81 RID: 11649
		internal uint receivedPacketsDelivered;

		// Token: 0x04002D82 RID: 11650
		internal uint packetOutputRequests;

		// Token: 0x04002D83 RID: 11651
		internal uint outputPacketRoutingDiscards;

		// Token: 0x04002D84 RID: 11652
		internal uint outputPacketsDiscarded;

		// Token: 0x04002D85 RID: 11653
		internal uint outputPacketsWithNoRoute;

		// Token: 0x04002D86 RID: 11654
		internal uint packetReassemblyTimeout;

		// Token: 0x04002D87 RID: 11655
		internal uint packetsReassemblyRequired;

		// Token: 0x04002D88 RID: 11656
		internal uint packetsReassembled;

		// Token: 0x04002D89 RID: 11657
		internal uint packetsReassemblyFailed;

		// Token: 0x04002D8A RID: 11658
		internal uint packetsFragmented;

		// Token: 0x04002D8B RID: 11659
		internal uint packetsFragmentFailed;

		// Token: 0x04002D8C RID: 11660
		internal uint packetsFragmentCreated;

		// Token: 0x04002D8D RID: 11661
		internal uint interfaces;

		// Token: 0x04002D8E RID: 11662
		internal uint ipAddresses;

		// Token: 0x04002D8F RID: 11663
		internal uint routes;
	}
}
