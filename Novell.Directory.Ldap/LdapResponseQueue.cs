using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000040 RID: 64
	public class LdapResponseQueue : LdapMessageQueue
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000D328 File Offset: 0x0000C328
		internal LdapResponseQueue(MessageAgent agent) : base("LdapResponseQueue", agent)
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000D344 File Offset: 0x0000C344
		public virtual void merge(LdapMessageQueue queue2)
		{
			LdapResponseQueue ldapResponseQueue = (LdapResponseQueue)queue2;
			this.agent.merge(ldapResponseQueue.MessageAgent);
		}
	}
}
