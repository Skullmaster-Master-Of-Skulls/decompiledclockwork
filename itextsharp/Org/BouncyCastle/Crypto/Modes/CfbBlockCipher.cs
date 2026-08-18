using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000508 RID: 1288
	public class CfbBlockCipher : IBlockCipher
	{
		// Token: 0x06002BEB RID: 11243 RVA: 0x00109804 File Offset: 0x00108804
		public CfbBlockCipher(IBlockCipher cipher, int bitBlockSize)
		{
			this.cipher = cipher;
			this.blockSize = bitBlockSize / 8;
			this.IV = new byte[cipher.GetBlockSize()];
			this.cfbV = new byte[cipher.GetBlockSize()];
			this.cfbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x0010985A File Offset: 0x0010885A
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x00109864 File Offset: 0x00108864
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.encrypting = forEncryption;
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				int num = this.IV.Length - iv.Length;
				Array.Copy(iv, 0, this.IV, num, iv.Length);
				Array.Clear(this.IV, 0, num);
				parameters = parametersWithIV.Parameters;
			}
			this.Reset();
			this.cipher.Init(true, parameters);
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x001098D4 File Offset: 0x001088D4
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/CFB" + this.blockSize * 8;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x001098F8 File Offset: 0x001088F8
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x001098FB File Offset: 0x001088FB
		public int GetBlockSize()
		{
			return this.blockSize;
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x00109903 File Offset: 0x00108903
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.encrypting)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x00109924 File Offset: 0x00108924
		public int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
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

		// Token: 0x06002BF3 RID: 11251 RVA: 0x001099F4 File Offset: 0x001089F4
		public int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
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
			Array.Copy(this.cfbV, this.blockSize, this.cfbV, 0, this.cfbV.Length - this.blockSize);
			Array.Copy(input, inOff, this.cfbV, this.cfbV.Length - this.blockSize, this.blockSize);
			for (int i = 0; i < this.blockSize; i++)
			{
				outBytes[outOff + i] = (this.cfbOutV[i] ^ input[inOff + i]);
			}
			return this.blockSize;
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x00109AC1 File Offset: 0x00108AC1
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.cfbV, 0, this.IV.Length);
			this.cipher.Reset();
		}

		// Token: 0x04001E53 RID: 7763
		private byte[] IV;

		// Token: 0x04001E54 RID: 7764
		private byte[] cfbV;

		// Token: 0x04001E55 RID: 7765
		private byte[] cfbOutV;

		// Token: 0x04001E56 RID: 7766
		private bool encrypting;

		// Token: 0x04001E57 RID: 7767
		private readonly int blockSize;

		// Token: 0x04001E58 RID: 7768
		private readonly IBlockCipher cipher;
	}
}
