using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000049 RID: 73
	public class LdapUnbindRequest : LdapMessage
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0000EA48 File Offset: 0x0000DA48
		public LdapUnbindRequest(LdapControl[] cont) : base(2, new RfcUnbindRequest(), cont)
		{
		}
	}
}
