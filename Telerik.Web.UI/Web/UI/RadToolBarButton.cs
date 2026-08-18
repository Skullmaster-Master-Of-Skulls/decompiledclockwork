using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200093F RID: 2367
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[XmlRoot("Button")]
	public class RadToolBarButton : RadToolBarItem, ICloneable, IRadToolBarButton
	{
		// Token: 0x17001DCC RID: 7628
		// (get) Token: 0x06005A45 RID: 23109 RVA: 0x001122D1 File Offset: 0x001104D1
		internal override RadToolBarItemType ItemType
		{
			get
			{
				return RadToolBarItemType.Button;
			}
		}

		// Token: 0x06005A46 RID: 23110 RVA: 0x001122D4 File Offset: 0x001104D4
		protected override RadToolBarItem.RendererBase CreateRenderer()
		{
			if (base.ToolBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				if (this.IsSeparator)
				{
					return new RadToolBarButton.LiteSeparatorRenderer(this);
				}
				if (this.Templated)
				{
					return new RadToolBarButton.LiteTemplatedButtonRenderer(this);
				}
				if (this.Owner is IRadToolBarButtonContainer)
				{
					return new RadToolBarButton.LiteDropDownItemRenderer(this);
				}
				return new RadToolBarButton.LiteButtonRenderer(this);
			}
			else
			{
				if (this.IsSeparator)
				{
					return new RadToolBarButton.SeparatorRenderer(this);
				}
				if (this.Templated)
				{
					return new RadToolBarButton.TemplatedButtonRenderer(this);
				}
				if (this.Owner is IRadToolBarButtonContainer)
				{
					return new RadToolBarButton.DropDownItemRenderer(this);
				}
				return new RadToolBarButton.ButtonRenderer(this);
			}
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x00112360 File Offset: 0x00110560
		protected override string GetCurrentImageUrl()
		{
			if (string.IsNullOrEmpty(this.ImageUrl))
			{
				return null;
			}
			if (!this.Enabled && !string.IsNullOrEmpty(this.DisabledImageUrl))
			{
				return base.ResolveClientUrl(this.DisabledImageUrl);
			}
			if (this.Checked && !string.IsNullOrEmpty(this.CheckedImageUrl))
			{
				return base.ResolveClientUrl(this.CheckedImageUrl);
			}
			if (!string.IsNullOrEmpty(this.ImageUrl))
			{
				return base.ResolveClientUrl(this.ImageUrl);
			}
			return null;
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x001123DB File Offset: 0x001105DB
		internal void RenderChildControls(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x001123E4 File Offset: 0x001105E4
		protected override ControlItemCollection CreateChildItemCollection()
		{
			throw new InvalidOperationException("RadToolBarButton is not a hierarchical control and does not have children!");
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x001123F0 File Offset: 0x001105F0
		protected internal override void SetItemContainer(ControlItemContainer itemContainer)
		{
			base.SetItemContainer(itemContainer);
			if (this._pendingCheckOnClick)
			{
				this.CheckOnClick = true;
				this._pendingCheckOnClick = false;
			}
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x00112410 File Offset: 0x00110610
		protected internal override void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			base.LoadFromDictionary(dictionary);
			if (dictionary.ContainsKey("navigateUrl"))
			{
				this.NavigateUrl = dictionary["navigateUrl"].ToString();
			}
			if (dictionary.ContainsKey("target"))
			{
				this.Target = dictionary["target"].ToString();
			}
			if (dictionary.ContainsKey("commandName"))
			{
				this.CommandName = dictionary["commandName"].ToString();
			}
			if (dictionary.ContainsKey("commandArgument"))
			{
				this.CommandArgument = dictionary["commandArgument"].ToString();
			}
			if (dictionary.ContainsKey("checkedCssClass"))
			{
				this.CheckedCssClass = dictionary["checkedCssClass"].ToString();
			}
			if (dictionary.ContainsKey("checkedImageUrl"))
			{
				this.CheckedImageUrl = dictionary["checkedImageUrl"].ToString();
			}
			if (dictionary.ContainsKey("checked"))
			{
				this.Checked = (bool)dictionary["checked"];
			}
			if (dictionary.ContainsKey("checkOnClick"))
			{
				this.CheckOnClick = (bool)dictionary["checkOnClick"];
			}
			if (dictionary.ContainsKey("allowSelfUnCheck"))
			{
				this.AllowSelfUnCheck = (bool)dictionary["allowSelfUnCheck"];
			}
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x0011255F File Offset: 0x0011075F
		public RadToolBarButton()
		{
		}

		// Token: 0x06005A4D RID: 23117 RVA: 0x00112567 File Offset: 0x00110767
		public RadToolBarButton(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x00112576 File Offset: 0x00110776
		public RadToolBarButton(string text, bool isChecked, string group) : this(text)
		{
			this.CheckOnClick = true;
			this.Checked = isChecked;
			this.Group = group;
		}

		// Token: 0x17001DCD RID: 7629
		// (get) Token: 0x06005A4F RID: 23119 RVA: 0x00112594 File Offset: 0x00110794
		// (set) Token: 0x06005A50 RID: 23120 RVA: 0x0011259C File Offset: 0x0011079C
		[Browsable(false)]
		[Bindable(false)]
		public IRadToolBarItemContainer Owner
		{
			get
			{
				return this._owner;
			}
			internal set
			{
				this._owner = value;
			}
		}

		// Token: 0x17001DCE RID: 7630
		// (get) Token: 0x06005A51 RID: 23121 RVA: 0x001125A5 File Offset: 0x001107A5
		// (set) Token: 0x06005A52 RID: 23122 RVA: 0x001125AD File Offset: 0x001107AD
		[Browsable(false)]
		public override object DataItem
		{
			get
			{
				return base.DataItem;
			}
			set
			{
				base.DataItem = value;
			}
		}

		// Token: 0x17001DCF RID: 7631
		// (get) Token: 0x06005A53 RID: 23123 RVA: 0x001125B6 File Offset: 0x001107B6
		// (set) Token: 0x06005A54 RID: 23124 RVA: 0x001125D7 File Offset: 0x001107D7
		[DefaultValue(false)]
		[Description("Gets/sets whether the button is separator.")]
		[Category("Behavior")]
		public bool IsSeparator
		{
			get
			{
				return (bool)(this.ViewState["IsSeparator"] ?? false);
			}
			set
			{
				this.ViewState["IsSeparator"] = value;
			}
		}

		// Token: 0x17001DD0 RID: 7632
		// (get) Token: 0x06005A55 RID: 23125 RVA: 0x001125EF File Offset: 0x001107EF
		// (set) Token: 0x06005A56 RID: 23126 RVA: 0x00112610 File Offset: 0x00110810
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Gets or sets whether the button has a check state.")]
		public bool CheckOnClick
		{
			get
			{
				return (bool)(this.ViewState["CheckOnClick"] ?? false);
			}
			set
			{
				if (base.ToolBar == null)
				{
					this._pendingCheckOnClick = value;
					return;
				}
				if (value && this.Checked)
				{
					base.ToolBar.ClearGroupButtonsCheckedState(this.Group);
				}
				this.ViewState["CheckOnClick"] = value;
			}
		}

		// Token: 0x17001DD1 RID: 7633
		// (get) Token: 0x06005A57 RID: 23127 RVA: 0x0011265F File Offset: 0x0011085F
		// (set) Token: 0x06005A58 RID: 23128 RVA: 0x00112680 File Offset: 0x00110880
		[Description("Gets or sets if the button is checked.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return (bool)(this.ViewState["Checked"] ?? false);
			}
			set
			{
				if (base.ToolBar != null && this.CheckOnClick && value)
				{
					base.ToolBar.ClearGroupButtonsCheckedState(this.Group);
				}
				this.ViewState["Checked"] = value;
			}
		}

		// Token: 0x17001DD2 RID: 7634
		// (get) Token: 0x06005A59 RID: 23129 RVA: 0x001126BC File Offset: 0x001108BC
		// (set) Token: 0x06005A5A RID: 23130 RVA: 0x001126DC File Offset: 0x001108DC
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets the group to which the button belongs.")]
		public string Group
		{
			get
			{
				return (string)(this.ViewState["Group"] ?? string.Empty);
			}
			set
			{
				if (base.ToolBar != null && this.CheckOnClick && this.Checked)
				{
					base.ToolBar.ClearGroupButtonsCheckedState(value);
				}
				this.ViewState["Group"] = value;
			}
		}

		// Token: 0x17001DD3 RID: 7635
		// (get) Token: 0x06005A5B RID: 23131 RVA: 0x00112713 File Offset: 0x00110913
		// (set) Token: 0x06005A5C RID: 23132 RVA: 0x00112733 File Offset: 0x00110933
		[Description("CSS class applied to the toolbar button when it is clicked.")]
		[DefaultValue("")]
		[Category("Appearance")]
		public string CheckedCssClass
		{
			get
			{
				return (string)(this.ViewState["CheckedCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["CheckedCssClass"] = value;
			}
		}

		// Token: 0x17001DD4 RID: 7636
		// (get) Token: 0x06005A5D RID: 23133 RVA: 0x00112746 File Offset: 0x00110946
		// (set) Token: 0x06005A5E RID: 23134 RVA: 0x00112766 File Offset: 0x00110966
		[Category("Appearance")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The URL for the image when the button is checked.")]
		[UrlProperty]
		public string CheckedImageUrl
		{
			get
			{
				return (string)(this.ViewState["CheckedImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["CheckedImageUrl"] = value;
			}
		}

		// Token: 0x17001DD5 RID: 7637
		// (get) Token: 0x06005A5F RID: 23135 RVA: 0x00112779 File Offset: 0x00110979
		// (set) Token: 0x06005A60 RID: 23136 RVA: 0x0011279A File Offset: 0x0011099A
		[Description("Gets or sets a value indicating if a checked button will get unchecked when clicked.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool AllowSelfUnCheck
		{
			get
			{
				return (bool)(this.ViewState["AllowSelfUnCheck"] ?? false);
			}
			set
			{
				this.ViewState["AllowSelfUnCheck"] = value;
			}
		}

		// Token: 0x06005A61 RID: 23137 RVA: 0x001127B4 File Offset: 0x001109B4
		public RadToolBarButton Clone()
		{
			return new RadToolBarButton
			{
				Enabled = this.Enabled,
				ImageUrl = this.ImageUrl,
				DisabledImageUrl = this.DisabledImageUrl,
				HoveredImageUrl = this.HoveredImageUrl,
				ClickedImageUrl = base.ClickedImageUrl,
				FocusedImageUrl = this.FocusedImageUrl,
				CheckedImageUrl = this.CheckedImageUrl,
				CssClass = this.CssClass,
				DisabledCssClass = base.DisabledCssClass,
				HoveredCssClass = base.HoveredCssClass,
				ClickedCssClass = base.ClickedCssClass,
				FocusedCssClass = base.FocusedCssClass,
				CheckedCssClass = this.CheckedCssClass,
				SpriteCssClass = base.SpriteCssClass,
				EnableImageSprite = base.EnableImageSprite,
				Checked = this.Checked,
				CheckOnClick = this.CheckOnClick,
				Group = this.Group,
				NavigateUrl = this.NavigateUrl,
				Target = this.Target,
				Text = this.Text,
				ToolTip = this.ToolTip,
				Value = this.Value,
				CausesValidation = this.CausesValidation,
				ValidationGroup = this.ValidationGroup,
				PostBackUrl = this.PostBackUrl,
				CommandName = this.CommandName,
				CommandArgument = this.CommandArgument
			};
		}

		// Token: 0x06005A62 RID: 23138 RVA: 0x00112918 File Offset: 0x00110B18
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x17001DD6 RID: 7638
		// (get) Token: 0x06005A63 RID: 23139 RVA: 0x00112920 File Offset: 0x00110B20
		// (set) Token: 0x06005A64 RID: 23140 RVA: 0x00112928 File Offset: 0x00110B28
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RadToolBarButton))]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Bindable(false)]
		public ITemplate ItemTemplate
		{
			get
			{
				return base.Template;
			}
			set
			{
				base.Template = value;
			}
		}

		// Token: 0x17001DD7 RID: 7639
		// (get) Token: 0x06005A65 RID: 23141 RVA: 0x00112931 File Offset: 0x00110B31
		// (set) Token: 0x06005A66 RID: 23142 RVA: 0x00112952 File Offset: 0x00110B52
		[DefaultValue(true)]
		[Description("Whether the button should postback")]
		public bool PostBack
		{
			get
			{
				return (bool)(this.ViewState["PostBack"] ?? true);
			}
			set
			{
				this.ViewState["PostBack"] = value;
			}
		}

		// Token: 0x17001DD8 RID: 7640
		// (get) Token: 0x06005A67 RID: 23143 RVA: 0x0011296A File Offset: 0x00110B6A
		// (set) Token: 0x06005A68 RID: 23144 RVA: 0x00112972 File Offset: 0x00110B72
		[Description("The value of the toolbar button")]
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x17001DD9 RID: 7641
		// (get) Token: 0x06005A69 RID: 23145 RVA: 0x0011297B File Offset: 0x00110B7B
		// (set) Token: 0x06005A6A RID: 23146 RVA: 0x0011299B File Offset: 0x00110B9B
		[Bindable(true)]
		[DefaultValue("")]
		[Category("Navigation")]
		[Description("The URL to which the toolbar button navigates when selected.")]
		[Editor("Telerik.Web.Design.ControlItemUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		public string NavigateUrl
		{
			get
			{
				return (string)(this.ViewState["NavigateUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x17001DDA RID: 7642
		// (get) Token: 0x06005A6B RID: 23147 RVA: 0x001129AE File Offset: 0x00110BAE
		// (set) Token: 0x06005A6C RID: 23148 RVA: 0x001129CE File Offset: 0x00110BCE
		[Description("The navigation target used when the toolbar button is selected.")]
		[DefaultValue("")]
		[Category("Navigation")]
		[TypeConverter(typeof(TargetConverter))]
		public string Target
		{
			get
			{
				return (string)(this.ViewState["Target"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x17001DDB RID: 7643
		// (get) Token: 0x06005A6D RID: 23149 RVA: 0x001129E1 File Offset: 0x00110BE1
		// (set) Token: 0x06005A6E RID: 23150 RVA: 0x00112A01 File Offset: 0x00110C01
		[Category("Behavior")]
		[DefaultValue("")]
		public string CommandName
		{
			get
			{
				return (string)(this.ViewState["CommandName"] ?? string.Empty);
			}
			set
			{
				this.ViewState["CommandName"] = value;
			}
		}

		// Token: 0x17001DDC RID: 7644
		// (get) Token: 0x06005A6F RID: 23151 RVA: 0x00112A14 File Offset: 0x00110C14
		// (set) Token: 0x06005A70 RID: 23152 RVA: 0x00112A34 File Offset: 0x00110C34
		[DefaultValue("")]
		[Category("Behavior")]
		public string CommandArgument
		{
			get
			{
				return (string)(this.ViewState["CommandArgument"] ?? string.Empty);
			}
			set
			{
				this.ViewState["CommandArgument"] = value;
			}
		}

		// Token: 0x17001DDD RID: 7645
		// (get) Token: 0x06005A71 RID: 23153 RVA: 0x00112A47 File Offset: 0x00110C47
		// (set) Token: 0x06005A72 RID: 23154 RVA: 0x00112A68 File Offset: 0x00110C68
		[DefaultValue(true)]
		[Description("Gets or sets if validation is performed when the RadToolBarButton is clicked.")]
		public bool CausesValidation
		{
			get
			{
				return (bool)(this.ViewState["CausesValidation"] ?? true);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		// Token: 0x17001DDE RID: 7646
		// (get) Token: 0x06005A73 RID: 23155 RVA: 0x00112A80 File Offset: 0x00110C80
		// (set) Token: 0x06005A74 RID: 23156 RVA: 0x00112AA0 File Offset: 0x00110CA0
		[Category("Behavior")]
		[Description("Gets or sets the name of the validation group to which the RadToolBarButton belongs.")]
		[DefaultValue("")]
		public string ValidationGroup
		{
			get
			{
				return (string)(this.ViewState["ValidationGroup"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x17001DDF RID: 7647
		// (get) Token: 0x06005A75 RID: 23157 RVA: 0x00112AB3 File Offset: 0x00110CB3
		// (set) Token: 0x06005A76 RID: 23158 RVA: 0x00112AD3 File Offset: 0x00110CD3
		[Description("Gets or sets the URL of the page to post to from the current page.")]
		[Editor("Telerik.Web.Design.ControlItemUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty("*.aspx")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string PostBackUrl
		{
			get
			{
				return (string)(this.ViewState["PostBackUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		// Token: 0x040015D4 RID: 5588
		private IRadToolBarItemContainer _owner;

		// Token: 0x040015D5 RID: 5589
		private bool _pendingCheckOnClick;

		// Token: 0x02000940 RID: 2368
		private class LiteButtonRenderer : RadToolBarItem.LiteRenderer
		{
			// Token: 0x17001DE0 RID: 7648
			// (get) Token: 0x06005A77 RID: 23159 RVA: 0x00112AE6 File Offset: 0x00110CE6
			// (set) Token: 0x06005A78 RID: 23160 RVA: 0x00112AEE File Offset: 0x00110CEE
			private Unit ButtonWidth
			{
				get
				{
					return this._buttonWidth;
				}
				set
				{
					this._buttonWidth = value;
				}
			}

			// Token: 0x17001DE1 RID: 7649
			// (get) Token: 0x06005A79 RID: 23161 RVA: 0x00112AF7 File Offset: 0x00110CF7
			private RadToolBarButton Button
			{
				get
				{
					if (this._button == null)
					{
						this._button = (RadToolBarButton)base.Item;
					}
					return this._button;
				}
			}

			// Token: 0x06005A7A RID: 23162 RVA: 0x00112B18 File Offset: 0x00110D18
			public LiteButtonRenderer(RadToolBarButton button) : base(button)
			{
			}

			// Token: 0x06005A7B RID: 23163 RVA: 0x00112B34 File Offset: 0x00110D34
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				string text = ToolBarStyles.Combine(new string[]
				{
					"rtbLI",
					"rtbItem",
					this.Button.OuterCssClass
				});
				if (this.Button.CheckOnClick && this.Button.Checked)
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						"rtbChecked"
					});
					if (!string.IsNullOrEmpty(this.Button.CheckedCssClass))
					{
						text = ToolBarStyles.Combine(new string[]
						{
							text,
							this.Button.CheckedCssClass
						});
					}
				}
				string disabledClasses = base.GetDisabledClasses();
				if (!string.IsNullOrEmpty(disabledClasses))
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						disabledClasses
					});
				}
				base.SetClassName(writer, text);
				base.AddAttributesToRender(writer);
			}

			// Token: 0x06005A7C RID: 23164 RVA: 0x00112C10 File Offset: 0x00110E10
			public override void RenderContents(HtmlTextWriter writer)
			{
				if (!this.Button.Width.IsEmpty)
				{
					this.ButtonWidth = this.Button.Width;
					this.Button.Width = Unit.Empty;
				}
				if (!this.Button.Height.IsEmpty && this.Button.Height.Type == UnitType.Pixel)
				{
					this.ButtonHeight = (int)this.Button.Height.Value - 6;
				}
				if (!this.ButtonWidth.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.ButtonWidth.ToString());
				}
				if (this.ButtonHeight > 0)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.ButtonHeight + "px");
					writer.AddStyleAttribute("line-height", this.ButtonHeight + "px");
				}
				string text = ToolBarStyles.Combine(new string[]
				{
					this.Button.CssClass,
					"rtbButton",
					base.GetInnerItemElementClass()
				});
				if ((string.IsNullOrEmpty(this.Button.Text) || this.Button.ShowText == ToolBarShowPosition.OverFlow) && (!string.IsNullOrEmpty(this.Button.ImageUrl) || this.Button.EnableImageSpriteResolved) && this.Button.ShowImage != ToolBarShowPosition.OverFlow)
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						"rtbIconOnly"
					});
				}
				this.Button.CssClass = text;
				this.Button.AddAttributes(writer);
				base.ApplyLinkAttributes(writer);
				if (string.IsNullOrEmpty(this.Button.NavigateUrl))
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
				}
				else
				{
					writer.RenderBeginTag(HtmlTextWriterTag.A);
				}
				this.RenderLinkContent(writer);
				writer.RenderEndTag();
			}

			// Token: 0x06005A7D RID: 23165 RVA: 0x00112DFB File Offset: 0x00110FFB
			private void RenderLinkContent(HtmlTextWriter writer)
			{
				base.RenderImageAndTextElements(writer);
			}

			// Token: 0x040015D6 RID: 5590
			private Unit _buttonWidth = Unit.Empty;

			// Token: 0x040015D7 RID: 5591
			private RadToolBarButton _button;

			// Token: 0x040015D8 RID: 5592
			private int ButtonHeight = -1;
		}

		// Token: 0x02000941 RID: 2369
		private class LiteDropDownItemRenderer : RadToolBarItem.LiteRenderer
		{
			// Token: 0x17001DE2 RID: 7650
			// (get) Token: 0x06005A7E RID: 23166 RVA: 0x00112E04 File Offset: 0x00111004
			private RadToolBarButton Button
			{
				get
				{
					if (this._button == null)
					{
						this._button = (RadToolBarButton)base.Item;
					}
					return this._button;
				}
			}

			// Token: 0x06005A7F RID: 23167 RVA: 0x00112E25 File Offset: 0x00111025
			public LiteDropDownItemRenderer(RadToolBarButton button) : base(button)
			{
			}

			// Token: 0x06005A80 RID: 23168 RVA: 0x00112E30 File Offset: 0x00111030
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				string text = ToolBarStyles.Combine(new string[]
				{
					"rtbLI",
					"rtbItem",
					this.Button.OuterCssClass
				});
				if (this.Button.CheckOnClick && this.Button.Checked)
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						"rtbChecked"
					});
					if (!string.IsNullOrEmpty(this.Button.CheckedCssClass))
					{
						text = ToolBarStyles.Combine(new string[]
						{
							text,
							this.Button.CheckedCssClass
						});
					}
				}
				string disabledClasses = base.GetDisabledClasses();
				if (!string.IsNullOrEmpty(disabledClasses))
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						disabledClasses
					});
				}
				base.SetClassName(writer, text);
				base.AddAttributesToRender(writer);
			}

			// Token: 0x06005A81 RID: 23169 RVA: 0x00112F0C File Offset: 0x0011110C
			public override void RenderContents(HtmlTextWriter writer)
			{
				base.ApplyLinkAttributes(writer);
				string text = ToolBarStyles.Combine(new string[]
				{
					this.Button.CssClass,
					"rtbLink"
				});
				if ((string.IsNullOrEmpty(this.Button.Text) || this.Button.ShowText == ToolBarShowPosition.OverFlow) && (!string.IsNullOrEmpty(this.Button.ImageUrl) || this.Button.EnableImageSpriteResolved) && this.Button.ShowImage != ToolBarShowPosition.OverFlow)
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						"rtbIconOnly"
					});
				}
				this.Button.CssClass = text;
				this.Button.AddAttributes(writer);
				if (string.IsNullOrEmpty(this.Button.NavigateUrl))
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
				}
				else
				{
					writer.RenderBeginTag(HtmlTextWriterTag.A);
				}
				base.RenderImageAndTextElements(writer);
				writer.RenderEndTag();
			}

			// Token: 0x040015D9 RID: 5593
			private RadToolBarButton _button;
		}

		// Token: 0x02000942 RID: 2370
		private class LiteSeparatorRenderer : RadToolBarItem.LiteRenderer
		{
			// Token: 0x06005A82 RID: 23170 RVA: 0x00112FF3 File Offset: 0x001111F3
			public LiteSeparatorRenderer(RadToolBarButton button) : base(button)
			{
			}

			// Token: 0x06005A83 RID: 23171 RVA: 0x00112FFC File Offset: 0x001111FC
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				base.Item.CssClass = ToolBarStyles.Combine(new string[]
				{
					"rtbLI",
					"rtbSeparator",
					base.Item.CssClass
				});
				base.Item.AddAttributes(writer);
				base.AddAttributesToRender(writer);
			}

			// Token: 0x06005A84 RID: 23172 RVA: 0x00113052 File Offset: 0x00111252
			public override void RenderContents(HtmlTextWriter writer)
			{
			}
		}

		// Token: 0x02000943 RID: 2371
		private class LiteTemplatedButtonRenderer : RadToolBarItem.LiteRenderer
		{
			// Token: 0x17001DE3 RID: 7651
			// (get) Token: 0x06005A85 RID: 23173 RVA: 0x00113054 File Offset: 0x00111254
			private RadToolBarButton Button
			{
				get
				{
					if (this._button == null)
					{
						this._button = (RadToolBarButton)base.Item;
					}
					return this._button;
				}
			}

			// Token: 0x06005A86 RID: 23174 RVA: 0x00113075 File Offset: 0x00111275
			public LiteTemplatedButtonRenderer(RadToolBarButton button) : base(button)
			{
			}

			// Token: 0x06005A87 RID: 23175 RVA: 0x00113080 File Offset: 0x00111280
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				base.SetClassName(writer, ToolBarStyles.Combine(new string[]
				{
					"rtbLI",
					"rtbItem",
					"rtbTemplate",
					base.Item.CssClass,
					base.Item.OuterCssClass
				}));
				base.AddAttributesToRender(writer);
			}

			// Token: 0x06005A88 RID: 23176 RVA: 0x001130DC File Offset: 0x001112DC
			public override void RenderContents(HtmlTextWriter writer)
			{
				this.Button.RenderChildControls(writer);
			}

			// Token: 0x040015DA RID: 5594
			private RadToolBarButton _button;
		}

		// Token: 0x02000944 RID: 2372
		private class ButtonRenderer : RadToolBarItem.Renderer
		{
			// Token: 0x17001DE4 RID: 7652
			// (get) Token: 0x06005A89 RID: 23177 RVA: 0x001130EA File Offset: 0x001112EA
			// (set) Token: 0x06005A8A RID: 23178 RVA: 0x001130F2 File Offset: 0x001112F2
			private Unit ButtonWidth
			{
				get
				{
					return this._buttonWidth;
				}
				set
				{
					this._buttonWidth = value;
				}
			}

			// Token: 0x17001DE5 RID: 7653
			// (get) Token: 0x06005A8B RID: 23179 RVA: 0x001130FB File Offset: 0x001112FB
			private RadToolBarButton Button
			{
				get
				{
					if (this._button == null)
					{
						this._button = (RadToolBarButton)base.Item;
					}
					return this._button;
				}
			}

			// Token: 0x06005A8C RID: 23180 RVA: 0x0011311C File Offset: 0x0011131C
			public ButtonRenderer(RadToolBarButton button) : base(button)
			{
			}

			// Token: 0x06005A8D RID: 23181 RVA: 0x00113138 File Offset: 0x00111338
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				string text = ToolBarStyles.Combine(new string[]
				{
					"rtbItem",
					"rtbBtn",
					this.Button.OuterCssClass
				});
				if (this.Button.CheckOnClick && this.Button.Checked)
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						"rtbChecked"
					});
					if (!string.IsNullOrEmpty(this.Button.CheckedCssClass))
					{
						text = ToolBarStyles.Combine(new string[]
						{
							text,
							this.Button.CheckedCssClass
						});
					}
				}
				string disabledClasses = base.GetDisabledClasses();
				if (!string.IsNullOrEmpty(disabledClasses))
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						disabledClasses
					});
				}
				base.SetClassName(writer, text);
				base.AddAttributesToRender(writer);
			}

			// Token: 0x06005A8E RID: 23182 RVA: 0x00113214 File Offset: 0x00111414
			public override void RenderContents(HtmlTextWriter writer)
			{
				if (!this.Button.Width.IsEmpty)
				{
					this.ButtonWidth = this.Button.Width;
					this.Button.Width = Unit.Empty;
				}
				if (!this.Button.Height.IsEmpty && this.Button.Height.Type == UnitType.Pixel)
				{
					this.ButtonHeight = (int)this.Button.Height.Value - 6;
				}
				string text = ToolBarStyles.Combine(new string[]
				{
					this.Button.CssClass,
					"rtbWrap"
				});
				if (string.IsNullOrEmpty(this.Button.Text) && (!string.IsNullOrEmpty(this.Button.ImageUrl) || this.Button.EnableImageSpriteResolved))
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						"rtbIconOnly"
					});
				}
				this.Button.CssClass = text;
				this.Button.AddAttributes(writer);
				base.ApplyLinkAttributes(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				this.RenderLinkContent(writer);
				writer.RenderEndTag();
			}

			// Token: 0x06005A8F RID: 23183 RVA: 0x0011334C File Offset: 0x0011154C
			private void RenderLinkContent(HtmlTextWriter writer)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbOut");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbMid");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				if (!this.ButtonWidth.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.ButtonWidth.ToString());
				}
				if (this.ButtonHeight > 0)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.ButtonHeight + "px");
					writer.AddStyleAttribute("line-height", this.ButtonHeight + "px");
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, base.GetInnerItemElementClass());
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				base.RenderImageAndTextElements(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}

			// Token: 0x040015DB RID: 5595
			private Unit _buttonWidth = Unit.Empty;

			// Token: 0x040015DC RID: 5596
			private RadToolBarButton _button;

			// Token: 0x040015DD RID: 5597
			private int ButtonHeight = -1;
		}

		// Token: 0x02000945 RID: 2373
		private class DropDownItemRenderer : RadToolBarItem.Renderer
		{
			// Token: 0x17001DE6 RID: 7654
			// (get) Token: 0x06005A90 RID: 23184 RVA: 0x00113424 File Offset: 0x00111624
			private RadToolBarButton Button
			{
				get
				{
					if (this._button == null)
					{
						this._button = (RadToolBarButton)base.Item;
					}
					return this._button;
				}
			}

			// Token: 0x06005A91 RID: 23185 RVA: 0x00113445 File Offset: 0x00111645
			public DropDownItemRenderer(RadToolBarButton button) : base(button)
			{
			}

			// Token: 0x06005A92 RID: 23186 RVA: 0x00113450 File Offset: 0x00111650
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				string text = ToolBarStyles.Combine(new string[]
				{
					"rtbItem",
					this.Button.OuterCssClass
				});
				if (this.Button.CheckOnClick && this.Button.Checked)
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						"rtbChecked"
					});
					if (!string.IsNullOrEmpty(this.Button.CheckedCssClass))
					{
						text = ToolBarStyles.Combine(new string[]
						{
							text,
							this.Button.CheckedCssClass
						});
					}
				}
				string disabledClasses = base.GetDisabledClasses();
				if (!string.IsNullOrEmpty(disabledClasses))
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						disabledClasses
					});
				}
				base.SetClassName(writer, text);
				base.AddAttributesToRender(writer);
			}

			// Token: 0x06005A93 RID: 23187 RVA: 0x00113524 File Offset: 0x00111724
			public override void RenderContents(HtmlTextWriter writer)
			{
				base.ApplyLinkAttributes(writer);
				string text = ToolBarStyles.Combine(new string[]
				{
					this.Button.CssClass,
					"rtbWrap"
				});
				if (string.IsNullOrEmpty(this.Button.Text) && (!string.IsNullOrEmpty(this.Button.ImageUrl) || this.Button.EnableImageSpriteResolved))
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						"rtbIconOnly"
					});
				}
				this.Button.CssClass = text;
				this.Button.AddAttributes(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				base.RenderImageAndTextElements(writer);
				writer.RenderEndTag();
			}

			// Token: 0x040015DE RID: 5598
			private RadToolBarButton _button;
		}

		// Token: 0x02000946 RID: 2374
		private class SeparatorRenderer : RadToolBarItem.Renderer
		{
			// Token: 0x06005A94 RID: 23188 RVA: 0x001135D3 File Offset: 0x001117D3
			public SeparatorRenderer(RadToolBarButton button) : base(button)
			{
			}

			// Token: 0x06005A95 RID: 23189 RVA: 0x001135DC File Offset: 0x001117DC
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				base.Item.CssClass = ToolBarStyles.Combine(new string[]
				{
					base.Item.CssClass,
					"rtbSeparator"
				});
				base.Item.AddAttributes(writer);
				base.AddAttributesToRender(writer);
			}

			// Token: 0x06005A96 RID: 23190 RVA: 0x0011362A File Offset: 0x0011182A
			public override void RenderContents(HtmlTextWriter writer)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbText");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
			}
		}

		// Token: 0x02000947 RID: 2375
		private class TemplatedButtonRenderer : RadToolBarItem.Renderer
		{
			// Token: 0x17001DE7 RID: 7655
			// (get) Token: 0x06005A97 RID: 23191 RVA: 0x00113647 File Offset: 0x00111847
			private RadToolBarButton Button
			{
				get
				{
					if (this._button == null)
					{
						this._button = (RadToolBarButton)base.Item;
					}
					return this._button;
				}
			}

			// Token: 0x06005A98 RID: 23192 RVA: 0x00113668 File Offset: 0x00111868
			public TemplatedButtonRenderer(RadToolBarButton button) : base(button)
			{
			}

			// Token: 0x06005A99 RID: 23193 RVA: 0x00113674 File Offset: 0x00111874
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				base.SetClassName(writer, ToolBarStyles.Combine(new string[]
				{
					"rtbItem",
					"rtbTemplate",
					base.Item.CssClass,
					base.Item.OuterCssClass
				}));
				base.AddAttributesToRender(writer);
			}

			// Token: 0x06005A9A RID: 23194 RVA: 0x001136C8 File Offset: 0x001118C8
			public override void RenderContents(HtmlTextWriter writer)
			{
				this.Button.RenderChildControls(writer);
			}

			// Token: 0x040015DF RID: 5599
			private RadToolBarButton _button;
		}
	}
}
