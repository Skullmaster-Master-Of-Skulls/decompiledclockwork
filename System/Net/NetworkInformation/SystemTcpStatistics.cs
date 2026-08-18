using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200063D RID: 1597
	internal class SystemTcpStatistics : TcpStatistics
	{
		// Token: 0x0600317C RID: 12668 RVA: 0x000D450C File Offset: 0x000D350C
		private SystemTcpStatistics()
		{
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000D4514 File Offset: 0x000D3514
		internal SystemTcpStatistics(AddressFamily family)
		{
			uint num;
			if (!ComNetOS.IsPostWin2K)
			{
				if (family != AddressFamily.InterNetwork)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				num = UnsafeNetInfoNativeMethods.GetTcpStatistics(out this.stats);
			}
			else
			{
				num = UnsafeNetInfoNativeMethods.GetTcpStatisticsEx(out this.stats, family);
			}
			if (num != 0U)
			{
				throw new NetworkInformationException((int)num);
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x000D4567 File Offset: 0x000D3567
		public override long MinimumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.stats.minimumRetransmissionTimeOut);
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000D4575 File Offset: 0x000D3575
		public override long MaximumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.stats.maximumRetransmissionTimeOut);
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x000D4583 File Offset: 0x000D3583
		public override long MaximumConnections
		{
			get
			{
				return (long)((ulong)this.stats.maximumConnections);
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x000D4591 File Offset: 0x000D3591
		public override long ConnectionsInitiated
		{
			get
			{
				return (long)((ulong)this.stats.activeOpens);
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x000D459F File Offset: 0x000D359F
		public override long ConnectionsAccepted
		{
			get
			{
				return (long)((ulong)this.stats.passiveOpens);
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x000D45AD File Offset: 0x000D35AD
		public override long FailedConnectionAttempts
		{
			get
			{
				return (long)((ulong)this.stats.failedConnectionAttempts);
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x000D45BB File Offset: 0x000D35BB
		public override long ResetConnections
		{
			get
			{
				return (long)((ulong)this.stats.resetConnections);
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x000D45C9 File Offset: 0x000D35C9
		public override long CurrentConnections
		{
			get
			{
				return (long)((ulong)this.stats.currentConnections);
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06003186 RID: 12678 RVA: 0x000D45D7 File Offset: 0x000D35D7
		public override long SegmentsReceived
		{
			get
			{
				return (long)((ulong)this.stats.segmentsReceived);
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x000D45E5 File Offset: 0x000D35E5
		public override long SegmentsSent
		{
			get
			{
				return (long)((ulong)this.stats.segmentsSent);
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x000D45F3 File Offset: 0x000D35F3
		public override long SegmentsResent
		{
			get
			{
				return (long)((ulong)this.stats.segmentsResent);
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x000D4601 File Offset: 0x000D3601
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.stats.errorsReceived);
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x0600318A RID: 12682 RVA: 0x000D460F File Offset: 0x000D360F
		public override long ResetsSent
		{
			get
			{
				return (long)((ulong)this.stats.segmentsSentWithReset);
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x000D461D File Offset: 0x000D361D
		public override long CumulativeConnections
		{
			get
			{
				return (long)((ulong)this.stats.cumulativeConnections);
			}
		}

		// Token: 0x04002E8F RID: 11919
		private MibTcpStats stats;
	}
}
