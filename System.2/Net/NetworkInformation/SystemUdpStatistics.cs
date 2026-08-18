using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000303 RID: 771
	internal class SystemUdpStatistics : UdpStatistics
	{
		// Token: 0x06001B5F RID: 7007 RVA: 0x00081FFE File Offset: 0x000801FE
		private SystemUdpStatistics()
		{
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00082008 File Offset: 0x00080208
		internal SystemUdpStatistics(AddressFamily family)
		{
			uint udpStatisticsEx = UnsafeNetInfoNativeMethods.GetUdpStatisticsEx(out this.stats, family);
			if (udpStatisticsEx != 0U)
			{
				throw new NetworkInformationException((int)udpStatisticsEx);
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x00082032 File Offset: 0x00080232
		public override long DatagramsReceived
		{
			get
			{
				return (long)((ulong)this.stats.datagramsReceived);
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x00082040 File Offset: 0x00080240
		public override long IncomingDatagramsDiscarded
		{
			get
			{
				return (long)((ulong)this.stats.incomingDatagramsDiscarded);
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0008204E File Offset: 0x0008024E
		public override long IncomingDatagramsWithErrors
		{
			get
			{
				return (long)((ulong)this.stats.incomingDatagramsWithErrors);
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0008205C File Offset: 0x0008025C
		public override long DatagramsSent
		{
			get
			{
				return (long)((ulong)this.stats.datagramsSent);
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x0008206A File Offset: 0x0008026A
		public override int UdpListeners
		{
			get
			{
				return (int)this.stats.udpListeners;
			}
		}

		// Token: 0x04001AEE RID: 6894
		private MibUdpStats stats;
	}
}
