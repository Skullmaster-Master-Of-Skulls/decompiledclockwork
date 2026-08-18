using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Agreement.Srp
{
	// Token: 0x02000353 RID: 851
	public class Srp6Server
	{
		// Token: 0x06001EB5 RID: 7861 RVA: 0x000B9494 File Offset: 0x000B8494
		public virtual void Init(BigInteger N, BigInteger g, BigInteger v, IDigest digest, SecureRandom random)
		{
			this.N = N;
			this.g = g;
			this.v = v;
			this.random = random;
			this.digest = digest;
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x000B94BC File Offset: 0x000B84BC
		public virtual BigInteger GenerateServerCredentials()
		{
			BigInteger bigInteger = Srp6Utilities.CalculateK(this.digest, this.N, this.g);
			this.privB = this.SelectPrivateValue();
			this.pubB = bigInteger.Multiply(this.v).Mod(this.N).Add(this.g.ModPow(this.privB, this.N)).Mod(this.N);
			return this.pubB;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000B9538 File Offset: 0x000B8538
		public virtual BigInteger CalculateSecret(BigInteger clientA)
		{
			this.A = Srp6Utilities.ValidatePublicValue(this.N, clientA);
			this.u = Srp6Utilities.CalculateU(this.digest, this.N, this.A, this.pubB);
			this.S = this.CalculateS();
			return this.S;
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x000B958C File Offset: 0x000B858C
		protected virtual BigInteger SelectPrivateValue()
		{
			return Srp6Utilities.GeneratePrivateValue(this.digest, this.N, this.g, this.random);
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000B95AB File Offset: 0x000B85AB
		private BigInteger CalculateS()
		{
			return this.v.ModPow(this.u, this.N).Multiply(this.A).Mod(this.N).ModPow(this.privB, this.N);
		}

		// Token: 0x0400153A RID: 5434
		protected BigInteger N;

		// Token: 0x0400153B RID: 5435
		protected BigInteger g;

		// Token: 0x0400153C RID: 5436
		protected BigInteger v;

		// Token: 0x0400153D RID: 5437
		protected SecureRandom random;

		// Token: 0x0400153E RID: 5438
		protected IDigest digest;

		// Token: 0x0400153F RID: 5439
		protected BigInteger A;

		// Token: 0x04001540 RID: 5440
		protected BigInteger privB;

		// Token: 0x04001541 RID: 5441
		protected BigInteger pubB;

		// Token: 0x04001542 RID: 5442
		protected BigInteger u;

		// Token: 0x04001543 RID: 5443
		protected BigInteger S;
	}
}
