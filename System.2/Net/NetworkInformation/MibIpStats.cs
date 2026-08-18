using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C5 RID: 709
	internal struct MibIpStats
	{
		// Token: 0x040019D7 RID: 6615
		internal bool forwardingEnabled;

		// Token: 0x040019D8 RID: 6616
		internal uint defaultTtl;

		// Token: 0x040019D9 RID: 6617
		internal uint packetsReceived;

		// Token: 0x040019DA RID: 6618
		internal uint receivedPacketsWithHeaderErrors;

		// Token: 0x040019DB RID: 6619
		internal uint receivedPacketsWithAddressErrors;

		// Token: 0x040019DC RID: 6620
		internal uint packetsForwarded;

		// Token: 0x040019DD RID: 6621
		internal uint receivedPacketsWithUnknownProtocols;

		// Token: 0x040019DE RID: 6622
		internal uint receivedPacketsDiscarded;

		// Token: 0x040019DF RID: 6623
		internal uint receivedPacketsDelivered;

		// Token: 0x040019E0 RID: 6624
		internal uint packetOutputRequests;

		// Token: 0x040019E1 RID: 6625
		internal uint outputPacketRoutingDiscards;

		// Token: 0x040019E2 RID: 6626
		internal uint outputPacketsDiscarded;

		// Token: 0x040019E3 RID: 6627
		internal uint outputPacketsWithNoRoute;

		// Token: 0x040019E4 RID: 6628
		internal uint packetReassemblyTimeout;

		// Token: 0x040019E5 RID: 6629
		internal uint packetsReassemblyRequired;

		// Token: 0x040019E6 RID: 6630
		internal uint packetsReassembled;

		// Token: 0x040019E7 RID: 6631
		internal uint packetsReassemblyFailed;

		// Token: 0x040019E8 RID: 6632
		internal uint packetsFragmented;

		// Token: 0x040019E9 RID: 6633
		internal uint packetsFragmentFailed;

		// Token: 0x040019EA RID: 6634
		internal uint packetsFragmentCreated;

		// Token: 0x040019EB RID: 6635
		internal uint interfaces;

		// Token: 0x040019EC RID: 6636
		internal uint ipAddresses;

		// Token: 0x040019ED RID: 6637
		internal uint routes;
	}
}
