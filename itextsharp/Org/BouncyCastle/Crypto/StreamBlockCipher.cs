using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000119 RID: 281
	public class StreamBlockCipher : IStreamCipher
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x00037934 File Offset: 0x00036934
		public StreamBlockCipher(IBlockCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			if (cipher.GetBlockSize() != 1)
			{
				throw new ArgumentException("block cipher block size != 1.", "cipher");
			}
			this.cipher = cipher;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00037981 File Offset: 0x00036981
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00037990 File Offset: 0x00036990
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0003799D File Offset: 0x0003699D
		public byte ReturnByte(byte input)
		{
			this.oneByte[0] = input;
			this.cipher.ProcessBlock(this.oneByte, 0, this.oneByte, 0);
			return this.oneByte[0];
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x000379CC File Offset: 0x000369CC
		public void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (outOff + length > output.Length)
			{
				throw new DataLengthException("output buffer too small in ProcessBytes()");
			}
			for (int num = 0; num != length; num++)
			{
				this.cipher.ProcessBlock(input, inOff + num, output, outOff + num);
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00037A10 File Offset: 0x00036A10
		public void Reset()
		{
			this.cipher.Reset();
		}

		// Token: 0x04000871 RID: 2161
		private readonly IBlockCipher cipher;

		// Token: 0x04000872 RID: 2162
		private readonly byte[] oneByte = new byte[1];
	}
}
