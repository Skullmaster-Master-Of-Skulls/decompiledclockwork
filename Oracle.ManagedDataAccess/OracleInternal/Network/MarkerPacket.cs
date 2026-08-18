using System;

namespace OracleInternal.Network
{
	// Token: 0x02000169 RID: 361
	internal class MarkerPacket : Packet
	{
		// Token: 0x06000E27 RID: 3623 RVA: 0x000957A8 File Offset: 0x000939A8
		internal MarkerPacket(SessionContext sessCtx, int markerType) : base(sessCtx)
		{
			base.CreateBuffer((int)(TNSPacketOffsets.NSPMKDAT + 1), 12, 0);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPHDTYP] = 12;
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPMKTYP] = 1;
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPMKDAT] = (byte)markerType;
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x000957F8 File Offset: 0x000939F8
		internal MarkerPacket(Packet pkt) : base(pkt)
		{
			if (this.m_length != (int)(TNSPacketOffsets.NSPMKDAT + 1))
			{
				throw new NetworkException(12539);
			}
			switch (this.m_dataBuffer[(int)TNSPacketOffsets.NSPMKTYP])
			{
			case 0:
				this.m_isBreakMarker = true;
				return;
			case 1:
				this.m_markerData = (int)this.m_dataBuffer[(int)TNSPacketOffsets.NSPMKDAT];
				if (this.m_markerData == (int)MarkerPacket.NIQRMARK)
				{
					this.m_isResetMarker = true;
					return;
				}
				this.m_isBreakMarker = true;
				return;
			default:
				throw new NetworkException(12592);
			}
		}

		// Token: 0x04001028 RID: 4136
		internal const int NSPMKTD0 = 0;

		// Token: 0x04001029 RID: 4137
		internal const int NSPMKTD1 = 1;

		// Token: 0x0400102A RID: 4138
		internal static byte NIQBMARK = 1;

		// Token: 0x0400102B RID: 4139
		internal static byte NIQRMARK = 2;

		// Token: 0x0400102C RID: 4140
		internal static byte NIQIMARK = 3;

		// Token: 0x0400102D RID: 4141
		internal bool m_isResetMarker;

		// Token: 0x0400102E RID: 4142
		internal bool m_isBreakMarker;

		// Token: 0x0400102F RID: 4143
		internal int m_markerData;
	}
}
