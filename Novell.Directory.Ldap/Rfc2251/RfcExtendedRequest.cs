using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000D1 RID: 209
	public class RfcExtendedRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x00018360 File Offset: 0x00017360
		public RfcExtendedRequest(RfcLdapOID requestName) : this(requestName, null)
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00018378 File Offset: 0x00017378
		public RfcExtendedRequest(RfcLdapOID requestName, Asn1OctetString requestValue) : base(2)
		{
			base.add(new Asn1Tagged(new Asn1Identifier(2, false, 0), requestName, false));
			if (requestValue != null)
			{
				base.add(new Asn1Tagged(new Asn1Identifier(2, false, 1), requestValue, false));
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000183BC File Offset: 0x000173BC
		public RfcExtendedRequest(Asn1Object[] origRequest) : base(origRequest, origRequest.Length)
		{
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000183D8 File Offset: 0x000173D8
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 23);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000183F4 File Offset: 0x000173F4
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcExtendedRequest(base.toArray());
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00018410 File Offset: 0x00017410
		public string getRequestDN()
		{
			return null;
		}

		// Token: 0x040003FD RID: 1021
		public const int REQUEST_NAME = 0;

		// Token: 0x040003FE RID: 1022
		public const int REQUEST_VALUE = 1;
	}
}
