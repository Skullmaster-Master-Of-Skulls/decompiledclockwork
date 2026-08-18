using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000548 RID: 1352
	public class RsaKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002E89 RID: 11913 RVA: 0x0011FA4B File Offset: 0x0011EA4B
		public RsaKeyGenerationParameters(BigInteger publicExponent, SecureRandom random, int strength, int certainty) : base(random, strength)
		{
			this.publicExponent = publicExponent;
			this.certainty = certainty;
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x0011FA64 File Offset: 0x0011EA64
		public BigInteger PublicExponent
		{
			get
			{
				return this.publicExponent;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x0011FA6C File Offset: 0x0011EA6C
		public int Certainty
		{
			get
			{
				return this.certainty;
			}
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x0011FA74 File Offset: 0x0011EA74
		public override bool Equals(object obj)
		{
			RsaKeyGenerationParameters rsaKeyGenerationParameters = obj as RsaKeyGenerationParameters;
			return rsaKeyGenerationParameters != null && this.certainty == rsaKeyGenerationParameters.certainty && this.publicExponent.Equals(rsaKeyGenerationParameters.publicExponent);
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x0011FAB0 File Offset: 0x0011EAB0
		public override int GetHashCode()
		{
			return this.certainty.GetHashCode() ^ this.publicExponent.GetHashCode();
		}

		// Token: 0x0400200C RID: 8204
		private readonly BigInteger publicExponent;

		// Token: 0x0400200D RID: 8205
		private readonly int certainty;
	}
}
