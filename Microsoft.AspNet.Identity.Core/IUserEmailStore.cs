using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200000F RID: 15
	public interface IUserEmailStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
	{
		// Token: 0x06000031 RID: 49
		Task SetEmailAsync(TUser user, string email);

		// Token: 0x06000032 RID: 50
		Task<string> GetEmailAsync(TUser user);

		// Token: 0x06000033 RID: 51
		Task<bool> GetEmailConfirmedAsync(TUser user);

		// Token: 0x06000034 RID: 52
		Task SetEmailConfirmedAsync(TUser user, bool confirmed);

		// Token: 0x06000035 RID: 53
		Task<TUser> FindByEmailAsync(string email);
	}
}
