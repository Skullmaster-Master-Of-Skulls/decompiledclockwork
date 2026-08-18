using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000010 RID: 16
	public interface IUserEmailStore<TUser> : IUserEmailStore<TUser, string>, IUserStore<TUser, string>, IDisposable where TUser : class, IUser<string>
	{
	}
}
