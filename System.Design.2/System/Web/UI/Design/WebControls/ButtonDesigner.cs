using System;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000A2 RID: 162
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ButtonDesigner : ControlDesigner
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x00016D84 File Offset: 0x00014F84
		public override string GetDesignTimeHtml()
		{
			Button button = (Button)base.ViewControl;
			string text = button.Text;
			bool flag = text.Trim().Length == 0;
			if (flag)
			{
				button.Text = "[" + button.ID + "]";
			}
			string designTimeHtml = base.GetDesignTimeHtml();
			if (flag)
			{
				button.Text = text;
			}
			return designTimeHtml;
		}
	}
}
