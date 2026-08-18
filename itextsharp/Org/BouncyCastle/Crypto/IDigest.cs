using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000022 RID: 34
	public interface IDigest
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D5 RID: 213
		string AlgorithmName { get; }

		// Token: 0x060000D6 RID: 214
		int GetDigestSize();

		// Token: 0x060000D7 RID: 215
		int GetByteLength();

		// Token: 0x060000D8 RID: 216
		void Update(byte input);

		// Token: 0x060000D9 RID: 217
		void BlockUpdate(byte[] input, int inOff, int length);

		// Token: 0x060000DA RID: 218
		int DoFinal(byte[] output, int outOff);

		// Token: 0x060000DB RID: 219
		void Reset();
	}
}
