using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200009D RID: 157
	public class BcpgOutputStream : BaseOutputStream
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x0001AAD8 File Offset: 0x00019AD8
		internal static BcpgOutputStream Wrap(Stream outStr)
		{
			if (outStr is BcpgOutputStream)
			{
				return (BcpgOutputStream)outStr;
			}
			return new BcpgOutputStream(outStr);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001AAEF File Offset: 0x00019AEF
		public BcpgOutputStream(Stream outStr)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.outStr = outStr;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001AB0C File Offset: 0x00019B0C
		public BcpgOutputStream(Stream outStr, PacketTag tag)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.outStr = outStr;
			this.WriteHeader(tag, true, true, 0L);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001AB34 File Offset: 0x00019B34
		public BcpgOutputStream(Stream outStr, PacketTag tag, long length, bool oldFormat)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.outStr = outStr;
			if (length > (long)((ulong)-1))
			{
				this.WriteHeader(tag, false, true, 0L);
				this.partialBufferLength = 65536;
				this.partialBuffer = new byte[this.partialBufferLength];
				this.partialPower = 16;
				this.partialOffset = 0;
				return;
			}
			this.WriteHeader(tag, oldFormat, false, length);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001ABA3 File Offset: 0x00019BA3
		public BcpgOutputStream(Stream outStr, PacketTag tag, long length)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.outStr = outStr;
			this.WriteHeader(tag, false, false, length);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001ABCC File Offset: 0x00019BCC
		public BcpgOutputStream(Stream outStr, PacketTag tag, byte[] buffer)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.outStr = outStr;
			this.WriteHeader(tag, false, true, 0L);
			this.partialBuffer = buffer;
			uint num = (uint)this.partialBuffer.Length;
			this.partialPower = 0;
			while (num != 1U)
			{
				num >>= 1;
				this.partialPower++;
			}
			if (this.partialPower > 30)
			{
				throw new IOException("Buffer cannot be greater than 2^30 in length.");
			}
			this.partialBufferLength = 1 << this.partialPower;
			this.partialOffset = 0;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001AC5C File Offset: 0x00019C5C
		private void WriteNewPacketLength(long bodyLen)
		{
			if (bodyLen < 192L)
			{
				this.outStr.WriteByte((byte)bodyLen);
				return;
			}
			if (bodyLen <= 8383L)
			{
				bodyLen -= 192L;
				this.outStr.WriteByte((byte)((bodyLen >> 8 & 255L) + 192L));
				this.outStr.WriteByte((byte)bodyLen);
				return;
			}
			this.outStr.WriteByte(byte.MaxValue);
			this.outStr.WriteByte((byte)(bodyLen >> 24));
			this.outStr.WriteByte((byte)(bodyLen >> 16));
			this.outStr.WriteByte((byte)(bodyLen >> 8));
			this.outStr.WriteByte((byte)bodyLen);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001AD0C File Offset: 0x00019D0C
		private void WriteHeader(PacketTag tag, bool oldPackets, bool partial, long bodyLen)
		{
			int num = 128;
			if (this.partialBuffer != null)
			{
				this.PartialFlush(true);
				this.partialBuffer = null;
			}
			if (oldPackets)
			{
				num |= (int)((int)tag << 2);
				if (partial)
				{
					this.WriteByte((byte)(num | 3));
					return;
				}
				if (bodyLen <= 255L)
				{
					this.WriteByte((byte)num);
					this.WriteByte((byte)bodyLen);
					return;
				}
				if (bodyLen <= 65535L)
				{
					this.WriteByte((byte)(num | 1));
					this.WriteByte((byte)(bodyLen >> 8));
					this.WriteByte((byte)bodyLen);
					return;
				}
				this.WriteByte((byte)(num | 2));
				this.WriteByte((byte)(bodyLen >> 24));
				this.WriteByte((byte)(bodyLen >> 16));
				this.WriteByte((byte)(bodyLen >> 8));
				this.WriteByte((byte)bodyLen);
				return;
			}
			else
			{
				num |= (int)((PacketTag)64 | tag);
				this.WriteByte((byte)num);
				if (partial)
				{
					this.partialOffset = 0;
					return;
				}
				this.WriteNewPacketLength(bodyLen);
				return;
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001ADF0 File Offset: 0x00019DF0
		private void PartialFlush(bool isLast)
		{
			if (isLast)
			{
				this.WriteNewPacketLength((long)this.partialOffset);
				this.outStr.Write(this.partialBuffer, 0, this.partialOffset);
			}
			else
			{
				this.outStr.WriteByte((byte)(224 | this.partialPower));
				this.outStr.Write(this.partialBuffer, 0, this.partialBufferLength);
			}
			this.partialOffset = 0;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001AE60 File Offset: 0x00019E60
		private void WritePartial(byte b)
		{
			if (this.partialOffset == this.partialBufferLength)
			{
				this.PartialFlush(false);
			}
			this.partialBuffer[this.partialOffset++] = b;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001AE9C File Offset: 0x00019E9C
		private void WritePartial(byte[] buffer, int off, int len)
		{
			if (this.partialOffset == this.partialBufferLength)
			{
				this.PartialFlush(false);
			}
			if (len <= this.partialBufferLength - this.partialOffset)
			{
				Array.Copy(buffer, off, this.partialBuffer, this.partialOffset, len);
				this.partialOffset += len;
				return;
			}
			int num = this.partialBufferLength - this.partialOffset;
			Array.Copy(buffer, off, this.partialBuffer, this.partialOffset, num);
			off += num;
			len -= num;
			this.PartialFlush(false);
			while (len > this.partialBufferLength)
			{
				Array.Copy(buffer, off, this.partialBuffer, 0, this.partialBufferLength);
				off += this.partialBufferLength;
				len -= this.partialBufferLength;
				this.PartialFlush(false);
			}
			Array.Copy(buffer, off, this.partialBuffer, 0, len);
			this.partialOffset += len;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001AF7B File Offset: 0x00019F7B
		public override void WriteByte(byte value)
		{
			if (this.partialBuffer != null)
			{
				this.WritePartial(value);
				return;
			}
			this.outStr.WriteByte(value);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001AF99 File Offset: 0x00019F99
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.partialBuffer != null)
			{
				this.WritePartial(buffer, offset, count);
				return;
			}
			this.outStr.Write(buffer, offset, count);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001AFBC File Offset: 0x00019FBC
		internal virtual void WriteShort(short n)
		{
			this.Write(new byte[]
			{
				(byte)(n >> 8),
				(byte)n
			});
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001AFE4 File Offset: 0x00019FE4
		internal virtual void WriteInt(int n)
		{
			this.Write(new byte[]
			{
				(byte)(n >> 24),
				(byte)(n >> 16),
				(byte)(n >> 8),
				(byte)n
			});
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001B01C File Offset: 0x0001A01C
		internal virtual void WriteLong(long n)
		{
			this.Write(new byte[]
			{
				(byte)(n >> 56),
				(byte)(n >> 48),
				(byte)(n >> 40),
				(byte)(n >> 32),
				(byte)(n >> 24),
				(byte)(n >> 16),
				(byte)(n >> 8),
				(byte)n
			});
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001B073 File Offset: 0x0001A073
		public void WritePacket(ContainedPacket p)
		{
			p.Encode(this);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001B07C File Offset: 0x0001A07C
		internal void WritePacket(PacketTag tag, byte[] body, bool oldFormat)
		{
			this.WriteHeader(tag, oldFormat, false, (long)body.Length);
			this.Write(body);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001B092 File Offset: 0x0001A092
		public void WriteObject(BcpgObject bcpgObject)
		{
			bcpgObject.Encode(this);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001B09C File Offset: 0x0001A09C
		public void WriteObjects(params BcpgObject[] v)
		{
			foreach (BcpgObject bcpgObject in v)
			{
				bcpgObject.Encode(this);
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001B0C4 File Offset: 0x0001A0C4
		public override void Flush()
		{
			this.outStr.Flush();
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001B0D1 File Offset: 0x0001A0D1
		public void Finish()
		{
			if (this.partialBuffer != null)
			{
				this.PartialFlush(true);
				this.partialBuffer = null;
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001B0E9 File Offset: 0x0001A0E9
		public override void Close()
		{
			this.Finish();
			this.outStr.Flush();
			this.outStr.Close();
			base.Close();
		}

		// Token: 0x04000283 RID: 643
		private const int BufferSizePower = 16;

		// Token: 0x04000284 RID: 644
		private Stream outStr;

		// Token: 0x04000285 RID: 645
		private byte[] partialBuffer;

		// Token: 0x04000286 RID: 646
		private int partialBufferLength;

		// Token: 0x04000287 RID: 647
		private int partialPower;

		// Token: 0x04000288 RID: 648
		private int partialOffset;
	}
}
