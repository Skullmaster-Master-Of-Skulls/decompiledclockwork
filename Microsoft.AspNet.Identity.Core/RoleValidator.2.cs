using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000028 RID: 40
	public class RoleValidator<TRole> : RoleValidator<TRole, string> where TRole : class, IRole<string>
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003C22 File Offset: 0x00001E22
		public RoleValidator(RoleManager<TRole, string> manager) : base(manager)
		{
		}
	}
}
