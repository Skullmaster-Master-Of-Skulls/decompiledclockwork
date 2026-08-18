using System;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x02000014 RID: 20
	public class IdentityUserLogin<TKey>
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000763F File Offset: 0x0000583F
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00007647 File Offset: 0x00005847
		public virtual string LoginProvider { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00007650 File Offset: 0x00005850
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00007658 File Offset: 0x00005858
		public virtual string ProviderKey { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00007661 File Offset: 0x00005861
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00007669 File Offset: 0x00005869
		public virtual TKey UserId { get; set; }
	}
}
