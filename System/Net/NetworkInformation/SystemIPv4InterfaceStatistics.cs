using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000632 RID: 1586
	internal class SystemIPv4InterfaceStatistics : IPv4InterfaceStatistics
	{
		// Token: 0x060030FB RID: 12539 RVA: 0x000D30E8 File Offset: 0x000D20E8
		private SystemIPv4InterfaceStatistics()
		{
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000D30FC File Offset: 0x000D20FC
		internal SystemIPv4InterfaceStatistics(long index)
		{
			this.GetIfEntry(index);
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x000D3117 File Offset: 0x000D2117
		public override long OutputQueueLength
		{
			get
			{
				return (long)((ulong)this.ifRow.dwOutQLen);
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000D3125 File Offset: 0x000D2125
		public override long BytesSent
		{
			get
			{
				return (long)((ulong)this.ifRow.dwOutOctets);
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x000D3133 File Offset: 0x000D2133
		public override long BytesReceived
		{
			get
			{
				return (long)((ulong)this.ifRow.dwInOctets);
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x000D3141 File Offset: 0x000D2141
		public override long UnicastPacketsSent
		{
			get
			{
				return (long)((ulong)this.ifRow.dwOutUcastPkts);
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x000D314F File Offset: 0x000D214F
		public override long UnicastPacketsReceived
		{
			get
			{
				return (long)((ulong)this.ifRow.dwInUcastPkts);
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000D315D File Offset: 0x000D215D
		public override long NonUnicastPacketsSent
		{
			get
			{
				return (long)((ulong)this.ifRow.dwOutNUcastPkts);
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x000D316B File Offset: 0x000D216B
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return (long)((ulong)this.ifRow.dwInNUcastPkts);
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000D3179 File Offset: 0x000D2179
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.ifRow.dwInDiscards);
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x000D3187 File Offset: 0x000D2187
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.ifRow.dwOutDiscards);
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000D3195 File Offset: 0x000D2195
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return (long)((ulong)this.ifRow.dwInErrors);
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x000D31A3 File Offset: 0x000D21A3
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return (long)((ulong)this.ifRow.dwOutErrors);
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x000D31B1 File Offset: 0x000D21B1
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return (long)((ulong)this.ifRow.dwInUnknownProtos);
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x000D31BF File Offset: 0x000D21BF
		internal long Mtu
		{
			get
			{
				return (long)((ulong)this.ifRow.dwMtu);
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x000D31D0 File Offset: 0x000D21D0
		internal OperationalStatus OperationalStatus
		{
			get
			{
				switch (this.ifRow.operStatus)
				{
				case OldOperationalStatus.NonOperational:
					return OperationalStatus.Down;
				case OldOperationalStatus.Unreachable:
					return OperationalStatus.Down;
				case OldOperationalStatus.Disconnected:
					return OperationalStatus.Dormant;
				case OldOperationalStatus.Connecting:
					return OperationalStatus.Dormant;
				case OldOperationalStatus.Connected:
					return OperationalStatus.Up;
				case OldOperationalStatus.Operational:
					return OperationalStatus.Up;
				default:
					return OperationalStatus.Unknown;
				}
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000D3216 File Offset: 0x000D2216
		internal long Speed
		{
			get
			{
				return (long)((ulong)this.ifRow.dwSpeed);
			}
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000D3224 File Offset: 0x000D2224
		private void GetIfEntry(long index)
		{
			if (index == 0L)
			{
				return;
			}
			this.ifRow.dwIndex = (uint)index;
			uint ifEntry = UnsafeNetInfoNativeMethods.GetIfEntry(ref this.ifRow);
			if (ifEntry != 0U)
			{
				throw new NetworkInformationException((int)ifEntry);
			}
		}

		// Token: 0x04002E67 RID: 11879
		private MibIfRow ifRow = default(MibIfRow);
	}
}
