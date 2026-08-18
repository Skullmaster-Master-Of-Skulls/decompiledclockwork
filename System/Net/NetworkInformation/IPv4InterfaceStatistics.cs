using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005DF RID: 1503
	public abstract class IPv4InterfaceStatistics
	{
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002F79 RID: 12153
		public abstract long BytesReceived { get; }

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06002F7A RID: 12154
		public abstract long BytesSent { get; }

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06002F7B RID: 12155
		public abstract long IncomingPacketsDiscarded { get; }

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002F7C RID: 12156
		public abstract long IncomingPacketsWithErrors { get; }

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002F7D RID: 12157
		public abstract long IncomingUnknownProtocolPackets { get; }

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002F7E RID: 12158
		public abstract long NonUnicastPacketsReceived { get; }

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06002F7F RID: 12159
		public abstract long NonUnicastPacketsSent { get; }

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06002F80 RID: 12160
		public abstract long OutgoingPacketsDiscarded { get; }

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06002F81 RID: 12161
		public abstract long OutgoingPacketsWithErrors { get; }

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002F82 RID: 12162
		public abstract long OutputQueueLength { get; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002F83 RID: 12163
		public abstract long UnicastPacketsReceived { get; }

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06002F84 RID: 12164
		public abstract long UnicastPacketsSent { get; }
	}
}
