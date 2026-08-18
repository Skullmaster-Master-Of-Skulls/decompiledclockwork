using System;
using OracleInternal.I18N;

namespace OracleInternal.Network
{
	// Token: 0x02000164 RID: 356
	internal class AcceptPacket : Packet
	{
		// Token: 0x06000E12 RID: 3602 RVA: 0x00094478 File Offset: 0x00092678
		internal AcceptPacket(Packet pkt) : base(pkt)
		{
			this.m_sessionCtx.m_myversion = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPACVSN] & byte.MaxValue);
			this.m_sessionCtx.m_myversion <<= 8;
			this.m_sessionCtx.m_myversion |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACVSN + 1)] & byte.MaxValue);
			this.m_sessionCtx.m_negotiatedOptions = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPACOPT] & byte.MaxValue);
			this.m_sessionCtx.m_negotiatedOptions <<= 8;
			this.m_sessionCtx.m_negotiatedOptions |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACOPT + 1)] & byte.MaxValue);
			this.m_sessionCtx.m_sessionDataUnit = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPACSDU] & byte.MaxValue);
			this.m_sessionCtx.m_sessionDataUnit <<= 8;
			this.m_sessionCtx.m_sessionDataUnit |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACSDU + 1)] & byte.MaxValue);
			this.m_sessionCtx.m_transportDataUnit = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPACTDU] & byte.MaxValue);
			this.m_sessionCtx.m_transportDataUnit <<= 8;
			this.m_sessionCtx.m_transportDataUnit |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACTDU + 1)] & byte.MaxValue);
			this.m_sessionCtx.m_hisone = (ushort)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPACONE] & byte.MaxValue);
			SessionContext sessionCtx = this.m_sessionCtx;
			sessionCtx.m_hisone = (ushort)(sessionCtx.m_hisone << 8);
			SessionContext sessionCtx2 = this.m_sessionCtx;
			sessionCtx2.m_hisone |= (ushort)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACONE + 1)] & byte.MaxValue);
			this.m_dataLength = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPACLEN] & byte.MaxValue);
			this.m_dataLength <<= 8;
			this.m_dataLength |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACLEN + 1)] & byte.MaxValue);
			this.m_dataOffset = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPACOFF] & byte.MaxValue);
			this.m_dataOffset <<= 8;
			this.m_dataOffset |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACOFF + 1)] & byte.MaxValue);
			this.m_sessionCtx.m_ACFL0 = (int)this.m_dataBuffer[(int)TNSPacketOffsets.NSPACFL0];
			this.m_sessionCtx.m_ACFL1 = (int)this.m_dataBuffer[(int)TNSPacketOffsets.NSPACFL1];
			if (this.m_sessionCtx.m_myversion >= 310)
			{
				int num = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPACADL] & byte.MaxValue);
				num <<= 8;
				num |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACADL + 1)] & byte.MaxValue);
				if (num > 0)
				{
					int num2 = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPACAOF] & byte.MaxValue);
					num2 <<= 8;
					num2 |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACAOF + 1)] & byte.MaxValue);
					this.m_sessionCtx.m_reconAddr = Conv.GetInstance(871).ConvertBytesToString(this.m_dataBuffer, num2, num, null, true);
					if ((this.m_flags & Packet.NSPFSID) > 0)
					{
						this.m_length -= Packet.NSPSID_SZ;
						this.m_sessionCtx.m_SID = new byte[Packet.NSPSID_SZ];
						Buffer.BlockCopy(this.m_dataBuffer, this.m_length, this.m_sessionCtx.m_SID, 0, Packet.NSPSID_SZ);
					}
				}
			}
			base.ExtractData();
			if (this.m_sessionCtx.m_transportDataUnit < this.m_sessionCtx.m_sessionDataUnit)
			{
				this.m_sessionCtx.m_sessionDataUnit = this.m_sessionCtx.m_transportDataUnit;
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00094824 File Offset: 0x00092A24
		internal AcceptPacket(SessionContext sessCtx, string AcceptData) : base(sessCtx)
		{
			int num = (AcceptData == null) ? 0 : AcceptData.Length;
			int num2 = (int)TNSPacketOffsets.NSPACDAT;
			if (AcceptData != null)
			{
				this.m_AcceptData = Conv.GetInstance(871).ConvertStringToBytes(AcceptData, 0, AcceptData.Length, true);
				this.m_AcceptDataOF = (AcceptData.Length >= (int)TNSPacketOffsets.NSPMXCDATA);
				if (!this.m_AcceptDataOF)
				{
					num2 += AcceptData.Length;
				}
			}
			base.CreateBuffer(num2, 2, 0);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPACVSN] = (byte)(sessCtx.m_myversion / 256 & 255);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACVSN + 1)] = (byte)(sessCtx.m_myversion % 256 & 255);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPACOPT] = (byte)(sessCtx.m_options / 256 & 255);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACOPT + 1)] = (byte)(sessCtx.m_options % 256 & 255);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPACSDU] = (byte)(sessCtx.m_sessionDataUnit / 256);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACSDU + 1)] = (byte)(sessCtx.m_sessionDataUnit % 256);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPACTDU] = (byte)(sessCtx.m_transportDataUnit / 256);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACTDU + 1)] = (byte)(sessCtx.m_transportDataUnit % 256);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPACONE] = (byte)(sessCtx.m_ourone >> 8 & 255);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACONE + 1)] = (byte)(sessCtx.m_ourone & 255);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPACLEN] = (byte)(num / 256);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACLEN + 1)] = (byte)(num % 256);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPACOFF + 1)] = TNSPacketOffsets.NSPACDAT;
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPACFL0] = TNSPacketOffsets.NSINADISABLEFORCONNECTION;
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPACFL1] = TNSPacketOffsets.NSINADISABLEFORCONNECTION;
			if (this.m_sessionCtx.m_bAnoEnabled)
			{
				this.m_dataBuffer[(int)TNSPacketOffsets.NSPACFL0] = (byte)this.m_sessionCtx.m_ano.m_naFlags;
				this.m_dataBuffer[(int)TNSPacketOffsets.NSPACFL1] = (byte)this.m_sessionCtx.m_ano.m_naFlags;
			}
			else
			{
				this.m_dataBuffer[(int)TNSPacketOffsets.NSPACFL0] = TNSPacketOffsets.NSINADISABLEFORCONNECTION;
				this.m_dataBuffer[(int)TNSPacketOffsets.NSPACFL1] = TNSPacketOffsets.NSINADISABLEFORCONNECTION;
			}
			if (AcceptData != null && !this.m_AcceptDataOF && this.m_AcceptData.Length > 0)
			{
				Buffer.BlockCopy(this.m_AcceptData, 0, this.m_dataBuffer, (int)TNSPacketOffsets.NSPACDAT, this.m_AcceptData.Length);
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00094ABC File Offset: 0x00092CBC
		internal override void Send()
		{
			base.Send();
			if (this.m_AcceptDataOF)
			{
				DataPacket dataPacket = new DataPacket(this.m_sessionCtx, this.m_AcceptData.Length + Packet.NSPOVR_SZ);
				dataPacket.PutDataInBuffer(this.m_AcceptData, 0, this.m_AcceptData.Length);
				dataPacket.Send();
			}
		}

		// Token: 0x04000F87 RID: 3975
		private byte[] m_AcceptData;

		// Token: 0x04000F88 RID: 3976
		private bool m_AcceptDataOF;
	}
}
