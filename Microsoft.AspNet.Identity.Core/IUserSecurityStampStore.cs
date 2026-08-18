using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000016 RID: 22
	public interface IUserSecurityStampStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
	{
		// Token: 0x0600003D RID: 61
		Task SetSecurityStampAsync(TUser user, string stamp);

		// Token: 0x0600003E RID: 62
		Task<string> GetSecurityStampAsync(TUser user);
	}
}
