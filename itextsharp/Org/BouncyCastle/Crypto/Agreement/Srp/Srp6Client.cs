using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Agreement.Srp
{
	// Token: 0x0200024F RID: 591
	public class Srp6Client
	{
		// Token: 0x06001699 RID: 5785 RVA: 0x000832A7 File Offset: 0x000822A7
		public virtual void Init(BigInteger N, BigInteger g, IDigest digest, SecureRandom random)
		{
			this.N = N;
			this.g = g;
			this.digest = digest;
			this.random = random;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000832C8 File Offset: 0x000822C8
		public virtual BigInteger GenerateClientCredentials(byte[] salt, byte[] identity, byte[] password)
		{
			this.x = Srp6Utilities.CalculateX(this.digest, this.N, salt, identity, password);
			this.privA = this.SelectPrivateValue();
			this.pubA = this.g.ModPow(this.privA, this.N);
			return this.pubA;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00083320 File Offset: 0x00082320
		public virtual BigInteger CalculateSecret(BigInteger serverB)
		{
			this.B = Srp6Utilities.ValidatePublicValue(this.N, serverB);
			this.u = Srp6Utilities.CalculateU(this.digest, this.N, this.pubA, this.B);
			this.S = this.CalculateS();
			return this.S;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00083374 File Offset: 0x00082374
		protected virtual BigInteger SelectPrivateValue()
		{
			return Srp6Utilities.GeneratePrivateValue(this.digest, this.N, this.g, this.random);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00083394 File Offset: 0x00082394
		private BigInteger CalculateS()
		{
			BigInteger val = Srp6Utilities.CalculateK(this.digest, this.N, this.g);
			BigInteger exponent = this.u.Multiply(this.x).Add(this.privA);
			BigInteger n = this.g.ModPow(this.x, this.N).Multiply(val).Mod(this.N);
			return this.B.Subtract(n).Mod(this.N).ModPow(exponent, this.N);
		}

		// Token: 0x04000F72 RID: 3954
		protected BigInteger N;

		// Token: 0x04000F73 RID: 3955
		protected BigInteger g;

		// Token: 0x04000F74 RID: 3956
		protected BigInteger privA;

		// Token: 0x04000F75 RID: 3957
		protected BigInteger pubA;

		// Token: 0x04000F76 RID: 3958
		protected BigInteger B;

		// Token: 0x04000F77 RID: 3959
		protected BigInteger x;

		// Token: 0x04000F78 RID: 3960
		protected BigInteger u;

		// Token: 0x04000F79 RID: 3961
		protected BigInteger S;

		// Token: 0x04000F7A RID: 3962
		protected IDigest digest;

		// Token: 0x04000F7B RID: 3963
		protected SecureRandom random;
	}
}
