using System;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000EB RID: 235
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class MultiViewDesigner : ContainerControlDesigner
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x0002CBD0 File Offset: 0x0002ADD0
		public MultiViewDesigner()
		{
			base.FrameStyleInternal.Width = Unit.Percentage(100.0);
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool NoWrap
		{
			get
			{
				return false;
			}
		}
	}
}
