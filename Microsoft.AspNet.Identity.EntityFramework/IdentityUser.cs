using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x02000010 RID: 16
	public class IdentityUser<TKey, TLogin, TRole, TClaim> : IUser<TKey> where TLogin : IdentityUserLogin<TKey> where TRole : IdentityUserRole<TKey> where TClaim : IdentityUserClaim<TKey>
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00007487 File Offset: 0x00005687
		public IdentityUser()
		{
			this.Claims = new List<TClaim>();
			this.Roles = new List<TRole>();
			this.Logins = new List<TLogin>();
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000074B0 File Offset: 0x000056B0
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000074B8 File Offset: 0x000056B8
		public virtual string Email { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000074C1 File Offset: 0x000056C1
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000074C9 File Offset: 0x000056C9
		public virtual bool EmailConfirmed { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000074D2 File Offset: 0x000056D2
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000074DA File Offset: 0x000056DA
		public virtual string PasswordHash { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000074E3 File Offset: 0x000056E3
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000074EB File Offset: 0x000056EB
		public virtual string SecurityStamp { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000074F4 File Offset: 0x000056F4
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000074FC File Offset: 0x000056FC
		public virtual string PhoneNumber { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00007505 File Offset: 0x00005705
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x0000750D File Offset: 0x0000570D
		public virtual bool PhoneNumberConfirmed { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00007516 File Offset: 0x00005716
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000751E File Offset: 0x0000571E
		public virtual bool TwoFactorEnabled { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00007527 File Offset: 0x00005727
		// (set) Token: 0x060000AB RID: 171 RVA: 0x0000752F File Offset: 0x0000572F
		public virtual DateTime? LockoutEndDateUtc { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00007538 File Offset: 0x00005738
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00007540 File Offset: 0x00005740
		public virtual bool LockoutEnabled { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00007549 File Offset: 0x00005749
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00007551 File Offset: 0x00005751
		public virtual int AccessFailedCount { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000755A File Offset: 0x0000575A
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00007562 File Offset: 0x00005762
		public virtual ICollection<TRole> Roles { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000756B File Offset: 0x0000576B
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00007573 File Offset: 0x00005773
		public virtual ICollection<TClaim> Claims { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000757C File Offset: 0x0000577C
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00007584 File Offset: 0x00005784
		public virtual ICollection<TLogin> Logins { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000758D File Offset: 0x0000578D
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00007595 File Offset: 0x00005795
		public virtual TKey Id { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000759E File Offset: 0x0000579E
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000075A6 File Offset: 0x000057A6
		public virtual string UserName { get; set; }
	}
}
