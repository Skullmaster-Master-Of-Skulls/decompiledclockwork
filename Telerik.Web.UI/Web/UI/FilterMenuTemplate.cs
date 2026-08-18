using System;
using System.Web.UI;
using Telerik.Web.UI.SpreadsheetFilterMenu;

namespace Telerik.Web.UI
{
	// Token: 0x020008AE RID: 2222
	public class FilterMenuTemplate : ITemplate
	{
		// Token: 0x17001B00 RID: 6912
		// (get) Token: 0x06005275 RID: 21109 RVA: 0x00100740 File Offset: 0x000FE940
		// (set) Token: 0x06005276 RID: 21110 RVA: 0x00100748 File Offset: 0x000FE948
		internal IFilterMenuView View { get; set; }

		// Token: 0x17001B01 RID: 6913
		// (get) Token: 0x06005277 RID: 21111 RVA: 0x00100751 File Offset: 0x000FE951
		// (set) Token: 0x06005278 RID: 21112 RVA: 0x00100759 File Offset: 0x000FE959
		internal IFilterMenuRenderer Renderer { get; set; }

		// Token: 0x17001B02 RID: 6914
		// (get) Token: 0x06005279 RID: 21113 RVA: 0x00100762 File Offset: 0x000FE962
		// (set) Token: 0x0600527A RID: 21114 RVA: 0x0010076A File Offset: 0x000FE96A
		public ISpreadsheet Owner { get; set; }

		// Token: 0x0600527B RID: 21115 RVA: 0x00100773 File Offset: 0x000FE973
		public FilterMenuTemplate(ISpreadsheet owner)
		{
			this.Owner = owner;
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x00100782 File Offset: 0x000FE982
		public void InstantiateIn(Control container)
		{
			this.CreateView();
			this.CreateRenderer();
			this.CreateLayout(container);
			this.CreateControls(container);
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x0010079E File Offset: 0x000FE99E
		private void CreateView()
		{
			this.View = new ViewFactory(this).CreateView();
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x001007B1 File Offset: 0x000FE9B1
		private void CreateRenderer()
		{
			this.Renderer = new RendererFactory(this).CreateRenderer();
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x001007C4 File Offset: 0x000FE9C4
		private void CreateLayout(Control container)
		{
			this.Renderer.CreateLayout(container);
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x001007D2 File Offset: 0x000FE9D2
		private void CreateControls(Control container)
		{
			this.View.CreateControls();
			this.Renderer.CreateControls();
		}
	}
}
