using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000074 RID: 116
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class TextControlDesigner : ControlDesigner
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x00012170 File Offset: 0x00010370
		public override string GetDesignTimeHtml()
		{
			Control viewControl = base.ViewControl;
			PropertyInfo property = viewControl.GetType().GetProperty("Text");
			string text = (string)property.GetValue(viewControl, null);
			bool flag = text == null || text.Length == 0;
			bool flag2 = viewControl.HasControls();
			Control[] array = null;
			if (flag)
			{
				if (flag2)
				{
					array = new Control[viewControl.Controls.Count];
					viewControl.Controls.CopyTo(array, 0);
				}
				property.SetValue(viewControl, "[" + viewControl.ID + "]", null);
			}
			string designTimeHtml;
			try
			{
				designTimeHtml = base.GetDesignTimeHtml();
			}
			finally
			{
				if (flag)
				{
					property.SetValue(viewControl, text, null);
					if (flag2)
					{
						foreach (Control child in array)
						{
							viewControl.Controls.Add(child);
						}
					}
				}
			}
			return designTimeHtml;
		}
	}
}
