using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000045 RID: 69
	public class UserManager<TUser> : UserManager<TUser, string> where TUser : class, IUser<string>
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x0001063C File Offset: 0x0000E83C
		public UserManager(IUserStore<TUser> store) : base(store)
		{
		}
	}
}
