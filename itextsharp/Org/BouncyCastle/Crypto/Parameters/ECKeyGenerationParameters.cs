using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000241 RID: 577
	public class ECKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06001654 RID: 5716 RVA: 0x000823D8 File Offset: 0x000813D8
		public ECKeyGenerationParameters(ECDomainParameters domainParameters, SecureRandom random) : base(random, domainParameters.N.BitLength)
		{
			this.domainParams = domainParameters;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x000823F3 File Offset: 0x000813F3
		public ECKeyGenerationParameters(DerObjectIdentifier publicKeyParamSet, SecureRandom random) : this(ECKeyParameters.LookupParameters(publicKeyParamSet), random)
		{
			this.publicKeyParamSet = publicKeyParamSet;
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x00082409 File Offset: 0x00081409
		public ECDomainParameters DomainParameters
		{
			get
			{
				return this.domainParams;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x00082411 File Offset: 0x00081411
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x04000F51 RID: 3921
		private readonly ECDomainParameters domainParams;

		// Token: 0x04000F52 RID: 3922
		private readonly DerObjectIdentifier publicKeyParamSet;
	}
}
