using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200003A RID: 58
	public interface IUserStore<TUser> : IUserStore<TUser, string>, IDisposable where TUser : class, IUser<string>
	{
	}
}
