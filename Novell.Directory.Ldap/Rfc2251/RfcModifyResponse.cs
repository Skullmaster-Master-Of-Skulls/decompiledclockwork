using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000DF RID: 223
	public class RfcModifyResponse : RfcLdapResult
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x0001A8C4 File Offset: 0x000198C4
		[CLSCompliant(false)]
		public RfcModifyResponse(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001A8DC File Offset: 0x000198DC
		public RfcModifyResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral) : base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001A8F8 File Offset: 0x000198F8
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 7);
		}
	}
}
