using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Web.UI.Design
{
	// Token: 0x02000012 RID: 18
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public sealed class ColorBuilder
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000362F File Offset: 0x0000182F
		private ColorBuilder()
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003638 File Offset: 0x00001838
		public static string BuildColor(IComponent component, Control owner, string initialColor)
		{
			string result = null;
			ISite site = component.Site;
			if (site == null)
			{
				return null;
			}
			if (site != null)
			{
				IWebFormsBuilderUIService webFormsBuilderUIService = (IWebFormsBuilderUIService)site.GetService(typeof(IWebFormsBuilderUIService));
				if (webFormsBuilderUIService != null)
				{
					result = webFormsBuilderUIService.BuildColor(owner, initialColor);
				}
			}
			return result;
		}
	}
}
