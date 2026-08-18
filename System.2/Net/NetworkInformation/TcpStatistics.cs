using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000307 RID: 775
	[__DynamicallyInvokable]
	public abstract class TcpStatistics
	{
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001B70 RID: 7024
		[__DynamicallyInvokable]
		public abstract long ConnectionsAccepted { [__DynamicallyInvokable] get; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001B71 RID: 7025
		[__DynamicallyInvokable]
		public abstract long ConnectionsInitiated { [__DynamicallyInvokable] get; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001B72 RID: 7026
		[__DynamicallyInvokable]
		public abstract long CumulativeConnections { [__DynamicallyInvokable] get; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001B73 RID: 7027
		[__DynamicallyInvokable]
		public abstract long CurrentConnections { [__DynamicallyInvokable] get; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001B74 RID: 7028
		[__DynamicallyInvokable]
		public abstract long ErrorsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001B75 RID: 7029
		[__DynamicallyInvokable]
		public abstract long FailedConnectionAttempts { [__DynamicallyInvokable] get; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001B76 RID: 7030
		[__DynamicallyInvokable]
		public abstract long MaximumConnections { [__DynamicallyInvokable] get; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001B77 RID: 7031
		[__DynamicallyInvokable]
		public abstract long MaximumTransmissionTimeout { [__DynamicallyInvokable] get; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001B78 RID: 7032
		[__DynamicallyInvokable]
		public abstract long MinimumTransmissionTimeout { [__DynamicallyInvokable] get; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001B79 RID: 7033
		[__DynamicallyInvokable]
		public abstract long ResetConnections { [__DynamicallyInvokable] get; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001B7A RID: 7034
		[__DynamicallyInvokable]
		public abstract long SegmentsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001B7B RID: 7035
		[__DynamicallyInvokable]
		public abstract long SegmentsResent { [__DynamicallyInvokable] get; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001B7C RID: 7036
		[__DynamicallyInvokable]
		public abstract long SegmentsSent { [__DynamicallyInvokable] get; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001B7D RID: 7037
		[__DynamicallyInvokable]
		public abstract long ResetsSent { [__DynamicallyInvokable] get; }

		// Token: 0x06001B7E RID: 7038 RVA: 0x000822C4 File Offset: 0x000804C4
		[__DynamicallyInvokable]
		protected TcpStatistics()
		{
		}
	}
}
