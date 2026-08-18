using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000022 RID: 34
	public class EmailTokenProvider<TUser> : EmailTokenProvider<TUser, string> where TUser : class, IUser<string>
	{
	}
}
