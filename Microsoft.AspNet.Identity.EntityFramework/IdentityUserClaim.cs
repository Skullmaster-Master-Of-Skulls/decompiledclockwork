using System;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x02000012 RID: 18
	public class IdentityUserClaim<TKey>
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000075EB File Offset: 0x000057EB
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000075F3 File Offset: 0x000057F3
		public virtual int Id { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000075FC File Offset: 0x000057FC
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00007604 File Offset: 0x00005804
		public virtual TKey UserId { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000760D File Offset: 0x0000580D
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00007615 File Offset: 0x00005815
		public virtual string ClaimType { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000761E File Offset: 0x0000581E
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00007626 File Offset: 0x00005826
		public virtual string ClaimValue { get; set; }
	}
}
