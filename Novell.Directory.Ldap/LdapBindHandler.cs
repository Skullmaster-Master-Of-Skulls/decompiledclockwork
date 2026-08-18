using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000024 RID: 36
	public interface LdapBindHandler : LdapReferralHandler
	{
		// Token: 0x0600015B RID: 347
		LdapConnection Bind(string[] ldapurl, LdapConnection conn);
	}
}
