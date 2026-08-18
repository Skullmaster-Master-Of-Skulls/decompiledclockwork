using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200002D RID: 45
	public interface IUserRoleStore<TUser> : IUserRoleStore<TUser, string>, IUserStore<TUser, string>, IDisposable where TUser : class, IUser<string>
	{
	}
}
