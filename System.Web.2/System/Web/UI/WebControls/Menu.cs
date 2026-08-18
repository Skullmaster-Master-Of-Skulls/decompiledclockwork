using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.WebControls.Adapters;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200046B RID: 1131
	[ControlValueProperty("SelectedValue")]
	[DefaultEvent("MenuItemClick")]
	[Designer("System.Web.UI.Design.WebControls.MenuDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class Menu : HierarchicalDataBoundControl, IPostBackEventHandler, INamingContainer
	{
		// Token: 0x060036EE RID: 14062 RVA: 0x000B1AEF File Offset: 0x000AFCEF
		public Menu()
		{
			this._nodeIndex = 0;
			this._maximumDepth = 0;
			this.IncludeStyleBlock = true;
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x060036EF RID: 14063 RVA: 0x000B1B0C File Offset: 0x000AFD0C
		// (set) Token: 0x060036F0 RID: 14064 RVA: 0x000B1B14 File Offset: 0x000AFD14
		internal bool AccessKeyRendered
		{
			get
			{
				return this._accessKeyRendered;
			}
			set
			{
				this._accessKeyRendered = value;
			}
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x000B1B1D File Offset: 0x000AFD1D
		private Collection<int> CachedLevelsContainingCssClass
		{
			get
			{
				if (this._cachedLevelsContainingCssClass == null)
				{
					this._cachedLevelsContainingCssClass = new Collection<int>();
				}
				return this._cachedLevelsContainingCssClass;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000B1B38 File Offset: 0x000AFD38
		private List<string> CachedMenuItemClassNames
		{
			get
			{
				if (this._cachedMenuItemClassNames == null)
				{
					this._cachedMenuItemClassNames = new List<string>();
				}
				return this._cachedMenuItemClassNames;
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x000B1B53 File Offset: 0x000AFD53
		private List<string> CachedMenuItemHyperLinkClassNames
		{
			get
			{
				if (this._cachedMenuItemHyperLinkClassNames == null)
				{
					this._cachedMenuItemHyperLinkClassNames = new List<string>();
				}
				return this._cachedMenuItemHyperLinkClassNames;
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x000B1B6E File Offset: 0x000AFD6E
		private List<MenuItemStyle> CachedMenuItemStyles
		{
			get
			{
				if (this._cachedMenuItemStyles == null)
				{
					this._cachedMenuItemStyles = new List<MenuItemStyle>();
				}
				return this._cachedMenuItemStyles;
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x000B1B89 File Offset: 0x000AFD89
		private List<string> CachedSubMenuClassNames
		{
			get
			{
				if (this._cachedSubMenuClassNames == null)
				{
					this._cachedSubMenuClassNames = new List<string>();
				}
				return this._cachedSubMenuClassNames;
			}
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000B1BA4 File Offset: 0x000AFDA4
		private List<SubMenuStyle> CachedSubMenuStyles
		{
			get
			{
				if (this._cachedSubMenuStyles == null)
				{
					this._cachedSubMenuStyles = new List<SubMenuStyle>();
				}
				return this._cachedSubMenuStyles;
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x000B1BBF File Offset: 0x000AFDBF
		internal string ClientDataObjectID
		{
			get
			{
				return this.ClientID + "_Data";
			}
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000856CA File Offset: 0x000838CA
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x060036F9 RID: 14073 RVA: 0x000B1BD1 File Offset: 0x000AFDD1
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.MenuBindingsEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[WebSysDescription("Menu_Bindings")]
		public MenuItemBindingCollection DataBindings
		{
			get
			{
				if (this._bindings == null)
				{
					this._bindings = new MenuItemBindingCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._bindings).TrackViewState();
					}
				}
				return this._bindings;
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x000B1C00 File Offset: 0x000AFE00
		// (set) Token: 0x060036FB RID: 14075 RVA: 0x000B1C2D File Offset: 0x000AFE2D
		[WebCategory("Behavior")]
		[DefaultValue(500)]
		[WebSysDescription("Menu_DisappearAfter")]
		[Themeable(false)]
		public int DisappearAfter
		{
			get
			{
				object obj = this.ViewState["DisappearAfter"];
				if (obj == null)
				{
					return 500;
				}
				return (int)obj;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DisappearAfter"] = value;
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x000B1C54 File Offset: 0x000AFE54
		// (set) Token: 0x060036FD RID: 14077 RVA: 0x000B1C81 File Offset: 0x000AFE81
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(true)]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_DynamicBottomSeparatorImageUrl")]
		public string DynamicBottomSeparatorImageUrl
		{
			get
			{
				object obj = this.ViewState["DynamicBottomSeparatorImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["DynamicBottomSeparatorImageUrl"] = value;
			}
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000B1C94 File Offset: 0x000AFE94
		// (set) Token: 0x060036FF RID: 14079 RVA: 0x000B1CBD File Offset: 0x000AFEBD
		[DefaultValue(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_DynamicDisplayPopOutImage")]
		public bool DynamicEnableDefaultPopOutImage
		{
			get
			{
				object obj = this.ViewState["DynamicEnableDefaultPopOutImage"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["DynamicEnableDefaultPopOutImage"] = value;
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x000B1CD8 File Offset: 0x000AFED8
		// (set) Token: 0x06003701 RID: 14081 RVA: 0x000B1D01 File Offset: 0x000AFF01
		[DefaultValue(0)]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_DynamicHorizontalOffset")]
		public int DynamicHorizontalOffset
		{
			get
			{
				object obj = this.ViewState["DynamicHorizontalOffset"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["DynamicHorizontalOffset"] = value;
			}
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06003702 RID: 14082 RVA: 0x000B1D19 File Offset: 0x000AFF19
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("Menu_DynamicHoverStyle")]
		public Style DynamicHoverStyle
		{
			get
			{
				if (this._dynamicHoverStyle == null)
				{
					this._dynamicHoverStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._dynamicHoverStyle).TrackViewState();
					}
				}
				return this._dynamicHoverStyle;
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06003703 RID: 14083 RVA: 0x000B1D48 File Offset: 0x000AFF48
		// (set) Token: 0x06003704 RID: 14084 RVA: 0x000B1D75 File Offset: 0x000AFF75
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_DynamicItemFormatString")]
		public string DynamicItemFormatString
		{
			get
			{
				object obj = this.ViewState["DynamicItemFormatString"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["DynamicItemFormatString"] = value;
			}
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000B1D88 File Offset: 0x000AFF88
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Menu_DynamicMenuItemStyle")]
		public MenuItemStyle DynamicMenuItemStyle
		{
			get
			{
				if (this._dynamicItemStyle == null)
				{
					this._dynamicItemStyle = new MenuItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._dynamicItemStyle).TrackViewState();
					}
				}
				return this._dynamicItemStyle;
			}
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x000B1DB6 File Offset: 0x000AFFB6
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Menu_DynamicMenuStyle")]
		public SubMenuStyle DynamicMenuStyle
		{
			get
			{
				if (this._dynamicMenuStyle == null)
				{
					this._dynamicMenuStyle = new SubMenuStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._dynamicMenuStyle).TrackViewState();
					}
				}
				return this._dynamicMenuStyle;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06003707 RID: 14087 RVA: 0x000B1DE4 File Offset: 0x000AFFE4
		// (set) Token: 0x06003708 RID: 14088 RVA: 0x000B1E11 File Offset: 0x000B0011
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_DynamicPopoutImageUrl")]
		public string DynamicPopOutImageUrl
		{
			get
			{
				object obj = this.ViewState["DynamicPopOutImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["DynamicPopOutImageUrl"] = value;
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06003709 RID: 14089 RVA: 0x000B1E24 File Offset: 0x000B0024
		// (set) Token: 0x0600370A RID: 14090 RVA: 0x000B1E56 File Offset: 0x000B0056
		[WebSysDefaultValue("MenuAdapter_Expand")]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_DynamicPopoutImageText")]
		public string DynamicPopOutImageTextFormatString
		{
			get
			{
				object obj = this.ViewState["DynamicPopOutImageTextFormatString"];
				if (obj == null)
				{
					return SR.GetString("MenuAdapter_Expand");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["DynamicPopOutImageTextFormatString"] = value;
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x0600370B RID: 14091 RVA: 0x000B1E69 File Offset: 0x000B0069
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Menu_DynamicSelectedStyle")]
		public MenuItemStyle DynamicSelectedStyle
		{
			get
			{
				if (this._dynamicSelectedStyle == null)
				{
					this._dynamicSelectedStyle = new MenuItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._dynamicSelectedStyle).TrackViewState();
					}
				}
				return this._dynamicSelectedStyle;
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x000B1E97 File Offset: 0x000B0097
		// (set) Token: 0x0600370D RID: 14093 RVA: 0x000B1E9F File Offset: 0x000B009F
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(MenuItemTemplateContainer))]
		[WebSysDescription("Menu_DynamicTemplate")]
		public ITemplate DynamicItemTemplate
		{
			get
			{
				return this._dynamicTemplate;
			}
			set
			{
				this._dynamicTemplate = value;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x0600370E RID: 14094 RVA: 0x000B1EA8 File Offset: 0x000B00A8
		// (set) Token: 0x0600370F RID: 14095 RVA: 0x000B1ED5 File Offset: 0x000B00D5
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_DynamicTopSeparatorImageUrl")]
		public string DynamicTopSeparatorImageUrl
		{
			get
			{
				object obj = this.ViewState["DynamicTopSeparatorImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["DynamicTopSeparatorImageUrl"] = value;
			}
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06003710 RID: 14096 RVA: 0x000B1EE8 File Offset: 0x000B00E8
		// (set) Token: 0x06003711 RID: 14097 RVA: 0x000B1F11 File Offset: 0x000B0111
		[DefaultValue(0)]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_DynamicVerticalOffset")]
		public int DynamicVerticalOffset
		{
			get
			{
				object obj = this.ViewState["DynamicVerticalOffset"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["DynamicVerticalOffset"] = value;
			}
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06003712 RID: 14098 RVA: 0x000B1F29 File Offset: 0x000B0129
		private string[] ImageUrls
		{
			get
			{
				if (this._imageUrls == null)
				{
					this._imageUrls = new string[3];
				}
				return this._imageUrls;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06003713 RID: 14099 RVA: 0x000B1F45 File Offset: 0x000B0145
		// (set) Token: 0x06003714 RID: 14100 RVA: 0x000B1F4D File Offset: 0x000B014D
		[DefaultValue(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_IncludeStyleBlock")]
		public bool IncludeStyleBlock { get; set; }

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06003715 RID: 14101 RVA: 0x000B1F56 File Offset: 0x000B0156
		internal bool IsNotIE
		{
			get
			{
				return this._isNotIE;
			}
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06003716 RID: 14102 RVA: 0x000B1F5E File Offset: 0x000B015E
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.MenuItemCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[WebSysDescription("Menu_Items")]
		public MenuItemCollection Items
		{
			get
			{
				return this.RootItem.ChildItems;
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06003717 RID: 14103 RVA: 0x000B1F6C File Offset: 0x000B016C
		// (set) Token: 0x06003718 RID: 14104 RVA: 0x000B1F95 File Offset: 0x000B0195
		[DefaultValue(false)]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_ItemWrap")]
		public bool ItemWrap
		{
			get
			{
				object obj = this.ViewState["ItemWrap"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ItemWrap"] = value;
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06003719 RID: 14105 RVA: 0x000B1FAD File Offset: 0x000B01AD
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.MenuItemStyleCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("Menu_LevelMenuItemStyles")]
		public MenuItemStyleCollection LevelMenuItemStyles
		{
			get
			{
				if (this._levelMenuItemStyles == null)
				{
					this._levelMenuItemStyles = new MenuItemStyleCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._levelMenuItemStyles).TrackViewState();
					}
				}
				return this._levelMenuItemStyles;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x0600371A RID: 14106 RVA: 0x000B1FDB File Offset: 0x000B01DB
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.MenuItemStyleCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("Menu_LevelSelectedStyles")]
		public MenuItemStyleCollection LevelSelectedStyles
		{
			get
			{
				if (this._levelSelectedStyles == null)
				{
					this._levelSelectedStyles = new MenuItemStyleCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._levelSelectedStyles).TrackViewState();
					}
				}
				return this._levelSelectedStyles;
			}
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x0600371B RID: 14107 RVA: 0x000B2009 File Offset: 0x000B0209
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.SubMenuStyleCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("Menu_LevelSubMenuStyles")]
		public SubMenuStyleCollection LevelSubMenuStyles
		{
			get
			{
				if (this._levelStyles == null)
				{
					this._levelStyles = new SubMenuStyleCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._levelStyles).TrackViewState();
					}
				}
				return this._levelStyles;
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x0600371C RID: 14108 RVA: 0x000B2038 File Offset: 0x000B0238
		internal int MaximumDepth
		{
			get
			{
				if (this._maximumDepth > 0)
				{
					return this._maximumDepth;
				}
				this._maximumDepth = this.MaximumDynamicDisplayLevels + this.StaticDisplayLevels;
				if (this._maximumDepth < this.MaximumDynamicDisplayLevels || this._maximumDepth < this.StaticDisplayLevels)
				{
					this._maximumDepth = int.MaxValue;
				}
				return this._maximumDepth;
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x0600371D RID: 14109 RVA: 0x000B2098 File Offset: 0x000B0298
		// (set) Token: 0x0600371E RID: 14110 RVA: 0x000B20C4 File Offset: 0x000B02C4
		[WebCategory("Behavior")]
		[DefaultValue(3)]
		[Themeable(true)]
		[WebSysDescription("Menu_MaximumDynamicDisplayLevels")]
		public int MaximumDynamicDisplayLevels
		{
			get
			{
				object obj = this.ViewState["MaximumDynamicDisplayLevels"];
				if (obj == null)
				{
					return 3;
				}
				return (int)obj;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("MaximumDynamicDisplayLevels", SR.GetString("Menu_MaximumDynamicDisplayLevelsInvalid"));
				}
				this.ViewState["MaximumDynamicDisplayLevels"] = value;
				this._maximumDepth = 0;
				if (this._dataBound)
				{
					this._dataBound = false;
					this.PerformDataBinding();
				}
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x0600371F RID: 14111 RVA: 0x000B211C File Offset: 0x000B031C
		// (set) Token: 0x06003720 RID: 14112 RVA: 0x000B2145 File Offset: 0x000B0345
		[WebCategory("Layout")]
		[DefaultValue(Orientation.Vertical)]
		[WebSysDescription("Menu_Orientation")]
		public Orientation Orientation
		{
			get
			{
				object obj = this.ViewState["Orientation"];
				if (obj == null)
				{
					return Orientation.Vertical;
				}
				return (Orientation)obj;
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x000B215D File Offset: 0x000B035D
		internal PopOutPanel Panel
		{
			get
			{
				if (this._panel == null)
				{
					this._panel = new PopOutPanel(this, this._panelStyle);
					if (!base.DesignMode)
					{
						this._panel.Page = this.Page;
					}
				}
				return this._panel;
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06003722 RID: 14114 RVA: 0x000B2198 File Offset: 0x000B0398
		// (set) Token: 0x06003723 RID: 14115 RVA: 0x000B21C4 File Offset: 0x000B03C4
		[DefaultValue('/')]
		[WebSysDescription("Menu_PathSeparator")]
		public char PathSeparator
		{
			get
			{
				object obj = this.ViewState["PathSeparator"];
				if (obj == null)
				{
					return '/';
				}
				return (char)obj;
			}
			set
			{
				if (value == '\0')
				{
					this.ViewState["PathSeparator"] = null;
				}
				else
				{
					this.ViewState["PathSeparator"] = value;
				}
				foreach (object obj in this.Items)
				{
					MenuItem menuItem = (MenuItem)obj;
					menuItem.ResetValuePathRecursive();
				}
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06003724 RID: 14116 RVA: 0x000B2248 File Offset: 0x000B0448
		internal string PopoutImageUrlInternal
		{
			get
			{
				if (this._cachedPopOutImageUrl != null)
				{
					return this._cachedPopOutImageUrl;
				}
				this._cachedPopOutImageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(Menu), "Menu_Popout.gif");
				return this._cachedPopOutImageUrl;
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x000B2284 File Offset: 0x000B0484
		private Menu.MenuRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					switch (this.RenderingMode)
					{
					case MenuRenderingMode.Default:
						if (this.RenderingCompatibility < VersionUtil.Framework40)
						{
							this._renderer = new Menu.MenuRendererClassic(this);
						}
						else
						{
							this._renderer = new Menu.MenuRendererStandards(this);
						}
						break;
					case MenuRenderingMode.Table:
						this._renderer = new Menu.MenuRendererClassic(this);
						break;
					case MenuRenderingMode.List:
						this._renderer = new Menu.MenuRendererStandards(this);
						break;
					}
				}
				return this._renderer;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06003726 RID: 14118 RVA: 0x000B2302 File Offset: 0x000B0502
		// (set) Token: 0x06003727 RID: 14119 RVA: 0x000B230A File Offset: 0x000B050A
		[WebCategory("Layout")]
		[DefaultValue(MenuRenderingMode.Default)]
		[WebSysDescription("Menu_RenderingMode")]
		public MenuRenderingMode RenderingMode
		{
			get
			{
				return this._renderingMode;
			}
			set
			{
				if (value < MenuRenderingMode.Default || value > MenuRenderingMode.List)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this._renderer != null)
				{
					throw new InvalidOperationException(SR.GetString("Menu_CannotChangeRenderingMode"));
				}
				this._renderingMode = value;
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06003728 RID: 14120 RVA: 0x000B233E File Offset: 0x000B053E
		internal MenuItem RootItem
		{
			get
			{
				if (this._rootItem == null)
				{
					this._rootItem = new MenuItem(this, true);
				}
				return this._rootItem;
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06003729 RID: 14121 RVA: 0x000B235B File Offset: 0x000B055B
		internal Style RootMenuItemStyle
		{
			get
			{
				this.EnsureRootMenuStyle();
				return this._rootMenuItemStyle;
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x000B236C File Offset: 0x000B056C
		// (set) Token: 0x0600372B RID: 14123 RVA: 0x000B2399 File Offset: 0x000B0599
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_ScrollDownImageUrl")]
		public string ScrollDownImageUrl
		{
			get
			{
				object obj = this.ViewState["ScrollDownImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ScrollDownImageUrl"] = value;
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x000B23AC File Offset: 0x000B05AC
		internal string ScrollDownImageUrlInternal
		{
			get
			{
				if (this._cachedScrollDownImageUrl != null)
				{
					return this._cachedScrollDownImageUrl;
				}
				this._cachedScrollDownImageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(Menu), "Menu_ScrollDown.gif");
				return this._cachedScrollDownImageUrl;
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x000B23E8 File Offset: 0x000B05E8
		// (set) Token: 0x0600372E RID: 14126 RVA: 0x000B241A File Offset: 0x000B061A
		[WebSysDefaultValue("Menu_ScrollDown")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_ScrollDownText")]
		public string ScrollDownText
		{
			get
			{
				object obj = this.ViewState["ScrollDownText"];
				if (obj == null)
				{
					return SR.GetString("Menu_ScrollDown");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ScrollDownText"] = value;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x000B2430 File Offset: 0x000B0630
		// (set) Token: 0x06003730 RID: 14128 RVA: 0x000B245D File Offset: 0x000B065D
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_ScrollUpImageUrl")]
		public string ScrollUpImageUrl
		{
			get
			{
				object obj = this.ViewState["ScrollUpImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ScrollUpImageUrl"] = value;
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x000B2470 File Offset: 0x000B0670
		internal string ScrollUpImageUrlInternal
		{
			get
			{
				if (this._cachedScrollUpImageUrl != null)
				{
					return this._cachedScrollUpImageUrl;
				}
				this._cachedScrollUpImageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(Menu), "Menu_ScrollUp.gif");
				return this._cachedScrollUpImageUrl;
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06003732 RID: 14130 RVA: 0x000B24AC File Offset: 0x000B06AC
		// (set) Token: 0x06003733 RID: 14131 RVA: 0x000B24DE File Offset: 0x000B06DE
		[WebSysDefaultValue("Menu_ScrollUp")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_ScrollUpText")]
		public string ScrollUpText
		{
			get
			{
				object obj = this.ViewState["ScrollUpText"];
				if (obj == null)
				{
					return SR.GetString("Menu_ScrollUp");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ScrollUpText"] = value;
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06003734 RID: 14132 RVA: 0x000B24F1 File Offset: 0x000B06F1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MenuItem SelectedItem
		{
			get
			{
				return this._selectedItem;
			}
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x000B24F9 File Offset: 0x000B06F9
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string SelectedValue
		{
			get
			{
				if (this.SelectedItem != null)
				{
					return this.SelectedItem.Value;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06003736 RID: 14134 RVA: 0x000B2514 File Offset: 0x000B0714
		// (set) Token: 0x06003737 RID: 14135 RVA: 0x000B2546 File Offset: 0x000B0746
		[WebSysDefaultValue("Menu_SkipLinkTextDefault")]
		[Localizable(true)]
		[WebCategory("Accessibility")]
		[WebSysDescription("WebControl_SkipLinkText")]
		public string SkipLinkText
		{
			get
			{
				object obj = this.ViewState["SkipLinkText"];
				if (obj == null)
				{
					return SR.GetString("Menu_SkipLinkTextDefault");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["SkipLinkText"] = value;
			}
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x000B255C File Offset: 0x000B075C
		// (set) Token: 0x06003739 RID: 14137 RVA: 0x000B2589 File Offset: 0x000B0789
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_StaticBottomSeparatorImageUrl")]
		public string StaticBottomSeparatorImageUrl
		{
			get
			{
				object obj = this.ViewState["StaticBottomSeparatorImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StaticBottomSeparatorImageUrl"] = value;
			}
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x0600373A RID: 14138 RVA: 0x000B259C File Offset: 0x000B079C
		// (set) Token: 0x0600373B RID: 14139 RVA: 0x000B25C8 File Offset: 0x000B07C8
		[WebCategory("Behavior")]
		[DefaultValue(1)]
		[Themeable(true)]
		[WebSysDescription("Menu_StaticDisplayLevels")]
		public int StaticDisplayLevels
		{
			get
			{
				object obj = this.ViewState["StaticDisplayLevels"];
				if (obj == null)
				{
					return 1;
				}
				return (int)obj;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["StaticDisplayLevels"] = value;
				this._maximumDepth = 0;
				if (this._dataBound && !base.DesignMode)
				{
					this._dataBound = false;
					this.PerformDataBinding();
				}
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x0600373C RID: 14140 RVA: 0x000B2620 File Offset: 0x000B0820
		// (set) Token: 0x0600373D RID: 14141 RVA: 0x000B2649 File Offset: 0x000B0849
		[DefaultValue(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_StaticDisplayPopOutImage")]
		public bool StaticEnableDefaultPopOutImage
		{
			get
			{
				object obj = this.ViewState["StaticEnableDefaultPopOutImage"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["StaticEnableDefaultPopOutImage"] = value;
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x0600373E RID: 14142 RVA: 0x000B2661 File Offset: 0x000B0861
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("Menu_StaticHoverStyle")]
		public Style StaticHoverStyle
		{
			get
			{
				if (this._staticHoverStyle == null)
				{
					this._staticHoverStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._staticHoverStyle).TrackViewState();
					}
				}
				return this._staticHoverStyle;
			}
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x0600373F RID: 14143 RVA: 0x000B2690 File Offset: 0x000B0890
		// (set) Token: 0x06003740 RID: 14144 RVA: 0x000B26BD File Offset: 0x000B08BD
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_StaticItemFormatString")]
		public string StaticItemFormatString
		{
			get
			{
				object obj = this.ViewState["StaticItemFormatString"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StaticItemFormatString"] = value;
			}
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000B26D0 File Offset: 0x000B08D0
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Menu_StaticMenuItemStyle")]
		public MenuItemStyle StaticMenuItemStyle
		{
			get
			{
				if (this._staticItemStyle == null)
				{
					this._staticItemStyle = new MenuItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._staticItemStyle).TrackViewState();
					}
				}
				return this._staticItemStyle;
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x000B26FE File Offset: 0x000B08FE
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Menu_StaticMenuStyle")]
		public SubMenuStyle StaticMenuStyle
		{
			get
			{
				if (this._staticMenuStyle == null)
				{
					this._staticMenuStyle = new SubMenuStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._staticMenuStyle).TrackViewState();
					}
				}
				return this._staticMenuStyle;
			}
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06003743 RID: 14147 RVA: 0x000B272C File Offset: 0x000B092C
		// (set) Token: 0x06003744 RID: 14148 RVA: 0x000B2759 File Offset: 0x000B0959
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_StaticPopoutImageUrl")]
		public string StaticPopOutImageUrl
		{
			get
			{
				object obj = this.ViewState["StaticPopOutImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StaticPopOutImageUrl"] = value;
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06003745 RID: 14149 RVA: 0x000B276C File Offset: 0x000B096C
		// (set) Token: 0x06003746 RID: 14150 RVA: 0x000B279E File Offset: 0x000B099E
		[WebSysDefaultValue("MenuAdapter_Expand")]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_StaticPopoutImageText")]
		public string StaticPopOutImageTextFormatString
		{
			get
			{
				object obj = this.ViewState["StaticPopOutImageTextFormatString"];
				if (obj == null)
				{
					return SR.GetString("MenuAdapter_Expand");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StaticPopOutImageTextFormatString"] = value;
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06003747 RID: 14151 RVA: 0x000B27B1 File Offset: 0x000B09B1
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Menu_StaticSelectedStyle")]
		public MenuItemStyle StaticSelectedStyle
		{
			get
			{
				if (this._staticSelectedStyle == null)
				{
					this._staticSelectedStyle = new MenuItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._staticSelectedStyle).TrackViewState();
					}
				}
				return this._staticSelectedStyle;
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x000B27E0 File Offset: 0x000B09E0
		// (set) Token: 0x06003749 RID: 14153 RVA: 0x000B280D File Offset: 0x000B0A0D
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Unit), "")]
		[Themeable(true)]
		[WebSysDescription("Menu_StaticSubMenuIndent")]
		public Unit StaticSubMenuIndent
		{
			get
			{
				object obj = this.ViewState["StaticSubMenuIndent"];
				if (obj == null)
				{
					return Unit.Empty;
				}
				return (Unit)obj;
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["StaticSubMenuIndent"] = value;
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x000B2842 File Offset: 0x000B0A42
		// (set) Token: 0x0600374B RID: 14155 RVA: 0x000B284A File Offset: 0x000B0A4A
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(MenuItemTemplateContainer))]
		[WebSysDescription("Menu_StaticTemplate")]
		public ITemplate StaticItemTemplate
		{
			get
			{
				return this._staticTemplate;
			}
			set
			{
				this._staticTemplate = value;
			}
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x000B2854 File Offset: 0x000B0A54
		// (set) Token: 0x0600374D RID: 14157 RVA: 0x000B2881 File Offset: 0x000B0A81
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("Menu_StaticTopSeparatorImageUrl")]
		public string StaticTopSeparatorImageUrl
		{
			get
			{
				object obj = this.ViewState["StaticTopSeparatorImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StaticTopSeparatorImageUrl"] = value;
			}
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x000B2894 File Offset: 0x000B0A94
		// (set) Token: 0x0600374F RID: 14159 RVA: 0x000835A9 File Offset: 0x000817A9
		[DefaultValue("")]
		[WebSysDescription("MenuItem_Target")]
		public string Target
		{
			get
			{
				object obj = this.ViewState["Target"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x0008BDAD File Offset: 0x00089FAD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06003751 RID: 14161 RVA: 0x000B28C1 File Offset: 0x000B0AC1
		// (remove) Token: 0x06003752 RID: 14162 RVA: 0x000B28D4 File Offset: 0x000B0AD4
		[WebCategory("Behavior")]
		[WebSysDescription("Menu_MenuItemClick")]
		public event MenuEventHandler MenuItemClick
		{
			add
			{
				base.Events.AddHandler(Menu._menuItemClickedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Menu._menuItemClickedEvent, value);
			}
		}

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x06003753 RID: 14163 RVA: 0x000B28E7 File Offset: 0x000B0AE7
		// (remove) Token: 0x06003754 RID: 14164 RVA: 0x000B28FA File Offset: 0x000B0AFA
		[WebCategory("Behavior")]
		[WebSysDescription("Menu_MenuItemDataBound")]
		public event MenuEventHandler MenuItemDataBound
		{
			add
			{
				base.Events.AddHandler(Menu._menuItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Menu._menuItemDataBoundEvent, value);
			}
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000B2910 File Offset: 0x000B0B10
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.VerifyRenderingInServerForm();
			string accessKey = this.AccessKey;
			try
			{
				this.AccessKey = string.Empty;
				base.AddAttributesToRender(writer);
			}
			finally
			{
				this.AccessKey = accessKey;
			}
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000B2958 File Offset: 0x000B0B58
		private static bool AppendCssClassName(StringBuilder builder, MenuItemStyle style, bool hyperlink)
		{
			bool result = false;
			if (style != null)
			{
				if (style.CssClass.Length != 0)
				{
					builder.Append(style.CssClass);
					builder.Append(' ');
					result = true;
				}
				string text = hyperlink ? style.HyperLinkStyle.RegisteredCssClass : style.RegisteredCssClass;
				if (text.Length > 0)
				{
					builder.Append(text);
					builder.Append(' ');
				}
			}
			return result;
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000B29C4 File Offset: 0x000B0BC4
		private static void AppendMenuCssClassName(StringBuilder builder, SubMenuStyle style)
		{
			if (style != null)
			{
				if (style.CssClass.Length != 0)
				{
					builder.Append(style.CssClass);
					builder.Append(' ');
				}
				string registeredCssClass = style.RegisteredCssClass;
				if (registeredCssClass.Length > 0)
				{
					builder.Append(registeredCssClass);
					builder.Append(' ');
				}
			}
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000B2A18 File Offset: 0x000B0C18
		private static T CacheGetItem<T>(List<T> cacheList, int index) where T : class
		{
			if (index < cacheList.Count)
			{
				return cacheList[index];
			}
			return default(T);
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000B2A40 File Offset: 0x000B0C40
		private static void CacheSetItem<T>(List<T> cacheList, int index, T item) where T : class
		{
			if (cacheList.Count > index)
			{
				cacheList[index] = item;
				return;
			}
			for (int i = cacheList.Count; i < index; i++)
			{
				cacheList.Add(default(T));
			}
			cacheList.Add(item);
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000B2A88 File Offset: 0x000B0C88
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			if (this.StaticItemTemplate != null || this.DynamicItemTemplate != null)
			{
				if (base.RequiresDataBinding && (!string.IsNullOrEmpty(this.DataSourceID) || this.DataSource != null))
				{
					this.EnsureDataBound();
					return;
				}
				this.CreateChildControlsFromItems(false);
				base.ClearChildViewState();
			}
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000B2AE4 File Offset: 0x000B0CE4
		private void CreateChildControlsFromItems(bool dataBinding)
		{
			if (this.StaticItemTemplate != null || this.DynamicItemTemplate != null)
			{
				int num = 0;
				foreach (object obj in this.Items)
				{
					MenuItem item = (MenuItem)obj;
					this.CreateTemplatedControls(this.StaticItemTemplate, item, num++, 0, dataBinding);
				}
			}
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000B2B5C File Offset: 0x000B0D5C
		internal int CreateItemIndex()
		{
			int nodeIndex = this._nodeIndex;
			this._nodeIndex = nodeIndex + 1;
			return nodeIndex;
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000B2B7C File Offset: 0x000B0D7C
		private void CreateTemplatedControls(ITemplate template, MenuItem item, int position, int depth, bool dataBinding)
		{
			if (template != null)
			{
				MenuItemTemplateContainer menuItemTemplateContainer = new MenuItemTemplateContainer(position, item);
				item.Container = menuItemTemplateContainer;
				template.InstantiateIn(menuItemTemplateContainer);
				this.Controls.Add(menuItemTemplateContainer);
				if (dataBinding)
				{
					menuItemTemplateContainer.DataBind();
				}
			}
			int num = 0;
			foreach (object obj in item.ChildItems)
			{
				MenuItem item2 = (MenuItem)obj;
				int num2 = depth + 1;
				if (template == this.DynamicItemTemplate)
				{
					this.CreateTemplatedControls(this.DynamicItemTemplate, item2, num++, num2, dataBinding);
				}
				else if (num2 < this.StaticDisplayLevels)
				{
					this.CreateTemplatedControls(template, item2, num++, num2, dataBinding);
				}
				else if (this.DynamicItemTemplate != null)
				{
					this.CreateTemplatedControls(this.DynamicItemTemplate, item2, num++, num2, dataBinding);
				}
			}
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x0009C00D File Offset: 0x0009A20D
		public sealed override void DataBind()
		{
			base.DataBind();
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x000B2C64 File Offset: 0x000B0E64
		private void DataBindItem(MenuItem item)
		{
			HierarchicalDataSourceView data = this.GetData(item.DataPath);
			if (!base.IsBoundUsingDataSourceID && this.DataSource == null)
			{
				return;
			}
			if (data == null)
			{
				throw new InvalidOperationException(SR.GetString("Menu_DataSourceReturnedNullView", new object[]
				{
					this.ID
				}));
			}
			IHierarchicalEnumerable hierarchicalEnumerable = data.Select();
			item.ChildItems.Clear();
			if (hierarchicalEnumerable != null)
			{
				if (base.IsBoundUsingDataSourceID)
				{
					SiteMapDataSource siteMapDataSource = this.GetDataSource() as SiteMapDataSource;
					if (siteMapDataSource != null)
					{
						SiteMapNode currentNode = siteMapDataSource.Provider.CurrentNode;
						if (currentNode != null)
						{
							this._currentSiteMapNodeUrl = currentNode.Url;
						}
					}
				}
				try
				{
					this.DataBindRecursive(item, hierarchicalEnumerable);
				}
				finally
				{
					this._currentSiteMapNodeUrl = null;
				}
			}
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x000B2D1C File Offset: 0x000B0F1C
		private void DataBindRecursive(MenuItem node, IHierarchicalEnumerable enumerable)
		{
			int num = node.Depth + 1;
			if (this.MaximumDynamicDisplayLevels != -1 && num >= this.MaximumDepth)
			{
				return;
			}
			foreach (object obj in enumerable)
			{
				IHierarchyData hierarchyData = enumerable.GetHierarchyData(obj);
				string text = null;
				string text2 = null;
				string text3 = string.Empty;
				string text4 = string.Empty;
				string text5 = string.Empty;
				string text6 = string.Empty;
				string text7 = string.Empty;
				bool enabled = true;
				bool flag = false;
				bool selectable = true;
				bool flag2 = false;
				string text8 = string.Empty;
				string dataMember = string.Empty;
				dataMember = hierarchyData.Type;
				MenuItemBinding binding = this.DataBindings.GetBinding(dataMember, num);
				if (binding != null)
				{
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
					string textField = binding.TextField;
					if (textField.Length > 0)
					{
						PropertyDescriptor propertyDescriptor = properties.Find(textField, true);
						if (propertyDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("Menu_InvalidDataBinding", new object[]
							{
								textField,
								"TextField"
							}));
						}
						object value = propertyDescriptor.GetValue(obj);
						if (value != null)
						{
							if (binding.FormatString.Length > 0)
							{
								text = string.Format(CultureInfo.CurrentCulture, binding.FormatString, new object[]
								{
									value
								});
							}
							else
							{
								text = value.ToString();
							}
						}
					}
					if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(binding.Text))
					{
						text = binding.Text;
					}
					string valueField = binding.ValueField;
					if (valueField.Length > 0)
					{
						PropertyDescriptor propertyDescriptor2 = properties.Find(valueField, true);
						if (propertyDescriptor2 == null)
						{
							throw new InvalidOperationException(SR.GetString("Menu_InvalidDataBinding", new object[]
							{
								valueField,
								"ValueField"
							}));
						}
						object value2 = propertyDescriptor2.GetValue(obj);
						if (value2 != null)
						{
							text2 = value2.ToString();
						}
					}
					if (string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(binding.Value))
					{
						text2 = binding.Value;
					}
					string targetField = binding.TargetField;
					if (targetField.Length > 0)
					{
						PropertyDescriptor propertyDescriptor3 = properties.Find(targetField, true);
						if (propertyDescriptor3 == null)
						{
							throw new InvalidOperationException(SR.GetString("Menu_InvalidDataBinding", new object[]
							{
								targetField,
								"TargetField"
							}));
						}
						object value3 = propertyDescriptor3.GetValue(obj);
						if (value3 != null)
						{
							text7 = value3.ToString();
						}
					}
					if (string.IsNullOrEmpty(text7))
					{
						text7 = binding.Target;
					}
					string imageUrlField = binding.ImageUrlField;
					if (imageUrlField.Length > 0)
					{
						PropertyDescriptor propertyDescriptor4 = properties.Find(imageUrlField, true);
						if (propertyDescriptor4 == null)
						{
							throw new InvalidOperationException(SR.GetString("Menu_InvalidDataBinding", new object[]
							{
								imageUrlField,
								"ImageUrlField"
							}));
						}
						object value4 = propertyDescriptor4.GetValue(obj);
						if (value4 != null)
						{
							text4 = value4.ToString();
						}
					}
					if (string.IsNullOrEmpty(text4))
					{
						text4 = binding.ImageUrl;
					}
					string navigateUrlField = binding.NavigateUrlField;
					if (navigateUrlField.Length > 0)
					{
						PropertyDescriptor propertyDescriptor5 = properties.Find(navigateUrlField, true);
						if (propertyDescriptor5 == null)
						{
							throw new InvalidOperationException(SR.GetString("Menu_InvalidDataBinding", new object[]
							{
								navigateUrlField,
								"NavigateUrlField"
							}));
						}
						object value5 = propertyDescriptor5.GetValue(obj);
						if (value5 != null)
						{
							text3 = value5.ToString();
						}
					}
					if (string.IsNullOrEmpty(text3))
					{
						text3 = binding.NavigateUrl;
					}
					string popOutImageUrlField = binding.PopOutImageUrlField;
					if (popOutImageUrlField.Length > 0)
					{
						PropertyDescriptor propertyDescriptor6 = properties.Find(popOutImageUrlField, true);
						if (propertyDescriptor6 == null)
						{
							throw new InvalidOperationException(SR.GetString("Menu_InvalidDataBinding", new object[]
							{
								popOutImageUrlField,
								"PopOutImageUrlField"
							}));
						}
						object value6 = propertyDescriptor6.GetValue(obj);
						if (value6 != null)
						{
							text5 = value6.ToString();
						}
					}
					if (string.IsNullOrEmpty(text5))
					{
						text5 = binding.PopOutImageUrl;
					}
					string separatorImageUrlField = binding.SeparatorImageUrlField;
					if (separatorImageUrlField.Length > 0)
					{
						PropertyDescriptor propertyDescriptor7 = properties.Find(separatorImageUrlField, true);
						if (propertyDescriptor7 == null)
						{
							throw new InvalidOperationException(SR.GetString("Menu_InvalidDataBinding", new object[]
							{
								separatorImageUrlField,
								"SeparatorImageUrlField"
							}));
						}
						object value7 = propertyDescriptor7.GetValue(obj);
						if (value7 != null)
						{
							text6 = value7.ToString();
						}
					}
					if (string.IsNullOrEmpty(text6))
					{
						text6 = binding.SeparatorImageUrl;
					}
					string toolTipField = binding.ToolTipField;
					if (toolTipField.Length > 0)
					{
						PropertyDescriptor propertyDescriptor8 = properties.Find(toolTipField, true);
						if (propertyDescriptor8 == null)
						{
							throw new InvalidOperationException(SR.GetString("Menu_InvalidDataBinding", new object[]
							{
								toolTipField,
								"ToolTipField"
							}));
						}
						object value8 = propertyDescriptor8.GetValue(obj);
						if (value8 != null)
						{
							text8 = value8.ToString();
						}
					}
					if (string.IsNullOrEmpty(text8))
					{
						text8 = binding.ToolTip;
					}
					string enabledField = binding.EnabledField;
					if (enabledField.Length > 0)
					{
						PropertyDescriptor propertyDescriptor9 = properties.Find(enabledField, true);
						if (propertyDescriptor9 == null)
						{
							throw new InvalidOperationException(SR.GetString("Menu_InvalidDataBinding", new object[]
							{
								enabledField,
								"EnabledField"
							}));
						}
						object value9 = propertyDescriptor9.GetValue(obj);
						if (value9 != null)
						{
							if (value9 is bool)
							{
								enabled = (bool)value9;
								flag = true;
							}
							else if (bool.TryParse(value9.ToString(), out enabled))
							{
								flag = true;
							}
						}
					}
					if (!flag)
					{
						enabled = binding.Enabled;
					}
					string selectableField = binding.SelectableField;
					if (selectableField.Length > 0)
					{
						PropertyDescriptor propertyDescriptor10 = properties.Find(selectableField, true);
						if (propertyDescriptor10 == null)
						{
							throw new InvalidOperationException(SR.GetString("Menu_InvalidDataBinding", new object[]
							{
								selectableField,
								"SelectableField"
							}));
						}
						object value10 = propertyDescriptor10.GetValue(obj);
						if (value10 != null)
						{
							if (value10 is bool)
							{
								selectable = (bool)value10;
								flag2 = true;
							}
							else if (bool.TryParse(value10.ToString(), out selectable))
							{
								flag2 = true;
							}
						}
					}
					if (!flag2)
					{
						selectable = binding.Selectable;
					}
				}
				else if (obj is INavigateUIData)
				{
					INavigateUIData navigateUIData = (INavigateUIData)obj;
					text = navigateUIData.Name;
					text2 = navigateUIData.Value;
					text3 = navigateUIData.NavigateUrl;
					if (string.IsNullOrEmpty(text3))
					{
						selectable = false;
					}
					text8 = navigateUIData.Description;
				}
				if (text == null)
				{
					text = obj.ToString();
				}
				MenuItem menuItem = null;
				if (text != null || text2 != null)
				{
					menuItem = new MenuItem(text, text2, text4, text3, text7);
				}
				if (menuItem != null)
				{
					if (text8.Length > 0)
					{
						menuItem.ToolTip = text8;
					}
					if (text5.Length > 0)
					{
						menuItem.PopOutImageUrl = text5;
					}
					if (text6.Length > 0)
					{
						menuItem.SeparatorImageUrl = text6;
					}
					menuItem.Enabled = enabled;
					menuItem.Selectable = selectable;
					menuItem.SetDataPath(hierarchyData.Path);
					menuItem.SetDataBound(true);
					node.ChildItems.Add(menuItem);
					if (string.Equals(hierarchyData.Path, this._currentSiteMapNodeUrl, StringComparison.OrdinalIgnoreCase))
					{
						menuItem.Selected = true;
					}
					menuItem.SetDataItem(hierarchyData.Item);
					this.OnMenuItemDataBound(new MenuEventArgs(menuItem));
					menuItem.SetDataItem(null);
					if (hierarchyData.HasChildren && num < this.MaximumDepth)
					{
						IHierarchicalEnumerable children = hierarchyData.GetChildren();
						if (children != null)
						{
							this.DataBindRecursive(menuItem, children);
						}
					}
				}
			}
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x000B3438 File Offset: 0x000B1638
		protected override void EnsureDataBound()
		{
			base.EnsureDataBound();
			if (!this._subControlsDataBound)
			{
				foreach (object obj in this.Controls)
				{
					Control control = (Control)obj;
					control.DataBind();
				}
				this._subControlsDataBound = true;
			}
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x000B34A8 File Offset: 0x000B16A8
		public MenuItem FindItem(string valuePath)
		{
			if (valuePath == null)
			{
				return null;
			}
			return this.Items.FindItem(valuePath.Split(new char[]
			{
				this.PathSeparator
			}), 0);
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000B34D0 File Offset: 0x000B16D0
		internal string GetCssClassName(MenuItem item, bool hyperLink)
		{
			bool flag;
			return this.GetCssClassName(item, hyperLink, out flag);
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x000B34E8 File Offset: 0x000B16E8
		internal string GetCssClassName(MenuItem item, bool hyperlink, out bool containsClassName)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			containsClassName = false;
			int depth = item.Depth;
			string text = Menu.CacheGetItem<string>(hyperlink ? this.CachedMenuItemHyperLinkClassNames : this.CachedMenuItemClassNames, depth);
			if (this.CachedLevelsContainingCssClass.Contains(depth))
			{
				containsClassName = true;
			}
			if (!item.Selected && text != null)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (text != null)
			{
				if (!item.Selected)
				{
					return text;
				}
				stringBuilder.Append(text);
				stringBuilder.Append(' ');
			}
			else
			{
				if (hyperlink)
				{
					stringBuilder.Append(this.RootMenuItemStyle.RegisteredCssClass);
					stringBuilder.Append(' ');
				}
				if (depth < this.StaticDisplayLevels)
				{
					containsClassName |= Menu.AppendCssClassName(stringBuilder, this._staticItemStyle, hyperlink);
				}
				else
				{
					containsClassName |= Menu.AppendCssClassName(stringBuilder, this._dynamicItemStyle, hyperlink);
				}
				if (depth < this.LevelMenuItemStyles.Count && this.LevelMenuItemStyles[depth] != null)
				{
					containsClassName |= Menu.AppendCssClassName(stringBuilder, this.LevelMenuItemStyles[depth], hyperlink);
				}
				text = stringBuilder.ToString().Trim();
				Menu.CacheSetItem<string>(hyperlink ? this.CachedMenuItemHyperLinkClassNames : this.CachedMenuItemClassNames, depth, text);
				if (containsClassName && !this.CachedLevelsContainingCssClass.Contains(depth))
				{
					this.CachedLevelsContainingCssClass.Add(depth);
				}
			}
			if (item.Selected)
			{
				if (depth < this.StaticDisplayLevels)
				{
					containsClassName |= Menu.AppendCssClassName(stringBuilder, this._staticSelectedStyle, hyperlink);
				}
				else
				{
					containsClassName |= Menu.AppendCssClassName(stringBuilder, this._dynamicSelectedStyle, hyperlink);
				}
				if (depth < this.LevelSelectedStyles.Count && this.LevelSelectedStyles[depth] != null)
				{
					MenuItemStyle style = this.LevelSelectedStyles[depth];
					containsClassName |= Menu.AppendCssClassName(stringBuilder, style, hyperlink);
				}
				return stringBuilder.ToString().Trim();
			}
			return text;
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x000B36AC File Offset: 0x000B18AC
		private MenuItem GetOneDynamicItem(MenuItem item)
		{
			if (item.Depth >= this.StaticDisplayLevels)
			{
				return item;
			}
			for (int i = 0; i < item.ChildItems.Count; i++)
			{
				MenuItem oneDynamicItem = this.GetOneDynamicItem(item.ChildItems[i]);
				if (oneDynamicItem != null)
				{
					return oneDynamicItem;
				}
			}
			return null;
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x000B36F8 File Offset: 0x000B18F8
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			IDictionary designModeState = base.GetDesignModeState();
			this.CreateChildControls();
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				control.DataBind();
			}
			using (StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture))
			{
				using (HtmlTextWriter designTimeWriter = this.GetDesignTimeWriter(stringWriter))
				{
					this.Renderer.RenderBeginTag(designTimeWriter, true);
					this.Renderer.RenderContents(designTimeWriter, true);
					this.Renderer.RenderEndTag(designTimeWriter, true);
					designModeState["GetDesignTimeStaticHtml"] = stringWriter.ToString();
				}
			}
			int staticDisplayLevels = this.StaticDisplayLevels;
			try
			{
				MenuItem menuItem = this.GetOneDynamicItem(this.RootItem);
				if (menuItem == null)
				{
					this._dataBound = false;
					this.StaticDisplayLevels = 1;
					menuItem = new MenuItem();
					menuItem.SetDepth(0);
					menuItem.SetOwner(this);
					string @string = SR.GetString("Menu_DesignTimeDummyItemText");
					for (int i = 0; i < 5; i++)
					{
						MenuItem menuItem2 = new MenuItem(@string);
						if (this.DynamicItemTemplate != null)
						{
							MenuItemTemplateContainer menuItemTemplateContainer = new MenuItemTemplateContainer(i, menuItem2);
							menuItem2.Container = menuItemTemplateContainer;
							this.DynamicItemTemplate.InstantiateIn(menuItemTemplateContainer);
							menuItemTemplateContainer.Site = base.Site;
							menuItemTemplateContainer.DataBind();
						}
						menuItem.ChildItems.Add(menuItem2);
					}
					menuItem.ChildItems[1].ChildItems.Add(new MenuItem());
					this._cachedLevelsContainingCssClass = null;
					this._cachedMenuItemStyles = null;
					this._cachedSubMenuStyles = null;
					this._cachedMenuItemClassNames = null;
					this._cachedMenuItemHyperLinkClassNames = null;
					this._cachedSubMenuClassNames = null;
				}
				else
				{
					menuItem = menuItem.Parent;
				}
				using (StringWriter stringWriter2 = new StringWriter(CultureInfo.CurrentCulture))
				{
					using (HtmlTextWriter designTimeWriter2 = this.GetDesignTimeWriter(stringWriter2))
					{
						base.Attributes.AddAttributes(designTimeWriter2);
						designTimeWriter2.RenderBeginTag(HtmlTextWriterTag.Table);
						designTimeWriter2.RenderBeginTag(HtmlTextWriterTag.Tr);
						designTimeWriter2.RenderBeginTag(HtmlTextWriterTag.Td);
						menuItem.Render(designTimeWriter2, true, false, false);
						designTimeWriter2.RenderEndTag();
						designTimeWriter2.RenderEndTag();
						designTimeWriter2.RenderEndTag();
						designModeState["GetDesignTimeDynamicHtml"] = stringWriter2.ToString();
					}
				}
			}
			finally
			{
				if (this.StaticDisplayLevels != staticDisplayLevels)
				{
					this.StaticDisplayLevels = staticDisplayLevels;
				}
			}
			return designModeState;
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000B39F0 File Offset: 0x000B1BF0
		private HtmlTextWriter GetDesignTimeWriter(StringWriter stringWriter)
		{
			if (this._designTimeTextWriterType == null)
			{
				return new HtmlTextWriter(stringWriter);
			}
			ConstructorInfo constructor = this._designTimeTextWriterType.GetConstructor(new Type[]
			{
				typeof(TextWriter)
			});
			if (constructor == null)
			{
				return new HtmlTextWriter(stringWriter);
			}
			return (HtmlTextWriter)constructor.Invoke(new object[]
			{
				stringWriter
			});
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000B3A58 File Offset: 0x000B1C58
		internal string GetImageUrl(int index)
		{
			if (this.ImageUrls[index] == null)
			{
				switch (index)
				{
				case 0:
					this.ImageUrls[index] = this.ScrollUpImageUrlInternal;
					break;
				case 1:
					this.ImageUrls[index] = this.ScrollDownImageUrlInternal;
					break;
				case 2:
					this.ImageUrls[index] = this.PopoutImageUrlInternal;
					break;
				}
				this.ImageUrls[index] = base.ResolveClientUrl(this.ImageUrls[index]);
			}
			return this.ImageUrls[index];
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000B3AD0 File Offset: 0x000B1CD0
		internal MenuItemStyle GetMenuItemStyle(MenuItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			int depth = item.Depth;
			MenuItemStyle menuItemStyle = Menu.CacheGetItem<MenuItemStyle>(this.CachedMenuItemStyles, depth);
			if (!item.Selected && menuItemStyle != null)
			{
				return menuItemStyle;
			}
			if (menuItemStyle == null)
			{
				menuItemStyle = new MenuItemStyle();
				menuItemStyle.CopyFrom(this.RootMenuItemStyle);
				if (depth < this.StaticDisplayLevels)
				{
					if (this._staticItemStyle != null)
					{
						TreeView.GetMergedStyle(menuItemStyle, this._staticItemStyle);
					}
				}
				else if (depth >= this.StaticDisplayLevels && this._dynamicItemStyle != null)
				{
					TreeView.GetMergedStyle(menuItemStyle, this._dynamicItemStyle);
				}
				if (depth < this.LevelMenuItemStyles.Count && this.LevelMenuItemStyles[depth] != null)
				{
					TreeView.GetMergedStyle(menuItemStyle, this.LevelMenuItemStyles[depth]);
				}
				Menu.CacheSetItem<MenuItemStyle>(this.CachedMenuItemStyles, depth, menuItemStyle);
			}
			if (item.Selected)
			{
				MenuItemStyle menuItemStyle2 = new MenuItemStyle();
				menuItemStyle2.CopyFrom(menuItemStyle);
				if (depth < this.StaticDisplayLevels)
				{
					if (this._staticSelectedStyle != null)
					{
						TreeView.GetMergedStyle(menuItemStyle2, this._staticSelectedStyle);
					}
				}
				else if (depth >= this.StaticDisplayLevels && this._dynamicSelectedStyle != null)
				{
					TreeView.GetMergedStyle(menuItemStyle2, this._dynamicSelectedStyle);
				}
				if (depth < this.LevelSelectedStyles.Count && this.LevelSelectedStyles[depth] != null)
				{
					TreeView.GetMergedStyle(menuItemStyle2, this.LevelSelectedStyles[depth]);
				}
				return menuItemStyle2;
			}
			return menuItemStyle;
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000B3C20 File Offset: 0x000B1E20
		internal string GetSubMenuCssClassName(MenuItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			int num = item.Depth + 1;
			string text = Menu.CacheGetItem<string>(this.CachedSubMenuClassNames, num);
			if (text != null)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (num < this.StaticDisplayLevels)
			{
				Menu.AppendMenuCssClassName(stringBuilder, this._staticMenuStyle);
			}
			else
			{
				SubMenuStyle subMenuStyle = this._panelStyle as SubMenuStyle;
				if (subMenuStyle != null)
				{
					Menu.AppendMenuCssClassName(stringBuilder, subMenuStyle);
				}
				Menu.AppendMenuCssClassName(stringBuilder, this._dynamicMenuStyle);
			}
			if (num < this.LevelSubMenuStyles.Count && this.LevelSubMenuStyles[num] != null)
			{
				SubMenuStyle style = this.LevelSubMenuStyles[num];
				Menu.AppendMenuCssClassName(stringBuilder, style);
			}
			text = stringBuilder.ToString().Trim();
			Menu.CacheSetItem<string>(this.CachedSubMenuClassNames, num, text);
			return text;
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000B3CE4 File Offset: 0x000B1EE4
		internal SubMenuStyle GetSubMenuStyle(MenuItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			int num = item.Depth + 1;
			SubMenuStyle subMenuStyle = Menu.CacheGetItem<SubMenuStyle>(this.CachedSubMenuStyles, num);
			if (subMenuStyle != null)
			{
				return subMenuStyle;
			}
			int staticDisplayLevels = this.StaticDisplayLevels;
			if (num >= staticDisplayLevels && !base.DesignMode)
			{
				subMenuStyle = new PopOutPanel.PopOutPanelStyle(this.Panel);
			}
			else
			{
				subMenuStyle = new SubMenuStyle();
			}
			if (num < staticDisplayLevels)
			{
				if (this._staticMenuStyle != null)
				{
					subMenuStyle.CopyFrom(this._staticMenuStyle);
				}
			}
			else if (num >= staticDisplayLevels && this._dynamicMenuStyle != null)
			{
				subMenuStyle.CopyFrom(this._dynamicMenuStyle);
			}
			if (this._levelStyles != null && this._levelStyles.Count > num && this._levelStyles[num] != null)
			{
				TreeView.GetMergedStyle(subMenuStyle, this._levelStyles[num]);
			}
			Menu.CacheSetItem<SubMenuStyle>(this.CachedSubMenuStyles, num, subMenuStyle);
			return subMenuStyle;
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x000B3DB8 File Offset: 0x000B1FB8
		internal void EnsureRootMenuStyle()
		{
			if (this._rootMenuItemStyle == null)
			{
				this._rootMenuItemStyle = new Style();
				this._rootMenuItemStyle.Font.CopyFrom(this.Font);
				if (!this.ForeColor.IsEmpty)
				{
					this._rootMenuItemStyle.ForeColor = this.ForeColor;
				}
				if (!base.ControlStyle.IsSet(8192))
				{
					this._rootMenuItemStyle.Font.Underline = false;
				}
			}
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x000B3E34 File Offset: 0x000B2034
		protected internal override void LoadControlState(object savedState)
		{
			Pair pair = savedState as Pair;
			if (pair == null)
			{
				base.LoadControlState(savedState);
				return;
			}
			base.LoadControlState(pair.First);
			this._selectedItem = null;
			if (pair.Second != null)
			{
				string text = pair.Second as string;
				if (text != null)
				{
					this._selectedItem = this.Items.FindItem(text.Split(new char[]
					{
						'\\'
					}), 0);
				}
			}
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000B3EA0 File Offset: 0x000B20A0
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				if (array[1] != null)
				{
					((IStateManager)this.StaticMenuItemStyle).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					((IStateManager)this.StaticSelectedStyle).LoadViewState(array[2]);
				}
				if (array[3] != null)
				{
					((IStateManager)this.StaticHoverStyle).LoadViewState(array[3]);
				}
				if (array[4] != null)
				{
					((IStateManager)this.StaticMenuStyle).LoadViewState(array[4]);
				}
				if (array[5] != null)
				{
					((IStateManager)this.DynamicMenuItemStyle).LoadViewState(array[5]);
				}
				if (array[6] != null)
				{
					((IStateManager)this.DynamicSelectedStyle).LoadViewState(array[6]);
				}
				if (array[7] != null)
				{
					((IStateManager)this.DynamicHoverStyle).LoadViewState(array[7]);
				}
				if (array[8] != null)
				{
					((IStateManager)this.DynamicMenuStyle).LoadViewState(array[8]);
				}
				if (array[9] != null)
				{
					((IStateManager)this.LevelMenuItemStyles).LoadViewState(array[9]);
				}
				if (array[10] != null)
				{
					((IStateManager)this.LevelSelectedStyles).LoadViewState(array[10]);
				}
				if (array[11] != null)
				{
					((IStateManager)this.LevelSubMenuStyles).LoadViewState(array[11]);
				}
				if (array[12] != null)
				{
					((IStateManager)this.Items).LoadViewState(array[12]);
					if (!string.IsNullOrEmpty(this.DataSourceID) || this.DataSource != null)
					{
						this._dataBound = true;
					}
				}
				if (array[0] != null)
				{
					base.LoadViewState(array[0]);
				}
			}
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000B3FD0 File Offset: 0x000B21D0
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			MenuEventArgs menuEventArgs = e as MenuEventArgs;
			if (menuEventArgs != null && StringUtil.EqualsIgnoreCase(menuEventArgs.CommandName, Menu.MenuItemClickCommandName))
			{
				if (!base.IsEnabled)
				{
					return true;
				}
				this.OnMenuItemClick(menuEventArgs);
				if (base.AdapterInternal != null)
				{
					MenuAdapter menuAdapter = base.AdapterInternal as MenuAdapter;
					if (menuAdapter != null)
					{
						MenuItem item = menuEventArgs.Item;
						if (item != null && item.ChildItems.Count > 0 && item.Depth + 1 >= this.StaticDisplayLevels)
						{
							menuAdapter.SetPath(menuEventArgs.Item.InternalValuePath);
						}
					}
				}
				base.RaiseBubbleEvent(this, e);
				return true;
			}
			else
			{
				if (e is CommandEventArgs)
				{
					base.RaiseBubbleEvent(this, e);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000B4077 File Offset: 0x000B2277
		protected override void OnDataBinding(EventArgs e)
		{
			this.EnsureChildControls();
			base.OnDataBinding(e);
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000B4086 File Offset: 0x000B2286
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.RegisterRequiresControlState(this);
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000B409C File Offset: 0x000B229C
		protected virtual void OnMenuItemClick(MenuEventArgs e)
		{
			this.SetSelectedItem(e.Item);
			MenuEventHandler menuEventHandler = (MenuEventHandler)base.Events[Menu._menuItemClickedEvent];
			if (menuEventHandler != null)
			{
				menuEventHandler(this, e);
			}
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x000B40D8 File Offset: 0x000B22D8
		protected virtual void OnMenuItemDataBound(MenuEventArgs e)
		{
			MenuEventHandler menuEventHandler = (MenuEventHandler)base.Events[Menu._menuItemDataBoundEvent];
			if (menuEventHandler != null)
			{
				menuEventHandler(this, e);
			}
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x000B4106 File Offset: 0x000B2306
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Items.Count > 0)
			{
				this.Renderer.PreRender(base.IsEnabled);
			}
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000B412E File Offset: 0x000B232E
		internal void OnPreRender(EventArgs e, bool registerScript)
		{
			base.OnPreRender(e);
			if (this.Items.Count > 0)
			{
				this.Renderer.PreRender(registerScript);
			}
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000B4154 File Offset: 0x000B2354
		protected internal override void PerformDataBinding()
		{
			base.PerformDataBinding();
			this.DataBindItem(this.RootItem);
			if (!base.DesignMode && this._dataBound && string.IsNullOrEmpty(this.DataSourceID) && this.DataSource == null)
			{
				this.Items.Clear();
				this.Controls.Clear();
				base.ClearChildViewState();
				this.TrackViewState();
				base.ChildControlsCreated = true;
				return;
			}
			if (!string.IsNullOrEmpty(this.DataSourceID) || this.DataSource != null)
			{
				this.Controls.Clear();
				base.ClearChildState();
				this.TrackViewState();
				this.CreateChildControlsFromItems(true);
				base.ChildControlsCreated = true;
				this._dataBound = true;
			}
			else if (!this._subControlsDataBound)
			{
				foreach (object obj in this.Controls)
				{
					Control control = (Control)obj;
					control.DataBind();
				}
			}
			this._subControlsDataBound = true;
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000B4260 File Offset: 0x000B2460
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.VerifyRenderingInServerForm();
			if (this.Items.Count > 0)
			{
				this.Renderer.RenderBeginTag(writer, false);
				this.Renderer.RenderContents(writer, false);
				this.Renderer.RenderEndTag(writer, false);
			}
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x000B429D File Offset: 0x000B249D
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			this.Renderer.RenderBeginTag(writer, false);
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x000B42AC File Offset: 0x000B24AC
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer, false);
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000B42BB File Offset: 0x000B24BB
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			this.Renderer.RenderEndTag(writer, false);
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000B42CC File Offset: 0x000B24CC
		internal void ResetCachedStyles()
		{
			if (this._dynamicItemStyle != null)
			{
				this._dynamicItemStyle.ResetCachedStyles();
			}
			if (this._staticItemStyle != null)
			{
				this._staticItemStyle.ResetCachedStyles();
			}
			if (this._dynamicSelectedStyle != null)
			{
				this._dynamicSelectedStyle.ResetCachedStyles();
			}
			if (this._staticSelectedStyle != null)
			{
				this._staticSelectedStyle.ResetCachedStyles();
			}
			if (this._staticHoverStyle != null)
			{
				this._staticHoverHyperLinkStyle = new HyperLinkStyle(this._staticHoverStyle);
			}
			if (this._dynamicHoverStyle != null)
			{
				this._dynamicHoverHyperLinkStyle = new HyperLinkStyle(this._dynamicHoverStyle);
			}
			foreach (object obj in this.LevelMenuItemStyles)
			{
				MenuItemStyle menuItemStyle = (MenuItemStyle)obj;
				menuItemStyle.ResetCachedStyles();
			}
			foreach (object obj2 in this.LevelSelectedStyles)
			{
				MenuItemStyle menuItemStyle2 = (MenuItemStyle)obj2;
				menuItemStyle2.ResetCachedStyles();
			}
			if (this._imageUrls != null)
			{
				for (int i = 0; i < this._imageUrls.Length; i++)
				{
					this._imageUrls[i] = null;
				}
			}
			this._cachedPopOutImageUrl = null;
			this._cachedScrollDownImageUrl = null;
			this._cachedScrollUpImageUrl = null;
			this._cachedLevelsContainingCssClass = null;
			this._cachedMenuItemClassNames = null;
			this._cachedMenuItemHyperLinkClassNames = null;
			this._cachedMenuItemStyles = null;
			this._cachedSubMenuClassNames = null;
			this._cachedSubMenuStyles = null;
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000B4454 File Offset: 0x000B2654
		protected internal override object SaveControlState()
		{
			object obj = base.SaveControlState();
			if (this._selectedItem != null)
			{
				return new Pair(obj, this._selectedItem.InternalValuePath);
			}
			return obj;
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x000B4484 File Offset: 0x000B2684
		protected override object SaveViewState()
		{
			object[] array = new object[13];
			array[0] = base.SaveViewState();
			bool flag = array[0] != null;
			if (this._staticItemStyle != null)
			{
				array[1] = ((IStateManager)this._staticItemStyle).SaveViewState();
				flag |= (array[1] != null);
			}
			if (this._staticSelectedStyle != null)
			{
				array[2] = ((IStateManager)this._staticSelectedStyle).SaveViewState();
				flag |= (array[2] != null);
			}
			if (this._staticHoverStyle != null)
			{
				array[3] = ((IStateManager)this._staticHoverStyle).SaveViewState();
				flag |= (array[3] != null);
			}
			if (this._staticMenuStyle != null)
			{
				array[4] = ((IStateManager)this._staticMenuStyle).SaveViewState();
				flag |= (array[4] != null);
			}
			if (this._dynamicItemStyle != null)
			{
				array[5] = ((IStateManager)this._dynamicItemStyle).SaveViewState();
				flag |= (array[5] != null);
			}
			if (this._dynamicSelectedStyle != null)
			{
				array[6] = ((IStateManager)this._dynamicSelectedStyle).SaveViewState();
				flag |= (array[6] != null);
			}
			if (this._dynamicHoverStyle != null)
			{
				array[7] = ((IStateManager)this._dynamicHoverStyle).SaveViewState();
				flag |= (array[7] != null);
			}
			if (this._dynamicMenuStyle != null)
			{
				array[8] = ((IStateManager)this._dynamicMenuStyle).SaveViewState();
				flag |= (array[8] != null);
			}
			if (this._levelMenuItemStyles != null)
			{
				array[9] = ((IStateManager)this._levelMenuItemStyles).SaveViewState();
				flag |= (array[9] != null);
			}
			if (this._levelSelectedStyles != null)
			{
				array[10] = ((IStateManager)this._levelSelectedStyles).SaveViewState();
				flag |= (array[10] != null);
			}
			if (this._levelStyles != null)
			{
				array[11] = ((IStateManager)this._levelStyles).SaveViewState();
				flag |= (array[11] != null);
			}
			array[12] = ((IStateManager)this.Items).SaveViewState();
			flag |= (array[12] != null);
			if (flag)
			{
				return array;
			}
			return null;
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000B4624 File Offset: 0x000B2824
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			if (data.Contains("DesignTimeTextWriterType"))
			{
				Type type = data["DesignTimeTextWriterType"] as Type;
				if (type != null && type.IsSubclassOf(typeof(HtmlTextWriter)))
				{
					this._designTimeTextWriterType = type;
				}
			}
			base.SetDesignModeState(data);
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000B4678 File Offset: 0x000B2878
		protected void SetItemDataBound(MenuItem node, bool dataBound)
		{
			node.SetDataBound(dataBound);
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000B4681 File Offset: 0x000B2881
		protected void SetItemDataItem(MenuItem node, object dataItem)
		{
			node.SetDataItem(dataItem);
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000B468A File Offset: 0x000B288A
		protected void SetItemDataPath(MenuItem node, string dataPath)
		{
			node.SetDataPath(dataPath);
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000B4694 File Offset: 0x000B2894
		internal void SetSelectedItem(MenuItem node)
		{
			if (this._selectedItem != node)
			{
				if (node != null)
				{
					if (node.Depth >= this.MaximumDepth)
					{
						throw new InvalidOperationException(SR.GetString("Menu_InvalidDepth"));
					}
					if (!node.IsEnabledNoOwner || !node.Selectable)
					{
						throw new InvalidOperationException(SR.GetString("Menu_InvalidSelection"));
					}
				}
				if (this._selectedItem != null && this._selectedItem.Selected)
				{
					this._selectedItem.SetSelected(false);
				}
				this._selectedItem = node;
				if (this._selectedItem != null && !this._selectedItem.Selected)
				{
					this._selectedItem.SetSelected(true);
				}
			}
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000B4738 File Offset: 0x000B2938
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._staticItemStyle != null)
			{
				((IStateManager)this._staticItemStyle).TrackViewState();
			}
			if (this._staticSelectedStyle != null)
			{
				((IStateManager)this._staticSelectedStyle).TrackViewState();
			}
			if (this._staticHoverStyle != null)
			{
				((IStateManager)this._staticHoverStyle).TrackViewState();
			}
			if (this._staticMenuStyle != null)
			{
				((IStateManager)this._staticMenuStyle).TrackViewState();
			}
			if (this._dynamicItemStyle != null)
			{
				((IStateManager)this._dynamicItemStyle).TrackViewState();
			}
			if (this._dynamicSelectedStyle != null)
			{
				((IStateManager)this._dynamicSelectedStyle).TrackViewState();
			}
			if (this._dynamicHoverStyle != null)
			{
				((IStateManager)this._dynamicHoverStyle).TrackViewState();
			}
			if (this._dynamicMenuStyle != null)
			{
				((IStateManager)this._dynamicMenuStyle).TrackViewState();
			}
			if (this._levelMenuItemStyles != null)
			{
				((IStateManager)this._levelMenuItemStyles).TrackViewState();
			}
			if (this._levelSelectedStyles != null)
			{
				((IStateManager)this._levelSelectedStyles).TrackViewState();
			}
			if (this._levelStyles != null)
			{
				((IStateManager)this._levelStyles).TrackViewState();
			}
			if (this._bindings != null)
			{
				((IStateManager)this._bindings).TrackViewState();
			}
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000B483A File Offset: 0x000B2A3A
		internal void VerifyRenderingInServerForm()
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000B4850 File Offset: 0x000B2A50
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000B485C File Offset: 0x000B2A5C
		protected internal virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (!base.IsEnabled)
			{
				return;
			}
			this.EnsureChildControls();
			if (base.AdapterInternal != null)
			{
				IPostBackEventHandler postBackEventHandler = base.AdapterInternal as IPostBackEventHandler;
				if (postBackEventHandler != null)
				{
					postBackEventHandler.RaisePostBackEvent(eventArgument);
					return;
				}
			}
			else
			{
				this.InternalRaisePostBackEvent(eventArgument);
			}
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x000B48AC File Offset: 0x000B2AAC
		internal void InternalRaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.Length == 0)
			{
				return;
			}
			string text = HttpUtility.HtmlDecode(eventArgument);
			int num = 0;
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '\\' && ++num >= this.MaximumDepth)
				{
					throw new InvalidOperationException(SR.GetString("Menu_InvalidDepth"));
				}
			}
			MenuItem menuItem = this.Items.FindItem(text.Split(new char[]
			{
				'\\'
			}), 0);
			if (menuItem != null)
			{
				this.OnMenuItemClick(new MenuEventArgs(menuItem));
			}
		}

		// Token: 0x0400222B RID: 8747
		internal const int ScrollUpImageIndex = 0;

		// Token: 0x0400222C RID: 8748
		internal const int ScrollDownImageIndex = 1;

		// Token: 0x0400222D RID: 8749
		internal const int PopOutImageIndex = 2;

		// Token: 0x0400222E RID: 8750
		internal const int ImageUrlsCount = 3;

		// Token: 0x0400222F RID: 8751
		private const string _getDesignTimeStaticHtml = "GetDesignTimeStaticHtml";

		// Token: 0x04002230 RID: 8752
		private const string _getDesignTimeDynamicHtml = "GetDesignTimeDynamicHtml";

		// Token: 0x04002231 RID: 8753
		public static readonly string MenuItemClickCommandName = "Click";

		// Token: 0x04002232 RID: 8754
		private static readonly object _menuItemClickedEvent = new object();

		// Token: 0x04002233 RID: 8755
		private static readonly object _menuItemDataBoundEvent = new object();

		// Token: 0x04002234 RID: 8756
		private MenuRenderingMode _renderingMode;

		// Token: 0x04002235 RID: 8757
		private string[] _imageUrls;

		// Token: 0x04002236 RID: 8758
		private SubMenuStyle _staticMenuStyle;

		// Token: 0x04002237 RID: 8759
		private SubMenuStyle _dynamicMenuStyle;

		// Token: 0x04002238 RID: 8760
		private MenuItemStyle _staticItemStyle;

		// Token: 0x04002239 RID: 8761
		private MenuItemStyle _staticSelectedStyle;

		// Token: 0x0400223A RID: 8762
		private Style _staticHoverStyle;

		// Token: 0x0400223B RID: 8763
		private HyperLinkStyle _staticHoverHyperLinkStyle;

		// Token: 0x0400223C RID: 8764
		private MenuItemStyle _dynamicItemStyle;

		// Token: 0x0400223D RID: 8765
		private MenuItemStyle _dynamicSelectedStyle;

		// Token: 0x0400223E RID: 8766
		private Style _dynamicHoverStyle;

		// Token: 0x0400223F RID: 8767
		private HyperLinkStyle _dynamicHoverHyperLinkStyle;

		// Token: 0x04002240 RID: 8768
		private Style _rootMenuItemStyle;

		// Token: 0x04002241 RID: 8769
		private SubMenuStyleCollection _levelStyles;

		// Token: 0x04002242 RID: 8770
		private MenuItemStyleCollection _levelMenuItemStyles;

		// Token: 0x04002243 RID: 8771
		private MenuItemStyleCollection _levelSelectedStyles;

		// Token: 0x04002244 RID: 8772
		private List<MenuItemStyle> _cachedMenuItemStyles;

		// Token: 0x04002245 RID: 8773
		private List<SubMenuStyle> _cachedSubMenuStyles;

		// Token: 0x04002246 RID: 8774
		private List<string> _cachedMenuItemClassNames;

		// Token: 0x04002247 RID: 8775
		private List<string> _cachedMenuItemHyperLinkClassNames;

		// Token: 0x04002248 RID: 8776
		private List<string> _cachedSubMenuClassNames;

		// Token: 0x04002249 RID: 8777
		private Collection<int> _cachedLevelsContainingCssClass;

		// Token: 0x0400224A RID: 8778
		private MenuItem _rootItem;

		// Token: 0x0400224B RID: 8779
		private MenuItem _selectedItem;

		// Token: 0x0400224C RID: 8780
		private MenuItemBindingCollection _bindings;

		// Token: 0x0400224D RID: 8781
		private string _cachedScrollUpImageUrl;

		// Token: 0x0400224E RID: 8782
		private string _cachedScrollDownImageUrl;

		// Token: 0x0400224F RID: 8783
		private string _cachedPopOutImageUrl;

		// Token: 0x04002250 RID: 8784
		private ITemplate _dynamicTemplate;

		// Token: 0x04002251 RID: 8785
		private ITemplate _staticTemplate;

		// Token: 0x04002252 RID: 8786
		private int _maximumDepth;

		// Token: 0x04002253 RID: 8787
		private int _nodeIndex;

		// Token: 0x04002254 RID: 8788
		private string _currentSiteMapNodeUrl;

		// Token: 0x04002255 RID: 8789
		private bool _dataBound;

		// Token: 0x04002256 RID: 8790
		private bool _subControlsDataBound;

		// Token: 0x04002257 RID: 8791
		private bool _accessKeyRendered;

		// Token: 0x04002258 RID: 8792
		private PopOutPanel _panel;

		// Token: 0x04002259 RID: 8793
		private Style _panelStyle;

		// Token: 0x0400225A RID: 8794
		private bool _isNotIE;

		// Token: 0x0400225B RID: 8795
		private Type _designTimeTextWriterType;

		// Token: 0x0400225C RID: 8796
		private Menu.MenuRenderer _renderer;

		// Token: 0x020009AA RID: 2474
		internal abstract class MenuRenderer
		{
			// Token: 0x06006BB4 RID: 27572 RVA: 0x0018005F File Offset: 0x0017E25F
			protected MenuRenderer(Menu menu)
			{
				this.Menu = menu;
			}

			// Token: 0x17001DBA RID: 7610
			// (get) Token: 0x06006BB5 RID: 27573 RVA: 0x0018006E File Offset: 0x0017E26E
			// (set) Token: 0x06006BB6 RID: 27574 RVA: 0x00180076 File Offset: 0x0017E276
			private protected Menu Menu { protected get; private set; }

			// Token: 0x06006BB7 RID: 27575
			public abstract void PreRender(bool registerScript);

			// Token: 0x06006BB8 RID: 27576
			public abstract void RenderBeginTag(HtmlTextWriter writer, bool staticOnly);

			// Token: 0x06006BB9 RID: 27577
			public abstract void RenderContents(HtmlTextWriter writer, bool staticOnly);

			// Token: 0x06006BBA RID: 27578
			public abstract void RenderEndTag(HtmlTextWriter writer, bool staticOnly);

			// Token: 0x06006BBB RID: 27579 RVA: 0x0018007F File Offset: 0x0017E27F
			public virtual void Render(HtmlTextWriter writer, bool staticOnly)
			{
				this.RenderBeginTag(writer, staticOnly);
				this.RenderContents(writer, staticOnly);
				this.RenderEndTag(writer, staticOnly);
			}
		}

		// Token: 0x020009AB RID: 2475
		private class MenuRendererClassic : Menu.MenuRenderer
		{
			// Token: 0x06006BBC RID: 27580 RVA: 0x00180099 File Offset: 0x0017E299
			public MenuRendererClassic(Menu menu) : base(menu)
			{
			}

			// Token: 0x06006BBD RID: 27581 RVA: 0x001800A4 File Offset: 0x0017E2A4
			internal void EnsureRenderSettings()
			{
				if (base.Menu.Page == null)
				{
					return;
				}
				if (base.Menu.Page.Header != null)
				{
					base.Menu._isNotIE = (base.Menu.Page.Request.Browser.MSDomVersion.Major < 4);
					if (base.Menu.Page.SupportsStyleSheets || (base.Menu.Page.ScriptManager != null && base.Menu.Page.ScriptManager.IsInAsyncPostBack))
					{
						base.Menu._panelStyle = base.Menu.Panel.GetEmptyPopOutPanelStyle();
						this.RegisterStyle(base.Menu._panelStyle);
						this.RegisterStyle(base.Menu.RootMenuItemStyle);
						this.RegisterStyle(base.Menu.ControlStyle);
						if (base.Menu._staticItemStyle != null)
						{
							base.Menu._staticItemStyle.HyperLinkStyle.DoNotRenderDefaults = true;
							this.RegisterStyle(base.Menu._staticItemStyle.HyperLinkStyle);
							this.RegisterStyle(base.Menu._staticItemStyle);
						}
						if (base.Menu._staticMenuStyle != null)
						{
							this.RegisterStyle(base.Menu._staticMenuStyle);
						}
						if (base.Menu._dynamicItemStyle != null)
						{
							base.Menu._dynamicItemStyle.HyperLinkStyle.DoNotRenderDefaults = true;
							this.RegisterStyle(base.Menu._dynamicItemStyle.HyperLinkStyle);
							this.RegisterStyle(base.Menu._dynamicItemStyle);
						}
						if (base.Menu._dynamicMenuStyle != null)
						{
							this.RegisterStyle(base.Menu._dynamicMenuStyle);
						}
						foreach (object obj in base.Menu.LevelMenuItemStyles)
						{
							MenuItemStyle menuItemStyle = (MenuItemStyle)obj;
							menuItemStyle.HyperLinkStyle.DoNotRenderDefaults = true;
							this.RegisterStyle(menuItemStyle.HyperLinkStyle);
							this.RegisterStyle(menuItemStyle);
						}
						foreach (object obj2 in base.Menu.LevelSubMenuStyles)
						{
							SubMenuStyle style = (SubMenuStyle)obj2;
							this.RegisterStyle(style);
						}
						if (base.Menu._staticSelectedStyle != null)
						{
							base.Menu._staticSelectedStyle.HyperLinkStyle.DoNotRenderDefaults = true;
							this.RegisterStyle(base.Menu._staticSelectedStyle.HyperLinkStyle);
							this.RegisterStyle(base.Menu._staticSelectedStyle);
						}
						if (base.Menu._dynamicSelectedStyle != null)
						{
							base.Menu._dynamicSelectedStyle.HyperLinkStyle.DoNotRenderDefaults = true;
							this.RegisterStyle(base.Menu._dynamicSelectedStyle.HyperLinkStyle);
							this.RegisterStyle(base.Menu._dynamicSelectedStyle);
						}
						foreach (object obj3 in base.Menu.LevelSelectedStyles)
						{
							MenuItemStyle menuItemStyle2 = (MenuItemStyle)obj3;
							menuItemStyle2.HyperLinkStyle.DoNotRenderDefaults = true;
							this.RegisterStyle(menuItemStyle2.HyperLinkStyle);
							this.RegisterStyle(menuItemStyle2);
						}
						if (base.Menu._staticHoverStyle != null)
						{
							base.Menu._staticHoverHyperLinkStyle = new HyperLinkStyle(base.Menu._staticHoverStyle);
							base.Menu._staticHoverHyperLinkStyle.DoNotRenderDefaults = true;
							this.RegisterStyle(base.Menu._staticHoverHyperLinkStyle);
							this.RegisterStyle(base.Menu._staticHoverStyle);
						}
						if (base.Menu._dynamicHoverStyle != null)
						{
							base.Menu._dynamicHoverHyperLinkStyle = new HyperLinkStyle(base.Menu._dynamicHoverStyle);
							base.Menu._dynamicHoverHyperLinkStyle.DoNotRenderDefaults = true;
							this.RegisterStyle(base.Menu._dynamicHoverHyperLinkStyle);
							this.RegisterStyle(base.Menu._dynamicHoverStyle);
						}
					}
					return;
				}
				if (base.Menu._staticHoverStyle != null)
				{
					throw new InvalidOperationException(SR.GetString("NeedHeader", new object[]
					{
						"Menu.StaticHoverStyle"
					}));
				}
				if (base.Menu._dynamicHoverStyle != null)
				{
					throw new InvalidOperationException(SR.GetString("NeedHeader", new object[]
					{
						"Menu.DynamicHoverStyle"
					}));
				}
			}

			// Token: 0x06006BBE RID: 27582 RVA: 0x00180524 File Offset: 0x0017E724
			public override void PreRender(bool registerScript)
			{
				this.EnsureRenderSettings();
				if (base.Menu.Page != null && registerScript)
				{
					base.Menu.Page.RegisterWebFormsScript();
					base.Menu.Page.ClientScript.RegisterClientScriptResource(base.Menu, typeof(Menu), "Menu.js");
					string clientDataObjectID = base.Menu.ClientDataObjectID;
					StringBuilder stringBuilder = new StringBuilder("var ");
					stringBuilder.Append(clientDataObjectID);
					stringBuilder.Append(" = new Object();\r\n");
					stringBuilder.Append(clientDataObjectID);
					stringBuilder.Append(".disappearAfter = ");
					stringBuilder.Append(base.Menu.DisappearAfter);
					stringBuilder.Append(";\r\n");
					stringBuilder.Append(clientDataObjectID);
					stringBuilder.Append(".horizontalOffset = ");
					stringBuilder.Append(base.Menu.DynamicHorizontalOffset);
					stringBuilder.Append(";\r\n");
					stringBuilder.Append(clientDataObjectID);
					stringBuilder.Append(".verticalOffset = ");
					stringBuilder.Append(base.Menu.DynamicVerticalOffset);
					stringBuilder.Append(";\r\n");
					if (base.Menu._dynamicHoverStyle != null)
					{
						stringBuilder.Append(clientDataObjectID);
						stringBuilder.Append(".hoverClass = '");
						stringBuilder.Append(base.Menu._dynamicHoverStyle.RegisteredCssClass);
						if (!string.IsNullOrEmpty(base.Menu._dynamicHoverStyle.CssClass))
						{
							if (!string.IsNullOrEmpty(base.Menu._dynamicHoverStyle.RegisteredCssClass))
							{
								stringBuilder.Append(' ');
							}
							stringBuilder.Append(base.Menu._dynamicHoverStyle.CssClass);
						}
						stringBuilder.Append("';\r\n");
						if (base.Menu._dynamicHoverHyperLinkStyle != null)
						{
							stringBuilder.Append(clientDataObjectID);
							stringBuilder.Append(".hoverHyperLinkClass = '");
							stringBuilder.Append(base.Menu._dynamicHoverHyperLinkStyle.RegisteredCssClass);
							if (!string.IsNullOrEmpty(base.Menu._dynamicHoverStyle.CssClass))
							{
								if (!string.IsNullOrEmpty(base.Menu._dynamicHoverHyperLinkStyle.RegisteredCssClass))
								{
									stringBuilder.Append(' ');
								}
								stringBuilder.Append(base.Menu._dynamicHoverStyle.CssClass);
							}
							stringBuilder.Append("';\r\n");
						}
					}
					if (base.Menu._staticHoverStyle != null && base.Menu._staticHoverHyperLinkStyle != null)
					{
						stringBuilder.Append(clientDataObjectID);
						stringBuilder.Append(".staticHoverClass = '");
						stringBuilder.Append(base.Menu._staticHoverStyle.RegisteredCssClass);
						if (!string.IsNullOrEmpty(base.Menu._staticHoverStyle.CssClass))
						{
							if (!string.IsNullOrEmpty(base.Menu._staticHoverStyle.RegisteredCssClass))
							{
								stringBuilder.Append(' ');
							}
							stringBuilder.Append(base.Menu._staticHoverStyle.CssClass);
						}
						stringBuilder.Append("';\r\n");
						if (base.Menu._staticHoverHyperLinkStyle != null)
						{
							stringBuilder.Append(clientDataObjectID);
							stringBuilder.Append(".staticHoverHyperLinkClass = '");
							stringBuilder.Append(base.Menu._staticHoverHyperLinkStyle.RegisteredCssClass);
							if (!string.IsNullOrEmpty(base.Menu._staticHoverStyle.CssClass))
							{
								if (!string.IsNullOrEmpty(base.Menu._staticHoverHyperLinkStyle.RegisteredCssClass))
								{
									stringBuilder.Append(' ');
								}
								stringBuilder.Append(base.Menu._staticHoverStyle.CssClass);
							}
							stringBuilder.Append("';\r\n");
						}
					}
					if (base.Menu.Page.RequestInternal != null && string.Equals(base.Menu.Page.Request.Url.Scheme, "https", StringComparison.OrdinalIgnoreCase))
					{
						stringBuilder.Append(clientDataObjectID);
						stringBuilder.Append(".iframeUrl = '");
						stringBuilder.Append(Util.QuoteJScriptString(base.Menu.Page.ClientScript.GetWebResourceUrl(typeof(Menu), "SmartNav.htm"), false));
						stringBuilder.Append("';\r\n");
					}
					base.Menu.Page.ClientScript.RegisterStartupScript(base.Menu, base.GetType(), base.Menu.ClientID + "_CreateDataObject", stringBuilder.ToString(), true);
				}
			}

			// Token: 0x06006BBF RID: 27583 RVA: 0x00180974 File Offset: 0x0017EB74
			private void RegisterStyle(Style style)
			{
				if (base.Menu.Page != null && base.Menu.Page.SupportsStyleSheets)
				{
					string clientID = base.Menu.ClientID;
					string str = "_";
					int cssStyleIndex = this._cssStyleIndex;
					this._cssStyleIndex = cssStyleIndex + 1;
					string text = clientID + str + cssStyleIndex.ToString(NumberFormatInfo.InvariantInfo);
					base.Menu.Page.Header.StyleSheet.CreateStyleRule(style, base.Menu, "." + text);
					style.SetRegisteredCssClass(text);
				}
			}

			// Token: 0x06006BC0 RID: 27584 RVA: 0x00180A08 File Offset: 0x0017EC08
			public override void RenderBeginTag(HtmlTextWriter writer, bool staticOnly)
			{
				ControlRenderingHelper.WriteSkipLinkStart(writer, base.Menu.RenderingCompatibility, base.Menu.DesignMode, base.Menu.SkipLinkText, base.Menu.SpacerImageUrl, base.Menu.ClientID);
				base.Menu.EnsureRootMenuStyle();
				if (base.Menu.Font != null)
				{
					base.Menu.Font.Reset();
				}
				base.Menu.ForeColor = Color.Empty;
				SubMenuStyle subMenuStyle = base.Menu.GetSubMenuStyle(base.Menu.RootItem);
				if (base.Menu.Page != null && base.Menu.Page.SupportsStyleSheets)
				{
					string subMenuCssClassName = base.Menu.GetSubMenuCssClassName(base.Menu.RootItem);
					if (subMenuCssClassName.Length > 0)
					{
						if (base.Menu.CssClass.Length == 0)
						{
							base.Menu.CssClass = subMenuCssClassName;
						}
						else
						{
							Menu menu = base.Menu;
							menu.CssClass = menu.CssClass + " " + subMenuCssClassName;
						}
					}
				}
				else if (subMenuStyle != null && !subMenuStyle.IsEmpty)
				{
					subMenuStyle.Font.Reset();
					subMenuStyle.ForeColor = Color.Empty;
					base.Menu.ControlStyle.CopyFrom(subMenuStyle);
				}
				base.Menu.AddAttributesToRender(writer);
				writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
				writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
				writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
				writer.RenderBeginTag(HtmlTextWriterTag.Table);
			}

			// Token: 0x06006BC1 RID: 27585 RVA: 0x00180B88 File Offset: 0x0017ED88
			public override void RenderContents(HtmlTextWriter writer, bool staticOnly)
			{
				if (base.Menu.Orientation == Orientation.Horizontal)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				}
				bool isEnabled = base.Menu.IsEnabled;
				if (base.Menu.StaticDisplayLevels > 1)
				{
					if (base.Menu.Orientation == Orientation.Vertical)
					{
						for (int i = 0; i < base.Menu.Items.Count; i++)
						{
							base.Menu.Items[i].RenderItem(writer, i, isEnabled, base.Menu.Orientation, staticOnly);
							if (base.Menu.Items[i].ChildItems.Count != 0)
							{
								writer.RenderBeginTag(HtmlTextWriterTag.Tr);
								writer.RenderBeginTag(HtmlTextWriterTag.Td);
								base.Menu.Items[i].Render(writer, isEnabled, staticOnly);
								writer.RenderEndTag();
								writer.RenderEndTag();
							}
						}
					}
					else
					{
						for (int j = 0; j < base.Menu.Items.Count; j++)
						{
							base.Menu.Items[j].RenderItem(writer, j, isEnabled, base.Menu.Orientation, staticOnly);
							if (base.Menu.Items[j].ChildItems.Count != 0)
							{
								writer.RenderBeginTag(HtmlTextWriterTag.Td);
								base.Menu.Items[j].Render(writer, isEnabled, staticOnly);
								writer.RenderEndTag();
							}
						}
					}
				}
				else
				{
					for (int k = 0; k < base.Menu.Items.Count; k++)
					{
						base.Menu.Items[k].RenderItem(writer, k, isEnabled, base.Menu.Orientation, staticOnly);
					}
				}
				if (base.Menu.Orientation == Orientation.Horizontal)
				{
					writer.RenderEndTag();
				}
				if (base.Menu.DesignMode)
				{
					base.Menu.ResetCachedStyles();
				}
			}

			// Token: 0x06006BC2 RID: 27586 RVA: 0x00180D64 File Offset: 0x0017EF64
			public override void RenderEndTag(HtmlTextWriter writer, bool staticOnly)
			{
				writer.RenderEndTag();
				if (base.Menu.StaticDisplayLevels <= 1 && !staticOnly)
				{
					bool isEnabled = base.Menu.IsEnabled;
					for (int i = 0; i < base.Menu.Items.Count; i++)
					{
						base.Menu.Items[i].Render(writer, isEnabled, staticOnly);
					}
				}
				ControlRenderingHelper.WriteSkipLinkEnd(writer, base.Menu.DesignMode, base.Menu.SkipLinkText, base.Menu.ClientID);
			}

			// Token: 0x04003953 RID: 14675
			private int _cssStyleIndex;
		}

		// Token: 0x020009AC RID: 2476
		internal class MenuRendererStandards : Menu.MenuRenderer
		{
			// Token: 0x06006BC3 RID: 27587 RVA: 0x00180099 File Offset: 0x0017E299
			public MenuRendererStandards(Menu menu) : base(menu)
			{
			}

			// Token: 0x17001DBB RID: 7611
			// (get) Token: 0x06006BC4 RID: 27588 RVA: 0x00180DEF File Offset: 0x0017EFEF
			private string DynamicPopOutUrl
			{
				get
				{
					if (this._dynamicPopOutUrl == null)
					{
						this._dynamicPopOutUrl = this.GetDynamicPopOutImageUrl();
					}
					return this._dynamicPopOutUrl;
				}
			}

			// Token: 0x17001DBC RID: 7612
			// (get) Token: 0x06006BC5 RID: 27589 RVA: 0x00180E0B File Offset: 0x0017F00B
			protected virtual string SpacerImageUrl
			{
				get
				{
					return base.Menu.SpacerImageUrl;
				}
			}

			// Token: 0x17001DBD RID: 7613
			// (get) Token: 0x06006BC6 RID: 27590 RVA: 0x00180E18 File Offset: 0x0017F018
			private string StaticPopOutUrl
			{
				get
				{
					if (this._staticPopOutUrl == null)
					{
						this._staticPopOutUrl = this.GetStaticPopOutImageUrl();
					}
					return this._staticPopOutUrl;
				}
			}

			// Token: 0x06006BC7 RID: 27591 RVA: 0x00180E34 File Offset: 0x0017F034
			private void AddScriptReference()
			{
				string key = "_registerMenu_" + base.Menu.ClientID;
				string script = string.Format(CultureInfo.InvariantCulture, "<script type='text/javascript'>new Sys.WebForms.Menu({{ element: '{0}', disappearAfter: {1}, orientation: '{2}', tabIndex: {3}, disabled: {4} }});</script>", new object[]
				{
					base.Menu.ClientID,
					base.Menu.DisappearAfter,
					base.Menu.Orientation.ToString().ToLowerInvariant(),
					base.Menu.TabIndex,
					(!base.Menu.IsEnabled).ToString().ToLowerInvariant()
				});
				if (base.Menu.Page.ScriptManager != null)
				{
					base.Menu.Page.ScriptManager.RegisterClientScriptResource(base.Menu.Page, typeof(Menu), "MenuStandards.js");
					base.Menu.Page.ScriptManager.RegisterStartupScript(base.Menu, typeof(Menu.MenuRendererStandards), key, script, false);
					return;
				}
				base.Menu.Page.ClientScript.RegisterClientScriptResource(base.Menu.Page, typeof(Menu), "MenuStandards.js");
				base.Menu.Page.ClientScript.RegisterStartupScript(typeof(Menu.MenuRendererStandards), key, script);
			}

			// Token: 0x06006BC8 RID: 27592 RVA: 0x00180F9A File Offset: 0x0017F19A
			private void AddStyleBlock()
			{
				if (base.Menu.IncludeStyleBlock)
				{
					base.Menu.Page.Header.Controls.Add(this.CreateStyleBlock());
				}
			}

			// Token: 0x06006BC9 RID: 27593 RVA: 0x00180FCC File Offset: 0x0017F1CC
			private StyleBlock CreateStyleBlock()
			{
				StyleBlock styleBlock = new StyleBlock();
				Style rootMenuItemStyle = base.Menu.RootMenuItemStyle;
				Style style = null;
				if (!base.Menu.ControlStyle.IsEmpty)
				{
					style = new Style();
					style.CopyFrom(base.Menu.ControlStyle);
					style.Font.Reset();
					style.ForeColor = Color.Empty;
				}
				styleBlock.AddStyleDefinition("#{0}", new object[]
				{
					base.Menu.ClientID
				}).AddStyles(style);
				styleBlock.AddStyleDefinition("#{0} img.icon", new object[]
				{
					base.Menu.ClientID
				}).AddStyle(HtmlTextWriterStyle.BorderStyle, "none").AddStyle(HtmlTextWriterStyle.VerticalAlign, "middle");
				styleBlock.AddStyleDefinition("#{0} img.separator", new object[]
				{
					base.Menu.ClientID
				}).AddStyle(HtmlTextWriterStyle.BorderStyle, "none").AddStyle(HtmlTextWriterStyle.Display, "block");
				if (base.Menu.Orientation == Orientation.Horizontal)
				{
					styleBlock.AddStyleDefinition("#{0} img.horizontal-separator", new object[]
					{
						base.Menu.ClientID
					}).AddStyle(HtmlTextWriterStyle.BorderStyle, "none").AddStyle(HtmlTextWriterStyle.VerticalAlign, "middle");
				}
				styleBlock.AddStyleDefinition("#{0} ul", new object[]
				{
					base.Menu.ClientID
				}).AddStyle("list-style", "none").AddStyle(HtmlTextWriterStyle.Margin, "0").AddStyle(HtmlTextWriterStyle.Padding, "0").AddStyle(HtmlTextWriterStyle.Width, "auto");
				styleBlock.AddStyleDefinition("#{0} ul.static", new object[]
				{
					base.Menu.ClientID
				}).AddStyles(base.Menu._staticMenuStyle);
				StyleBlockStyles styleBlockStyles = styleBlock.AddStyleDefinition("#{0} ul.dynamic", new object[]
				{
					base.Menu.ClientID
				}).AddStyles(base.Menu._dynamicMenuStyle).AddStyle(HtmlTextWriterStyle.ZIndex, "1");
				if (base.Menu.DynamicHorizontalOffset != 0)
				{
					styleBlockStyles.AddStyle(HtmlTextWriterStyle.MarginLeft, base.Menu.DynamicHorizontalOffset.ToString(CultureInfo.InvariantCulture) + "px");
				}
				if (base.Menu.DynamicVerticalOffset != 0)
				{
					styleBlockStyles.AddStyle(HtmlTextWriterStyle.MarginTop, base.Menu.DynamicVerticalOffset.ToString(CultureInfo.InvariantCulture) + "px");
				}
				if (base.Menu._levelStyles != null)
				{
					int num = 1;
					foreach (object obj in base.Menu._levelStyles)
					{
						SubMenuStyle style2 = (SubMenuStyle)obj;
						styleBlock.AddStyleDefinition("#{0} ul.level{1}", new object[]
						{
							base.Menu.ClientID,
							num++
						}).AddStyles(style2);
					}
				}
				styleBlock.AddStyleDefinition("#{0} a", new object[]
				{
					base.Menu.ClientID
				}).AddStyle(HtmlTextWriterStyle.WhiteSpace, "nowrap").AddStyle(HtmlTextWriterStyle.Display, "block").AddStyles(rootMenuItemStyle);
				StyleBlockStyles styleBlockStyles2 = styleBlock.AddStyleDefinition("#{0} a.static", new object[]
				{
					base.Menu.ClientID
				});
				if (base.Menu.Orientation == Orientation.Horizontal && (base.Menu._staticItemStyle == null || base.Menu._staticItemStyle.HorizontalPadding.IsEmpty))
				{
					styleBlockStyles2.AddStyle(HtmlTextWriterStyle.PaddingLeft, "0.15em").AddStyle(HtmlTextWriterStyle.PaddingRight, "0.15em");
				}
				styleBlockStyles2.AddStyles(base.Menu._staticItemStyle);
				if (base.Menu._staticItemStyle != null)
				{
					styleBlockStyles2.AddStyles(base.Menu._staticItemStyle.HyperLinkStyle);
				}
				if (!string.IsNullOrEmpty(this.StaticPopOutUrl))
				{
					styleBlock.AddStyleDefinition("#{0} a.popout", new object[]
					{
						base.Menu.ClientID
					}).AddStyle("background-image", "url(\"" + base.Menu.ResolveClientUrl(this.StaticPopOutUrl).Replace("\"", "\\\"") + "\")").AddStyle("background-repeat", "no-repeat").AddStyle("background-position", "right center").AddStyle(HtmlTextWriterStyle.PaddingRight, "14px");
				}
				if (!string.IsNullOrEmpty(this.DynamicPopOutUrl) && this.DynamicPopOutUrl != this.StaticPopOutUrl)
				{
					styleBlock.AddStyleDefinition("#{0} a.popout-dynamic", new object[]
					{
						base.Menu.ClientID
					}).AddStyle("background", "url(\"" + base.Menu.ResolveClientUrl(this.DynamicPopOutUrl).Replace("\"", "\\\"") + "\") no-repeat right center").AddStyle(HtmlTextWriterStyle.PaddingRight, "14px");
				}
				StyleBlockStyles styleBlockStyles3 = styleBlock.AddStyleDefinition("#{0} a.dynamic", new object[]
				{
					base.Menu.ClientID
				}).AddStyles(base.Menu._dynamicItemStyle);
				if (base.Menu._dynamicItemStyle != null)
				{
					styleBlockStyles3.AddStyles(base.Menu._dynamicItemStyle.HyperLinkStyle);
				}
				if (base.Menu._levelMenuItemStyles != null || base.Menu.StaticDisplayLevels > 1)
				{
					int num2 = base.Menu.StaticDisplayLevels;
					if (base.Menu._levelMenuItemStyles != null)
					{
						num2 = Math.Max(num2, base.Menu._levelMenuItemStyles.Count);
					}
					for (int i = 0; i < num2; i++)
					{
						StyleBlockStyles styleBlockStyles4 = styleBlock.AddStyleDefinition("#{0} a.level{1}", new object[]
						{
							base.Menu.ClientID,
							i + 1
						});
						if (i > 0 && i < base.Menu.StaticDisplayLevels)
						{
							Unit staticSubMenuIndent = base.Menu.StaticSubMenuIndent;
							if (staticSubMenuIndent.IsEmpty && base.Menu.Orientation == Orientation.Vertical)
							{
								staticSubMenuIndent = new Unit(1.0, UnitType.Em);
							}
							if (!staticSubMenuIndent.IsEmpty && staticSubMenuIndent.Value != 0.0)
							{
								double num3 = staticSubMenuIndent.Value * (double)i;
								if (num3 < 32767.0)
								{
									staticSubMenuIndent = new Unit(num3, staticSubMenuIndent.Type);
								}
								else
								{
									staticSubMenuIndent = new Unit(32767.0, staticSubMenuIndent.Type);
								}
								styleBlockStyles4.AddStyle(HtmlTextWriterStyle.PaddingLeft, staticSubMenuIndent.ToString(CultureInfo.InvariantCulture));
							}
						}
						if (base.Menu._levelMenuItemStyles != null && i < base.Menu._levelMenuItemStyles.Count)
						{
							MenuItemStyle menuItemStyle = base.Menu._levelMenuItemStyles[i];
							styleBlockStyles4.AddStyles(menuItemStyle).AddStyles(menuItemStyle.HyperLinkStyle);
						}
					}
				}
				styleBlockStyles3 = styleBlock.AddStyleDefinition("#{0} a.static.selected", new object[]
				{
					base.Menu.ClientID
				}).AddStyles(base.Menu._staticSelectedStyle);
				if (base.Menu._staticSelectedStyle != null)
				{
					styleBlockStyles3.AddStyles(base.Menu._staticSelectedStyle.HyperLinkStyle);
				}
				styleBlockStyles3 = styleBlock.AddStyleDefinition("#{0} a.dynamic.selected", new object[]
				{
					base.Menu.ClientID
				}).AddStyles(base.Menu._dynamicSelectedStyle);
				if (base.Menu._dynamicSelectedStyle != null)
				{
					styleBlockStyles3.AddStyles(base.Menu._dynamicSelectedStyle.HyperLinkStyle);
				}
				styleBlock.AddStyleDefinition("#{0} a.static.highlighted", new object[]
				{
					base.Menu.ClientID
				}).AddStyles(base.Menu._staticHoverStyle);
				styleBlock.AddStyleDefinition("#{0} a.dynamic.highlighted", new object[]
				{
					base.Menu.ClientID
				}).AddStyles(base.Menu._dynamicHoverStyle);
				if (base.Menu._levelSelectedStyles != null)
				{
					int num4 = 1;
					foreach (object obj2 in base.Menu._levelSelectedStyles)
					{
						MenuItemStyle menuItemStyle2 = (MenuItemStyle)obj2;
						styleBlock.AddStyleDefinition("#{0} a.selected.level{1}", new object[]
						{
							base.Menu.ClientID,
							num4++
						}).AddStyles(menuItemStyle2).AddStyles(menuItemStyle2.HyperLinkStyle);
					}
				}
				return styleBlock;
			}

			// Token: 0x06006BCA RID: 27594 RVA: 0x00181878 File Offset: 0x0017FA78
			private string GetCssClass(int level, Style staticStyle, Style dynamicStyle, IList levelStyles)
			{
				string text = "level" + level.ToString();
				Style style;
				if (level > base.Menu.StaticDisplayLevels)
				{
					style = dynamicStyle;
				}
				else
				{
					if (base.Menu.DesignMode)
					{
						text += " static";
						if (base.Menu.Orientation == Orientation.Horizontal)
						{
							text += " horizontal";
						}
					}
					style = staticStyle;
				}
				if (style != null && !string.IsNullOrEmpty(style.CssClass))
				{
					text = text + " " + style.CssClass;
				}
				if (levelStyles != null && levelStyles.Count >= level)
				{
					Style style2 = (Style)levelStyles[level - 1];
					if (style2 != null && !string.IsNullOrEmpty(style2.CssClass))
					{
						text = text + " " + style2.CssClass;
					}
				}
				return text;
			}

			// Token: 0x06006BCB RID: 27595 RVA: 0x00181940 File Offset: 0x0017FB40
			protected virtual string GetDynamicPopOutImageUrl()
			{
				string text = base.Menu.DynamicPopOutImageUrl;
				if (string.IsNullOrEmpty(text) && base.Menu.DynamicEnableDefaultPopOutImage)
				{
					text = base.Menu.GetImageUrl(2);
				}
				return text;
			}

			// Token: 0x06006BCC RID: 27596 RVA: 0x0018197C File Offset: 0x0017FB7C
			protected virtual string GetStaticPopOutImageUrl()
			{
				string text = base.Menu.StaticPopOutImageUrl;
				if (string.IsNullOrEmpty(text) && base.Menu.StaticEnableDefaultPopOutImage)
				{
					text = base.Menu.GetImageUrl(2);
				}
				return text;
			}

			// Token: 0x06006BCD RID: 27597 RVA: 0x001819B8 File Offset: 0x0017FBB8
			private string GetMenuCssClass(int level)
			{
				return this.GetCssClass(level, base.Menu.StaticMenuStyle, base.Menu.DynamicMenuStyle, base.Menu._levelStyles);
			}

			// Token: 0x06006BCE RID: 27598 RVA: 0x001819E4 File Offset: 0x0017FBE4
			private string GetMenuItemCssClass(MenuItem item, int level)
			{
				string text = null;
				if (this.ShouldHavePopOutImage(item))
				{
					if (level > base.Menu.StaticDisplayLevels)
					{
						if (!string.IsNullOrEmpty(this.DynamicPopOutUrl))
						{
							text = ((this.DynamicPopOutUrl == this.StaticPopOutUrl) ? "popout" : "popout-dynamic");
						}
					}
					else if (!string.IsNullOrEmpty(this.StaticPopOutUrl))
					{
						text = "popout";
					}
				}
				string cssClass = this.GetCssClass(level, base.Menu.StaticMenuItemStyle, base.Menu.DynamicMenuItemStyle, base.Menu._levelMenuItemStyles);
				if (!string.IsNullOrEmpty(text))
				{
					return text + " " + cssClass;
				}
				return cssClass;
			}

			// Token: 0x06006BCF RID: 27599 RVA: 0x00181A8B File Offset: 0x0017FC8B
			protected virtual string GetPostBackEventReference(MenuItem item)
			{
				return base.Menu.Page.ClientScript.GetPostBackEventReference(base.Menu, item.InternalValuePath, true);
			}

			// Token: 0x06006BD0 RID: 27600 RVA: 0x00181AAF File Offset: 0x0017FCAF
			private bool IsChildPastMaximumDepth(MenuItem item)
			{
				return item.Depth + 1 >= base.Menu.MaximumDepth;
			}

			// Token: 0x06006BD1 RID: 27601 RVA: 0x00181AC9 File Offset: 0x0017FCC9
			private bool IsChildDepthDynamic(MenuItem item)
			{
				return item.Depth + 1 >= base.Menu.StaticDisplayLevels;
			}

			// Token: 0x06006BD2 RID: 27602 RVA: 0x00181AE3 File Offset: 0x0017FCE3
			private bool IsDepthDynamic(MenuItem item)
			{
				return item.Depth >= base.Menu.StaticDisplayLevels;
			}

			// Token: 0x06006BD3 RID: 27603 RVA: 0x00181AFB File Offset: 0x0017FCFB
			private bool IsDepthStatic(MenuItem item)
			{
				return !this.IsDepthDynamic(item);
			}

			// Token: 0x06006BD4 RID: 27604 RVA: 0x00181B08 File Offset: 0x0017FD08
			public override void PreRender(bool registerScript)
			{
				if (base.Menu.DesignMode || base.Menu.Page == null)
				{
					return;
				}
				if (base.Menu.IncludeStyleBlock && base.Menu.Page.Header == null)
				{
					throw new InvalidOperationException(SR.GetString("NeedHeader", new object[]
					{
						"Menu.IncludeStyleBlock"
					}));
				}
				this.AddScriptReference();
				this.AddStyleBlock();
			}

			// Token: 0x06006BD5 RID: 27605 RVA: 0x00181B7C File Offset: 0x0017FD7C
			public override void RenderBeginTag(HtmlTextWriter writer, bool staticOnly)
			{
				ControlRenderingHelper.WriteSkipLinkStart(writer, base.Menu.RenderingCompatibility, base.Menu.DesignMode, base.Menu.SkipLinkText, this.SpacerImageUrl, base.Menu.ClientID);
				if (base.Menu.DesignMode && base.Menu.IncludeStyleBlock)
				{
					this.CreateStyleBlock().Render(writer);
				}
				if (base.Menu.HasAttributes)
				{
					foreach (object obj in base.Menu.Attributes.Keys)
					{
						string text = (string)obj;
						writer.AddAttribute(text, base.Menu.Attributes[text]);
					}
				}
				string text2 = base.Menu.CssClass ?? "";
				if (!base.Menu.Enabled)
				{
					text2 = (text2 + " " + WebControl.DisabledCssClass).Trim();
				}
				if (!string.IsNullOrEmpty(text2))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, text2);
				}
				if (base.Menu.DesignMode)
				{
					writer.AddStyleAttribute("float", "left");
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Id, base.Menu.ClientID);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}

			// Token: 0x06006BD6 RID: 27606 RVA: 0x00181CDC File Offset: 0x0017FEDC
			public override void RenderContents(HtmlTextWriter writer, bool staticOnly)
			{
				this.RenderItems(writer, staticOnly || base.Menu.DesignMode || !base.Menu.Enabled, base.Menu.Items, 1, !string.IsNullOrEmpty(base.Menu.AccessKey));
			}

			// Token: 0x06006BD7 RID: 27607 RVA: 0x00181D30 File Offset: 0x0017FF30
			public override void RenderEndTag(HtmlTextWriter writer, bool staticOnly)
			{
				writer.RenderEndTag();
				if (base.Menu.DesignMode)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Style, "clear: left");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					writer.RenderEndTag();
				}
				ControlRenderingHelper.WriteSkipLinkEnd(writer, base.Menu.DesignMode, base.Menu.SkipLinkText, base.Menu.ClientID);
			}

			// Token: 0x06006BD8 RID: 27608 RVA: 0x00181D94 File Offset: 0x0017FF94
			private bool RenderItem(HtmlTextWriter writer, MenuItem item, int level, string cssClass, bool needsAccessKey)
			{
				this.RenderItemPreSeparator(writer, item);
				if (base.Menu.DesignMode && base.Menu.Orientation == Orientation.Horizontal)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
				}
				needsAccessKey = this.RenderItemLinkAttributes(writer, item, level, cssClass, needsAccessKey);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				this.RenderItemIcon(writer, item);
				item.RenderText(writer);
				writer.RenderEndTag();
				this.RenderItemPostSeparator(writer, item);
				return needsAccessKey;
			}

			// Token: 0x06006BD9 RID: 27609 RVA: 0x00181E08 File Offset: 0x00180008
			private void RenderItemIcon(HtmlTextWriter writer, MenuItem item)
			{
				if (string.IsNullOrEmpty(item.ImageUrl) || !item.NotTemplated())
				{
					return;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Src, base.Menu.ResolveClientUrl(item.ImageUrl));
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, item.ToolTip);
				writer.AddAttribute(HtmlTextWriterAttribute.Title, item.ToolTip);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}

			// Token: 0x06006BDA RID: 27610 RVA: 0x00181E7C File Offset: 0x0018007C
			private bool RenderItemLinkAttributes(HtmlTextWriter writer, MenuItem item, int level, string cssClass, bool needsAccessKey)
			{
				if (!string.IsNullOrEmpty(item.ToolTip))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Title, item.ToolTip);
				}
				if (!item.Enabled || !base.Menu.Enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass + " " + WebControl.DisabledCssClass);
					return needsAccessKey;
				}
				if (!item.Selectable)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
					return needsAccessKey;
				}
				if (item.Selected)
				{
					cssClass += " selected";
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
				if (needsAccessKey)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, base.Menu.AccessKey);
				}
				if (string.IsNullOrEmpty(item.NavigateUrl))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
					writer.AddAttribute(HtmlTextWriterAttribute.Onclick, this.GetPostBackEventReference(item));
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, base.Menu.ResolveClientUrl(item.NavigateUrl));
					string target = item.Target;
					if (string.IsNullOrEmpty(target))
					{
						target = base.Menu.Target;
					}
					if (!string.IsNullOrEmpty(target))
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Target, target);
					}
				}
				return false;
			}

			// Token: 0x06006BDB RID: 27611 RVA: 0x00181F94 File Offset: 0x00180194
			private void RenderItemPostSeparator(HtmlTextWriter writer, MenuItem item)
			{
				string text = item.SeparatorImageUrl;
				if (string.IsNullOrEmpty(text))
				{
					text = (this.IsDepthStatic(item) ? base.Menu.StaticBottomSeparatorImageUrl : base.Menu.DynamicBottomSeparatorImageUrl);
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.RenderItemSeparatorImage(writer, item, text);
				}
			}

			// Token: 0x06006BDC RID: 27612 RVA: 0x00181FE4 File Offset: 0x001801E4
			private void RenderItemPreSeparator(HtmlTextWriter writer, MenuItem item)
			{
				string text = this.IsDepthStatic(item) ? base.Menu.StaticTopSeparatorImageUrl : base.Menu.DynamicTopSeparatorImageUrl;
				if (!string.IsNullOrEmpty(text))
				{
					this.RenderItemSeparatorImage(writer, item, text);
				}
			}

			// Token: 0x06006BDD RID: 27613 RVA: 0x00182024 File Offset: 0x00180224
			private void RenderItemSeparatorImage(HtmlTextWriter writer, MenuItem item, string separatorImageUrl)
			{
				if (base.Menu.RenderingCompatibility >= VersionUtil.Framework45)
				{
					separatorImageUrl = base.Menu.ResolveClientUrl(separatorImageUrl);
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Src, separatorImageUrl);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, (this.IsDepthStatic(item) && base.Menu.Orientation == Orientation.Horizontal) ? "horizontal-separator" : "separator");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}

			// Token: 0x06006BDE RID: 27614 RVA: 0x001820A4 File Offset: 0x001802A4
			private void RenderItems(HtmlTextWriter writer, bool staticOnly, MenuItemCollection items, int level, bool needsAccessKey)
			{
				if (level == 1 || level > base.Menu.StaticDisplayLevels)
				{
					if (base.Menu.DesignMode && base.Menu.Orientation == Orientation.Horizontal)
					{
						writer.AddStyleAttribute("float", "left");
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetMenuCssClass(level));
					writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				}
				foreach (object obj in items)
				{
					MenuItem menuItem = (MenuItem)obj;
					if (base.Menu.DesignMode && base.Menu.Orientation == Orientation.Horizontal)
					{
						writer.AddStyleAttribute("float", "left");
						writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Li);
					needsAccessKey = this.RenderItem(writer, menuItem, level, this.GetMenuItemCssClass(menuItem, level), needsAccessKey);
					if (level < base.Menu.StaticDisplayLevels)
					{
						writer.RenderEndTag();
					}
					if (menuItem.ChildItems.Count > 0 && !this.IsChildPastMaximumDepth(menuItem) && menuItem.Enabled && (level < base.Menu.StaticDisplayLevels || !staticOnly))
					{
						this.RenderItems(writer, staticOnly, menuItem.ChildItems, level + 1, needsAccessKey);
					}
					if (level >= base.Menu.StaticDisplayLevels)
					{
						writer.RenderEndTag();
					}
				}
				if (level == 1 || level > base.Menu.StaticDisplayLevels)
				{
					writer.RenderEndTag();
				}
			}

			// Token: 0x06006BDF RID: 27615 RVA: 0x0018222C File Offset: 0x0018042C
			private bool ShouldHavePopOutImage(MenuItem item)
			{
				return item.ChildItems.Count > 0 && this.IsChildDepthDynamic(item) && !this.IsChildPastMaximumDepth(item);
			}

			// Token: 0x04003954 RID: 14676
			private string _dynamicPopOutUrl;

			// Token: 0x04003955 RID: 14677
			private string _staticPopOutUrl;
		}
	}
}
