using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000002 RID: 2
	public interface IX509Extension
	{
		// Token: 0x06000001 RID: 1
		ISet GetCriticalExtensionOids();

		// Token: 0x06000002 RID: 2
		ISet GetNonCriticalExtensionOids();

		// Token: 0x06000003 RID: 3
		[Obsolete("Use version taking a DerObjectIdentifier instead")]
		Asn1OctetString GetExtensionValue(string oid);

		// Token: 0x06000004 RID: 4
		Asn1OctetString GetExtensionValue(DerObjectIdentifier oid);
	}
}
