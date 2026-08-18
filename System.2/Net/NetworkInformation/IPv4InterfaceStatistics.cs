using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002A6 RID: 678
	[__DynamicallyInvokable]
	public abstract class IPv4InterfaceStatistics
	{
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001961 RID: 6497
		[__DynamicallyInvokable]
		public abstract long BytesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001962 RID: 6498
		[__DynamicallyInvokable]
		public abstract long BytesSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001963 RID: 6499
		[__DynamicallyInvokable]
		public abstract long IncomingPacketsDiscarded { [__DynamicallyInvokable] get; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001964 RID: 6500
		[__DynamicallyInvokable]
		public abstract long IncomingPacketsWithErrors { [__DynamicallyInvokable] get; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001965 RID: 6501
		[__DynamicallyInvokable]
		public abstract long IncomingUnknownProtocolPackets { [__DynamicallyInvokable] get; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001966 RID: 6502
		[__DynamicallyInvokable]
		public abstract long NonUnicastPacketsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001967 RID: 6503
		[__DynamicallyInvokable]
		public abstract long NonUnicastPacketsSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001968 RID: 6504
		[__DynamicallyInvokable]
		public abstract long OutgoingPacketsDiscarded { [__DynamicallyInvokable] get; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001969 RID: 6505
		[__DynamicallyInvokable]
		public abstract long OutgoingPacketsWithErrors { [__DynamicallyInvokable] get; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600196A RID: 6506
		[__DynamicallyInvokable]
		public abstract long OutputQueueLength { [__DynamicallyInvokable] get; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600196B RID: 6507
		[__DynamicallyInvokable]
		public abstract long UnicastPacketsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600196C RID: 6508
		[__DynamicallyInvokable]
		public abstract long UnicastPacketsSent { [__DynamicallyInvokable] get; }

		// Token: 0x0600196D RID: 6509 RVA: 0x0007E042 File Offset: 0x0007C242
		[__DynamicallyInvokable]
		protected IPv4InterfaceStatistics()
		{
		}
	}
}
