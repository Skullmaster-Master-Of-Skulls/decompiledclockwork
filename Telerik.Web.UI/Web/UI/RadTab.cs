using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Telerik.Web.UI.TabStrip.Rendering;

namespace Telerik.Web.UI
{
	// Token: 0x02001ADB RID: 6875
	[DefaultProperty("Text")]
	[XmlRoot("Tab")]
	[ToolboxItem(false)]
	public class RadTab : NavigationItem, IRadTabContainer
	{
		// Token: 0x06010A5E RID: 68190 RVA: 0x003B6424 File Offset: 0x003B4624
		public RadTab()
		{
		}

		// Token: 0x06010A5F RID: 68191 RVA: 0x003B642C File Offset: 0x003B462C
		public RadTab(string text)
		{
			this.Text = text;
		}

		// Token: 0x06010A60 RID: 68192 RVA: 0x003B643B File Offset: 0x003B463B
		public RadTab(string text, string value) : this(text)
		{
			this.Value = value;
		}

		// Token: 0x170050FA RID: 20730
		// (get) Token: 0x06010A61 RID: 68193 RVA: 0x003B644B File Offset: 0x003B464B
		// (set) Token: 0x06010A62 RID: 68194 RVA: 0x003B646C File Offset: 0x003B466C
		[DefaultValue(false)]
		[Description("Whether the tab will be displayed as separator.")]
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

		// Token: 0x170050FB RID: 20731
		// (get) Token: 0x06010A63 RID: 68195 RVA: 0x003B6484 File Offset: 0x003B4684
		// (set) Token: 0x06010A64 RID: 68196 RVA: 0x003B648C File Offset: 0x003B468C
		[Bindable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[TemplateContainer(typeof(RadTab))]
		public virtual ITemplate TabTemplate
		{
			get
			{
				return this.Template;
			}
			set
			{
				this.Template = value;
			}
		}

		// Token: 0x170050FC RID: 20732
		// (get) Token: 0x06010A65 RID: 68197 RVA: 0x003B6495 File Offset: 0x003B4695
		[Browsable(false)]
		public int Level
		{
			get
			{
				if (this.Owner is RadTabStrip)
				{
					return 0;
				}
				return ((RadTab)this.Owner).Level + 1;
			}
		}

		// Token: 0x170050FD RID: 20733
		// (get) Token: 0x06010A66 RID: 68198 RVA: 0x003B64B8 File Offset: 0x003B46B8
		// (set) Token: 0x06010A67 RID: 68199 RVA: 0x003B64E6 File Offset: 0x003B46E6
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Specifies whether the tab is selected.")]
		public bool Selected
		{
			get
			{
				if (this.Owner == null)
				{
					return this._cachedSelected;
				}
				return this.Enabled && this.Owner.SelectedIndex == base.Index;
			}
			set
			{
				if (this.Owner == null)
				{
					this._cachedSelected = value;
					return;
				}
				this.Owner.SelectedIndex = (value ? base.Index : -1);
			}
		}

		// Token: 0x170050FE RID: 20734
		// (get) Token: 0x06010A68 RID: 68200 RVA: 0x003B650F File Offset: 0x003B470F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadTabStrip TabStrip
		{
			get
			{
				return (RadTabStrip)base.Container;
			}
		}

		// Token: 0x170050FF RID: 20735
		// (get) Token: 0x06010A69 RID: 68201 RVA: 0x003B651C File Offset: 0x003B471C
		// (set) Token: 0x06010A6A RID: 68202 RVA: 0x003B653D File Offset: 0x003B473D
		[Description("Whether the tab should postback")]
		[DefaultValue(true)]
		[Category("Behavior")]
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

		// Token: 0x17005100 RID: 20736
		// (get) Token: 0x06010A6B RID: 68203 RVA: 0x003B6555 File Offset: 0x003B4755
		// (set) Token: 0x06010A6C RID: 68204 RVA: 0x003B655D File Offset: 0x003B475D
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

		// Token: 0x17005101 RID: 20737
		// (get) Token: 0x06010A6D RID: 68205 RVA: 0x003B6566 File Offset: 0x003B4766
		// (set) Token: 0x06010A6E RID: 68206 RVA: 0x003B6587 File Offset: 0x003B4787
		[DefaultValue(false)]
		[Category("Scrolling")]
		[Description("Whether child tabs are scrolled.")]
		public bool ScrollChildren
		{
			get
			{
				return (bool)(this.ViewState["ScrollChildren"] ?? false);
			}
			set
			{
				this.ViewState["ScrollChildren"] = value;
			}
		}

		// Token: 0x17005102 RID: 20738
		// (get) Token: 0x06010A6F RID: 68207 RVA: 0x003B659F File Offset: 0x003B479F
		// (set) Token: 0x06010A70 RID: 68208 RVA: 0x003B65C0 File Offset: 0x003B47C0
		[Category("Scrolling")]
		[DefaultValue(TabStripScrollButtonsPosition.Right)]
		[Description("The position of the scroll buttons.")]
		public TabStripScrollButtonsPosition ScrollButtonsPosition
		{
			get
			{
				return (TabStripScrollButtonsPosition)(this.ViewState["ScrollButtonsPosition"] ?? TabStripScrollButtonsPosition.Right);
			}
			set
			{
				this.ViewState["ScrollButtonsPosition"] = value;
			}
		}

		// Token: 0x17005103 RID: 20739
		// (get) Token: 0x06010A71 RID: 68209 RVA: 0x003B65D8 File Offset: 0x003B47D8
		// (set) Token: 0x06010A72 RID: 68210 RVA: 0x003B65F9 File Offset: 0x003B47F9
		[Category("Scrolling")]
		[Description("Gets or sets the position of the scrollable band of tabs relative to the beginning of the scrolling area.")]
		[Bindable(true)]
		[DefaultValue(0)]
		public int ScrollPosition
		{
			get
			{
				return (int)(this.ViewState["ScrollPosition"] ?? 0);
			}
			set
			{
				this.ViewState["ScrollPosition"] = value;
			}
		}

		// Token: 0x17005104 RID: 20740
		// (get) Token: 0x06010A73 RID: 68211 RVA: 0x003B6611 File Offset: 0x003B4811
		// (set) Token: 0x06010A74 RID: 68212 RVA: 0x003B6632 File Offset: 0x003B4832
		[DefaultValue(false)]
		[Category("Scrolling")]
		[Description("Whether to scroll directly to the next tab.")]
		public bool PerTabScrolling
		{
			get
			{
				return (bool)(this.ViewState["PerTabScrolling"] ?? false);
			}
			set
			{
				this.ViewState["PerTabScrolling"] = value;
			}
		}

		// Token: 0x17005105 RID: 20741
		// (get) Token: 0x06010A75 RID: 68213 RVA: 0x003B664A File Offset: 0x003B484A
		// (set) Token: 0x06010A76 RID: 68214 RVA: 0x003B666B File Offset: 0x003B486B
		[DefaultValue(-1)]
		[Category("Behavior")]
		[Description("The index of the selected child tab")]
		public int SelectedIndex
		{
			get
			{
				return (int)(this.ViewState["SelectedIndex"] ?? -1);
			}
			set
			{
				this.ViewState["SelectedIndex"] = value;
			}
		}

		// Token: 0x17005106 RID: 20742
		// (get) Token: 0x06010A77 RID: 68215 RVA: 0x003B6683 File Offset: 0x003B4883
		[Browsable(false)]
		public RadTab SelectedTab
		{
			get
			{
				if (this.SelectedIndex == -1 || this.SelectedIndex >= this.Tabs.Count)
				{
					return null;
				}
				return this.Tabs[this.SelectedIndex];
			}
		}

		// Token: 0x17005107 RID: 20743
		// (get) Token: 0x06010A78 RID: 68216 RVA: 0x003B66B4 File Offset: 0x003B48B4
		// (set) Token: 0x06010A79 RID: 68217 RVA: 0x003B66BC File Offset: 0x003B48BC
		[Browsable(false)]
		public IRadTabContainer Owner
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

		// Token: 0x17005108 RID: 20744
		// (get) Token: 0x06010A7A RID: 68218 RVA: 0x003B66C5 File Offset: 0x003B48C5
		[MergableProperty(false)]
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadTabCollection Tabs
		{
			get
			{
				return (RadTabCollection)base.Children;
			}
		}

		// Token: 0x17005109 RID: 20745
		// (get) Token: 0x06010A7B RID: 68219 RVA: 0x003B66D2 File Offset: 0x003B48D2
		// (set) Token: 0x06010A7C RID: 68220 RVA: 0x003B66F2 File Offset: 0x003B48F2
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("CSS Class name applied when the tab is selected.")]
		public string SelectedCssClass
		{
			get
			{
				return (string)(this.ViewState["SelectedCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["SelectedCssClass"] = value;
			}
		}

		// Token: 0x1700510A RID: 20746
		// (get) Token: 0x06010A7D RID: 68221 RVA: 0x003B6705 File Offset: 0x003B4905
		// (set) Token: 0x06010A7E RID: 68222 RVA: 0x003B6725 File Offset: 0x003B4925
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("CSS Class name applied when the tab is disabled.")]
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

		// Token: 0x1700510B RID: 20747
		// (get) Token: 0x06010A7F RID: 68223 RVA: 0x003B6738 File Offset: 0x003B4938
		// (set) Token: 0x06010A80 RID: 68224 RVA: 0x003B6758 File Offset: 0x003B4958
		[Description("CSS Class name applied when the tab is hovered.")]
		[DefaultValue("")]
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

		// Token: 0x1700510C RID: 20748
		// (get) Token: 0x06010A81 RID: 68225 RVA: 0x003B676B File Offset: 0x003B496B
		// (set) Token: 0x06010A82 RID: 68226 RVA: 0x003B678B File Offset: 0x003B498B
		[DefaultValue("")]
		[Description("CSS Class name applied on the outmost tab wrapper (<LI>).")]
		[Category("Appearance")]
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

		// Token: 0x1700510D RID: 20749
		// (get) Token: 0x06010A83 RID: 68227 RVA: 0x003B679E File Offset: 0x003B499E
		// (set) Token: 0x06010A84 RID: 68228 RVA: 0x003B67BE File Offset: 0x003B49BE
		[Description("CSS Class name applied to child tab strip.")]
		[DefaultValue("")]
		[Category("Appearance")]
		public string ChildGroupCssClass
		{
			get
			{
				return (string)(this.ViewState["ChildGroupCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ChildGroupCssClass"] = value;
			}
		}

		// Token: 0x1700510E RID: 20750
		// (get) Token: 0x06010A85 RID: 68229 RVA: 0x003B67D1 File Offset: 0x003B49D1
		// (set) Token: 0x06010A86 RID: 68230 RVA: 0x003B67F2 File Offset: 0x003B49F2
		[Description("Whether the next tab will displayed one a new line.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool IsBreak
		{
			get
			{
				return (bool)(this.ViewState["IsBreak"] ?? false);
			}
			set
			{
				this.ViewState["IsBreak"] = value;
			}
		}

		// Token: 0x1700510F RID: 20751
		// (get) Token: 0x06010A87 RID: 68231 RVA: 0x003B680A File Offset: 0x003B4A0A
		// (set) Token: 0x06010A88 RID: 68232 RVA: 0x003B682A File Offset: 0x003B4A2A
		[Bindable(true)]
		[Description("Gets or sets the ID of the PageView in a RadMultiPage that will be switched when this Tab is pressed.")]
		[Category("Setup")]
		[DefaultValue("")]
		public string PageViewID
		{
			get
			{
				return (string)(this.ViewState["PageViewID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["PageViewID"] = value;
			}
		}

		// Token: 0x17005110 RID: 20752
		// (get) Token: 0x06010A89 RID: 68233 RVA: 0x003B683D File Offset: 0x003B4A3D
		// (set) Token: 0x06010A8A RID: 68234 RVA: 0x003B6845 File Offset: 0x003B4A45
		[DefaultValue("")]
		[Description("The text of the tab")]
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

		// Token: 0x17005111 RID: 20753
		// (get) Token: 0x06010A8B RID: 68235 RVA: 0x003B684E File Offset: 0x003B4A4E
		// (set) Token: 0x06010A8C RID: 68236 RVA: 0x003B6856 File Offset: 0x003B4A56
		[Localizable(true)]
		[DefaultValue("")]
		[Description("Custom data associated with the tab")]
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

		// Token: 0x17005112 RID: 20754
		// (get) Token: 0x06010A8D RID: 68237 RVA: 0x003B685F File Offset: 0x003B4A5F
		// (set) Token: 0x06010A8E RID: 68238 RVA: 0x003B6867 File Offset: 0x003B4A67
		[Editor("Telerik.Web.Design.ControlItemUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Localizable(true)]
		[UrlProperty]
		[DefaultValue("")]
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

		// Token: 0x17005113 RID: 20755
		// (get) Token: 0x06010A8F RID: 68239 RVA: 0x003B6870 File Offset: 0x003B4A70
		// (set) Token: 0x06010A90 RID: 68240 RVA: 0x003B6890 File Offset: 0x003B4A90
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[Category("Appearance")]
		[Localizable(true)]
		[Description("The URL of the image displayed for the tab.")]
		public override string ImageUrl
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

		// Token: 0x17005114 RID: 20756
		// (get) Token: 0x06010A91 RID: 68241 RVA: 0x003B68A3 File Offset: 0x003B4AA3
		// (set) Token: 0x06010A92 RID: 68242 RVA: 0x003B68C3 File Offset: 0x003B4AC3
		[Localizable(true)]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[UrlProperty]
		[Category("Appearance")]
		[Description("The URL of the image displayed for the tab when it is hovered.")]
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

		// Token: 0x17005115 RID: 20757
		// (get) Token: 0x06010A93 RID: 68243 RVA: 0x003B68D6 File Offset: 0x003B4AD6
		// (set) Token: 0x06010A94 RID: 68244 RVA: 0x003B68F6 File Offset: 0x003B4AF6
		[DefaultValue("")]
		[Localizable(true)]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[Category("Appearance")]
		[Description("The URL of the image displayed for the tab when it is selected.")]
		public string SelectedImageUrl
		{
			get
			{
				return (string)(this.ViewState["SelectedImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["SelectedImageUrl"] = value;
			}
		}

		// Token: 0x17005116 RID: 20758
		// (get) Token: 0x06010A95 RID: 68245 RVA: 0x003B6909 File Offset: 0x003B4B09
		// (set) Token: 0x06010A96 RID: 68246 RVA: 0x003B6929 File Offset: 0x003B4B29
		[DefaultValue("")]
		[Category("Appearance")]
		[Localizable(true)]
		[Description("The URL of the image displayed for the tab when it is disabled.")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		public string DisabledImageUrl
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

		// Token: 0x17005117 RID: 20759
		// (get) Token: 0x06010A97 RID: 68247 RVA: 0x003B693C File Offset: 0x003B4B3C
		// (set) Token: 0x06010A98 RID: 68248 RVA: 0x003B6944 File Offset: 0x003B4B44
		[Description("The target window or frame")]
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

		// Token: 0x17005118 RID: 20760
		// (get) Token: 0x06010A99 RID: 68249 RVA: 0x003B6950 File Offset: 0x003B4B50
		[Browsable(false)]
		public RadPageView PageView
		{
			get
			{
				if (this.TabStrip.MultiPage == null)
				{
					return null;
				}
				if (string.IsNullOrEmpty(this.PageViewID))
				{
					if (!string.IsNullOrEmpty(this.ImplicitPageViewID))
					{
						return (RadPageView)this.TabStrip.MultiPage.FindControl(this.ImplicitPageViewID);
					}
					int num = this.TabStrip.GetAllTabs().IndexOf(this);
					if (this.TabStrip.MultiPage.PageViews.Count > num)
					{
						return this.TabStrip.MultiPage.PageViews[num];
					}
				}
				return (RadPageView)this.TabStrip.MultiPage.FindControl(this.PageViewID);
			}
		}

		// Token: 0x06010A9A RID: 68250 RVA: 0x003B6A00 File Offset: 0x003B4C00
		[Description("Selects recursively all parent tabs in the hierarchy")]
		public void SelectParents()
		{
			for (RadTab radTab = this; radTab != null; radTab = (radTab.Owner as RadTab))
			{
				radTab.Selected = true;
			}
		}

		// Token: 0x06010A9B RID: 68251 RVA: 0x003B6A27 File Offset: 0x003B4C27
		internal void ApplySelection()
		{
			if (this._cachedSelected)
			{
				this.Selected = true;
				this._cachedSelected = false;
			}
		}

		// Token: 0x06010A9C RID: 68252 RVA: 0x003B6A3F File Offset: 0x003B4C3F
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadTabCollection(this);
		}

		// Token: 0x06010A9D RID: 68253 RVA: 0x003B6A48 File Offset: 0x003B4C48
		protected internal override void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			base.LoadFromDictionary(dictionary);
			if (dictionary.ContainsKey("navigateUrl"))
			{
				this.NavigateUrl = dictionary["navigateUrl"].ToString();
			}
			if (dictionary.ContainsKey("cssClass"))
			{
				this.CssClass = dictionary["cssClass"].ToString();
			}
			if (dictionary.ContainsKey("outerCssClass"))
			{
				this.OuterCssClass = dictionary["outerCssClass"].ToString();
			}
			if (dictionary.ContainsKey("hoveredCssClass"))
			{
				this.HoveredCssClass = dictionary["hoveredCssClass"].ToString();
			}
			if (dictionary.ContainsKey("selectedCssClass"))
			{
				this.SelectedCssClass = dictionary["selectedCssClass"].ToString();
			}
			if (dictionary.ContainsKey("disabledCssClass"))
			{
				this.DisabledCssClass = dictionary["disabledCssClass"].ToString();
			}
			if (dictionary.ContainsKey("target"))
			{
				this.Target = dictionary["target"].ToString();
			}
			if (dictionary.ContainsKey("isBreak"))
			{
				this.IsBreak = (bool)dictionary["isBreak"];
			}
			if (dictionary.ContainsKey("disabledImageUrl"))
			{
				this.DisabledImageUrl = dictionary["disabledImageUrl"].ToString();
			}
			if (dictionary.ContainsKey("selectedImageUrl"))
			{
				this.SelectedImageUrl = dictionary["selectedImageUrl"].ToString();
			}
		}

		// Token: 0x06010A9E RID: 68254 RVA: 0x003B6BBC File Offset: 0x003B4DBC
		internal override void PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			base.PopulateFromDataItem(properties, dataItem, dataMember, depth);
			IHierarchyData hierarchyData = dataItem as IHierarchyData;
			if (hierarchyData != null && string.Equals(hierarchyData.Path, this.TabStrip.CurrentSiteMapUrl, StringComparison.OrdinalIgnoreCase))
			{
				this.SelectParents();
				this.Selected = true;
			}
		}

		// Token: 0x17005119 RID: 20761
		// (get) Token: 0x06010A9F RID: 68255 RVA: 0x003B6C04 File Offset: 0x003B4E04
		private IRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = RendererFactory.CreateTabRenderer(this);
				}
				return this._renderer;
			}
		}

		// Token: 0x1700511A RID: 20762
		// (get) Token: 0x06010AA0 RID: 68256 RVA: 0x003B6C20 File Offset: 0x003B4E20
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x06010AA1 RID: 68257 RVA: 0x003B6C2D File Offset: 0x003B4E2D
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x06010AA2 RID: 68258 RVA: 0x003B6C3B File Offset: 0x003B4E3B
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06010AA3 RID: 68259 RVA: 0x003B6C49 File Offset: 0x003B4E49
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			if (this.IsBreak)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtsBreak");
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				writer.RenderEndTag();
			}
		}

		// Token: 0x1700511B RID: 20763
		// (get) Token: 0x06010AA4 RID: 68260 RVA: 0x003B6C78 File Offset: 0x003B4E78
		internal int VisibleIndex
		{
			get
			{
				if (!this.Visible)
				{
					return -1;
				}
				int num = 0;
				for (int i = 0; i < base.Index; i++)
				{
					if (!this.Owner.Tabs[i].Visible)
					{
						num++;
					}
				}
				return base.Index - num;
			}
		}

		// Token: 0x1700511C RID: 20764
		// (get) Token: 0x06010AA5 RID: 68261 RVA: 0x003B6CC8 File Offset: 0x003B4EC8
		internal string CurrentImageUrl
		{
			get
			{
				if (!this.Enabled && !string.IsNullOrEmpty(this.DisabledImageUrl))
				{
					return base.ResolveClientUrl(this.DisabledImageUrl);
				}
				if (this.Selected && !string.IsNullOrEmpty(this.SelectedImageUrl))
				{
					return base.ResolveClientUrl(this.SelectedImageUrl);
				}
				if (!string.IsNullOrEmpty(this.ImageUrl))
				{
					return base.ResolveClientUrl(this.ImageUrl);
				}
				return null;
			}
		}

		// Token: 0x1700511D RID: 20765
		// (get) Token: 0x06010AA6 RID: 68262 RVA: 0x003B6D34 File Offset: 0x003B4F34
		internal int ReorderedIndex
		{
			get
			{
				IList<IList<RadTab>> list = RadTab.ReorderTabs(this.Owner);
				IList<RadTab> list2 = this.FindGroupWhichContainsTab(list);
				int num = list2.IndexOf(this);
				for (int i = 0; i < list.IndexOf(list2); i++)
				{
					num += list[i].Count;
				}
				return num;
			}
		}

		// Token: 0x06010AA7 RID: 68263 RVA: 0x003B6D80 File Offset: 0x003B4F80
		internal IList<RadTab> FindGroupWhichContainsTab(IList<IList<RadTab>> groups)
		{
			foreach (IList<RadTab> list in groups)
			{
				if (list.Contains(this))
				{
					return list;
				}
			}
			return null;
		}

		// Token: 0x06010AA8 RID: 68264 RVA: 0x003B6DD4 File Offset: 0x003B4FD4
		internal static IList<IList<RadTab>> ReorderTabs(IRadTabContainer container)
		{
			IList<IList<RadTab>> list = new List<IList<RadTab>>();
			IList<RadTab> list2 = new List<RadTab>();
			list.Add(list2);
			IList<RadTab> list3 = null;
			foreach (ControlItem controlItem in container.Tabs.VisibleItems)
			{
				RadTab radTab = (RadTab)controlItem;
				list2.Add(radTab);
				if (radTab.Selected)
				{
					list3 = list2;
				}
				if (radTab.IsBreak)
				{
					list2 = new List<RadTab>();
					list.Add(list2);
				}
			}
			if (list3 != null)
			{
				int index = list.IndexOf(list3);
				IList<RadTab> item = list[list.Count - 1];
				list.Remove(item);
				list.Insert(index, item);
				list.Remove(list3);
				list.Add(list3);
			}
			return list;
		}

		// Token: 0x06010AA9 RID: 68265 RVA: 0x003B6EA4 File Offset: 0x003B50A4
		internal void RenderChildControls(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
		}

		// Token: 0x1700511E RID: 20766
		// (get) Token: 0x06010AAA RID: 68266 RVA: 0x003B6EAD File Offset: 0x003B50AD
		// (set) Token: 0x06010AAB RID: 68267 RVA: 0x003B6ECD File Offset: 0x003B50CD
		internal string ImplicitPageViewID
		{
			get
			{
				return ((string)this.ViewState["ImplicitPageViewID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ImplicitPageViewID"] = value;
			}
		}

		// Token: 0x04004A5E RID: 19038
		private IRadTabContainer _owner;

		// Token: 0x04004A5F RID: 19039
		private bool _cachedSelected;

		// Token: 0x04004A60 RID: 19040
		private IRenderer _renderer;
	}
}
