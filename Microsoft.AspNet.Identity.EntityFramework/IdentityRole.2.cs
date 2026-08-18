using System;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x0200000F RID: 15
	public class IdentityRole : IdentityRole<string, IdentityUserRole>
	{
		// Token: 0x06000099 RID: 153 RVA: 0x0000744C File Offset: 0x0000564C
		public IdentityRole()
		{
			base.Id = Guid.NewGuid().ToString();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00007478 File Offset: 0x00005678
		public IdentityRole(string roleName) : this()
		{
			base.Name = roleName;
		}
	}
}
