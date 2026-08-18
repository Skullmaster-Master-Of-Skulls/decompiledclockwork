using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000E8 RID: 232
	public class RfcUnbindRequest : Asn1Null, RfcRequest
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x0001ABC4 File Offset: 0x00019BC4
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, false, 2);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001ABE0 File Offset: 0x00019BE0
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			throw new LdapException("NO_DUP_REQUEST", new object[]
			{
				"unbind"
			}, 92, null);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001AC0C File Offset: 0x00019C0C
		public string getRequestDN()
		{
			return null;
		}
	}
}
