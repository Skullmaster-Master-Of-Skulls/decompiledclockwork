using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x020004C1 RID: 1217
	public class SicBlockCipher : IBlockCipher
	{
		// Token: 0x06002977 RID: 10615 RVA: 0x000FCAE8 File Offset: 0x000FBAE8
		public SicBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			this.IV = new byte[this.blockSize];
			this.counter = new byte[this.blockSize];
			this.counterOut = new byte[this.blockSize];
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000FCB41 File Offset: 0x000FBB41
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000FCB4C File Offset: 0x000FBB4C
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				Array.Copy(iv, 0, this.IV, 0, this.IV.Length);
				this.Reset();
				this.cipher.Init(true, parametersWithIV.Parameters);
				return;
			}
			throw new ArgumentException("SIC mode requires ParametersWithIV", "parameters");
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x000FCBAD File Offset: 0x000FBBAD
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/SIC";
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x000FCBC4 File Offset: 0x000FBBC4
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000FCBC7 File Offset: 0x000FBBC7
		public int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000FCBD4 File Offset: 0x000FBBD4
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			this.cipher.ProcessBlock(this.counter, 0, this.counterOut, 0);
			for (int i = 0; i < this.counterOut.Length; i++)
			{
				output[outOff + i] = (this.counterOut[i] ^ input[inOff + i]);
			}
			int num = this.counter.Length;
			while (--num >= 0)
			{
				byte[] array = this.counter;
				int num2 = num;
				if ((array[num2] += 1) != 0)
				{
					break;
				}
			}
			return this.counter.Length;
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x000FCC5B File Offset: 0x000FBC5B
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.counter, 0, this.counter.Length);
			this.cipher.Reset();
		}

		// Token: 0x04001CFA RID: 7418
		private readonly IBlockCipher cipher;

		// Token: 0x04001CFB RID: 7419
		private readonly int blockSize;

		// Token: 0x04001CFC RID: 7420
		private readonly byte[] IV;

		// Token: 0x04001CFD RID: 7421
		private readonly byte[] counter;

		// Token: 0x04001CFE RID: 7422
		private readonly byte[] counterOut;
	}
}
