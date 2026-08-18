using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000537 RID: 1335
	[Designer("System.Web.UI.Design.WebControls.WebParts.DeclarativeCatalogPartDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class DeclarativeCatalogPart : CatalogPart
	{
		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x060043FE RID: 17406 RVA: 0x000E1B60 File Offset: 0x000DFD60
		// (set) Token: 0x060043FF RID: 17407 RVA: 0x000D9EF2 File Offset: 0x000D80F2
		[WebSysDefaultValue("DeclarativeCatalogPart_PartTitle")]
		public override string Title
		{
			get
			{
				string text = (string)this.ViewState["Title"];
				if (text == null)
				{
					return SR.GetString("DeclarativeCatalogPart_PartTitle");
				}
				return text;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x000E1B92 File Offset: 0x000DFD92
		// (set) Token: 0x06004401 RID: 17409 RVA: 0x000E1BA8 File Offset: 0x000DFDA8
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UserControlFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
		[UrlProperty]
		[WebCategory("Behavior")]
		[WebSysDescription("DeclarativeCatlaogPart_WebPartsListUserControlPath")]
		public string WebPartsListUserControlPath
		{
			get
			{
				if (this._webPartsListUserControlPath == null)
				{
					return string.Empty;
				}
				return this._webPartsListUserControlPath;
			}
			set
			{
				this._webPartsListUserControlPath = value;
				this._descriptions = null;
			}
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06004402 RID: 17410 RVA: 0x000E1BB8 File Offset: 0x000DFDB8
		// (set) Token: 0x06004403 RID: 17411 RVA: 0x000E1BC0 File Offset: 0x000DFDC0
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DeclarativeCatalogPart))]
		public ITemplate WebPartsTemplate
		{
			get
			{
				return this._webPartsTemplate;
			}
			set
			{
				this._webPartsTemplate = value;
				this._descriptions = null;
			}
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x000E1BD0 File Offset: 0x000DFDD0
		private void AddControlToDescriptions(Control control, ArrayList descriptions)
		{
			WebPart webPart = control as WebPart;
			if (webPart == null && !(control is LiteralControl))
			{
				if (base.WebPartManager != null)
				{
					webPart = base.WebPartManager.CreateWebPart(control);
				}
				else
				{
					webPart = WebPartManager.CreateWebPartStatic(control);
				}
			}
			if (webPart != null && (base.WebPartManager == null || base.WebPartManager.IsAuthorized(webPart)))
			{
				WebPartDescription value = new WebPartDescription(webPart);
				descriptions.Add(value);
			}
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x000E1C35 File Offset: 0x000DFE35
		public override WebPartDescriptionCollection GetAvailableWebPartDescriptions()
		{
			if (this._descriptions == null)
			{
				this.LoadAvailableWebParts();
			}
			return this._descriptions;
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000E1C4C File Offset: 0x000DFE4C
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

		// Token: 0x06004407 RID: 17415 RVA: 0x000E1C94 File Offset: 0x000DFE94
		private void LoadAvailableWebParts()
		{
			ArrayList arrayList = new ArrayList();
			if (this.WebPartsTemplate != null)
			{
				Control control = new NonParentingControl();
				this.WebPartsTemplate.InstantiateIn(control);
				if (control.HasControls())
				{
					Control[] array = new Control[control.Controls.Count];
					control.Controls.CopyTo(array, 0);
					foreach (Control control2 in array)
					{
						this.AddControlToDescriptions(control2, arrayList);
					}
				}
			}
			string webPartsListUserControlPath = this.WebPartsListUserControlPath;
			if (!string.IsNullOrEmpty(webPartsListUserControlPath) && !base.DesignMode)
			{
				Control control3 = this.Page.LoadControl(webPartsListUserControlPath);
				if (control3 != null && control3.HasControls())
				{
					Control[] array3 = new Control[control3.Controls.Count];
					control3.Controls.CopyTo(array3, 0);
					foreach (Control control4 in array3)
					{
						this.AddControlToDescriptions(control4, arrayList);
					}
				}
			}
			this._descriptions = new WebPartDescriptionCollection(arrayList);
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x00006164 File Offset: 0x00004364
		protected internal override void Render(HtmlTextWriter writer)
		{
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06004409 RID: 17417 RVA: 0x000E1D94 File Offset: 0x000DFF94
		// (set) Token: 0x0600440A RID: 17418 RVA: 0x000E1D9C File Offset: 0x000DFF9C
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

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x0600440B RID: 17419 RVA: 0x000E1DA5 File Offset: 0x000DFFA5
		// (set) Token: 0x0600440C RID: 17420 RVA: 0x000E1DAD File Offset: 0x000DFFAD
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

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x0600440D RID: 17421 RVA: 0x000E1DB6 File Offset: 0x000DFFB6
		// (set) Token: 0x0600440E RID: 17422 RVA: 0x000E1DBE File Offset: 0x000DFFBE
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

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x0009E7D8 File Offset: 0x0009C9D8
		// (set) Token: 0x06004410 RID: 17424 RVA: 0x0009E7E0 File Offset: 0x0009C9E0
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

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x06004411 RID: 17425 RVA: 0x0009E7E9 File Offset: 0x0009C9E9
		// (set) Token: 0x06004412 RID: 17426 RVA: 0x0009E7F1 File Offset: 0x0009C9F1
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

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x06004413 RID: 17427 RVA: 0x0009E7FA File Offset: 0x0009C9FA
		// (set) Token: 0x06004414 RID: 17428 RVA: 0x0009E802 File Offset: 0x0009CA02
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

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x000E1DC7 File Offset: 0x000DFFC7
		// (set) Token: 0x06004416 RID: 17430 RVA: 0x000E1DCF File Offset: 0x000DFFCF
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

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x000D9E7A File Offset: 0x000D807A
		// (set) Token: 0x06004418 RID: 17432 RVA: 0x000D9E82 File Offset: 0x000D8082
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

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06004419 RID: 17433 RVA: 0x000E1DD8 File Offset: 0x000DFFD8
		// (set) Token: 0x0600441A RID: 17434 RVA: 0x000E1DE0 File Offset: 0x000DFFE0
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

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x0600441B RID: 17435 RVA: 0x00085F4D File Offset: 0x0008414D
		// (set) Token: 0x0600441C RID: 17436 RVA: 0x000AAFC8 File Offset: 0x000A91C8
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

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x0600441E RID: 17438 RVA: 0x000610E7 File Offset: 0x0005F2E7
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

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x00083455 File Offset: 0x00081655
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

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x06004420 RID: 17440 RVA: 0x00085E74 File Offset: 0x00084074
		// (set) Token: 0x06004421 RID: 17441 RVA: 0x000E1DE9 File Offset: 0x000DFFE9
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

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x06004422 RID: 17442 RVA: 0x000E1DF2 File Offset: 0x000DFFF2
		// (set) Token: 0x06004423 RID: 17443 RVA: 0x000E1DFA File Offset: 0x000DFFFA
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

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06004424 RID: 17444 RVA: 0x000E1E03 File Offset: 0x000E0003
		// (set) Token: 0x06004425 RID: 17445 RVA: 0x000E1E0B File Offset: 0x000E000B
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

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06004426 RID: 17446 RVA: 0x000E1E14 File Offset: 0x000E0014
		// (set) Token: 0x06004427 RID: 17447 RVA: 0x000E1E1C File Offset: 0x000E001C
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

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x06004428 RID: 17448 RVA: 0x000E1E25 File Offset: 0x000E0025
		// (set) Token: 0x06004429 RID: 17449 RVA: 0x000E1E2D File Offset: 0x000E002D
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

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x0600442A RID: 17450 RVA: 0x00028752 File Offset: 0x00026952
		// (set) Token: 0x0600442B RID: 17451 RVA: 0x000610E7 File Offset: 0x0005F2E7
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

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x0600442C RID: 17452 RVA: 0x000E1E36 File Offset: 0x000E0036
		// (set) Token: 0x0600442D RID: 17453 RVA: 0x000E1E3E File Offset: 0x000E003E
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

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x0600442E RID: 17454 RVA: 0x000E1E47 File Offset: 0x000E0047
		// (set) Token: 0x0600442F RID: 17455 RVA: 0x000E1E4F File Offset: 0x000E004F
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

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x06004430 RID: 17456 RVA: 0x000698D5 File Offset: 0x00067AD5
		// (set) Token: 0x06004431 RID: 17457 RVA: 0x000698DD File Offset: 0x00067ADD
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

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x06004432 RID: 17458 RVA: 0x000E1E58 File Offset: 0x000E0058
		// (set) Token: 0x06004433 RID: 17459 RVA: 0x000E1E60 File Offset: 0x000E0060
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

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x000E1E69 File Offset: 0x000E0069
		// (set) Token: 0x06004435 RID: 17461 RVA: 0x000E1E71 File Offset: 0x000E0071
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

		// Token: 0x0400261E RID: 9758
		private ITemplate _webPartsTemplate;

		// Token: 0x0400261F RID: 9759
		private WebPartDescriptionCollection _descriptions;

		// Token: 0x04002620 RID: 9760
		private string _webPartsListUserControlPath;
	}
}
