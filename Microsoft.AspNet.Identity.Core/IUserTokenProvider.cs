using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000009 RID: 9
	public interface IUserTokenProvider<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
	{
		// Token: 0x0600001B RID: 27
		Task<string> GenerateAsync(string purpose, UserManager<TUser, TKey> manager, TUser user);

		// Token: 0x0600001C RID: 28
		Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser, TKey> manager, TUser user);

		// Token: 0x0600001D RID: 29
		Task NotifyAsync(string token, UserManager<TUser, TKey> manager, TUser user);

		// Token: 0x0600001E RID: 30
		Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user);
	}
}
