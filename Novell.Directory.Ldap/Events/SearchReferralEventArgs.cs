using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x02000095 RID: 149
	public class SearchReferralEventArgs : LdapEventArgs
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x00015810 File Offset: 0x00014810
		public SearchReferralEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification, LdapEventType aType) : base(sourceMessage, EventClassifiers.CLASSIFICATION_LDAP_PSEARCH, LdapEventType.LDAP_PSEARCH_ANY)
		{
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00015828 File Offset: 0x00014828
		public string[] getUrls()
		{
			return ((LdapSearchResultReference)this.ldap_message).Referrals;
		}
	}
}
