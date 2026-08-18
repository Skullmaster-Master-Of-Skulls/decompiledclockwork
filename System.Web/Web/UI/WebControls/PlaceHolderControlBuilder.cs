using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000619 RID: 1561
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class PlaceHolderControlBuilder : ControlBuilder
	{
		// Token: 0x06004D93 RID: 19859 RVA: 0x0013AD0D File Offset: 0x00139D0D
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
