using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000939 RID: 2361
	public abstract class RadToolBarItem : ControlItem
	{
		// Token: 0x17001D9C RID: 7580
		// (get) Token: 0x060059C3 RID: 22979 RVA: 0x001111D2 File Offset: 0x0010F3D2
		private string CurrentImageUrl
		{
			get
			{
				if (this._currentImageUrl == null)
				{
					this._currentImageUrl = this.GetCurrentImageUrl();
				}
				return this._currentImageUrl;
			}
		}

		// Token: 0x060059C4 RID: 22980
		protected abstract string GetCurrentImageUrl();

		// Token: 0x17001D9D RID: 7581
		// (get) Token: 0x060059C5 RID: 22981 RVA: 0x001111EE File Offset: 0x0010F3EE
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x17001D9E RID: 7582
		// (get) Token: 0x060059C6 RID: 22982 RVA: 0x001111F2 File Offset: 0x0010F3F2
		protected RadToolBarItem.RendererBase ItemRenderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = this.CreateRenderer();
				}
				return this._renderer;
			}
		}

		// Token: 0x060059C7 RID: 22983
		protected abstract RadToolBarItem.RendererBase CreateRenderer();

		// Token: 0x060059C8 RID: 22984 RVA: 0x0011120E File Offset: 0x0010F40E
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.ItemRenderer.RenderContents(writer);
		}

		// Token: 0x060059C9 RID: 22985 RVA: 0x0011121C File Offset: 0x0010F41C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.ItemRenderer.AddAttributesToRender(writer);
		}

		// Token: 0x17001D9F RID: 7583
		// (get) Token: 0x060059CA RID: 22986
		internal abstract RadToolBarItemType ItemType { get; }

		// Token: 0x060059CB RID: 22987 RVA: 0x0011122C File Offset: 0x0010F42C
		protected static void WriteXmlForChildren(XmlWriter writer, RadToolBarItemCollection children)
		{
			foreach (object obj in children)
			{
				RadToolBarItem radToolBarItem = (RadToolBarItem)obj;
				XmlSerializer xmlSerializer = new XmlSerializer(radToolBarItem.GetType());
				xmlSerializer.Serialize(writer, radToolBarItem);
			}
		}

		// Token: 0x060059CC RID: 22988 RVA: 0x00111290 File Offset: 0x0010F490
		protected void ReadXmlForChildren(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(RadToolBarButton));
					using (XmlReader xmlReader = reader.ReadSubtree())
					{
						RadToolBarButton item = (RadToolBarButton)xmlSerializer.Deserialize(xmlReader);
						base.Children.Add(item);
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x060059CD RID: 22989 RVA: 0x00111310 File Offset: 0x0010F510
		protected internal override void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			base.LoadFromDictionary(dictionary);
			if (dictionary.ContainsKey("cssClass"))
			{
				this.CssClass = dictionary["cssClass"].ToString();
			}
			if (dictionary.ContainsKey("outerCssClass"))
			{
				this.CssClass = dictionary["outerCssClass"].ToString();
			}
			if (dictionary.ContainsKey("disabledCssClass"))
			{
				this.DisabledCssClass = dictionary["disabledCssClass"].ToString();
			}
			if (dictionary.ContainsKey("hoveredCssClass"))
			{
				this.HoveredCssClass = dictionary["hoveredCssClass"].ToString();
			}
			if (dictionary.ContainsKey("focusedCssClass"))
			{
				this.FocusedCssClass = dictionary["focusedCssClass"].ToString();
			}
			if (dictionary.ContainsKey("clickedCssClass"))
			{
				this.ClickedCssClass = dictionary["clickedCssClass"].ToString();
			}
			if (dictionary.ContainsKey("imageUrl"))
			{
				this.ImageUrl = dictionary["imageUrl"].ToString();
			}
			if (dictionary.ContainsKey("disabledImageUrl"))
			{
				this.DisabledImageUrl = dictionary["disabledImageUrl"].ToString();
			}
			if (dictionary.ContainsKey("hoveredImageUrl"))
			{
				this.HoveredImageUrl = dictionary["hoveredImageUrl"].ToString();
			}
			if (dictionary.ContainsKey("focusedImageUrl"))
			{
				this.FocusedImageUrl = dictionary["focusedImageUrl"].ToString();
			}
			if (dictionary.ContainsKey("clickedImageUrl"))
			{
				this.ClickedImageUrl = dictionary["clickedImageUrl"].ToString();
			}
			if (dictionary.ContainsKey("toolTip"))
			{
				this.ToolTip = dictionary["toolTip"].ToString();
			}
			if (dictionary.ContainsKey("imagePosition"))
			{
				this.ImagePosition = (ToolBarImagePosition)((int)dictionary["imagePosition"]);
			}
		}

		// Token: 0x17001DA0 RID: 7584
		// (get) Token: 0x060059CE RID: 22990 RVA: 0x001114EB File Offset: 0x0010F6EB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadToolBar ToolBar
		{
			get
			{
				return (RadToolBar)base.Container;
			}
		}

		// Token: 0x17001DA1 RID: 7585
		// (get) Token: 0x060059CF RID: 22991 RVA: 0x001114F8 File Offset: 0x0010F6F8
		// (set) Token: 0x060059D0 RID: 22992 RVA: 0x00111500 File Offset: 0x0010F700
		[DefaultValue("")]
		[Description("The text of the item")]
		[Localizable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x17001DA2 RID: 7586
		// (get) Token: 0x060059D1 RID: 22993 RVA: 0x00111509 File Offset: 0x0010F709
		// (set) Token: 0x060059D2 RID: 22994 RVA: 0x00111511 File Offset: 0x0010F711
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
			}
		}

		// Token: 0x17001DA3 RID: 7587
		// (get) Token: 0x060059D3 RID: 22995 RVA: 0x0011151A File Offset: 0x0010F71A
		// (set) Token: 0x060059D4 RID: 22996 RVA: 0x0011153A File Offset: 0x0010F73A
		[DefaultValue("")]
		[Description("The URL for the image for the item.")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Appearance")]
		[UrlProperty]
		public virtual string ImageUrl
		{
			get
			{
				return (string)(this.ViewState["ImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17001DA4 RID: 7588
		// (get) Token: 0x060059D5 RID: 22997 RVA: 0x0011154D File Offset: 0x0010F74D
		// (set) Token: 0x060059D6 RID: 22998 RVA: 0x0011156D File Offset: 0x0010F76D
		[UrlProperty]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Appearance")]
		[Description("The URL for the image when the mouse moves over the item.")]
		public virtual string HoveredImageUrl
		{
			get
			{
				return (string)(this.ViewState["HoveredImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["HoveredImageUrl"] = value;
			}
		}

		// Token: 0x17001DA5 RID: 7589
		// (get) Token: 0x060059D7 RID: 22999 RVA: 0x00111580 File Offset: 0x0010F780
		// (set) Token: 0x060059D8 RID: 23000 RVA: 0x001115A0 File Offset: 0x0010F7A0
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("The CSS that is used in sprite image scenarios.")]
		public string SpriteCssClass
		{
			get
			{
				return (string)(this.ViewState["SpriteCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["SpriteCssClass"] = value;
			}
		}

		// Token: 0x17001DA6 RID: 7590
		// (get) Token: 0x060059D9 RID: 23001 RVA: 0x001115B3 File Offset: 0x0010F7B3
		// (set) Token: 0x060059DA RID: 23002 RVA: 0x001115D3 File Offset: 0x0010F7D3
		[DefaultValue("")]
		[Description("CSS Class name applied to the toolbar item when the user moves the mouse over it.")]
		[Category("Appearance")]
		public string HoveredCssClass
		{
			get
			{
				return (string)(this.ViewState["HoveredCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["HoveredCssClass"] = value;
			}
		}

		// Token: 0x17001DA7 RID: 7591
		// (get) Token: 0x060059DB RID: 23003 RVA: 0x001115E6 File Offset: 0x0010F7E6
		// (set) Token: 0x060059DC RID: 23004 RVA: 0x00111606 File Offset: 0x0010F806
		[Description("CSS class applied to the toolbar item when it is clicked.")]
		[Category("Appearance")]
		[DefaultValue("")]
		public string ClickedCssClass
		{
			get
			{
				return (string)(this.ViewState["ClickedCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ClickedCssClass"] = value;
			}
		}

		// Token: 0x17001DA8 RID: 7592
		// (get) Token: 0x060059DD RID: 23005 RVA: 0x00111619 File Offset: 0x0010F819
		// (set) Token: 0x060059DE RID: 23006 RVA: 0x00111639 File Offset: 0x0010F839
		[Category("Appearance")]
		[UrlProperty]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The URL for the image when the item is clicked.")]
		[DefaultValue("")]
		public string ClickedImageUrl
		{
			get
			{
				return (string)(this.ViewState["ClickedImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ClickedImageUrl"] = value;
			}
		}

		// Token: 0x17001DA9 RID: 7593
		// (get) Token: 0x060059DF RID: 23007 RVA: 0x0011164C File Offset: 0x0010F84C
		// (set) Token: 0x060059E0 RID: 23008 RVA: 0x0011166C File Offset: 0x0010F86C
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[Description("The URL for the image when the item is disabled.")]
		[Category("Appearance")]
		public virtual string DisabledImageUrl
		{
			get
			{
				return (string)(this.ViewState["DisabledImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DisabledImageUrl"] = value;
			}
		}

		// Token: 0x17001DAA RID: 7594
		// (get) Token: 0x060059E1 RID: 23009 RVA: 0x0011167F File Offset: 0x0010F87F
		// (set) Token: 0x060059E2 RID: 23010 RVA: 0x0011169F File Offset: 0x0010F89F
		[Description("CSS Class name applied to the toolbar item when it is disabled.")]
		[DefaultValue("")]
		[Category("Appearance")]
		public new string DisabledCssClass
		{
			get
			{
				return (string)(this.ViewState["DisabledCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DisabledCssClass"] = value;
			}
		}

		// Token: 0x17001DAB RID: 7595
		// (get) Token: 0x060059E3 RID: 23011 RVA: 0x001116B2 File Offset: 0x0010F8B2
		// (set) Token: 0x060059E4 RID: 23012 RVA: 0x001116D2 File Offset: 0x0010F8D2
		[Description("CSS class applied to the toolBar item when it is focused.")]
		[DefaultValue("")]
		[Category("Appearance")]
		public string FocusedCssClass
		{
			get
			{
				return (string)(this.ViewState["FocusedCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["FocusedCssClass"] = value;
			}
		}

		// Token: 0x17001DAC RID: 7596
		// (get) Token: 0x060059E5 RID: 23013 RVA: 0x001116E5 File Offset: 0x0010F8E5
		// (set) Token: 0x060059E6 RID: 23014 RVA: 0x00111705 File Offset: 0x0010F905
		[Category("Appearance")]
		[Description("The URL for the image when the item gets focus.")]
		[DefaultValue("")]
		[UrlProperty]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string FocusedImageUrl
		{
			get
			{
				return (string)(this.ViewState["FocusedImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["FocusedImageUrl"] = value;
			}
		}

		// Token: 0x17001DAD RID: 7597
		// (get) Token: 0x060059E7 RID: 23015 RVA: 0x00111718 File Offset: 0x0010F918
		// (set) Token: 0x060059E8 RID: 23016 RVA: 0x00111738 File Offset: 0x0010F938
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("CSS Class name applied on the outmost item wrapper (<LI>).")]
		public string OuterCssClass
		{
			get
			{
				return (string)(this.ViewState["OuterCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OuterCssClass"] = value;
			}
		}

		// Token: 0x17001DAE RID: 7598
		// (get) Token: 0x060059E9 RID: 23017 RVA: 0x0011174B File Offset: 0x0010F94B
		// (set) Token: 0x060059EA RID: 23018 RVA: 0x0011176C File Offset: 0x0010F96C
		[Category("Layout")]
		[Description("The position of the item image according to the item text.")]
		[DefaultValue(ToolBarImagePosition.Left)]
		public ToolBarImagePosition ImagePosition
		{
			get
			{
				return (ToolBarImagePosition)(this.ViewState["ImagePosition"] ?? ToolBarImagePosition.Left);
			}
			set
			{
				this.ViewState["ImagePosition"] = value;
			}
		}

		// Token: 0x17001DAF RID: 7599
		// (get) Token: 0x060059EB RID: 23019 RVA: 0x00111784 File Offset: 0x0010F984
		// (set) Token: 0x060059EC RID: 23020 RVA: 0x001117A5 File Offset: 0x0010F9A5
		[ClientPropertyName("enableImageSprite")]
		[Description("A value indicating if an image sprite container should be used instead of the default image")]
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(false)]
		public bool EnableImageSprite
		{
			get
			{
				return (bool)(this.ViewState["EnableImageSprite"] ?? false);
			}
			set
			{
				this.ViewState["EnableImageSprite"] = value;
			}
		}

		// Token: 0x17001DB0 RID: 7600
		// (get) Token: 0x060059ED RID: 23021 RVA: 0x001117BD File Offset: 0x0010F9BD
		// (set) Token: 0x060059EE RID: 23022 RVA: 0x001117DE File Offset: 0x0010F9DE
		[DefaultValue(ToolBarOverflow.Auto)]
		[Description("The position of the item image according to the item text.")]
		[Category("Layout")]
		[ClientControlProperty]
		[ClientPropertyName("overFlow")]
		public ToolBarOverflow OverFlow
		{
			get
			{
				return (ToolBarOverflow)(this.ViewState["OverFlow"] ?? ToolBarOverflow.Auto);
			}
			set
			{
				this.ViewState["OverFlow"] = value;
			}
		}

		// Token: 0x17001DB1 RID: 7601
		// (get) Token: 0x060059EF RID: 23023 RVA: 0x001117F6 File Offset: 0x0010F9F6
		// (set) Token: 0x060059F0 RID: 23024 RVA: 0x00111817 File Offset: 0x0010FA17
		[Description("The position of the item image according to the item text.")]
		[ClientControlProperty]
		[DefaultValue(ToolBarShowPosition.Both)]
		[ClientPropertyName("showText")]
		[Category("Layout")]
		public virtual ToolBarShowPosition ShowText
		{
			get
			{
				return (ToolBarShowPosition)(this.ViewState["ShowText"] ?? ToolBarShowPosition.Both);
			}
			set
			{
				this.ViewState["ShowText"] = value;
			}
		}

		// Token: 0x17001DB2 RID: 7602
		// (get) Token: 0x060059F1 RID: 23025 RVA: 0x0011182F File Offset: 0x0010FA2F
		// (set) Token: 0x060059F2 RID: 23026 RVA: 0x00111850 File Offset: 0x0010FA50
		[Description("The position of the item image according to the item text.")]
		[ClientPropertyName("showImage")]
		[Category("Layout")]
		[ClientControlProperty]
		[DefaultValue(ToolBarShowPosition.Both)]
		public virtual ToolBarShowPosition ShowImage
		{
			get
			{
				return (ToolBarShowPosition)(this.ViewState["ShowImage"] ?? ToolBarShowPosition.Both);
			}
			set
			{
				this.ViewState["ShowImage"] = value;
			}
		}

		// Token: 0x17001DB3 RID: 7603
		// (get) Token: 0x060059F3 RID: 23027 RVA: 0x00111868 File Offset: 0x0010FA68
		internal bool EnableImageSpriteResolved
		{
			get
			{
				if (this.ViewState["EnableImageSprite"] == null)
				{
					return this.ToolBar != null && this.ToolBar.EnableImageSprites;
				}
				return this.EnableImageSprite;
			}
		}

		// Token: 0x040015D1 RID: 5585
		private RadToolBarItem.RendererBase _renderer;

		// Token: 0x040015D2 RID: 5586
		private string _currentImageUrl;

		// Token: 0x0200093A RID: 2362
		protected abstract class RendererBase
		{
			// Token: 0x17001DB4 RID: 7604
			// (get) Token: 0x060059F5 RID: 23029 RVA: 0x001118A0 File Offset: 0x0010FAA0
			protected RadToolBarItem Item
			{
				get
				{
					return this._item;
				}
			}

			// Token: 0x060059F6 RID: 23030 RVA: 0x001118A8 File Offset: 0x0010FAA8
			public RendererBase(RadToolBarItem item)
			{
				this._item = item;
			}

			// Token: 0x17001DB5 RID: 7605
			// (get) Token: 0x060059F7 RID: 23031 RVA: 0x001118B7 File Offset: 0x0010FAB7
			internal bool ShouldRenderImagePlaceholder
			{
				get
				{
					return this.Item.EnableImageSpriteResolved;
				}
			}

			// Token: 0x060059F8 RID: 23032 RVA: 0x001118C4 File Offset: 0x0010FAC4
			private RendererBase()
			{
			}

			// Token: 0x060059F9 RID: 23033 RVA: 0x001118CC File Offset: 0x0010FACC
			public virtual void AddAttributesToRender(HtmlTextWriter writer)
			{
				if (!string.IsNullOrEmpty(this.Item.Style[HtmlTextWriterStyle.Display]))
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, this.Item.Style[HtmlTextWriterStyle.Display]);
				}
				this.Item.Style.Remove(HtmlTextWriterStyle.Display);
				string text = "display";
				if (!string.IsNullOrEmpty(this.Item.Style[text]))
				{
					writer.AddStyleAttribute(text, this.Item.Style[text]);
				}
				this.Item.Style.Remove(text);
			}

			// Token: 0x060059FA RID: 23034
			public abstract void RenderContents(HtmlTextWriter writer);

			// Token: 0x060059FB RID: 23035 RVA: 0x00111965 File Offset: 0x0010FB65
			protected void SetClassName(HtmlTextWriter writer, string className)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, className);
			}

			// Token: 0x060059FC RID: 23036 RVA: 0x00111970 File Offset: 0x0010FB70
			internal void RenderTextContainer(HtmlTextWriter writer, RadToolBarItem item)
			{
				if (string.IsNullOrEmpty(item.Text))
				{
					return;
				}
				string text = "rtbText";
				if (item.ShowText == ToolBarShowPosition.OverFlow)
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						"rtbHidden"
					});
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(item.Text);
				writer.RenderEndTag();
			}

			// Token: 0x060059FD RID: 23037 RVA: 0x001119D7 File Offset: 0x0010FBD7
			protected string GetDisabledClasses()
			{
				return this.GetDisabledClasses(this.Item);
			}

			// Token: 0x060059FE RID: 23038 RVA: 0x001119E8 File Offset: 0x0010FBE8
			protected string GetDisabledClasses(RadToolBarItem item)
			{
				string text = string.Empty;
				if (!item.Enabled || (item.ToolBar != null && !item.ToolBar.Enabled))
				{
					text = "rtbDisabled";
					if (!string.IsNullOrEmpty(item.DisabledCssClass))
					{
						text = ToolBarStyles.Combine(new string[]
						{
							text,
							item.DisabledCssClass
						});
					}
				}
				return text;
			}

			// Token: 0x040015D3 RID: 5587
			private RadToolBarItem _item;
		}

		// Token: 0x0200093B RID: 2363
		protected class LiteRenderer : RadToolBarItem.RendererBase
		{
			// Token: 0x060059FF RID: 23039 RVA: 0x00111A47 File Offset: 0x0010FC47
			public LiteRenderer(RadToolBarItem item) : base(item)
			{
			}

			// Token: 0x06005A00 RID: 23040 RVA: 0x00111A50 File Offset: 0x0010FC50
			public override void RenderContents(HtmlTextWriter writer)
			{
			}

			// Token: 0x06005A01 RID: 23041 RVA: 0x00111A54 File Offset: 0x0010FC54
			protected void ApplyLinkAttributes(HtmlTextWriter writer, RadToolBarItem item)
			{
				IRadToolBarButton radToolBarButton = item as IRadToolBarButton;
				if (radToolBarButton != null)
				{
					IRadToolBarButton radToolBarButton2 = radToolBarButton;
					if (!string.IsNullOrEmpty(radToolBarButton2.NavigateUrl))
					{
						string value = item.ResolveClientUrl(radToolBarButton2.NavigateUrl);
						writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
						if (!string.IsNullOrEmpty(radToolBarButton2.Target))
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Target, radToolBarButton2.Target);
						}
					}
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, item.TabIndex.ToString());
			}

			// Token: 0x06005A02 RID: 23042 RVA: 0x00111AC1 File Offset: 0x0010FCC1
			protected void ApplyLinkAttributes(HtmlTextWriter writer)
			{
				this.ApplyLinkAttributes(writer, base.Item);
			}

			// Token: 0x06005A03 RID: 23043 RVA: 0x00111AD0 File Offset: 0x0010FCD0
			protected void RenderImageAndTextElements(HtmlTextWriter writer)
			{
				this.RenderImageAndTextElements(writer, base.Item);
			}

			// Token: 0x06005A04 RID: 23044 RVA: 0x00111AE0 File Offset: 0x0010FCE0
			protected void RenderImageAndTextElements(HtmlTextWriter writer, RadToolBarItem item)
			{
				if (this.IsItemAChildButton(item) || item.ImagePosition == ToolBarImagePosition.Left || item.ImagePosition == ToolBarImagePosition.AboveText)
				{
					if (!string.IsNullOrEmpty(item.CurrentImageUrl))
					{
						this.RenderImage(writer, item);
					}
					else if (base.ShouldRenderImagePlaceholder)
					{
						RadToolBarItem.LiteRenderer.RenderImagePlaceholder(writer, item);
					}
					base.RenderTextContainer(writer, item);
					return;
				}
				base.RenderTextContainer(writer, item);
				if (!string.IsNullOrEmpty(item.CurrentImageUrl))
				{
					this.RenderImage(writer, item);
					return;
				}
				if (base.ShouldRenderImagePlaceholder)
				{
					RadToolBarItem.LiteRenderer.RenderImagePlaceholder(writer, item);
				}
			}

			// Token: 0x06005A05 RID: 23045 RVA: 0x00111B63 File Offset: 0x0010FD63
			private bool IsItemAChildButton(RadToolBarItem item)
			{
				return base.Item.ItemType == RadToolBarItemType.Button && ((RadToolBarButton)base.Item).Owner != base.Item.ToolBar;
			}

			// Token: 0x06005A06 RID: 23046 RVA: 0x00111B94 File Offset: 0x0010FD94
			private void RenderImage(HtmlTextWriter writer, RadToolBarItem item)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, item.ToolTip);
				writer.AddAttribute(HtmlTextWriterAttribute.Src, item.CurrentImageUrl);
				string text = "rtbImage";
				if (item.ShowImage == ToolBarShowPosition.OverFlow)
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						"rtbHidden"
					});
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}

			// Token: 0x06005A07 RID: 23047 RVA: 0x00111BFC File Offset: 0x0010FDFC
			private static void RenderImagePlaceholder(HtmlTextWriter writer, RadToolBarItem item)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, item.ToolTip);
				string text = "rtbImage";
				if (!string.IsNullOrEmpty(item.SpriteCssClass))
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						item.SpriteCssClass
					});
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
			}

			// Token: 0x06005A08 RID: 23048 RVA: 0x00111C5C File Offset: 0x0010FE5C
			protected void RenderDropDown(HtmlTextWriter htmlWriter, RadToolBarButtonCollection buttons)
			{
				RadToolBarItem.WhiteSpaceStrippingHtmlTextWriter whiteSpaceStrippingHtmlTextWriter = new RadToolBarItem.WhiteSpaceStrippingHtmlTextWriter(htmlWriter);
				base.SetClassName(whiteSpaceStrippingHtmlTextWriter, "rtbSlide");
				whiteSpaceStrippingHtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Div);
				string text = string.Empty;
				if (base.Item.ToolBar.EnableRoundedCorners || base.Item.ToolBar.EnableShadows)
				{
					string text2 = "rtbScrollWrap";
					if (base.Item.ToolBar.EnableRoundedCorners)
					{
						text2 = ToolBarStyles.Combine(new string[]
						{
							text2,
							"rtbRoundedCorners"
						});
					}
					if (base.Item.ToolBar.EnableShadows)
					{
						text2 = ToolBarStyles.Combine(new string[]
						{
							text2,
							"rtbShadows"
						});
					}
					text = text2;
				}
				base.SetClassName(whiteSpaceStrippingHtmlTextWriter, ToolBarStyles.Combine(new string[]
				{
					"rtbPopup",
					"rtbMenuPopup",
					string.Format("{0}_{1}", "rtbPopup", SkinRegistrar.GetRuntimeSkin(base.Item.ToolBar)),
					text
				}));
				whiteSpaceStrippingHtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Div);
				if (buttons.Count > 0)
				{
					whiteSpaceStrippingHtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "rtbMenu");
					whiteSpaceStrippingHtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Ul);
					foreach (object obj in buttons)
					{
						RadToolBarButton radToolBarButton = (RadToolBarButton)obj;
						radToolBarButton.RenderControl(whiteSpaceStrippingHtmlTextWriter);
					}
					whiteSpaceStrippingHtmlTextWriter.RenderEndTag();
				}
				whiteSpaceStrippingHtmlTextWriter.RenderEndTag();
				whiteSpaceStrippingHtmlTextWriter.RenderEndTag();
			}

			// Token: 0x06005A09 RID: 23049 RVA: 0x00111DEC File Offset: 0x0010FFEC
			protected string GetInnerItemElementClass()
			{
				if (base.Item.ImagePosition == ToolBarImagePosition.AboveText || base.Item.ImagePosition == ToolBarImagePosition.BelowText)
				{
					return "rtbVOriented";
				}
				return string.Empty;
			}

			// Token: 0x06005A0A RID: 23050 RVA: 0x00111E18 File Offset: 0x00110018
			protected void RenderChevron(HtmlTextWriter writer)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbArrow");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, ToolBarStyles.Combine(new string[]
				{
					"radIcon",
					"radIconDown"
				}));
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x0200093C RID: 2364
		private class WhiteSpaceStrippingHtmlTextWriter : HtmlTextWriter
		{
			// Token: 0x06005A0B RID: 23051 RVA: 0x00111E73 File Offset: 0x00110073
			public WhiteSpaceStrippingHtmlTextWriter(TextWriter writer) : base(writer)
			{
			}

			// Token: 0x06005A0C RID: 23052 RVA: 0x00111E7C File Offset: 0x0011007C
			protected override void OutputTabs()
			{
			}

			// Token: 0x06005A0D RID: 23053 RVA: 0x00111E7E File Offset: 0x0011007E
			public override void WriteLine()
			{
			}
		}

		// Token: 0x0200093D RID: 2365
		protected class Renderer : RadToolBarItem.RendererBase
		{
			// Token: 0x06005A0E RID: 23054 RVA: 0x00111E80 File Offset: 0x00110080
			public Renderer(RadToolBarItem item) : base(item)
			{
			}

			// Token: 0x06005A0F RID: 23055 RVA: 0x00111E89 File Offset: 0x00110089
			public override void RenderContents(HtmlTextWriter writer)
			{
			}

			// Token: 0x06005A10 RID: 23056 RVA: 0x00111E8C File Offset: 0x0011008C
			protected void ApplyLinkAttributes(HtmlTextWriter writer, RadToolBarItem item)
			{
				string value = "#";
				IRadToolBarButton radToolBarButton = item as IRadToolBarButton;
				if (radToolBarButton != null)
				{
					IRadToolBarButton radToolBarButton2 = radToolBarButton;
					if (!string.IsNullOrEmpty(radToolBarButton2.NavigateUrl))
					{
						value = item.ResolveClientUrl(radToolBarButton2.NavigateUrl);
						if (!string.IsNullOrEmpty(radToolBarButton2.Target))
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Target, radToolBarButton2.Target);
						}
					}
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
			}

			// Token: 0x06005A11 RID: 23057 RVA: 0x00111EE9 File Offset: 0x001100E9
			protected void ApplyLinkAttributes(HtmlTextWriter writer)
			{
				this.ApplyLinkAttributes(writer, base.Item);
			}

			// Token: 0x06005A12 RID: 23058 RVA: 0x00111EF8 File Offset: 0x001100F8
			protected void RenderImageAndTextElements(HtmlTextWriter writer)
			{
				this.RenderImageAndTextElements(writer, base.Item);
			}

			// Token: 0x06005A13 RID: 23059 RVA: 0x00111F08 File Offset: 0x00110108
			protected void RenderImageAndTextElements(HtmlTextWriter writer, RadToolBarItem item)
			{
				if (this.IsItemAChildButton(item) || item.ImagePosition == ToolBarImagePosition.Left || item.ImagePosition == ToolBarImagePosition.AboveText)
				{
					if (!string.IsNullOrEmpty(item.CurrentImageUrl))
					{
						this.RenderImage(writer, item);
					}
					else if (base.ShouldRenderImagePlaceholder)
					{
						RadToolBarItem.Renderer.RenderImagePlaceholder(writer, item);
					}
					base.RenderTextContainer(writer, item);
					return;
				}
				base.RenderTextContainer(writer, item);
				if (!string.IsNullOrEmpty(item.CurrentImageUrl))
				{
					this.RenderImage(writer, item);
					return;
				}
				if (base.ShouldRenderImagePlaceholder)
				{
					RadToolBarItem.Renderer.RenderImagePlaceholder(writer, item);
				}
			}

			// Token: 0x06005A14 RID: 23060 RVA: 0x00111F8B File Offset: 0x0011018B
			private bool IsItemAChildButton(RadToolBarItem item)
			{
				return base.Item.ItemType == RadToolBarItemType.Button && ((RadToolBarButton)base.Item).Owner != base.Item.ToolBar;
			}

			// Token: 0x06005A15 RID: 23061 RVA: 0x00111FBC File Offset: 0x001101BC
			private void RenderImage(HtmlTextWriter writer, RadToolBarItem item)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, item.ToolTip);
				writer.AddAttribute(HtmlTextWriterAttribute.Src, item.CurrentImageUrl);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbIcon");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}

			// Token: 0x06005A16 RID: 23062 RVA: 0x00111FF4 File Offset: 0x001101F4
			private static void RenderImagePlaceholder(HtmlTextWriter writer, RadToolBarItem item)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, item.ToolTip);
				string text = "rtbIcon";
				if (!string.IsNullOrEmpty(item.SpriteCssClass))
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						item.SpriteCssClass
					});
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
			}

			// Token: 0x06005A17 RID: 23063 RVA: 0x00112054 File Offset: 0x00110254
			protected void RenderDropDown(HtmlTextWriter htmlWriter, RadToolBarButtonCollection buttons)
			{
				RadToolBarItem.WhiteSpaceStrippingHtmlTextWriter whiteSpaceStrippingHtmlTextWriter = new RadToolBarItem.WhiteSpaceStrippingHtmlTextWriter(htmlWriter);
				base.SetClassName(whiteSpaceStrippingHtmlTextWriter, "rtbSlide");
				whiteSpaceStrippingHtmlTextWriter.AddStyleAttribute("display", "none");
				whiteSpaceStrippingHtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Div);
				string text = "";
				if (base.Item.ToolBar.EnableRoundedCorners)
				{
					text += "rtbNoBackground";
				}
				base.SetClassName(whiteSpaceStrippingHtmlTextWriter, ToolBarStyles.Combine(new string[]
				{
					"RadToolBarDropDown",
					string.Format("{0}_{1}", "RadToolBarDropDown", SkinRegistrar.GetRuntimeSkin(base.Item.ToolBar)),
					text
				}));
				whiteSpaceStrippingHtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Div);
				if (base.Item.ToolBar.EnableRoundedCorners || base.Item.ToolBar.EnableShadows)
				{
					string text2 = "rtbScrollWrap";
					if (base.Item.ToolBar.EnableRoundedCorners)
					{
						text2 = ToolBarStyles.Combine(new string[]
						{
							text2,
							"rtbRoundedCorners"
						});
					}
					if (base.Item.ToolBar.EnableShadows)
					{
						text2 = ToolBarStyles.Combine(new string[]
						{
							text2,
							"rtbShadows"
						});
					}
					base.SetClassName(whiteSpaceStrippingHtmlTextWriter, text2);
					whiteSpaceStrippingHtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Div);
				}
				if (buttons.Count > 0)
				{
					whiteSpaceStrippingHtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, ToolBarStyles.Combine(new string[]
					{
						"rtbActive",
						"rtbGroup",
						"rtbLevel1"
					}));
					whiteSpaceStrippingHtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Ul);
					foreach (object obj in buttons)
					{
						RadToolBarButton radToolBarButton = (RadToolBarButton)obj;
						radToolBarButton.RenderControl(whiteSpaceStrippingHtmlTextWriter);
					}
					whiteSpaceStrippingHtmlTextWriter.RenderEndTag();
				}
				if (base.Item.ToolBar.EnableRoundedCorners || base.Item.ToolBar.EnableShadows)
				{
					whiteSpaceStrippingHtmlTextWriter.RenderEndTag();
				}
				whiteSpaceStrippingHtmlTextWriter.RenderEndTag();
				whiteSpaceStrippingHtmlTextWriter.RenderEndTag();
			}

			// Token: 0x06005A18 RID: 23064 RVA: 0x00112268 File Offset: 0x00110468
			protected string GetInnerItemElementClass()
			{
				if (base.Item.ImagePosition == ToolBarImagePosition.AboveText || base.Item.ImagePosition == ToolBarImagePosition.BelowText)
				{
					return ToolBarStyles.Combine(new string[]
					{
						"rtbIn",
						"rtbVOriented"
					});
				}
				return "rtbIn";
			}

			// Token: 0x06005A19 RID: 23065 RVA: 0x001122B4 File Offset: 0x001104B4
			protected void RenderChevron(HtmlTextWriter writer)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbChoiceArrow");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
			}
		}
	}
}
