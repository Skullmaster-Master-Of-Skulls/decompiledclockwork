using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200094D RID: 2381
	[DefaultProperty("Text")]
	[XmlRoot("SplitButton")]
	[ToolboxItem(false)]
	public class RadToolBarSplitButton : RadToolBarItem, IRadToolBarButtonContainer, IRadToolBarItemContainer, IControlItemContainer, IRadToolBarButton
	{
		// Token: 0x17001DF8 RID: 7672
		// (get) Token: 0x06005AC8 RID: 23240 RVA: 0x00113F4B File Offset: 0x0011214B
		internal override RadToolBarItemType ItemType
		{
			get
			{
				return RadToolBarItemType.SplitButton;
			}
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x00113F4E File Offset: 0x0011214E
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadToolBarButtonCollection(this);
		}

		// Token: 0x06005ACA RID: 23242 RVA: 0x00113F56 File Offset: 0x00112156
		protected internal override void SetItemContainer(ControlItemContainer itemContainer)
		{
			base.SetItemContainer(itemContainer);
			this.Buttons.SetItemContainer(itemContainer);
		}

		// Token: 0x06005ACB RID: 23243 RVA: 0x00113F6C File Offset: 0x0011216C
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
			if (dictionary.ContainsKey("enableDefaultButton"))
			{
				this.EnableDefaultButton = (bool)dictionary["enableDefaultButton"];
			}
			if (dictionary.ContainsKey("defaultButtonIndex"))
			{
				this.DefaultButtonIndex = (int)dictionary["defaultButtonIndex"];
			}
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x00114052 File Offset: 0x00112252
		protected override void ReadXml(XmlReader reader)
		{
			base.ReadXml(reader);
			base.ReadXmlForChildren(reader);
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x00114062 File Offset: 0x00112262
		protected override void WriteXml(XmlWriter writer)
		{
			base.WriteXml(writer);
			RadToolBarItem.WriteXmlForChildren(writer, this.Buttons);
		}

		// Token: 0x06005ACE RID: 23246 RVA: 0x00114078 File Offset: 0x00112278
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
			if (!string.IsNullOrEmpty(this.ImageUrl))
			{
				return base.ResolveClientUrl(this.ImageUrl);
			}
			return null;
		}

		// Token: 0x06005ACF RID: 23247 RVA: 0x001140D1 File Offset: 0x001122D1
		protected override RadToolBarItem.RendererBase CreateRenderer()
		{
			if (base.ToolBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				if (this.Templated)
				{
					return new RadToolBarSplitButton.LiteTemplatedSplitButtonRenderer(this);
				}
				return new RadToolBarSplitButton.LiteSplitButtonRenderer(this);
			}
			else
			{
				if (this.Templated)
				{
					return new RadToolBarSplitButton.TemplatedSplitButtonRenderer(this);
				}
				return new RadToolBarSplitButton.SplitButtonRenderer(this);
			}
		}

		// Token: 0x06005AD0 RID: 23248 RVA: 0x0011410C File Offset: 0x0011230C
		private RadToolBarButton GetDefaultButton()
		{
			int index = 0;
			if (this.DefaultButtonIndex < this.Buttons.Count && this.DefaultButtonIndex >= 0)
			{
				index = this.DefaultButtonIndex;
			}
			if (this.Buttons.Count > 0)
			{
				return this.Buttons[index];
			}
			return null;
		}

		// Token: 0x06005AD1 RID: 23249 RVA: 0x0011415A File Offset: 0x0011235A
		internal void RenderChildControls(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
		}

		// Token: 0x06005AD2 RID: 23250 RVA: 0x00114163 File Offset: 0x00112363
		protected override void LoadChildViewState(object viewState)
		{
			if (viewState == null)
			{
				base.Children.Clear();
				return;
			}
			((IStateManager)base.Children).LoadViewState(viewState);
		}

		// Token: 0x06005AD3 RID: 23251 RVA: 0x00114180 File Offset: 0x00112380
		protected override object SaveChildViewState()
		{
			return ((IStateManager)base.Children).SaveViewState();
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x0011418D File Offset: 0x0011238D
		protected override void TrackChildViewState()
		{
			((IStateManager)base.Children).TrackViewState();
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x0011419A File Offset: 0x0011239A
		protected override void SetChildrenDirty()
		{
			base.Children.SetDirty();
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x001141A7 File Offset: 0x001123A7
		public RadToolBarSplitButton()
		{
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x001141AF File Offset: 0x001123AF
		public RadToolBarSplitButton(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x17001DF9 RID: 7673
		// (get) Token: 0x06005AD8 RID: 23256 RVA: 0x001141BE File Offset: 0x001123BE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		RadToolBarItemCollection IRadToolBarItemContainer.Items
		{
			get
			{
				throw new InvalidOperationException("The Items property of the RadToolBarSplitButton is not available. User Buttons instead.");
			}
		}

		// Token: 0x17001DFA RID: 7674
		// (get) Token: 0x06005AD9 RID: 23257 RVA: 0x001141CA File Offset: 0x001123CA
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadToolBarButtonCollection Buttons
		{
			get
			{
				return (RadToolBarButtonCollection)base.Children;
			}
		}

		// Token: 0x17001DFB RID: 7675
		// (get) Token: 0x06005ADA RID: 23258 RVA: 0x001141D7 File Offset: 0x001123D7
		// (set) Token: 0x06005ADB RID: 23259 RVA: 0x001141F8 File Offset: 0x001123F8
		[DefaultValue(ToolBarDropDownExpandDirection.Down)]
		[Description("The expand direction of the drop down.")]
		[Category("Layout")]
		public ToolBarDropDownExpandDirection ExpandDirection
		{
			get
			{
				return (ToolBarDropDownExpandDirection)(this.ViewState["ExpandDirection"] ?? ToolBarDropDownExpandDirection.Down);
			}
			set
			{
				this.ViewState["ExpandDirection"] = value;
			}
		}

		// Token: 0x17001DFC RID: 7676
		// (get) Token: 0x06005ADC RID: 23260 RVA: 0x00114210 File Offset: 0x00112410
		// (set) Token: 0x06005ADD RID: 23261 RVA: 0x00114235 File Offset: 0x00112435
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[Bindable(true)]
		public Unit DropDownWidth
		{
			get
			{
				return (Unit)(this.ViewState["DropDownWidth"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DropDownWidth"] = value;
			}
		}

		// Token: 0x17001DFD RID: 7677
		// (get) Token: 0x06005ADE RID: 23262 RVA: 0x0011426A File Offset: 0x0011246A
		// (set) Token: 0x06005ADF RID: 23263 RVA: 0x0011428F File Offset: 0x0011248F
		[DefaultValue(typeof(Unit), "")]
		[Bindable(true)]
		[Category("Layout")]
		public Unit DropDownHeight
		{
			get
			{
				return (Unit)(this.ViewState["DropDownHeight"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DropDownHeight"] = value;
			}
		}

		// Token: 0x17001DFE RID: 7678
		// (get) Token: 0x06005AE0 RID: 23264 RVA: 0x001142C4 File Offset: 0x001124C4
		// (set) Token: 0x06005AE1 RID: 23265 RVA: 0x001142E5 File Offset: 0x001124E5
		[Description("Gets or sets a value, indicating if the RadToolBarSplitButton will use the DefaultButton behavior.")]
		[Bindable(true)]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool EnableDefaultButton
		{
			get
			{
				return (bool)(this.ViewState["EnableDefaultButton"] ?? true);
			}
			set
			{
				this.ViewState["EnableDefaultButton"] = value;
			}
		}

		// Token: 0x17001DFF RID: 7679
		// (get) Token: 0x06005AE2 RID: 23266 RVA: 0x001142FD File Offset: 0x001124FD
		// (set) Token: 0x06005AE3 RID: 23267 RVA: 0x0011431E File Offset: 0x0011251E
		[DefaultValue(0)]
		[Description("The index of the button which properties will be used by default when the EnableDefaultButton property set to true.")]
		[Bindable(true)]
		[Category("Behavior")]
		public int DefaultButtonIndex
		{
			get
			{
				return (int)(this.ViewState["DefaultButtonIndex"] ?? 0);
			}
			set
			{
				this.ViewState["DefaultButtonIndex"] = value;
			}
		}

		// Token: 0x17001E00 RID: 7680
		// (get) Token: 0x06005AE4 RID: 23268 RVA: 0x00114336 File Offset: 0x00112536
		// (set) Token: 0x06005AE5 RID: 23269 RVA: 0x0011433E File Offset: 0x0011253E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(RadToolBarButton))]
		[Bindable(false)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x17001E01 RID: 7681
		// (get) Token: 0x06005AE6 RID: 23270 RVA: 0x00114347 File Offset: 0x00112547
		// (set) Token: 0x06005AE7 RID: 23271 RVA: 0x00114368 File Offset: 0x00112568
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

		// Token: 0x17001E02 RID: 7682
		// (get) Token: 0x06005AE8 RID: 23272 RVA: 0x00114380 File Offset: 0x00112580
		// (set) Token: 0x06005AE9 RID: 23273 RVA: 0x00114388 File Offset: 0x00112588
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

		// Token: 0x17001E03 RID: 7683
		// (get) Token: 0x06005AEA RID: 23274 RVA: 0x00114391 File Offset: 0x00112591
		// (set) Token: 0x06005AEB RID: 23275 RVA: 0x001143B1 File Offset: 0x001125B1
		[Category("Navigation")]
		[Editor("Telerik.Web.Design.ControlItemUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[Description("The URL to which the toolbar button navigates when selected.")]
		[Bindable(true)]
		[DefaultValue("")]
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

		// Token: 0x17001E04 RID: 7684
		// (get) Token: 0x06005AEC RID: 23276 RVA: 0x001143C4 File Offset: 0x001125C4
		// (set) Token: 0x06005AED RID: 23277 RVA: 0x001143E4 File Offset: 0x001125E4
		[TypeConverter(typeof(TargetConverter))]
		[DefaultValue("")]
		[Category("Navigation")]
		[Description("The navigation target used when the toolbar button is selected.")]
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

		// Token: 0x17001E05 RID: 7685
		// (get) Token: 0x06005AEE RID: 23278 RVA: 0x001143F7 File Offset: 0x001125F7
		// (set) Token: 0x06005AEF RID: 23279 RVA: 0x00114417 File Offset: 0x00112617
		[DefaultValue("")]
		[Category("Behavior")]
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

		// Token: 0x17001E06 RID: 7686
		// (get) Token: 0x06005AF0 RID: 23280 RVA: 0x0011442A File Offset: 0x0011262A
		// (set) Token: 0x06005AF1 RID: 23281 RVA: 0x0011444A File Offset: 0x0011264A
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

		// Token: 0x17001E07 RID: 7687
		// (get) Token: 0x06005AF2 RID: 23282 RVA: 0x0011445D File Offset: 0x0011265D
		// (set) Token: 0x06005AF3 RID: 23283 RVA: 0x0011447E File Offset: 0x0011267E
		[DefaultValue(true)]
		[Description("Gets or sets if validation is performed when the RadToolBarSplitButton is clicked.")]
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

		// Token: 0x17001E08 RID: 7688
		// (get) Token: 0x06005AF4 RID: 23284 RVA: 0x00114496 File Offset: 0x00112696
		// (set) Token: 0x06005AF5 RID: 23285 RVA: 0x001144B6 File Offset: 0x001126B6
		[Description("Gets or sets the name of the validation group to which the RadToolBarSplitButton belongs.")]
		[Category("Behavior")]
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

		// Token: 0x17001E09 RID: 7689
		// (get) Token: 0x06005AF6 RID: 23286 RVA: 0x001144C9 File Offset: 0x001126C9
		// (set) Token: 0x06005AF7 RID: 23287 RVA: 0x001144E9 File Offset: 0x001126E9
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Behavior")]
		[UrlProperty("*.aspx")]
		[Description("Gets or sets the URL of the page to post to from the current page.")]
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

		// Token: 0x17001E0A RID: 7690
		// (get) Token: 0x06005AF8 RID: 23288 RVA: 0x001144FC File Offset: 0x001126FC
		ControlItemCollection IControlItemContainer.Items
		{
			get
			{
				return base.Children;
			}
		}

		// Token: 0x0200094E RID: 2382
		private class LiteSplitButtonRenderer : RadToolBarItem.LiteRenderer
		{
			// Token: 0x17001E0B RID: 7691
			// (get) Token: 0x06005AF9 RID: 23289 RVA: 0x00114504 File Offset: 0x00112704
			private RadToolBarSplitButton Button
			{
				get
				{
					if (this._button == null)
					{
						this._button = (RadToolBarSplitButton)base.Item;
					}
					return this._button;
				}
			}

			// Token: 0x17001E0C RID: 7692
			// (get) Token: 0x06005AFA RID: 23290 RVA: 0x00114528 File Offset: 0x00112728
			private RadToolBarButton DefaultButton
			{
				get
				{
					if (!this._defaultButtonSet)
					{
						if (this.Button.EnableDefaultButton)
						{
							this._defaultButton = this.Button.GetDefaultButton();
							if (this._defaultButton != null)
							{
								this._defaultButton = this._defaultButton.Clone();
							}
						}
						this._defaultButtonSet = true;
					}
					return this._defaultButton;
				}
			}

			// Token: 0x06005AFB RID: 23291 RVA: 0x00114581 File Offset: 0x00112781
			public LiteSplitButtonRenderer(RadToolBarSplitButton splitButton) : base(splitButton)
			{
			}

			// Token: 0x06005AFC RID: 23292 RVA: 0x00114594 File Offset: 0x00112794
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				string text = ToolBarStyles.Combine(new string[]
				{
					"rtbLI",
					"rtbItem",
					this.Button.OuterCssClass
				});
				string disabledClasses;
				if (this.DefaultButton == null)
				{
					disabledClasses = base.GetDisabledClasses();
				}
				else
				{
					this.DefaultButton.Enabled = ((this.Button.ToolBar == null || this.Button.ToolBar.Enabled) && this.Button.Enabled);
					disabledClasses = base.GetDisabledClasses(this.DefaultButton);
				}
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

			// Token: 0x06005AFD RID: 23293 RVA: 0x00114650 File Offset: 0x00112850
			public override void RenderContents(HtmlTextWriter writer)
			{
				string text;
				if (this.Button.ExpandDirection == ToolBarDropDownExpandDirection.Up)
				{
					text = "rtbExpandUp";
				}
				else
				{
					text = "rtbExpandDown";
				}
				if (!this.Button.Height.IsEmpty && this.Button.Height.Type == UnitType.Pixel)
				{
					this.ButtonHeight = (int)this.Button.Height.Value - 6;
				}
				string text2 = string.Empty;
				if (this.DefaultButton == null)
				{
					if ((string.IsNullOrEmpty(this.Button.Text) || this.Button.ShowText == ToolBarShowPosition.OverFlow) && (!string.IsNullOrEmpty(this.Button.ImageUrl) || this.Button.EnableImageSpriteResolved) && this.Button.ShowImage != ToolBarShowPosition.OverFlow)
					{
						text2 = ToolBarStyles.Combine(new string[]
						{
							text2,
							"rtbIconOnly"
						});
					}
					if (this.Button.EnableDefaultButton)
					{
						this.Button.CssClass = string.Empty;
					}
					this.Button.CssClass = ToolBarStyles.Combine(new string[]
					{
						this.Button.CssClass,
						"rtbButton",
						"rtbSplitButton",
						text,
						base.GetInnerItemElementClass(),
						text2
					});
					this.Button.AddAttributes(writer);
					base.ApplyLinkAttributes(writer);
				}
				else
				{
					bool flag = this.DefaultButton.EnableImageSprite || (this.Button.ToolBar != null && this.Button.ToolBar.EnableImageSprites);
					if ((string.IsNullOrEmpty(this.DefaultButton.Text) || this.Button.ShowText == ToolBarShowPosition.OverFlow) && (!string.IsNullOrEmpty(this.DefaultButton.ImageUrl) || flag) && this.Button.ShowImage != ToolBarShowPosition.OverFlow)
					{
						text2 = ToolBarStyles.Combine(new string[]
						{
							text2,
							"rtbIconOnly"
						});
					}
					this.DefaultButton.CssClass = ToolBarStyles.Combine(new string[]
					{
						this.DefaultButton.CssClass,
						"rtbButton",
						"rtbSplitButton",
						text,
						base.GetInnerItemElementClass(),
						text2
					});
					this.DefaultButton.AccessKey = this.Button.AccessKey;
					this.DefaultButton.AddAttributes(writer);
					base.ApplyLinkAttributes(writer, this.DefaultButton);
				}
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
				if (!this.Button.DesignMode)
				{
					base.RenderDropDown(writer, this.Button.Buttons);
				}
			}

			// Token: 0x06005AFE RID: 23294 RVA: 0x00114920 File Offset: 0x00112B20
			private void RenderLinkContent(HtmlTextWriter writer)
			{
				if (this.ButtonHeight > 0)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.ButtonHeight + "px");
					writer.AddStyleAttribute("line-height", this.ButtonHeight + "px");
				}
				if (this.ButtonHeight > 0)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "middle");
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbSplBtnActivator");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				if (this.DefaultButton == null)
				{
					base.RenderImageAndTextElements(writer);
				}
				else
				{
					base.RenderImageAndTextElements(writer, this.DefaultButton);
				}
				writer.RenderEndTag();
				base.RenderChevron(writer);
			}

			// Token: 0x040015E6 RID: 5606
			private RadToolBarSplitButton _button;

			// Token: 0x040015E7 RID: 5607
			private int ButtonHeight = -1;

			// Token: 0x040015E8 RID: 5608
			private RadToolBarButton _defaultButton;

			// Token: 0x040015E9 RID: 5609
			private bool _defaultButtonSet;
		}

		// Token: 0x0200094F RID: 2383
		private class LiteTemplatedSplitButtonRenderer : RadToolBarItem.LiteRenderer
		{
			// Token: 0x17001E0D RID: 7693
			// (get) Token: 0x06005AFF RID: 23295 RVA: 0x001149C9 File Offset: 0x00112BC9
			private RadToolBarSplitButton Button
			{
				get
				{
					if (this._button == null)
					{
						this._button = (RadToolBarSplitButton)base.Item;
					}
					return this._button;
				}
			}

			// Token: 0x06005B00 RID: 23296 RVA: 0x001149EA File Offset: 0x00112BEA
			public LiteTemplatedSplitButtonRenderer(RadToolBarSplitButton splitButton) : base(splitButton)
			{
			}

			// Token: 0x06005B01 RID: 23297 RVA: 0x001149F4 File Offset: 0x00112BF4
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				string className = ToolBarStyles.Combine(new string[]
				{
					"rtbItem",
					"rtbSplBtn",
					"rtbSplBtnTemplate",
					this.Button.OuterCssClass
				});
				base.SetClassName(writer, className);
				base.AddAttributesToRender(writer);
			}

			// Token: 0x06005B02 RID: 23298 RVA: 0x00114A44 File Offset: 0x00112C44
			public override void RenderContents(HtmlTextWriter writer)
			{
				this.RenderButtonPart(writer);
				base.RenderChevron(writer);
				base.RenderDropDown(writer, this.Button.Buttons);
			}

			// Token: 0x06005B03 RID: 23299 RVA: 0x00114A66 File Offset: 0x00112C66
			private void RenderButtonPart(HtmlTextWriter writer)
			{
				base.SetClassName(writer, "rtbButton");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				base.SetClassName(writer, "rtbSplBtnActivator");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				this.RenderChildControls(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}

			// Token: 0x06005B04 RID: 23300 RVA: 0x00114AA4 File Offset: 0x00112CA4
			private void RenderChildControls(HtmlTextWriter writer)
			{
				if (this.Button.Controls.IsReadOnly)
				{
					this.Button.RenderChildControls(writer);
					return;
				}
				foreach (object obj in this.Button.Controls)
				{
					Control control = (Control)obj;
					if (!(control is RadToolBarButton))
					{
						control.RenderControl(writer);
					}
				}
			}

			// Token: 0x040015EA RID: 5610
			private RadToolBarSplitButton _button;
		}

		// Token: 0x02000950 RID: 2384
		private class SplitButtonRenderer : RadToolBarItem.Renderer
		{
			// Token: 0x17001E0E RID: 7694
			// (get) Token: 0x06005B05 RID: 23301 RVA: 0x00114B2C File Offset: 0x00112D2C
			private RadToolBarSplitButton Button
			{
				get
				{
					if (this._button == null)
					{
						this._button = (RadToolBarSplitButton)base.Item;
					}
					return this._button;
				}
			}

			// Token: 0x17001E0F RID: 7695
			// (get) Token: 0x06005B06 RID: 23302 RVA: 0x00114B50 File Offset: 0x00112D50
			private RadToolBarButton DefaultButton
			{
				get
				{
					if (!this._defaultButtonSet)
					{
						if (this.Button.EnableDefaultButton)
						{
							this._defaultButton = this.Button.GetDefaultButton();
							if (this._defaultButton != null)
							{
								this._defaultButton = this._defaultButton.Clone();
							}
						}
						this._defaultButtonSet = true;
					}
					return this._defaultButton;
				}
			}

			// Token: 0x06005B07 RID: 23303 RVA: 0x00114BA9 File Offset: 0x00112DA9
			public SplitButtonRenderer(RadToolBarSplitButton splitButton) : base(splitButton)
			{
			}

			// Token: 0x06005B08 RID: 23304 RVA: 0x00114BBC File Offset: 0x00112DBC
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				string text = ToolBarStyles.Combine(new string[]
				{
					"rtbItem",
					"rtbSplBtn",
					this.Button.OuterCssClass
				});
				string disabledClasses;
				if (this.DefaultButton == null)
				{
					disabledClasses = base.GetDisabledClasses();
				}
				else
				{
					this.DefaultButton.Enabled = ((this.Button.ToolBar == null || this.Button.ToolBar.Enabled) && this.Button.Enabled);
					disabledClasses = base.GetDisabledClasses(this.DefaultButton);
				}
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

			// Token: 0x06005B09 RID: 23305 RVA: 0x00114C78 File Offset: 0x00112E78
			public override void RenderContents(HtmlTextWriter writer)
			{
				string text;
				if (this.Button.ExpandDirection == ToolBarDropDownExpandDirection.Up)
				{
					text = "rtbExpandUp";
				}
				else
				{
					text = "rtbExpandDown";
				}
				if (!this.Button.Height.IsEmpty && this.Button.Height.Type == UnitType.Pixel)
				{
					this.ButtonHeight = (int)this.Button.Height.Value - 6;
				}
				string text2 = string.Empty;
				if (this.DefaultButton == null)
				{
					if ((string.IsNullOrEmpty(this.Button.Text) || this.Button.ShowText == ToolBarShowPosition.OverFlow) && (!string.IsNullOrEmpty(this.Button.ImageUrl) || this.Button.EnableImageSpriteResolved))
					{
						text2 = ToolBarStyles.Combine(new string[]
						{
							text2,
							"rtbIconOnly"
						});
					}
					if (this.Button.EnableDefaultButton)
					{
						this.Button.CssClass = string.Empty;
					}
					this.Button.CssClass = ToolBarStyles.Combine(new string[]
					{
						this.Button.CssClass,
						"rtbWrap",
						text,
						text2
					});
					this.Button.AddAttributes(writer);
					base.ApplyLinkAttributes(writer);
				}
				else
				{
					bool flag = this.DefaultButton.EnableImageSprite || (this.Button.ToolBar != null && this.Button.ToolBar.EnableImageSprites);
					if ((string.IsNullOrEmpty(this.DefaultButton.Text) || this.Button.ShowText == ToolBarShowPosition.OverFlow) && (!string.IsNullOrEmpty(this.DefaultButton.ImageUrl) || flag))
					{
						text2 = ToolBarStyles.Combine(new string[]
						{
							text2,
							"rtbIconOnly"
						});
					}
					this.DefaultButton.CssClass = ToolBarStyles.Combine(new string[]
					{
						this.DefaultButton.CssClass,
						"rtbWrap",
						text,
						text2
					});
					this.DefaultButton.AddAttributes(writer);
					base.ApplyLinkAttributes(writer, this.DefaultButton);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				this.RenderLinkContent(writer);
				writer.RenderEndTag();
				if (!this.Button.DesignMode)
				{
					base.RenderDropDown(writer, this.Button.Buttons);
				}
			}

			// Token: 0x06005B0A RID: 23306 RVA: 0x00114ED4 File Offset: 0x001130D4
			private void RenderLinkContent(HtmlTextWriter writer)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbOut");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbMid");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				if (this.ButtonHeight > 0)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.ButtonHeight + "px");
					writer.AddStyleAttribute("line-height", this.ButtonHeight + "px");
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, base.GetInnerItemElementClass());
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				if (this.ButtonHeight > 0)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "middle");
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbSplBtnActivator");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				if (this.DefaultButton == null)
				{
					base.RenderImageAndTextElements(writer);
				}
				else
				{
					base.RenderImageAndTextElements(writer, this.DefaultButton);
				}
				writer.RenderEndTag();
				base.RenderChevron(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}

			// Token: 0x040015EB RID: 5611
			private RadToolBarSplitButton _button;

			// Token: 0x040015EC RID: 5612
			private int ButtonHeight = -1;

			// Token: 0x040015ED RID: 5613
			private RadToolBarButton _defaultButton;

			// Token: 0x040015EE RID: 5614
			private bool _defaultButtonSet;
		}

		// Token: 0x02000951 RID: 2385
		private class TemplatedSplitButtonRenderer : RadToolBarItem.Renderer
		{
			// Token: 0x17001E10 RID: 7696
			// (get) Token: 0x06005B0B RID: 23307 RVA: 0x00114FCF File Offset: 0x001131CF
			private RadToolBarSplitButton Button
			{
				get
				{
					if (this._button == null)
					{
						this._button = (RadToolBarSplitButton)base.Item;
					}
					return this._button;
				}
			}

			// Token: 0x06005B0C RID: 23308 RVA: 0x00114FF0 File Offset: 0x001131F0
			public TemplatedSplitButtonRenderer(RadToolBarSplitButton splitButton) : base(splitButton)
			{
			}

			// Token: 0x06005B0D RID: 23309 RVA: 0x00114FFC File Offset: 0x001131FC
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				string className = ToolBarStyles.Combine(new string[]
				{
					"rtbItem",
					"rtbSplBtn",
					"rtbSplBtnTemplate",
					this.Button.OuterCssClass
				});
				base.SetClassName(writer, className);
				base.AddAttributesToRender(writer);
			}

			// Token: 0x06005B0E RID: 23310 RVA: 0x0011504C File Offset: 0x0011324C
			public override void RenderContents(HtmlTextWriter writer)
			{
				this.RenderButtonPart(writer);
				base.RenderChevron(writer);
				base.RenderDropDown(writer, this.Button.Buttons);
			}

			// Token: 0x06005B0F RID: 23311 RVA: 0x00115070 File Offset: 0x00113270
			private void RenderButtonPart(HtmlTextWriter writer)
			{
				base.SetClassName(writer, "rtbWrap");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				base.SetClassName(writer, "rtbOut");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				base.SetClassName(writer, "rtbMiddle");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				base.SetClassName(writer, "rtbIn");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				base.SetClassName(writer, "rtbSplBtnActivator");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				this.RenderChildControls(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}

			// Token: 0x06005B10 RID: 23312 RVA: 0x00115108 File Offset: 0x00113308
			private void RenderChildControls(HtmlTextWriter writer)
			{
				if (this.Button.Controls.IsReadOnly)
				{
					this.Button.RenderChildControls(writer);
					return;
				}
				foreach (object obj in this.Button.Controls)
				{
					Control control = (Control)obj;
					if (!(control is RadToolBarButton))
					{
						control.RenderControl(writer);
					}
				}
			}

			// Token: 0x040015EF RID: 5615
			private RadToolBarSplitButton _button;
		}
	}
}
