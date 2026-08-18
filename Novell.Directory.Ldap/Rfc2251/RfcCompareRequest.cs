using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000CA RID: 202
	public class RfcCompareRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00017EE0 File Offset: 0x00016EE0
		public virtual RfcAttributeValueAssertion AttributeValueAssertion
		{
			get
			{
				return (RfcAttributeValueAssertion)base.get_Renamed(1);
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00017F00 File Offset: 0x00016F00
		public RfcCompareRequest(RfcLdapDN entry, RfcAttributeValueAssertion ava) : base(2)
		{
			base.add(entry);
			base.add(ava);
			if (ava.AssertionValue == null)
			{
				throw new ArgumentException("compare: Attribute must have an assertion value");
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00017F38 File Offset: 0x00016F38
		internal RfcCompareRequest(Asn1Object[] origRequest, string base_Renamed) : base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00017F64 File Offset: 0x00016F64
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 14);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00017F80 File Offset: 0x00016F80
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcCompareRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00017FA0 File Offset: 0x00016FA0
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
