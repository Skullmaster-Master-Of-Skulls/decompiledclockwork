using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200012B RID: 299
	public interface IBlockCipher
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000AE3 RID: 2787
		string AlgorithmName { get; }

		// Token: 0x06000AE4 RID: 2788
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x06000AE5 RID: 2789
		int GetBlockSize();

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000AE6 RID: 2790
		bool IsPartialBlockOkay { get; }

		// Token: 0x06000AE7 RID: 2791
		int ProcessBlock(byte[] inBuf, int inOff, byte[] outBuf, int outOff);

		// Token: 0x06000AE8 RID: 2792
		void Reset();
	}
}
