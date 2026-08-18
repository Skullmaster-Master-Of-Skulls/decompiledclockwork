using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200063F RID: 1599
	internal class SystemUdpStatistics : UdpStatistics
	{
		// Token: 0x06003192 RID: 12690 RVA: 0x000D4633 File Offset: 0x000D3633
		private SystemUdpStatistics()
		{
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000D463C File Offset: 0x000D363C
		internal SystemUdpStatistics(AddressFamily family)
		{
			uint num;
			if (!ComNetOS.IsPostWin2K)
			{
				if (family != AddressFamily.InterNetwork)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				num = UnsafeNetInfoNativeMethods.GetUdpStatistics(out this.stats);
			}
			else
			{
				num = UnsafeNetInfoNativeMethods.GetUdpStatisticsEx(out this.stats, family);
			}
			if (num != 0U)
			{
				throw new NetworkInformationException((int)num);
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x000D468F File Offset: 0x000D368F
		public override long DatagramsReceived
		{
			get
			{
				return (long)((ulong)this.stats.datagramsReceived);
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x000D469D File Offset: 0x000D369D
		public override long IncomingDatagramsDiscarded
		{
			get
			{
				return (long)((ulong)this.stats.incomingDatagramsDiscarded);
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06003196 RID: 12694 RVA: 0x000D46AB File Offset: 0x000D36AB
		public override long IncomingDatagramsWithErrors
		{
			get
			{
				return (long)((ulong)this.stats.incomingDatagramsWithErrors);
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000D46B9 File Offset: 0x000D36B9
		public override long DatagramsSent
		{
			get
			{
				return (long)((ulong)this.stats.datagramsSent);
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x000D46C7 File Offset: 0x000D36C7
		public override int UdpListeners
		{
			get
			{
				return (int)this.stats.udpListeners;
			}
		}

		// Token: 0x04002E90 RID: 11920
		private MibUdpStats stats;
	}
}
