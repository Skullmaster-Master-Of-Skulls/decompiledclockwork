using System;
using System.IO;

namespace OracleInternal.Network
{
	// Token: 0x02000149 RID: 329
	internal class AnoStream : Stream
	{
		// Token: 0x06000CFF RID: 3327 RVA: 0x0008E818 File Offset: 0x0008CA18
		internal AnoStream(ITransportAdapter Trans, SessionContext SessCtx)
		{
			this.m_trns = Trans;
			this.m_strm = Trans.GetStream();
			this.m_sess = SessCtx;
			this.m_wpac = SessCtx.m_writerStream.m_dataPacket;
			this.recv_buf = new byte[SessCtx.m_sessionDataUnit + this.my_slough];
			this.send_packet = 1;
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0008E8A8 File Offset: 0x0008CAA8
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x0008E8B8 File Offset: 0x0008CAB8
		public override long Position
		{
			get
			{
				return this.m_strm.Position;
			}
			set
			{
				this.m_strm.Position = value;
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0008E8C8 File Offset: 0x0008CAC8
		public override void Close()
		{
			this.m_strm.Close();
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0008E8D8 File Offset: 0x0008CAD8
		public new void Dispose()
		{
			this.m_strm.Dispose();
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0008E8E8 File Offset: 0x0008CAE8
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_strm.Seek(offset, origin);
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0008E8F8 File Offset: 0x0008CAF8
		public override long Length
		{
			get
			{
				return this.m_strm.Length;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0008E908 File Offset: 0x0008CB08
		public override bool CanRead
		{
			get
			{
				return this.m_strm.CanRead;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0008E918 File Offset: 0x0008CB18
		public override bool CanSeek
		{
			get
			{
				return this.m_strm.CanSeek;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0008E928 File Offset: 0x0008CB28
		public override bool CanTimeout
		{
			get
			{
				return this.m_strm.CanTimeout;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0008E938 File Offset: 0x0008CB38
		public override bool CanWrite
		{
			get
			{
				return this.m_strm.CanWrite;
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0008E948 File Offset: 0x0008CB48
		public override void SetLength(long value)
		{
			this.m_strm.SetLength(value);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0008E958 File Offset: 0x0008CB58
		public override void Flush()
		{
			this.m_strm.Flush();
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0008E968 File Offset: 0x0008CB68
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.flush_write_data();
			if (this.bytes_read != 0)
			{
				int num = Math.Min(this.bytes_read - AnoStream.SSPI_OFFSET, count);
				Array.Copy(this.recv_buf, this.read_offset, buffer, offset, num);
				this.read_offset += num;
				if (this.read_offset >= this.bytes_read)
				{
					this.bytes_read = 0;
				}
				return num;
			}
			if (count != AnoStream.Length_Packet.Length)
			{
				throw new NetworkException(-6328);
			}
			if (this.auth_msg_sent)
			{
				Array.Copy(AnoStream.EOF_Packet, 0, buffer, offset, count);
			}
			else
			{
				this.bytes_read = this.m_strm.Read(this.recv_buf, 0, AnoStream.NS_HEADER_LEN);
				while (this.bytes_read < AnoStream.NS_HEADER_LEN)
				{
					this.bytes_read += this.m_strm.Read(this.recv_buf, this.bytes_read, AnoStream.NS_HEADER_LEN - this.bytes_read);
				}
				int num2 = (int)(this.recv_buf[(int)TNSPacketOffsets.NSPHDLEN] & byte.MaxValue);
				num2 <<= 8;
				num2 |= (int)(this.recv_buf[(int)(TNSPacketOffsets.NSPHDLEN + 1)] & byte.MaxValue);
				while (this.bytes_read < num2)
				{
					this.bytes_read += this.m_strm.Read(this.recv_buf, this.bytes_read, this.recv_buf.Length - this.bytes_read);
				}
				this.read_offset = AnoStream.SSPI_OFFSET;
				if (this.bytes_read < AnoStream.SSPI_OFFSET)
				{
					throw new NetworkException(-6329);
				}
				int num3 = this.bytes_read - AnoStream.SSPI_OFFSET;
				Array.Copy(AnoStream.Length_Packet, 0, buffer, offset, 3);
				buffer[3 + offset] = ((byte)(num3 / 256) & byte.MaxValue);
				buffer[4 + offset] = ((byte)(num3 % 256) & byte.MaxValue);
			}
			return count;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0008EB34 File Offset: 0x0008CD34
		public override int ReadByte()
		{
			return this.m_strm.ReadByte();
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0008EB44 File Offset: 0x0008CD44
		private void flush_write_data()
		{
			ushort num = (ushort)this.m_wpac.m_packetOffset;
			byte[] dataBuffer = this.m_wpac.m_dataBuffer;
			if ((int)num > AnoStream.NS_HEADER_LEN)
			{
				dataBuffer[AnoStream.NS_HEADER_LEN_OFFSET] = (byte)(num / 256 & 255);
				dataBuffer[AnoStream.NS_HEADER_LEN_OFFSET + 1] = (byte)(num % 256 & 255);
				dataBuffer[AnoStream.NA_HEADER_LEN_OFFSET] = (byte)((num - 10) / 256 & 255);
				dataBuffer[AnoStream.NA_HEADER_LEN_OFFSET + 1] = (byte)((num - 10) % 256 & 255);
				int num2 = this.SSPI_Offset - 8;
				int num3 = this.SSPI_Offset - 4;
				int num4 = (int)num - this.SSPI_Offset;
				byte[] bytes = BitConverter.GetBytes(num4);
				for (int i = 0; i < 4; i++)
				{
					dataBuffer[num2 + i] = bytes[i];
				}
				dataBuffer[num3] = ((byte)(num4 / 256) & byte.MaxValue);
				dataBuffer[num3 + 1] = ((byte)(num4 % 256) & byte.MaxValue);
				this.m_wpac.Send(DataPacket.NSPDAFZER);
				this.send_packet++;
				this.new_send = true;
				if (!this.neg_msg_sent)
				{
					this.neg_msg_sent = true;
					return;
				}
				if (!this.auth_msg_sent)
				{
					this.auth_msg_sent = true;
				}
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0008EC88 File Offset: 0x0008CE88
		private void Process_write(byte[] buffer, int offset, int count)
		{
			bool flag = this.send_packet == 1;
			if (this.new_send)
			{
				this.new_send = false;
				byte[] array = flag ? AnoStream.First_Packet : AnoStream.Subsq_Packets;
				if (!flag)
				{
					this.m_wpac.PutDataInBuffer(AnoStream.Initial_Header, AnoStream.NS_HEADER_LEN, AnoStream.Initial_Header.Length - AnoStream.NS_HEADER_LEN);
				}
				this.m_wpac.PutDataInBuffer(array, 0, array.Length);
				this.SSPI_Offset = this.m_wpac.m_packetOffset;
			}
			this.m_wpac.PutDataInBuffer(buffer, offset, count);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0008ED18 File Offset: 0x0008CF18
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count != 5)
			{
				this.Process_write(buffer, offset, count);
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0008ED28 File Offset: 0x0008CF28
		public override void WriteByte(byte value)
		{
			this.m_strm.WriteByte(value);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0008ED38 File Offset: 0x0008CF38
		// Note: this type is marked as 'beforefieldinit'.
		static AnoStream()
		{
			byte[] array = new byte[5];
			array[0] = 22;
			array[1] = 1;
			AnoStream.Length_Packet = array;
			byte[] array2 = new byte[5];
			array2[0] = 20;
			array2[1] = 1;
			AnoStream.EOF_Packet = array2;
		}

		// Token: 0x04000E27 RID: 3623
		private ITransportAdapter m_trns;

		// Token: 0x04000E28 RID: 3624
		private Stream m_strm;

		// Token: 0x04000E29 RID: 3625
		private SessionContext m_sess;

		// Token: 0x04000E2A RID: 3626
		private DataPacket m_wpac;

		// Token: 0x04000E2B RID: 3627
		private static byte[] Initial_Header = new byte[]
		{
			0,
			154,
			0,
			0,
			6,
			0,
			0,
			0,
			0,
			0,
			222,
			173,
			190,
			239,
			0,
			144,
			0,
			0,
			0,
			0,
			0,
			1,
			0
		};

		// Token: 0x04000E2C RID: 3628
		private static int NS_HEADER_LEN = 10;

		// Token: 0x04000E2D RID: 3629
		private static int NS_HEADER_LEN_OFFSET = 0;

		// Token: 0x04000E2E RID: 3630
		private static int NA_HEADER_LEN_OFFSET = 14;

		// Token: 0x04000E2F RID: 3631
		private static int NA_HEADER_NMS_OFFSET = 20;

		// Token: 0x04000E30 RID: 3632
		private static int HEADER_TOT_OFFSET = 23;

		// Token: 0x04000E31 RID: 3633
		private static int FIRST_PACKET_SSPI_1ST_LEN_OFFSET = 91;

		// Token: 0x04000E32 RID: 3634
		private static int FIRST_PACKET_SSPI_2ND_LEN_OFFSET = 95;

		// Token: 0x04000E33 RID: 3635
		private static int SSPI_1ST_LEN_OFFSET = 35;

		// Token: 0x04000E34 RID: 3636
		private static int SSPI_2ND_LEN_OFFSET = 39;

		// Token: 0x04000E35 RID: 3637
		private static int SSPI_OFFSET = 43;

		// Token: 0x04000E36 RID: 3638
		private static int SSPI_LEN_SZ = 5;

		// Token: 0x04000E37 RID: 3639
		private static byte[] First_Packet = new byte[]
		{
			0,
			1,
			0,
			7,
			0,
			0,
			0,
			0,
			0,
			4,
			0,
			5,
			2,
			0,
			0,
			0,
			0,
			4,
			0,
			4,
			0,
			0,
			0,
			9,
			0,
			4,
			0,
			4,
			0,
			0,
			0,
			2,
			0,
			20,
			0,
			1,
			2,
			0,
			0,
			0,
			4,
			0,
			0,
			0,
			2,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			4,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			4,
			0,
			1,
			55,
			0,
			0,
			0,
			0,
			55,
			0,
			1
		};

		// Token: 0x04000E38 RID: 3640
		private static byte[] Subsq_Packets = new byte[]
		{
			0,
			1,
			0,
			2,
			0,
			0,
			0,
			0,
			0,
			4,
			0,
			1,
			55,
			0,
			0,
			0,
			0,
			55,
			0,
			1
		};

		// Token: 0x04000E39 RID: 3641
		private static byte[] Length_Packet;

		// Token: 0x04000E3A RID: 3642
		private static byte[] EOF_Packet;

		// Token: 0x04000E3B RID: 3643
		private byte[] what_is_this = new byte[]
		{
			22,
			1,
			0,
			0,
			40
		};

		// Token: 0x04000E3C RID: 3644
		private int read_offset = AnoStream.SSPI_OFFSET;

		// Token: 0x04000E3D RID: 3645
		private int bytes_read;

		// Token: 0x04000E3E RID: 3646
		private int send_packet;

		// Token: 0x04000E3F RID: 3647
		private bool new_send = true;

		// Token: 0x04000E40 RID: 3648
		private bool neg_msg_sent;

		// Token: 0x04000E41 RID: 3649
		private bool auth_msg_sent;

		// Token: 0x04000E42 RID: 3650
		private int my_slough = 50;

		// Token: 0x04000E43 RID: 3651
		private int SSPI_Offset;

		// Token: 0x04000E44 RID: 3652
		private byte[] recv_buf;
	}
}
