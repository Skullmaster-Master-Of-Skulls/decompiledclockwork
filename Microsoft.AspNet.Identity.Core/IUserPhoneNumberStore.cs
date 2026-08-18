using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200000B RID: 11
	public interface IUserPhoneNumberStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
	{
		// Token: 0x06000024 RID: 36
		Task SetPhoneNumberAsync(TUser user, string phoneNumber);

		// Token: 0x06000025 RID: 37
		Task<string> GetPhoneNumberAsync(TUser user);

		// Token: 0x06000026 RID: 38
		Task<bool> GetPhoneNumberConfirmedAsync(TUser user);

		// Token: 0x06000027 RID: 39
		Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed);
	}
}
