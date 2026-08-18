using System;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x02000011 RID: 17
	public class IdentityUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUser, IUser<string>
	{
		// Token: 0x060000BA RID: 186 RVA: 0x000075B0 File Offset: 0x000057B0
		public IdentityUser()
		{
			this.Id = Guid.NewGuid().ToString();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000075DC File Offset: 0x000057DC
		public IdentityUser(string userName) : this()
		{
			this.UserName = userName;
		}
	}
}
