using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200000C RID: 12
	public interface IUserPhoneNumberStore<TUser> : IUserPhoneNumberStore<TUser, string>, IUserStore<TUser, string>, IDisposable where TUser : class, IUser<string>
	{
	}
}
