using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200000A RID: 10
	public interface IUserStore<TUser, in TKey> : IDisposable where TUser : class, IUser<TKey>
	{
		// Token: 0x0600001F RID: 31
		Task CreateAsync(TUser user);

		// Token: 0x06000020 RID: 32
		Task UpdateAsync(TUser user);

		// Token: 0x06000021 RID: 33
		Task DeleteAsync(TUser user);

		// Token: 0x06000022 RID: 34
		Task<TUser> FindByIdAsync(TKey userId);

		// Token: 0x06000023 RID: 35
		Task<TUser> FindByNameAsync(string userName);
	}
}
