using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000729 RID: 1833
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WebPartEventArgs : EventArgs
	{
		// Token: 0x060058B2 RID: 22706 RVA: 0x001641E1 File Offset: 0x001631E1
		public WebPartEventArgs(WebPart webPart)
		{
			this._webPart = webPart;
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x060058B3 RID: 22707 RVA: 0x001641F0 File Offset: 0x001631F0
		public WebPart WebPart
		{
			get
			{
				return this._webPart;
			}
		}

		// Token: 0x04002FEE RID: 12270
		private WebPart _webPart;
	}
}
