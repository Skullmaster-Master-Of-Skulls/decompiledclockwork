using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x0200007A RID: 122
	public class BaseEventArgs : EventArgs
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00014718 File Offset: 0x00013718
		public LdapMessage ContianedEventInformation
		{
			get
			{
				return this.ldap_message;
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00014730 File Offset: 0x00013730
		public BaseEventArgs(LdapMessage message)
		{
			this.ldap_message = message;
		}

		// Token: 0x04000215 RID: 533
		protected LdapMessage ldap_message;
	}
}
