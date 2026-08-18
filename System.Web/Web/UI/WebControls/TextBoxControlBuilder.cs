using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000664 RID: 1636
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TextBoxControlBuilder : ControlBuilder
	{
		// Token: 0x06004FE8 RID: 20456 RVA: 0x001409B6 File Offset: 0x0013F9B6
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x001409B9 File Offset: 0x0013F9B9
		public override bool HtmlDecodeLiterals()
		{
			return true;
		}
	}
}
