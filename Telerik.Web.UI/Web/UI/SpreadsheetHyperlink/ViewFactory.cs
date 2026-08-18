using System;

namespace Telerik.Web.UI.SpreadsheetHyperlink
{
	// Token: 0x020008C3 RID: 2243
	internal class ViewFactory : IViewFactory
	{
		// Token: 0x17001B3F RID: 6975
		// (get) Token: 0x06005335 RID: 21301 RVA: 0x00101B7B File Offset: 0x000FFD7B
		public HyperlinkTemplate Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x00101B83 File Offset: 0x000FFD83
		public ViewFactory(HyperlinkTemplate owner)
		{
			this._owner = owner;
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x00101B92 File Offset: 0x000FFD92
		public IHyperlinkView CreateView()
		{
			return new View(this.Owner);
		}

		// Token: 0x04001467 RID: 5223
		private readonly HyperlinkTemplate _owner;
	}
}
