using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000023 RID: 35
	public class LdapAuthProvider
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000791C File Offset: 0x0000691C
		public virtual string DN
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00007934 File Offset: 0x00006934
		[CLSCompliant(false)]
		public virtual sbyte[] Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000794C File Offset: 0x0000694C
		[CLSCompliant(false)]
		public LdapAuthProvider(string dn, sbyte[] password)
		{
			this.dn = dn;
			this.password = password;
		}

		// Token: 0x040000C0 RID: 192
		private string dn;

		// Token: 0x040000C1 RID: 193
		private sbyte[] password;
	}
}
