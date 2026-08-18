using System;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000A8 RID: 168
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class CheckBoxDesigner : ControlDesigner
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x00018EF8 File Offset: 0x000170F8
		public override string GetDesignTimeHtml()
		{
			CheckBox checkBox = (CheckBox)base.ViewControl;
			string text = checkBox.Text;
			bool flag = text == null || text.Length == 0;
			if (flag)
			{
				checkBox.Text = "[" + checkBox.ID + "]";
			}
			string designTimeHtml = base.GetDesignTimeHtml();
			if (flag)
			{
				checkBox.Text = text;
			}
			return designTimeHtml;
		}
	}
}
