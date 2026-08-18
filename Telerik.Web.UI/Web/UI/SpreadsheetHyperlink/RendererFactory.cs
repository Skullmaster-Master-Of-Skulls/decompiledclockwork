using System;

namespace Telerik.Web.UI.SpreadsheetHyperlink
{
	// Token: 0x020008BE RID: 2238
	internal class RendererFactory
	{
		// Token: 0x17001B32 RID: 6962
		// (get) Token: 0x06005311 RID: 21265 RVA: 0x0010192B File Offset: 0x000FFB2B
		public HyperlinkTemplate Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x00101933 File Offset: 0x000FFB33
		public RendererFactory(HyperlinkTemplate owner)
		{
			this._owner = owner;
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x00101942 File Offset: 0x000FFB42
		public IHyperlinkRenderer CreateRenderer()
		{
			return new Renderer(this.Owner.View);
		}

		// Token: 0x04001461 RID: 5217
		private readonly HyperlinkTemplate _owner;
	}
}
