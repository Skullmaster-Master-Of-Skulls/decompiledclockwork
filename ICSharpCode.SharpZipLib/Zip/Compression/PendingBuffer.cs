using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000046 RID: 70
	public class PendingBuffer
	{
		// Token: 0x06000314 RID: 788 RVA: 0x00011034 File Offset: 0x00010034
		public PendingBuffer() : this(4096)
		{
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00011041 File Offset: 0x00010041
		public PendingBuffer(int bufferSize)
		{
			this.buffer_ = new byte[bufferSize];
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00011058 File Offset: 0x00010058
		public void Reset()
		{
			this.start = (this.end = (this.bitCount = 0));
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00011080 File Offset: 0x00010080
		public void WriteByte(int value)
		{
			this.buffer_[this.end++] = (byte)value;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000110A8 File Offset: 0x000100A8
		public void WriteShort(int value)
		{
			this.buffer_[this.end++] = (byte)value;
			this.buffer_[this.end++] = (byte)(value >> 8);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000110EC File Offset: 0x000100EC
		public void WriteInt(int value)
		{
			this.buffer_[this.end++] = (byte)value;
			this.buffer_[this.end++] = (byte)(value >> 8);
			this.buffer_[this.end++] = (byte)(value >> 16);
			this.buffer_[this.end++] = (byte)(value >> 24);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00011169 File Offset: 0x00010169
		public void WriteBlock(byte[] block, int offset, int length)
		{
			Array.Copy(block, offset, this.buffer_, this.end, length);
			this.end += length;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0001118D File Offset: 0x0001018D
		public int BitCount
		{
			get
			{
				return this.bitCount;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00011198 File Offset: 0x00010198
		public void AlignToByte()
		{
			if (this.bitCount > 0)
			{
				this.buffer_[this.end++] = (byte)this.bits;
				if (this.bitCount > 8)
				{
					this.buffer_[this.end++] = (byte)(this.bits >> 8);
				}
			}
			this.bits = 0U;
			this.bitCount = 0;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00011208 File Offset: 0x00010208
		public void WriteBits(int b, int count)
		{
			this.bits |= (uint)((uint)b << this.bitCount);
			this.bitCount += count;
			if (this.bitCount >= 16)
			{
				this.buffer_[this.end++] = (byte)this.bits;
				this.buffer_[this.end++] = (byte)(this.bits >> 8);
				this.bits >>= 16;
				this.bitCount -= 16;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000112A4 File Offset: 0x000102A4
		public void WriteShortMSB(int s)
		{
			this.buffer_[this.end++] = (byte)(s >> 8);
			this.buffer_[this.end++] = (byte)s;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000112E7 File Offset: 0x000102E7
		public bool IsFlushed
		{
			get
			{
				return this.end == 0;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000112F4 File Offset: 0x000102F4
		public int Flush(byte[] output, int offset, int length)
		{
			if (this.bitCount >= 8)
			{
				this.buffer_[this.end++] = (byte)this.bits;
				this.bits >>= 8;
				this.bitCount -= 8;
			}
			if (length > this.end - this.start)
			{
				length = this.end - this.start;
				Array.Copy(this.buffer_, this.start, output, offset, length);
				this.start = 0;
				this.end = 0;
			}
			else
			{
				Array.Copy(this.buffer_, this.start, output, offset, length);
				this.start += length;
			}
			return length;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000113AC File Offset: 0x000103AC
		public byte[] ToByteArray()
		{
			byte[] array = new byte[this.end - this.start];
			Array.Copy(this.buffer_, this.start, array, 0, array.Length);
			this.start = 0;
			this.end = 0;
			return array;
		}

		// Token: 0x040001ED RID: 493
		private byte[] buffer_;

		// Token: 0x040001EE RID: 494
		private int start;

		// Token: 0x040001EF RID: 495
		private int end;

		// Token: 0x040001F0 RID: 496
		private uint bits;

		// Token: 0x040001F1 RID: 497
		private int bitCount;
	}
}
