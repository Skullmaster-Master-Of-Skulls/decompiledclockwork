using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200002C RID: 44
	public interface IUserRoleStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
	{
		// Token: 0x06000092 RID: 146
		Task AddToRoleAsync(TUser user, string roleName);

		// Token: 0x06000093 RID: 147
		Task RemoveFromRoleAsync(TUser user, string roleName);

		// Token: 0x06000094 RID: 148
		Task<IList<string>> GetRolesAsync(TUser user);

		// Token: 0x06000095 RID: 149
		Task<bool> IsInRoleAsync(TUser user, string roleName);
	}
}
