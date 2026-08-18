using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x020002F5 RID: 757
	public class PaddedBufferedBlockCipher : BufferedBlockCipher
	{
		// Token: 0x06001BCD RID: 7117 RVA: 0x000A62C5 File Offset: 0x000A52C5
		public PaddedBufferedBlockCipher(IBlockCipher cipher, IBlockCipherPadding padding)
		{
			this.cipher = cipher;
			this.padding = padding;
			this.buf = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x000A62F3 File Offset: 0x000A52F3
		public PaddedBufferedBlockCipher(IBlockCipher cipher) : this(cipher, new Pkcs7Padding())
		{
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000A6304 File Offset: 0x000A5304
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			SecureRandom random = null;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				random = parametersWithRandom.Random;
				parameters = parametersWithRandom.Parameters;
			}
			this.Reset();
			this.padding.Init(random);
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x000A6358 File Offset: 0x000A5358
		public override int GetOutputSize(int length)
		{
			int num = length + this.bufOff;
			int num2 = num % this.buf.Length;
			if (num2 != 0)
			{
				return num - num2 + this.buf.Length;
			}
			if (this.forEncryption)
			{
				return num + this.buf.Length;
			}
			return num;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x000A63A0 File Offset: 0x000A53A0
		public override int GetUpdateOutputSize(int length)
		{
			int num = length + this.bufOff;
			int num2 = num % this.buf.Length;
			if (num2 == 0)
			{
				return num - this.buf.Length;
			}
			return num - num2;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x000A63D4 File Offset: 0x000A53D4
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			int result = 0;
			if (this.bufOff == this.buf.Length)
			{
				result = this.cipher.ProcessBlock(this.buf, 0, output, outOff);
				this.bufOff = 0;
			}
			this.buf[this.bufOff++] = input;
			return result;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x000A642C File Offset: 0x000A542C
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (length < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.GetBlockSize();
			int updateOutputSize = this.GetUpdateOutputSize(length);
			if (updateOutputSize > 0 && outOff + updateOutputSize > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			int num = 0;
			int num2 = this.buf.Length - this.bufOff;
			if (length > num2)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num2);
				num += this.cipher.ProcessBlock(this.buf, 0, output, outOff);
				this.bufOff = 0;
				length -= num2;
				inOff += num2;
				while (length > this.buf.Length)
				{
					num += this.cipher.ProcessBlock(input, inOff, output, outOff + num);
					length -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, length);
			this.bufOff += length;
			return num;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x000A6514 File Offset: 0x000A5514
		public override int DoFinal(byte[] output, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			int num = 0;
			if (this.forEncryption)
			{
				if (this.bufOff == blockSize)
				{
					if (outOff + 2 * blockSize > output.Length)
					{
						this.Reset();
						throw new DataLengthException("output buffer too short");
					}
					num = this.cipher.ProcessBlock(this.buf, 0, output, outOff);
					this.bufOff = 0;
				}
				this.padding.AddPadding(this.buf, this.bufOff);
				num += this.cipher.ProcessBlock(this.buf, 0, output, outOff + num);
				this.Reset();
			}
			else
			{
				if (this.bufOff != blockSize)
				{
					this.Reset();
					throw new DataLengthException("last block incomplete in decryption");
				}
				num = this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
				this.bufOff = 0;
				try
				{
					num -= this.padding.PadCount(this.buf);
					Array.Copy(this.buf, 0, output, outOff, num);
				}
				finally
				{
					this.Reset();
				}
			}
			return num;
		}

		// Token: 0x04001308 RID: 4872
		private readonly IBlockCipherPadding padding;
	}
}
