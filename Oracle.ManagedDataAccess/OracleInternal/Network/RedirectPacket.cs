using System;
using OracleInternal.I18N;

namespace OracleInternal.Network
{
	// Token: 0x0200016E RID: 366
	internal class RedirectPacket : Packet
	{
		// Token: 0x06000E5F RID: 3679 RVA: 0x00097004 File Offset: 0x00095204
		internal RedirectPacket(Packet pkt) : base(pkt)
		{
			this.m_dataOffset = (int)TNSPacketOffsets.NSPRDDAT;
			this.m_dataLength = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPRDLEN] & byte.MaxValue);
			this.m_dataLength <<= 8;
			this.m_dataLength |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPRDLEN + 1)] & byte.MaxValue);
			byte[] bytes = base.ExtractData();
			this.redirectAddress = Conv.GetInstance(871).ConvertBytesToString(bytes, 0, this.m_dataLength, null, true);
			if ((this.m_flags & Packet.NSPFRDS) > 0)
			{
				int num = this.redirectAddress.IndexOf('\0');
				if (num > 0)
				{
					this.redirectConnectData = this.redirectAddress.Substring(num + 1, this.redirectAddress.Length - num - 1);
					this.redirectAddress = this.redirectAddress.Substring(0, num);
				}
			}
		}

		// Token: 0x0400104A RID: 4170
		internal string redirectAddress;

		// Token: 0x0400104B RID: 4171
		internal string redirectConnectData;
	}
}
