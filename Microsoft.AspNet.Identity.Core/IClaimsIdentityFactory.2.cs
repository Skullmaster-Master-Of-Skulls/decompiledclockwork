using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000019 RID: 25
	public interface IClaimsIdentityFactory<TUser> where TUser : class, IUser
	{
		// Token: 0x06000040 RID: 64
		Task<ClaimsIdentity> CreateAsync(UserManager<TUser> manager, TUser user, string authenticationType);
	}
}
