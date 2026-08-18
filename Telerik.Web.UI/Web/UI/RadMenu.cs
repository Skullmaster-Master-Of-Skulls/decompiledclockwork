using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.Web.UI.Menu.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020005D5 RID: 1493
	[ControlValueProperty("SelectedValue")]
	[ClientScriptResource("Telerik.Web.UI.RadMenu", "Telerik.Web.UI.Menu.RadMenuScripts.js", LoadOrder = 6)]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadMenu))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Mobile, typeof(RadButton))]
	[DefaultProperty("Items")]
	[RequiredScript(typeof(ScrollingScripts), 3)]
	[EmbeddedSkin("Menu", "Default", typeof(RadMenu))]
	[DefaultEvent("ItemClick")]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadMenu), "Telerik.Web.UI.Menu.png")]
	[Designer("Telerik.Web.Design.RadMenuDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadMenu Runat=\"server\"></{0}:RadMenu>")]
	[AdaptiveRendering]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ValidationProperty("SelectedValue")]
	[XmlRoot("Menu")]
	[EmbeddedSkin("Menu", typeof(RadMenu))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadMenu))]
	[RequiredScript(typeof(Core), 1)]
	[RequiredScript(typeof(jQueryPlugins), 2)]
	[RequiredScript(typeof(OData), 4)]
	[RequiredScript(typeof(MaterialRipple))]
	[RequiredScript(typeof(AnimationFramework), 5)]
	public class RadMenu : HierarchicalControlItemContainer, IRadMenuItemContainer, IPostBackEventHandler
	{
		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x060035C3 RID: 13763 RVA: 0x000B2508 File Offset: 0x000B0708
		[Browsable(false)]
		public IList<ClientOperation<RadMenuItem>> ClientChanges
		{
			get
			{
				return this._clientChanges;
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x060035C4 RID: 13764 RVA: 0x000B2510 File Offset: 0x000B0710
		[Editor("Telerik.Web.Design.ControlItemCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadMenuItemCollection Items
		{
			get
			{
				return (RadMenuItemCollection)base.Children;
			}
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x060035C5 RID: 13765 RVA: 0x000B251D File Offset: 0x000B071D
		// (set) Token: 0x060035C6 RID: 13766 RVA: 0x000B253E File Offset: 0x000B073E
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("When set to true enables support for WAI-ARIA.")]
		[ClientControlProperty]
		[ClientPropertyName("enableAriaSupport")]
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

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x060035C7 RID: 13767 RVA: 0x000B2558 File Offset: 0x000B0758
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
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

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x060035C8 RID: 13768 RVA: 0x000B257D File Offset: 0x000B077D
		// (set) Token: 0x060035C9 RID: 13769 RVA: 0x000B2585 File Offset: 0x000B0785
		[DefaultValue(null)]
		[TemplateContainer(typeof(RadMenuItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
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

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x060035CA RID: 13770 RVA: 0x000B258E File Offset: 0x000B078E
		// (set) Token: 0x060035CB RID: 13771 RVA: 0x000B25AE File Offset: 0x000B07AE
		[Description("Gets or sets the template for displaying the items in Radmenu.")]
		[Browsable(false)]
		[DefaultValue("")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual string ClientItemTemplate
		{
			get
			{
				return (this.ViewState["ClientItemTemplate"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientItemTemplate"] = value;
			}
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x060035CC RID: 13772 RVA: 0x000B25C1 File Offset: 0x000B07C1
		// (set) Token: 0x060035CD RID: 13773 RVA: 0x000B25C9 File Offset: 0x000B07C9
		[DefaultValue(null)]
		[Bindable(false)]
		[TemplateContainer(typeof(RadMenuItem))]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate LoadingStatusTemplate { get; set; }

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x060035CE RID: 13774 RVA: 0x000B25D2 File Offset: 0x000B07D2
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("The animation played when item is opened")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public AnimationSettings ExpandAnimation
		{
			get
			{
				return this._expandAnimation;
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x060035CF RID: 13775 RVA: 0x000B25DA File Offset: 0x000B07DA
		// (set) Token: 0x060035D0 RID: 13776 RVA: 0x000B25FC File Offset: 0x000B07FC
		[Description("Delay in milliseconds between the mouse entering a RadMenuItem and its child items starting to expand")]
		[Category("Behavior")]
		[ClientPropertyName("expandDelay")]
		[DefaultValue(100)]
		[ClientControlProperty]
		public int ExpandDelay
		{
			get
			{
				return (int)(this.ViewState["ExpandDelay"] ?? 100);
			}
			set
			{
				this.ViewState["ExpandDelay"] = value;
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x060035D1 RID: 13777 RVA: 0x000B2614 File Offset: 0x000B0814
		[Category("Behavior")]
		[Description("The animation played when item is closed")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AnimationSettings CollapseAnimation
		{
			get
			{
				return this._collapseAnimation;
			}
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x000B261C File Offset: 0x000B081C
		// (set) Token: 0x060035D3 RID: 13779 RVA: 0x000B2641 File Offset: 0x000B0841
		[DefaultValue(500)]
		[Description("Delay in milliseconds between the mouse leaving the menu and the menu starting to collapse")]
		[ClientControlProperty]
		[ClientPropertyName("collapseDelay")]
		[Category("Behavior")]
		public int CollapseDelay
		{
			get
			{
				return (int)(this.ViewState["CollapseDelay"] ?? 500);
			}
			set
			{
				this.ViewState["CollapseDelay"] = value;
			}
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x060035D4 RID: 13780 RVA: 0x000B2659 File Offset: 0x000B0859
		// (set) Token: 0x060035D5 RID: 13781 RVA: 0x000B267A File Offset: 0x000B087A
		[ClientControlProperty]
		[Description("Orientation of the root items")]
		[DefaultValue(ItemFlow.Horizontal)]
		[Category("Behavior")]
		[ClientPropertyName("_flow")]
		public virtual ItemFlow Flow
		{
			get
			{
				return (ItemFlow)(this.ViewState["Flow"] ?? ItemFlow.Horizontal);
			}
			set
			{
				this.ViewState["Flow"] = value;
			}
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x060035D6 RID: 13782 RVA: 0x000B2692 File Offset: 0x000B0892
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Default child item settings")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Layout")]
		public RadMenuItemGroupSettings DefaultGroupSettings
		{
			get
			{
				return this._groupSettings;
			}
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x060035D7 RID: 13783 RVA: 0x000B269A File Offset: 0x000B089A
		// (set) Token: 0x060035D8 RID: 13784 RVA: 0x000B26BB File Offset: 0x000B08BB
		[ClientPropertyName("enableAutoScroll")]
		[Description("a value indicating if an automatic scroll is applied if the groups are larger then the screen height")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		public bool EnableAutoScroll
		{
			get
			{
				return (bool)(this.ViewState["EnableAutoScroll"] ?? false);
			}
			set
			{
				this.ViewState["EnableAutoScroll"] = value;
			}
		}

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x060035D9 RID: 13785 RVA: 0x000B26D3 File Offset: 0x000B08D3
		// (set) Token: 0x060035DA RID: 13786 RVA: 0x000B26F4 File Offset: 0x000B08F4
		[DefaultValue(false)]
		[ClientPropertyName("enableRootItemScroll")]
		[ClientControlProperty]
		[Description("a value indicating if scroll is enabled for the root items")]
		[Category("Behavior")]
		public bool EnableRootItemScroll
		{
			get
			{
				return (bool)(this.ViewState["EnableRootItemScroll"] ?? false);
			}
			set
			{
				this.ViewState["EnableRootItemScroll"] = value;
			}
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x060035DB RID: 13787 RVA: 0x000B270C File Offset: 0x000B090C
		// (set) Token: 0x060035DC RID: 13788 RVA: 0x000B272D File Offset: 0x000B092D
		[ClientPropertyName("enableSelection")]
		[Description("a value indicating if the currently selected item will be tracked and highlighted")]
		[DefaultValue(true)]
		[Category("Behavior")]
		[ClientControlProperty]
		public virtual bool EnableSelection
		{
			get
			{
				return (bool)(this.ViewState["EnableSelection"] ?? true);
			}
			set
			{
				this.ViewState["EnableSelection"] = value;
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x060035DD RID: 13789 RVA: 0x000B2745 File Offset: 0x000B0945
		// (set) Token: 0x060035DE RID: 13790 RVA: 0x000B2767 File Offset: 0x000B0967
		[ClientControlProperty]
		[Category("Behavior")]
		[ClientPropertyName("autoScrollMinimumHeight")]
		[Description("The minimum available height that is needed to enable the auto-scroll")]
		[DefaultValue(50)]
		public int AutoScrollMinimumHeight
		{
			get
			{
				return (int)(this.ViewState["AutoScrollMinimumHeight"] ?? 50);
			}
			set
			{
				this.ViewState["AutoScrollMinimumHeight"] = value;
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x000B277F File Offset: 0x000B097F
		// (set) Token: 0x060035E0 RID: 13792 RVA: 0x000B27A1 File Offset: 0x000B09A1
		[DefaultValue(50)]
		[Description("The minimum available width that is needed to enable the auto-scroll")]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("autoScrollMinimumWidth")]
		public int AutoScrollMinimumWidth
		{
			get
			{
				return (int)(this.ViewState["AutoScrollMinimumWidth"] ?? 50);
			}
			set
			{
				this.ViewState["AutoScrollMinimumWidth"] = value;
			}
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x000B27B9 File Offset: 0x000B09B9
		// (set) Token: 0x060035E2 RID: 13794 RVA: 0x000B27DA File Offset: 0x000B09DA
		[Category("Behavior")]
		[DefaultValue(true)]
		[ClientControlProperty]
		[ClientPropertyName("enableScreenBoundaryDetection")]
		[Description("Specifies where screen boundary detection is enabled or not.")]
		public bool EnableScreenBoundaryDetection
		{
			get
			{
				return (bool)(this.ViewState["EnableScreenBoundaryDetection"] ?? true);
			}
			set
			{
				this.ViewState["EnableScreenBoundaryDetection"] = value;
			}
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x060035E3 RID: 13795 RVA: 0x000B27F2 File Offset: 0x000B09F2
		// (set) Token: 0x060035E4 RID: 13796 RVA: 0x000B2813 File Offset: 0x000B0A13
		[ClientPropertyName("clickToOpen")]
		[Description("Specifying if child items should open when the user clicks on their parent item, rather than just pointing the mouse over it.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool ClickToOpen
		{
			get
			{
				return (bool)(this.ViewState["ClickToOpen"] ?? false);
			}
			set
			{
				this.ViewState["ClickToOpen"] = value;
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x000B282B File Offset: 0x000B0A2B
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(null)]
		[Description("The web service to be used for populating items with ExpandMode set to WebService.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public NavigationControlWebServiceSettings WebServiceSettings
		{
			get
			{
				return this._webServiceSettings;
			}
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x060035E6 RID: 13798 RVA: 0x000B2833 File Offset: 0x000B0A33
		// (set) Token: 0x060035E7 RID: 13799 RVA: 0x000B2854 File Offset: 0x000B0A54
		[DefaultValue(true)]
		[Description("When set to true, the items populated through Load On Demand are persisted on the server")]
		[ClientControlProperty]
		[ClientPropertyName("persistLoadOnDemandItems")]
		[Category("Behavior")]
		public bool PersistLoadOnDemandItems
		{
			get
			{
				return (bool)(this.ViewState["PersistLoadOnDemandItems"] ?? true);
			}
			set
			{
				this.ViewState["PersistLoadOnDemandItems"] = value;
			}
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x060035E8 RID: 13800 RVA: 0x000B286C File Offset: 0x000B0A6C
		// (set) Token: 0x060035E9 RID: 13801 RVA: 0x000B288D File Offset: 0x000B0A8D
		[Description("A value indicating if an overlay should be rendered (only in Internet Explorer).")]
		[ClientPropertyName("enableOverlay")]
		[DefaultValue(true)]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool EnableOverlay
		{
			get
			{
				return (bool)(this.ViewState["EnableOverlay"] ?? true);
			}
			set
			{
				this.ViewState["EnableOverlay"] = value;
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x060035EA RID: 13802 RVA: 0x000B28A5 File Offset: 0x000B0AA5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[Category("Data")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadMenuItemBindingCollection DataBindings
		{
			get
			{
				return (RadMenuItemBindingCollection)base.NavigationItemBindings;
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x000B28B2 File Offset: 0x000B0AB2
		// (set) Token: 0x060035EC RID: 13804 RVA: 0x000B28BA File Offset: 0x000B0ABA
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

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x000B28C3 File Offset: 0x000B0AC3
		// (set) Token: 0x060035EE RID: 13806 RVA: 0x000B28CB File Offset: 0x000B0ACB
		[Category("Behavior")]
		[UrlProperty("*.aspx")]
		[DefaultValue("")]
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

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x000B28D4 File Offset: 0x000B0AD4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadMenuItem SelectedItem
		{
			get
			{
				foreach (RadMenuItem radMenuItem in this.GetAllItems())
				{
					if (radMenuItem.Selected)
					{
						return radMenuItem;
					}
				}
				return null;
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x000B292C File Offset: 0x000B0B2C
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ClientControlProperty]
		[ClientPropertyName("_selectedValue")]
		[Browsable(false)]
		public string SelectedValue
		{
			get
			{
				RadMenuItem selectedItem = this.SelectedItem;
				if (selectedItem != null)
				{
					string text = selectedItem.Value;
					if (string.IsNullOrEmpty(text))
					{
						text = selectedItem.Text;
					}
					return text;
				}
				return string.Empty;
			}
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x060035F1 RID: 13809 RVA: 0x000B2960 File Offset: 0x000B0B60
		// (set) Token: 0x060035F2 RID: 13810 RVA: 0x000B2981 File Offset: 0x000B0B81
		[DefaultValue(false)]
		[Category("Appearance")]
		[ClientControlProperty]
		[Description("Specifying if child items should have rounded corners.")]
		[ClientPropertyName("enableRoundedCorners")]
		public bool EnableRoundedCorners
		{
			get
			{
				return (bool)(this.ViewState["EnableRoundedCorners"] ?? false);
			}
			set
			{
				this.ViewState["EnableRoundedCorners"] = value;
			}
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x060035F3 RID: 13811 RVA: 0x000B2999 File Offset: 0x000B0B99
		// (set) Token: 0x060035F4 RID: 13812 RVA: 0x000B29BA File Offset: 0x000B0BBA
		[Description("Specifying if child items should have shadows.")]
		[ClientPropertyName("enableShadows")]
		[DefaultValue(false)]
		[Category("Appearance")]
		[ClientControlProperty]
		public bool EnableShadows
		{
			get
			{
				return (bool)(this.ViewState["EnableShadows"] ?? false);
			}
			set
			{
				this.ViewState["EnableShadows"] = value;
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x060035F5 RID: 13813 RVA: 0x000B29D2 File Offset: 0x000B0BD2
		// (set) Token: 0x060035F6 RID: 13814 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		[Description("Specifies whether a toggle button is rendered when an item has children.")]
		[DefaultValue(false)]
		[ClientPropertyName("showToggleHandle")]
		[ClientControlProperty]
		[Category("Behavior")]
		public bool ShowToggleHandle
		{
			get
			{
				return (bool)(this.ViewState["ShowToggleHandle"] ?? false);
			}
			set
			{
				this.ViewState["ShowToggleHandle"] = value;
			}
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x000B2A0B File Offset: 0x000B0C0B
		// (set) Token: 0x060035F8 RID: 13816 RVA: 0x000B2A2C File Offset: 0x000B0C2C
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Specifies whether the text encoding when rendering menu item is enabled or not.")]
		public bool EnableTextHTMLEncoding
		{
			get
			{
				return (bool)(this.ViewState["EnableTextHTMLEncoding"] ?? false);
			}
			set
			{
				this.ViewState["EnableTextHTMLEncoding"] = value;
			}
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x000B2A44 File Offset: 0x000B0C44
		// (set) Token: 0x060035FA RID: 13818 RVA: 0x000B2A65 File Offset: 0x000B0C65
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("enableImageSprites")]
		[DefaultValue(false)]
		[Description("A value indicating if an image sprite containers should be used instead of the default images")]
		public virtual bool EnableImageSprites
		{
			get
			{
				return (bool)(this.ViewState["EnableImageSprites"] ?? false);
			}
			set
			{
				this.ViewState["EnableImageSprites"] = value;
			}
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x060035FB RID: 13819 RVA: 0x000B2A7D File Offset: 0x000B0C7D
		// (set) Token: 0x060035FC RID: 13820 RVA: 0x000B2A9E File Offset: 0x000B0C9E
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Appearance")]
		[Description("Specifying if items images should be preloaded.")]
		[ClientPropertyName("_enableItemImagesPreloading")]
		public bool EnableImagePreloading
		{
			get
			{
				return (bool)(this.ViewState["EnableImagePreloading"] ?? false);
			}
			set
			{
				this.ViewState["EnableImagePreloading"] = value;
			}
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000B2AB6 File Offset: 0x000B0CB6
		public override void LoadContentFile(string xmlFileName)
		{
			base.LoadContentFile(xmlFileName);
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000B2ABF File Offset: 0x000B0CBF
		public IList<RadMenuItem> GetAllItems()
		{
			return base.GetAllChildren<RadMenuItem>();
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000B2AC7 File Offset: 0x000B0CC7
		public RadMenuItem FindItemByText(string text)
		{
			return base.FindChildByText<RadMenuItem>(text);
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000B2AD0 File Offset: 0x000B0CD0
		public RadMenuItem FindItemByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadMenuItem>(text, ignoreCase);
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000B2ADA File Offset: 0x000B0CDA
		public RadMenuItem FindItemByValue(string value)
		{
			return this.FindChildByValue<RadMenuItem>(value);
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000B2AE3 File Offset: 0x000B0CE3
		public RadMenuItem FindItemByValue(string value, bool ignoreCase)
		{
			return this.FindChildByValue<RadMenuItem>(value, ignoreCase);
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000B2AED File Offset: 0x000B0CED
		public RadMenuItem FindItemByUrl(string url)
		{
			return base.FindChildByUrl<RadMenuItem>(url);
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000B2AF6 File Offset: 0x000B0CF6
		public RadMenuItem FindItem(Predicate<RadMenuItem> match)
		{
			return base.FindChild<RadMenuItem>(match);
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000B2B00 File Offset: 0x000B0D00
		public void ClearSelectedItem()
		{
			foreach (RadMenuItem radMenuItem in this.GetAllItems())
			{
				radMenuItem.Selected = false;
			}
		}

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x06003606 RID: 13830 RVA: 0x000B2B50 File Offset: 0x000B0D50
		// (remove) Token: 0x06003607 RID: 13831 RVA: 0x000B2B63 File Offset: 0x000B0D63
		[Description("Fired after a RadMenuItem is created.")]
		[Category("Behavior")]
		public event RadMenuEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadMenu.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMenu.ItemCreatedEvent, value);
			}
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000B2B76 File Offset: 0x000B0D76
		protected virtual void OnItemCreated(RadMenuEventArgs e)
		{
			this.RaiseMenuItemEvent(RadMenu.ItemCreatedEvent, e);
		}

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x06003609 RID: 13833 RVA: 0x000B2B84 File Offset: 0x000B0D84
		// (remove) Token: 0x0600360A RID: 13834 RVA: 0x000B2B97 File Offset: 0x000B0D97
		public event RadMenuEventHandler TemplateNeeded
		{
			add
			{
				base.Events.AddHandler(RadMenu.TemplateNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMenu.TemplateNeededEvent, value);
			}
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000B2BAA File Offset: 0x000B0DAA
		protected virtual void OnTemplateNeeded(RadMenuEventArgs e)
		{
			this.RaiseMenuItemEvent(RadMenu.TemplateNeededEvent, e);
		}

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x0600360C RID: 13836 RVA: 0x000B2BB8 File Offset: 0x000B0DB8
		// (remove) Token: 0x0600360D RID: 13837 RVA: 0x000B2BCB File Offset: 0x000B0DCB
		[Description("Fired after a menu item is clicked.")]
		public virtual event RadMenuEventHandler ItemClick
		{
			add
			{
				base.Events.AddHandler(RadMenu.ItemClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMenu.ItemClickEvent, value);
			}
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000B2BDE File Offset: 0x000B0DDE
		protected virtual void OnItemClick(RadMenuEventArgs e)
		{
			this.RaiseMenuItemEvent(RadMenu.ItemClickEvent, e);
		}

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x0600360F RID: 13839 RVA: 0x000B2BEC File Offset: 0x000B0DEC
		// (remove) Token: 0x06003610 RID: 13840 RVA: 0x000B2BFF File Offset: 0x000B0DFF
		[Category("Behavior")]
		[Description("Fired after a RadMenuItem is databound.")]
		public event RadMenuEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadMenu.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMenu.ItemDataBoundEvent, value);
			}
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000B2C12 File Offset: 0x000B0E12
		protected virtual void OnItemDataBound(RadMenuEventArgs e)
		{
			this.RaiseMenuItemEvent(RadMenu.ItemDataBoundEvent, e);
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x000B2C20 File Offset: 0x000B0E20
		// (set) Token: 0x06003613 RID: 13843 RVA: 0x000B2C40 File Offset: 0x000B0E40
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the javascript function called when the mouse hovers a menu item.")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("mouseOver")]
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

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x06003614 RID: 13844 RVA: 0x000B2C53 File Offset: 0x000B0E53
		// (set) Token: 0x06003615 RID: 13845 RVA: 0x000B2C73 File Offset: 0x000B0E73
		[DefaultValue("")]
		[ClientControlEvent]
		[Description("The name of the javascript function called after the mouse leaves a menu item.")]
		[ClientPropertyName("mouseOut")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
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

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x000B2C86 File Offset: 0x000B0E86
		// (set) Token: 0x06003617 RID: 13847 RVA: 0x000B2CA6 File Offset: 0x000B0EA6
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("The name of the javascript function called after a menu item is checked.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientPropertyName("itemFocus")]
		public string OnClientItemFocus
		{
			get
			{
				return (string)(this.ViewState["OnClientItemFocus"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemFocus"] = value;
			}
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x000B2CB9 File Offset: 0x000B0EB9
		// (set) Token: 0x06003619 RID: 13849 RVA: 0x000B2CD9 File Offset: 0x000B0ED9
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called after a menu item loses focus.")]
		[ClientPropertyName("itemBlur")]
		public string OnClientItemBlur
		{
			get
			{
				return (string)(this.ViewState["OnClientItemBlur"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemBlur"] = value;
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x000B2CEC File Offset: 0x000B0EEC
		// (set) Token: 0x0600361B RID: 13851 RVA: 0x000B2D0C File Offset: 0x000B0F0C
		[ClientControlEvent]
		[DefaultValue("")]
		[ClientPropertyName("itemClicking")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the javascript function called before item clicking.")]
		[Category("Client-side events")]
		public virtual string OnClientItemClicking
		{
			get
			{
				return (string)(this.ViewState["OnClientItemClicking"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemClicking"] = value;
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x000B2D1F File Offset: 0x000B0F1F
		// (set) Token: 0x0600361D RID: 13853 RVA: 0x000B2D3F File Offset: 0x000B0F3F
		[ClientPropertyName("itemClicked")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called after a menu item is clicked.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public virtual string OnClientItemClicked
		{
			get
			{
				return (string)(this.ViewState["OnClientItemClicked"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemClicked"] = value;
			}
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x000B2D52 File Offset: 0x000B0F52
		// (set) Token: 0x0600361F RID: 13855 RVA: 0x000B2D72 File Offset: 0x000B0F72
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when a menu item is opening.")]
		[DefaultValue("")]
		[ClientPropertyName("itemOpening")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientItemOpening
		{
			get
			{
				return (string)(this.ViewState["OnClientItemOpening"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemOpening"] = value;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06003620 RID: 13856 RVA: 0x000B2D85 File Offset: 0x000B0F85
		// (set) Token: 0x06003621 RID: 13857 RVA: 0x000B2DA5 File Offset: 0x000B0FA5
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemOpened")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("The name of the javascript function called after a menu item is opened.")]
		public string OnClientItemOpened
		{
			get
			{
				return (string)(this.ViewState["OnClientItemOpened"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemOpened"] = value;
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000B2DB8 File Offset: 0x000B0FB8
		// (set) Token: 0x06003623 RID: 13859 RVA: 0x000B2DD8 File Offset: 0x000B0FD8
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the javascript function called when a menu item is closing.")]
		[ClientControlEvent]
		[ClientPropertyName("itemClosing")]
		[Category("Client-side events")]
		public string OnClientItemClosing
		{
			get
			{
				return (string)(this.ViewState["OnClientItemClosing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemClosing"] = value;
			}
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x000B2DEB File Offset: 0x000B0FEB
		// (set) Token: 0x06003625 RID: 13861 RVA: 0x000B2E0B File Offset: 0x000B100B
		[Description("The name of the javascript function called after a menu item is closed.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemClosed")]
		[Category("Client-side events")]
		public string OnClientItemClosed
		{
			get
			{
				return (string)(this.ViewState["OnClientItemClosed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemClosed"] = value;
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000B2E1E File Offset: 0x000B101E
		// (set) Token: 0x06003627 RID: 13863 RVA: 0x000B2E3E File Offset: 0x000B103E
		[Category("Client-side events")]
		[ClientPropertyName("itemPopulating")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("The name of the javascript function called before the children of a menu item are populated.")]
		public string OnClientItemPopulating
		{
			get
			{
				return (string)(this.ViewState["OnClientItemPopulating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemPopulating"] = value;
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000B2E51 File Offset: 0x000B1051
		// (set) Token: 0x06003629 RID: 13865 RVA: 0x000B2E71 File Offset: 0x000B1071
		[Category("Client-side events")]
		[Description("The name of the javascript function called after the children of a menu item were populated.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemPopulated")]
		[DefaultValue("")]
		public string OnClientItemPopulated
		{
			get
			{
				return (string)(this.ViewState["OnClientItemPopulated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemPopulated"] = value;
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000B2E84 File Offset: 0x000B1084
		// (set) Token: 0x0600362B RID: 13867 RVA: 0x000B2EA4 File Offset: 0x000B10A4
		[ClientControlEvent]
		[Description("The name of the javascript function called before the children of a menu item are populated.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("menuPopulating")]
		[Category("Client-side events")]
		public string OnClientMenuPopulating
		{
			get
			{
				return (string)(this.ViewState["OnClientMenuPopulating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientMenuPopulating"] = value;
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x000B2EB7 File Offset: 0x000B10B7
		// (set) Token: 0x0600362D RID: 13869 RVA: 0x000B2ED7 File Offset: 0x000B10D7
		[Description("The name of the javascript function called before the children of a menu item are populated.")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("menuPopulated")]
		[Category("Client-side events")]
		public string OnClientMenuPopulated
		{
			get
			{
				return (string)(this.ViewState["OnClientMenuPopulated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientMenuPopulated"] = value;
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x0600362E RID: 13870 RVA: 0x000B2EEA File Offset: 0x000B10EA
		// (set) Token: 0x0600362F RID: 13871 RVA: 0x000B2F0A File Offset: 0x000B110A
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the operation to populate the children of a menu item has failed.")]
		[ClientControlEvent]
		[ClientPropertyName("itemPopulationFailed")]
		public string OnClientItemPopulationFailed
		{
			get
			{
				return (string)(this.ViewState["OnClientItemPopulationFailed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemPopulationFailed"] = value;
			}
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x000B2F1D File Offset: 0x000B111D
		// (set) Token: 0x06003631 RID: 13873 RVA: 0x000B2F3D File Offset: 0x000B113D
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when the client template for a item is evaluated")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("templateDataBound")]
		[DefaultValue("")]
		public string OnClientTemplateDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientTemplateDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTemplateDataBound"] = value;
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x000B2F50 File Offset: 0x000B1150
		// (set) Token: 0x06003633 RID: 13875 RVA: 0x000B2F70 File Offset: 0x000B1170
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called after a menu is loaded.")]
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

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06003634 RID: 13876 RVA: 0x000B2F83 File Offset: 0x000B1183
		protected override string CssClassFormatString
		{
			get
			{
				return this.Renderer.CssClassFormatString;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x000B2F90 File Offset: 0x000B1190
		IRadMenuItemContainer IRadMenuItemContainer.Owner
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06003636 RID: 13878 RVA: 0x000B2F93 File Offset: 0x000B1193
		internal override bool SupportsOData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000B2F96 File Offset: 0x000B1196
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06003638 RID: 13880 RVA: 0x000B2F9C File Offset: 0x000B119C
		internal bool AutoPostBack
		{
			get
			{
				return (RadMenuEventHandler)base.Events[RadMenu.ItemClickEvent] != null || !string.IsNullOrEmpty(this.PostBackUrl);
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000B2FD2 File Offset: 0x000B11D2
		// (set) Token: 0x0600363A RID: 13882 RVA: 0x000B2FDA File Offset: 0x000B11DA
		[DefaultValue("")]
		[ClientControlProperty]
		[ClientPropertyName("_childListElementCssClass")]
		protected internal string ChildListElementCssClass { get; set; }

		// Token: 0x0600363B RID: 13883 RVA: 0x000B2FE4 File Offset: 0x000B11E4
		internal static string GetFlowCssClass(ItemFlow flow)
		{
			switch (flow)
			{
			case ItemFlow.Vertical:
				return "rmVertical";
			case ItemFlow.Horizontal:
				return "rmHorizontal";
			default:
				return string.Empty;
			}
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x000B3014 File Offset: 0x000B1214
		public RadMenu()
		{
			this._webServiceSettings = new NavigationControlWebServiceSettings(this.ViewState);
			this._groupSettings = new RadMenuItemGroupSettings(this.ViewState);
			this._expandAnimation = new MenuAnimationSettings("Expand", this.ViewState);
			this._collapseAnimation = new MenuAnimationSettings("Collapse", this.ViewState);
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x000B308B File Offset: 0x000B128B
		protected override NavigationItemBindingCollection CreateDataBindings()
		{
			return new RadMenuItemBindingCollection();
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000B3092 File Offset: 0x000B1292
		protected internal override ControlItem CreateItem()
		{
			return new RadMenuItem();
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000B3099 File Offset: 0x000B1299
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadMenuItemCollection(this);
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x000B30A4 File Offset: 0x000B12A4
		protected internal override void InitializeItem(ControlItem item)
		{
			RadMenuItem radMenuItem = item as RadMenuItem;
			radMenuItem.ApplyContentTemplate();
			base.InitializeItem(item);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000B30C8 File Offset: 0x000B12C8
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new RadMenuItemConverter(),
				new RadMenuItemGroupSettingsConverter(),
				new AttributeCollectionConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(converters);
			descriptor.AddScriptProperty("itemData", javaScriptSerializer.Serialize(this.Items.VisibleItems));
			if (base.Attributes.Count > 0)
			{
				descriptor.AddScriptProperty("attributes", javaScriptSerializer.Serialize(base.Attributes));
			}
			if (this.AutoPostBack)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
			if (this.SelectedItem != null && this.SelectedItem.Visible)
			{
				descriptor.AddProperty("_selectedItemIndex", this.SelectedItem.HierarchicalIndex);
			}
			if (!string.IsNullOrEmpty(this.ClientItemTemplate))
			{
				descriptor.AddProperty("clientTemplate", this.ClientItemTemplate);
			}
			if (!string.IsNullOrEmpty(this._cachedClickedIndex))
			{
				descriptor.AddProperty("_cachedClickedIndex", this._cachedClickedIndex);
			}
			if (this.IsBoundUsingOData && this.DataBindings.Count > 0)
			{
				descriptor.AddScriptProperty("dataBindings", javaScriptSerializer.Serialize(DataBindingsCollection.FromStateManagedCollection(this.DataBindings)));
			}
			ControlItemContainer.AddProperty(descriptor, "_skin", base.RuntimeSkin, string.Empty);
			if (this.ResolvedRenderMode == RenderMode.Mobile && !this.Height.IsEmpty)
			{
				descriptor.AddProperty("_popUpHeight", this.Height.Value);
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				descriptor.AddProperty("_dataFieldParentID", this.DataFieldParentID);
				descriptor.AddProperty("_dataFieldID", this.DataFieldID);
				descriptor.AddProperty("_dataNavigateUrlField", this.DataNavigateUrlField);
			}
			base.DescribeRenderingMode(descriptor);
			this.DescribeDefaultGroupSettings(javaScriptSerializer, descriptor);
			this.DescribeLoadingTemplate(descriptor);
			this.AriaSettings.Describe(descriptor);
			this.ExpandAnimation.Describe("expandAnimation", javaScriptSerializer, descriptor);
			this.CollapseAnimation.Describe("collapseAnimation", javaScriptSerializer, descriptor);
			this.WebServiceSettings.Describe("webServiceSettings", javaScriptSerializer, descriptor);
			base.DescribeComponent(descriptor);
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000B32EC File Offset: 0x000B14EC
		private void DescribeDefaultGroupSettings(JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			RadMenuItemGroupSettingsConverter radMenuItemGroupSettingsConverter = new RadMenuItemGroupSettingsConverter();
			IDictionary<string, object> dictionary = radMenuItemGroupSettingsConverter.Serialize(this.DefaultGroupSettings, serializer);
			if (dictionary.Count > 0)
			{
				descriptor.AddProperty("defaultGroupSettings", serializer.Serialize(this.DefaultGroupSettings));
			}
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000B3330 File Offset: 0x000B1530
		private void DescribeLoadingTemplate(IScriptDescriptor descriptor)
		{
			if (this.LoadingStatusTemplate != null)
			{
				Control control = new Control();
				this.Controls.Add(control);
				this.LoadingStatusTemplate.InstantiateIn(control);
				StringWriter stringWriter = new StringWriter();
				control.RenderControl(new HtmlTextWriter(stringWriter));
				descriptor.AddProperty("loadingTemplate", stringWriter.ToString());
				this.Controls.Remove(control);
			}
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000B3394 File Offset: 0x000B1594
		protected void RaiseMenuItemEvent(object eventKey, RadMenuEventArgs e)
		{
			RadMenuEventHandler radMenuEventHandler = (RadMenuEventHandler)base.Events[eventKey];
			if (radMenuEventHandler != null)
			{
				radMenuEventHandler(this, e);
			}
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000B33BE File Offset: 0x000B15BE
		protected override void RaiseItemCreated(ControlItem item)
		{
			this.OnItemCreated(new RadMenuEventArgs((RadMenuItem)item));
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000B33D1 File Offset: 0x000B15D1
		protected override void RaiseTemplateNeeded(ControlItem item)
		{
			this.OnTemplateNeeded(new RadMenuEventArgs((RadMenuItem)item));
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000B33E4 File Offset: 0x000B15E4
		protected virtual void RaiseItemClick(ControlItem item)
		{
			this.OnItemClick(new RadMenuEventArgs((RadMenuItem)item));
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000B33F7 File Offset: 0x000B15F7
		protected internal override void RaiseItemDataBound(ControlItem item)
		{
			this.OnItemDataBound(new RadMenuEventArgs((RadMenuItem)item));
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x000B340A File Offset: 0x000B160A
		protected internal override IRenderer CreateControlRenderer()
		{
			return RendererFactory.CreateMenuRenderer(this);
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000B3414 File Offset: 0x000B1614
		internal void CallBaseAddAttributesToRender(HtmlTextWriter writer)
		{
			short tabIndex = this.TabIndex;
			this.TabIndex = 0;
			writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, tabIndex.ToString());
			base.AddAttributesToRender(writer);
			this.TabIndex = tabIndex;
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000B344C File Offset: 0x000B164C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000B345A File Offset: 0x000B165A
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000B3470 File Offset: 0x000B1670
		protected override void ReadXmlForChildren(XmlReader reader)
		{
			do
			{
				reader.Read();
			}
			while (reader.NodeType == XmlNodeType.Comment);
			if (reader.NodeType == XmlNodeType.EndElement)
			{
				return;
			}
			RadMenuItemGroupSettings radMenuItemGroupSettings = new RadMenuItemGroupSettings();
			XmlPersister.Deserialize(radMenuItemGroupSettings, null, null, reader);
			this.DefaultGroupSettings.ExpandDirection = radMenuItemGroupSettings.ExpandDirection;
			this.DefaultGroupSettings.OffsetX = radMenuItemGroupSettings.OffsetX;
			this.DefaultGroupSettings.OffsetY = radMenuItemGroupSettings.OffsetY;
			this.DefaultGroupSettings.Flow = radMenuItemGroupSettings.Flow;
			if (radMenuItemGroupSettings.Width != Unit.Empty)
			{
				this.Width = radMenuItemGroupSettings.Width;
			}
			if (radMenuItemGroupSettings.Height != Unit.Empty)
			{
				this.Height = radMenuItemGroupSettings.Height;
			}
			base.ReadXmlForChildren(reader);
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000B352E File Offset: 0x000B172E
		protected override void LoadXml(ControlItemContainer deserialized)
		{
			base.LoadXml(deserialized);
			XmlPersister.MergeObjects(((RadMenu)deserialized).DefaultGroupSettings, this.DefaultGroupSettings);
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000B354D File Offset: 0x000B174D
		protected override void WriteXmlForChildren(XmlWriter writer)
		{
			writer.WriteStartElement("Group");
			this.DefaultGroupSettings.SerializeTo(writer);
			base.WriteXmlForChildren(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000B3574 File Offset: 0x000B1774
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
				this.LoadClientState(javaScriptSerializer.Deserialize<RadMenuClientState>(text));
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000B35D0 File Offset: 0x000B17D0
		private void LoadClientState(RadMenuClientState clientState)
		{
			if (clientState.LogEntries != null)
			{
				ClientStateLogPlayer<RadMenuItem> clientStateLogPlayer = new ClientStateLogPlayer<RadMenuItem>(this);
				this._clientChanges = clientStateLogPlayer.Play(clientState.LogEntries);
			}
			if (!string.IsNullOrEmpty(clientState.SelectedItemIndex))
			{
				RadMenuItem radMenuItem = (RadMenuItem)this.FindItemByHierarchicalIndex(clientState.SelectedItemIndex);
				if (radMenuItem != null)
				{
					radMenuItem.Selected = true;
				}
			}
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000B3627 File Offset: 0x000B1827
		void IPostBackEventHandler.RaisePostBackEvent(string nodeIndex)
		{
			this.RaisePostBackEvent(nodeIndex);
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000B3630 File Offset: 0x000B1830
		protected virtual void RaisePostBackEvent(string nodeIndex)
		{
			ControlItem controlItem = this.FindItemByHierarchicalIndex(nodeIndex);
			if (controlItem != null)
			{
				this._cachedClickedIndex = nodeIndex;
				this.RaiseItemClick(controlItem);
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06003654 RID: 13908 RVA: 0x000B3656 File Offset: 0x000B1856
		// (set) Token: 0x06003655 RID: 13909 RVA: 0x000B3674 File Offset: 0x000B1874
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
				RadMenuItem radMenuItem = this.FindItemByHierarchicalIndex(value) as RadMenuItem;
				if (radMenuItem != null)
				{
					radMenuItem.Selected = true;
				}
			}
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000B3698 File Offset: 0x000B1898
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<int>(descriptor, "autoScrollMinimumHeight", this.AutoScrollMinimumHeight, 50);
			base.DescribeProperty<int>(descriptor, "autoScrollMinimumWidth", this.AutoScrollMinimumWidth, 50);
			base.DescribeProperty<string>(descriptor, "_childListElementCssClass", this.ChildListElementCssClass, "");
			base.DescribeProperty<bool>(descriptor, "clickToOpen", this.ClickToOpen, false);
			base.DescribeProperty<int>(descriptor, "collapseDelay", this.CollapseDelay, 500);
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<bool>(descriptor, "enableAutoScroll", this.EnableAutoScroll, false);
			base.DescribeProperty<bool>(descriptor, "_enableItemImagesPreloading", this.EnableImagePreloading, false);
			base.DescribeProperty<bool>(descriptor, "enableImageSprites", this.EnableImageSprites, false);
			base.DescribeProperty<bool>(descriptor, "enableOverlay", this.EnableOverlay, true);
			base.DescribeProperty<bool>(descriptor, "enableRootItemScroll", this.EnableRootItemScroll, false);
			base.DescribeProperty<bool>(descriptor, "enableRoundedCorners", this.EnableRoundedCorners, false);
			base.DescribeProperty<bool>(descriptor, "enableScreenBoundaryDetection", this.EnableScreenBoundaryDetection, true);
			base.DescribeProperty<bool>(descriptor, "enableSelection", this.EnableSelection, true);
			base.DescribeProperty<bool>(descriptor, "enableShadows", this.EnableShadows, false);
			base.DescribeProperty<int>(descriptor, "expandDelay", this.ExpandDelay, 100);
			base.DescribeProperty<ItemFlow>(descriptor, "_flow", this.Flow, ItemFlow.Horizontal);
			base.DescribeProperty<bool>(descriptor, "persistLoadOnDemandItems", this.PersistLoadOnDemandItems, true);
			base.DescribeProperty<string>(descriptor, "_selectedValue", this.SelectedValue, "");
			base.DescribeProperty<bool>(descriptor, "showToggleHandle", this.ShowToggleHandle, false);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000B3838 File Offset: 0x000B1A38
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "itemBlur", this.OnClientItemBlur);
			RadDataBoundControl.DescribeEvent(descriptor, "itemClicked", this.OnClientItemClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "itemClicking", this.OnClientItemClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "itemClosed", this.OnClientItemClosed);
			RadDataBoundControl.DescribeEvent(descriptor, "itemClosing", this.OnClientItemClosing);
			RadDataBoundControl.DescribeEvent(descriptor, "itemFocus", this.OnClientItemFocus);
			RadDataBoundControl.DescribeEvent(descriptor, "itemOpened", this.OnClientItemOpened);
			RadDataBoundControl.DescribeEvent(descriptor, "itemOpening", this.OnClientItemOpening);
			RadDataBoundControl.DescribeEvent(descriptor, "itemPopulated", this.OnClientItemPopulated);
			RadDataBoundControl.DescribeEvent(descriptor, "itemPopulating", this.OnClientItemPopulating);
			RadDataBoundControl.DescribeEvent(descriptor, "itemPopulationFailed", this.OnClientItemPopulationFailed);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "menuPopulated", this.OnClientMenuPopulated);
			RadDataBoundControl.DescribeEvent(descriptor, "menuPopulating", this.OnClientMenuPopulating);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOut", this.OnClientMouseOut);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOver", this.OnClientMouseOver);
			RadDataBoundControl.DescribeEvent(descriptor, "templateDataBound", this.OnClientTemplateDataBound);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000B396D File Offset: 0x000B1B6D
		// Note: this type is marked as 'beforefieldinit'.
		static RadMenu()
		{
			RadMenu.TemplateNeededEvent = new object();
			RadMenu.ItemDataBoundEvent = new object();
			RadMenu.ItemClickEvent = new object();
			RadMenu.ItemCreatedEvent = new object();
		}

		// Token: 0x04000E85 RID: 3717
		private IList<ClientOperation<RadMenuItem>> _clientChanges = new List<ClientOperation<RadMenuItem>>();

		// Token: 0x04000E86 RID: 3718
		private WaiAriaSettings _ariaSettings;

		// Token: 0x04000E8B RID: 3723
		internal string _cachedClickedIndex = string.Empty;

		// Token: 0x04000E8C RID: 3724
		private readonly RadMenuItemGroupSettings _groupSettings;

		// Token: 0x04000E8D RID: 3725
		private readonly AnimationSettings _expandAnimation;

		// Token: 0x04000E8E RID: 3726
		private readonly AnimationSettings _collapseAnimation;

		// Token: 0x04000E8F RID: 3727
		private readonly NavigationControlWebServiceSettings _webServiceSettings;

		// Token: 0x020005D6 RID: 1494
		internal static class Styles
		{
			// Token: 0x04000E92 RID: 3730
			public const string RoundedCorners = "rmRoundedCorners";

			// Token: 0x04000E93 RID: 3731
			public const string Shadows = "rmShadows";

			// Token: 0x04000E94 RID: 3732
			public const string Popup = "rmPopup";

			// Token: 0x04000E95 RID: 3733
			public const string RootGroup = "rmRootGroup";

			// Token: 0x04000E96 RID: 3734
			public const string ToggleHandles = "rmToggleHandles";

			// Token: 0x04000E97 RID: 3735
			public const string RootScrollGroup = "rmRootScrollGroup";

			// Token: 0x04000E98 RID: 3736
			public const string Disabled = "rmDisabled";

			// Token: 0x04000E99 RID: 3737
			public const string Clicked = "rmClicked";

			// Token: 0x04000E9A RID: 3738
			public const string Expanded = "rmExpanded";

			// Token: 0x04000E9B RID: 3739
			public const string First = "rmFirst";

			// Token: 0x04000E9C RID: 3740
			public const string Focused = "rmFocused";

			// Token: 0x04000E9D RID: 3741
			public const string Selected = "rmSelected";

			// Token: 0x04000E9E RID: 3742
			public const string HorizontalFlow = "rmHorizontal";

			// Token: 0x04000E9F RID: 3743
			public const string Group = "rmGroup";

			// Token: 0x04000EA0 RID: 3744
			public const string Item = "rmItem";

			// Token: 0x04000EA1 RID: 3745
			public const string ParentItem = "rmParentItem";

			// Token: 0x04000EA2 RID: 3746
			public const string Last = "rmLast";

			// Token: 0x04000EA3 RID: 3747
			public const string LeftImage = "rmLeftImage";

			// Token: 0x04000EA4 RID: 3748
			public const string Level = "rmLevel";

			// Token: 0x04000EA5 RID: 3749
			public const string Link = "rmLink";

			// Token: 0x04000EA6 RID: 3750
			public const string ToggleButton = "rmToggle";

			// Token: 0x04000EA7 RID: 3751
			public const string ImageOnly = "rmImageOnly";

			// Token: 0x04000EA8 RID: 3752
			public const string RootLink = "rmRootLink";

			// Token: 0x04000EA9 RID: 3753
			public const string Separator = "rmSeparator";

			// Token: 0x04000EAA RID: 3754
			public const string ScrollWrap = "rmScrollWrap";

			// Token: 0x04000EAB RID: 3755
			public const string Slide = "rmSlide";

			// Token: 0x04000EAC RID: 3756
			public const string Templated = "rmTemplate";

			// Token: 0x04000EAD RID: 3757
			public const string Content = "rmContent";

			// Token: 0x04000EAE RID: 3758
			public const string ContentTemplate = "rmContentTemplate";

			// Token: 0x04000EAF RID: 3759
			public const string Text = "rmText";

			// Token: 0x04000EB0 RID: 3760
			public const string Icon = "rmIcon";

			// Token: 0x04000EB1 RID: 3761
			public const string VerticalFlow = "rmVertical";

			// Token: 0x04000EB2 RID: 3762
			public const string MultiColumn = "rmMultiColumn";

			// Token: 0x04000EB3 RID: 3763
			public const string GroupColumn = "rmGroupColumn";

			// Token: 0x04000EB4 RID: 3764
			public const string MultiGroup = "rmMultiGroup";

			// Token: 0x04000EB5 RID: 3765
			public const string FirstGroupColumn = "rmFirstGroupColumn";

			// Token: 0x04000EB6 RID: 3766
			public const string PopUpWrapper = "RadMenuPopup";

			// Token: 0x04000EB7 RID: 3767
			public const string RootToggleButton = "rmRootToggle";
		}
	}
}
