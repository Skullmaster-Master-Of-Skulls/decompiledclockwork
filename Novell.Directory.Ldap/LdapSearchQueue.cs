using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000043 RID: 67
	public class LdapSearchQueue : LdapMessageQueue
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x0000DE38 File Offset: 0x0000CE38
		internal LdapSearchQueue(MessageAgent agent) : base("LdapSearchQueue", agent)
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000DE54 File Offset: 0x0000CE54
		public virtual void merge(LdapMessageQueue queue2)
		{
			LdapSearchQueue ldapSearchQueue = (LdapSearchQueue)queue2;
			this.agent.merge(ldapSearchQueue.MessageAgent);
		}
	}
}
