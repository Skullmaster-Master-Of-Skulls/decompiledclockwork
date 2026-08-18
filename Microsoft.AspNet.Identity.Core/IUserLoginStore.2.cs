using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000038 RID: 56
	public interface IUserLoginStore<TUser> : IUserLoginStore<TUser, string>, IUserStore<TUser, string>, IDisposable where TUser : class, IUser<string>
	{
	}
}
