using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200000D RID: 13
	public interface IUserLockoutStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
	{
		// Token: 0x06000028 RID: 40
		Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user);

		// Token: 0x06000029 RID: 41
		Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd);

		// Token: 0x0600002A RID: 42
		Task<int> IncrementAccessFailedCountAsync(TUser user);

		// Token: 0x0600002B RID: 43
		Task ResetAccessFailedCountAsync(TUser user);

		// Token: 0x0600002C RID: 44
		Task<int> GetAccessFailedCountAsync(TUser user);

		// Token: 0x0600002D RID: 45
		Task<bool> GetLockoutEnabledAsync(TUser user);

		// Token: 0x0600002E RID: 46
		Task SetLockoutEnabledAsync(TUser user, bool enabled);
	}
}
