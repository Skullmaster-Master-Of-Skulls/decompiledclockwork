using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200054A RID: 1354
	public class Gost3410KeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002E93 RID: 11923 RVA: 0x0011FB4F File Offset: 0x0011EB4F
		public Gost3410KeyGenerationParameters(SecureRandom random, Gost3410Parameters parameters) : base(random, parameters.P.BitLength - 1)
		{
			this.parameters = parameters;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x0011FB6C File Offset: 0x0011EB6C
		public Gost3410KeyGenerationParameters(SecureRandom random, DerObjectIdentifier publicKeyParamSet) : this(random, Gost3410KeyGenerationParameters.LookupParameters(publicKeyParamSet))
		{
			this.publicKeyParamSet = publicKeyParamSet;
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x0011FB82 File Offset: 0x0011EB82
		public Gost3410Parameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x0011FB8A File Offset: 0x0011EB8A
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x0011FB94 File Offset: 0x0011EB94
		private static Gost3410Parameters LookupParameters(DerObjectIdentifier publicKeyParamSet)
		{
			if (publicKeyParamSet == null)
			{
				throw new ArgumentNullException("publicKeyParamSet");
			}
			Gost3410ParamSetParameters byOid = Gost3410NamedParameters.GetByOid(publicKeyParamSet);
			if (byOid == null)
			{
				throw new ArgumentException("OID is not a valid CryptoPro public key parameter set", "publicKeyParamSet");
			}
			return new Gost3410Parameters(byOid.P, byOid.Q, byOid.A);
		}

		// Token: 0x04002011 RID: 8209
		private readonly Gost3410Parameters parameters;

		// Token: 0x04002012 RID: 8210
		private readonly DerObjectIdentifier publicKeyParamSet;
	}
}
