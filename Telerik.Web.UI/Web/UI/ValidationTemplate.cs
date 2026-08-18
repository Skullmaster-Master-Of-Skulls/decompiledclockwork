using System;
using System.Web.UI;
using Telerik.Web.UI.SpreadsheetValidation;

namespace Telerik.Web.UI
{
	// Token: 0x020008E1 RID: 2273
	public class ValidationTemplate : ITemplate
	{
		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x0600558F RID: 21903 RVA: 0x00106B40 File Offset: 0x00104D40
		// (set) Token: 0x06005590 RID: 21904 RVA: 0x00106B48 File Offset: 0x00104D48
		internal IValidationView View { get; set; }

		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x06005591 RID: 21905 RVA: 0x00106B51 File Offset: 0x00104D51
		// (set) Token: 0x06005592 RID: 21906 RVA: 0x00106B59 File Offset: 0x00104D59
		internal IValidationRenderer Renderer { get; set; }

		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x06005593 RID: 21907 RVA: 0x00106B62 File Offset: 0x00104D62
		// (set) Token: 0x06005594 RID: 21908 RVA: 0x00106B6A File Offset: 0x00104D6A
		public ISpreadsheet Owner { get; set; }

		// Token: 0x06005595 RID: 21909 RVA: 0x00106B73 File Offset: 0x00104D73
		public ValidationTemplate(ISpreadsheet owner)
		{
			this.Owner = owner;
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x00106B82 File Offset: 0x00104D82
		public void InstantiateIn(Control container)
		{
			this.CreateView();
			this.CreateRenderer();
			this.CreateLayout(container);
			this.CreateControls(container);
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x00106B9E File Offset: 0x00104D9E
		private void CreateView()
		{
			this.View = new ViewFactory(this).CreateView();
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x00106BB1 File Offset: 0x00104DB1
		private void CreateRenderer()
		{
			this.Renderer = new RendererFactory(this).CreateRenderer();
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x00106BC4 File Offset: 0x00104DC4
		private void CreateLayout(Control container)
		{
			this.Renderer.CreateLayout(container);
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x00106BD2 File Offset: 0x00104DD2
		private void CreateControls(Control container)
		{
			this.View.CreateControls();
			this.Renderer.CreateControls();
		}
	}
}
