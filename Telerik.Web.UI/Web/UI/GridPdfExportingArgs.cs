using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001131 RID: 4401
	public class GridPdfExportingArgs : EventArgs
	{
		// Token: 0x0600B37B RID: 45947 RVA: 0x00271755 File Offset: 0x0026F955
		public GridPdfExportingArgs(string rawHTML)
		{
			this._rawHtml = rawHTML;
		}

		// Token: 0x170039FE RID: 14846
		// (get) Token: 0x0600B37C RID: 45948 RVA: 0x00271764 File Offset: 0x0026F964
		// (set) Token: 0x0600B37D RID: 45949 RVA: 0x0027176C File Offset: 0x0026F96C
		public string RawHTML
		{
			get
			{
				return this._rawHtml;
			}
			set
			{
				this._rawHtml = value;
			}
		}

		// Token: 0x04002F42 RID: 12098
		private string _rawHtml;
	}
}
