using System;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000E0 RID: 224
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class LoginNameDesigner : ControlDesigner
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00018A79 File Offset: 0x00016C79
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("Control_ErrorRenderingShort") + "<br />" + e.Message);
		}
	}
}
