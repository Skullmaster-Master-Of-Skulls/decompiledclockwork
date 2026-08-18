using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200004A RID: 74
	public interface LdapUnsolicitedNotificationListener
	{
		// Token: 0x060002CB RID: 715
		void messageReceived(LdapExtendedResponse msg);
	}
}
