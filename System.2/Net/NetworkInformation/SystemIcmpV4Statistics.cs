using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F2 RID: 754
	internal class SystemIcmpV4Statistics : IcmpV4Statistics
	{
		// Token: 0x06001A6E RID: 6766 RVA: 0x0008019C File Offset: 0x0007E39C
		internal SystemIcmpV4Statistics()
		{
			uint icmpStatistics = UnsafeNetInfoNativeMethods.GetIcmpStatistics(out this.stats);
			if (icmpStatistics != 0U)
			{
				throw new NetworkInformationException((int)icmpStatistics);
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x000801C5 File Offset: 0x0007E3C5
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.messages);
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x000801D8 File Offset: 0x0007E3D8
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.messages);
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x000801EB File Offset: 0x0007E3EB
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.errors);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x000801FE File Offset: 0x0007E3FE
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.errors);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x00080211 File Offset: 0x0007E411
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.destinationUnreachables);
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x00080224 File Offset: 0x0007E424
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.destinationUnreachables);
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x00080237 File Offset: 0x0007E437
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.timeExceeds);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0008024A File Offset: 0x0007E44A
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.timeExceeds);
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x0008025D File Offset: 0x0007E45D
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.parameterProblems);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x00080270 File Offset: 0x0007E470
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.parameterProblems);
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x00080283 File Offset: 0x0007E483
		public override long SourceQuenchesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.sourceQuenches);
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x00080296 File Offset: 0x0007E496
		public override long SourceQuenchesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.sourceQuenches);
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x000802A9 File Offset: 0x0007E4A9
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.redirects);
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x000802BC File Offset: 0x0007E4BC
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.redirects);
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x000802CF File Offset: 0x0007E4CF
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.echoRequests);
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x000802E2 File Offset: 0x0007E4E2
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.echoRequests);
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x000802F5 File Offset: 0x0007E4F5
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.echoReplies);
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00080308 File Offset: 0x0007E508
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.echoReplies);
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0008031B File Offset: 0x0007E51B
		public override long TimestampRequestsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.timestampRequests);
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x0008032E File Offset: 0x0007E52E
		public override long TimestampRequestsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.timestampRequests);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x00080341 File Offset: 0x0007E541
		public override long TimestampRepliesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.timestampReplies);
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x00080354 File Offset: 0x0007E554
		public override long TimestampRepliesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.timestampReplies);
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x00080367 File Offset: 0x0007E567
		public override long AddressMaskRequestsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.addressMaskRequests);
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x0008037A File Offset: 0x0007E57A
		public override long AddressMaskRequestsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.addressMaskRequests);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0008038D File Offset: 0x0007E58D
		public override long AddressMaskRepliesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.addressMaskReplies);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x000803A0 File Offset: 0x0007E5A0
		public override long AddressMaskRepliesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.addressMaskReplies);
			}
		}

		// Token: 0x04001AA0 RID: 6816
		private MibIcmpInfo stats;
	}
}
