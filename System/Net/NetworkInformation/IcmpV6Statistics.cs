using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005D8 RID: 1496
	public abstract class IcmpV6Statistics
	{
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002F11 RID: 12049
		public abstract long DestinationUnreachableMessagesReceived { get; }

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002F12 RID: 12050
		public abstract long DestinationUnreachableMessagesSent { get; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002F13 RID: 12051
		public abstract long EchoRepliesReceived { get; }

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002F14 RID: 12052
		public abstract long EchoRepliesSent { get; }

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002F15 RID: 12053
		public abstract long EchoRequestsReceived { get; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002F16 RID: 12054
		public abstract long EchoRequestsSent { get; }

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002F17 RID: 12055
		public abstract long ErrorsReceived { get; }

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002F18 RID: 12056
		public abstract long ErrorsSent { get; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002F19 RID: 12057
		public abstract long MembershipQueriesReceived { get; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002F1A RID: 12058
		public abstract long MembershipQueriesSent { get; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06002F1B RID: 12059
		public abstract long MembershipReductionsReceived { get; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06002F1C RID: 12060
		public abstract long MembershipReductionsSent { get; }

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002F1D RID: 12061
		public abstract long MembershipReportsReceived { get; }

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002F1E RID: 12062
		public abstract long MembershipReportsSent { get; }

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002F1F RID: 12063
		public abstract long MessagesReceived { get; }

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002F20 RID: 12064
		public abstract long MessagesSent { get; }

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002F21 RID: 12065
		public abstract long NeighborAdvertisementsReceived { get; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002F22 RID: 12066
		public abstract long NeighborAdvertisementsSent { get; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002F23 RID: 12067
		public abstract long NeighborSolicitsReceived { get; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002F24 RID: 12068
		public abstract long NeighborSolicitsSent { get; }

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002F25 RID: 12069
		public abstract long PacketTooBigMessagesReceived { get; }

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002F26 RID: 12070
		public abstract long PacketTooBigMessagesSent { get; }

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002F27 RID: 12071
		public abstract long ParameterProblemsReceived { get; }

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002F28 RID: 12072
		public abstract long ParameterProblemsSent { get; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002F29 RID: 12073
		public abstract long RedirectsReceived { get; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002F2A RID: 12074
		public abstract long RedirectsSent { get; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002F2B RID: 12075
		public abstract long RouterAdvertisementsReceived { get; }

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002F2C RID: 12076
		public abstract long RouterAdvertisementsSent { get; }

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002F2D RID: 12077
		public abstract long RouterSolicitsReceived { get; }

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002F2E RID: 12078
		public abstract long RouterSolicitsSent { get; }

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002F2F RID: 12079
		public abstract long TimeExceededMessagesReceived { get; }

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002F30 RID: 12080
		public abstract long TimeExceededMessagesSent { get; }
	}
}
