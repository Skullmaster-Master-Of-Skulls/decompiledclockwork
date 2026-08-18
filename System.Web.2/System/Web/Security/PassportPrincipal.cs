using System;
using System.Security.Principal;

namespace System.Web.Security
{
	// Token: 0x020005EF RID: 1519
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	public sealed class PassportPrincipal : GenericPrincipal
	{
		// Token: 0x06004CBA RID: 19642 RVA: 0x001065AA File Offset: 0x001047AA
		public PassportPrincipal(PassportIdentity identity, string[] roles) : base(identity, roles)
		{
		}
	}
}
