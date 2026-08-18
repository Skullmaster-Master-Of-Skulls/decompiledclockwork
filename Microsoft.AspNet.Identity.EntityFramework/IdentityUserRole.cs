using System;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x02000007 RID: 7
	public class IdentityUserRole<TKey>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00006220 File Offset: 0x00004420
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00006228 File Offset: 0x00004428
		public virtual TKey UserId { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00006231 File Offset: 0x00004431
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00006239 File Offset: 0x00004439
		public virtual TKey RoleId { get; set; }
	}
}
