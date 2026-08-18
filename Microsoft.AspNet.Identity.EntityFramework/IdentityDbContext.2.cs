using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x0200000B RID: 11
	public class IdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00006F89 File Offset: 0x00005189
		public IdentityDbContext() : this("DefaultConnection")
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00006F96 File Offset: 0x00005196
		public IdentityDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00006F9F File Offset: 0x0000519F
		public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00006FAA File Offset: 0x000051AA
		public IdentityDbContext(DbCompiledModel model) : base(model)
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00006FB3 File Offset: 0x000051B3
		public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00006FBD File Offset: 0x000051BD
		public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model)
		{
		}
	}
}
