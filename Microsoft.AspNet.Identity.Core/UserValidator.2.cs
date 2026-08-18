using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000047 RID: 71
	public class UserValidator<TUser> : UserValidator<TUser, string> where TUser : class, IUser<string>
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00010E3E File Offset: 0x0000F03E
		public UserValidator(UserManager<TUser, string> manager) : base(manager)
		{
		}
	}
}
