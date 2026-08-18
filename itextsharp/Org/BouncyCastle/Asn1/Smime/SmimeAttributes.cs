using System;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x02000208 RID: 520
	public abstract class SmimeAttributes
	{
		// Token: 0x04000DCB RID: 3531
		public static readonly DerObjectIdentifier SmimeCapabilities = PkcsObjectIdentifiers.Pkcs9AtSmimeCapabilities;

		// Token: 0x04000DCC RID: 3532
		public static readonly DerObjectIdentifier EncrypKeyPref = PkcsObjectIdentifiers.IdAAEncrypKeyPref;
	}
}
