using System;
using OracleInternal.I18N;

namespace OracleInternal.Network
{
	// Token: 0x0200016F RID: 367
	internal class RefusePacket : Packet
	{
		// Token: 0x06000E60 RID: 3680 RVA: 0x000970E8 File Offset: 0x000952E8
		internal RefusePacket(Packet pkt) : base(pkt)
		{
			this.m_userReason = (int)this.m_dataBuffer[(int)TNSPacketOffsets.NSPRFURS];
			this.m_systemReason = (int)this.m_dataBuffer[(int)TNSPacketOffsets.NSPRFSRS];
			this.m_dataOffset = (int)TNSPacketOffsets.NSPRFDAT;
			this.m_dataLength = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPRFLEN] & byte.MaxValue);
			this.m_dataLength <<= 8;
			this.m_dataLength |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPRFLEN + 1)] & byte.MaxValue);
			byte[] array = base.ExtractData();
			this.m_data = Conv.GetInstance(871).ConvertBytesToString(array, 0, array.Length, null, true);
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00097198 File Offset: 0x00095398
		internal string Data
		{
			get
			{
				return this.m_data;
			}
		}

		// Token: 0x0400104C RID: 4172
		internal int m_userReason;

		// Token: 0x0400104D RID: 4173
		internal int m_systemReason;

		// Token: 0x0400104E RID: 4174
		private string m_data;
	}
}
