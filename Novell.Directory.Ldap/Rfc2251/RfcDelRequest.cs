using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000CF RID: 207
	public class RfcDelRequest : RfcLdapDN, RfcRequest
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0001828C File Offset: 0x0001728C
		public RfcDelRequest(string dn) : base(dn)
		{
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000182A0 File Offset: 0x000172A0
		[CLSCompliant(false)]
		public RfcDelRequest(sbyte[] dn) : base(dn)
		{
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000182B4 File Offset: 0x000172B4
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, false, 10);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000182D0 File Offset: 0x000172D0
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			RfcRequest result;
			if (base_Renamed == null)
			{
				result = new RfcDelRequest(base.byteValue());
			}
			else
			{
				result = new RfcDelRequest(base_Renamed);
			}
			return result;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000182F8 File Offset: 0x000172F8
		public string getRequestDN()
		{
			return base.stringValue();
		}
	}
}
