using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetHyperlink
{
	// Token: 0x020008BB RID: 2235
	internal interface IHyperlinkRenderer
	{
		// Token: 0x17001B2B RID: 6955
		// (get) Token: 0x060052FB RID: 21243
		IHyperlinkView View { get; }

		// Token: 0x17001B2C RID: 6956
		// (get) Token: 0x060052FC RID: 21244
		// (set) Token: 0x060052FD RID: 21245
		WebControl UrlPanel { get; set; }

		// Token: 0x17001B2D RID: 6957
		// (get) Token: 0x060052FE RID: 21246
		// (set) Token: 0x060052FF RID: 21247
		Panel ButtonsPanel { get; set; }

		// Token: 0x06005300 RID: 21248
		void CreateLayout(Control container);

		// Token: 0x06005301 RID: 21249
		void CreateControls();
	}
}
