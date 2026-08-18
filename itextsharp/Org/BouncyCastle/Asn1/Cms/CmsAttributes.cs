using System;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000390 RID: 912
	public abstract class CmsAttributes
	{
		// Token: 0x040015E1 RID: 5601
		public static readonly DerObjectIdentifier ContentType = PkcsObjectIdentifiers.Pkcs9AtContentType;

		// Token: 0x040015E2 RID: 5602
		public static readonly DerObjectIdentifier MessageDigest = PkcsObjectIdentifiers.Pkcs9AtMessageDigest;

		// Token: 0x040015E3 RID: 5603
		public static readonly DerObjectIdentifier SigningTime = PkcsObjectIdentifiers.Pkcs9AtSigningTime;

		// Token: 0x040015E4 RID: 5604
		public static readonly DerObjectIdentifier CounterSignature = PkcsObjectIdentifiers.Pkcs9AtCounterSignature;

		// Token: 0x040015E5 RID: 5605
		public static readonly DerObjectIdentifier ContentHint = PkcsObjectIdentifiers.IdAAContentHint;
	}
}
