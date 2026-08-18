using System;
using System.Web.UI;
using Telerik.Web.UI.SpreadsheetCustomFormat;

namespace Telerik.Web.UI
{
	// Token: 0x0200089F RID: 2207
	public class CustomFormatTemplate : ITemplate
	{
		// Token: 0x17001AE1 RID: 6881
		// (get) Token: 0x06005212 RID: 21010 RVA: 0x000FFB49 File Offset: 0x000FDD49
		// (set) Token: 0x06005213 RID: 21011 RVA: 0x000FFB51 File Offset: 0x000FDD51
		internal ICustomFormatView View { get; set; }

		// Token: 0x17001AE2 RID: 6882
		// (get) Token: 0x06005214 RID: 21012 RVA: 0x000FFB5A File Offset: 0x000FDD5A
		// (set) Token: 0x06005215 RID: 21013 RVA: 0x000FFB62 File Offset: 0x000FDD62
		internal ICustomFormatRenderer Renderer { get; set; }

		// Token: 0x17001AE3 RID: 6883
		// (get) Token: 0x06005216 RID: 21014 RVA: 0x000FFB6B File Offset: 0x000FDD6B
		// (set) Token: 0x06005217 RID: 21015 RVA: 0x000FFB73 File Offset: 0x000FDD73
		public ISpreadsheet Owner { get; set; }

		// Token: 0x06005218 RID: 21016 RVA: 0x000FFB7C File Offset: 0x000FDD7C
		public CustomFormatTemplate(ISpreadsheet owner)
		{
			this.Owner = owner;
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x000FFB8B File Offset: 0x000FDD8B
		public void InstantiateIn(Control container)
		{
			this.CreateView();
			this.CreateRenderer();
			this.CreateLayout(container);
			this.CreateControls(container);
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x000FFBA7 File Offset: 0x000FDDA7
		private void CreateView()
		{
			this.View = new ViewFactory(this).CreateView();
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x000FFBBA File Offset: 0x000FDDBA
		private void CreateRenderer()
		{
			this.Renderer = new RendererFactory(this).CreateRenderer();
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x000FFBCD File Offset: 0x000FDDCD
		private void CreateLayout(Control container)
		{
			this.Renderer.CreateLayout(container);
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x000FFBDB File Offset: 0x000FDDDB
		private void CreateControls(Control container)
		{
			this.View.CreateControls();
			this.Renderer.CreateControls();
		}
	}
}
