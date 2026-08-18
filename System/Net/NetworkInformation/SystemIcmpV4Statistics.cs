using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200062B RID: 1579
	internal class SystemIcmpV4Statistics : IcmpV4Statistics
	{
		// Token: 0x06003088 RID: 12424 RVA: 0x000D1BDC File Offset: 0x000D0BDC
		internal SystemIcmpV4Statistics()
		{
			uint icmpStatistics = UnsafeNetInfoNativeMethods.GetIcmpStatistics(out this.stats);
			if (icmpStatistics != 0U)
			{
				throw new NetworkInformationException((int)icmpStatistics);
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x000D1C05 File Offset: 0x000D0C05
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.messages);
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x000D1C18 File Offset: 0x000D0C18
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.messages);
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x000D1C2B File Offset: 0x000D0C2B
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.errors);
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x000D1C3E File Offset: 0x000D0C3E
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.errors);
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x000D1C51 File Offset: 0x000D0C51
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.destinationUnreachables);
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x000D1C64 File Offset: 0x000D0C64
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.destinationUnreachables);
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x000D1C77 File Offset: 0x000D0C77
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.timeExceeds);
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x000D1C8A File Offset: 0x000D0C8A
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.timeExceeds);
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x000D1C9D File Offset: 0x000D0C9D
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.parameterProblems);
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x000D1CB0 File Offset: 0x000D0CB0
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.parameterProblems);
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x000D1CC3 File Offset: 0x000D0CC3
		public override long SourceQuenchesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.sourceQuenches);
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x000D1CD6 File Offset: 0x000D0CD6
		public override long SourceQuenchesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.sourceQuenches);
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000D1CE9 File Offset: 0x000D0CE9
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.redirects);
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000D1CFC File Offset: 0x000D0CFC
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.redirects);
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000D1D0F File Offset: 0x000D0D0F
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.echoRequests);
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x000D1D22 File Offset: 0x000D0D22
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.echoRequests);
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x000D1D35 File Offset: 0x000D0D35
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.echoReplies);
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x000D1D48 File Offset: 0x000D0D48
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.echoReplies);
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x000D1D5B File Offset: 0x000D0D5B
		public override long TimestampRequestsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.timestampRequests);
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x000D1D6E File Offset: 0x000D0D6E
		public override long TimestampRequestsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.timestampRequests);
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x000D1D81 File Offset: 0x000D0D81
		public override long TimestampRepliesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.timestampReplies);
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000D1D94 File Offset: 0x000D0D94
		public override long TimestampRepliesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.timestampReplies);
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x000D1DA7 File Offset: 0x000D0DA7
		public override long AddressMaskRequestsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.addressMaskRequests);
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000D1DBA File Offset: 0x000D0DBA
		public override long AddressMaskRequestsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.addressMaskRequests);
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x000D1DCD File Offset: 0x000D0DCD
		public override long AddressMaskRepliesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.addressMaskReplies);
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x000D1DE0 File Offset: 0x000D0DE0
		public override long AddressMaskRepliesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.addressMaskReplies);
			}
		}

		// Token: 0x04002E3D RID: 11837
		private MibIcmpInfo stats;
	}
}
