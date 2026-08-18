using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200062D RID: 1581
	internal class SystemIcmpV6Statistics : IcmpV6Statistics
	{
		// Token: 0x060030A3 RID: 12451 RVA: 0x000D1DF4 File Offset: 0x000D0DF4
		internal SystemIcmpV6Statistics()
		{
			if (!ComNetOS.IsPostWin2K)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
			}
			uint icmpStatisticsEx = UnsafeNetInfoNativeMethods.GetIcmpStatisticsEx(out this.stats, AddressFamily.InterNetworkV6);
			if (icmpStatisticsEx != 0U)
			{
				throw new NetworkInformationException((int)icmpStatisticsEx);
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x000D1E36 File Offset: 0x000D0E36
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.dwMsgs);
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x000D1E49 File Offset: 0x000D0E49
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.dwMsgs);
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x000D1E5C File Offset: 0x000D0E5C
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.dwErrors);
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x000D1E6F File Offset: 0x000D0E6F
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.dwErrors);
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x000D1E82 File Offset: 0x000D0E82
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)1L))]);
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x000D1E99 File Offset: 0x000D0E99
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)1L))]);
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x000D1EB0 File Offset: 0x000D0EB0
		public override long PacketTooBigMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)2L))]);
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060030AB RID: 12459 RVA: 0x000D1EC7 File Offset: 0x000D0EC7
		public override long PacketTooBigMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)2L))]);
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x000D1EDE File Offset: 0x000D0EDE
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)3L))]);
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x000D1EF5 File Offset: 0x000D0EF5
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)3L))]);
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x000D1F0C File Offset: 0x000D0F0C
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)4L))]);
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x000D1F23 File Offset: 0x000D0F23
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)4L))]);
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x000D1F3A File Offset: 0x000D0F3A
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)128L))]);
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x000D1F55 File Offset: 0x000D0F55
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)128L))]);
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000D1F70 File Offset: 0x000D0F70
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)129L))]);
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060030B3 RID: 12467 RVA: 0x000D1F8B File Offset: 0x000D0F8B
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)129L))]);
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x000D1FA6 File Offset: 0x000D0FA6
		public override long MembershipQueriesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)130L))]);
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000D1FC1 File Offset: 0x000D0FC1
		public override long MembershipQueriesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)130L))]);
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x000D1FDC File Offset: 0x000D0FDC
		public override long MembershipReportsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)131L))]);
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x000D1FF7 File Offset: 0x000D0FF7
		public override long MembershipReportsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)131L))]);
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000D2012 File Offset: 0x000D1012
		public override long MembershipReductionsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)132L))]);
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x000D202D File Offset: 0x000D102D
		public override long MembershipReductionsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)132L))]);
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x000D2048 File Offset: 0x000D1048
		public override long RouterAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)134L))]);
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x000D2063 File Offset: 0x000D1063
		public override long RouterAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)134L))]);
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x000D207E File Offset: 0x000D107E
		public override long RouterSolicitsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)133L))]);
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x000D2099 File Offset: 0x000D1099
		public override long RouterSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)133L))]);
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x000D20B4 File Offset: 0x000D10B4
		public override long NeighborAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)136L))]);
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x000D20CF File Offset: 0x000D10CF
		public override long NeighborAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)136L))]);
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x000D20EA File Offset: 0x000D10EA
		public override long NeighborSolicitsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)135L))]);
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x000D2105 File Offset: 0x000D1105
		public override long NeighborSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)135L))]);
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x000D2120 File Offset: 0x000D1120
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)137L))]);
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x000D213B File Offset: 0x000D113B
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)137L))]);
			}
		}

		// Token: 0x04002E4D RID: 11853
		private MibIcmpInfoEx stats;
	}
}
