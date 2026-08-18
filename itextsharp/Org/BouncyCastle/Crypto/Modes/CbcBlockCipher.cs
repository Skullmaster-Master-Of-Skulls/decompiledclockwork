using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x020002A3 RID: 675
	public class CbcBlockCipher : IBlockCipher
	{
		// Token: 0x06001970 RID: 6512 RVA: 0x00094384 File Offset: 0x00093384
		public CbcBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			this.IV = new byte[this.blockSize];
			this.cbcV = new byte[this.blockSize];
			this.cbcNextV = new byte[this.blockSize];
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000943DD File Offset: 0x000933DD
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000943E8 File Offset: 0x000933E8
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.encrypting = forEncryption;
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				if (iv.Length != this.blockSize)
				{
					throw new ArgumentException("initialisation vector must be the same length as block size");
				}
				Array.Copy(iv, 0, this.IV, 0, iv.Length);
				parameters = parametersWithIV.Parameters;
			}
			this.Reset();
			this.cipher.Init(this.encrypting, parameters);
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00094459 File Offset: 0x00093459
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/CBC";
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x00094470 File Offset: 0x00093470
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00094473 File Offset: 0x00093473
		public int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00094480 File Offset: 0x00093480
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.encrypting)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x000944A1 File Offset: 0x000934A1
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.cbcV, 0, this.IV.Length);
			Array.Clear(this.cbcNextV, 0, this.cbcNextV.Length);
			this.cipher.Reset();
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x000944E0 File Offset: 0x000934E0
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			for (int i = 0; i < this.blockSize; i++)
			{
				byte[] array = this.cbcV;
				int num = i;
				array[num] ^= input[inOff + i];
			}
			int result = this.cipher.ProcessBlock(this.cbcV, 0, outBytes, outOff);
			Array.Copy(outBytes, outOff, this.cbcV, 0, this.cbcV.Length);
			return result;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00094564 File Offset: 0x00093564
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			Array.Copy(input, inOff, this.cbcNextV, 0, this.blockSize);
			int result = this.cipher.ProcessBlock(input, inOff, outBytes, outOff);
			for (int i = 0; i < this.blockSize; i++)
			{
				int num = outOff + i;
				outBytes[num] ^= this.cbcV[i];
			}
			byte[] array = this.cbcV;
			this.cbcV = this.cbcNextV;
			this.cbcNextV = array;
			return result;
		}

		// Token: 0x04001101 RID: 4353
		private byte[] IV;

		// Token: 0x04001102 RID: 4354
		private byte[] cbcV;

		// Token: 0x04001103 RID: 4355
		private byte[] cbcNextV;

		// Token: 0x04001104 RID: 4356
		private int blockSize;

		// Token: 0x04001105 RID: 4357
		private IBlockCipher cipher;

		// Token: 0x04001106 RID: 4358
		private bool encrypting;
	}
}
