using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetHyperlink
{
	// Token: 0x020008C1 RID: 2241
	internal abstract class ViewBase : IHyperlinkView
	{
		// Token: 0x17001B39 RID: 6969
		// (get) Token: 0x06005320 RID: 21280 RVA: 0x00101954 File Offset: 0x000FFB54
		// (set) Token: 0x06005321 RID: 21281 RVA: 0x0010195C File Offset: 0x000FFB5C
		public HyperlinkTemplate Owner
		{
			get
			{
				return this._owner;
			}
			protected set
			{
				this._owner = value;
			}
		}

		// Token: 0x17001B3A RID: 6970
		// (get) Token: 0x06005322 RID: 21282 RVA: 0x00101965 File Offset: 0x000FFB65
		public SpreadsheetStrings Localization
		{
			get
			{
				return this.Owner.Owner.Localization;
			}
		}

		// Token: 0x17001B3B RID: 6971
		// (get) Token: 0x06005323 RID: 21283 RVA: 0x00101977 File Offset: 0x000FFB77
		// (set) Token: 0x06005324 RID: 21284 RVA: 0x0010197F File Offset: 0x000FFB7F
		public WebControl SaveButton { get; set; }

		// Token: 0x17001B3C RID: 6972
		// (get) Token: 0x06005325 RID: 21285 RVA: 0x00101988 File Offset: 0x000FFB88
		// (set) Token: 0x06005326 RID: 21286 RVA: 0x00101990 File Offset: 0x000FFB90
		public WebControl CancelButton { get; set; }

		// Token: 0x17001B3D RID: 6973
		// (get) Token: 0x06005327 RID: 21287 RVA: 0x00101999 File Offset: 0x000FFB99
		// (set) Token: 0x06005328 RID: 21288 RVA: 0x001019A1 File Offset: 0x000FFBA1
		public WebControl RemoveButton { get; set; }

		// Token: 0x17001B3E RID: 6974
		// (get) Token: 0x06005329 RID: 21289 RVA: 0x001019AA File Offset: 0x000FFBAA
		// (set) Token: 0x0600532A RID: 21290 RVA: 0x001019B2 File Offset: 0x000FFBB2
		public WebControl UrlTextBox { get; set; }

		// Token: 0x0600532B RID: 21291 RVA: 0x001019BB File Offset: 0x000FFBBB
		public ViewBase(HyperlinkTemplate owner)
		{
			this.Owner = owner;
		}

		// Token: 0x0600532C RID: 21292 RVA: 0x001019CA File Offset: 0x000FFBCA
		public void CreateControls()
		{
			this.CreateUrlControls();
			this.CreateCommandButtons();
		}

		// Token: 0x0600532D RID: 21293 RVA: 0x001019D8 File Offset: 0x000FFBD8
		protected void CreateUrlControls()
		{
			this.UrlTextBox = this.CreateTextBox("UrlTextBox", this.Localization.HyperlinkUrl);
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x001019F6 File Offset: 0x000FFBF6
		protected void CreateCommandButtons()
		{
			this.CreateSaveButton();
			this.CreateCancelButton();
			this.CreateRemoveButton();
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x00101A0A File Offset: 0x000FFC0A
		protected void CreateSaveButton()
		{
			this.SaveButton = this.CreateCommandButton(this.Localization.HyperlinkSave, "rssPrimary");
			this.SaveButton.Attributes.Add("data-command", "save");
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x00101A42 File Offset: 0x000FFC42
		protected void CreateCancelButton()
		{
			this.CancelButton = this.CreateCommandButton(this.Localization.HyperlinkCancel, "");
			this.CancelButton.Attributes.Add("data-command", "cancel");
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x00101A7A File Offset: 0x000FFC7A
		protected void CreateRemoveButton()
		{
			this.RemoveButton = this.CreateCommandButton(this.Localization.HyperlinkRemove, "");
			this.RemoveButton.Attributes.Add("data-command", "remove");
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x00101AB4 File Offset: 0x000FFCB4
		private WebControl CreateTextBox(string id, string label = "")
		{
			return new RadTextBox
			{
				ID = id,
				RenderMode = RenderMode.Lightweight,
				Skin = this.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.Owner.Owner.EnableEmbeddedSkins,
				EnableViewState = false,
				Label = ((label == string.Empty) ? "" : (label + ":"))
			};
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x00101B30 File Offset: 0x000FFD30
		private WebControl CreateCommandButton(string text, string cssClass = "")
		{
			return new WebControl(HtmlTextWriterTag.Span)
			{
				Controls = 
				{
					new LiteralControl(text)
				},
				CssClass = string.Format("{0} {1}", "rssButton", cssClass).Trim()
			};
		}

		// Token: 0x04001462 RID: 5218
		private HyperlinkTemplate _owner;
	}
}
