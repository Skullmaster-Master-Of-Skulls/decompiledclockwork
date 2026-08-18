using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200001B RID: 27
	public interface IUserPasswordStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
	{
		// Token: 0x06000047 RID: 71
		Task SetPasswordHashAsync(TUser user, string passwordHash);

		// Token: 0x06000048 RID: 72
		Task<string> GetPasswordHashAsync(TUser user);

		// Token: 0x06000049 RID: 73
		Task<bool> HasPasswordAsync(TUser user);
	}
}
