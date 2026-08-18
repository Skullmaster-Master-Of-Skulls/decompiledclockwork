using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200054F RID: 1359
	[Designer("System.Web.UI.Design.WebControls.WebParts.PageCatalogPartDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class PageCatalogPart : CatalogPart
	{
		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x000E4798 File Offset: 0x000E2998
		// (set) Token: 0x0600450A RID: 17674 RVA: 0x000D9EF2 File Offset: 0x000D80F2
		[WebSysDefaultValue("PageCatalogPart_PartTitle")]
		public override string Title
		{
			get
			{
				string text = (string)this.ViewState["Title"];
				if (text == null)
				{
					return SR.GetString("PageCatalogPart_PartTitle");
				}
				return text;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x000E47CC File Offset: 0x000E29CC
		public override WebPartDescriptionCollection GetAvailableWebPartDescriptions()
		{
			if (base.DesignMode)
			{
				return PageCatalogPart.DesignModeAvailableWebParts;
			}
			if (this._availableWebPartDescriptions == null)
			{
				WebPartCollection webPartCollection;
				if (base.WebPartManager != null)
				{
					WebPartCollection closedWebParts = this.GetClosedWebParts();
					if (closedWebParts != null)
					{
						webPartCollection = closedWebParts;
					}
					else
					{
						webPartCollection = new WebPartCollection();
					}
				}
				else
				{
					webPartCollection = new WebPartCollection();
				}
				ArrayList arrayList = new ArrayList();
				foreach (object obj in webPartCollection)
				{
					WebPart webPart = (WebPart)obj;
					if (!(webPart is UnauthorizedWebPart))
					{
						WebPartDescription value = new WebPartDescription(webPart);
						arrayList.Add(value);
					}
				}
				this._availableWebPartDescriptions = new WebPartDescriptionCollection(arrayList);
			}
			return this._availableWebPartDescriptions;
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x000E4890 File Offset: 0x000E2A90
		private WebPartCollection GetClosedWebParts()
		{
			ArrayList arrayList = new ArrayList();
			WebPartCollection webParts = base.WebPartManager.WebParts;
			if (webParts != null)
			{
				foreach (object obj in webParts)
				{
					WebPart webPart = (WebPart)obj;
					if (webPart.IsClosed)
					{
						arrayList.Add(webPart);
					}
				}
			}
			return new WebPartCollection(arrayList);
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x000E490C File Offset: 0x000E2B0C
		public override WebPart GetWebPart(WebPartDescription description)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			WebPartDescriptionCollection availableWebPartDescriptions = this.GetAvailableWebPartDescriptions();
			if (!availableWebPartDescriptions.Contains(description))
			{
				throw new ArgumentException(SR.GetString("CatalogPart_UnknownDescription"), "description");
			}
			return description.WebPart;
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x000E4954 File Offset: 0x000E2B54
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (base.WebPartManager != null)
			{
				base.WebPartManager.WebPartAdded += this.OnWebPartsChanged;
				base.WebPartManager.WebPartClosed += this.OnWebPartsChanged;
				base.WebPartManager.WebPartDeleted += this.OnWebPartsChanged;
			}
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x000E49B5 File Offset: 0x000E2BB5
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this._availableWebPartDescriptions = null;
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x000E49C5 File Offset: 0x000E2BC5
		private void OnWebPartsChanged(object sender, WebPartEventArgs e)
		{
			this._availableWebPartDescriptions = null;
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x00006164 File Offset: 0x00004364
		protected internal override void Render(HtmlTextWriter writer)
		{
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x000E1D94 File Offset: 0x000DFF94
		// (set) Token: 0x06004513 RID: 17683 RVA: 0x000E1D9C File Offset: 0x000DFF9C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override string AccessKey
		{
			get
			{
				return base.AccessKey;
			}
			set
			{
				base.AccessKey = value;
			}
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x06004514 RID: 17684 RVA: 0x000E1DA5 File Offset: 0x000DFFA5
		// (set) Token: 0x06004515 RID: 17685 RVA: 0x000E1DAD File Offset: 0x000DFFAD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x000E1DB6 File Offset: 0x000DFFB6
		// (set) Token: 0x06004517 RID: 17687 RVA: 0x000E1DBE File Offset: 0x000DFFBE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override string BackImageUrl
		{
			get
			{
				return base.BackImageUrl;
			}
			set
			{
				base.BackImageUrl = value;
			}
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x06004518 RID: 17688 RVA: 0x0009E7D8 File Offset: 0x0009C9D8
		// (set) Token: 0x06004519 RID: 17689 RVA: 0x0009E7E0 File Offset: 0x0009C9E0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
			}
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x0600451A RID: 17690 RVA: 0x0009E7E9 File Offset: 0x0009C9E9
		// (set) Token: 0x0600451B RID: 17691 RVA: 0x0009E7F1 File Offset: 0x0009C9F1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x0600451C RID: 17692 RVA: 0x0009E7FA File Offset: 0x0009C9FA
		// (set) Token: 0x0600451D RID: 17693 RVA: 0x0009E802 File Offset: 0x0009CA02
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override Unit BorderWidth
		{
			get
			{
				return base.BorderWidth;
			}
			set
			{
				base.BorderWidth = value;
			}
		}

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x0600451E RID: 17694 RVA: 0x000E1DC7 File Offset: 0x000DFFC7
		// (set) Token: 0x0600451F RID: 17695 RVA: 0x000E1DCF File Offset: 0x000DFFCF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		[CssClassProperty]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				base.CssClass = value;
			}
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x06004520 RID: 17696 RVA: 0x000D9E7A File Offset: 0x000D807A
		// (set) Token: 0x06004521 RID: 17697 RVA: 0x000D9E82 File Offset: 0x000D8082
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override string DefaultButton
		{
			get
			{
				return base.DefaultButton;
			}
			set
			{
				base.DefaultButton = value;
			}
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x06004522 RID: 17698 RVA: 0x000E1DD8 File Offset: 0x000DFFD8
		// (set) Token: 0x06004523 RID: 17699 RVA: 0x000E1DE0 File Offset: 0x000DFFE0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override ContentDirection Direction
		{
			get
			{
				return base.Direction;
			}
			set
			{
				base.Direction = value;
			}
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x00085F4D File Offset: 0x0008414D
		// (set) Token: 0x06004525 RID: 17701 RVA: 0x000AAFC8 File Offset: 0x000A91C8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x06004526 RID: 17702 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06004527 RID: 17703 RVA: 0x000610E7 File Offset: 0x0005F2E7
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override bool EnableTheming
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NoThemingSupport", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x00083455 File Offset: 0x00081655
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x06004529 RID: 17705 RVA: 0x00085E74 File Offset: 0x00084074
		// (set) Token: 0x0600452A RID: 17706 RVA: 0x000E1DE9 File Offset: 0x000DFFE9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x0600452B RID: 17707 RVA: 0x000E1DF2 File Offset: 0x000DFFF2
		// (set) Token: 0x0600452C RID: 17708 RVA: 0x000E1DFA File Offset: 0x000DFFFA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override string GroupingText
		{
			get
			{
				return base.GroupingText;
			}
			set
			{
				base.GroupingText = value;
			}
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x0600452D RID: 17709 RVA: 0x000E1E03 File Offset: 0x000E0003
		// (set) Token: 0x0600452E RID: 17710 RVA: 0x000E1E0B File Offset: 0x000E000B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x0600452F RID: 17711 RVA: 0x000E1E14 File Offset: 0x000E0014
		// (set) Token: 0x06004530 RID: 17712 RVA: 0x000E1E1C File Offset: 0x000E001C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override HorizontalAlign HorizontalAlign
		{
			get
			{
				return base.HorizontalAlign;
			}
			set
			{
				base.HorizontalAlign = value;
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x06004531 RID: 17713 RVA: 0x000E1E25 File Offset: 0x000E0025
		// (set) Token: 0x06004532 RID: 17714 RVA: 0x000E1E2D File Offset: 0x000E002D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override ScrollBars ScrollBars
		{
			get
			{
				return base.ScrollBars;
			}
			set
			{
				base.ScrollBars = value;
			}
		}

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x06004533 RID: 17715 RVA: 0x00028752 File Offset: 0x00026952
		// (set) Token: 0x06004534 RID: 17716 RVA: 0x000610E7 File Offset: 0x0005F2E7
		[Browsable(false)]
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override string SkinID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NoThemingSupport", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x06004535 RID: 17717 RVA: 0x000E1E36 File Offset: 0x000E0036
		// (set) Token: 0x06004536 RID: 17718 RVA: 0x000E1E3E File Offset: 0x000E003E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override short TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x06004537 RID: 17719 RVA: 0x000E1E47 File Offset: 0x000E0047
		// (set) Token: 0x06004538 RID: 17720 RVA: 0x000E1E4F File Offset: 0x000E004F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override string ToolTip
		{
			get
			{
				return base.ToolTip;
			}
			set
			{
				base.ToolTip = value;
			}
		}

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x06004539 RID: 17721 RVA: 0x000698D5 File Offset: 0x00067AD5
		// (set) Token: 0x0600453A RID: 17722 RVA: 0x000698DD File Offset: 0x00067ADD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x0600453B RID: 17723 RVA: 0x000E1E58 File Offset: 0x000E0058
		// (set) Token: 0x0600453C RID: 17724 RVA: 0x000E1E60 File Offset: 0x000E0060
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x0600453D RID: 17725 RVA: 0x000E1E69 File Offset: 0x000E0069
		// (set) Token: 0x0600453E RID: 17726 RVA: 0x000E1E71 File Offset: 0x000E0071
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override bool Wrap
		{
			get
			{
				return base.Wrap;
			}
			set
			{
				base.Wrap = value;
			}
		}

		// Token: 0x04002651 RID: 9809
		private WebPartDescriptionCollection _availableWebPartDescriptions;

		// Token: 0x04002652 RID: 9810
		private static readonly WebPartDescriptionCollection DesignModeAvailableWebParts = new WebPartDescriptionCollection(new WebPartDescription[]
		{
			new WebPartDescription("webpart1", string.Format(CultureInfo.CurrentCulture, SR.GetString("CatalogPart_SampleWebPartTitle"), new object[]
			{
				"1"
			}), null, null),
			new WebPartDescription("webpart2", string.Format(CultureInfo.CurrentCulture, SR.GetString("CatalogPart_SampleWebPartTitle"), new object[]
			{
				"2"
			}), null, null),
			new WebPartDescription("webpart3", string.Format(CultureInfo.CurrentCulture, SR.GetString("CatalogPart_SampleWebPartTitle"), new object[]
			{
				"3"
			}), null, null)
		});
	}
}
