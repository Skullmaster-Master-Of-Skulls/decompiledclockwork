using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000025 RID: 37
	public class LdapBindRequest : LdapMessage
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00007970 File Offset: 0x00006970
		public virtual string AuthenticationDN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000798C File Offset: 0x0000698C
		[CLSCompliant(false)]
		public LdapBindRequest(int version, string dn, sbyte[] passwd, LdapControl[] cont) : base(0, new RfcBindRequest(new Asn1Integer(version), new RfcLdapDN(dn), new RfcAuthenticationChoice(new Asn1Tagged(new Asn1Identifier(2, false, 0), new Asn1OctetString(passwd), false))), cont)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000079D0 File Offset: 0x000069D0
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
