using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000079 RID: 121
	public class Pkcs12StoreBuilder
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x000152AC File Offset: 0x000142AC
		public Pkcs12Store Build()
		{
			return new Pkcs12Store(this.keyAlgorithm, this.certAlgorithm, this.useDerEncoding);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000152C5 File Offset: 0x000142C5
		public Pkcs12StoreBuilder SetCertAlgorithm(DerObjectIdentifier certAlgorithm)
		{
			this.certAlgorithm = certAlgorithm;
			return this;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000152CF File Offset: 0x000142CF
		public Pkcs12StoreBuilder SetKeyAlgorithm(DerObjectIdentifier keyAlgorithm)
		{
			this.keyAlgorithm = keyAlgorithm;
			return this;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000152D9 File Offset: 0x000142D9
		public Pkcs12StoreBuilder SetUseDerEncoding(bool useDerEncoding)
		{
			this.useDerEncoding = useDerEncoding;
			return this;
		}

		// Token: 0x04000208 RID: 520
		private DerObjectIdentifier keyAlgorithm = PkcsObjectIdentifiers.PbeWithShaAnd3KeyTripleDesCbc;

		// Token: 0x04000209 RID: 521
		private DerObjectIdentifier certAlgorithm = PkcsObjectIdentifiers.PbewithShaAnd40BitRC2Cbc;

		// Token: 0x0400020A RID: 522
		private bool useDerEncoding;
	}
}
