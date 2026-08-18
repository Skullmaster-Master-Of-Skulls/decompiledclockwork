using System;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000123 RID: 291
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SubstitutionDesigner : ControlDesigner
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x00043368 File Offset: 0x00041568
		public override string GetDesignTimeHtml()
		{
			return this.GetEmptyDesignTimeHtml();
		}
	}
}
