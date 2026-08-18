using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020001EA RID: 490
	public interface IDsa
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001330 RID: 4912
		string AlgorithmName { get; }

		// Token: 0x06001331 RID: 4913
		void Init(bool forSigning, ICipherParameters parameters);

		// Token: 0x06001332 RID: 4914
		BigInteger[] GenerateSignature(byte[] message);

		// Token: 0x06001333 RID: 4915
		bool VerifySignature(byte[] message, BigInteger r, BigInteger s);
	}
}
