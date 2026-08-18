using System;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000F4 RID: 244
	public class ReferralInfo
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001D128 File Offset: 0x0001C128
		public virtual LdapUrl ReferralUrl
		{
			get
			{
				return this.referralUrl;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001D140 File Offset: 0x0001C140
		public virtual LdapConnection ReferralConnection
		{
			get
			{
				return this.conn;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001D158 File Offset: 0x0001C158
		public virtual string[] ReferralList
		{
			get
			{
				return this.referralList;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001D170 File Offset: 0x0001C170
		public ReferralInfo(LdapConnection lc, string[] refList, LdapUrl refUrl)
		{
			this.conn = lc;
			this.referralUrl = refUrl;
			this.referralList = refList;
		}

		// Token: 0x04000489 RID: 1161
		private LdapConnection conn;

		// Token: 0x0400048A RID: 1162
		private LdapUrl referralUrl;

		// Token: 0x0400048B RID: 1163
		private string[] referralList;
	}
}
