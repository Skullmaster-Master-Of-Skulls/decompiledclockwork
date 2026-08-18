using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x0200008E RID: 142
	public class DirectoryExceptionEventArgs : BaseEventArgs
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00015410 File Offset: 0x00014410
		public LdapException LdapExceptionObject
		{
			get
			{
				return this.ldap_exception_object;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00015428 File Offset: 0x00014428
		public DirectoryExceptionEventArgs(LdapMessage message, LdapException ldapException) : base(message)
		{
			this.ldap_exception_object = ldapException;
		}

		// Token: 0x0400032F RID: 815
		protected LdapException ldap_exception_object;
	}
}
