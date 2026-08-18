using System;
using System.IO;

namespace Ionic.BZip2
{
	// Token: 0x02000042 RID: 66
	internal class BitWriter
	{
		// Token: 0x06000338 RID: 824 RVA: 0x00011B94 File Offset: 0x0000FD94
		public BitWriter(Stream s)
		{
			this.output = s;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00011BA3 File Offset: 0x0000FDA3
		public byte RemainingBits
		{
			get
			{
				return (byte)(this.accumulator >> 32 - this.nAccumulatedBits & 255U);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00011BBF File Offset: 0x0000FDBF
		public int NumRemainingBits
		{
			get
			{
				return this.nAccumulatedBits;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00011BC7 File Offset: 0x0000FDC7
		public int TotalBytesWrittenOut
		{
			get
			{
				return this.totalBytesWrittenOut;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00011BCF File Offset: 0x0000FDCF
		public void Reset()
		{
			this.accumulator = 0U;
			this.nAccumulatedBits = 0;
			this.totalBytesWrittenOut = 0;
			this.output.Seek(0L, SeekOrigin.Begin);
			this.output.SetLength(0L);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00011C04 File Offset: 0x0000FE04
		public void WriteBits(int nbits, uint value)
		{
			int i = this.nAccumulatedBits;
			uint num = this.accumulator;
			while (i >= 8)
			{
				this.output.WriteByte((byte)(num >> 24 & 255U));
				this.totalBytesWrittenOut++;
				num <<= 8;
				i -= 8;
			}
			this.accumulator = (num | value << 32 - i - nbits);
			this.nAccumulatedBits = i + nbits;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00011C6D File Offset: 0x0000FE6D
		public void WriteByte(byte b)
		{
			this.WriteBits(8, (uint)b);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00011C78 File Offset: 0x0000FE78
		public void WriteInt(uint u)
		{
			this.WriteBits(8, u >> 24 & 255U);
			this.WriteBits(8, u >> 16 & 255U);
			this.WriteBits(8, u >> 8 & 255U);
			this.WriteBits(8, u & 255U);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00011CC5 File Offset: 0x0000FEC5
		public void Flush()
		{
			this.WriteBits(0, 0U);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00011CD0 File Offset: 0x0000FED0
		public void FinishAndPad()
		{
			this.Flush();
			if (this.NumRemainingBits > 0)
			{
				byte value = (byte)(this.accumulator >> 24 & 255U);
				this.output.WriteByte(value);
				this.totalBytesWrittenOut++;
			}
		}

		// Token: 0x040001A9 RID: 425
		private uint accumulator;

		// Token: 0x040001AA RID: 426
		private int nAccumulatedBits;

		// Token: 0x040001AB RID: 427
		private Stream output;

		// Token: 0x040001AC RID: 428
		private int totalBytesWrittenOut;
	}
}
