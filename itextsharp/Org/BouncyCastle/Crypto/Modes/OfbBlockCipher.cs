using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x0200054D RID: 1357
	public class OfbBlockCipher : IBlockCipher
	{
		// Token: 0x06002EA5 RID: 11941 RVA: 0x0011FD40 File Offset: 0x0011ED40
		public OfbBlockCipher(IBlockCipher cipher, int blockSize)
		{
			this.cipher = cipher;
			this.blockSize = blockSize / 8;
			this.IV = new byte[cipher.GetBlockSize()];
			this.ofbV = new byte[cipher.GetBlockSize()];
			this.ofbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x0011FD96 File Offset: 0x0011ED96
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x0011FDA0 File Offset: 0x0011EDA0
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
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

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002EA8 RID: 11944 RVA: 0x0011FE3F File Offset: 0x0011EE3F
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/OFB" + this.blockSize * 8;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x0011FE63 File Offset: 0x0011EE63
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x0011FE66 File Offset: 0x0011EE66
		public int GetBlockSize()
		{
			return this.blockSize;
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x0011FE70 File Offset: 0x0011EE70
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + this.blockSize > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			this.cipher.ProcessBlock(this.ofbV, 0, this.ofbOutV, 0);
			for (int i = 0; i < this.blockSize; i++)
			{
				output[outOff + i] = (this.ofbOutV[i] ^ input[inOff + i]);
			}
			Array.Copy(this.ofbV, this.blockSize, this.ofbV, 0, this.ofbV.Length - this.blockSize);
			Array.Copy(this.ofbOutV, 0, this.ofbV, this.ofbV.Length - this.blockSize, this.blockSize);
			return this.blockSize;
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x0011FF42 File Offset: 0x0011EF42
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.ofbV, 0, this.IV.Length);
			this.cipher.Reset();
		}

		// Token: 0x04002015 RID: 8213
		private byte[] IV;

		// Token: 0x04002016 RID: 8214
		private byte[] ofbV;

		// Token: 0x04002017 RID: 8215
		private byte[] ofbOutV;

		// Token: 0x04002018 RID: 8216
		private readonly int blockSize;

		// Token: 0x04002019 RID: 8217
		private readonly IBlockCipher cipher;
	}
}
