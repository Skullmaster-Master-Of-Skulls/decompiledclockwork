using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetHyperlink
{
	// Token: 0x020008BC RID: 2236
	internal abstract class RendererBase : IHyperlinkRenderer
	{
		// Token: 0x17001B2E RID: 6958
		// (get) Token: 0x06005302 RID: 21250 RVA: 0x00101783 File Offset: 0x000FF983
		// (set) Token: 0x06005303 RID: 21251 RVA: 0x0010178B File Offset: 0x000FF98B
		public IHyperlinkView View
		{
			get
			{
				return this._view;
			}
			protected set
			{
				this._view = value;
			}
		}

		// Token: 0x17001B2F RID: 6959
		// (get) Token: 0x06005304 RID: 21252 RVA: 0x00101794 File Offset: 0x000FF994
		public SpreadsheetStrings Localization
		{
			get
			{
				return this.View.Localization;
			}
		}

		// Token: 0x17001B30 RID: 6960
		// (get) Token: 0x06005305 RID: 21253 RVA: 0x001017A1 File Offset: 0x000FF9A1
		// (set) Token: 0x06005306 RID: 21254 RVA: 0x001017A9 File Offset: 0x000FF9A9
		public WebControl UrlPanel { get; set; }

		// Token: 0x17001B31 RID: 6961
		// (get) Token: 0x06005307 RID: 21255 RVA: 0x001017B2 File Offset: 0x000FF9B2
		// (set) Token: 0x06005308 RID: 21256 RVA: 0x001017BA File Offset: 0x000FF9BA
		public Panel ButtonsPanel { get; set; }

		// Token: 0x06005309 RID: 21257 RVA: 0x001017C3 File Offset: 0x000FF9C3
		public RendererBase(IHyperlinkView view)
		{
			this.View = view;
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x001017D4 File Offset: 0x000FF9D4
		public void CreateLayout(Control container)
		{
			this.UrlPanel = this.CreateFormList("");
			container.Controls.Add(this.UrlPanel);
			this.ButtonsPanel = new Panel
			{
				CssClass = "rssButtons"
			};
			container.Controls.Add(this.ButtonsPanel);
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x0010182C File Offset: 0x000FFA2C
		public virtual void CreateControls()
		{
			this.CreateUrlControls();
			this.CreateCommandButtons();
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x0010183C File Offset: 0x000FFA3C
		private void CreateUrlControls()
		{
			WebControl webControl = this.CreateListItem();
			this.UrlPanel.Controls.Add(webControl);
			webControl.Controls.Add(this.View.UrlTextBox);
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x00101878 File Offset: 0x000FFA78
		private void CreateCommandButtons()
		{
			this.ButtonsPanel.Controls.Add(this.View.SaveButton);
			this.ButtonsPanel.Controls.Add(this.View.CancelButton);
			this.ButtonsPanel.Controls.Add(this.View.RemoveButton);
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x001018D8 File Offset: 0x000FFAD8
		private WebControl CreateFormList(string cssClass = "")
		{
			return new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = string.Format("{0} {1}", "rssFormList", cssClass).Trim()
			};
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x0010190C File Offset: 0x000FFB0C
		private WebControl CreateListItem()
		{
			return new WebControl(HtmlTextWriterTag.Li);
		}

		// Token: 0x0400145E RID: 5214
		private IHyperlinkView _view;
	}
}
