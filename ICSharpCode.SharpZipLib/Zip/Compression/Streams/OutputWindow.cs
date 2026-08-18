using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000053 RID: 83
	public class OutputWindow
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x000150E0 File Offset: 0x000140E0
		public void Write(int value)
		{
			if (this.windowFilled++ == 32768)
			{
				throw new InvalidOperationException("Window full");
			}
			this.window[this.windowEnd++] = (byte)value;
			this.windowEnd &= 32767;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001513C File Offset: 0x0001413C
		private void SlowRepeat(int repStart, int length, int distance)
		{
			while (length-- > 0)
			{
				this.window[this.windowEnd++] = this.window[repStart++];
				this.windowEnd &= 32767;
				repStart &= 32767;
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00015194 File Offset: 0x00014194
		public void Repeat(int length, int distance)
		{
			if ((this.windowFilled += length) > 32768)
			{
				throw new InvalidOperationException("Window full");
			}
			int num = this.windowEnd - distance & 32767;
			int num2 = 32768 - length;
			if (num > num2 || this.windowEnd >= num2)
			{
				this.SlowRepeat(num, length, distance);
				return;
			}
			if (length <= distance)
			{
				Array.Copy(this.window, num, this.window, this.windowEnd, length);
				this.windowEnd += length;
				return;
			}
			while (length-- > 0)
			{
				this.window[this.windowEnd++] = this.window[num++];
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001524C File Offset: 0x0001424C
		public int CopyStored(StreamManipulator input, int length)
		{
			length = Math.Min(Math.Min(length, 32768 - this.windowFilled), input.AvailableBytes);
			int num = 32768 - this.windowEnd;
			int num2;
			if (length > num)
			{
				num2 = input.CopyBytes(this.window, this.windowEnd, num);
				if (num2 == num)
				{
					num2 += input.CopyBytes(this.window, 0, length - num);
				}
			}
			else
			{
				num2 = input.CopyBytes(this.window, this.windowEnd, length);
			}
			this.windowEnd = (this.windowEnd + num2 & 32767);
			this.windowFilled += num2;
			return num2;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000152F0 File Offset: 0x000142F0
		public void CopyDict(byte[] dictionary, int offset, int length)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (this.windowFilled > 0)
			{
				throw new InvalidOperationException();
			}
			if (length > 32768)
			{
				offset += length - 32768;
				length = 32768;
			}
			Array.Copy(dictionary, offset, this.window, 0, length);
			this.windowEnd = (length & 32767);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00015350 File Offset: 0x00014350
		public int GetFreeSpace()
		{
			return 32768 - this.windowFilled;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001535E File Offset: 0x0001435E
		public int GetAvailable()
		{
			return this.windowFilled;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00015368 File Offset: 0x00014368
		public int CopyOutput(byte[] output, int offset, int len)
		{
			int num = this.windowEnd;
			if (len > this.windowFilled)
			{
				len = this.windowFilled;
			}
			else
			{
				num = (this.windowEnd - this.windowFilled + len & 32767);
			}
			int num2 = len;
			int num3 = len - num;
			if (num3 > 0)
			{
				Array.Copy(this.window, 32768 - num3, output, offset, num3);
				offset += num3;
				len = num;
			}
			Array.Copy(this.window, num - len, output, offset, len);
			this.windowFilled -= num2;
			if (this.windowFilled < 0)
			{
				throw new InvalidOperationException();
			}
			return num2;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000153FC File Offset: 0x000143FC
		public void Reset()
		{
			this.windowFilled = (this.windowEnd = 0);
		}

		// Token: 0x040002A2 RID: 674
		private const int WindowSize = 32768;

		// Token: 0x040002A3 RID: 675
		private const int WindowMask = 32767;

		// Token: 0x040002A4 RID: 676
		private byte[] window = new byte[32768];

		// Token: 0x040002A5 RID: 677
		private int windowEnd;

		// Token: 0x040002A6 RID: 678
		private int windowFilled;
	}
}
