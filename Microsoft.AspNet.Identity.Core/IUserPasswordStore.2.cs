using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200001C RID: 28
	public interface IUserPasswordStore<TUser> : IUserPasswordStore<TUser, string>, IUserStore<TUser, string>, IDisposable where TUser : class, IUser<string>
	{
	}
}
