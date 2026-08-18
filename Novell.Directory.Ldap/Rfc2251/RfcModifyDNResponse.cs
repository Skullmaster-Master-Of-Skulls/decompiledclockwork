using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000DD RID: 221
	public class RfcModifyDNResponse : RfcLdapResult
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x0001A7A4 File Offset: 0x000197A4
		[CLSCompliant(false)]
		public RfcModifyDNResponse(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001A7BC File Offset: 0x000197BC
		public RfcModifyDNResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral) : base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001A7D8 File Offset: 0x000197D8
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 13);
		}
	}
}
