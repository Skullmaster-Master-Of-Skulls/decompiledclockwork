using System;
using System.IO;
using System.Net.Sockets;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x02000166 RID: 358
	internal class DataPacket : Packet
	{
		// Token: 0x06000E1A RID: 3610 RVA: 0x000952B4 File Offset: 0x000934B4
		internal DataPacket(SessionContext sessCtx, int pktLength) : base(sessCtx, pktLength, 6, 0)
		{
			this.Initialize(pktLength);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x000952C8 File Offset: 0x000934C8
		internal DataPacket(SessionContext sessCtx) : base(sessCtx, sessCtx.m_sessionDataUnit, 6, 0)
		{
			this.Initialize(sessCtx.m_sessionDataUnit);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000952E8 File Offset: 0x000934E8
		internal static void InitForSend(byte[] hdr, int length, SessionContext SCtx)
		{
			hdr[(int)TNSPacketOffsets.NSPHDLEN] = (byte)(length / 256);
			hdr[(int)(TNSPacketOffsets.NSPHDLEN + 1)] = (byte)(length % 256);
			hdr[(int)TNSPacketOffsets.NSPDAFLG] = (byte)(DataPacket.NSPDAFZER / 256);
			hdr[(int)(TNSPacketOffsets.NSPDAFLG + 1)] = (byte)(DataPacket.NSPDAFZER % 256);
			hdr[(int)TNSPacketOffsets.NSPHDTYP] = 6;
			hdr[(int)TNSPacketOffsets.NSPHDFLGS] = 0;
			hdr[(int)TNSPacketOffsets.NSPHDHSM] = 0;
			hdr[(int)(TNSPacketOffsets.NSPHDHSM + 1)] = 0;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00095360 File Offset: 0x00093560
		internal override void Receive()
		{
			base.Receive();
			if ((this.m_flags & Packet.NSPFSID) > 0)
			{
				this.m_length -= Packet.NSPSID_SZ;
			}
			this.m_dataOffset = (this.m_packetOffset = (int)TNSPacketOffsets.NSPDADAT);
			this.m_dataLength = this.m_length - this.m_dataOffset;
			this.m_dataFlags = (int)(this.m_dataBuffer[(int)TNSPacketOffsets.NSPDAFLG] & byte.MaxValue);
			this.m_dataFlags <<= 8;
			this.m_dataFlags |= (int)(this.m_dataBuffer[(int)(TNSPacketOffsets.NSPDAFLG + 1)] & byte.MaxValue);
			if (this.m_type == TNSPacketType.DATA && this.m_dataLength == 0)
			{
				this.m_type = TNSPacketType.NULL;
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0009541C File Offset: 0x0009361C
		internal override void Send()
		{
			this.Send(DataPacket.NSPDAFZER);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0009542C File Offset: 0x0009362C
		internal void Send(int dataFlags)
		{
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPDAFLG] = (byte)(this.m_dataFlags / 256);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPDAFLG + 1)] = (byte)(this.m_dataFlags % 256);
			if (this.m_sessionCtx.m_SID != null)
			{
				Buffer.BlockCopy(this.m_sessionCtx.m_SID, 0, this.m_dataBuffer, this.m_packetOffset, Packet.NSPSID_SZ);
				this.m_packetOffset += Packet.NSPSID_SZ;
			}
			this.SetBufferLength(this.m_packetOffset);
			try
			{
				this.m_sessionCtx.m_socketStream.Write(this.m_dataBuffer, 0, this.m_packetOffset);
				if (ProviderConfig.m_bTraceLevelNetwork)
				{
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Send, this.m_dataBuffer, 0, this.m_packetOffset);
				}
			}
			catch (IOException ex)
			{
				if (this.m_sessionCtx.m_SID == null)
				{
					throw ex;
				}
				Exception innerException = ex.InnerException;
				while (innerException != null && !(innerException is SocketException))
				{
					innerException = innerException.InnerException;
				}
				if (innerException == null || ((SocketException)innerException).ErrorCode != 10054)
				{
					throw ex;
				}
				AddressResolution addressResolution = new AddressResolution(this.m_sessionCtx.m_reconAddr, null);
				ConnectionOption connectionOption = addressResolution.ResolveConnectionString();
				if (connectionOption == null)
				{
					throw ex;
				}
				this.m_sessionCtx.m_transportAdapter.Connect(connectionOption);
				this.m_sessionCtx.m_socketStream = this.m_sessionCtx.m_transportAdapter.GetStream();
				this.m_sessionCtx.m_socketStream.Write(this.m_dataBuffer, 0, this.m_packetOffset);
				if (ProviderConfig.m_bTraceLevelNetwork)
				{
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Send, this.m_dataBuffer, 0, this.m_packetOffset);
				}
			}
			this.m_packetOffset = (int)TNSPacketOffsets.NSPDADAT;
			this.m_availableBytesToSend = 0;
			this.m_isBufferFull = false;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x000955F0 File Offset: 0x000937F0
		internal void Initialize(int length)
		{
			this.m_dataOffset = (this.m_packetOffset = (int)TNSPacketOffsets.NSPDADAT);
			this.m_dataLength = length - this.m_dataOffset;
			this.m_dataFlags = DataPacket.NSPDAFZER;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0009562C File Offset: 0x0009382C
		internal void Initialize()
		{
			this.Initialize(this.m_totalLength);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0009563C File Offset: 0x0009383C
		internal void SetBufferLength(int length)
		{
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPHDLEN] = (byte)(length / 256);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPHDLEN + 1)] = (byte)(length % 256);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00095668 File Offset: 0x00093868
		internal int PutDataInBuffer(byte[] userBuffer, int offset, int length)
		{
			int num;
			if (this.m_totalLength - this.m_packetOffset <= length)
			{
				num = this.m_totalLength - this.m_packetOffset;
			}
			else
			{
				num = length;
			}
			if (num > 0)
			{
				Buffer.BlockCopy(userBuffer, offset, this.m_dataBuffer, this.m_packetOffset, num);
				this.m_packetOffset += num;
				this.m_isBufferFull = (this.m_packetOffset == this.m_totalLength);
				this.m_availableBytesToSend = ((this.m_dataOffset < this.m_packetOffset) ? (this.m_packetOffset - this.m_dataOffset) : 0);
			}
			return num;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x000956FC File Offset: 0x000938FC
		internal int getDataFromBuffer(byte[] userBuffer, int offset, int length)
		{
			int num;
			if (this.m_length - this.m_packetOffset <= length)
			{
				num = this.m_length - this.m_packetOffset;
			}
			else
			{
				num = length;
			}
			if (num > 0)
			{
				Buffer.BlockCopy(this.m_dataBuffer, this.m_packetOffset, userBuffer, offset, num);
				this.m_packetOffset += num;
				this.m_isBufferEmpty = (this.m_packetOffset == this.m_length);
				this.m_availableBytesToRead = this.m_dataOffset + this.m_dataLength - this.m_packetOffset;
			}
			return num;
		}

		// Token: 0x04000F8C RID: 3980
		internal static readonly int NSPDAFZER = 0;

		// Token: 0x04000F8D RID: 3981
		internal static readonly int NSPDAFMOR = 32;

		// Token: 0x04000F8E RID: 3982
		internal static readonly int NSPDAFEOF = 64;

		// Token: 0x04000F8F RID: 3983
		internal int m_packetOffset;

		// Token: 0x04000F90 RID: 3984
		internal int m_dataFlags;

		// Token: 0x04000F91 RID: 3985
		internal int m_availableBytesToSend;

		// Token: 0x04000F92 RID: 3986
		internal int m_availableBytesToRead;

		// Token: 0x04000F93 RID: 3987
		internal bool m_isBufferFull;

		// Token: 0x04000F94 RID: 3988
		internal bool m_isBufferEmpty;
	}
}
