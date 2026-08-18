using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000011 RID: 17
	public interface IRoleStore<TRole, in TKey> : IDisposable where TRole : IRole<TKey>
	{
		// Token: 0x06000036 RID: 54
		Task CreateAsync(TRole role);

		// Token: 0x06000037 RID: 55
		Task UpdateAsync(TRole role);

		// Token: 0x06000038 RID: 56
		Task DeleteAsync(TRole role);

		// Token: 0x06000039 RID: 57
		Task<TRole> FindByIdAsync(TKey roleId);

		// Token: 0x0600003A RID: 58
		Task<TRole> FindByNameAsync(string roleName);
	}
}
