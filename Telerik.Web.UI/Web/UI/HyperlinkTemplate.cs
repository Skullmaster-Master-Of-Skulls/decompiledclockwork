using System;
using System.Web.UI;
using Telerik.Web.UI.SpreadsheetHyperlink;

namespace Telerik.Web.UI
{
	// Token: 0x020008B9 RID: 2233
	public class HyperlinkTemplate : ITemplate
	{
		// Token: 0x17001B28 RID: 6952
		// (get) Token: 0x060052EE RID: 21230 RVA: 0x001016DA File Offset: 0x000FF8DA
		// (set) Token: 0x060052EF RID: 21231 RVA: 0x001016E2 File Offset: 0x000FF8E2
		internal IHyperlinkView View { get; set; }

		// Token: 0x17001B29 RID: 6953
		// (get) Token: 0x060052F0 RID: 21232 RVA: 0x001016EB File Offset: 0x000FF8EB
		// (set) Token: 0x060052F1 RID: 21233 RVA: 0x001016F3 File Offset: 0x000FF8F3
		internal IHyperlinkRenderer Renderer { get; set; }

		// Token: 0x17001B2A RID: 6954
		// (get) Token: 0x060052F2 RID: 21234 RVA: 0x001016FC File Offset: 0x000FF8FC
		// (set) Token: 0x060052F3 RID: 21235 RVA: 0x00101704 File Offset: 0x000FF904
		public ISpreadsheet Owner { get; set; }

		// Token: 0x060052F4 RID: 21236 RVA: 0x0010170D File Offset: 0x000FF90D
		public HyperlinkTemplate(ISpreadsheet owner)
		{
			this.Owner = owner;
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x0010171C File Offset: 0x000FF91C
		public void InstantiateIn(Control container)
		{
			this.CreateView();
			this.CreateRenderer();
			this.CreateLayout(container);
			this.CreateControls();
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00101737 File Offset: 0x000FF937
		private void CreateView()
		{
			this.View = new ViewFactory(this).CreateView();
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x0010174A File Offset: 0x000FF94A
		private void CreateRenderer()
		{
			this.Renderer = new RendererFactory(this).CreateRenderer();
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x0010175D File Offset: 0x000FF95D
		private void CreateLayout(Control container)
		{
			this.Renderer.CreateLayout(container);
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x0010176B File Offset: 0x000FF96B
		private void CreateControls()
		{
			this.View.CreateControls();
			this.Renderer.CreateControls();
		}
	}
}
