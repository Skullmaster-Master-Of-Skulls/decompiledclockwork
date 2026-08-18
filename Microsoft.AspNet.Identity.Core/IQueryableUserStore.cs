using System;
using System.Linq;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000014 RID: 20
	public interface IQueryableUserStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003C RID: 60
		IQueryable<TUser> Users { get; }
	}
}
