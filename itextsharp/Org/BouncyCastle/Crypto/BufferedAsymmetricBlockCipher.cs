using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020005AD RID: 1453
	public class BufferedAsymmetricBlockCipher : BufferedCipherBase
	{
		// Token: 0x0600322A RID: 12842 RVA: 0x0013836A File Offset: 0x0013736A
		public BufferedAsymmetricBlockCipher(IAsymmetricBlockCipher cipher)
		{
			this.cipher = cipher;
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x00138379 File Offset: 0x00137379
		internal int GetBufferPosition()
		{
			return this.bufOff;
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600322C RID: 12844 RVA: 0x00138381 File Offset: 0x00137381
		public override string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x0013838E File Offset: 0x0013738E
		public override int GetBlockSize()
		{
			return this.cipher.GetInputBlockSize();
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x0013839B File Offset: 0x0013739B
		public override int GetOutputSize(int length)
		{
			return this.cipher.GetOutputBlockSize();
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x001383A8 File Offset: 0x001373A8
		public override int GetUpdateOutputSize(int length)
		{
			return 0;
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x001383AB File Offset: 0x001373AB
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.Reset();
			this.cipher.Init(forEncryption, parameters);
			this.buffer = new byte[this.cipher.GetInputBlockSize() + (forEncryption ? 1 : 0)];
			this.bufOff = 0;
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x001383E8 File Offset: 0x001373E8
		public override byte[] ProcessByte(byte input)
		{
			if (this.bufOff >= this.buffer.Length)
			{
				throw new DataLengthException("attempt to process message to long for cipher");
			}
			this.buffer[this.bufOff++] = input;
			return null;
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x0013842C File Offset: 0x0013742C
		public override byte[] ProcessBytes(byte[] input, int inOff, int length)
		{
			if (length < 1)
			{
				return null;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (this.bufOff + length > this.buffer.Length)
			{
				throw new DataLengthException("attempt to process message to long for cipher");
			}
			Array.Copy(input, inOff, this.buffer, this.bufOff, length);
			this.bufOff += length;
			return null;
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x00138490 File Offset: 0x00137490
		public override byte[] DoFinal()
		{
			byte[] result = (this.bufOff > 0) ? this.cipher.ProcessBlock(this.buffer, 0, this.bufOff) : BufferedCipherBase.EmptyBuffer;
			this.Reset();
			return result;
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x001384CD File Offset: 0x001374CD
		public override byte[] DoFinal(byte[] input, int inOff, int length)
		{
			this.ProcessBytes(input, inOff, length);
			return this.DoFinal();
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x001384DF File Offset: 0x001374DF
		public override void Reset()
		{
			if (this.buffer != null)
			{
				Array.Clear(this.buffer, 0, this.buffer.Length);
				this.bufOff = 0;
			}
		}

		// Token: 0x04002268 RID: 8808
		private readonly IAsymmetricBlockCipher cipher;

		// Token: 0x04002269 RID: 8809
		private byte[] buffer;

		// Token: 0x0400226A RID: 8810
		private int bufOff;
	}
}
