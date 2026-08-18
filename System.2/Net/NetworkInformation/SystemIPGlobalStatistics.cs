using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002FB RID: 763
	internal class SystemIPGlobalStatistics : IPGlobalStatistics
	{
		// Token: 0x06001AF8 RID: 6904 RVA: 0x00081556 File Offset: 0x0007F756
		private SystemIPGlobalStatistics()
		{
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00081560 File Offset: 0x0007F760
		internal SystemIPGlobalStatistics(AddressFamily family)
		{
			uint ipStatisticsEx = UnsafeNetInfoNativeMethods.GetIpStatisticsEx(out this.stats, family);
			if (ipStatisticsEx != 0U)
			{
				throw new NetworkInformationException((int)ipStatisticsEx);
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x0008158A File Offset: 0x0007F78A
		public override bool ForwardingEnabled
		{
			get
			{
				return this.stats.forwardingEnabled;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x00081597 File Offset: 0x0007F797
		public override int DefaultTtl
		{
			get
			{
				return (int)this.stats.defaultTtl;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x000815A4 File Offset: 0x0007F7A4
		public override long ReceivedPackets
		{
			get
			{
				return (long)((ulong)this.stats.packetsReceived);
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x000815B2 File Offset: 0x0007F7B2
		public override long ReceivedPacketsWithHeadersErrors
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsWithHeaderErrors);
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x000815C0 File Offset: 0x0007F7C0
		public override long ReceivedPacketsWithAddressErrors
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsWithAddressErrors);
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x000815CE File Offset: 0x0007F7CE
		public override long ReceivedPacketsForwarded
		{
			get
			{
				return (long)((ulong)this.stats.packetsForwarded);
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x000815DC File Offset: 0x0007F7DC
		public override long ReceivedPacketsWithUnknownProtocol
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsWithUnknownProtocols);
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x000815EA File Offset: 0x0007F7EA
		public override long ReceivedPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsDiscarded);
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x000815F8 File Offset: 0x0007F7F8
		public override long ReceivedPacketsDelivered
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsDelivered);
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x00081606 File Offset: 0x0007F806
		public override long OutputPacketRequests
		{
			get
			{
				return (long)((ulong)this.stats.packetOutputRequests);
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x00081614 File Offset: 0x0007F814
		public override long OutputPacketRoutingDiscards
		{
			get
			{
				return (long)((ulong)this.stats.outputPacketRoutingDiscards);
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x00081622 File Offset: 0x0007F822
		public override long OutputPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.stats.outputPacketsDiscarded);
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x00081630 File Offset: 0x0007F830
		public override long OutputPacketsWithNoRoute
		{
			get
			{
				return (long)((ulong)this.stats.outputPacketsWithNoRoute);
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x0008163E File Offset: 0x0007F83E
		public override long PacketReassemblyTimeout
		{
			get
			{
				return (long)((ulong)this.stats.packetReassemblyTimeout);
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x0008164C File Offset: 0x0007F84C
		public override long PacketReassembliesRequired
		{
			get
			{
				return (long)((ulong)this.stats.packetsReassemblyRequired);
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0008165A File Offset: 0x0007F85A
		public override long PacketsReassembled
		{
			get
			{
				return (long)((ulong)this.stats.packetsReassembled);
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x00081668 File Offset: 0x0007F868
		public override long PacketReassemblyFailures
		{
			get
			{
				return (long)((ulong)this.stats.packetsReassemblyFailed);
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x00081676 File Offset: 0x0007F876
		public override long PacketsFragmented
		{
			get
			{
				return (long)((ulong)this.stats.packetsFragmented);
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00081684 File Offset: 0x0007F884
		public override long PacketFragmentFailures
		{
			get
			{
				return (long)((ulong)this.stats.packetsFragmentFailed);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x00081692 File Offset: 0x0007F892
		public override int NumberOfInterfaces
		{
			get
			{
				return (int)this.stats.interfaces;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x0008169F File Offset: 0x0007F89F
		public override int NumberOfIPAddresses
		{
			get
			{
				return (int)this.stats.ipAddresses;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000816AC File Offset: 0x0007F8AC
		public override int NumberOfRoutes
		{
			get
			{
				return (int)this.stats.routes;
			}
		}

		// Token: 0x04001AC9 RID: 6857
		private MibIpStats stats;
	}
}
