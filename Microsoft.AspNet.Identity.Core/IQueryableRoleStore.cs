using System;
using System.Linq;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000012 RID: 18
	public interface IQueryableRoleStore<TRole, in TKey> : IRoleStore<TRole, TKey>, IDisposable where TRole : IRole<TKey>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003B RID: 59
		IQueryable<TRole> Roles { get; }
	}
}
