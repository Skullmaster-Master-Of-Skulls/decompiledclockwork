using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F9 RID: 761
	internal class SystemIPInterfaceStatistics : IPInterfaceStatistics
	{
		// Token: 0x06001ADD RID: 6877 RVA: 0x000813C0 File Offset: 0x0007F5C0
		internal SystemIPInterfaceStatistics(long index)
		{
			this.ifRow = SystemIPInterfaceStatistics.GetIfEntry2(index);
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x000813D4 File Offset: 0x0007F5D4
		public override long OutputQueueLength
		{
			get
			{
				return (long)this.ifRow.outQLen;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x000813E1 File Offset: 0x0007F5E1
		public override long BytesSent
		{
			get
			{
				return (long)this.ifRow.outOctets;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x000813EE File Offset: 0x0007F5EE
		public override long BytesReceived
		{
			get
			{
				return (long)this.ifRow.inOctets;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x000813FB File Offset: 0x0007F5FB
		public override long UnicastPacketsSent
		{
			get
			{
				return (long)this.ifRow.outUcastPkts;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00081408 File Offset: 0x0007F608
		public override long UnicastPacketsReceived
		{
			get
			{
				return (long)this.ifRow.inUcastPkts;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00081415 File Offset: 0x0007F615
		public override long NonUnicastPacketsSent
		{
			get
			{
				return (long)this.ifRow.outNUcastPkts;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x00081422 File Offset: 0x0007F622
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return (long)this.ifRow.inNUcastPkts;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0008142F File Offset: 0x0007F62F
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return (long)this.ifRow.inDiscards;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0008143C File Offset: 0x0007F63C
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return (long)this.ifRow.outDiscards;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x00081449 File Offset: 0x0007F649
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return (long)this.ifRow.inErrors;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00081456 File Offset: 0x0007F656
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return (long)this.ifRow.outErrors;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x00081463 File Offset: 0x0007F663
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return (long)this.ifRow.inUnknownProtos;
			}
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00081470 File Offset: 0x0007F670
		internal static MibIfRow2 GetIfEntry2(long index)
		{
			MibIfRow2 result = default(MibIfRow2);
			if (index == 0L)
			{
				return result;
			}
			result.interfaceIndex = (uint)index;
			uint ifEntry = UnsafeNetInfoNativeMethods.GetIfEntry2(ref result);
			if (ifEntry != 0U)
			{
				throw new NetworkInformationException((int)ifEntry);
			}
			return result;
		}

		// Token: 0x04001AC7 RID: 6855
		private MibIfRow2 ifRow;
	}
}
