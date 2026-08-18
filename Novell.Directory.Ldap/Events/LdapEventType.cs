using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x02000091 RID: 145
	public enum LdapEventType
	{
		// Token: 0x04000336 RID: 822
		TYPE_UNKNOWN = -1,
		// Token: 0x04000337 RID: 823
		LDAP_PSEARCH_ADD = 1,
		// Token: 0x04000338 RID: 824
		LDAP_PSEARCH_DELETE,
		// Token: 0x04000339 RID: 825
		LDAP_PSEARCH_MODIFY = 4,
		// Token: 0x0400033A RID: 826
		LDAP_PSEARCH_MODDN = 8,
		// Token: 0x0400033B RID: 827
		LDAP_PSEARCH_ANY = 15
	}
}
