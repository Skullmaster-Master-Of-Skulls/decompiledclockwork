using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000033 RID: 51
	public class ClaimsIdentityFactory<TUser> : ClaimsIdentityFactory<TUser, string> where TUser : class, IUser<string>
	{
	}
}
