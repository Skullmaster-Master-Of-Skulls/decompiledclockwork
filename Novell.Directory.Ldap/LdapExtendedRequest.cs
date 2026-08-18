using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000032 RID: 50
	public class LdapExtendedRequest : LdapMessage
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000B278 File Offset: 0x0000A278
		public virtual LdapExtendedOperation ExtendedOperation
		{
			get
			{
				RfcExtendedRequest rfcExtendedRequest = (RfcExtendedRequest)this.Asn1Object.get_Renamed(1);
				Asn1Tagged asn1Tagged = (Asn1Tagged)rfcExtendedRequest.get_Renamed(0);
				RfcLdapOID rfcLdapOID = (RfcLdapOID)asn1Tagged.taggedValue();
				string oid = rfcLdapOID.stringValue();
				sbyte[] vals = null;
				if (rfcExtendedRequest.size() >= 2)
				{
					asn1Tagged = (Asn1Tagged)rfcExtendedRequest.get_Renamed(1);
					Asn1OctetString asn1OctetString = (Asn1OctetString)asn1Tagged.taggedValue();
					vals = asn1OctetString.byteValue();
				}
				return new LdapExtendedOperation(oid, vals);
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000B2F4 File Offset: 0x0000A2F4
		public LdapExtendedRequest(LdapExtendedOperation op, LdapControl[] cont) : base(23, new RfcExtendedRequest(new RfcLdapOID(op.getID()), (op.getValue() != null) ? new Asn1OctetString(op.getValue()) : null), cont)
		{
		}
	}
}
