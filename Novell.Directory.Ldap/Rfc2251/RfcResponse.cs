using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000BC RID: 188
	public interface RfcResponse
	{
		// Token: 0x060004E3 RID: 1251
		Asn1Enumerated getResultCode();

		// Token: 0x060004E4 RID: 1252
		RfcLdapDN getMatchedDN();

		// Token: 0x060004E5 RID: 1253
		RfcLdapString getErrorMessage();

		// Token: 0x060004E6 RID: 1254
		RfcReferral getReferral();
	}
}
