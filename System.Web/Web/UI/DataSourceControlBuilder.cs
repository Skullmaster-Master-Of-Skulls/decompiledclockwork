using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003DE RID: 990
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataSourceControlBuilder : ControlBuilder
	{
		// Token: 0x06003022 RID: 12322 RVA: 0x000D4E18 File Offset: 0x000D3E18
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
