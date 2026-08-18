using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020006C2 RID: 1730
	[Designer("System.Web.UI.Design.WebControls.WebParts.WebPartDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class WebPart : Part, IWebPart, IWebActionable, IWebEditable
	{
		// Token: 0x060054EF RID: 21743 RVA: 0x00159530 File Offset: 0x00158530
		protected WebPart()
		{
			this._allowClose = true;
			this._allowConnect = true;
			this._allowEdit = true;
			this._allowHide = true;
			this._allowMinimize = true;
			this._allowZoneChange = true;
			this._chromeState = PartChromeState.Normal;
			this._exportMode = WebPartExportMode.None;
			this._helpMode = WebPartHelpMode.Navigate;
			this._isStatic = true;
			this._isStandalone = true;
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x060054F0 RID: 21744 RVA: 0x00159590 File Offset: 0x00158590
		// (set) Token: 0x060054F1 RID: 21745 RVA: 0x00159598 File Offset: 0x00158598
		[DefaultValue(true)]
		[WebSysDescription("WebPart_AllowClose")]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		public virtual bool AllowClose
		{
			get
			{
				return this._allowClose;
			}
			set
			{
				this._allowClose = value;
			}
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x060054F2 RID: 21746 RVA: 0x001595A1 File Offset: 0x001585A1
		// (set) Token: 0x060054F3 RID: 21747 RVA: 0x001595A9 File Offset: 0x001585A9
		[WebCategory("WebPartBehavior")]
		[DefaultValue(true)]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebSysDescription("WebPart_AllowConnect")]
		public virtual bool AllowConnect
		{
			get
			{
				return this._allowConnect;
			}
			set
			{
				this._allowConnect = value;
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x060054F4 RID: 21748 RVA: 0x001595B2 File Offset: 0x001585B2
		// (set) Token: 0x060054F5 RID: 21749 RVA: 0x001595BA File Offset: 0x001585BA
		[DefaultValue(true)]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		[WebSysDescription("WebPart_AllowEdit")]
		public virtual bool AllowEdit
		{
			get
			{
				return this._allowEdit;
			}
			set
			{
				this._allowEdit = value;
			}
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x060054F6 RID: 21750 RVA: 0x001595C3 File Offset: 0x001585C3
		// (set) Token: 0x060054F7 RID: 21751 RVA: 0x001595CB File Offset: 0x001585CB
		[WebSysDescription("WebPart_AllowHide")]
		[DefaultValue(true)]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		public virtual bool AllowHide
		{
			get
			{
				return this._allowHide;
			}
			set
			{
				this._allowHide = value;
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x060054F8 RID: 21752 RVA: 0x001595D4 File Offset: 0x001585D4
		// (set) Token: 0x060054F9 RID: 21753 RVA: 0x001595DC File Offset: 0x001585DC
		[WebSysDescription("WebPart_AllowMinimize")]
		[DefaultValue(true)]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		public virtual bool AllowMinimize
		{
			get
			{
				return this._allowMinimize;
			}
			set
			{
				this._allowMinimize = value;
			}
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x060054FA RID: 21754 RVA: 0x001595E5 File Offset: 0x001585E5
		// (set) Token: 0x060054FB RID: 21755 RVA: 0x001595ED File Offset: 0x001585ED
		[Personalizable(PersonalizationScope.Shared)]
		[DefaultValue(true)]
		[WebSysDescription("WebPart_AllowZoneChange")]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		public virtual bool AllowZoneChange
		{
			get
			{
				return this._allowZoneChange;
			}
			set
			{
				this._allowZoneChange = value;
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x060054FC RID: 21756 RVA: 0x001595F6 File Offset: 0x001585F6
		// (set) Token: 0x060054FD RID: 21757 RVA: 0x0015960C File Offset: 0x0015860C
		[DefaultValue("")]
		[WebSysDescription("WebPart_AuthorizationFilter")]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		public virtual string AuthorizationFilter
		{
			get
			{
				if (this._authorizationFilter == null)
				{
					return string.Empty;
				}
				return this._authorizationFilter;
			}
			set
			{
				this._authorizationFilter = value;
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x060054FE RID: 21758 RVA: 0x00159615 File Offset: 0x00158615
		// (set) Token: 0x060054FF RID: 21759 RVA: 0x0015962C File Offset: 0x0015862C
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("WebPartAppearance")]
		[Personalizable(PersonalizationScope.Shared)]
		[WebSysDescription("WebPart_CatalogIconImageUrl")]
		public virtual string CatalogIconImageUrl
		{
			get
			{
				if (this._catalogIconImageUrl == null)
				{
					return string.Empty;
				}
				return this._catalogIconImageUrl;
			}
			set
			{
				if (CrossSiteScriptingValidation.IsDangerousUrl(value))
				{
					throw new ArgumentException(SR.GetString("WebPart_BadUrl", new object[]
					{
						value
					}), "value");
				}
				this._catalogIconImageUrl = value;
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x06005500 RID: 21760 RVA: 0x00159669 File Offset: 0x00158669
		// (set) Token: 0x06005501 RID: 21761 RVA: 0x00159671 File Offset: 0x00158671
		[Personalizable]
		public override PartChromeState ChromeState
		{
			get
			{
				return this._chromeState;
			}
			set
			{
				if (value < PartChromeState.Normal || value > PartChromeState.Minimized)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._chromeState = value;
			}
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x06005502 RID: 21762 RVA: 0x0015968D File Offset: 0x0015868D
		// (set) Token: 0x06005503 RID: 21763 RVA: 0x00159695 File Offset: 0x00158695
		[Personalizable]
		public override PartChromeType ChromeType
		{
			get
			{
				return base.ChromeType;
			}
			set
			{
				base.ChromeType = value;
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06005504 RID: 21764 RVA: 0x0015969E File Offset: 0x0015869E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string ConnectErrorMessage
		{
			get
			{
				if (this._connectErrorMessage == null)
				{
					return string.Empty;
				}
				return this._connectErrorMessage;
			}
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06005505 RID: 21765 RVA: 0x001596B4 File Offset: 0x001586B4
		// (set) Token: 0x06005506 RID: 21766 RVA: 0x001596BC File Offset: 0x001586BC
		[Personalizable(PersonalizationScope.Shared)]
		public override string Description
		{
			get
			{
				return base.Description;
			}
			set
			{
				base.Description = value;
			}
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06005507 RID: 21767 RVA: 0x001596C5 File Offset: 0x001586C5
		// (set) Token: 0x06005508 RID: 21768 RVA: 0x001596CD File Offset: 0x001586CD
		[Personalizable]
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

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06005509 RID: 21769 RVA: 0x001596D8 File Offset: 0x001586D8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DisplayTitle
		{
			get
			{
				if (this._webPartManager != null)
				{
					return this._webPartManager.GetDisplayTitle(this);
				}
				string text = this.Title;
				if (string.IsNullOrEmpty(text))
				{
					text = SR.GetString("Part_Untitled");
				}
				return text;
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x0600550A RID: 21770 RVA: 0x00159715 File Offset: 0x00158715
		// (set) Token: 0x0600550B RID: 21771 RVA: 0x00159720 File Offset: 0x00158720
		[WebSysDescription("WebPart_ExportMode")]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		[DefaultValue(WebPartExportMode.None)]
		public virtual WebPartExportMode ExportMode
		{
			get
			{
				return this._exportMode;
			}
			set
			{
				if (base.ControlState >= ControlState.Loaded && (this.WebPartManager == null || (this.WebPartManager.Personalization.Scope == PersonalizationScope.User && this.IsShared)))
				{
					throw new InvalidOperationException(SR.GetString("WebPart_CantSetExportMode"));
				}
				if (value < WebPartExportMode.None || value > WebPartExportMode.NonSensitiveData)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._exportMode = value;
			}
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x0600550C RID: 21772 RVA: 0x00159782 File Offset: 0x00158782
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasUserData
		{
			get
			{
				return this._hasUserData;
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x0600550D RID: 21773 RVA: 0x0015978A File Offset: 0x0015878A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasSharedData
		{
			get
			{
				return this._hasSharedData;
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x0600550E RID: 21774 RVA: 0x00159792 File Offset: 0x00158792
		// (set) Token: 0x0600550F RID: 21775 RVA: 0x0015979A File Offset: 0x0015879A
		[Personalizable]
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

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06005510 RID: 21776 RVA: 0x001597A3 File Offset: 0x001587A3
		// (set) Token: 0x06005511 RID: 21777 RVA: 0x001597AB File Offset: 0x001587AB
		[WebSysDescription("WebPart_HelpMode")]
		[DefaultValue(WebPartHelpMode.Navigate)]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		public virtual WebPartHelpMode HelpMode
		{
			get
			{
				return this._helpMode;
			}
			set
			{
				if (value < WebPartHelpMode.Modal || value > WebPartHelpMode.Navigate)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._helpMode = value;
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06005512 RID: 21778 RVA: 0x001597C7 File Offset: 0x001587C7
		// (set) Token: 0x06005513 RID: 21779 RVA: 0x001597E0 File Offset: 0x001587E0
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[UrlProperty]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		[WebSysDescription("WebPart_HelpUrl")]
		public virtual string HelpUrl
		{
			get
			{
				if (this._helpUrl == null)
				{
					return string.Empty;
				}
				return this._helpUrl;
			}
			set
			{
				if (CrossSiteScriptingValidation.IsDangerousUrl(value))
				{
					throw new ArgumentException(SR.GetString("WebPart_BadUrl", new object[]
					{
						value
					}), "value");
				}
				this._helpUrl = value;
			}
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06005514 RID: 21780 RVA: 0x0015981D File Offset: 0x0015881D
		// (set) Token: 0x06005515 RID: 21781 RVA: 0x00159825 File Offset: 0x00158825
		[WebSysDescription("WebPart_Hidden")]
		[DefaultValue(false)]
		[Personalizable]
		[Themeable(false)]
		[WebCategory("WebPartAppearance")]
		public virtual bool Hidden
		{
			get
			{
				return this._hidden;
			}
			set
			{
				this._hidden = value;
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06005516 RID: 21782 RVA: 0x0015982E File Offset: 0x0015882E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x06005517 RID: 21783 RVA: 0x00159836 File Offset: 0x00158836
		internal bool IsOrphaned
		{
			get
			{
				return this.Zone == null && !this.IsClosed;
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06005518 RID: 21784 RVA: 0x0015984B File Offset: 0x0015884B
		// (set) Token: 0x06005519 RID: 21785 RVA: 0x00159866 File Offset: 0x00158866
		[Personalizable(PersonalizationScope.Shared)]
		[Localizable(true)]
		[WebCategory("WebPartAppearance")]
		[WebSysDefaultValue("WebPart_DefaultImportErrorMessage")]
		[WebSysDescription("WebPart_ImportErrorMessage")]
		public virtual string ImportErrorMessage
		{
			get
			{
				if (this._importErrorMessage == null)
				{
					return SR.GetString("WebPart_DefaultImportErrorMessage");
				}
				return this._importErrorMessage;
			}
			set
			{
				this._importErrorMessage = value;
			}
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x0600551A RID: 21786 RVA: 0x0015986F File Offset: 0x0015886F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsShared
		{
			get
			{
				return this._isShared;
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x0600551B RID: 21787 RVA: 0x00159877 File Offset: 0x00158877
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsStandalone
		{
			get
			{
				return this._isStandalone;
			}
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x0600551C RID: 21788 RVA: 0x0015987F File Offset: 0x0015887F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsStatic
		{
			get
			{
				return this._isStatic;
			}
		}

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x0600551D RID: 21789 RVA: 0x00159887 File Offset: 0x00158887
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		public virtual string Subtitle
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x0600551E RID: 21790 RVA: 0x0015988E File Offset: 0x0015888E
		// (set) Token: 0x0600551F RID: 21791 RVA: 0x00159896 File Offset: 0x00158896
		[Personalizable]
		public override string Title
		{
			get
			{
				return base.Title;
			}
			set
			{
				base.Title = value;
			}
		}

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06005520 RID: 21792 RVA: 0x0015989F File Offset: 0x0015889F
		internal string TitleBarID
		{
			get
			{
				return "WebPartTitle_" + this.ID;
			}
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06005521 RID: 21793 RVA: 0x001598B1 File Offset: 0x001588B1
		// (set) Token: 0x06005522 RID: 21794 RVA: 0x001598C8 File Offset: 0x001588C8
		[DefaultValue("")]
		[Personalizable(PersonalizationScope.Shared)]
		[WebSysDescription("WebPart_TitleIconImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("WebPartAppearance")]
		public virtual string TitleIconImageUrl
		{
			get
			{
				if (this._titleIconImageUrl == null)
				{
					return string.Empty;
				}
				return this._titleIconImageUrl;
			}
			set
			{
				if (CrossSiteScriptingValidation.IsDangerousUrl(value))
				{
					throw new ArgumentException(SR.GetString("WebPart_BadUrl", new object[]
					{
						value
					}), "value");
				}
				this._titleIconImageUrl = value;
			}
		}

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x06005523 RID: 21795 RVA: 0x00159905 File Offset: 0x00158905
		// (set) Token: 0x06005524 RID: 21796 RVA: 0x0015991C File Offset: 0x0015891C
		[WebSysDescription("WebPart_TitleUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string TitleUrl
		{
			get
			{
				if (this._titleUrl == null)
				{
					return string.Empty;
				}
				return this._titleUrl;
			}
			set
			{
				if (CrossSiteScriptingValidation.IsDangerousUrl(value))
				{
					throw new ArgumentException(SR.GetString("WebPart_BadUrl", new object[]
					{
						value
					}), "value");
				}
				this._titleUrl = value;
			}
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x06005525 RID: 21797 RVA: 0x00159959 File Offset: 0x00158959
		internal Dictionary<ProviderConnectionPoint, int> TrackerCounter
		{
			get
			{
				if (this._trackerCounter == null)
				{
					this._trackerCounter = new Dictionary<ProviderConnectionPoint, int>();
				}
				return this._trackerCounter;
			}
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06005526 RID: 21798 RVA: 0x00159974 File Offset: 0x00158974
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual WebPartVerbCollection Verbs
		{
			get
			{
				return WebPartVerbCollection.Empty;
			}
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06005527 RID: 21799 RVA: 0x0015997B File Offset: 0x0015897B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual object WebBrowsableObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06005528 RID: 21800 RVA: 0x0015997E File Offset: 0x0015897E
		protected WebPartManager WebPartManager
		{
			get
			{
				return this._webPartManager;
			}
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06005529 RID: 21801 RVA: 0x00159986 File Offset: 0x00158986
		internal string WholePartID
		{
			get
			{
				return "WebPart_" + this.ID;
			}
		}

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x0600552A RID: 21802 RVA: 0x00159998 File Offset: 0x00158998
		// (set) Token: 0x0600552B RID: 21803 RVA: 0x001599A0 File Offset: 0x001589A0
		[Personalizable]
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

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x0600552C RID: 21804 RVA: 0x001599AC File Offset: 0x001589AC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPartZoneBase Zone
		{
			get
			{
				if (this._zone == null)
				{
					string zoneID = this.ZoneID;
					if (!string.IsNullOrEmpty(zoneID) && this.WebPartManager != null)
					{
						WebPartZoneCollection zones = this.WebPartManager.Zones;
						if (zones != null)
						{
							this._zone = zones[zoneID];
						}
					}
				}
				return this._zone;
			}
		}

		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x0600552D RID: 21805 RVA: 0x001599FA File Offset: 0x001589FA
		// (set) Token: 0x0600552E RID: 21806 RVA: 0x00159A02 File Offset: 0x00158A02
		internal string ZoneID
		{
			get
			{
				return this._zoneID;
			}
			set
			{
				if (this.ZoneID != value)
				{
					this._zoneID = value;
					this._zone = null;
				}
			}
		}

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x0600552F RID: 21807 RVA: 0x00159A20 File Offset: 0x00158A20
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int ZoneIndex
		{
			get
			{
				return this._zoneIndex;
			}
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x00159A28 File Offset: 0x00158A28
		public virtual EditorPartCollection CreateEditorParts()
		{
			return EditorPartCollection.Empty;
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x00159A2F File Offset: 0x00158A2F
		protected internal virtual void OnClosing(EventArgs e)
		{
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x00159A31 File Offset: 0x00158A31
		protected internal virtual void OnConnectModeChanged(EventArgs e)
		{
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x00159A33 File Offset: 0x00158A33
		protected internal virtual void OnDeleting(EventArgs e)
		{
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x00159A35 File Offset: 0x00158A35
		protected internal virtual void OnEditModeChanged(EventArgs e)
		{
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x00159A38 File Offset: 0x00158A38
		internal override void PreRenderRecursiveInternal()
		{
			if (this.IsStandalone)
			{
				if (this.Hidden)
				{
					throw new InvalidOperationException(SR.GetString("WebPart_NotStandalone", new object[]
					{
						"Hidden",
						this.ID
					}));
				}
			}
			else if (!this.Visible)
			{
				throw new InvalidOperationException(SR.GetString("WebPart_OnlyStandalone", new object[]
				{
					"Visible",
					this.ID
				}));
			}
			base.PreRenderRecursiveInternal();
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x00159AB5 File Offset: 0x00158AB5
		internal void SetConnectErrorMessage(string connectErrorMessage)
		{
			if (string.IsNullOrEmpty(this._connectErrorMessage))
			{
				this._connectErrorMessage = connectErrorMessage;
			}
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x00159ACB File Offset: 0x00158ACB
		internal void SetHasUserData(bool hasUserData)
		{
			this._hasUserData = hasUserData;
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x00159AD4 File Offset: 0x00158AD4
		internal void SetHasSharedData(bool hasSharedData)
		{
			this._hasSharedData = hasSharedData;
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x00159ADD File Offset: 0x00158ADD
		internal void SetIsClosed(bool isClosed)
		{
			this._isClosed = isClosed;
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x00159AE6 File Offset: 0x00158AE6
		internal void SetIsShared(bool isShared)
		{
			this._isShared = isShared;
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x00159AEF File Offset: 0x00158AEF
		internal void SetIsStandalone(bool isStandalone)
		{
			this._isStandalone = isStandalone;
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x00159AF8 File Offset: 0x00158AF8
		internal void SetIsStatic(bool isStatic)
		{
			this._isStatic = isStatic;
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x00159B01 File Offset: 0x00158B01
		protected void SetPersonalizationDirty()
		{
			if (this.WebPartManager == null)
			{
				throw new InvalidOperationException(SR.GetString("WebPartManagerRequired"));
			}
			this.WebPartManager.Personalization.SetDirty(this);
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x00159B2C File Offset: 0x00158B2C
		public static void SetPersonalizationDirty(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.Page == null)
			{
				throw new ArgumentException(SR.GetString("PropertyCannotBeNull", new object[]
				{
					"Page"
				}), "control");
			}
			WebPartManager currentWebPartManager = WebPartManager.GetCurrentWebPartManager(control.Page);
			if (currentWebPartManager == null)
			{
				throw new InvalidOperationException(SR.GetString("WebPartManagerRequired"));
			}
			WebPart genericWebPart = currentWebPartManager.GetGenericWebPart(control);
			if (genericWebPart == null)
			{
				throw new ArgumentException(SR.GetString("WebPart_NonWebPart"), "control");
			}
			genericWebPart.SetPersonalizationDirty();
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x00159BB9 File Offset: 0x00158BB9
		internal void SetWebPartManager(WebPartManager webPartManager)
		{
			this._webPartManager = webPartManager;
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x00159BC2 File Offset: 0x00158BC2
		internal void SetZoneIndex(int zoneIndex)
		{
			if (zoneIndex < 0)
			{
				throw new ArgumentOutOfRangeException("zoneIndex");
			}
			this._zoneIndex = zoneIndex;
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x00159BDC File Offset: 0x00158BDC
		internal Control ToControl()
		{
			GenericWebPart genericWebPart = this as GenericWebPart;
			if (genericWebPart == null)
			{
				return this;
			}
			Control childControl = genericWebPart.ChildControl;
			if (childControl != null)
			{
				return childControl;
			}
			throw new InvalidOperationException(SR.GetString("GenericWebPart_ChildControlIsNull"));
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x00159C10 File Offset: 0x00158C10
		protected override void TrackViewState()
		{
			if (this.WebPartManager != null)
			{
				this.WebPartManager.Personalization.ApplyPersonalizationState(this);
			}
			base.TrackViewState();
		}

		// Token: 0x04002EF8 RID: 12024
		internal const string WholePartIDPrefix = "WebPart_";

		// Token: 0x04002EF9 RID: 12025
		private const string titleBarIDPrefix = "WebPartTitle_";

		// Token: 0x04002EFA RID: 12026
		private WebPartManager _webPartManager;

		// Token: 0x04002EFB RID: 12027
		private string _zoneID;

		// Token: 0x04002EFC RID: 12028
		private int _zoneIndex;

		// Token: 0x04002EFD RID: 12029
		private WebPartZoneBase _zone;

		// Token: 0x04002EFE RID: 12030
		private bool _allowClose;

		// Token: 0x04002EFF RID: 12031
		private bool _allowConnect;

		// Token: 0x04002F00 RID: 12032
		private bool _allowEdit;

		// Token: 0x04002F01 RID: 12033
		private bool _allowHide;

		// Token: 0x04002F02 RID: 12034
		private bool _allowMinimize;

		// Token: 0x04002F03 RID: 12035
		private bool _allowZoneChange;

		// Token: 0x04002F04 RID: 12036
		private string _authorizationFilter;

		// Token: 0x04002F05 RID: 12037
		private string _catalogIconImageUrl;

		// Token: 0x04002F06 RID: 12038
		private PartChromeState _chromeState;

		// Token: 0x04002F07 RID: 12039
		private string _connectErrorMessage;

		// Token: 0x04002F08 RID: 12040
		private WebPartExportMode _exportMode;

		// Token: 0x04002F09 RID: 12041
		private WebPartHelpMode _helpMode;

		// Token: 0x04002F0A RID: 12042
		private string _helpUrl;

		// Token: 0x04002F0B RID: 12043
		private bool _hidden;

		// Token: 0x04002F0C RID: 12044
		private string _importErrorMessage;

		// Token: 0x04002F0D RID: 12045
		private string _titleIconImageUrl;

		// Token: 0x04002F0E RID: 12046
		private string _titleUrl;

		// Token: 0x04002F0F RID: 12047
		private bool _hasUserData;

		// Token: 0x04002F10 RID: 12048
		private bool _hasSharedData;

		// Token: 0x04002F11 RID: 12049
		private bool _isClosed;

		// Token: 0x04002F12 RID: 12050
		private bool _isShared;

		// Token: 0x04002F13 RID: 12051
		private bool _isStandalone;

		// Token: 0x04002F14 RID: 12052
		private bool _isStatic;

		// Token: 0x04002F15 RID: 12053
		private Dictionary<ProviderConnectionPoint, int> _trackerCounter;

		// Token: 0x020006C3 RID: 1731
		internal sealed class ZoneIndexComparer : IComparer
		{
			// Token: 0x06005543 RID: 21827 RVA: 0x00159C34 File Offset: 0x00158C34
			public int Compare(object x, object y)
			{
				WebPart webPart = (WebPart)x;
				WebPart webPart2 = (WebPart)y;
				int num = webPart.ZoneIndex - webPart2.ZoneIndex;
				if (num == 0)
				{
					num = webPart.ID.CompareTo(webPart2.ID);
				}
				return num;
			}
		}
	}
}
