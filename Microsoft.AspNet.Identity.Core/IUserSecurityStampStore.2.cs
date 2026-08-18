using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000017 RID: 23
	public interface IUserSecurityStampStore<TUser> : IUserSecurityStampStore<TUser, string>, IUserStore<TUser, string>, IDisposable where TUser : class, IUser<string>
	{
	}
}
