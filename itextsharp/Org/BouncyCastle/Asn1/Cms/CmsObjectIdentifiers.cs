using System;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000630 RID: 1584
	public abstract class CmsObjectIdentifiers
	{
		// Token: 0x040023E2 RID: 9186
		public static readonly DerObjectIdentifier Data = PkcsObjectIdentifiers.Data;

		// Token: 0x040023E3 RID: 9187
		public static readonly DerObjectIdentifier SignedData = PkcsObjectIdentifiers.SignedData;

		// Token: 0x040023E4 RID: 9188
		public static readonly DerObjectIdentifier EnvelopedData = PkcsObjectIdentifiers.EnvelopedData;

		// Token: 0x040023E5 RID: 9189
		public static readonly DerObjectIdentifier SignedAndEnvelopedData = PkcsObjectIdentifiers.SignedAndEnvelopedData;

		// Token: 0x040023E6 RID: 9190
		public static readonly DerObjectIdentifier DigestedData = PkcsObjectIdentifiers.DigestedData;

		// Token: 0x040023E7 RID: 9191
		public static readonly DerObjectIdentifier EncryptedData = PkcsObjectIdentifiers.EncryptedData;

		// Token: 0x040023E8 RID: 9192
		public static readonly DerObjectIdentifier AuthenticatedData = PkcsObjectIdentifiers.IdCTAuthData;

		// Token: 0x040023E9 RID: 9193
		public static readonly DerObjectIdentifier CompressedData = PkcsObjectIdentifiers.IdCTCompressedData;

		// Token: 0x040023EA RID: 9194
		public static readonly DerObjectIdentifier AuthEnvelopedData = PkcsObjectIdentifiers.IdCTAuthEnvelopedData;
	}
}
