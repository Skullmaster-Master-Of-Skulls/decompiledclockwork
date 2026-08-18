using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200059D RID: 1437
	public class WebPartEventArgs : EventArgs
	{
		// Token: 0x0600483F RID: 18495 RVA: 0x000ED0FE File Offset: 0x000EB2FE
		public WebPartEventArgs(WebPart webPart)
		{
			this._webPart = webPart;
		}

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06004840 RID: 18496 RVA: 0x000ED10D File Offset: 0x000EB30D
		public WebPart WebPart
		{
			get
			{
				return this._webPart;
			}
		}

		// Token: 0x04002725 RID: 10021
		private WebPart _webPart;
	}
}
