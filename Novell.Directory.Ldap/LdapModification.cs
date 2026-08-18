using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003A RID: 58
	public class LdapModification
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000C194 File Offset: 0x0000B194
		public virtual LdapAttribute Attribute
		{
			get
			{
				return this.attr;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000C1AC File Offset: 0x0000B1AC
		public virtual int Op
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000C1C4 File Offset: 0x0000B1C4
		public LdapModification(int op, LdapAttribute attr)
		{
			this.op = op;
			this.attr = attr;
		}

		// Token: 0x04000116 RID: 278
		public const int ADD = 0;

		// Token: 0x04000117 RID: 279
		public const int DELETE = 1;

		// Token: 0x04000118 RID: 280
		public const int REPLACE = 2;

		// Token: 0x04000119 RID: 281
		private int op;

		// Token: 0x0400011A RID: 282
		private LdapAttribute attr;
	}
}
