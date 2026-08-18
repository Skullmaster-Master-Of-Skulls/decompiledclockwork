using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.Design;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.Web.UI.TabStrip.Rendering;

namespace Telerik.Web.UI
{
	// Token: 0x02000F15 RID: 3861
	[RequiredScript(typeof(ScrollingScripts))]
	[ToolboxData("<{0}:RadTabStrip runat=server></{0}:RadTabStrip>")]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadTabStrip), "Telerik.Web.UI.TabStrip.png")]
	[Designer("Telerik.Web.Design.RadTabStripDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(jQueryPlugins))]
	[DefaultEvent("TabClick")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadTabStrip))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[EmbeddedSkin("TabStrip", typeof(RadTabStrip))]
	[EmbeddedSkin("TabStrip", "Default", typeof(RadTabStrip))]
	[RequiredScript(typeof(MaterialRipple))]
	[ClientScriptResource("Telerik.Web.UI.RadTabStrip", "Telerik.Web.UI.TabStrip.RadTabStripScripts.js")]
	[XmlRoot("TabStrip")]
	[DefaultProperty("Tabs")]
	public class RadTabStrip : HierarchicalControlItemContainer, IRadTabContainer, IPostBackEventHandler
	{
		// Token: 0x17002E42 RID: 11842
		// (get) Token: 0x0600925B RID: 37467 RVA: 0x0020F2B3 File Offset: 0x0020D4B3
		[Browsable(false)]
		public IList<ClientOperation<RadTab>> ClientChanges
		{
			get
			{
				return this._clientChanges;
			}
		}

		// Token: 0x17002E43 RID: 11843
		// (get) Token: 0x0600925C RID: 37468 RVA: 0x0020F2BB File Offset: 0x0020D4BB
		// (set) Token: 0x0600925D RID: 37469 RVA: 0x0020F2DC File Offset: 0x0020D4DC
		[Description("Whether child tabs are scrolled.")]
		[Category("Scrolling")]
		[ClientPropertyName("_scrollChildren")]
		[DefaultValue(false)]
		[ClientControlProperty]
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

		// Token: 0x17002E44 RID: 11844
		// (get) Token: 0x0600925E RID: 37470 RVA: 0x0020F2F4 File Offset: 0x0020D4F4
		// (set) Token: 0x0600925F RID: 37471 RVA: 0x0020F315 File Offset: 0x0020D515
		[Description("The position of the scroll buttons.")]
		[ClientControlProperty]
		[ClientPropertyName("_scrollButtonsPosition")]
		[DefaultValue(TabStripScrollButtonsPosition.Right)]
		[Category("Scrolling")]
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

		// Token: 0x17002E45 RID: 11845
		// (get) Token: 0x06009260 RID: 37472 RVA: 0x0020F32D File Offset: 0x0020D52D
		// (set) Token: 0x06009261 RID: 37473 RVA: 0x0020F34E File Offset: 0x0020D54E
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

		// Token: 0x17002E46 RID: 11846
		// (get) Token: 0x06009262 RID: 37474 RVA: 0x0020F366 File Offset: 0x0020D566
		// (set) Token: 0x06009263 RID: 37475 RVA: 0x0020F387 File Offset: 0x0020D587
		[ClientPropertyName("_perTabScrolling")]
		[ClientControlProperty]
		[Category("Scrolling")]
		[DefaultValue(false)]
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

		// Token: 0x17002E47 RID: 11847
		// (get) Token: 0x06009264 RID: 37476 RVA: 0x0020F39F File Offset: 0x0020D59F
		// (set) Token: 0x06009265 RID: 37477 RVA: 0x0020F3C0 File Offset: 0x0020D5C0
		[Category("Behavior")]
		[DefaultValue(-1)]
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

		// Token: 0x17002E48 RID: 11848
		// (get) Token: 0x06009266 RID: 37478 RVA: 0x0020F3D8 File Offset: 0x0020D5D8
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

		// Token: 0x17002E49 RID: 11849
		// (get) Token: 0x06009267 RID: 37479 RVA: 0x0020F409 File Offset: 0x0020D609
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("Telerik.Web.Design.ControlItemCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[MergableProperty(false)]
		public RadTabCollection Tabs
		{
			get
			{
				return (RadTabCollection)base.Children;
			}
		}

		// Token: 0x17002E4A RID: 11850
		// (get) Token: 0x06009268 RID: 37480 RVA: 0x0020F416 File Offset: 0x0020D616
		// (set) Token: 0x06009269 RID: 37481 RVA: 0x0020F41E File Offset: 0x0020D61E
		[TemplateContainer(typeof(RadTab))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Bindable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate TabTemplate
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

		// Token: 0x17002E4B RID: 11851
		// (get) Token: 0x0600926A RID: 37482 RVA: 0x0020F427 File Offset: 0x0020D627
		// (set) Token: 0x0600926B RID: 37483 RVA: 0x0020F448 File Offset: 0x0020D648
		[ClientControlProperty]
		[Description("Postback to the server when tabs are clicked.")]
		[Category("Behavior")]
		[ClientPropertyName("_autoPostBack")]
		[DefaultValue(false)]
		public bool AutoPostBack
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBack"] ?? false);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x17002E4C RID: 11852
		// (get) Token: 0x0600926C RID: 37484 RVA: 0x0020F460 File Offset: 0x0020D660
		// (set) Token: 0x0600926D RID: 37485 RVA: 0x0020F481 File Offset: 0x0020D681
		[ClientPropertyName("_enableDragToReorder")]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Whether drag-to-reorder is enabled.")]
		[ClientControlProperty]
		public bool EnableDragToReorder
		{
			get
			{
				return (bool)(this.ViewState["EnbaleDragToReorder"] ?? false);
			}
			set
			{
				this.ViewState["EnbaleDragToReorder"] = value;
			}
		}

		// Token: 0x17002E4D RID: 11853
		// (get) Token: 0x0600926E RID: 37486 RVA: 0x0020F499 File Offset: 0x0020D699
		// (set) Token: 0x0600926F RID: 37487 RVA: 0x0020F4BA File Offset: 0x0020D6BA
		[ClientPropertyName("enableAriaSupport")]
		[Description("When set to true enables support for WAI-ARIA")]
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x17002E4E RID: 11854
		// (get) Token: 0x06009270 RID: 37488 RVA: 0x0020F4D2 File Offset: 0x0020D6D2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[Category("Data")]
		[MergableProperty(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadTabBindingCollection DataBindings
		{
			get
			{
				return (RadTabBindingCollection)base.NavigationItemBindings;
			}
		}

		// Token: 0x17002E4F RID: 11855
		// (get) Token: 0x06009271 RID: 37489 RVA: 0x0020F4DF File Offset: 0x0020D6DF
		// (set) Token: 0x06009272 RID: 37490 RVA: 0x0020F4E7 File Offset: 0x0020D6E7
		[DefaultValue(false)]
		[Description("Whether tabs are cleared before databinding.")]
		[Category("Data")]
		[Bindable(false)]
		public override bool AppendDataBoundItems
		{
			get
			{
				return base.AppendDataBoundItems;
			}
			set
			{
				base.AppendDataBoundItems = value;
			}
		}

		// Token: 0x17002E50 RID: 11856
		// (get) Token: 0x06009273 RID: 37491 RVA: 0x0020F4F0 File Offset: 0x0020D6F0
		// (set) Token: 0x06009274 RID: 37492 RVA: 0x0020F4F8 File Offset: 0x0020D6F8
		public override int MaxDataBindDepth
		{
			get
			{
				return base.MaxDataBindDepth;
			}
			set
			{
				base.MaxDataBindDepth = value;
			}
		}

		// Token: 0x17002E51 RID: 11857
		// (get) Token: 0x06009275 RID: 37493 RVA: 0x0020F501 File Offset: 0x0020D701
		// (set) Token: 0x06009276 RID: 37494 RVA: 0x0020F509 File Offset: 0x0020D709
		[Description("The field in the data source which provides the tab text.")]
		[Category("Data")]
		[DefaultValue("")]
		public override string DataTextField
		{
			get
			{
				return base.DataTextField;
			}
			set
			{
				base.DataTextField = value;
			}
		}

		// Token: 0x17002E52 RID: 11858
		// (get) Token: 0x06009277 RID: 37495 RVA: 0x0020F512 File Offset: 0x0020D712
		// (set) Token: 0x06009278 RID: 37496 RVA: 0x0020F51A File Offset: 0x0020D71A
		[Category("Data")]
		[Description("The field in the data source which provides the tab value.")]
		[DefaultValue("")]
		public override string DataValueField
		{
			get
			{
				return base.DataValueField;
			}
			set
			{
				base.DataValueField = value;
			}
		}

		// Token: 0x17002E53 RID: 11859
		// (get) Token: 0x06009279 RID: 37497 RVA: 0x0020F523 File Offset: 0x0020D723
		// (set) Token: 0x0600927A RID: 37498 RVA: 0x0020F52B File Offset: 0x0020D72B
		[Description("The field in the data source which provides the NavigateUrl of tabs")]
		[Category("Data")]
		[DefaultValue("")]
		public override string DataNavigateUrlField
		{
			get
			{
				return base.DataNavigateUrlField;
			}
			set
			{
				base.DataNavigateUrlField = value;
			}
		}

		// Token: 0x17002E54 RID: 11860
		// (get) Token: 0x0600927B RID: 37499 RVA: 0x0020F534 File Offset: 0x0020D734
		// (set) Token: 0x0600927C RID: 37500 RVA: 0x0020F53C File Offset: 0x0020D73C
		[Description("The field of the data source that will be used as the child column in hierarchical databinding.")]
		[Category("Data")]
		[DefaultValue("")]
		public override string DataFieldID
		{
			get
			{
				return base.DataFieldID;
			}
			set
			{
				base.DataFieldID = value;
			}
		}

		// Token: 0x17002E55 RID: 11861
		// (get) Token: 0x0600927D RID: 37501 RVA: 0x0020F545 File Offset: 0x0020D745
		// (set) Token: 0x0600927E RID: 37502 RVA: 0x0020F54D File Offset: 0x0020D74D
		[Category("Data")]
		[Description("The field of the data source that will be used as the parent column in hierarchical databinding.")]
		[DefaultValue("")]
		public override string DataFieldParentID
		{
			get
			{
				return base.DataFieldParentID;
			}
			set
			{
				base.DataFieldParentID = value;
			}
		}

		// Token: 0x17002E56 RID: 11862
		// (get) Token: 0x0600927F RID: 37503 RVA: 0x0020F556 File Offset: 0x0020D756
		// (set) Token: 0x06009280 RID: 37504 RVA: 0x0020F55E File Offset: 0x0020D75E
		[Category("Data")]
		[Description("The formatting applied to the text field.")]
		[DefaultValue("")]
		public override string DataTextFormatString
		{
			get
			{
				return base.DataTextFormatString;
			}
			set
			{
				base.DataTextFormatString = value;
			}
		}

		// Token: 0x17002E57 RID: 11863
		// (get) Token: 0x06009281 RID: 37505 RVA: 0x0020F568 File Offset: 0x0020D768
		[Browsable(false)]
		public RadTab InnermostSelectedTab
		{
			get
			{
				RadTab selectedTab = this.SelectedTab;
				if (selectedTab == null)
				{
					return null;
				}
				while (selectedTab.SelectedTab != null)
				{
					selectedTab = selectedTab.SelectedTab;
				}
				return selectedTab;
			}
		}

		// Token: 0x17002E58 RID: 11864
		// (get) Token: 0x06009282 RID: 37506 RVA: 0x0020F591 File Offset: 0x0020D791
		// (set) Token: 0x06009283 RID: 37507 RVA: 0x0020F599 File Offset: 0x0020D799
		[ClientControlProperty]
		[ClientPropertyName("validationGroup")]
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the validation group to which this validation control belongs.")]
		public override string ValidationGroup
		{
			get
			{
				return base.ValidationGroup;
			}
			set
			{
				base.ValidationGroup = value;
			}
		}

		// Token: 0x17002E59 RID: 11865
		// (get) Token: 0x06009284 RID: 37508 RVA: 0x0020F5A2 File Offset: 0x0020D7A2
		// (set) Token: 0x06009285 RID: 37509 RVA: 0x0020F5AA File Offset: 0x0020D7AA
		[DefaultValue("")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[Category("Behavior")]
		[UrlProperty("*.aspx")]
		public override string PostBackUrl
		{
			get
			{
				return base.PostBackUrl;
			}
			set
			{
				base.PostBackUrl = value;
			}
		}

		// Token: 0x17002E5A RID: 11866
		// (get) Token: 0x06009286 RID: 37510 RVA: 0x0020F5B3 File Offset: 0x0020D7B3
		// (set) Token: 0x06009287 RID: 37511 RVA: 0x0020F5D3 File Offset: 0x0020D7D3
		[DefaultValue("")]
		[Description("Gets or sets the ID of the RadMultiPage control that will be controlled by this RadTabStrip.")]
		[TypeConverter("Telerik.Web.Design.MultiPageIDTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		public string MultiPageID
		{
			get
			{
				return (string)(this.ViewState["MultiPageID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["MultiPageID"] = value;
			}
		}

		// Token: 0x17002E5B RID: 11867
		// (get) Token: 0x06009288 RID: 37512 RVA: 0x0020F5E8 File Offset: 0x0020D7E8
		[Browsable(false)]
		public RadMultiPage MultiPage
		{
			get
			{
				if (string.IsNullOrEmpty(this.MultiPageID))
				{
					return null;
				}
				RadMultiPage radMultiPage = (RadMultiPage)this.NamingContainer.FindControl(this.MultiPageID);
				if (radMultiPage == null)
				{
					radMultiPage = (RadMultiPage)this.Page.FindControl(this.MultiPageID);
				}
				return radMultiPage;
			}
		}

		// Token: 0x17002E5C RID: 11868
		// (get) Token: 0x06009289 RID: 37513 RVA: 0x0020F636 File Offset: 0x0020D836
		// (set) Token: 0x0600928A RID: 37514 RVA: 0x0020F657 File Offset: 0x0020D857
		[Category("Behavior")]
		[Description("Whether the selected tab will fire the client-side events and postback on click")]
		[ClientControlProperty]
		[ClientPropertyName("clickSelectedTab")]
		[DefaultValue(false)]
		public bool ClickSelectedTab
		{
			get
			{
				return (bool)(this.ViewState["ClickSelectedTab"] ?? false);
			}
			set
			{
				this.ViewState["ClickSelectedTab"] = value;
			}
		}

		// Token: 0x17002E5D RID: 11869
		// (get) Token: 0x0600928B RID: 37515 RVA: 0x0020F66F File Offset: 0x0020D86F
		// (set) Token: 0x0600928C RID: 37516 RVA: 0x0020F690 File Offset: 0x0020D890
		[ClientPropertyName("_orientation")]
		[DefaultValue(TabStripOrientation.HorizontalTop)]
		[Category("Layout")]
		[Description("Orientation of the tabs.")]
		[ClientControlProperty]
		public TabStripOrientation Orientation
		{
			get
			{
				return (TabStripOrientation)(this.ViewState["Orientation"] ?? TabStripOrientation.HorizontalTop);
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x17002E5E RID: 11870
		// (get) Token: 0x0600928D RID: 37517 RVA: 0x0020F6A8 File Offset: 0x0020D8A8
		// (set) Token: 0x0600928E RID: 37518 RVA: 0x0020F6C9 File Offset: 0x0020D8C9
		[ClientControlProperty]
		[ClientPropertyName("_align")]
		[Bindable(true)]
		[Category("Layout")]
		[DefaultValue(TabStripAlign.Left)]
		[Description("Gets or sets the alignment of the RadTabStrip Tabs.")]
		public TabStripAlign Align
		{
			get
			{
				return (TabStripAlign)(this.ViewState["TabStripAlign"] ?? TabStripAlign.Left);
			}
			set
			{
				this.ViewState["TabStripAlign"] = value;
			}
		}

		// Token: 0x17002E5F RID: 11871
		// (get) Token: 0x0600928F RID: 37519 RVA: 0x0020F6E1 File Offset: 0x0020D8E1
		// (set) Token: 0x06009290 RID: 37520 RVA: 0x0020F702 File Offset: 0x0020D902
		[ClientControlProperty]
		[Description("Whether or not to move the selected tab row to the bottom in a multirow tabstrip.")]
		[Category("Layout")]
		[DefaultValue(false)]
		[ClientPropertyName("_reorderTabsOnSelect")]
		public bool ReorderTabsOnSelect
		{
			get
			{
				return (bool)(this.ViewState["ReorderTabsOnSelect"] ?? false);
			}
			set
			{
				this.ViewState["ReorderTabsOnSelect"] = value;
			}
		}

		// Token: 0x17002E60 RID: 11872
		// (get) Token: 0x06009291 RID: 37521 RVA: 0x0020F71A File Offset: 0x0020D91A
		// (set) Token: 0x06009292 RID: 37522 RVA: 0x0020F73B File Offset: 0x0020D93B
		[Category("Layout")]
		[Description("Whether there will be a line on the whole length of the tabstrip.")]
		[DefaultValue(false)]
		public bool ShowBaseLine
		{
			get
			{
				return (bool)(this.ViewState["ShowBaseLine"] ?? false);
			}
			set
			{
				this.ViewState["ShowBaseLine"] = value;
			}
		}

		// Token: 0x17002E61 RID: 11873
		// (get) Token: 0x06009293 RID: 37523 RVA: 0x0020F753 File Offset: 0x0020D953
		// (set) Token: 0x06009294 RID: 37524 RVA: 0x0020F774 File Offset: 0x0020D974
		[Category("Layout")]
		[DefaultValue(false)]
		[Description("Whether there will be different styles for sub-items.")]
		public bool EnableSubLevelStyles
		{
			get
			{
				return (bool)(this.ViewState["EnableSubLevelStyles"] ?? false);
			}
			set
			{
				this.ViewState["EnableSubLevelStyles"] = value;
			}
		}

		// Token: 0x17002E62 RID: 11874
		// (get) Token: 0x06009295 RID: 37525 RVA: 0x0020F78C File Offset: 0x0020D98C
		// (set) Token: 0x06009296 RID: 37526 RVA: 0x0020F7AD File Offset: 0x0020D9AD
		[DefaultValue(false)]
		[Description("Whether child tabs are unselected also when their parent is unselected.")]
		[ClientPropertyName("unselectChildren")]
		[Bindable(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool UnSelectChildren
		{
			get
			{
				return (bool)(this.ViewState["UnSelectChildren"] ?? false);
			}
			set
			{
				this.ViewState["UnSelectChildren"] = value;
			}
		}

		// Token: 0x17002E63 RID: 11875
		// (get) Token: 0x06009297 RID: 37527 RVA: 0x0020F7C5 File Offset: 0x0020D9C5
		// (set) Token: 0x06009298 RID: 37528 RVA: 0x0020F7CD File Offset: 0x0020D9CD
		[DefaultValue(true)]
		[ClientPropertyName("causesValidation")]
		[ClientControlProperty]
		public override bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		// Token: 0x17002E64 RID: 11876
		// (get) Token: 0x06009299 RID: 37529 RVA: 0x0020F7D8 File Offset: 0x0020D9D8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		[Description("Gets the object that controls the Wai-Aria settings applied on the control's element.")]
		[DefaultValue(null)]
		public WaiAriaSettings AriaSettings
		{
			get
			{
				WaiAriaSettings result;
				if ((result = this._ariaSettings) == null)
				{
					result = (this._ariaSettings = new WaiAriaSettings());
				}
				return result;
			}
		}

		// Token: 0x17002E65 RID: 11877
		// (get) Token: 0x0600929A RID: 37530 RVA: 0x0020F7FD File Offset: 0x0020D9FD
		// (set) Token: 0x0600929B RID: 37531 RVA: 0x0020F81D File Offset: 0x0020DA1D
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("The name of the javascript function called after a tab has been selected.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("tabSelected")]
		public string OnClientTabSelected
		{
			get
			{
				return (string)(this.ViewState["OnClientTabSelected"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTabSelected"] = value;
			}
		}

		// Token: 0x17002E66 RID: 11878
		// (get) Token: 0x0600929C RID: 37532 RVA: 0x0020F830 File Offset: 0x0020DA30
		// (set) Token: 0x0600929D RID: 37533 RVA: 0x0020F850 File Offset: 0x0020DA50
		[Description("The name of the javascript function called before context menu shows.")]
		[ClientPropertyName("contextMenu")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientContextMenu
		{
			get
			{
				return (string)(this.ViewState["OnClientContextMenu"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientContextMenu"] = value;
			}
		}

		// Token: 0x17002E67 RID: 11879
		// (get) Token: 0x0600929E RID: 37534 RVA: 0x0020F863 File Offset: 0x0020DA63
		// (set) Token: 0x0600929F RID: 37535 RVA: 0x0020F883 File Offset: 0x0020DA83
		[ClientControlEvent]
		[Description("The name of the javascript function called when the user double-clicks a tab.")]
		[Category("Client-side events")]
		[ClientPropertyName("doubleClick")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientDoubleClick
		{
			get
			{
				return (string)(this.ViewState["OnClientDoubleClick"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDoubleClick"] = value;
			}
		}

		// Token: 0x17002E68 RID: 11880
		// (get) Token: 0x060092A0 RID: 37536 RVA: 0x0020F896 File Offset: 0x0020DA96
		// (set) Token: 0x060092A1 RID: 37537 RVA: 0x0020F8B6 File Offset: 0x0020DAB6
		[ClientPropertyName("reordering")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the dragged tab is about to be placed at a new position.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientReordering
		{
			get
			{
				return (string)(this.ViewState["OnClientReordering"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientReordering"] = value;
			}
		}

		// Token: 0x17002E69 RID: 11881
		// (get) Token: 0x060092A2 RID: 37538 RVA: 0x0020F8C9 File Offset: 0x0020DAC9
		// (set) Token: 0x060092A3 RID: 37539 RVA: 0x0020F8E9 File Offset: 0x0020DAE9
		[ClientPropertyName("reordered")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the javascript function called when the dragged tab is placed at a new positon and the tabstrip is reordered.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientReordered
		{
			get
			{
				return (string)(this.ViewState["OnClientReordered"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientReordered"] = value;
			}
		}

		// Token: 0x17002E6A RID: 11882
		// (get) Token: 0x060092A4 RID: 37540 RVA: 0x0020F8FC File Offset: 0x0020DAFC
		// (set) Token: 0x060092A5 RID: 37541 RVA: 0x0020F91C File Offset: 0x0020DB1C
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the user starts draggin a tab which could be cancelled.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("tabDragStart")]
		public string OnClientTabDragStart
		{
			get
			{
				return (string)(this.ViewState["OnClientTabDragStart"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTabDragStart"] = value;
			}
		}

		// Token: 0x17002E6B RID: 11883
		// (get) Token: 0x060092A6 RID: 37542 RVA: 0x0020F92F File Offset: 0x0020DB2F
		// (set) Token: 0x060092A7 RID: 37543 RVA: 0x0020F94F File Offset: 0x0020DB4F
		[ClientPropertyName("tabSelecting")]
		[Description("The name of the javascript function called before a tab is selected.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientTabSelecting
		{
			get
			{
				return (string)(this.ViewState["OnClientTabSelecting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTabSelecting"] = value;
			}
		}

		// Token: 0x17002E6C RID: 11884
		// (get) Token: 0x060092A8 RID: 37544 RVA: 0x0020F962 File Offset: 0x0020DB62
		// (set) Token: 0x060092A9 RID: 37545 RVA: 0x0020F982 File Offset: 0x0020DB82
		[Description("The name of the javascript function called after the mouse has hovered a tab.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("mouseOver")]
		[Category("Client-side events")]
		public string OnClientMouseOver
		{
			get
			{
				return (string)(this.ViewState["OnClientMouseOver"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientMouseOver"] = value;
			}
		}

		// Token: 0x17002E6D RID: 11885
		// (get) Token: 0x060092AA RID: 37546 RVA: 0x0020F995 File Offset: 0x0020DB95
		// (set) Token: 0x060092AB RID: 37547 RVA: 0x0020F9B5 File Offset: 0x0020DBB5
		[ClientControlEvent]
		[DefaultValue("")]
		[ClientPropertyName("mouseOut")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called after the mouse has left a tab.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientMouseOut
		{
			get
			{
				return (string)(this.ViewState["OnClientMouseOut"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientMouseOut"] = value;
			}
		}

		// Token: 0x17002E6E RID: 11886
		// (get) Token: 0x060092AC RID: 37548 RVA: 0x0020F9C8 File Offset: 0x0020DBC8
		// (set) Token: 0x060092AD RID: 37549 RVA: 0x0020F9E8 File Offset: 0x0020DBE8
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when a tab is unselected.")]
		[ClientPropertyName("tabUnSelected")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientTabUnSelected
		{
			get
			{
				return (string)(this.ViewState["OnClientTabUnSelected"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTabUnSelected"] = value;
			}
		}

		// Token: 0x17002E6F RID: 11887
		// (get) Token: 0x060092AE RID: 37550 RVA: 0x0020F9FB File Offset: 0x0020DBFB
		// (set) Token: 0x060092AF RID: 37551 RVA: 0x0020FA1B File Offset: 0x0020DC1B
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the control is fully initialized on the client side.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		public string OnClientLoad
		{
			get
			{
				return (string)(this.ViewState["OnClientLoad"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x14000160 RID: 352
		// (add) Token: 0x060092B0 RID: 37552 RVA: 0x0020FA2E File Offset: 0x0020DC2E
		// (remove) Token: 0x060092B1 RID: 37553 RVA: 0x0020FA41 File Offset: 0x0020DC41
		public event RadTabStripEventHandler TabCreated
		{
			add
			{
				base.Events.AddHandler(RadTabStrip.TabCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTabStrip.TabCreatedEvent, value);
			}
		}

		// Token: 0x060092B2 RID: 37554 RVA: 0x0020FA54 File Offset: 0x0020DC54
		protected virtual void OnTabCreated(RadTabStripEventArgs e)
		{
			this.RaiseEvent(RadTabStrip.TabCreatedEvent, e);
		}

		// Token: 0x14000161 RID: 353
		// (add) Token: 0x060092B3 RID: 37555 RVA: 0x0020FA62 File Offset: 0x0020DC62
		// (remove) Token: 0x060092B4 RID: 37556 RVA: 0x0020FA75 File Offset: 0x0020DC75
		public event RadTabStripEventHandler TemplateNeeded
		{
			add
			{
				base.Events.AddHandler(RadTabStrip.TemplateNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTabStrip.TemplateNeededEvent, value);
			}
		}

		// Token: 0x060092B5 RID: 37557 RVA: 0x0020FA88 File Offset: 0x0020DC88
		protected virtual void OnTemplateNeeded(RadTabStripEventArgs e)
		{
			this.RaiseEvent(RadTabStrip.TemplateNeededEvent, e);
		}

		// Token: 0x14000162 RID: 354
		// (add) Token: 0x060092B6 RID: 37558 RVA: 0x0020FA96 File Offset: 0x0020DC96
		// (remove) Token: 0x060092B7 RID: 37559 RVA: 0x0020FAA9 File Offset: 0x0020DCA9
		public event RadTabStripEventHandler TabDataBound
		{
			add
			{
				base.Events.AddHandler(RadTabStrip.TabDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTabStrip.TabDataBoundEvent, value);
			}
		}

		// Token: 0x060092B8 RID: 37560 RVA: 0x0020FABC File Offset: 0x0020DCBC
		protected virtual void OnTabDataBound(RadTabStripEventArgs e)
		{
			this.RaiseEvent(RadTabStrip.TabDataBoundEvent, e);
		}

		// Token: 0x14000163 RID: 355
		// (add) Token: 0x060092B9 RID: 37561 RVA: 0x0020FACA File Offset: 0x0020DCCA
		// (remove) Token: 0x060092BA RID: 37562 RVA: 0x0020FADD File Offset: 0x0020DCDD
		public event RadTabStripEventHandler TabClick
		{
			add
			{
				base.Events.AddHandler(RadTabStrip.TabClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTabStrip.TabClickEvent, value);
			}
		}

		// Token: 0x060092BB RID: 37563 RVA: 0x0020FAF0 File Offset: 0x0020DCF0
		protected virtual void OnTabClick(RadTabStripEventArgs e)
		{
			this.RaiseEvent(RadTabStrip.TabClickEvent, e);
		}

		// Token: 0x14000164 RID: 356
		// (add) Token: 0x060092BC RID: 37564 RVA: 0x0020FAFE File Offset: 0x0020DCFE
		// (remove) Token: 0x060092BD RID: 37565 RVA: 0x0020FB11 File Offset: 0x0020DD11
		public event RadTabStripReorderedEventHandler Reordered
		{
			add
			{
				base.Events.AddHandler(RadTabStrip.ReorderedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTabStrip.ReorderedEvent, value);
			}
		}

		// Token: 0x060092BE RID: 37566 RVA: 0x0020FB24 File Offset: 0x0020DD24
		protected virtual void OnReordered(RadTabStripReorderedEventArgs e)
		{
			this.RaiseEvent(RadTabStrip.ReorderedEvent, e);
		}

		// Token: 0x060092BF RID: 37567 RVA: 0x0020FB32 File Offset: 0x0020DD32
		public override void LoadContentFile(string xmlFileName)
		{
			base.LoadContentFile(xmlFileName);
		}

		// Token: 0x060092C0 RID: 37568 RVA: 0x0020FB3B File Offset: 0x0020DD3B
		public IList<RadTab> GetAllTabs()
		{
			return base.GetAllChildren<RadTab>();
		}

		// Token: 0x060092C1 RID: 37569 RVA: 0x0020FB43 File Offset: 0x0020DD43
		public RadTab FindTabByUrl(string url)
		{
			return base.FindChildByUrl<RadTab>(url);
		}

		// Token: 0x060092C2 RID: 37570 RVA: 0x0020FB4C File Offset: 0x0020DD4C
		public RadTab FindTabByValue(string value)
		{
			return this.FindChildByValue<RadTab>(value);
		}

		// Token: 0x060092C3 RID: 37571 RVA: 0x0020FB55 File Offset: 0x0020DD55
		public RadTab FindTabByValue(string value, bool ignoreCase)
		{
			return this.FindChildByValue<RadTab>(value, ignoreCase);
		}

		// Token: 0x060092C4 RID: 37572 RVA: 0x0020FB5F File Offset: 0x0020DD5F
		public RadTab FindTabByText(string text)
		{
			return base.FindChildByText<RadTab>(text);
		}

		// Token: 0x060092C5 RID: 37573 RVA: 0x0020FB68 File Offset: 0x0020DD68
		public RadTab FindTabByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadTab>(text, ignoreCase);
		}

		// Token: 0x060092C6 RID: 37574 RVA: 0x0020FB72 File Offset: 0x0020DD72
		public RadTab FindTab(Predicate<RadTab> match)
		{
			return base.FindChild<RadTab>(match);
		}

		// Token: 0x17002E70 RID: 11888
		// (get) Token: 0x060092C7 RID: 37575 RVA: 0x0020FB7B File Offset: 0x0020DD7B
		private bool ShouldReorder
		{
			get
			{
				return this.ReorderTabsOnSelect && this.SelectedTab != null && this.SelectedTab.VisibleIndex > -1;
			}
		}

		// Token: 0x17002E71 RID: 11889
		// (get) Token: 0x060092C8 RID: 37576 RVA: 0x0020FB9D File Offset: 0x0020DD9D
		private bool PostBackOnClick
		{
			get
			{
				return base.Events[RadTabStrip.TabClickEvent] != null;
			}
		}

		// Token: 0x17002E72 RID: 11890
		// (get) Token: 0x060092C9 RID: 37577 RVA: 0x0020FBB5 File Offset: 0x0020DDB5
		private bool PostBackOnReorder
		{
			get
			{
				return this.EnableDragToReorder && base.Events[RadTabStrip.ReorderedEvent] != null;
			}
		}

		// Token: 0x17002E73 RID: 11891
		// (get) Token: 0x060092CA RID: 37578 RVA: 0x0020FBD7 File Offset: 0x0020DDD7
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060092CB RID: 37579 RVA: 0x0020FBDA File Offset: 0x0020DDDA
		protected override NavigationItemBindingCollection CreateDataBindings()
		{
			return new RadTabBindingCollection();
		}

		// Token: 0x060092CC RID: 37580 RVA: 0x0020FBE1 File Offset: 0x0020DDE1
		protected internal override ControlItem CreateItem()
		{
			return new RadTab();
		}

		// Token: 0x060092CD RID: 37581 RVA: 0x0020FBE8 File Offset: 0x0020DDE8
		protected internal override void RaiseItemDataBound(ControlItem item)
		{
			this.OnTabDataBound(new RadTabStripEventArgs((RadTab)item));
		}

		// Token: 0x060092CE RID: 37582 RVA: 0x0020FBFB File Offset: 0x0020DDFB
		protected override void RaiseItemCreated(ControlItem item)
		{
			this.OnTabCreated(new RadTabStripEventArgs((RadTab)item));
		}

		// Token: 0x060092CF RID: 37583 RVA: 0x0020FC0E File Offset: 0x0020DE0E
		protected override void RaiseTemplateNeeded(ControlItem item)
		{
			this.OnTemplateNeeded(new RadTabStripEventArgs((RadTab)item));
		}

		// Token: 0x060092D0 RID: 37584 RVA: 0x0020FC21 File Offset: 0x0020DE21
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadTabCollection(this);
		}

		// Token: 0x17002E74 RID: 11892
		// (get) Token: 0x060092D1 RID: 37585 RVA: 0x0020FC29 File Offset: 0x0020DE29
		IRadTabContainer IRadTabContainer.Owner
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060092D2 RID: 37586 RVA: 0x0020FC2C File Offset: 0x0020DE2C
		protected internal override IRenderer CreateControlRenderer()
		{
			return RendererFactory.CreateTabStripRenderer(this);
		}

		// Token: 0x17002E75 RID: 11893
		// (get) Token: 0x060092D3 RID: 37587 RVA: 0x0020FC34 File Offset: 0x0020DE34
		protected override string CssClassFormatString
		{
			get
			{
				return this.Renderer.CssClassFormatString;
			}
		}

		// Token: 0x060092D4 RID: 37588 RVA: 0x0020FC41 File Offset: 0x0020DE41
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x060092D5 RID: 37589 RVA: 0x0020FC4F File Offset: 0x0020DE4F
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x060092D6 RID: 37590 RVA: 0x0020FC5D File Offset: 0x0020DE5D
		internal void CallBaseAddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060092D7 RID: 37591 RVA: 0x0020FC68 File Offset: 0x0020DE68
		internal IList<IList<IList<RadTab>>> GroupTabsByLevel()
		{
			IList<IList<IList<RadTab>>> list = new List<IList<IList<RadTab>>>();
			IList<IList<RadTab>> list2 = new List<IList<RadTab>>();
			IList<RadTab> list3 = this.Tabs.VisibleChildren<RadTab>();
			if (list3.Count > 0)
			{
				list2.Add(list3);
				list.Add(list2);
			}
			while (list2.Count > 0)
			{
				IList<IList<RadTab>> list4 = RadTabStrip.BuildNextTabLevel(list2);
				if (list4.Count > 0)
				{
					list.Add(list4);
				}
				list2 = list4;
			}
			return list;
		}

		// Token: 0x060092D8 RID: 37592 RVA: 0x0020FCCC File Offset: 0x0020DECC
		internal static IList<IList<RadTab>> BuildNextTabLevel(IEnumerable<IList<RadTab>> currentLevel)
		{
			IList<IList<RadTab>> list = new List<IList<RadTab>>();
			foreach (IList<RadTab> list2 in currentLevel)
			{
				foreach (RadTab radTab in list2)
				{
					IList<RadTab> list3 = radTab.Tabs.VisibleChildren<RadTab>();
					if (list3.Count > 0)
					{
						list.Add(list3);
					}
				}
			}
			return list;
		}

		// Token: 0x060092D9 RID: 37593 RVA: 0x0020FD6C File Offset: 0x0020DF6C
		internal static bool ChildrenShouldBeHidden(RadTab tab)
		{
			RadTab radTab = tab;
			while (radTab != null && radTab.Selected)
			{
				radTab = (radTab.Owner as RadTab);
			}
			return radTab != null;
		}

		// Token: 0x060092DA RID: 37594 RVA: 0x0020FD9C File Offset: 0x0020DF9C
		private void RaiseEvent(object eventKey, RadTabStripEventArgs e)
		{
			RadTabStripEventHandler radTabStripEventHandler = base.Events[eventKey] as RadTabStripEventHandler;
			if (radTabStripEventHandler != null)
			{
				radTabStripEventHandler(this, e);
			}
		}

		// Token: 0x060092DB RID: 37595 RVA: 0x0020FDC8 File Offset: 0x0020DFC8
		private void RaiseEvent(object eventKey, RadTabStripReorderedEventArgs e)
		{
			RadTabStripReorderedEventHandler radTabStripReorderedEventHandler = base.Events[eventKey] as RadTabStripReorderedEventHandler;
			if (radTabStripReorderedEventHandler != null)
			{
				radTabStripReorderedEventHandler(this, e);
			}
		}

		// Token: 0x060092DC RID: 37596 RVA: 0x0020FDF4 File Offset: 0x0020DFF4
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			IList<JavaScriptConverter> converters = new JavaScriptConverter[]
			{
				new TabJavaScriptConverter(),
				new AttributeCollectionConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(converters);
			IList<RadTab> list = this.Tabs.VisibleChildren<RadTab>();
			if (list.Count > 0)
			{
				descriptor.AddScriptProperty("tabData", javaScriptSerializer.Serialize(list));
			}
			if (base.Attributes.Count > 0)
			{
				descriptor.AddScriptProperty("attributes", javaScriptSerializer.Serialize(base.Attributes));
			}
			if (this.AutoPostBack || this.PostBackOnClick || this.PostBackOnReorder)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
			if (this.PostBackOnClick)
			{
				descriptor.AddProperty("_postBackOnClick", true);
			}
			if (this.PostBackOnReorder)
			{
				descriptor.AddProperty("_postBackOnReorder", true);
			}
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			if (this.MultiPage != null)
			{
				descriptor.AddProperty("multiPageID", this.MultiPage.ClientID);
			}
			if (this.ShouldReorder)
			{
				descriptor.AddProperty("_shouldReorder", true);
			}
			if (this.ScrollChildren && this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				descriptor.AddProperty("_scrollPosition", this.ScrollPosition);
			}
			base.DescribeRenderingMode(descriptor);
			this.AriaSettings.Describe(descriptor);
			this.SerializeSelectedIndexes(descriptor, javaScriptSerializer);
		}

		// Token: 0x060092DD RID: 37597 RVA: 0x0020FF60 File Offset: 0x0020E160
		private void SerializeSelectedIndexes(IScriptDescriptor descriptor, JavaScriptSerializer serializer)
		{
			if (this.SelectedTab != null)
			{
				descriptor.AddProperty("_selectedIndex", this.Tabs.VisibleItems.IndexOf(this.SelectedTab));
			}
			IList<string> list = new List<string>();
			foreach (RadTab radTab in this.GetAllTabs())
			{
				if (radTab.Selected && radTab.Visible)
				{
					list.Add(radTab.HierarchicalIndex);
				}
			}
			if (list.Count > 0)
			{
				descriptor.AddScriptProperty("selectedIndexes", serializer.Serialize(list));
			}
		}

		// Token: 0x060092DE RID: 37598 RVA: 0x00210014 File Offset: 0x0020E214
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this._lastSelectedTab = this.InnermostSelectedTab;
			if (string.IsNullOrEmpty(postCollection[base.ClientStateFieldID]))
			{
				return false;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				this.LoadClientState(javaScriptSerializer.Deserialize<TabStripClientState>(postCollection[base.ClientStateFieldID]));
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			return false;
		}

		// Token: 0x060092DF RID: 37599 RVA: 0x00210088 File Offset: 0x0020E288
		internal void LoadClientState(TabStripClientState state)
		{
			if (state.LogEntries != null)
			{
				this.LoadLogEntries(state);
			}
			if (state.SelectedIndexes != null)
			{
				this.LoadSelectedState(state);
			}
			if (state.ScrollState != null)
			{
				this.LoadScrollState(state);
			}
		}

		// Token: 0x060092E0 RID: 37600 RVA: 0x002100B8 File Offset: 0x0020E2B8
		private void LoadScrollState(TabStripClientState state)
		{
			foreach (KeyValuePair<string, int> keyValuePair in state.ScrollState)
			{
				if (keyValuePair.Key == "-1")
				{
					this.ScrollPosition = keyValuePair.Value;
				}
				else
				{
					RadTab radTab = (RadTab)this.FindItemByHierarchicalIndex(keyValuePair.Key);
					if (radTab != null)
					{
						radTab.ScrollPosition = keyValuePair.Value;
					}
				}
			}
		}

		// Token: 0x060092E1 RID: 37601 RVA: 0x00210144 File Offset: 0x0020E344
		private void LoadLogEntries(TabStripClientState state)
		{
			ClientStateLogPlayer<RadTab> clientStateLogPlayer = new ClientStateLogPlayer<RadTab>(this);
			this._clientChanges = clientStateLogPlayer.Play(state.LogEntries);
		}

		// Token: 0x060092E2 RID: 37602 RVA: 0x0021016C File Offset: 0x0020E36C
		internal void LoadSelectedState(TabStripClientState clientState)
		{
			if (!this.Visible)
			{
				return;
			}
			foreach (RadTab radTab in this.GetAllTabs())
			{
				radTab.Selected = false;
			}
			foreach (string hierarchicalIndex in clientState.SelectedIndexes)
			{
				RadTab radTab2 = (RadTab)this.FindItemByHierarchicalIndex(hierarchicalIndex);
				if (radTab2 != null)
				{
					radTab2.Selected = true;
				}
			}
		}

		// Token: 0x060092E3 RID: 37603 RVA: 0x002101FC File Offset: 0x0020E3FC
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x060092E4 RID: 37604 RVA: 0x00210208 File Offset: 0x0020E408
		protected internal virtual void RaisePostBackEvent(string eventArgument)
		{
			TabStripPostBackCommand tabStripPostBackCommand = null;
			try
			{
				tabStripPostBackCommand = new JavaScriptSerializer().Deserialize<TabStripPostBackCommand>(eventArgument);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (tabStripPostBackCommand == null)
			{
				return;
			}
			RadTab radTab = (RadTab)this.FindItemByHierarchicalIndex(tabStripPostBackCommand.Index);
			if (radTab != null)
			{
				switch (tabStripPostBackCommand.Type)
				{
				case TabStripCommand.TabClick:
					this.PerformValidation(radTab);
					this.OnTabClick(new RadTabStripEventArgs(radTab));
					return;
				case TabStripCommand.Reorder:
					this.OnReordered(new RadTabStripReorderedEventArgs(radTab, tabStripPostBackCommand.Offset));
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060092E5 RID: 37605 RVA: 0x0021029C File Offset: 0x0020E49C
		private void PerformValidation(RadTab tab)
		{
			if (!this.CausesValidation)
			{
				return;
			}
			if (this.MultiPage == null)
			{
				this.Page.Validate(this.ValidationGroup);
				if (!this.Page.IsValid && this._lastSelectedTab != null)
				{
					this._lastSelectedTab.SelectParents();
				}
				return;
			}
			int num = this.MultiPage.SelectedIndex;
			int selectedIndex = this.MultiPage.SelectedIndex;
			if (this.UnSelectChildren && tab.PageView != null)
			{
				num = tab.PageView.Index;
			}
			else
			{
				num = this.GetSelectedChild(tab);
			}
			if (this._lastSelectedTab == null)
			{
				this.MultiPage.SelectedIndex = -1;
			}
			else if (this._lastSelectedTab.PageView != null)
			{
				this.MultiPage.SelectedIndex = this._lastSelectedTab.PageView.Index;
				selectedIndex = this._lastSelectedTab.PageView.Index;
			}
			this.Page.Validate(this.ValidationGroup);
			if (!this.Page.IsValid)
			{
				if (this._lastSelectedTab != null)
				{
					this._lastSelectedTab.SelectParents();
				}
				this.MultiPage.SelectedIndex = selectedIndex;
				return;
			}
			if (num > -1)
			{
				this.MultiPage.SelectedIndex = num;
			}
		}

		// Token: 0x060092E6 RID: 37606 RVA: 0x002103C8 File Offset: 0x0020E5C8
		private int GetSelectedChild(RadTab tab)
		{
			if (tab.PageView != null && tab.SelectedTab == null)
			{
				return tab.PageView.Index;
			}
			if (tab.SelectedTab != null)
			{
				return this.GetSelectedChild(tab.SelectedTab);
			}
			if (tab.PageView == null)
			{
				return -1;
			}
			return tab.PageView.Index;
		}

		// Token: 0x060092E7 RID: 37607 RVA: 0x0021041B File Offset: 0x0020E61B
		protected override void OnPreRender(EventArgs e)
		{
			if (this.MultiPage != null)
			{
				this.InitializeImplicitPageViewIDs();
			}
			base.OnPreRender(e);
		}

		// Token: 0x060092E8 RID: 37608 RVA: 0x00210434 File Offset: 0x0020E634
		private void InitializeImplicitPageViewIDs()
		{
			foreach (RadTab radTab in this.GetAllTabs())
			{
				int num = this.GetAllTabs().IndexOf(radTab);
				if (this.MultiPage.PageViews.Count > num && string.IsNullOrEmpty(radTab.ImplicitPageViewID))
				{
					radTab.ImplicitPageViewID = this.MultiPage.PageViews[num].ID;
				}
			}
		}

		// Token: 0x17002E76 RID: 11894
		// (get) Token: 0x060092E9 RID: 37609 RVA: 0x002104C4 File Offset: 0x0020E6C4
		// (set) Token: 0x060092EA RID: 37610 RVA: 0x002104CC File Offset: 0x0020E6CC
		[SimplePersistenceSetting]
		internal List<string> SelectedIndices
		{
			get
			{
				return this.GetSelectedItemsIndices();
			}
			set
			{
				foreach (string hierarchicalIndex in value)
				{
					RadTab radTab = this.FindItemByHierarchicalIndex(hierarchicalIndex) as RadTab;
					if (radTab != null)
					{
						radTab.Selected = true;
					}
				}
			}
		}

		// Token: 0x060092EB RID: 37611 RVA: 0x0021052C File Offset: 0x0020E72C
		private List<string> GetSelectedItemsIndices()
		{
			List<string> list = new List<string>();
			IList<RadTab> allTabs = this.GetAllTabs();
			foreach (RadTab radTab in allTabs)
			{
				if (radTab.Selected)
				{
					list.Add(radTab.GetHierarchicalIndex());
				}
			}
			return list;
		}

		// Token: 0x060092EC RID: 37612 RVA: 0x00210590 File Offset: 0x0020E790
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<TabStripAlign>(descriptor, "_align", this.Align, TabStripAlign.Left);
			base.DescribeProperty<bool>(descriptor, "_autoPostBack", this.AutoPostBack, false);
			base.DescribeProperty<bool>(descriptor, "causesValidation", this.CausesValidation, true);
			base.DescribeProperty<bool>(descriptor, "clickSelectedTab", this.ClickSelectedTab, false);
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<bool>(descriptor, "_enableDragToReorder", this.EnableDragToReorder, false);
			base.DescribeProperty<TabStripOrientation>(descriptor, "_orientation", this.Orientation, TabStripOrientation.HorizontalTop);
			base.DescribeProperty<bool>(descriptor, "_perTabScrolling", this.PerTabScrolling, false);
			base.DescribeProperty<bool>(descriptor, "_reorderTabsOnSelect", this.ReorderTabsOnSelect, false);
			base.DescribeProperty<TabStripScrollButtonsPosition>(descriptor, "_scrollButtonsPosition", this.ScrollButtonsPosition, TabStripScrollButtonsPosition.Right);
			base.DescribeProperty<bool>(descriptor, "_scrollChildren", this.ScrollChildren, false);
			base.DescribeProperty<bool>(descriptor, "unselectChildren", this.UnSelectChildren, false);
			base.DescribeProperty<string>(descriptor, "validationGroup", this.ValidationGroup, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060092ED RID: 37613 RVA: 0x002106A0 File Offset: 0x0020E8A0
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "contextMenu", this.OnClientContextMenu);
			RadDataBoundControl.DescribeEvent(descriptor, "doubleClick", this.OnClientDoubleClick);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOut", this.OnClientMouseOut);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOver", this.OnClientMouseOver);
			RadDataBoundControl.DescribeEvent(descriptor, "reordered", this.OnClientReordered);
			RadDataBoundControl.DescribeEvent(descriptor, "reordering", this.OnClientReordering);
			RadDataBoundControl.DescribeEvent(descriptor, "tabDragStart", this.OnClientTabDragStart);
			RadDataBoundControl.DescribeEvent(descriptor, "tabSelected", this.OnClientTabSelected);
			RadDataBoundControl.DescribeEvent(descriptor, "tabSelecting", this.OnClientTabSelecting);
			RadDataBoundControl.DescribeEvent(descriptor, "tabUnSelected", this.OnClientTabUnSelected);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x060092EE RID: 37614 RVA: 0x0021076F File Offset: 0x0020E96F
		// Note: this type is marked as 'beforefieldinit'.
		static RadTabStrip()
		{
			RadTabStrip.TabCreatedEvent = new object();
			RadTabStrip.TemplateNeededEvent = new object();
			RadTabStrip.TabDataBoundEvent = new object();
			RadTabStrip.TabClickEvent = new object();
			RadTabStrip.ReorderedEvent = new object();
		}

		// Token: 0x04002A3A RID: 10810
		private IList<ClientOperation<RadTab>> _clientChanges = new List<ClientOperation<RadTab>>();

		// Token: 0x04002A3B RID: 10811
		private WaiAriaSettings _ariaSettings;

		// Token: 0x04002A41 RID: 10817
		private RadTab _lastSelectedTab;
	}
}
