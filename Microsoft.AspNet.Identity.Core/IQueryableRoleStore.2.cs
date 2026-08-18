using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000013 RID: 19
	public interface IQueryableRoleStore<TRole> : IQueryableRoleStore<TRole, string>, IRoleStore<TRole, string>, IDisposable where TRole : IRole<string>
	{
	}
}
