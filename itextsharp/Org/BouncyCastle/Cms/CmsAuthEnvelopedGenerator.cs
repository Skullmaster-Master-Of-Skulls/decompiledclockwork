using System;
using Org.BouncyCastle.Asn1.Nist;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200035A RID: 858
	internal class CmsAuthEnvelopedGenerator
	{
		// Token: 0x04001551 RID: 5457
		public static readonly string Aes128Ccm = NistObjectIdentifiers.IdAes128Ccm.Id;

		// Token: 0x04001552 RID: 5458
		public static readonly string Aes192Ccm = NistObjectIdentifiers.IdAes192Ccm.Id;

		// Token: 0x04001553 RID: 5459
		public static readonly string Aes256Ccm = NistObjectIdentifiers.IdAes256Ccm.Id;

		// Token: 0x04001554 RID: 5460
		public static readonly string Aes128Gcm = NistObjectIdentifiers.IdAes128Gcm.Id;

		// Token: 0x04001555 RID: 5461
		public static readonly string Aes192Gcm = NistObjectIdentifiers.IdAes192Gcm.Id;

		// Token: 0x04001556 RID: 5462
		public static readonly string Aes256Gcm = NistObjectIdentifiers.IdAes256Gcm.Id;
	}
}
