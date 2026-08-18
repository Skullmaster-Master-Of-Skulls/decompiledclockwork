using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200063C RID: 1596
	public abstract class TcpStatistics
	{
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600316D RID: 12653
		public abstract long ConnectionsAccepted { get; }

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600316E RID: 12654
		public abstract long ConnectionsInitiated { get; }

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x0600316F RID: 12655
		public abstract long CumulativeConnections { get; }

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06003170 RID: 12656
		public abstract long CurrentConnections { get; }

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06003171 RID: 12657
		public abstract long ErrorsReceived { get; }

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06003172 RID: 12658
		public abstract long FailedConnectionAttempts { get; }

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06003173 RID: 12659
		public abstract long MaximumConnections { get; }

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06003174 RID: 12660
		public abstract long MaximumTransmissionTimeout { get; }

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06003175 RID: 12661
		public abstract long MinimumTransmissionTimeout { get; }

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06003176 RID: 12662
		public abstract long ResetConnections { get; }

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06003177 RID: 12663
		public abstract long SegmentsReceived { get; }

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06003178 RID: 12664
		public abstract long SegmentsResent { get; }

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06003179 RID: 12665
		public abstract long SegmentsSent { get; }

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x0600317A RID: 12666
		public abstract long ResetsSent { get; }
	}
}
