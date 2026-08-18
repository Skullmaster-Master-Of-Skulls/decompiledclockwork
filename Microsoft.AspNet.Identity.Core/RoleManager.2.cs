using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200002B RID: 43
	public class RoleManager<TRole> : RoleManager<TRole, string> where TRole : class, IRole<string>
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000467C File Offset: 0x0000287C
		public RoleManager(IRoleStore<TRole, string> store) : base(store)
		{
		}
	}
}
