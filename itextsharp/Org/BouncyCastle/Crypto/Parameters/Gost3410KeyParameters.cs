using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000120 RID: 288
	public abstract class Gost3410KeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x00037F98 File Offset: 0x00036F98
		protected Gost3410KeyParameters(bool isPrivate, Gost3410Parameters parameters) : base(isPrivate)
		{
			this.parameters = parameters;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00037FA8 File Offset: 0x00036FA8
		protected Gost3410KeyParameters(bool isPrivate, DerObjectIdentifier publicKeyParamSet) : base(isPrivate)
		{
			this.parameters = Gost3410KeyParameters.LookupParameters(publicKeyParamSet);
			this.publicKeyParamSet = publicKeyParamSet;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00037FC4 File Offset: 0x00036FC4
		public Gost3410Parameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00037FCC File Offset: 0x00036FCC
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00037FD4 File Offset: 0x00036FD4
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

		// Token: 0x0400087E RID: 2174
		private readonly Gost3410Parameters parameters;

		// Token: 0x0400087F RID: 2175
		private readonly DerObjectIdentifier publicKeyParamSet;
	}
}
