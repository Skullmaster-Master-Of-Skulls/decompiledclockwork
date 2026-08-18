using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000E3 RID: 227
	public class RfcSearchRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x0600059F RID: 1439 RVA: 0x0001A980 File Offset: 0x00019980
		public RfcSearchRequest(RfcLdapDN baseObject, Asn1Enumerated scope, Asn1Enumerated derefAliases, Asn1Integer sizeLimit, Asn1Integer timeLimit, Asn1Boolean typesOnly, RfcFilter filter, RfcAttributeDescriptionList attributes) : base(8)
		{
			base.add(baseObject);
			base.add(scope);
			base.add(derefAliases);
			base.add(sizeLimit);
			base.add(timeLimit);
			base.add(typesOnly);
			base.add(filter);
			base.add(attributes);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001A9D4 File Offset: 0x000199D4
		internal RfcSearchRequest(Asn1Object[] origRequest, string base_Renamed, string filter, bool request) : base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
			if (request)
			{
				int num = ((Asn1Enumerated)origRequest[1]).intValue();
				if (num == 1)
				{
					base.set_Renamed(1, new Asn1Enumerated(0));
				}
			}
			if (filter != null)
			{
				base.set_Renamed(6, new RfcFilter(filter));
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001AA30 File Offset: 0x00019A30
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 3);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001AA4C File Offset: 0x00019A4C
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcSearchRequest(base.toArray(), base_Renamed, filter, request);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001AA6C File Offset: 0x00019A6C
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
