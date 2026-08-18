using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005DD RID: 1501
	public abstract class IPGlobalStatistics
	{
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002F55 RID: 12117
		public abstract int DefaultTtl { get; }

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002F56 RID: 12118
		public abstract bool ForwardingEnabled { get; }

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002F57 RID: 12119
		public abstract int NumberOfInterfaces { get; }

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002F58 RID: 12120
		public abstract int NumberOfIPAddresses { get; }

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002F59 RID: 12121
		public abstract long OutputPacketRequests { get; }

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002F5A RID: 12122
		public abstract long OutputPacketRoutingDiscards { get; }

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002F5B RID: 12123
		public abstract long OutputPacketsDiscarded { get; }

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002F5C RID: 12124
		public abstract long OutputPacketsWithNoRoute { get; }

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002F5D RID: 12125
		public abstract long PacketFragmentFailures { get; }

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002F5E RID: 12126
		public abstract long PacketReassembliesRequired { get; }

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002F5F RID: 12127
		public abstract long PacketReassemblyFailures { get; }

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002F60 RID: 12128
		public abstract long PacketReassemblyTimeout { get; }

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06002F61 RID: 12129
		public abstract long PacketsFragmented { get; }

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06002F62 RID: 12130
		public abstract long PacketsReassembled { get; }

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002F63 RID: 12131
		public abstract long ReceivedPackets { get; }

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002F64 RID: 12132
		public abstract long ReceivedPacketsDelivered { get; }

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002F65 RID: 12133
		public abstract long ReceivedPacketsDiscarded { get; }

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002F66 RID: 12134
		public abstract long ReceivedPacketsForwarded { get; }

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06002F67 RID: 12135
		public abstract long ReceivedPacketsWithAddressErrors { get; }

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002F68 RID: 12136
		public abstract long ReceivedPacketsWithHeadersErrors { get; }

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002F69 RID: 12137
		public abstract long ReceivedPacketsWithUnknownProtocol { get; }

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06002F6A RID: 12138
		public abstract int NumberOfRoutes { get; }
	}
}
