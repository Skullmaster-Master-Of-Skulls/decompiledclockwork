using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200011A RID: 282
	public interface ISigner
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A83 RID: 2691
		string AlgorithmName { get; }

		// Token: 0x06000A84 RID: 2692
		void Init(bool forSigning, ICipherParameters parameters);

		// Token: 0x06000A85 RID: 2693
		void Update(byte input);

		// Token: 0x06000A86 RID: 2694
		void BlockUpdate(byte[] input, int inOff, int length);

		// Token: 0x06000A87 RID: 2695
		byte[] GenerateSignature();

		// Token: 0x06000A88 RID: 2696
		bool VerifySignature(byte[] signature);

		// Token: 0x06000A89 RID: 2697
		void Reset();
	}
}
