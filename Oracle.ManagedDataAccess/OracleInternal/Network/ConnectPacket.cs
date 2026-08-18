using System;
using OracleInternal.Common;
using OracleInternal.I18N;

namespace OracleInternal.Network
{
	// Token: 0x02000165 RID: 357
	internal class ConnectPacket : Packet
	{
		// Token: 0x06000E15 RID: 3605 RVA: 0x00094B10 File Offset: 0x00092D10
		internal ConnectPacket(SessionContext sessCtx) : base(sessCtx)
		{
			this.m_connectData = sessCtx.m_connectData;
			int num = (this.m_connectData == null) ? 0 : this.m_connectData.Length;
			this.m_bConnDataOverFlow = (num > (int)TNSPacketOffsets.NSPMXCDATA);
			int pktSize = (int)TNSPacketOffsets.NSPCNDAT + (this.m_bConnDataOverFlow ? 0 : num);
			base.CreateBuffer(pktSize, 1, 0);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNVSN] = (byte)(sessCtx.m_myversion / 256 & 255);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNVSN + 1)] = (byte)(sessCtx.m_myversion % 256 & 255);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNLOV] = (byte)(sessCtx.m_loversion / 256 & 255);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNLOV + 1)] = (byte)(sessCtx.m_loversion % 256 & 255);
			if (sessCtx.m_transportAdapter.UrgentDataSupported() && !SqlNetOraConfig.DisableOOB)
			{
				sessCtx.m_options |= (int)TNSPacketOffsets.NSGRECVATTN;
			}
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNOPT] = (byte)(sessCtx.m_options / 256 & 255);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNOPT + 1)] = (byte)(sessCtx.m_options % 256 & 255);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNSDU] = (byte)(sessCtx.m_sessionDataUnit / 256);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNSDU + 1)] = (byte)(sessCtx.m_sessionDataUnit % 256);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNTDU] = (byte)(sessCtx.m_transportDataUnit / 256);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNTDU + 1)] = (byte)(sessCtx.m_transportDataUnit % 256);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNNTC] = 79;
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNNTC + 1)] = 152;
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNONE] = (byte)(sessCtx.m_ourone >> 8 & 255);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNONE + 1)] = (byte)(sessCtx.m_ourone & 255);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNLEN] = (byte)(num / 256);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNLEN + 1)] = (byte)(num % 256);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNOFF + 1)] = TNSPacketOffsets.NSPCNDAT;
			if (this.m_sessionCtx.m_bAnoEnabled)
			{
				this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNFL0] = (byte)this.m_sessionCtx.m_ano.m_naFlags;
				this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNFL1] = (byte)this.m_sessionCtx.m_ano.m_naFlags;
			}
			else
			{
				this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNFL0] = TNSPacketOffsets.NSINADISABLEFORCONNECTION;
				this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNFL1] = TNSPacketOffsets.NSINADISABLEFORCONNECTION;
			}
			if (!this.m_bConnDataOverFlow && num > 0)
			{
				byte[] array = Conv.GetInstance(871).ConvertStringToBytes(this.m_connectData, 0, this.m_connectData.Length, true);
				int num2 = array.Length;
				for (int i = 0; i < num2; i++)
				{
					this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNDAT + i] = array[i];
				}
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00094E18 File Offset: 0x00093018
		internal ConnectPacket(SessionContext sessCtx, int size) : base(sessCtx, (size > 0) ? size : ConnectPacket.NSPMXTPKTLEN)
		{
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00094E30 File Offset: 0x00093030
		internal override void Send()
		{
			base.Send();
			if (this.m_bConnDataOverFlow)
			{
				byte[] array = Conv.GetInstance(871).ConvertStringToBytes(this.m_connectData, 0, this.m_connectData.Length, true);
				DataPacket dataPacket = new DataPacket(this.m_sessionCtx, array.Length + Packet.NSPOVR_SZ);
				dataPacket.PutDataInBuffer(array, 0, array.Length);
				dataPacket.Send();
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00094E98 File Offset: 0x00093098
		internal override void Receive()
		{
			base.Receive();
			if (this.m_type != TNSPacketType.CONNECT)
			{
				throw new NetworkException(12566);
			}
			int num = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNVSN] & byte.MaxValue);
			num <<= 8;
			num |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNVSN + 1)] & byte.MaxValue);
			int num2 = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNLOV] & byte.MaxValue);
			num2 <<= 8;
			num2 |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNLOV + 1)] & byte.MaxValue);
			int num3 = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNOPT] & byte.MaxValue);
			num3 <<= 8;
			num3 |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNOPT + 1)] & byte.MaxValue);
			int num4 = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNSDU] & byte.MaxValue);
			num4 <<= 8;
			num4 |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNSDU + 1)] & byte.MaxValue);
			int num5 = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNTDU] & byte.MaxValue);
			num5 <<= 8;
			num5 |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNTDU + 1)] & byte.MaxValue);
			this.m_sessionCtx.m_hisone = (ushort)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNONE] & byte.MaxValue);
			SessionContext sessionCtx = this.m_sessionCtx;
			sessionCtx.m_hisone = (ushort)(sessionCtx.m_hisone << 8);
			SessionContext sessionCtx2 = this.m_sessionCtx;
			sessionCtx2.m_hisone |= (ushort)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNONE + 1)] & byte.MaxValue);
			this.m_dataLength = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNLEN] & byte.MaxValue);
			this.m_dataLength <<= 8;
			this.m_dataLength |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNLEN + 1)] & byte.MaxValue);
			this.m_dataOffset = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNOFF] & byte.MaxValue);
			this.m_dataOffset <<= 8;
			this.m_dataOffset |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNOFF + 1)] & byte.MaxValue);
			byte b = this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNFL0];
			byte b2 = this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNFL1];
			if (num >= 310)
			{
				int num6 = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNADL] & byte.MaxValue);
				num6 <<= 8;
				num6 |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNADL + 1)] & byte.MaxValue);
				if (num6 > 0)
				{
					int num7 = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPCNADF] & byte.MaxValue);
					num7 <<= 8;
					num7 |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPCNADF + 1)] & byte.MaxValue);
					this.m_sessionCtx.m_reconAddr = Conv.GetInstance(871).ConvertBytesToString(this.m_dataBuffer, num7, num6, null, true);
					if ((this.m_flags & Packet.NSPFSID) > 0)
					{
						this.m_length -= Packet.NSPSID_SZ;
						this.m_sessionCtx.m_SID = new byte[Packet.NSPSID_SZ];
						Buffer.BlockCopy(this.m_dataBuffer, this.m_length, this.m_sessionCtx.m_SID, 0, Packet.NSPSID_SZ);
					}
				}
			}
			if (num5 < this.m_sessionCtx.m_transportDataUnit)
			{
				this.m_sessionCtx.m_transportDataUnit = num5;
			}
			if (num4 < this.m_sessionCtx.m_sessionDataUnit)
			{
				this.m_sessionCtx.m_sessionDataUnit = num4;
			}
			if (num <= this.m_sessionCtx.m_myversion)
			{
				this.m_sessionCtx.m_myversion = num;
				if (num < this.m_sessionCtx.m_loversion)
				{
					throw new NetworkException(12618);
				}
			}
			else if (this.m_sessionCtx.m_myversion < num2)
			{
				throw new NetworkException(12618);
			}
			this.m_sessionCtx.m_loversion = Math.Max(this.m_sessionCtx.m_loversion, num2);
			byte[] array = base.ExtractData();
			if (array != null)
			{
				this.m_sessionCtx.m_connectData = Conv.GetInstance(871).ConvertBytesToString(array, 0, this.m_dataLength, null, true);
			}
			if (this.m_sessionCtx.m_transportDataUnit < this.m_sessionCtx.m_sessionDataUnit)
			{
				this.m_sessionCtx.m_sessionDataUnit = this.m_sessionCtx.m_transportDataUnit;
			}
		}

		// Token: 0x04000F89 RID: 3977
		internal static readonly int NSPMXTPKTLEN = (int)(TNSPacketOffsets.NSPMXCDATA + (ushort)TNSPacketOffsets.NSPCNDAT);

		// Token: 0x04000F8A RID: 3978
		private bool m_bConnDataOverFlow;

		// Token: 0x04000F8B RID: 3979
		private string m_connectData;
	}
}
