using System;
using System.Data.Entity;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x02000003 RID: 3
	public class RoleStore<TRole> : RoleStore<TRole, string, IdentityUserRole>, IQueryableRoleStore<TRole>, IQueryableRoleStore<TRole, string>, IRoleStore<TRole, string>, IDisposable where TRole : IdentityRole, new()
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000026BE File Offset: 0x000008BE
		public RoleStore() : base(new IdentityDbContext())
		{
			base.DisposeContext = true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026D2 File Offset: 0x000008D2
		public RoleStore(DbContext context) : base(context)
		{
		}
	}
}
