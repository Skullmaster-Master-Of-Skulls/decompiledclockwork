using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x0200034C RID: 844
	internal class MacCFBBlockCipher : IBlockCipher
	{
		// Token: 0x06001E6C RID: 7788 RVA: 0x000B60EC File Offset: 0x000B50EC
		public MacCFBBlockCipher(IBlockCipher cipher, int bitBlockSize)
		{
			this.cipher = cipher;
			this.blockSize = bitBlockSize / 8;
			this.IV = new byte[cipher.GetBlockSize()];
			this.cfbV = new byte[cipher.GetBlockSize()];
			this.cfbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x000B6144 File Offset: 0x000B5144
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				if (iv.Length < this.IV.Length)
				{
					Array.Copy(iv, 0, this.IV, this.IV.Length - iv.Length, iv.Length);
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

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x000B61C3 File Offset: 0x000B51C3
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/CFB" + this.blockSize * 8;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x000B61E7 File Offset: 0x000B51E7
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x000B61EA File Offset: 0x000B51EA
		public int GetBlockSize()
		{
			return this.blockSize;
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x000B61F4 File Offset: 0x000B51F4
		public int ProcessBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + this.blockSize > outBytes.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			this.cipher.ProcessBlock(this.cfbV, 0, this.cfbOutV, 0);
			for (int i = 0; i < this.blockSize; i++)
			{
				outBytes[outOff + i] = (this.cfbOutV[i] ^ input[inOff + i]);
			}
			Array.Copy(this.cfbV, this.blockSize, this.cfbV, 0, this.cfbV.Length - this.blockSize);
			Array.Copy(outBytes, outOff, this.cfbV, this.cfbV.Length - this.blockSize, this.blockSize);
			return this.blockSize;
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x000B62C2 File Offset: 0x000B52C2
		public void Reset()
		{
			this.IV.CopyTo(this.cfbV, 0);
			this.cipher.Reset();
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000B62E1 File Offset: 0x000B52E1
		public void GetMacBlock(byte[] mac)
		{
			this.cipher.ProcessBlock(this.cfbV, 0, mac, 0);
		}

		// Token: 0x04001513 RID: 5395
		private byte[] IV;

		// Token: 0x04001514 RID: 5396
		private byte[] cfbV;

		// Token: 0x04001515 RID: 5397
		private byte[] cfbOutV;

		// Token: 0x04001516 RID: 5398
		private readonly int blockSize;

		// Token: 0x04001517 RID: 5399
		private readonly IBlockCipher cipher;
	}
}
