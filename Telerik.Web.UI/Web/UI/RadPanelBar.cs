using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.Design;
using System.Xml.Serialization;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x0200064B RID: 1611
	[LightweightRendering]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadPanelBar), "Telerik.Web.UI.PanelBar.png")]
	[Designer("Telerik.Web.Design.RadPanelBarDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadPanelBar Runat=\"server\"></{0}:RadPanelBar>")]
	[RequiredScript(typeof(jQueryPlugins))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("PanelBar", typeof(RadPanelBar))]
	[EmbeddedSkin("PanelBar", "Default", typeof(RadPanelBar))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadPanelBar))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(Core))]
	[ClientScriptResource("Telerik.Web.UI.RadPanelBar", "Telerik.Web.UI.PanelBar.RadPanelBarScripts.js")]
	[RequiredScript(typeof(jSlide))]
	[RequiredScript(typeof(TouchScrollExtender))]
	[RequiredScript(typeof(MaterialRipple))]
	[DefaultProperty("Items")]
	[DefaultEvent("ItemClick")]
	[XmlRoot("PanelBar")]
	public class RadPanelBar : HierarchicalControlItemContainer, IRadPanelItemContainer, IPostBackEventHandler
	{
		// Token: 0x06003AD9 RID: 15065 RVA: 0x000BF8FC File Offset: 0x000BDAFC
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "allowCollapseAllItems", this.AllowCollapseAllItems, false);
			base.DescribeProperty<int>(descriptor, "collapseDelay", this.CollapseDelay, 0);
			base.DescribeProperty<string>(descriptor, "cookieName", this.CookieName, "");
			base.DescribeProperty<int>(descriptor, "expandDelay", this.ExpandDelay, 0);
			base.DescribeProperty<PanelBarExpandMode>(descriptor, "expandMode", this.ExpandMode, PanelBarExpandMode.MultipleExpandedItems);
			base.DescribeProperty<bool>(descriptor, "persistStateInCookie", this.PersistStateInCookie, false);
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x000BF99C File Offset: 0x000BDB9C
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "contextMenu", this.OnClientContextMenu);
			RadDataBoundControl.DescribeEvent(descriptor, "itemAnimationEnd", this.OnClientItemAnimationEnd);
			RadDataBoundControl.DescribeEvent(descriptor, "itemBlur", this.OnClientItemBlur);
			RadDataBoundControl.DescribeEvent(descriptor, "itemClicked", this.OnClientItemClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "itemClicking", this.OnClientItemClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "itemCollapse", this.OnClientItemCollapse);
			RadDataBoundControl.DescribeEvent(descriptor, "itemExpand", this.OnClientItemExpand);
			RadDataBoundControl.DescribeEvent(descriptor, "itemFocus", this.OnClientItemFocus);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOut", this.OnClientMouseOut);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOver", this.OnClientMouseOver);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x000BFA6B File Offset: 0x000BDC6B
		public RadPanelBar()
		{
			this._expandAnimation = new PanelBarAnimationSettings("Expand", this.ViewState);
			this._collapseAnimation = new PanelBarAnimationSettings("Collapse", this.ViewState);
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x000BFAAA File Offset: 0x000BDCAA
		// (set) Token: 0x06003ADD RID: 15069 RVA: 0x000BFACB File Offset: 0x000BDCCB
		[ClientPropertyName("enableAriaSupport")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[Description("When set to true enables support for WAI-ARIA.")]
		[Category("Behavior")]
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

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x000BFAE4 File Offset: 0x000BDCE4
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the object that controls the Wai-Aria settings applied on the control's element.")]
		[MergableProperty(false)]
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

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06003ADF RID: 15071 RVA: 0x000BFB09 File Offset: 0x000BDD09
		[Browsable(false)]
		public IList<ClientOperation<RadPanelItem>> ClientChanges
		{
			get
			{
				return this._clientChanges;
			}
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06003AE0 RID: 15072 RVA: 0x000BFB11 File Offset: 0x000BDD11
		[Editor("Telerik.Web.Design.ControlItemCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[DefaultValue(null)]
		public RadPanelItemCollection Items
		{
			get
			{
				return (RadPanelItemCollection)base.Children;
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06003AE1 RID: 15073 RVA: 0x000BFB20 File Offset: 0x000BDD20
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadPanelItem SelectedItem
		{
			get
			{
				foreach (RadPanelItem radPanelItem in this.GetAllItems())
				{
					if (radPanelItem.Selected)
					{
						return radPanelItem;
					}
				}
				return null;
			}
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x000BFB78 File Offset: 0x000BDD78
		// (set) Token: 0x06003AE3 RID: 15075 RVA: 0x000BFB80 File Offset: 0x000BDD80
		[Bindable(false)]
		[Browsable(false)]
		[TemplateContainer(typeof(RadPanelItem))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x06003AE4 RID: 15076 RVA: 0x000BFB89 File Offset: 0x000BDD89
		public override void LoadContentFile(string xmlFileName)
		{
			base.LoadContentFile(xmlFileName);
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x000BFB92 File Offset: 0x000BDD92
		// (set) Token: 0x06003AE6 RID: 15078 RVA: 0x000BFBBD File Offset: 0x000BDDBD
		[DefaultValue(PanelBarExpandMode.MultipleExpandedItems)]
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("The behavior of RadPanelbar when an item is expanded.")]
		public PanelBarExpandMode ExpandMode
		{
			get
			{
				if (this.ViewState["ExpandMode"] == null)
				{
					return PanelBarExpandMode.MultipleExpandedItems;
				}
				return (PanelBarExpandMode)this.ViewState["ExpandMode"];
			}
			set
			{
				this.ViewState["ExpandMode"] = value;
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06003AE7 RID: 15079 RVA: 0x000BFBD5 File Offset: 0x000BDDD5
		// (set) Token: 0x06003AE8 RID: 15080 RVA: 0x000BFC00 File Offset: 0x000BDE00
		[Category("Behavior")]
		[Description("The behavior of RadPanelbar when an item is expanded.")]
		[DefaultValue(false)]
		[ClientControlProperty]
		public bool AllowCollapseAllItems
		{
			get
			{
				return this.ViewState["AllowCollapseAllItems"] != null && (bool)this.ViewState["AllowCollapseAllItems"];
			}
			set
			{
				this.ViewState["AllowCollapseAllItems"] = value;
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06003AE9 RID: 15081 RVA: 0x000BFC18 File Offset: 0x000BDE18
		// (set) Token: 0x06003AEA RID: 15082 RVA: 0x000BFC20 File Offset: 0x000BDE20
		[UrlProperty("*.aspx")]
		[Description("The URL to post to when an item is clicked.")]
		[DefaultValue("")]
		[Themeable(false)]
		[Category("Behavior")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
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

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x000BFC29 File Offset: 0x000BDE29
		[Category("Data")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Description("The data bindings for panel items in the panelbar")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RadPanelItemBindingCollection DataBindings
		{
			get
			{
				return (RadPanelItemBindingCollection)base.NavigationItemBindings;
			}
		}

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06003AEC RID: 15084 RVA: 0x000BFC36 File Offset: 0x000BDE36
		// (set) Token: 0x06003AED RID: 15085 RVA: 0x000BFC3E File Offset: 0x000BDE3E
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

		// Token: 0x06003AEE RID: 15086 RVA: 0x000BFC47 File Offset: 0x000BDE47
		public RadPanelItem FindItemByText(string text)
		{
			return base.FindChildByText<RadPanelItem>(text);
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x000BFC50 File Offset: 0x000BDE50
		public RadPanelItem FindItemByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadPanelItem>(text, ignoreCase);
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x000BFC5A File Offset: 0x000BDE5A
		public RadPanelItem FindItemByValue(string value)
		{
			return this.FindChildByValue<RadPanelItem>(value);
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x000BFC63 File Offset: 0x000BDE63
		public RadPanelItem FindItemByValue(string value, bool ignoreCase)
		{
			return this.FindChildByValue<RadPanelItem>(value, ignoreCase);
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x000BFC6D File Offset: 0x000BDE6D
		public RadPanelItem FindItemByUrl(string url)
		{
			return base.FindChildByUrl<RadPanelItem>(url);
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x000BFC76 File Offset: 0x000BDE76
		public RadPanelItem FindItem(Predicate<RadPanelItem> match)
		{
			return base.FindChild<RadPanelItem>(match);
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x000BFC7F File Offset: 0x000BDE7F
		public IList<RadPanelItem> GetAllItems()
		{
			return base.GetAllChildren<RadPanelItem>();
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x000BFC88 File Offset: 0x000BDE88
		public void ClearSelectedItems()
		{
			foreach (RadPanelItem radPanelItem in this.GetAllItems())
			{
				radPanelItem.Selected = false;
			}
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x000BFCD8 File Offset: 0x000BDED8
		public void CollapseAllItems()
		{
			foreach (RadPanelItem radPanelItem in this.GetAllItems())
			{
				radPanelItem.Expanded = false;
			}
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x000BFD28 File Offset: 0x000BDF28
		// (set) Token: 0x06003AF8 RID: 15096 RVA: 0x000BFD53 File Offset: 0x000BDF53
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		public bool PersistStateInCookie
		{
			get
			{
				return this.ViewState["PersistStateInCookie"] != null && (bool)this.ViewState["PersistStateInCookie"];
			}
			set
			{
				this.ViewState["PersistStateInCookie"] = value;
			}
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x000BFD6B File Offset: 0x000BDF6B
		// (set) Token: 0x06003AFA RID: 15098 RVA: 0x000BFD9A File Offset: 0x000BDF9A
		[Description("Use to override the default name of the panelbar state cookie.")]
		[DefaultValue("")]
		[ClientControlProperty]
		public string CookieName
		{
			get
			{
				if (this.ViewState["CookieName"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["CookieName"];
			}
			set
			{
				this.ViewState["CookieName"] = value;
			}
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x000BFDAD File Offset: 0x000BDFAD
		internal string StateCookieName
		{
			get
			{
				if (string.IsNullOrEmpty(this.CookieName))
				{
					return this.ClientID;
				}
				return this.CookieName;
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x000BFDC9 File Offset: 0x000BDFC9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[Category("Behavior")]
		[Description("The animation played when item is opened")]
		[NotifyParentProperty(true)]
		public AnimationSettings ExpandAnimation
		{
			get
			{
				return this._expandAnimation;
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x000BFDD1 File Offset: 0x000BDFD1
		// (set) Token: 0x06003AFE RID: 15102 RVA: 0x000BFDF2 File Offset: 0x000BDFF2
		[Category("Behavior")]
		[DefaultValue(0)]
		[ClientPropertyName("expandDelay")]
		[Description("Delay in milliseconds between the mouse entering a RadPanelBarItem and its child items starting to expand")]
		[ClientControlProperty]
		public int ExpandDelay
		{
			get
			{
				return (int)(this.ViewState["ExpandDelay"] ?? 0);
			}
			set
			{
				this.ViewState["ExpandDelay"] = value;
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06003AFF RID: 15103 RVA: 0x000BFE0A File Offset: 0x000BE00A
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		[Description("The animation played when item is closed")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public AnimationSettings CollapseAnimation
		{
			get
			{
				return this._collapseAnimation;
			}
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000BFE12 File Offset: 0x000BE012
		// (set) Token: 0x06003B01 RID: 15105 RVA: 0x000BFE33 File Offset: 0x000BE033
		[ClientPropertyName("collapseDelay")]
		[ClientControlProperty]
		[Description("Delay in milliseconds between the clicking the panel and panel starting to collapse")]
		[DefaultValue(0)]
		[Category("Behavior")]
		public int CollapseDelay
		{
			get
			{
				return (int)(this.ViewState["CollapseDelay"] ?? 0);
			}
			set
			{
				this.ViewState["CollapseDelay"] = value;
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x000BFE4B File Offset: 0x000BE04B
		// (set) Token: 0x06003B03 RID: 15107 RVA: 0x000BFE6C File Offset: 0x000BE06C
		[DefaultValue(false)]
		[Description("Whether to Html encode the text of items.")]
		[ClientControlProperty]
		[ClientPropertyName("_enableItemTextHtmlEncoding")]
		[Category("Behavior")]
		public bool EnableItemTextHtmlEncoding
		{
			get
			{
				return (bool)(this.ViewState["EnableItemTextHtmlEncoding"] ?? false);
			}
			set
			{
				this.ViewState["EnableItemTextHtmlEncoding"] = value;
			}
		}

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06003B04 RID: 15108 RVA: 0x000BFE84 File Offset: 0x000BE084
		// (remove) Token: 0x06003B05 RID: 15109 RVA: 0x000BFE97 File Offset: 0x000BE097
		[Category("Behavior")]
		[Description("Fired after a RadPanelBarItem is created.")]
		public event RadPanelBarEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadPanelBar.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPanelBar.ItemCreatedEvent, value);
			}
		}

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06003B06 RID: 15110 RVA: 0x000BFEAA File Offset: 0x000BE0AA
		// (remove) Token: 0x06003B07 RID: 15111 RVA: 0x000BFEBD File Offset: 0x000BE0BD
		public event RadPanelBarEventHandler TemplateNeeded
		{
			add
			{
				base.Events.AddHandler(RadPanelBar.TemplateNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPanelBar.TemplateNeededEvent, value);
			}
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000BFED0 File Offset: 0x000BE0D0
		protected virtual void OnTemplateNeeded(RadPanelBarEventArgs e)
		{
			this.RaiseEvent(RadPanelBar.TemplateNeededEvent, e);
		}

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x06003B09 RID: 15113 RVA: 0x000BFEDE File Offset: 0x000BE0DE
		// (remove) Token: 0x06003B0A RID: 15114 RVA: 0x000BFEF1 File Offset: 0x000BE0F1
		[Description("Fired after a panel item is clicked.")]
		public virtual event RadPanelBarEventHandler ItemClick
		{
			add
			{
				base.Events.AddHandler(RadPanelBar.ItemClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPanelBar.ItemClickEvent, value);
			}
		}

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x06003B0B RID: 15115 RVA: 0x000BFF04 File Offset: 0x000BE104
		// (remove) Token: 0x06003B0C RID: 15116 RVA: 0x000BFF17 File Offset: 0x000BE117
		[Description("Fired after a RadPanelBarItem is databound.")]
		[Category("Behavior")]
		public event RadPanelBarEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadPanelBar.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPanelBar.ItemDataBoundEvent, value);
			}
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x000BFF2A File Offset: 0x000BE12A
		// (set) Token: 0x06003B0E RID: 15118 RVA: 0x000BFF59 File Offset: 0x000BE159
		[DefaultValue("")]
		[ClientPropertyName("contextMenu")]
		[ClientControlEvent]
		[Description("The name of the javascript function called before context panel shows.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public string OnClientContextMenu
		{
			get
			{
				if (this.ViewState["OnClientContextMenu"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientContextMenu"];
			}
			set
			{
				this.ViewState["OnClientContextMenu"] = value;
			}
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06003B0F RID: 15119 RVA: 0x000BFF6C File Offset: 0x000BE16C
		// (set) Token: 0x06003B10 RID: 15120 RVA: 0x000BFF9B File Offset: 0x000BE19B
		[ClientPropertyName("itemClicking")]
		[Description("The name of the javascript function called before item clicking.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientItemClicking
		{
			get
			{
				if (this.ViewState["OnClientItemClicking"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientItemClicking"];
			}
			set
			{
				this.ViewState["OnClientItemClicking"] = value;
			}
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x000BFFAE File Offset: 0x000BE1AE
		// (set) Token: 0x06003B12 RID: 15122 RVA: 0x000BFFDD File Offset: 0x000BE1DD
		[ClientControlEvent]
		[ClientPropertyName("itemClicked")]
		[Description("The name of the javascript function called after a panel item is clicked.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public string OnClientItemClicked
		{
			get
			{
				if (this.ViewState["OnClientItemClicked"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientItemClicked"];
			}
			set
			{
				this.ViewState["OnClientItemClicked"] = value;
			}
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06003B13 RID: 15123 RVA: 0x000BFFF0 File Offset: 0x000BE1F0
		// (set) Token: 0x06003B14 RID: 15124 RVA: 0x000C001F File Offset: 0x000BE21F
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("itemFocus")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called after a panel item is checked.")]
		public string OnClientItemFocus
		{
			get
			{
				if (this.ViewState["OnClientItemFocus"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientItemFocus"];
			}
			set
			{
				this.ViewState["OnClientItemFocus"] = value;
			}
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x000C0032 File Offset: 0x000BE232
		// (set) Token: 0x06003B16 RID: 15126 RVA: 0x000C0061 File Offset: 0x000BE261
		[ClientPropertyName("itemBlur")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called after a panel item loses focus.")]
		[DefaultValue("")]
		public string OnClientItemBlur
		{
			get
			{
				if (this.ViewState["OnClientItemBlur"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientItemBlur"];
			}
			set
			{
				this.ViewState["OnClientItemBlur"] = value;
			}
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06003B17 RID: 15127 RVA: 0x000C0074 File Offset: 0x000BE274
		// (set) Token: 0x06003B18 RID: 15128 RVA: 0x000C00A3 File Offset: 0x000BE2A3
		[ClientControlEvent]
		[ClientPropertyName("itemExpand")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("The name of the javascript function called after a panel item is expanded.")]
		public string OnClientItemExpand
		{
			get
			{
				if (this.ViewState["OnClientItemExpand"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientItemExpand"];
			}
			set
			{
				this.ViewState["OnClientItemExpand"] = value;
			}
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x000C00B6 File Offset: 0x000BE2B6
		// (set) Token: 0x06003B1A RID: 15130 RVA: 0x000C00E5 File Offset: 0x000BE2E5
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("itemCollapse")]
		[Description("The name of the javascript function called after a panel item is collapsed.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientItemCollapse
		{
			get
			{
				if (this.ViewState["OnClientItemCollapse"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientItemCollapse"];
			}
			set
			{
				this.ViewState["OnClientItemCollapse"] = value;
			}
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x000C00F8 File Offset: 0x000BE2F8
		// (set) Token: 0x06003B1C RID: 15132 RVA: 0x000C0118 File Offset: 0x000BE318
		[ClientPropertyName("itemAnimationEnd")]
		[DefaultValue("")]
		[Description("The name of the JavaScript function called when an item's expand/collapse animation finishes")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[ClientControlEvent]
		public string OnClientItemAnimationEnd
		{
			get
			{
				return (string)(this.ViewState["OnClientItemAnimationEnd"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemAnimationEnd"] = value;
			}
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x000C012B File Offset: 0x000BE32B
		// (set) Token: 0x06003B1E RID: 15134 RVA: 0x000C015A File Offset: 0x000BE35A
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("The name of the javascript function called after a panelbar is loaded.")]
		[ClientPropertyName("load")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientLoad
		{
			get
			{
				if (this.ViewState["OnClientLoad"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientLoad"];
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000C016D File Offset: 0x000BE36D
		// (set) Token: 0x06003B20 RID: 15136 RVA: 0x000C019C File Offset: 0x000BE39C
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("mouseOver")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the mouse hovers a panel item.")]
		public string OnClientMouseOver
		{
			get
			{
				if (this.ViewState["OnClientMouseOver"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientMouseOver"];
			}
			set
			{
				this.ViewState["OnClientMouseOver"] = value;
			}
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x000C01AF File Offset: 0x000BE3AF
		// (set) Token: 0x06003B22 RID: 15138 RVA: 0x000C01DE File Offset: 0x000BE3DE
		[Category("Client-side events")]
		[ClientPropertyName("mouseOut")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[Description("The name of the javascript function called after the mouse leaves a panel item.")]
		public string OnClientMouseOut
		{
			get
			{
				if (this.ViewState["OnClientMouseOut"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientMouseOut"];
			}
			set
			{
				this.ViewState["OnClientMouseOut"] = value;
			}
		}

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x000C01F1 File Offset: 0x000BE3F1
		// (set) Token: 0x06003B24 RID: 15140 RVA: 0x000C0208 File Offset: 0x000BE408
		internal bool RenderEditableRegions
		{
			get
			{
				return string.IsNullOrEmpty(this.DataSourceID) && this._renderEditableRegions;
			}
			set
			{
				this._renderEditableRegions = value;
			}
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06003B25 RID: 15141 RVA: 0x000C0214 File Offset: 0x000BE414
		internal bool AutoPostBack
		{
			get
			{
				return (RadPanelBarEventHandler)base.Events[RadPanelBar.ItemClickEvent] != null || !string.IsNullOrEmpty(this.PostBackUrl);
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06003B26 RID: 15142 RVA: 0x000C024A File Offset: 0x000BE44A
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x000C024D File Offset: 0x000BE44D
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.TabIndex == 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, "0");
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x000C026C File Offset: 0x000BE46C
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadPanelBar RadPanelBar_{0}";
				if (base.Attributes["dir"] == "rtl")
				{
					text = "RadPanelBar RadPanelBar_{0} RadPanelBar_rtl RadPanelBar_{0}_rtl";
				}
				if (!base.IsEnabled)
				{
					text += " RadPanelBar_{0}_disabled rpDisabled";
				}
				return text;
			}
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x000C02B8 File Offset: 0x000BE4B8
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			List<JavaScriptConverter> list = new List<JavaScriptConverter>();
			list.Add(new RadPanelItemConverter());
			list.Add(new AttributeCollectionConverter());
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(list);
			base.DescribeRenderingMode(descriptor);
			if (base.Attributes.Count > 0)
			{
				descriptor.AddScriptProperty("attributes", javaScriptSerializer.Serialize(base.Attributes));
			}
			if (this.AutoPostBack)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
			if (this.EnableItemTextHtmlEncoding)
			{
				descriptor.AddProperty("_enableItemTextHtmlEncoding", this.EnableItemTextHtmlEncoding);
			}
			descriptor.AddScriptProperty("itemData", javaScriptSerializer.Serialize(this.Items.VisibleItems));
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			this.ExpandAnimation.Describe("expandAnimation", javaScriptSerializer, descriptor);
			this.CollapseAnimation.Describe("collapseAnimation", javaScriptSerializer, descriptor);
			this.AriaSettings.Describe(descriptor);
			base.DescribeComponent(descriptor);
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x000C03B5 File Offset: 0x000BE5B5
		protected override NavigationItemBindingCollection CreateDataBindings()
		{
			return new RadPanelItemBindingCollection();
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000C03BC File Offset: 0x000BE5BC
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadPanelItemCollection(this);
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x000C03C4 File Offset: 0x000BE5C4
		protected internal override ControlItem CreateItem()
		{
			return new RadPanelItem();
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x000C03CB File Offset: 0x000BE5CB
		protected virtual void OnItemClick(RadPanelBarEventArgs e)
		{
			this.RaiseEvent(RadPanelBar.ItemClickEvent, e);
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000C03D9 File Offset: 0x000BE5D9
		protected virtual void OnItemDataBound(RadPanelBarEventArgs e)
		{
			this.RaiseEvent(RadPanelBar.ItemDataBoundEvent, e);
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000C03E7 File Offset: 0x000BE5E7
		protected virtual void OnItemCreated(RadPanelBarEventArgs e)
		{
			this.RaiseEvent(RadPanelBar.ItemCreatedEvent, e);
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000C03F5 File Offset: 0x000BE5F5
		protected override void RaiseTemplateNeeded(ControlItem item)
		{
			this.OnTemplateNeeded(new RadPanelBarEventArgs((RadPanelItem)item));
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000C0408 File Offset: 0x000BE608
		protected override void RaiseItemCreated(ControlItem item)
		{
			this.OnItemCreated(new RadPanelBarEventArgs((RadPanelItem)item));
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000C041B File Offset: 0x000BE61B
		protected virtual void RaiseItemClick(ControlItem item)
		{
			this.OnItemClick(new RadPanelBarEventArgs((RadPanelItem)item));
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000C042E File Offset: 0x000BE62E
		protected internal override void RaiseItemDataBound(ControlItem item)
		{
			this.OnItemDataBound(new RadPanelBarEventArgs((RadPanelItem)item));
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000C0444 File Offset: 0x000BE644
		private void RaiseEvent(object eventKey, RadPanelBarEventArgs e)
		{
			RadPanelBarEventHandler radPanelBarEventHandler = (RadPanelBarEventHandler)base.Events[eventKey];
			if (radPanelBarEventHandler != null)
			{
				radPanelBarEventHandler(this, e);
			}
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000C046E File Offset: 0x000BE66E
		protected override void OnDataBound(EventArgs e)
		{
			if (!base.DesignMode)
			{
				this.LoadStateFromCookie();
			}
			base.OnDataBound(e);
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06003B36 RID: 15158 RVA: 0x000C0485 File Offset: 0x000BE685
		IRadPanelItemContainer IRadPanelItemContainer.Owner
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x000C0488 File Offset: 0x000BE688
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			if (this.Items.Count > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpRootGroup");
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				this.RenderItems(writer);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000C04E0 File Offset: 0x000BE6E0
		private void RenderItems(HtmlTextWriter writer)
		{
			foreach (object obj in this.Items)
			{
				RadPanelItem radPanelItem = (RadPanelItem)obj;
				radPanelItem.RenderControl(writer);
			}
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x000C053C File Offset: 0x000BE73C
		protected override void OnLoad(EventArgs e)
		{
			this.LoadStateFromCookie();
			base.OnLoad(e);
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x000C054C File Offset: 0x000BE74C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (base.ScriptManager.LoadScriptsBeforeUI && this.ExpandMode == PanelBarExpandMode.FullExpandedItem)
			{
				string script = string.Format("Telerik.Web.UI.RadPanelBar._preInitialize(\"{0}\");", this.ClientID);
				ScriptManager.RegisterStartupScript(this.Page, typeof(RadPanelBar), this.ClientID, script, true);
			}
			if (this.ExpandMode == PanelBarExpandMode.SingleExpandedItem || this.ExpandMode == PanelBarExpandMode.FullExpandedItem)
			{
				this.EnsureSingleItemIsExpanded();
			}
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x000C05BC File Offset: 0x000BE7BC
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text) || text == "null")
			{
				return false;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				this.LoadClientState(javaScriptSerializer.Deserialize<RadPanelBarClientState>(text));
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			return false;
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x000C0628 File Offset: 0x000BE828
		private void LoadClientState(RadPanelBarClientState clientState)
		{
			if (clientState.LogEntries != null)
			{
				this.LoadLogEntries(clientState);
			}
			if (clientState.ExpandedItems != null)
			{
				this.LoadExpandedState(clientState);
			}
			if (clientState.SelectedItems != null)
			{
				this.LoadSelectedState(clientState);
			}
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x000C0658 File Offset: 0x000BE858
		private void LoadLogEntries(RadPanelBarClientState clientState)
		{
			ClientStateLogPlayer<RadPanelItem> clientStateLogPlayer = new ClientStateLogPlayer<RadPanelItem>(this);
			this._clientChanges = clientStateLogPlayer.Play(clientState.LogEntries);
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x000C0680 File Offset: 0x000BE880
		private void LoadExpandedState(RadPanelBarClientState clientState)
		{
			this.CollapseAllVisibleItems();
			foreach (string hierarchicalIndex in clientState.ExpandedItems)
			{
				RadPanelItem radPanelItem = (RadPanelItem)this.FindItemByHierarchicalIndex(hierarchicalIndex);
				if (radPanelItem != null)
				{
					radPanelItem.Expanded = true;
				}
			}
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x000C06C4 File Offset: 0x000BE8C4
		private void LoadSelectedState(RadPanelBarClientState clientState)
		{
			this.ClearSelectedItems();
			foreach (string hierarchicalIndex in clientState.SelectedItems)
			{
				RadPanelItem radPanelItem = (RadPanelItem)this.FindItemByHierarchicalIndex(hierarchicalIndex);
				if (radPanelItem != null)
				{
					radPanelItem.Selected = true;
				}
			}
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x000C0707 File Offset: 0x000BE907
		void IPostBackEventHandler.RaisePostBackEvent(string itemIndex)
		{
			this.RaisePostBackEvent(itemIndex);
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x000C0710 File Offset: 0x000BE910
		protected virtual void RaisePostBackEvent(string itemIndex)
		{
			ControlItem controlItem = this.FindItemByHierarchicalIndex(itemIndex);
			if (controlItem != null)
			{
				this.PerformValidation();
				this.RaiseItemClick(controlItem);
			}
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x000C0735 File Offset: 0x000BE935
		private void PerformValidation()
		{
			if (!this.CausesValidation)
			{
				return;
			}
			this.Page.Validate(this.ValidationGroup);
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x000C0754 File Offset: 0x000BE954
		private void LoadStateFromCookie()
		{
			if (this.PersistStateInCookie)
			{
				HttpCookie httpCookie = this.Context.Request.Cookies[this.StateCookieName];
				if (httpCookie != null)
				{
					this.LoadState(httpCookie.Value);
				}
			}
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x000C0794 File Offset: 0x000BE994
		internal void LoadState(string state)
		{
			if (string.IsNullOrEmpty(state))
			{
				return;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			RadPanelBarClientState clientState = null;
			try
			{
				clientState = javaScriptSerializer.Deserialize<RadPanelBarClientState>(state);
			}
			catch
			{
				return;
			}
			this.LoadClientState(clientState);
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x000C07D8 File Offset: 0x000BE9D8
		internal bool HasExpandedItems()
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i].Expanded)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x000C0814 File Offset: 0x000BEA14
		private void EnsureSingleItemIsExpanded()
		{
			int num = -1;
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i].Expanded)
				{
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				for (int j = num + 1; j < this.Items.Count; j++)
				{
					this.Items[j].Expanded = false;
				}
			}
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x000C0880 File Offset: 0x000BEA80
		private void CollapseAllVisibleItems()
		{
			foreach (RadPanelItem radPanelItem in this.GetAllItems())
			{
				if (radPanelItem.Visible)
				{
					radPanelItem.Expanded = false;
				}
			}
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x000C08D8 File Offset: 0x000BEAD8
		protected internal override void InitializeItem(ControlItem item)
		{
			RadPanelItem radPanelItem = item as RadPanelItem;
			radPanelItem.ApplyHeaderTemplate();
			base.InitializeItem(item);
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x000C08F9 File Offset: 0x000BEAF9
		// (set) Token: 0x06003B4A RID: 15178 RVA: 0x000C0914 File Offset: 0x000BEB14
		[SimplePersistenceSetting]
		internal string SelectedIndex
		{
			get
			{
				if (this.SelectedItem != null)
				{
					return this.SelectedItem.GetHierarchicalIndex();
				}
				return string.Empty;
			}
			set
			{
				RadPanelItem radPanelItem = this.FindItemByHierarchicalIndex(value) as RadPanelItem;
				if (radPanelItem != null)
				{
					radPanelItem.Selected = true;
				}
			}
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000C0938 File Offset: 0x000BEB38
		// (set) Token: 0x06003B4C RID: 15180 RVA: 0x000C0940 File Offset: 0x000BEB40
		[SimplePersistenceSetting]
		internal List<string> ExpandedIndices
		{
			get
			{
				return this.GetExpandedItemsIndices();
			}
			set
			{
				this.CollapseAllItems();
				foreach (string hierarchicalIndex in value)
				{
					RadPanelItem radPanelItem = this.FindItemByHierarchicalIndex(hierarchicalIndex) as RadPanelItem;
					if (radPanelItem != null)
					{
						radPanelItem.Expanded = true;
					}
				}
			}
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x000C09A4 File Offset: 0x000BEBA4
		private List<string> GetExpandedItemsIndices()
		{
			List<string> list = new List<string>();
			IList<RadPanelItem> allItems = this.GetAllItems();
			foreach (RadPanelItem radPanelItem in allItems)
			{
				if (radPanelItem.Expanded)
				{
					list.Add(radPanelItem.GetHierarchicalIndex());
				}
			}
			return list;
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000C0A08 File Offset: 0x000BEC08
		// Note: this type is marked as 'beforefieldinit'.
		static RadPanelBar()
		{
			RadPanelBar.TemplateNeededEvent = new object();
			RadPanelBar.ItemDataBoundEvent = new object();
			RadPanelBar.ItemClickEvent = new object();
			RadPanelBar.ItemCreatedEvent = new object();
		}

		// Token: 0x04001003 RID: 4099
		private WaiAriaSettings _ariaSettings;

		// Token: 0x04001004 RID: 4100
		private IList<ClientOperation<RadPanelItem>> _clientChanges = new List<ClientOperation<RadPanelItem>>();

		// Token: 0x04001009 RID: 4105
		private AnimationSettings _expandAnimation;

		// Token: 0x0400100A RID: 4106
		private AnimationSettings _collapseAnimation;

		// Token: 0x0400100B RID: 4107
		private bool _renderEditableRegions;

		// Token: 0x0200064C RID: 1612
		internal static class Styles
		{
			// Token: 0x06003B4F RID: 15183 RVA: 0x000C0A32 File Offset: 0x000BEC32
			internal static string Combine(params string[] classNames)
			{
				return string.Join(" ", classNames).Trim();
			}

			// Token: 0x0400100C RID: 4108
			public const string LinkCssClass = "rpLink";

			// Token: 0x0400100D RID: 4109
			public const string RootLinkCssClass = "rpRootLink";

			// Token: 0x0400100E RID: 4110
			public const string OutCssClass = "rpOut";

			// Token: 0x0400100F RID: 4111
			public const string TextCssClass = "rpText";

			// Token: 0x04001010 RID: 4112
			public const string ImageCssClass = "rpImage";

			// Token: 0x04001011 RID: 4113
			public const string GroupCssClass = "rpGroup";

			// Token: 0x04001012 RID: 4114
			public const string NavigationCssClass = "rpNavigation";

			// Token: 0x04001013 RID: 4115
			public const string ExpandableCssClass = "rpExpandable";

			// Token: 0x04001014 RID: 4116
			public const string ExpandHandleCssClass = "rpExpandHandle";

			// Token: 0x04001015 RID: 4117
			public const string SlideCssClass = "rpSlide";

			// Token: 0x04001016 RID: 4118
			public const string TemplateCssClass = "rpTemplate";

			// Token: 0x04001017 RID: 4119
			public const string HeaderTemplateCssClass = "rpHeaderTemplate";
		}
	}
}
