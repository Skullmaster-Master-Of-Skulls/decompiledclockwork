using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000247 RID: 583
	public class GOfbBlockCipher : IBlockCipher
	{
		// Token: 0x0600166F RID: 5743 RVA: 0x00082694 File Offset: 0x00081694
		public GOfbBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			if (this.blockSize != 8)
			{
				throw new ArgumentException("GCTR only for 64 bit block ciphers");
			}
			this.IV = new byte[cipher.GetBlockSize()];
			this.ofbV = new byte[cipher.GetBlockSize()];
			this.ofbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00082708 File Offset: 0x00081708
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00082710 File Offset: 0x00081710
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.firstStep = true;
			this.N3 = 0;
			this.N4 = 0;
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

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x000827C4 File Offset: 0x000817C4
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/GCTR";
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x000827DB File Offset: 0x000817DB
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x000827DE File Offset: 0x000817DE
		public int GetBlockSize()
		{
			return this.blockSize;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x000827E8 File Offset: 0x000817E8
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
			if (this.firstStep)
			{
				this.firstStep = false;
				this.cipher.ProcessBlock(this.ofbV, 0, this.ofbOutV, 0);
				this.N3 = this.bytesToint(this.ofbOutV, 0);
				this.N4 = this.bytesToint(this.ofbOutV, 4);
			}
			this.N3 += 16843009;
			this.N4 += 16843012;
			this.intTobytes(this.N3, this.ofbV, 0);
			this.intTobytes(this.N4, this.ofbV, 4);
			this.cipher.ProcessBlock(this.ofbV, 0, this.ofbOutV, 0);
			for (int i = 0; i < this.blockSize; i++)
			{
				output[outOff + i] = (this.ofbOutV[i] ^ input[inOff + i]);
			}
			Array.Copy(this.ofbV, this.blockSize, this.ofbV, 0, this.ofbV.Length - this.blockSize);
			Array.Copy(this.ofbOutV, 0, this.ofbV, this.ofbV.Length - this.blockSize, this.blockSize);
			return this.blockSize;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x00082953 File Offset: 0x00081953
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.ofbV, 0, this.IV.Length);
			this.cipher.Reset();
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0008297B File Offset: 0x0008197B
		private int bytesToint(byte[] inBytes, int inOff)
		{
			return (int)((long)((long)inBytes[inOff + 3] << 24) & (long)((ulong)-16777216)) + ((int)inBytes[inOff + 2] << 16 & 16711680) + ((int)inBytes[inOff + 1] << 8 & 65280) + (int)(inBytes[inOff] & byte.MaxValue);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x000829B5 File Offset: 0x000819B5
		private void intTobytes(int num, byte[] outBytes, int outOff)
		{
			outBytes[outOff + 3] = (byte)(num >> 24);
			outBytes[outOff + 2] = (byte)(num >> 16);
			outBytes[outOff + 1] = (byte)(num >> 8);
			outBytes[outOff] = (byte)num;
		}

		// Token: 0x04000F59 RID: 3929
		private const int C1 = 16843012;

		// Token: 0x04000F5A RID: 3930
		private const int C2 = 16843009;

		// Token: 0x04000F5B RID: 3931
		private byte[] IV;

		// Token: 0x04000F5C RID: 3932
		private byte[] ofbV;

		// Token: 0x04000F5D RID: 3933
		private byte[] ofbOutV;

		// Token: 0x04000F5E RID: 3934
		private readonly int blockSize;

		// Token: 0x04000F5F RID: 3935
		private readonly IBlockCipher cipher;

		// Token: 0x04000F60 RID: 3936
		private bool firstStep = true;

		// Token: 0x04000F61 RID: 3937
		private int N3;

		// Token: 0x04000F62 RID: 3938
		private int N4;
	}
}
