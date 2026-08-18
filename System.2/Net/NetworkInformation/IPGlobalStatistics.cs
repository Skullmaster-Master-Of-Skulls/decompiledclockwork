using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002A2 RID: 674
	[__DynamicallyInvokable]
	public abstract class IPGlobalStatistics
	{
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001930 RID: 6448
		[__DynamicallyInvokable]
		public abstract int DefaultTtl { [__DynamicallyInvokable] get; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001931 RID: 6449
		[__DynamicallyInvokable]
		public abstract bool ForwardingEnabled { [__DynamicallyInvokable] get; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001932 RID: 6450
		[__DynamicallyInvokable]
		public abstract int NumberOfInterfaces { [__DynamicallyInvokable] get; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001933 RID: 6451
		[__DynamicallyInvokable]
		public abstract int NumberOfIPAddresses { [__DynamicallyInvokable] get; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001934 RID: 6452
		[__DynamicallyInvokable]
		public abstract long OutputPacketRequests { [__DynamicallyInvokable] get; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001935 RID: 6453
		[__DynamicallyInvokable]
		public abstract long OutputPacketRoutingDiscards { [__DynamicallyInvokable] get; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001936 RID: 6454
		[__DynamicallyInvokable]
		public abstract long OutputPacketsDiscarded { [__DynamicallyInvokable] get; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001937 RID: 6455
		[__DynamicallyInvokable]
		public abstract long OutputPacketsWithNoRoute { [__DynamicallyInvokable] get; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001938 RID: 6456
		[__DynamicallyInvokable]
		public abstract long PacketFragmentFailures { [__DynamicallyInvokable] get; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001939 RID: 6457
		[__DynamicallyInvokable]
		public abstract long PacketReassembliesRequired { [__DynamicallyInvokable] get; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600193A RID: 6458
		[__DynamicallyInvokable]
		public abstract long PacketReassemblyFailures { [__DynamicallyInvokable] get; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600193B RID: 6459
		[__DynamicallyInvokable]
		public abstract long PacketReassemblyTimeout { [__DynamicallyInvokable] get; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600193C RID: 6460
		[__DynamicallyInvokable]
		public abstract long PacketsFragmented { [__DynamicallyInvokable] get; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600193D RID: 6461
		[__DynamicallyInvokable]
		public abstract long PacketsReassembled { [__DynamicallyInvokable] get; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600193E RID: 6462
		[__DynamicallyInvokable]
		public abstract long ReceivedPackets { [__DynamicallyInvokable] get; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600193F RID: 6463
		[__DynamicallyInvokable]
		public abstract long ReceivedPacketsDelivered { [__DynamicallyInvokable] get; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001940 RID: 6464
		[__DynamicallyInvokable]
		public abstract long ReceivedPacketsDiscarded { [__DynamicallyInvokable] get; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001941 RID: 6465
		[__DynamicallyInvokable]
		public abstract long ReceivedPacketsForwarded { [__DynamicallyInvokable] get; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001942 RID: 6466
		[__DynamicallyInvokable]
		public abstract long ReceivedPacketsWithAddressErrors { [__DynamicallyInvokable] get; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001943 RID: 6467
		[__DynamicallyInvokable]
		public abstract long ReceivedPacketsWithHeadersErrors { [__DynamicallyInvokable] get; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001944 RID: 6468
		[__DynamicallyInvokable]
		public abstract long ReceivedPacketsWithUnknownProtocol { [__DynamicallyInvokable] get; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001945 RID: 6469
		[__DynamicallyInvokable]
		public abstract int NumberOfRoutes { [__DynamicallyInvokable] get; }

		// Token: 0x06001946 RID: 6470 RVA: 0x0007E02A File Offset: 0x0007C22A
		[__DynamicallyInvokable]
		protected IPGlobalStatistics()
		{
		}
	}
}
