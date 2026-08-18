using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200000E RID: 14
	public interface IUserTwoFactorStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
	{
		// Token: 0x0600002F RID: 47
		Task SetTwoFactorEnabledAsync(TUser user, bool enabled);

		// Token: 0x06000030 RID: 48
		Task<bool> GetTwoFactorEnabledAsync(TUser user);
	}
}
