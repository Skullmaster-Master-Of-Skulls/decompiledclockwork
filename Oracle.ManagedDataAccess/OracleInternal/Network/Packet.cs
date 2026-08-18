using System;
using System.IO;
using System.Net.Sockets;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x02000163 RID: 355
	internal class Packet
	{
		// Token: 0x06000E07 RID: 3591 RVA: 0x00093F74 File Offset: 0x00092174
		internal Packet(SessionContext sessCtx)
		{
			this.m_sessionCtx = sessCtx;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00093F84 File Offset: 0x00092184
		internal Packet(SessionContext sessCtx, int bufferSize) : this(sessCtx)
		{
			this.CreateBuffer(bufferSize, 0, 0);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00093F98 File Offset: 0x00092198
		internal Packet(SessionContext sessCtx, byte[] buf) : this(sessCtx)
		{
			this.m_totalLength = buf.Length - Packet.NSPSID_SZ;
			this.m_dataBuffer = buf;
			Packet.GetHeaderValues(buf, out this.m_length, out this.m_flags, out this.m_type);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00093FD0 File Offset: 0x000921D0
		internal Packet(SessionContext sessCtx, int bufferSize, int pktType, int pktFlags) : this(sessCtx)
		{
			this.CreateBuffer(bufferSize, pktType, pktFlags);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00093FE4 File Offset: 0x000921E4
		internal Packet(Packet pkt)
		{
			this.m_sessionCtx = pkt.m_sessionCtx;
			this.m_length = pkt.m_length;
			this.m_type = pkt.m_type;
			this.m_flags = pkt.m_flags;
			this.m_dataLength = pkt.m_dataLength;
			this.m_dataOffset = pkt.m_dataOffset;
			this.m_dataBuffer = pkt.m_dataBuffer;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0009404C File Offset: 0x0009224C
		internal void CreateBuffer(int pktSize, int pktType, int pktFlags)
		{
			this.m_dataBuffer = new byte[pktSize + Packet.NSPSID_SZ];
			if (pktType != 6)
			{
				this.m_totalLength = pktSize;
			}
			else
			{
				this.m_totalLength = pktSize - (Packet.NSPCHS_SZ + Packet.NSPFIF_SZ);
			}
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPHDLEN] = (byte)(this.m_totalLength / 256);
			this.m_dataBuffer[(int)(TNSPacketOffsets.NSPHDLEN + 1)] = (byte)(this.m_totalLength % 256);
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPHDTYP] = (byte)pktType;
			if (this.m_sessionCtx.m_SID != null)
			{
				pktFlags |= Packet.NSPFSID;
			}
			this.m_dataBuffer[(int)TNSPacketOffsets.NSPHDFLGS] = (byte)pktFlags;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000940F4 File Offset: 0x000922F4
		internal virtual void Send()
		{
			if (this.m_sessionCtx.m_SID == null)
			{
				this.m_sessionCtx.m_socketStream.Write(this.m_dataBuffer, 0, this.m_totalLength);
				if (ProviderConfig.m_bTraceLevelNetwork)
				{
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Send, this.m_dataBuffer, 0, this.m_totalLength);
					return;
				}
			}
			else
			{
				Buffer.BlockCopy(this.m_dataBuffer, this.m_totalLength, this.m_sessionCtx.m_SID, 0, Packet.NSPSID_SZ);
				this.m_sessionCtx.m_socketStream.Write(this.m_dataBuffer, 0, this.m_totalLength + Packet.NSPSID_SZ);
				if (ProviderConfig.m_bTraceLevelNetwork)
				{
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Send, this.m_dataBuffer, 0, this.m_totalLength + Packet.NSPSID_SZ);
				}
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x000941B8 File Offset: 0x000923B8
		internal virtual void Receive()
		{
			int i = 0;
			while (i < (int)TNSPacketOffsets.NSPSIZHD)
			{
				try
				{
					if ((i += this.m_sessionCtx.m_socketStream.Read(this.m_dataBuffer, i, (int)TNSPacketOffsets.NSPSIZHD - i)) <= 0)
					{
						if (i == 0)
						{
							throw new NetworkException(12537);
						}
						throw new NetworkException(12570);
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
				}
			}
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Receive, this.m_dataBuffer, 0, i);
			}
			Packet.GetHeaderValues(this.m_dataBuffer, out this.m_length, out this.m_flags, out this.m_type);
			int num = i;
			while (i < this.m_length)
			{
				try
				{
					if ((i += this.m_sessionCtx.m_socketStream.Read(this.m_dataBuffer, i, this.m_length - i)) <= 0)
					{
						throw new NetworkException(12570);
					}
				}
				catch (IOException)
				{
				}
			}
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Receive, this.m_dataBuffer, num, i - num);
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00094360 File Offset: 0x00092560
		internal static void GetHeaderValues(byte[] DataBuf, out int Length, out int Flags, out TNSPacketType Type)
		{
			Length = (int)(DataBuf[(int)TNSPacketOffsets.NSPHDLEN] & byte.MaxValue);
			Length <<= 8;
			Length |= (int)(DataBuf[(int)(TNSPacketOffsets.NSPHDLEN + 1)] & byte.MaxValue);
			Type = (TNSPacketType)DataBuf[(int)TNSPacketOffsets.NSPHDTYP];
			Flags = (int)DataBuf[(int)TNSPacketOffsets.NSPHDFLGS];
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000943A0 File Offset: 0x000925A0
		internal byte[] ExtractData()
		{
			byte[] array;
			if (this.m_dataLength <= 0)
			{
				array = null;
			}
			else if (this.m_length > this.m_dataOffset)
			{
				array = new byte[this.m_dataLength];
				Buffer.BlockCopy(this.m_dataBuffer, this.m_dataOffset, array, 0, this.m_dataLength);
			}
			else
			{
				byte[] array2 = new byte[this.m_dataLength];
				if (this.m_sessionCtx.m_readerStream.Read(array2) < 0)
				{
					throw new NetworkException(12570);
				}
				array = array2;
			}
			return array;
		}

		// Token: 0x04000F76 RID: 3958
		internal static readonly int NSPCHS_SZ = 64;

		// Token: 0x04000F77 RID: 3959
		internal static readonly int NSPFIF_SZ = 1;

		// Token: 0x04000F78 RID: 3960
		internal static readonly int NSPOVR_SZ = Packet.NSPCHS_SZ + Packet.NSPFIF_SZ + (int)TNSPacketOffsets.NSPDADAT;

		// Token: 0x04000F79 RID: 3961
		internal static readonly int NSPSID_SZ = 16;

		// Token: 0x04000F7A RID: 3962
		internal static readonly int NSPFSID = 1;

		// Token: 0x04000F7B RID: 3963
		internal static readonly int NSPFRDS = 2;

		// Token: 0x04000F7C RID: 3964
		internal static readonly int NSPFRDR = 4;

		// Token: 0x04000F7D RID: 3965
		internal static readonly int NSPFSRN = 8;

		// Token: 0x04000F7E RID: 3966
		internal static readonly int NSPFPRB = 16;

		// Token: 0x04000F7F RID: 3967
		internal TNSPacketType m_type;

		// Token: 0x04000F80 RID: 3968
		internal int m_length;

		// Token: 0x04000F81 RID: 3969
		internal int m_flags;

		// Token: 0x04000F82 RID: 3970
		protected int m_dataLength;

		// Token: 0x04000F83 RID: 3971
		protected int m_dataOffset;

		// Token: 0x04000F84 RID: 3972
		internal int m_totalLength;

		// Token: 0x04000F85 RID: 3973
		internal byte[] m_dataBuffer;

		// Token: 0x04000F86 RID: 3974
		internal SessionContext m_sessionCtx;
	}
}
