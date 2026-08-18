using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000CB RID: 203
	public class RfcCompareResponse : RfcLdapResult
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x00017FC4 File Offset: 0x00016FC4
		[CLSCompliant(false)]
		public RfcCompareResponse(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00017FDC File Offset: 0x00016FDC
		public RfcCompareResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral) : base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00017FF8 File Offset: 0x00016FF8
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 15);
		}
	}
}
