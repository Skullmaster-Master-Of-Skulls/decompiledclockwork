using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000BE RID: 190
	public class RfcAddResponse : RfcLdapResult
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x00017970 File Offset: 0x00016970
		[CLSCompliant(false)]
		public RfcAddResponse(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00017988 File Offset: 0x00016988
		public RfcAddResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral) : base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000179A4 File Offset: 0x000169A4
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 9);
		}
	}
}
