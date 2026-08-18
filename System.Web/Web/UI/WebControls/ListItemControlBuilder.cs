using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005C9 RID: 1481
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ListItemControlBuilder : ControlBuilder
	{
		// Token: 0x06004834 RID: 18484 RVA: 0x00126E4E File Offset: 0x00125E4E
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x00126E51 File Offset: 0x00125E51
		public override bool HtmlDecodeLiterals()
		{
			return true;
		}
	}
}
