using System;
using System.ComponentModel;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000659 RID: 1625
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadPdfViewerClientState
	{
		// Token: 0x06003BA2 RID: 15266 RVA: 0x000C23F7 File Offset: 0x000C05F7
		public RadPdfViewerClientState()
		{
			this.ActivePage = 1;
			this.Scale = new Scale();
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x000C2411 File Offset: 0x000C0611
		// (set) Token: 0x06003BA4 RID: 15268 RVA: 0x000C2419 File Offset: 0x000C0619
		public int ActivePage { get; set; }

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06003BA5 RID: 15269 RVA: 0x000C2422 File Offset: 0x000C0622
		// (set) Token: 0x06003BA6 RID: 15270 RVA: 0x000C242A File Offset: 0x000C062A
		public Scale Scale { get; set; }
	}
}
