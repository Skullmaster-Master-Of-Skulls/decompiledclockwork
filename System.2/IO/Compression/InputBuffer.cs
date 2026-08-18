using System;

namespace System.IO.Compression
{
	// Token: 0x02000434 RID: 1076
	internal class InputBuffer
	{
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x000B99F0 File Offset: 0x000B7BF0
		public int AvailableBits
		{
			get
			{
				return this.bitsInBuffer;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x000B99F8 File Offset: 0x000B7BF8
		public int AvailableBytes
		{
			get
			{
				return this.end - this.start + this.bitsInBuffer / 8;
			}
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000B9A10 File Offset: 0x000B7C10
		public bool EnsureBitsAvailable(int count)
		{
			if (this.bitsInBuffer < count)
			{
				if (this.NeedsInput())
				{
					return false;
				}
				uint num = this.bitBuffer;
				byte[] array = this.buffer;
				int num2 = this.start;
				this.start = num2 + 1;
				this.bitBuffer = (num | array[num2] << (this.bitsInBuffer & 31));
				this.bitsInBuffer += 8;
				if (this.bitsInBuffer < count)
				{
					if (this.NeedsInput())
					{
						return false;
					}
					uint num3 = this.bitBuffer;
					byte[] array2 = this.buffer;
					num2 = this.start;
					this.start = num2 + 1;
					this.bitBuffer = (num3 | array2[num2] << (this.bitsInBuffer & 31));
					this.bitsInBuffer += 8;
				}
			}
			return true;
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000B9AC4 File Offset: 0x000B7CC4
		public uint TryLoad16Bits()
		{
			if (this.bitsInBuffer < 8)
			{
				if (this.start < this.end)
				{
					uint num = this.bitBuffer;
					byte[] array = this.buffer;
					int num2 = this.start;
					this.start = num2 + 1;
					this.bitBuffer = (num | array[num2] << (this.bitsInBuffer & 31));
					this.bitsInBuffer += 8;
				}
				if (this.start < this.end)
				{
					uint num3 = this.bitBuffer;
					byte[] array2 = this.buffer;
					int num2 = this.start;
					this.start = num2 + 1;
					this.bitBuffer = (num3 | array2[num2] << (this.bitsInBuffer & 31));
					this.bitsInBuffer += 8;
				}
			}
			else if (this.bitsInBuffer < 16 && this.start < this.end)
			{
				uint num4 = this.bitBuffer;
				byte[] array3 = this.buffer;
				int num2 = this.start;
				this.start = num2 + 1;
				this.bitBuffer = (num4 | array3[num2] << (this.bitsInBuffer & 31));
				this.bitsInBuffer += 8;
			}
			return this.bitBuffer;
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000B9BD3 File Offset: 0x000B7DD3
		private uint GetBitMask(int count)
		{
			return (1U << count) - 1U;
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000B9BE0 File Offset: 0x000B7DE0
		public int GetBits(int count)
		{
			if (!this.EnsureBitsAvailable(count))
			{
				return -1;
			}
			int result = (int)(this.bitBuffer & this.GetBitMask(count));
			this.bitBuffer >>= count;
			this.bitsInBuffer -= count;
			return result;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000B9C28 File Offset: 0x000B7E28
		public int CopyTo(byte[] output, int offset, int length)
		{
			int num = 0;
			while (this.bitsInBuffer > 0 && length > 0)
			{
				output[offset++] = (byte)this.bitBuffer;
				this.bitBuffer >>= 8;
				this.bitsInBuffer -= 8;
				length--;
				num++;
			}
			if (length == 0)
			{
				return num;
			}
			int num2 = this.end - this.start;
			if (length > num2)
			{
				length = num2;
			}
			Array.Copy(this.buffer, this.start, output, offset, length);
			this.start += length;
			return num + length;
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x000B9CB9 File Offset: 0x000B7EB9
		public bool NeedsInput()
		{
			return this.start == this.end;
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000B9CC9 File Offset: 0x000B7EC9
		public void SetInput(byte[] buffer, int offset, int length)
		{
			this.buffer = buffer;
			this.start = offset;
			this.end = offset + length;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000B9CE2 File Offset: 0x000B7EE2
		public void SkipBits(int n)
		{
			this.bitBuffer >>= n;
			this.bitsInBuffer -= n;
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000B9D03 File Offset: 0x000B7F03
		public void SkipToByteBoundary()
		{
			this.bitBuffer >>= this.bitsInBuffer % 8;
			this.bitsInBuffer -= this.bitsInBuffer % 8;
		}

		// Token: 0x0400222D RID: 8749
		private byte[] buffer;

		// Token: 0x0400222E RID: 8750
		private int start;

		// Token: 0x0400222F RID: 8751
		private int end;

		// Token: 0x04002230 RID: 8752
		private uint bitBuffer;

		// Token: 0x04002231 RID: 8753
		private int bitsInBuffer;
	}
}
