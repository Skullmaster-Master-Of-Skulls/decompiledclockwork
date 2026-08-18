using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F4 RID: 756
	internal class SystemIcmpV6Statistics : IcmpV6Statistics
	{
		// Token: 0x06001A89 RID: 6793 RVA: 0x000803B4 File Offset: 0x0007E5B4
		internal SystemIcmpV6Statistics()
		{
			uint icmpStatisticsEx = UnsafeNetInfoNativeMethods.GetIcmpStatisticsEx(out this.stats, AddressFamily.InterNetworkV6);
			if (icmpStatisticsEx != 0U)
			{
				throw new NetworkInformationException((int)icmpStatisticsEx);
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x000803DF File Offset: 0x0007E5DF
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.dwMsgs);
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x000803F2 File Offset: 0x0007E5F2
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.dwMsgs);
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x00080405 File Offset: 0x0007E605
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.dwErrors);
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x00080418 File Offset: 0x0007E618
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.dwErrors);
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x0008042B File Offset: 0x0007E62B
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)1L))]);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x00080442 File Offset: 0x0007E642
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)1L))]);
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x00080459 File Offset: 0x0007E659
		public override long PacketTooBigMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)2L))]);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x00080470 File Offset: 0x0007E670
		public override long PacketTooBigMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)2L))]);
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x00080487 File Offset: 0x0007E687
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)3L))]);
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0008049E File Offset: 0x0007E69E
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)3L))]);
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x000804B5 File Offset: 0x0007E6B5
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)4L))]);
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x000804CC File Offset: 0x0007E6CC
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)4L))]);
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x000804E3 File Offset: 0x0007E6E3
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)128L))]);
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x000804FE File Offset: 0x0007E6FE
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)128L))]);
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x00080519 File Offset: 0x0007E719
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)129L))]);
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x00080534 File Offset: 0x0007E734
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)129L))]);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0008054F File Offset: 0x0007E74F
		public override long MembershipQueriesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)130L))]);
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x0008056A File Offset: 0x0007E76A
		public override long MembershipQueriesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)130L))]);
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x00080585 File Offset: 0x0007E785
		public override long MembershipReportsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)131L))]);
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x000805A0 File Offset: 0x0007E7A0
		public override long MembershipReportsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)131L))]);
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x000805BB File Offset: 0x0007E7BB
		public override long MembershipReductionsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)132L))]);
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x000805D6 File Offset: 0x0007E7D6
		public override long MembershipReductionsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)132L))]);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x000805F1 File Offset: 0x0007E7F1
		public override long RouterAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)134L))]);
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x0008060C File Offset: 0x0007E80C
		public override long RouterAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)134L))]);
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x00080627 File Offset: 0x0007E827
		public override long RouterSolicitsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)133L))]);
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x00080642 File Offset: 0x0007E842
		public override long RouterSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)133L))]);
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x0008065D File Offset: 0x0007E85D
		public override long NeighborAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)136L))]);
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x00080678 File Offset: 0x0007E878
		public override long NeighborAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)136L))]);
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x00080693 File Offset: 0x0007E893
		public override long NeighborSolicitsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)135L))]);
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x000806AE File Offset: 0x0007E8AE
		public override long NeighborSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)135L))]);
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x000806C9 File Offset: 0x0007E8C9
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)137L))]);
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x000806E4 File Offset: 0x0007E8E4
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)137L))]);
			}
		}

		// Token: 0x04001AB0 RID: 6832
		private MibIcmpInfoEx stats;
	}
}
