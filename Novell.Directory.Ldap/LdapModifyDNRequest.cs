using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003B RID: 59
	public class LdapModifyDNRequest : LdapMessage
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000C1E8 File Offset: 0x0000B1E8
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000C204 File Offset: 0x0000B204
		public virtual string NewRDN
		{
			get
			{
				RfcModifyDNRequest rfcModifyDNRequest = (RfcModifyDNRequest)this.Asn1Object.getRequest();
				RfcRelativeLdapDN rfcRelativeLdapDN = (RfcRelativeLdapDN)rfcModifyDNRequest.toArray()[1];
				return rfcRelativeLdapDN.stringValue();
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000C23C File Offset: 0x0000B23C
		public virtual bool DeleteOldRDN
		{
			get
			{
				RfcModifyDNRequest rfcModifyDNRequest = (RfcModifyDNRequest)this.Asn1Object.getRequest();
				Asn1Boolean asn1Boolean = (Asn1Boolean)rfcModifyDNRequest.toArray()[2];
				return asn1Boolean.booleanValue();
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000C274 File Offset: 0x0000B274
		public virtual string ParentDN
		{
			get
			{
				RfcModifyDNRequest rfcModifyDNRequest = (RfcModifyDNRequest)this.Asn1Object.getRequest();
				Asn1Object[] array = rfcModifyDNRequest.toArray();
				string result;
				if (array.Length < 4 || array[3] == null)
				{
					result = null;
				}
				else
				{
					RfcLdapDN rfcLdapDN = (RfcLdapDN)rfcModifyDNRequest.toArray()[3];
					result = rfcLdapDN.stringValue();
				}
				return result;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000C2C0 File Offset: 0x0000B2C0
		public LdapModifyDNRequest(string dn, string newRdn, string newParentdn, bool deleteOldRdn, LdapControl[] cont) : base(12, new RfcModifyDNRequest(new RfcLdapDN(dn), new RfcRelativeLdapDN(newRdn), new Asn1Boolean(deleteOldRdn), (newParentdn != null) ? new RfcLdapDN(newParentdn) : null), cont)
		{
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000C300 File Offset: 0x0000B300
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
