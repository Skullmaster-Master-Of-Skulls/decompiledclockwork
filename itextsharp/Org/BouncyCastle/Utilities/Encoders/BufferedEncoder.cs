using System;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x0200033B RID: 827
	public class BufferedEncoder
	{
		// Token: 0x06001DF4 RID: 7668 RVA: 0x000B4852 File Offset: 0x000B3852
		public BufferedEncoder(ITranslator translator, int bufferSize)
		{
			this.translator = translator;
			if (bufferSize % translator.GetEncodedBlockSize() != 0)
			{
				throw new ArgumentException("buffer size not multiple of input block size");
			}
			this.Buffer = new byte[bufferSize];
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x000B4884 File Offset: 0x000B3884
		public int ProcessByte(byte input, byte[] outBytes, int outOff)
		{
			int result = 0;
			this.Buffer[this.bufOff++] = input;
			if (this.bufOff == this.Buffer.Length)
			{
				result = this.translator.Encode(this.Buffer, 0, this.Buffer.Length, outBytes, outOff);
				this.bufOff = 0;
			}
			return result;
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000B48E4 File Offset: 0x000B38E4
		public int ProcessBytes(byte[] input, int inOff, int len, byte[] outBytes, int outOff)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int num = 0;
			int num2 = this.Buffer.Length - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.Buffer, this.bufOff, num2);
				num += this.translator.Encode(this.Buffer, 0, this.Buffer.Length, outBytes, outOff);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				outOff += num;
				int num3 = len - len % this.Buffer.Length;
				num += this.translator.Encode(input, inOff, num3, outBytes, outOff);
				len -= num3;
				inOff += num3;
			}
			if (len != 0)
			{
				Array.Copy(input, inOff, this.Buffer, this.bufOff, len);
				this.bufOff += len;
			}
			return num;
		}

		// Token: 0x040014EF RID: 5359
		internal byte[] Buffer;

		// Token: 0x040014F0 RID: 5360
		internal int bufOff;

		// Token: 0x040014F1 RID: 5361
		internal ITranslator translator;
	}
}
