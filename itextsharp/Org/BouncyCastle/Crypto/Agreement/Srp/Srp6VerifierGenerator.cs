using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Agreement.Srp
{
	// Token: 0x0200050D RID: 1293
	public class Srp6VerifierGenerator
	{
		// Token: 0x06002C33 RID: 11315 RVA: 0x0010D498 File Offset: 0x0010C498
		public virtual void Init(BigInteger N, BigInteger g, IDigest digest)
		{
			this.N = N;
			this.g = g;
			this.digest = digest;
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x0010D4B0 File Offset: 0x0010C4B0
		public virtual BigInteger GenerateVerifier(byte[] salt, byte[] identity, byte[] password)
		{
			BigInteger exponent = Srp6Utilities.CalculateX(this.digest, this.N, salt, identity, password);
			return this.g.ModPow(exponent, this.N);
		}

		// Token: 0x04001E75 RID: 7797
		protected BigInteger N;

		// Token: 0x04001E76 RID: 7798
		protected BigInteger g;

		// Token: 0x04001E77 RID: 7799
		protected IDigest digest;
	}
}
