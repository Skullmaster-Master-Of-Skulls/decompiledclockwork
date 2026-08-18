using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001219 RID: 4633
	public class TreeListPdfExportingEventArgs : EventArgs
	{
		// Token: 0x17003DB8 RID: 15800
		// (get) Token: 0x0600BF49 RID: 48969 RVA: 0x002A59CA File Offset: 0x002A3BCA
		// (set) Token: 0x0600BF4A RID: 48970 RVA: 0x002A59D2 File Offset: 0x002A3BD2
		public string RawHtml { get; set; }

		// Token: 0x0600BF4B RID: 48971 RVA: 0x002A59DB File Offset: 0x002A3BDB
		public TreeListPdfExportingEventArgs(string rawHtml)
		{
			this.RawHtml = rawHtml;
		}
	}
}
