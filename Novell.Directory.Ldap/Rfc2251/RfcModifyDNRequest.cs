using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000DC RID: 220
	public class RfcModifyDNRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x0001A6BC File Offset: 0x000196BC
		public RfcModifyDNRequest(RfcLdapDN entry, RfcRelativeLdapDN newrdn, Asn1Boolean deleteoldrdn) : this(entry, newrdn, deleteoldrdn, null)
		{
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001A6D4 File Offset: 0x000196D4
		public RfcModifyDNRequest(RfcLdapDN entry, RfcRelativeLdapDN newrdn, Asn1Boolean deleteoldrdn, RfcLdapDN newSuperior) : base(4)
		{
			base.add(entry);
			base.add(newrdn);
			base.add(deleteoldrdn);
			if (newSuperior != null)
			{
				newSuperior.setIdentifier(new Asn1Identifier(2, false, 0));
				base.add(newSuperior);
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001A718 File Offset: 0x00019718
		internal RfcModifyDNRequest(Asn1Object[] origRequest, string base_Renamed) : base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001A744 File Offset: 0x00019744
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 12);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001A760 File Offset: 0x00019760
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcModifyDNRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001A780 File Offset: 0x00019780
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
