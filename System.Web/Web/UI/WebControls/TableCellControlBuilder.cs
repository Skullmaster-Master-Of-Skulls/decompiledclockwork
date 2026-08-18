using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000656 RID: 1622
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableCellControlBuilder : ControlBuilder
	{
		// Token: 0x06004F77 RID: 20343 RVA: 0x0013F8E4 File Offset: 0x0013E8E4
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
