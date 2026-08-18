using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000037 RID: 55
	public interface IUserLoginStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
	{
		// Token: 0x060000E8 RID: 232
		Task AddLoginAsync(TUser user, UserLoginInfo login);

		// Token: 0x060000E9 RID: 233
		Task RemoveLoginAsync(TUser user, UserLoginInfo login);

		// Token: 0x060000EA RID: 234
		Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user);

		// Token: 0x060000EB RID: 235
		Task<TUser> FindAsync(UserLoginInfo login);
	}
}
