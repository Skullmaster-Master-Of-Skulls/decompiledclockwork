using System;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x02000068 RID: 104
	public class LdapSortKey
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0001238C File Offset: 0x0001138C
		public virtual string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000123A4 File Offset: 0x000113A4
		public virtual bool Reverse
		{
			get
			{
				return this.reverse;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003BB RID: 955 RVA: 0x000123BC File Offset: 0x000113BC
		public virtual string MatchRule
		{
			get
			{
				return this.matchRule;
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000123D4 File Offset: 0x000113D4
		public LdapSortKey(string keyDescription)
		{
			this.matchRule = null;
			this.reverse = false;
			string text = keyDescription;
			if (text[0] == '-')
			{
				text = text.Substring(1);
				this.reverse = true;
			}
			int num = text.IndexOf(":");
			if (num != -1)
			{
				this.key = text.Substring(0, num);
				this.matchRule = text.Substring(num + 1);
			}
			else
			{
				this.key = text;
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001244C File Offset: 0x0001144C
		public LdapSortKey(string key, bool reverse) : this(key, reverse, null)
		{
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00012464 File Offset: 0x00011464
		public LdapSortKey(string key, bool reverse, string matchRule)
		{
			this.key = key;
			this.reverse = reverse;
			this.matchRule = matchRule;
		}

		// Token: 0x040001BE RID: 446
		private string key;

		// Token: 0x040001BF RID: 447
		private bool reverse;

		// Token: 0x040001C0 RID: 448
		private string matchRule;
	}
}
