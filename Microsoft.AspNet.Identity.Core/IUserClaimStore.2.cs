using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000036 RID: 54
	public interface IUserClaimStore<TUser> : IUserClaimStore<TUser, string>, IUserStore<TUser, string>, IDisposable where TUser : class, IUser<string>
	{
	}
}
