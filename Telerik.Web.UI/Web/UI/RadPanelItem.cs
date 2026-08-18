using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Telerik.Web.UI.PanelBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02001B4A RID: 6986
	[XmlRoot("Item")]
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	public class RadPanelItem : NavigationItem, IRadPanelItemContainer, ICloneable
	{
		// Token: 0x06010E2B RID: 69163 RVA: 0x003BE454 File Offset: 0x003BC654
		internal void ApplySelection()
		{
			if (this._pendingSelect)
			{
				this.Selected = true;
				this._pendingSelect = false;
			}
		}

		// Token: 0x1700525C RID: 21084
		// (get) Token: 0x06010E2C RID: 69164 RVA: 0x003BE46C File Offset: 0x003BC66C
		protected internal override IDictionary<string, string> PropertyMappings
		{
			get
			{
				return new Dictionary<string, string>
				{
					{
						"ItemCollapsedCssClass",
						"CssClass"
					},
					{
						"ItemExpandedCssClass",
						"ExpandedCssClass"
					},
					{
						"ItemSelectedCssClass",
						"ClickedCssClass"
					},
					{
						"ItemDisabledCssClass",
						"DisabledCssClass"
					},
					{
						"ImageCollapsed",
						"ImageUrl"
					},
					{
						"ImageDisabled",
						"DisabledImageUrl"
					},
					{
						"ImageExpanded",
						"ExpandedImageUrl"
					},
					{
						"ImageHoverCollapsed",
						"ImageOverUrl"
					}
				};
			}
		}

		// Token: 0x1700525D RID: 21085
		// (get) Token: 0x06010E2D RID: 69165 RVA: 0x003BE500 File Offset: 0x003BC700
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x1700525E RID: 21086
		// (get) Token: 0x06010E2E RID: 69166 RVA: 0x003BE504 File Offset: 0x003BC704
		internal bool ShouldRenderHeaderTemplate
		{
			get
			{
				return this.HeaderTemplate != null && this.Header != null;
			}
		}

		// Token: 0x1700525F RID: 21087
		// (get) Token: 0x06010E2F RID: 69167 RVA: 0x003BE51C File Offset: 0x003BC71C
		internal bool ShouldRenderLink
		{
			get
			{
				return !this.IsSeparator && (!string.IsNullOrEmpty(this.Text) || !string.IsNullOrEmpty(this.ImageUrl)) && !this.ShouldRenderHeaderTemplate;
			}
		}

		// Token: 0x06010E30 RID: 69168 RVA: 0x003BE54B File Offset: 0x003BC74B
		internal void RenderChildrenBase(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
		}

		// Token: 0x17005260 RID: 21088
		// (get) Token: 0x06010E31 RID: 69169 RVA: 0x003BE554 File Offset: 0x003BC754
		internal string CurrentImageUrl
		{
			get
			{
				if (!this.Enabled && !string.IsNullOrEmpty(this.DisabledImageUrl))
				{
					return this.DisabledImageUrl;
				}
				if (this.Selected && !string.IsNullOrEmpty(this.SelectedImageUrl))
				{
					return this.SelectedImageUrl;
				}
				if (this.Expanded && !string.IsNullOrEmpty(this.ExpandedImageUrl))
				{
					return this.ExpandedImageUrl;
				}
				return this.ImageUrl;
			}
		}

		// Token: 0x17005261 RID: 21089
		// (get) Token: 0x06010E32 RID: 69170 RVA: 0x003BE5BC File Offset: 0x003BC7BC
		private bool IsFirstVisibleItem
		{
			get
			{
				if (base.Index == 0 && this.Visible)
				{
					return true;
				}
				for (int i = base.Index - 1; i > -1; i--)
				{
					if (this.Owner.Items[i].Visible)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17005262 RID: 21090
		// (get) Token: 0x06010E33 RID: 69171 RVA: 0x003BE609 File Offset: 0x003BC809
		[Browsable(false)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadPanelItemCollection Items
		{
			get
			{
				return (RadPanelItemCollection)base.Children;
			}
		}

		// Token: 0x06010E34 RID: 69172 RVA: 0x003BE616 File Offset: 0x003BC816
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadPanelItemCollection(this);
		}

		// Token: 0x17005263 RID: 21091
		// (get) Token: 0x06010E35 RID: 69173 RVA: 0x003BE61E File Offset: 0x003BC81E
		protected virtual IRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = this.CreateItemRenderer();
				}
				return this._renderer;
			}
		}

		// Token: 0x06010E36 RID: 69174 RVA: 0x003BE63A File Offset: 0x003BC83A
		protected internal virtual IRenderer CreateItemRenderer()
		{
			return RendererFactory.CreateItemRenderer(this);
		}

		// Token: 0x06010E37 RID: 69175 RVA: 0x003BE644 File Offset: 0x003BC844
		private string DetermineCssClass()
		{
			string text = "rpItem";
			if (this.IsFirstVisibleItem)
			{
				text += " rpFirst";
			}
			if (base.Index == this.GetLastVisibleItemIndex())
			{
				text += " rpLast";
			}
			if (this.IsSeparator)
			{
				text += " rpSeparator";
			}
			return text;
		}

		// Token: 0x06010E38 RID: 69176 RVA: 0x003BE69A File Offset: 0x003BC89A
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.DetermineCssClass());
		}

		// Token: 0x06010E39 RID: 69177 RVA: 0x003BE6AA File Offset: 0x003BC8AA
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06010E3A RID: 69178 RVA: 0x003BE6B8 File Offset: 0x003BC8B8
		private int GetLastVisibleItemIndex()
		{
			for (int i = this.Owner.Items.Count - 1; i > -1; i--)
			{
				if (this.Owner.Items[i].Visible)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x17005264 RID: 21092
		// (get) Token: 0x06010E3B RID: 69179 RVA: 0x003BE6FD File Offset: 0x003BC8FD
		internal bool InDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x06010E3C RID: 69180 RVA: 0x003BE708 File Offset: 0x003BC908
		internal override void PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			base.PopulateFromDataItem(properties, dataItem, dataMember, depth);
			if (dataItem is INavigateUIData)
			{
				IHierarchyData hierarchyData = (IHierarchyData)dataItem;
				if (string.Equals(hierarchyData.Path, this.PanelBar.CurrentSiteMapUrl, StringComparison.OrdinalIgnoreCase))
				{
					this.ExpandParentItems();
					this.Selected = true;
					if (hierarchyData.HasChildren)
					{
						this.Expanded = true;
					}
				}
			}
		}

		// Token: 0x06010E3D RID: 69181 RVA: 0x003BE764 File Offset: 0x003BC964
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.VerifyTemplatesConsistencyWithException();
		}

		// Token: 0x06010E3E RID: 69182 RVA: 0x003BE773 File Offset: 0x003BC973
		internal void VerifyTemplatesConsistencyWithException()
		{
			if (this._itemTemplateIsSet && this._contentTemplateIsSet)
			{
				throw new RadPanelItemTemplateException("Cannot set both ItemTemplate and ContentTemplate on a RadPanelItem.");
			}
			if (this._contentTemplateIsSet && this.Items.Count > 0)
			{
				throw new RadPanelItemTemplateException("Cannot set ContentTemplate on a RadPanelItem, which has child Items.");
			}
		}

		// Token: 0x17005265 RID: 21093
		// (get) Token: 0x06010E3F RID: 69183 RVA: 0x003BE7B4 File Offset: 0x003BC9B4
		internal override bool Templated
		{
			get
			{
				if (base.TemplateInstantiated)
				{
					return true;
				}
				if (!this._controlsTraversed)
				{
					this._controlsTraversed = true;
					foreach (object obj in this.Controls)
					{
						Control control = (Control)obj;
						if (!this.IsChildControl(control) && !control.Equals(this.Header))
						{
							this._templated = true;
							break;
						}
					}
				}
				return this._templated;
			}
		}

		// Token: 0x17005266 RID: 21094
		// (get) Token: 0x06010E40 RID: 69184 RVA: 0x003BE848 File Offset: 0x003BCA48
		internal bool HasContentTemplate
		{
			get
			{
				return this._contentTemplateIsSet;
			}
		}

		// Token: 0x06010E41 RID: 69185 RVA: 0x003BE850 File Offset: 0x003BCA50
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06010E42 RID: 69186 RVA: 0x003BE858 File Offset: 0x003BCA58
		public RadPanelItem Clone()
		{
			RadPanelItem radPanelItem = new RadPanelItem();
			foreach (object obj in TypeDescriptor.GetProperties(this))
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!(propertyDescriptor.Name == "ID"))
				{
					if (propertyDescriptor.Name == "Font")
					{
						radPanelItem.Font.CopyFrom(this.Font);
					}
					else if (!propertyDescriptor.IsReadOnly)
					{
						propertyDescriptor.SetValue(radPanelItem, propertyDescriptor.GetValue(this));
					}
				}
			}
			foreach (object obj2 in base.Attributes.Keys)
			{
				string key = (string)obj2;
				radPanelItem.Attributes[key] = base.Attributes[key];
			}
			foreach (object obj3 in this.Items)
			{
				RadPanelItem radPanelItem2 = (RadPanelItem)obj3;
				radPanelItem.Items.Add(radPanelItem2.Clone());
			}
			return radPanelItem;
		}

		// Token: 0x06010E43 RID: 69187 RVA: 0x003BE9C4 File Offset: 0x003BCBC4
		protected internal override void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			base.LoadFromDictionary(dictionary);
			if (dictionary.ContainsKey("navigateUrl"))
			{
				this.NavigateUrl = (string)dictionary["navigateUrl"];
			}
			if (dictionary.ContainsKey("postBack"))
			{
				this.PostBack = bool.Parse((string)dictionary["postBack"]);
			}
			if (dictionary.ContainsKey("target"))
			{
				this.Target = (string)dictionary["target"];
			}
			if (dictionary.ContainsKey("cssClass"))
			{
				this.CssClass = dictionary["cssClass"].ToString();
			}
			if (dictionary.ContainsKey("disabledCssClass"))
			{
				this.DisabledCssClass = (string)dictionary["disabledCssClass"];
			}
			if (dictionary.ContainsKey("expandedCssClass"))
			{
				this.ExpandedCssClass = (string)dictionary["expandedCssClass"];
			}
			if (dictionary.ContainsKey("focusedCssClass"))
			{
				this.FocusedCssClass = (string)dictionary["focusedCssClass"];
			}
			if (dictionary.ContainsKey("clickedCssClass"))
			{
				this.ClickedCssClass = (string)dictionary["clickedCssClass"];
			}
			if (dictionary.ContainsKey("imageUrl"))
			{
				this.ImageUrl = (string)dictionary["imageUrl"];
			}
			if (dictionary.ContainsKey("hoveredImageUrl"))
			{
				this.HoveredImageUrl = (string)dictionary["hoveredImageUrl"];
			}
			if (dictionary.ContainsKey("text"))
			{
				this.Text = (this.PanelBar.EnableItemTextHtmlEncoding ? HttpUtility.HtmlDecode((string)dictionary["text"]) : ((string)dictionary["text"]));
			}
		}

		// Token: 0x06010E44 RID: 69188 RVA: 0x003BEB82 File Offset: 0x003BCD82
		public RadPanelItem()
		{
		}

		// Token: 0x06010E45 RID: 69189 RVA: 0x003BEB8A File Offset: 0x003BCD8A
		public RadPanelItem(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x17005267 RID: 21095
		// (get) Token: 0x06010E46 RID: 69190 RVA: 0x003BEB99 File Offset: 0x003BCD99
		// (set) Token: 0x06010E47 RID: 69191 RVA: 0x003BEBC4 File Offset: 0x003BCDC4
		[DefaultValue(RadPanelItemImagePosition.Left)]
		[Description("Indicating the position of the image within the item.")]
		public RadPanelItemImagePosition ImagePosition
		{
			get
			{
				if (this.ViewState["ImagePosition"] == null)
				{
					return RadPanelItemImagePosition.Left;
				}
				return (RadPanelItemImagePosition)this.ViewState["ImagePosition"];
			}
			set
			{
				this.ViewState["ImagePosition"] = value;
			}
		}

		// Token: 0x17005268 RID: 21096
		// (get) Token: 0x06010E48 RID: 69192 RVA: 0x003BEBDC File Offset: 0x003BCDDC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadPanelBar PanelBar
		{
			get
			{
				return (RadPanelBar)base.Container;
			}
		}

		// Token: 0x17005269 RID: 21097
		// (get) Token: 0x06010E49 RID: 69193 RVA: 0x003BEBE9 File Offset: 0x003BCDE9
		// (set) Token: 0x06010E4A RID: 69194 RVA: 0x003BEBF1 File Offset: 0x003BCDF1
		[Browsable(false)]
		public IRadPanelItemContainer Owner
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

		// Token: 0x1700526A RID: 21098
		// (get) Token: 0x06010E4B RID: 69195 RVA: 0x003BEBFA File Offset: 0x003BCDFA
		// (set) Token: 0x06010E4C RID: 69196 RVA: 0x003BEC1B File Offset: 0x003BCE1B
		[DefaultValue(false)]
		[Description("Sets/gets that the item is separator. It also represents a logical state of the item. Might be used in some applications like keyboard navigation to omit processing of items that are marked like separators.")]
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

		// Token: 0x1700526B RID: 21099
		// (get) Token: 0x06010E4D RID: 69197 RVA: 0x003BEC34 File Offset: 0x003BCE34
		// (set) Token: 0x06010E4E RID: 69198 RVA: 0x003BEC98 File Offset: 0x003BCE98
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Indicating whether the panel item is expanded.")]
		[DefaultValue(false)]
		public bool Expanded
		{
			get
			{
				if (this.Items.Count == 0 && !this._contentTemplateIsSet)
				{
					this.ViewState["Expanded"] = false;
					return false;
				}
				return this.ViewState["Expanded"] != null && (bool)this.ViewState["Expanded"];
			}
			set
			{
				if (value && this.PanelBar != null && this.PanelBar.ExpandMode != PanelBarExpandMode.MultipleExpandedItems)
				{
					foreach (object obj in this.Owner.Items)
					{
						RadPanelItem radPanelItem = (RadPanelItem)obj;
						radPanelItem.Expanded = false;
					}
				}
				this.ViewState["Expanded"] = value;
			}
		}

		// Token: 0x1700526C RID: 21100
		// (get) Token: 0x06010E4F RID: 69199 RVA: 0x003BED24 File Offset: 0x003BCF24
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Level
		{
			get
			{
				if (this._owner is RadPanelBar)
				{
					return 0;
				}
				return ((RadPanelItem)this._owner).Level + 1;
			}
		}

		// Token: 0x1700526D RID: 21101
		// (get) Token: 0x06010E50 RID: 69200 RVA: 0x003BED47 File Offset: 0x003BCF47
		// (set) Token: 0x06010E51 RID: 69201 RVA: 0x003BED76 File Offset: 0x003BCF76
		[Description("The height of the element enclosing the child items.")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public Unit ChildGroupHeight
		{
			get
			{
				if (this.ViewState["ChildGroupHeight"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)this.ViewState["ChildGroupHeight"];
			}
			set
			{
				this.ViewState["ChildGroupHeight"] = value;
			}
		}

		// Token: 0x1700526E RID: 21102
		// (get) Token: 0x06010E52 RID: 69202 RVA: 0x003BED8E File Offset: 0x003BCF8E
		// (set) Token: 0x06010E53 RID: 69203 RVA: 0x003BEDBD File Offset: 0x003BCFBD
		[Description("CSS class applied to the element enclosing the child items.")]
		[Category("Appearance")]
		[DefaultValue("")]
		public string ChildGroupCssClass
		{
			get
			{
				if (this.ViewState["ChildGroupCssClass"] == null)
				{
					return "";
				}
				return (string)this.ViewState["ChildGroupCssClass"];
			}
			set
			{
				this.ViewState["ChildGroupCssClass"] = value;
			}
		}

		// Token: 0x1700526F RID: 21103
		// (get) Token: 0x06010E54 RID: 69204 RVA: 0x003BEDD0 File Offset: 0x003BCFD0
		// (set) Token: 0x06010E55 RID: 69205 RVA: 0x003BEDFF File Offset: 0x003BCFFF
		[Category("Appearance")]
		[DefaultValue("rpDisabled")]
		[Description("CSS Class name applied to the panel item when it is disabled.")]
		public new string DisabledCssClass
		{
			get
			{
				if (this.ViewState["DisabledCssClass"] == null)
				{
					return "rpDisabled";
				}
				return (string)this.ViewState["DisabledCssClass"];
			}
			set
			{
				this.ViewState["DisabledCssClass"] = value;
			}
		}

		// Token: 0x17005270 RID: 21104
		// (get) Token: 0x06010E56 RID: 69206 RVA: 0x003BEE12 File Offset: 0x003BD012
		// (set) Token: 0x06010E57 RID: 69207 RVA: 0x003BEE3D File Offset: 0x003BD03D
		[DefaultValue(true)]
		[Description("Whether the node should postback")]
		public bool PostBack
		{
			get
			{
				return this.ViewState["PostBack"] == null || (bool)this.ViewState["PostBack"];
			}
			set
			{
				this.ViewState["PostBack"] = value;
			}
		}

		// Token: 0x17005271 RID: 21105
		// (get) Token: 0x06010E58 RID: 69208 RVA: 0x003BEE55 File Offset: 0x003BD055
		// (set) Token: 0x06010E59 RID: 69209 RVA: 0x003BEE80 File Offset: 0x003BD080
		[Description("Whether the node should collapse")]
		[DefaultValue(false)]
		public bool PreventCollapse
		{
			get
			{
				return this.ViewState["PreventCollapse"] != null && (bool)this.ViewState["PreventCollapse"];
			}
			set
			{
				this.ViewState["PreventCollapse"] = value;
			}
		}

		// Token: 0x17005272 RID: 21106
		// (get) Token: 0x06010E5A RID: 69210 RVA: 0x003BEE98 File Offset: 0x003BD098
		// (set) Token: 0x06010E5B RID: 69211 RVA: 0x003BEED4 File Offset: 0x003BD0D4
		[Category("Behavior")]
		[Description("Indicating whether the item is selected.")]
		[DefaultValue(false)]
		public bool Selected
		{
			get
			{
				if (this.Owner == null)
				{
					return this._pendingSelect;
				}
				return this.ViewState["Selected"] != null && (bool)this.ViewState["Selected"];
			}
			set
			{
				if (this.Owner == null)
				{
					this._pendingSelect = value;
					return;
				}
				if (value && this.PanelBar != null)
				{
					foreach (RadPanelItem radPanelItem in this.PanelBar.GetAllItems())
					{
						radPanelItem.Selected = false;
					}
				}
				this.ViewState["Selected"] = value;
			}
		}

		// Token: 0x06010E5C RID: 69212 RVA: 0x003BEF58 File Offset: 0x003BD158
		public void ExpandParentItems()
		{
			IRadPanelItemContainer radPanelItemContainer = this;
			while (radPanelItemContainer is RadPanelItem)
			{
				RadPanelItem radPanelItem = radPanelItemContainer as RadPanelItem;
				radPanelItem.Expanded = true;
				radPanelItemContainer = radPanelItem.Owner;
			}
		}

		// Token: 0x06010E5D RID: 69213 RVA: 0x003BEF86 File Offset: 0x003BD186
		public RadPanelItem(string text, string navigateUrl) : this(text)
		{
			this.NavigateUrl = navigateUrl;
		}

		// Token: 0x17005273 RID: 21107
		// (get) Token: 0x06010E5E RID: 69214 RVA: 0x003BEF96 File Offset: 0x003BD196
		// (set) Token: 0x06010E5F RID: 69215 RVA: 0x003BEFC5 File Offset: 0x003BD1C5
		[Localizable(true)]
		[DefaultValue("")]
		[Description("The display text of the item.")]
		public override string Text
		{
			get
			{
				if (this.ViewState["Text"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["Text"];
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17005274 RID: 21108
		// (get) Token: 0x06010E60 RID: 69216 RVA: 0x003BEFD8 File Offset: 0x003BD1D8
		// (set) Token: 0x06010E61 RID: 69217 RVA: 0x003BEFE0 File Offset: 0x003BD1E0
		[Category("Navigation")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Bindable(true)]
		[UrlProperty]
		[Description("The URL to which the panel item navigates when selected.")]
		public override string NavigateUrl
		{
			get
			{
				return base.NavigateUrl;
			}
			set
			{
				base.NavigateUrl = value;
			}
		}

		// Token: 0x17005275 RID: 21109
		// (get) Token: 0x06010E62 RID: 69218 RVA: 0x003BEFE9 File Offset: 0x003BD1E9
		// (set) Token: 0x06010E63 RID: 69219 RVA: 0x003BEFF1 File Offset: 0x003BD1F1
		[Description("The navigation target used when the panel item is selected.")]
		[Category("Navigation")]
		[DefaultValue("")]
		[TypeConverter(typeof(TargetConverter))]
		public override string Target
		{
			get
			{
				return base.Target;
			}
			set
			{
				base.Target = value;
			}
		}

		// Token: 0x17005276 RID: 21110
		// (get) Token: 0x06010E64 RID: 69220 RVA: 0x003BEFFA File Offset: 0x003BD1FA
		// (set) Token: 0x06010E65 RID: 69221 RVA: 0x003BF029 File Offset: 0x003BD229
		[Localizable(true)]
		[DefaultValue("")]
		[Description("The value of the panel item")]
		[Category("Behavior")]
		public override string Value
		{
			get
			{
				if (this.ViewState["Value"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["Value"];
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x17005277 RID: 21111
		// (get) Token: 0x06010E66 RID: 69222 RVA: 0x003BF03C File Offset: 0x003BD23C
		// (set) Token: 0x06010E67 RID: 69223 RVA: 0x003BF044 File Offset: 0x003BD244
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

		// Token: 0x17005278 RID: 21112
		// (get) Token: 0x06010E68 RID: 69224 RVA: 0x003BF04D File Offset: 0x003BD24D
		// (set) Token: 0x06010E69 RID: 69225 RVA: 0x003BF067 File Offset: 0x003BD267
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(false)]
		[TemplateContainer(typeof(RadPanelItem))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				if (base.DesignMode && this._contentTemplateIsSet)
				{
					return null;
				}
				return base.Template;
			}
			set
			{
				base.Template = value;
				this._itemTemplateIsSet = (value != null);
			}
		}

		// Token: 0x17005279 RID: 21113
		// (get) Token: 0x06010E6A RID: 69226 RVA: 0x003BF07D File Offset: 0x003BD27D
		// (set) Token: 0x06010E6B RID: 69227 RVA: 0x003BF097 File Offset: 0x003BD297
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateInstance(TemplateInstance.Single)]
		[Bindable(false)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RadPanelItem))]
		public virtual ITemplate ContentTemplate
		{
			get
			{
				if (base.DesignMode && this._itemTemplateIsSet)
				{
					return null;
				}
				return base.Template;
			}
			set
			{
				base.Template = value;
				this._contentTemplateIsSet = (value != null);
			}
		}

		// Token: 0x1700527A RID: 21114
		// (get) Token: 0x06010E6C RID: 69228 RVA: 0x003BF0AD File Offset: 0x003BD2AD
		// (set) Token: 0x06010E6D RID: 69229 RVA: 0x003BF0B5 File Offset: 0x003BD2B5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateInstance(TemplateInstance.Single)]
		[Browsable(false)]
		[Bindable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RadPanelItemHeaderTemplateContainer))]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this._headerTemplate;
			}
			set
			{
				this._headerTemplate = value;
			}
		}

		// Token: 0x1700527B RID: 21115
		// (get) Token: 0x06010E6E RID: 69230 RVA: 0x003BF0BE File Offset: 0x003BD2BE
		[Browsable(false)]
		public RadPanelItemHeaderTemplateContainer Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x1700527C RID: 21116
		// (get) Token: 0x06010E6F RID: 69231 RVA: 0x003BF0C6 File Offset: 0x003BD2C6
		// (set) Token: 0x06010E70 RID: 69232 RVA: 0x003BF0F5 File Offset: 0x003BD2F5
		[Description("The URL for the image for the Item.")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Appearance")]
		[UrlProperty]
		public override string ImageUrl
		{
			get
			{
				if (this.ViewState["ImageUrl"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["ImageUrl"];
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x1700527D RID: 21117
		// (get) Token: 0x06010E71 RID: 69233 RVA: 0x003BF108 File Offset: 0x003BD308
		// (set) Token: 0x06010E72 RID: 69234 RVA: 0x003BF128 File Offset: 0x003BD328
		[Category("Appearance")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The image used when the node is hovered.")]
		[UrlProperty]
		public override string HoveredImageUrl
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

		// Token: 0x1700527E RID: 21118
		// (get) Token: 0x06010E73 RID: 69235 RVA: 0x003BF13B File Offset: 0x003BD33B
		// (set) Token: 0x06010E74 RID: 69236 RVA: 0x003BF16A File Offset: 0x003BD36A
		[Category("Appearance")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("The path to an image to display for the item when it is disabled.")]
		[UrlProperty]
		public string DisabledImageUrl
		{
			get
			{
				if (this.ViewState["DisabledImageUrl"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["DisabledImageUrl"];
			}
			set
			{
				this.ViewState["DisabledImageUrl"] = value;
			}
		}

		// Token: 0x1700527F RID: 21119
		// (get) Token: 0x06010E75 RID: 69237 RVA: 0x003BF17D File Offset: 0x003BD37D
		// (set) Token: 0x06010E76 RID: 69238 RVA: 0x003BF1AC File Offset: 0x003BD3AC
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Appearance")]
		[Description("The path to an image to display for the item when it is selected.")]
		[UrlProperty]
		public string SelectedImageUrl
		{
			get
			{
				if (this.ViewState["SelectedImageUrl"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["SelectedImageUrl"];
			}
			set
			{
				this.ViewState["SelectedImageUrl"] = value;
			}
		}

		// Token: 0x17005280 RID: 21120
		// (get) Token: 0x06010E77 RID: 69239 RVA: 0x003BF1BF File Offset: 0x003BD3BF
		// (set) Token: 0x06010E78 RID: 69240 RVA: 0x003BF1EE File Offset: 0x003BD3EE
		[UrlProperty]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Appearance")]
		[Description("The path to an image to display for the item when it is expanded.")]
		public string ExpandedImageUrl
		{
			get
			{
				if (this.ViewState["ExpandedImageUrl"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["ExpandedImageUrl"];
			}
			set
			{
				this.ViewState["ExpandedImageUrl"] = value;
			}
		}

		// Token: 0x17005281 RID: 21121
		// (get) Token: 0x06010E79 RID: 69241 RVA: 0x003BF201 File Offset: 0x003BD401
		// (set) Token: 0x06010E7A RID: 69242 RVA: 0x003BF209 File Offset: 0x003BD409
		[DefaultValue("")]
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

		// Token: 0x17005282 RID: 21122
		// (get) Token: 0x06010E7B RID: 69243 RVA: 0x003BF212 File Offset: 0x003BD412
		// (set) Token: 0x06010E7C RID: 69244 RVA: 0x003BF241 File Offset: 0x003BD441
		[DefaultValue("rpClicked")]
		[Description("CSS class applied to the panel item when it is clicked.")]
		[Category("Appearance")]
		public string ClickedCssClass
		{
			get
			{
				if (this.ViewState["ClickedCssClass"] == null)
				{
					return "rpClicked";
				}
				return (string)this.ViewState["ClickedCssClass"];
			}
			set
			{
				this.ViewState["ClickedCssClass"] = value;
			}
		}

		// Token: 0x17005283 RID: 21123
		// (get) Token: 0x06010E7D RID: 69245 RVA: 0x003BF254 File Offset: 0x003BD454
		// (set) Token: 0x06010E7E RID: 69246 RVA: 0x003BF283 File Offset: 0x003BD483
		[DefaultValue("rpSelected")]
		[Category("Appearance")]
		[Description("CSS Class name applied to the panel item when it is selected.")]
		public string SelectedCssClass
		{
			get
			{
				if (this.ViewState["SelectedCssClass"] == null)
				{
					return "rpSelected";
				}
				return (string)this.ViewState["SelectedCssClass"];
			}
			set
			{
				this.ViewState["SelectedCssClass"] = value;
			}
		}

		// Token: 0x17005284 RID: 21124
		// (get) Token: 0x06010E7F RID: 69247 RVA: 0x003BF296 File Offset: 0x003BD496
		// (set) Token: 0x06010E80 RID: 69248 RVA: 0x003BF2C5 File Offset: 0x003BD4C5
		[Category("Appearance")]
		[Description("CSS class applied to the panel item when it is expanded.")]
		[DefaultValue("rpExpanded")]
		public string ExpandedCssClass
		{
			get
			{
				if (this.ViewState["ExpandedCssClass"] == null)
				{
					return "rpExpanded";
				}
				return (string)this.ViewState["ExpandedCssClass"];
			}
			set
			{
				this.ViewState["ExpandedCssClass"] = value;
			}
		}

		// Token: 0x17005285 RID: 21125
		// (get) Token: 0x06010E81 RID: 69249 RVA: 0x003BF2D8 File Offset: 0x003BD4D8
		// (set) Token: 0x06010E82 RID: 69250 RVA: 0x003BF307 File Offset: 0x003BD507
		[Category("Appearance")]
		[DefaultValue("rpFocused")]
		[Description("CSS class applied to the panel item when it is focused.")]
		public string FocusedCssClass
		{
			get
			{
				if (this.ViewState["FocusedCssClass"] == null)
				{
					return "rpFocused";
				}
				return (string)this.ViewState["FocusedCssClass"];
			}
			set
			{
				this.ViewState["FocusedCssClass"] = value;
			}
		}

		// Token: 0x06010E83 RID: 69251 RVA: 0x003BF31C File Offset: 0x003BD51C
		public void ApplyHeaderTemplate()
		{
			if (this._headerTemplate != null)
			{
				if (this._header == null)
				{
					this._header = new RadPanelItemHeaderTemplateContainer(this);
					this.Controls.Add(this._header);
				}
				this._header.Controls.Clear();
				this._headerTemplate.InstantiateIn(this._header);
			}
		}

		// Token: 0x04004B97 RID: 19351
		private const string CannotSetBothItemAndContentTemplateOnItemExceptionMessage = "Cannot set both ItemTemplate and ContentTemplate on a RadPanelItem.";

		// Token: 0x04004B98 RID: 19352
		private const string CannotSetContentTemplateOnItemWhichHasChildItems = "Cannot set ContentTemplate on a RadPanelItem, which has child Items.";

		// Token: 0x04004B99 RID: 19353
		private IRadPanelItemContainer _owner;

		// Token: 0x04004B9A RID: 19354
		internal bool _itemTemplateIsSet;

		// Token: 0x04004B9B RID: 19355
		internal bool _contentTemplateIsSet;

		// Token: 0x04004B9C RID: 19356
		private bool _pendingSelect;

		// Token: 0x04004B9D RID: 19357
		private IRenderer _renderer;

		// Token: 0x04004B9E RID: 19358
		private bool _controlsTraversed;

		// Token: 0x04004B9F RID: 19359
		private bool _templated;

		// Token: 0x04004BA0 RID: 19360
		private ITemplate _headerTemplate;

		// Token: 0x04004BA1 RID: 19361
		private RadPanelItemHeaderTemplateContainer _header;
	}
}
