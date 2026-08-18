using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000129 RID: 297
	public interface IAsymmetricBlockCipher
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000AD4 RID: 2772
		string AlgorithmName { get; }

		// Token: 0x06000AD5 RID: 2773
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x06000AD6 RID: 2774
		int GetInputBlockSize();

		// Token: 0x06000AD7 RID: 2775
		int GetOutputBlockSize();

		// Token: 0x06000AD8 RID: 2776
		byte[] ProcessBlock(byte[] inBuf, int inOff, int inLen);
	}
}
