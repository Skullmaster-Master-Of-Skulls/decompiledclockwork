using System;
using System.Net.Sockets;
using OracleInternal.Common;
using OracleInternal.Secure.Network;

namespace OracleInternal.Network
{
	// Token: 0x0200016D RID: 365
	internal class ReaderStream
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x000965E0 File Offset: 0x000947E0
		internal ReaderStream(SessionContext sessCtx)
		{
			this.m_sessionCtx = sessCtx;
			this.m_dataPacket = new DataPacket(sessCtx);
			this.m_OraBuf = new OraBuf(this.m_dataPacket.m_dataBuffer);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00096648 File Offset: 0x00094848
		private void ProcessHeaderData(OraBuf OB, ref int Length, ref int Offset, int headerLen)
		{
			int num = headerLen - this.m_PacketHeaderLen;
			int num2 = (Length < num) ? Length : num;
			Buffer.BlockCopy(OB.buf, Offset, this.m_PacketHeader, this.m_PacketHeaderLen, num2);
			this.m_PacketHeaderLen += num2;
			Length -= num2;
			if (this.m_PacketHeaderLen == headerLen)
			{
				Packet.GetHeaderValues(this.m_PacketHeader, out this.m_PacketLength, out this.m_PacketFlags, out this.m_PacketType);
				this.m_remainder = this.m_PacketLength - headerLen;
				if (ProviderConfig.m_bTraceLevelNetwork)
				{
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Receive, new string[]
					{
						"New receive packet. Header: "
					});
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Receive, this.m_PacketHeader, 0, headerLen);
				}
			}
			Offset += num2;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0009670C File Offset: 0x0009490C
		private void ProcessOB(OraBuf OB)
		{
			int num = 0;
			if (OB.m_length == 0)
			{
				this.m_EOF = true;
				return;
			}
			int i = OB.m_length;
			OB.m_length = 0;
			while (i > 0)
			{
				if (this.m_PacketHeaderLen < (int)TNSPacketOffsets.NSPDADAT)
				{
					this.ProcessHeaderData(OB, ref i, ref num, (int)TNSPacketOffsets.NSPDADAT);
				}
				if (i > 0)
				{
					if (this.m_PacketType == TNSPacketType.MARKER)
					{
						this.m_PacketHeader[(int)TNSPacketOffsets.NSPMKDAT] = OB.buf[num];
						i--;
						num++;
						this.m_remainder--;
						MarkerPacket markerPacket = new MarkerPacket(new Packet(this.m_sessionCtx, this.m_PacketHeader));
						this.m_sessionCtx.m_onBreakReset = true;
						if (markerPacket.m_isResetMarker)
						{
							this.m_sessionCtx.m_gotReset = true;
						}
						if (ProviderConfig.m_bTraceLevelNetwork)
						{
							if (markerPacket.m_isResetMarker)
							{
								Trace.Write(OracleTraceLevel.Network, OracleTraceTag.None, new string[]
								{
									"Got a RESET marker packet"
								});
							}
							else if (markerPacket.m_isBreakMarker)
							{
								Trace.Write(OracleTraceLevel.Network, OracleTraceTag.None, new string[]
								{
									"Got a BREAK marker packet"
								});
							}
							Trace.Write(OracleTraceLevel.Network, OracleTraceTag.None, new string[]
							{
								"Marker data = " + markerPacket.m_markerData
							});
						}
						this.m_returnDataLength = 0;
					}
					if (this.m_remainder > 0)
					{
						int num2 = (i > this.m_remainder) ? this.m_remainder : i;
						OB.AddForReceive(num, num2);
						if (ProviderConfig.m_bTraceLevelNetwork)
						{
							Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Receive, OB.buf, num, num2);
						}
						i -= num2;
						this.m_remainder -= num2;
						num += num2;
					}
				}
				if (this.m_PacketHeaderLen == (int)TNSPacketOffsets.NSPDADAT && this.m_remainder == 0)
				{
					this.m_PacketHeaderLen = 0;
				}
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x000968C8 File Offset: 0x00094AC8
		private void ReadIt(OraBuf OB, int len)
		{
			OB.m_length = 0;
			try
			{
				do
				{
					if (this.m_sessionCtx.m_socket != null)
					{
						OB.m_length += this.m_sessionCtx.m_socket.Receive(OB.m_buf, OB.m_length, len - OB.m_length, SocketFlags.None);
					}
					else
					{
						OB.m_length += this.m_sessionCtx.m_socketStream.Read(OB.m_buf, OB.m_length, len - OB.m_length);
					}
				}
				while (OB.m_length != len && OB.m_length != 0);
			}
			catch (Exception inner)
			{
				throw new NetworkException(12570, inner);
			}
			if (OB.m_length == 0)
			{
				this.m_EOF = true;
				throw new NetworkException(12537);
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0009699C File Offset: 0x00094B9C
		internal void WaitForReset()
		{
			OraBuf oraBuf = this.m_OraBuf;
			oraBuf.Clear();
			if (this.m_sessionCtx.m_gotReset)
			{
				return;
			}
			if (this.m_remainder > 0)
			{
				this.ReadIt(oraBuf, this.m_remainder);
				this.ProcessOB(oraBuf);
			}
			while (!this.m_sessionCtx.m_gotReset)
			{
				oraBuf.Clear();
				this.ReadIt(oraBuf, (int)TNSPacketOffsets.NSPDADAT - this.m_PacketHeaderLen);
				this.ProcessOB(oraBuf);
				if (this.m_PacketLength - (int)TNSPacketOffsets.NSPDADAT > 0)
				{
					this.ReadIt(oraBuf, this.m_PacketLength - (int)TNSPacketOffsets.NSPDADAT);
					this.ProcessOB(oraBuf);
				}
				if (this.m_sessionCtx.cryptoNeeded)
				{
					this.HandleCrypto(oraBuf);
				}
			}
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00096A50 File Offset: 0x00094C50
		internal int Read(OraBuf OB)
		{
			if (this.m_sessionCtx.m_onBreakReset)
			{
				throw new NetworkException(3111);
			}
			if (this.m_sessionCtx.cryptoNeeded)
			{
				return this.ReadwithCrypto(OB);
			}
			goto IL_2D;
			try
			{
				do
				{
					IL_2D:
					if (this.m_sessionCtx.m_socket != null)
					{
						OB.m_length = this.m_sessionCtx.m_socket.Receive(OB.m_buf, 0, OB.m_buf.Length, SocketFlags.None);
					}
					else
					{
						OB.m_length = this.m_sessionCtx.m_socketStream.Read(OB.m_buf, 0, OB.m_buf.Length);
					}
					this.ProcessOB(OB);
				}
				while (OB.m_length == 0 && !this.m_EOF && !this.m_sessionCtx.m_onBreakReset);
			}
			catch (SocketException ex)
			{
				if (ex.ErrorCode == 10053 || ex.ErrorCode == 10054)
				{
					throw new NetworkException(3135, ex);
				}
				throw new NetworkException(12570, ex);
			}
			catch (Exception inner)
			{
				throw new NetworkException(12570, inner);
			}
			if (OB.m_length == 0 && this.m_EOF)
			{
				throw new NetworkException(12537);
			}
			if (this.m_sessionCtx.m_onBreakReset)
			{
				throw new NetworkException(3111);
			}
			return OB.m_length;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00096BA4 File Offset: 0x00094DA4
		private int ReadwithCrypto(OraBuf OB)
		{
			try
			{
				do
				{
					OB.Clear();
					this.ReadIt(OB, (int)TNSPacketOffsets.NSPDADAT - this.m_PacketHeaderLen);
					this.ProcessOB(OB);
					if (this.m_PacketLength - (int)TNSPacketOffsets.NSPDADAT > 0)
					{
						this.ReadIt(OB, this.m_PacketLength - (int)TNSPacketOffsets.NSPDADAT);
						this.ProcessOB(OB);
					}
				}
				while (OB.m_length == 0 && !this.m_EOF && !this.m_sessionCtx.m_onBreakReset);
			}
			catch (SocketException ex)
			{
				if (ex.ErrorCode == 10053 || ex.ErrorCode == 10054)
				{
					throw new NetworkException(3135, ex);
				}
				throw new NetworkException(12570, ex);
			}
			catch (Exception inner)
			{
				throw new NetworkException(12570, inner);
			}
			if (OB.m_length == 0 && this.m_EOF)
			{
				throw new NetworkException(12537);
			}
			this.HandleCrypto(OB);
			if (this.m_sessionCtx.m_onBreakReset)
			{
				throw new NetworkException(3111);
			}
			return OB.m_length;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00096CB8 File Offset: 0x00094EB8
		private void HandleCrypto(OraBuf OB)
		{
			if (OB.m_length > 0)
			{
				int num = 0;
				OraArraySegment[] the_ByteSegments = OB.the_ByteSegments;
				if (OB.the_ByteSegments_Count <= 0)
				{
					throw new NetworkException(12570, new NetworkException(12566));
				}
				OraArraySegment oraArraySegment = the_ByteSegments[0];
				if (OB.the_ByteSegments_Count == 1)
				{
					num = the_ByteSegments[0].Count;
				}
				else
				{
					for (int i = 1; i < OB.the_ByteSegments_Count; i++)
					{
						OraArraySegment oraArraySegment2 = the_ByteSegments[i - 1];
						OraArraySegment oraArraySegment3 = the_ByteSegments[i];
						if (oraArraySegment2.Array != oraArraySegment3.Array || oraArraySegment2.Array[oraArraySegment2.Offset + oraArraySegment2.Count] != oraArraySegment3.Array[oraArraySegment3.Offset])
						{
							throw new NetworkException(12570, new NetworkException(12566));
						}
						num += oraArraySegment2.Count;
					}
					num += the_ByteSegments[OB.the_ByteSegments_Count - 1].Count;
				}
				EncryptionAlgorithm encryptionAlg = this.m_sessionCtx.encryptionAlg;
				DataIntegrityAlgorithm dataIntegrityAlg = this.m_sessionCtx.m_ano.dataIntegrityAlg;
				int num2 = oraArraySegment.Count - 1;
				byte[] array;
				if (oraArraySegment.Offset == 0)
				{
					array = oraArraySegment.Array;
				}
				else
				{
					array = new byte[num2];
					Buffer.BlockCopy(oraArraySegment.Array, oraArraySegment.Offset, array, 0, num2);
				}
				byte[] array2;
				if (encryptionAlg != null)
				{
					array2 = encryptionAlg.decrypt(array, num2);
					num2 = array2.Length;
				}
				else
				{
					array2 = array;
				}
				if (dataIntegrityAlg != null)
				{
					if (dataIntegrityAlg.compare(array2, num2 - dataIntegrityAlg.size(), array2, num2 - dataIntegrityAlg.size()))
					{
						throw new NetworkException(12599);
					}
					num2 -= dataIntegrityAlg.size();
				}
				OB.Clear();
				OB.AddForReceive(array2, 0, num2);
			}
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00096E60 File Offset: 0x00095060
		internal int Read()
		{
			if (this.Read(this.m_oneByteBuffer, 0, 1) >= 0)
			{
				return (int)(this.m_oneByteBuffer[0] & byte.MaxValue);
			}
			return -1;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00096E84 File Offset: 0x00095084
		internal int ReadOne()
		{
			return this.Read();
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00096E8C File Offset: 0x0009508C
		internal int Read(byte[] userBuffer)
		{
			return this.Read(userBuffer, 0, userBuffer.Length);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00096E9C File Offset: 0x0009509C
		internal int Read(byte[] userBuffer, int offset, int length)
		{
			int num = 0;
			if (length <= 0)
			{
				throw new NetworkException(12532);
			}
			if (this.m_sessionCtx.m_usingAsyncReceives)
			{
				throw new NetworkException(12623);
			}
			if (this.m_sessionCtx.m_onBreakReset)
			{
				throw new NetworkException(3111);
			}
			goto IL_41;
			try
			{
				do
				{
					IL_41:
					if (this.m_dataPacket.m_availableBytesToRead <= 0 || this.m_dataPacket.m_type == TNSPacketType.NULL)
					{
						this.getNextPacket();
					}
					num += this.m_dataPacket.getDataFromBuffer(userBuffer, offset + num, length - num);
				}
				while (num < length);
			}
			catch (SocketException)
			{
			}
			return num;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00096F3C File Offset: 0x0009513C
		internal void getNextPacket()
		{
			if (this.m_sessionCtx.m_onBreakReset)
			{
				throw new NetworkException(3111);
			}
			if (this.m_sessionCtx.m_writerStream.m_dataPacket.m_availableBytesToSend > 0)
			{
				this.m_sessionCtx.m_writerStream.Flush();
			}
			this.m_dataPacket.Receive();
			TNSPacketType type = this.m_dataPacket.m_type;
			switch (type)
			{
			case TNSPacketType.DATA:
			case TNSPacketType.NULL:
				return;
			default:
				if (type == TNSPacketType.MARKER)
				{
					MarkerPacket markerPacket = new MarkerPacket(this.m_dataPacket);
					this.m_sessionCtx.m_onBreakReset = true;
					if (markerPacket.m_isResetMarker)
					{
						this.m_sessionCtx.m_gotReset = true;
					}
					throw new NetworkException(3111);
				}
				throw new NetworkException(12592);
			}
		}

		// Token: 0x0400103C RID: 4156
		protected SessionContext m_sessionCtx;

		// Token: 0x0400103D RID: 4157
		protected OraBuf m_OraBuf;

		// Token: 0x0400103E RID: 4158
		protected DataPacket m_dataPacket;

		// Token: 0x0400103F RID: 4159
		protected bool m_EOF;

		// Token: 0x04001040 RID: 4160
		private byte[] m_oneByteBuffer = new byte[1];

		// Token: 0x04001041 RID: 4161
		private byte[] m_PacketHeader = new byte[(int)(TNSPacketOffsets.NSPMKDAT + 1)];

		// Token: 0x04001042 RID: 4162
		private int m_PacketHeaderLen;

		// Token: 0x04001043 RID: 4163
		private int m_PacketLength;

		// Token: 0x04001044 RID: 4164
		private int m_PacketFlags;

		// Token: 0x04001045 RID: 4165
		private TNSPacketType m_PacketType;

		// Token: 0x04001046 RID: 4166
		private int m_remainder;

		// Token: 0x04001047 RID: 4167
		private int m_returnDataLength;

		// Token: 0x04001048 RID: 4168
		private static int m_listInitialSize = 10;

		// Token: 0x04001049 RID: 4169
		private object m_listlock = new object();
	}
}
