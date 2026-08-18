using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002A5 RID: 677
	[__DynamicallyInvokable]
	public abstract class IPInterfaceStatistics
	{
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001954 RID: 6484
		[__DynamicallyInvokable]
		public abstract long BytesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001955 RID: 6485
		[__DynamicallyInvokable]
		public abstract long BytesSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001956 RID: 6486
		[__DynamicallyInvokable]
		public abstract long IncomingPacketsDiscarded { [__DynamicallyInvokable] get; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001957 RID: 6487
		[__DynamicallyInvokable]
		public abstract long IncomingPacketsWithErrors { [__DynamicallyInvokable] get; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001958 RID: 6488
		[__DynamicallyInvokable]
		public abstract long IncomingUnknownProtocolPackets { [__DynamicallyInvokable] get; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001959 RID: 6489
		[__DynamicallyInvokable]
		public abstract long NonUnicastPacketsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600195A RID: 6490
		[__DynamicallyInvokable]
		public abstract long NonUnicastPacketsSent { [__DynamicallyInvokable] get; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600195B RID: 6491
		[__DynamicallyInvokable]
		public abstract long OutgoingPacketsDiscarded { [__DynamicallyInvokable] get; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600195C RID: 6492
		[__DynamicallyInvokable]
		public abstract long OutgoingPacketsWithErrors { [__DynamicallyInvokable] get; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600195D RID: 6493
		[__DynamicallyInvokable]
		public abstract long OutputQueueLength { [__DynamicallyInvokable] get; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600195E RID: 6494
		[__DynamicallyInvokable]
		public abstract long UnicastPacketsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600195F RID: 6495
		[__DynamicallyInvokable]
		public abstract long UnicastPacketsSent { [__DynamicallyInvokable] get; }

		// Token: 0x06001960 RID: 6496 RVA: 0x0007E03A File Offset: 0x0007C23A
		[__DynamicallyInvokable]
		protected IPInterfaceStatistics()
		{
		}
	}
}
