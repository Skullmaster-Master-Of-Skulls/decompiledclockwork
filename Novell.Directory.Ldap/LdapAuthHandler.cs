using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000022 RID: 34
	public interface LdapAuthHandler : LdapReferralHandler
	{
		// Token: 0x06000157 RID: 343
		LdapAuthProvider getAuthProvider(string host, int port);
	}
}
