using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000015 RID: 21
	public interface IQueryableUserStore<TUser> : IQueryableUserStore<TUser, string>, IUserStore<TUser, string>, IDisposable where TUser : class, IUser<string>
	{
	}
}
