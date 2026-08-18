using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002FA RID: 762
	internal class SystemIPv4InterfaceStatistics : IPv4InterfaceStatistics
	{
		// Token: 0x06001AEB RID: 6891 RVA: 0x000814A6 File Offset: 0x0007F6A6
		internal SystemIPv4InterfaceStatistics(long index)
		{
			this.ifRow = SystemIPInterfaceStatistics.GetIfEntry2(index);
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x000814BA File Offset: 0x0007F6BA
		public override long OutputQueueLength
		{
			get
			{
				return (long)this.ifRow.outQLen;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x000814C7 File Offset: 0x0007F6C7
		public override long BytesSent
		{
			get
			{
				return (long)this.ifRow.outOctets;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x000814D4 File Offset: 0x0007F6D4
		public override long BytesReceived
		{
			get
			{
				return (long)this.ifRow.inOctets;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000814E1 File Offset: 0x0007F6E1
		public override long UnicastPacketsSent
		{
			get
			{
				return (long)this.ifRow.outUcastPkts;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x000814EE File Offset: 0x0007F6EE
		public override long UnicastPacketsReceived
		{
			get
			{
				return (long)this.ifRow.inUcastPkts;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000814FB File Offset: 0x0007F6FB
		public override long NonUnicastPacketsSent
		{
			get
			{
				return (long)this.ifRow.outNUcastPkts;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x00081508 File Offset: 0x0007F708
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return (long)this.ifRow.inNUcastPkts;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x00081515 File Offset: 0x0007F715
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return (long)this.ifRow.inDiscards;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x00081522 File Offset: 0x0007F722
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return (long)this.ifRow.outDiscards;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x0008152F File Offset: 0x0007F72F
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return (long)this.ifRow.inErrors;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x0008153C File Offset: 0x0007F73C
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return (long)this.ifRow.outErrors;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00081549 File Offset: 0x0007F749
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return (long)this.ifRow.inUnknownProtos;
			}
		}

		// Token: 0x04001AC8 RID: 6856
		private MibIfRow2 ifRow;
	}
}
