using System;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x02000007 RID: 7
	public class BufferedDecoder
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002678 File Offset: 0x00001678
		public BufferedDecoder(ITranslator translator, int bufferSize)
		{
			this.translator = translator;
			if (bufferSize % translator.GetEncodedBlockSize() != 0)
			{
				throw new ArgumentException("buffer size not multiple of input block size");
			}
			this.buffer = new byte[bufferSize];
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026A8 File Offset: 0x000016A8
		public int ProcessByte(byte input, byte[] output, int outOff)
		{
			int result = 0;
			this.buffer[this.bufOff++] = input;
			if (this.bufOff == this.buffer.Length)
			{
				result = this.translator.Decode(this.buffer, 0, this.buffer.Length, output, outOff);
				this.bufOff = 0;
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002708 File Offset: 0x00001708
		public int ProcessBytes(byte[] input, int inOff, int len, byte[] outBytes, int outOff)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int num = 0;
			int num2 = this.buffer.Length - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.buffer, this.bufOff, num2);
				num += this.translator.Decode(this.buffer, 0, this.buffer.Length, outBytes, outOff);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				outOff += num;
				int num3 = len - len % this.buffer.Length;
				num += this.translator.Decode(input, inOff, num3, outBytes, outOff);
				len -= num3;
				inOff += num3;
			}
			if (len != 0)
			{
				Array.Copy(input, inOff, this.buffer, this.bufOff, len);
				this.bufOff += len;
			}
			return num;
		}

		// Token: 0x04000005 RID: 5
		internal byte[] buffer;

		// Token: 0x04000006 RID: 6
		internal int bufOff;

		// Token: 0x04000007 RID: 7
		internal ITranslator translator;
	}
}
