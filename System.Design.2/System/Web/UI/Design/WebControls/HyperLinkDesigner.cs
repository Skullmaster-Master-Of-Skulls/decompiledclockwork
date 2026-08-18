using System;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000D3 RID: 211
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class HyperLinkDesigner : TextControlDesigner
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x000279A4 File Offset: 0x00025BA4
		public override string GetDesignTimeHtml()
		{
			HyperLink hyperLink = (HyperLink)base.Component;
			string text = hyperLink.Text;
			string imageUrl = hyperLink.ImageUrl;
			string navigateUrl = hyperLink.NavigateUrl;
			bool flag = text.Trim().Length == 0 && imageUrl.Trim().Length == 0;
			bool flag2 = navigateUrl.Trim().Length == 0;
			bool flag3 = hyperLink.HasControls();
			Control[] array = null;
			if (flag)
			{
				if (flag3)
				{
					array = new Control[hyperLink.Controls.Count];
					hyperLink.Controls.CopyTo(array, 0);
				}
				hyperLink.Text = "[" + hyperLink.ID + "]";
			}
			if (flag2)
			{
				hyperLink.NavigateUrl = "url";
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
					hyperLink.Text = text;
					if (flag3)
					{
						foreach (Control child in array)
						{
							hyperLink.Controls.Add(child);
						}
					}
				}
				if (flag2)
				{
					hyperLink.NavigateUrl = navigateUrl;
				}
			}
			return designTimeHtml;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00027AC4 File Offset: 0x00025CC4
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			base.OnComponentChanged(sender, new ComponentChangedEventArgs(ce.Component, null, null, null));
		}
	}
}
