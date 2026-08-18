using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000036 RID: 54
	public class LdapLocalException : LdapException
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0000BA10 File Offset: 0x0000AA10
		public LdapLocalException()
		{
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000BA28 File Offset: 0x0000AA28
		public LdapLocalException(string messageOrKey, int resultCode) : base(messageOrKey, resultCode, null)
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000BA40 File Offset: 0x0000AA40
		public LdapLocalException(string messageOrKey, object[] arguments, int resultCode) : base(messageOrKey, arguments, resultCode, null)
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000BA5C File Offset: 0x0000AA5C
		public LdapLocalException(string messageOrKey, int resultCode, Exception rootException) : base(messageOrKey, resultCode, null, rootException)
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000BA78 File Offset: 0x0000AA78
		public LdapLocalException(string messageOrKey, object[] arguments, int resultCode, Exception rootException) : base(messageOrKey, arguments, resultCode, null, rootException)
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000BA94 File Offset: 0x0000AA94
		public override string ToString()
		{
			return this.getExceptionString("LdapLocalException");
		}
	}
}
