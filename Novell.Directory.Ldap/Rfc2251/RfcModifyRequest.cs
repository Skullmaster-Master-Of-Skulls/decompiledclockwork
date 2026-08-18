using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000DE RID: 222
	public class RfcModifyRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001A7F4 File Offset: 0x000197F4
		public virtual Asn1SequenceOf Modifications
		{
			get
			{
				return (Asn1SequenceOf)base.get_Renamed(1);
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001A814 File Offset: 0x00019814
		public RfcModifyRequest(RfcLdapDN object_Renamed, Asn1SequenceOf modification) : base(2)
		{
			base.add(object_Renamed);
			base.add(modification);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001A838 File Offset: 0x00019838
		internal RfcModifyRequest(Asn1Object[] origRequest, string base_Renamed) : base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001A864 File Offset: 0x00019864
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 6);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001A880 File Offset: 0x00019880
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcModifyRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001A8A0 File Offset: 0x000198A0
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
