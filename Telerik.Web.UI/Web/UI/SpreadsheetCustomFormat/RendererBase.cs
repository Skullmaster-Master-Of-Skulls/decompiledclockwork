using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetCustomFormat
{
	// Token: 0x020008A2 RID: 2210
	internal abstract class RendererBase : ICustomFormatRenderer
	{
		// Token: 0x17001AE8 RID: 6888
		// (get) Token: 0x06005228 RID: 21032 RVA: 0x000FFBF3 File Offset: 0x000FDDF3
		// (set) Token: 0x06005229 RID: 21033 RVA: 0x000FFBFB File Offset: 0x000FDDFB
		public ICustomFormatView View
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

		// Token: 0x17001AE9 RID: 6889
		// (get) Token: 0x0600522A RID: 21034 RVA: 0x000FFC04 File Offset: 0x000FDE04
		public SpreadsheetStrings Localization
		{
			get
			{
				return this.View.Localization;
			}
		}

		// Token: 0x17001AEA RID: 6890
		// (get) Token: 0x0600522B RID: 21035 RVA: 0x000FFC11 File Offset: 0x000FDE11
		// (set) Token: 0x0600522C RID: 21036 RVA: 0x000FFC19 File Offset: 0x000FDE19
		public Panel ButtonsPanel { get; set; }

		// Token: 0x17001AEB RID: 6891
		// (get) Token: 0x0600522D RID: 21037 RVA: 0x000FFC22 File Offset: 0x000FDE22
		// (set) Token: 0x0600522E RID: 21038 RVA: 0x000FFC2A File Offset: 0x000FDE2A
		public RadTabStrip FormatsTabStrip { get; set; }

		// Token: 0x17001AEC RID: 6892
		// (get) Token: 0x0600522F RID: 21039 RVA: 0x000FFC33 File Offset: 0x000FDE33
		// (set) Token: 0x06005230 RID: 21040 RVA: 0x000FFC3B File Offset: 0x000FDE3B
		public RadMultiPage FormatsMultiPage { get; set; }

		// Token: 0x06005231 RID: 21041 RVA: 0x000FFC44 File Offset: 0x000FDE44
		public RendererBase(ICustomFormatView view)
		{
			this.View = view;
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x000FFC54 File Offset: 0x000FDE54
		public void CreateLayout(Control container)
		{
			this.FormatsTabStrip = this.CreateTabStrip("FormatsTabStrip");
			container.Controls.Add(this.FormatsTabStrip);
			this.FormatsMultiPage = this.CreateMultiPage("FormatsMultiPage");
			container.Controls.Add(this.FormatsMultiPage);
			this.FormatsTabStrip.MultiPageID = this.FormatsMultiPage.ID;
			this.ButtonsPanel = new Panel
			{
				CssClass = "rssButtons"
			};
			container.Controls.Add(this.ButtonsPanel);
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x000FFCE4 File Offset: 0x000FDEE4
		public virtual void CreateControls()
		{
			this.CreateTabs();
			this.CreatePageViews();
			this.CreateCommandButtons();
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x000FFCF8 File Offset: 0x000FDEF8
		private void CreateTabs()
		{
			RadTab tab = new RadTab(this.Localization.CustomFormatNumber);
			this.FormatsTabStrip.Tabs.Add(tab);
			RadTab tab2 = new RadTab(this.Localization.CustomFormatCurrency);
			this.FormatsTabStrip.Tabs.Add(tab2);
			RadTab tab3 = new RadTab(this.Localization.CustomFormatDateTime);
			this.FormatsTabStrip.Tabs.Add(tab3);
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x000FFD6C File Offset: 0x000FDF6C
		private void CreatePageViews()
		{
			RadPageView radPageView = new RadPageView();
			this.FormatsMultiPage.PageViews.Add(radPageView);
			radPageView.Controls.Add(this.CreatePreview());
			radPageView.Controls.Add(this.View.NumberFormatsListBox);
			RadPageView radPageView2 = new RadPageView();
			this.FormatsMultiPage.PageViews.Add(radPageView2);
			radPageView2.Controls.Add(this.CreatePreview());
			radPageView2.Controls.Add(this.View.CurrencyFormatsListBox);
			RadPageView radPageView3 = new RadPageView();
			this.FormatsMultiPage.PageViews.Add(radPageView3);
			radPageView3.Controls.Add(this.CreatePreview());
			radPageView3.Controls.Add(this.View.DateTimeFormatsListBox);
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x000FFE33 File Offset: 0x000FE033
		private void CreateCommandButtons()
		{
			this.ButtonsPanel.Controls.Add(this.View.SaveButton);
			this.ButtonsPanel.Controls.Add(this.View.CancelButton);
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x000FFE6C File Offset: 0x000FE06C
		private RadTabStrip CreateTabStrip(string id)
		{
			return new RadTabStrip
			{
				EnableViewState = false,
				RenderMode = RenderMode.Lightweight,
				Skin = this.View.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.View.Owner.Owner.EnableEmbeddedSkins,
				CssClass = "rssWindowTabstrip rssTabstrip",
				ID = id,
				ScrollChildren = true,
				Width = Unit.Percentage(100.0)
			};
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x000FFEF4 File Offset: 0x000FE0F4
		protected RadMultiPage CreateMultiPage(string id)
		{
			return new RadMultiPage
			{
				EnableViewState = false,
				RenderMode = RenderMode.Lightweight,
				Skin = this.View.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.View.Owner.Owner.EnableEmbeddedSkins,
				CssClass = "rssWindowMultipage",
				ID = id
			};
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x000FFF60 File Offset: 0x000FE160
		private WebControl CreatePreview()
		{
			return new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rssFormatPreview"
			};
		}

		// Token: 0x0400140C RID: 5132
		private ICustomFormatView _view;
	}
}
