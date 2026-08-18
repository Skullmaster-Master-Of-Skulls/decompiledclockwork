using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000D0 RID: 208
	public class RfcDelResponse : RfcLdapResult
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x00018310 File Offset: 0x00017310
		[CLSCompliant(false)]
		public RfcDelResponse(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00018328 File Offset: 0x00017328
		public RfcDelResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral) : base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00018344 File Offset: 0x00017344
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 11);
		}
	}
}
