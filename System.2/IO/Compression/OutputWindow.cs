using System;

namespace System.IO.Compression
{
	// Token: 0x02000437 RID: 1079
	internal class OutputWindow
	{
		// Token: 0x06002877 RID: 10359 RVA: 0x000B9FF0 File Offset: 0x000B81F0
		public void Write(byte b)
		{
			byte[] array = this.window;
			int num = this.end;
			this.end = num + 1;
			array[num] = b;
			this.end &= 32767;
			this.bytesUsed++;
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000BA038 File Offset: 0x000B8238
		public void WriteLengthDistance(int length, int distance)
		{
			this.bytesUsed += length;
			int num = this.end - distance & 32767;
			int num2 = 32768 - length;
			if (num > num2 || this.end >= num2)
			{
				while (length-- > 0)
				{
					byte[] array = this.window;
					int num3 = this.end;
					this.end = num3 + 1;
					array[num3] = this.window[num++];
					this.end &= 32767;
					num &= 32767;
				}
				return;
			}
			if (length <= distance)
			{
				Array.Copy(this.window, num, this.window, this.end, length);
				this.end += length;
				return;
			}
			while (length-- > 0)
			{
				byte[] array2 = this.window;
				int num3 = this.end;
				this.end = num3 + 1;
				array2[num3] = this.window[num++];
			}
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000BA120 File Offset: 0x000B8320
		public int CopyFrom(InputBuffer input, int length)
		{
			length = Math.Min(Math.Min(length, 32768 - this.bytesUsed), input.AvailableBytes);
			int num = 32768 - this.end;
			int num2;
			if (length > num)
			{
				num2 = input.CopyTo(this.window, this.end, num);
				if (num2 == num)
				{
					num2 += input.CopyTo(this.window, 0, length - num);
				}
			}
			else
			{
				num2 = input.CopyTo(this.window, this.end, length);
			}
			this.end = (this.end + num2 & 32767);
			this.bytesUsed += num2;
			return num2;
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x000BA1C1 File Offset: 0x000B83C1
		public int FreeBytes
		{
			get
			{
				return 32768 - this.bytesUsed;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x000BA1CF File Offset: 0x000B83CF
		public int AvailableBytes
		{
			get
			{
				return this.bytesUsed;
			}
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000BA1D8 File Offset: 0x000B83D8
		public int CopyTo(byte[] output, int offset, int length)
		{
			int num;
			if (length > this.bytesUsed)
			{
				num = this.end;
				length = this.bytesUsed;
			}
			else
			{
				num = (this.end - this.bytesUsed + length & 32767);
			}
			int num2 = length;
			int num3 = length - num;
			if (num3 > 0)
			{
				Array.Copy(this.window, 32768 - num3, output, offset, num3);
				offset += num3;
				length = num;
			}
			Array.Copy(this.window, num - length, output, offset, length);
			this.bytesUsed -= num2;
			return num2;
		}

		// Token: 0x0400223A RID: 8762
		private const int WindowSize = 32768;

		// Token: 0x0400223B RID: 8763
		private const int WindowMask = 32767;

		// Token: 0x0400223C RID: 8764
		private byte[] window = new byte[32768];

		// Token: 0x0400223D RID: 8765
		private int end;

		// Token: 0x0400223E RID: 8766
		private int bytesUsed;
	}
}
