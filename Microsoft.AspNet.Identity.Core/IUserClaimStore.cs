using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000035 RID: 53
	public interface IUserClaimStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
	{
		// Token: 0x060000E5 RID: 229
		Task<IList<Claim>> GetClaimsAsync(TUser user);

		// Token: 0x060000E6 RID: 230
		Task AddClaimAsync(TUser user, Claim claim);

		// Token: 0x060000E7 RID: 231
		Task RemoveClaimAsync(TUser user, Claim claim);
	}
}
