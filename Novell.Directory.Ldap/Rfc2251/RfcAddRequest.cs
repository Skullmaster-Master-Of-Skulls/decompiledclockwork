using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000BB RID: 187
	public class RfcAddRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00017744 File Offset: 0x00016744
		public virtual RfcAttributeList Attributes
		{
			get
			{
				return (RfcAttributeList)base.get_Renamed(1);
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00017764 File Offset: 0x00016764
		public RfcAddRequest(RfcLdapDN entry, RfcAttributeList attributes) : base(2)
		{
			base.add(entry);
			base.add(attributes);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00017788 File Offset: 0x00016788
		internal RfcAddRequest(Asn1Object[] origRequest, string base_Renamed) : base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000177B4 File Offset: 0x000167B4
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 8);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000177D0 File Offset: 0x000167D0
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcAddRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000177F0 File Offset: 0x000167F0
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
