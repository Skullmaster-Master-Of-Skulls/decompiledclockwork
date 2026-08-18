using System;
using System.Data.Entity;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x02000006 RID: 6
	public class UserStore<TUser> : UserStore<TUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUserStore<TUser>, IUserStore<TUser, string>, IDisposable where TUser : IdentityUser
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00006203 File Offset: 0x00004403
		public UserStore() : this(new IdentityDbContext())
		{
			base.DisposeContext = true;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00006217 File Offset: 0x00004417
		public UserStore(DbContext context) : base(context)
		{
		}
	}
}
