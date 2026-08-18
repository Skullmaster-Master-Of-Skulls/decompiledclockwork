using System;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x02001138 RID: 4408
	public class GridHTMLExportingEventArgs : EventArgs
	{
		// Token: 0x17003A05 RID: 14853
		// (get) Token: 0x0600B390 RID: 45968 RVA: 0x002717FF File Offset: 0x0026F9FF
		public StringBuilder Styles
		{
			get
			{
				return this._styles;
			}
		}

		// Token: 0x17003A06 RID: 14854
		// (get) Token: 0x0600B391 RID: 45969 RVA: 0x00271807 File Offset: 0x0026FA07
		// (set) Token: 0x0600B392 RID: 45970 RVA: 0x0027180F File Offset: 0x0026FA0F
		public string XmlOptions { get; set; }

		// Token: 0x04002F54 RID: 12116
		private StringBuilder _styles = new StringBuilder();
	}
}
