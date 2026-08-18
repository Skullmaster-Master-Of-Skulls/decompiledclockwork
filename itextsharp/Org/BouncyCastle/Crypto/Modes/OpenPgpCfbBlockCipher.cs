using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x0200042E RID: 1070
	public class OpenPgpCfbBlockCipher : IBlockCipher
	{
		// Token: 0x0600246A RID: 9322 RVA: 0x000DE0BC File Offset: 0x000DD0BC
		public OpenPgpCfbBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			this.IV = new byte[this.blockSize];
			this.FR = new byte[this.blockSize];
			this.FRE = new byte[this.blockSize];
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000DE115 File Offset: 0x000DD115
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x000DE11D File Offset: 0x000DD11D
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/OpenPGPCFB";
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x000DE134 File Offset: 0x000DD134
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000DE137 File Offset: 0x000DD137
		public int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000DE144 File Offset: 0x000DD144
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.forEncryption)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000DE165 File Offset: 0x000DD165
		public void Reset()
		{
			this.count = 0;
			Array.Copy(this.IV, 0, this.FR, 0, this.FR.Length);
			this.cipher.Reset();
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000DE194 File Offset: 0x000DD194
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				if (iv.Length < this.IV.Length)
				{
					Array.Copy(iv, 0, this.IV, this.IV.Length - iv.Length, iv.Length);
					for (int i = 0; i < this.IV.Length - iv.Length; i++)
					{
						this.IV[i] = 0;
					}
				}
				else
				{
					Array.Copy(iv, 0, this.IV, 0, this.IV.Length);
				}
				parameters = parametersWithIV.Parameters;
			}
			this.Reset();
			this.cipher.Init(true, parameters);
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000DE23A File Offset: 0x000DD23A
		private byte EncryptByte(byte data, int blockOff)
		{
			return this.FRE[blockOff] ^ data;
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000DE248 File Offset: 0x000DD248
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + this.blockSize > outBytes.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			if (this.count > this.blockSize)
			{
				this.FR[this.blockSize - 2] = (outBytes[outOff] = this.EncryptByte(input[inOff], this.blockSize - 2));
				this.FR[this.blockSize - 1] = (outBytes[outOff + 1] = this.EncryptByte(input[inOff + 1], this.blockSize - 1));
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				for (int i = 2; i < this.blockSize; i++)
				{
					this.FR[i - 2] = (outBytes[outOff + i] = this.EncryptByte(input[inOff + i], i - 2));
				}
			}
			else if (this.count == 0)
			{
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				for (int j = 0; j < this.blockSize; j++)
				{
					this.FR[j] = (outBytes[outOff + j] = this.EncryptByte(input[inOff + j], j));
				}
				this.count += this.blockSize;
			}
			else if (this.count == this.blockSize)
			{
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				outBytes[outOff] = this.EncryptByte(input[inOff], 0);
				outBytes[outOff + 1] = this.EncryptByte(input[inOff + 1], 1);
				Array.Copy(this.FR, 2, this.FR, 0, this.blockSize - 2);
				Array.Copy(outBytes, outOff, this.FR, this.blockSize - 2, 2);
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				for (int k = 2; k < this.blockSize; k++)
				{
					this.FR[k - 2] = (outBytes[outOff + k] = this.EncryptByte(input[inOff + k], k - 2));
				}
				this.count += this.blockSize;
			}
			return this.blockSize;
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000DE48C File Offset: 0x000DD48C
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + this.blockSize > outBytes.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			if (this.count > this.blockSize)
			{
				byte b = input[inOff];
				this.FR[this.blockSize - 2] = b;
				outBytes[outOff] = this.EncryptByte(b, this.blockSize - 2);
				b = input[inOff + 1];
				this.FR[this.blockSize - 1] = b;
				outBytes[outOff + 1] = this.EncryptByte(b, this.blockSize - 1);
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				for (int i = 2; i < this.blockSize; i++)
				{
					b = input[inOff + i];
					this.FR[i - 2] = b;
					outBytes[outOff + i] = this.EncryptByte(b, i - 2);
				}
			}
			else if (this.count == 0)
			{
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				for (int j = 0; j < this.blockSize; j++)
				{
					this.FR[j] = input[inOff + j];
					outBytes[j] = this.EncryptByte(input[inOff + j], j);
				}
				this.count += this.blockSize;
			}
			else if (this.count == this.blockSize)
			{
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				byte b2 = input[inOff];
				byte b3 = input[inOff + 1];
				outBytes[outOff] = this.EncryptByte(b2, 0);
				outBytes[outOff + 1] = this.EncryptByte(b3, 1);
				Array.Copy(this.FR, 2, this.FR, 0, this.blockSize - 2);
				this.FR[this.blockSize - 2] = b2;
				this.FR[this.blockSize - 1] = b3;
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				for (int k = 2; k < this.blockSize; k++)
				{
					byte b4 = input[inOff + k];
					this.FR[k - 2] = b4;
					outBytes[outOff + k] = this.EncryptByte(b4, k - 2);
				}
				this.count += this.blockSize;
			}
			return this.blockSize;
		}

		// Token: 0x0400197B RID: 6523
		private byte[] IV;

		// Token: 0x0400197C RID: 6524
		private byte[] FR;

		// Token: 0x0400197D RID: 6525
		private byte[] FRE;

		// Token: 0x0400197E RID: 6526
		private readonly IBlockCipher cipher;

		// Token: 0x0400197F RID: 6527
		private readonly int blockSize;

		// Token: 0x04001980 RID: 6528
		private int count;

		// Token: 0x04001981 RID: 6529
		private bool forEncryption;
	}
}
