using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005EF RID: 1519
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class MultiViewControlBuilder : ControlBuilder
	{
		// Token: 0x06004B2E RID: 19246 RVA: 0x00132E1B File Offset: 0x00131E1B
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			if (subBuilder is CodeBlockBuilder)
			{
				throw new Exception(SR.GetString("Multiview_rendering_block_not_allowed"));
			}
			base.AppendSubBuilder(subBuilder);
		}
	}
}
