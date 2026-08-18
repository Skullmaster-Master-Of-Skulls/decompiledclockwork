using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002B RID: 43
	public class LdapDeleteRequest : LdapMessage
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000A0D0 File Offset: 0x000090D0
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000A0EC File Offset: 0x000090EC
		public LdapDeleteRequest(string dn, LdapControl[] cont) : base(10, new RfcDelRequest(dn), cont)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000A10C File Offset: 0x0000910C
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
