using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x0200000E RID: 14
	public class IdentityRole<TKey, TUserRole> : IRole<TKey> where TUserRole : IdentityUserRole<TKey>
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00007404 File Offset: 0x00005604
		public IdentityRole()
		{
			this.Users = new List<TUserRole>();
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00007417 File Offset: 0x00005617
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000741F File Offset: 0x0000561F
		public virtual ICollection<TUserRole> Users { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00007428 File Offset: 0x00005628
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00007430 File Offset: 0x00005630
		public TKey Id { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00007439 File Offset: 0x00005639
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00007441 File Offset: 0x00005641
		public string Name { get; set; }
	}
}
