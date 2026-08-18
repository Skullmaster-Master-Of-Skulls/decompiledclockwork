using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200057D RID: 1405
	public class WebPartCancelEventArgs : CancelEventArgs
	{
		// Token: 0x06004749 RID: 18249 RVA: 0x000EA838 File Offset: 0x000E8A38
		public WebPartCancelEventArgs(WebPart webPart)
		{
			this._webPart = webPart;
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x0600474A RID: 18250 RVA: 0x000EA847 File Offset: 0x000E8A47
		// (set) Token: 0x0600474B RID: 18251 RVA: 0x000EA84F File Offset: 0x000E8A4F
		public WebPart WebPart
		{
			get
			{
				return this._webPart;
			}
			set
			{
				this._webPart = value;
			}
		}

		// Token: 0x040026EB RID: 9963
		private WebPart _webPart;
	}
}
