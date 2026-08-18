using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000E4 RID: 228
	public class RfcSearchResultDone : RfcLdapResult
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x0001AA90 File Offset: 0x00019A90
		[CLSCompliant(false)]
		public RfcSearchResultDone(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001AAA8 File Offset: 0x00019AA8
		public RfcSearchResultDone(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral) : base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001AAC4 File Offset: 0x00019AC4
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 5);
		}
	}
}
