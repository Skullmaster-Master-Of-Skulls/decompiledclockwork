using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000302 RID: 770
	internal class SystemTcpStatistics : TcpStatistics
	{
		// Token: 0x06001B4F RID: 6991 RVA: 0x00081F07 File Offset: 0x00080107
		private SystemTcpStatistics()
		{
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00081F10 File Offset: 0x00080110
		internal SystemTcpStatistics(AddressFamily family)
		{
			uint tcpStatisticsEx = UnsafeNetInfoNativeMethods.GetTcpStatisticsEx(out this.stats, family);
			if (tcpStatisticsEx != 0U)
			{
				throw new NetworkInformationException((int)tcpStatisticsEx);
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00081F3A File Offset: 0x0008013A
		public override long MinimumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.stats.minimumRetransmissionTimeOut);
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x00081F48 File Offset: 0x00080148
		public override long MaximumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.stats.maximumRetransmissionTimeOut);
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x00081F56 File Offset: 0x00080156
		public override long MaximumConnections
		{
			get
			{
				return (long)((ulong)this.stats.maximumConnections);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x00081F64 File Offset: 0x00080164
		public override long ConnectionsInitiated
		{
			get
			{
				return (long)((ulong)this.stats.activeOpens);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x00081F72 File Offset: 0x00080172
		public override long ConnectionsAccepted
		{
			get
			{
				return (long)((ulong)this.stats.passiveOpens);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x00081F80 File Offset: 0x00080180
		public override long FailedConnectionAttempts
		{
			get
			{
				return (long)((ulong)this.stats.failedConnectionAttempts);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x00081F8E File Offset: 0x0008018E
		public override long ResetConnections
		{
			get
			{
				return (long)((ulong)this.stats.resetConnections);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x00081F9C File Offset: 0x0008019C
		public override long CurrentConnections
		{
			get
			{
				return (long)((ulong)this.stats.currentConnections);
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x00081FAA File Offset: 0x000801AA
		public override long SegmentsReceived
		{
			get
			{
				return (long)((ulong)this.stats.segmentsReceived);
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x00081FB8 File Offset: 0x000801B8
		public override long SegmentsSent
		{
			get
			{
				return (long)((ulong)this.stats.segmentsSent);
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x00081FC6 File Offset: 0x000801C6
		public override long SegmentsResent
		{
			get
			{
				return (long)((ulong)this.stats.segmentsResent);
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x00081FD4 File Offset: 0x000801D4
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.stats.errorsReceived);
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x00081FE2 File Offset: 0x000801E2
		public override long ResetsSent
		{
			get
			{
				return (long)((ulong)this.stats.segmentsSentWithReset);
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x00081FF0 File Offset: 0x000801F0
		public override long CumulativeConnections
		{
			get
			{
				return (long)((ulong)this.stats.cumulativeConnections);
			}
		}

		// Token: 0x04001AED RID: 6893
		private MibTcpStats stats;
	}
}
