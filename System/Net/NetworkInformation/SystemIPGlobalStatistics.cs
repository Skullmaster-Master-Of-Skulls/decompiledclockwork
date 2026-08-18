using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000633 RID: 1587
	internal class SystemIPGlobalStatistics : IPGlobalStatistics
	{
		// Token: 0x0600310D RID: 12557 RVA: 0x000D325A File Offset: 0x000D225A
		private SystemIPGlobalStatistics()
		{
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000D3270 File Offset: 0x000D2270
		internal SystemIPGlobalStatistics(AddressFamily family)
		{
			uint num;
			if (!ComNetOS.IsPostWin2K)
			{
				if (family != AddressFamily.InterNetwork)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				num = UnsafeNetInfoNativeMethods.GetIpStatistics(out this.stats);
			}
			else
			{
				num = UnsafeNetInfoNativeMethods.GetIpStatisticsEx(out this.stats, family);
			}
			if (num != 0U)
			{
				throw new NetworkInformationException((int)num);
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x000D32CF File Offset: 0x000D22CF
		public override bool ForwardingEnabled
		{
			get
			{
				return this.stats.forwardingEnabled;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x000D32DC File Offset: 0x000D22DC
		public override int DefaultTtl
		{
			get
			{
				return (int)this.stats.defaultTtl;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06003111 RID: 12561 RVA: 0x000D32E9 File Offset: 0x000D22E9
		public override long ReceivedPackets
		{
			get
			{
				return (long)((ulong)this.stats.packetsReceived);
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x000D32F7 File Offset: 0x000D22F7
		public override long ReceivedPacketsWithHeadersErrors
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsWithHeaderErrors);
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x000D3305 File Offset: 0x000D2305
		public override long ReceivedPacketsWithAddressErrors
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsWithAddressErrors);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x000D3313 File Offset: 0x000D2313
		public override long ReceivedPacketsForwarded
		{
			get
			{
				return (long)((ulong)this.stats.packetsForwarded);
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x000D3321 File Offset: 0x000D2321
		public override long ReceivedPacketsWithUnknownProtocol
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsWithUnknownProtocols);
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x000D332F File Offset: 0x000D232F
		public override long ReceivedPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsDiscarded);
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x000D333D File Offset: 0x000D233D
		public override long ReceivedPacketsDelivered
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsDelivered);
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x000D334B File Offset: 0x000D234B
		public override long OutputPacketRequests
		{
			get
			{
				return (long)((ulong)this.stats.packetOutputRequests);
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x000D3359 File Offset: 0x000D2359
		public override long OutputPacketRoutingDiscards
		{
			get
			{
				return (long)((ulong)this.stats.outputPacketRoutingDiscards);
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x000D3367 File Offset: 0x000D2367
		public override long OutputPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.stats.outputPacketsDiscarded);
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x000D3375 File Offset: 0x000D2375
		public override long OutputPacketsWithNoRoute
		{
			get
			{
				return (long)((ulong)this.stats.outputPacketsWithNoRoute);
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x000D3383 File Offset: 0x000D2383
		public override long PacketReassemblyTimeout
		{
			get
			{
				return (long)((ulong)this.stats.packetReassemblyTimeout);
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x000D3391 File Offset: 0x000D2391
		public override long PacketReassembliesRequired
		{
			get
			{
				return (long)((ulong)this.stats.packetsReassemblyRequired);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x000D339F File Offset: 0x000D239F
		public override long PacketsReassembled
		{
			get
			{
				return (long)((ulong)this.stats.packetsReassembled);
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x000D33AD File Offset: 0x000D23AD
		public override long PacketReassemblyFailures
		{
			get
			{
				return (long)((ulong)this.stats.packetsReassemblyFailed);
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x000D33BB File Offset: 0x000D23BB
		public override long PacketsFragmented
		{
			get
			{
				return (long)((ulong)this.stats.packetsFragmented);
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x000D33C9 File Offset: 0x000D23C9
		public override long PacketFragmentFailures
		{
			get
			{
				return (long)((ulong)this.stats.packetsFragmentFailed);
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x000D33D7 File Offset: 0x000D23D7
		public override int NumberOfInterfaces
		{
			get
			{
				return (int)this.stats.interfaces;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06003123 RID: 12579 RVA: 0x000D33E4 File Offset: 0x000D23E4
		public override int NumberOfIPAddresses
		{
			get
			{
				return (int)this.stats.ipAddresses;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06003124 RID: 12580 RVA: 0x000D33F1 File Offset: 0x000D23F1
		public override int NumberOfRoutes
		{
			get
			{
				return (int)this.stats.routes;
			}
		}

		// Token: 0x04002E68 RID: 11880
		private MibIpStats stats = default(MibIpStats);
	}
}
