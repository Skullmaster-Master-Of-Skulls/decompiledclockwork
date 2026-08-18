using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005D7 RID: 1495
	public abstract class IcmpV4Statistics
	{
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002EF6 RID: 12022
		public abstract long AddressMaskRepliesReceived { get; }

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002EF7 RID: 12023
		public abstract long AddressMaskRepliesSent { get; }

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002EF8 RID: 12024
		public abstract long AddressMaskRequestsReceived { get; }

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002EF9 RID: 12025
		public abstract long AddressMaskRequestsSent { get; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002EFA RID: 12026
		public abstract long DestinationUnreachableMessagesReceived { get; }

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002EFB RID: 12027
		public abstract long DestinationUnreachableMessagesSent { get; }

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002EFC RID: 12028
		public abstract long EchoRepliesReceived { get; }

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002EFD RID: 12029
		public abstract long EchoRepliesSent { get; }

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002EFE RID: 12030
		public abstract long EchoRequestsReceived { get; }

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002EFF RID: 12031
		public abstract long EchoRequestsSent { get; }

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002F00 RID: 12032
		public abstract long ErrorsReceived { get; }

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002F01 RID: 12033
		public abstract long ErrorsSent { get; }

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002F02 RID: 12034
		public abstract long MessagesReceived { get; }

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002F03 RID: 12035
		public abstract long MessagesSent { get; }

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002F04 RID: 12036
		public abstract long ParameterProblemsReceived { get; }

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002F05 RID: 12037
		public abstract long ParameterProblemsSent { get; }

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002F06 RID: 12038
		public abstract long RedirectsReceived { get; }

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002F07 RID: 12039
		public abstract long RedirectsSent { get; }

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002F08 RID: 12040
		public abstract long SourceQuenchesReceived { get; }

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002F09 RID: 12041
		public abstract long SourceQuenchesSent { get; }

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002F0A RID: 12042
		public abstract long TimeExceededMessagesReceived { get; }

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002F0B RID: 12043
		public abstract long TimeExceededMessagesSent { get; }

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002F0C RID: 12044
		public abstract long TimestampRepliesReceived { get; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002F0D RID: 12045
		public abstract long TimestampRepliesSent { get; }

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002F0E RID: 12046
		public abstract long TimestampRequestsReceived { get; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002F0F RID: 12047
		public abstract long TimestampRequestsSent { get; }
	}
}
