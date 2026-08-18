using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000577 RID: 1399
	[Designer("System.Web.UI.Design.WebControls.WebParts.WebPartDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class WebPart : Part, IWebPart, IWebActionable, IWebEditable
	{
		// Token: 0x060046DE RID: 18142 RVA: 0x000EA110 File Offset: 0x000E8310
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

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x060046DF RID: 18143 RVA: 0x000EA170 File Offset: 0x000E8370
		// (set) Token: 0x060046E0 RID: 18144 RVA: 0x000EA178 File Offset: 0x000E8378
		[DefaultValue(true)]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		[WebSysDescription("WebPart_AllowClose")]
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

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x060046E1 RID: 18145 RVA: 0x000EA181 File Offset: 0x000E8381
		// (set) Token: 0x060046E2 RID: 18146 RVA: 0x000EA189 File Offset: 0x000E8389
		[DefaultValue(true)]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
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

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x060046E3 RID: 18147 RVA: 0x000EA192 File Offset: 0x000E8392
		// (set) Token: 0x060046E4 RID: 18148 RVA: 0x000EA19A File Offset: 0x000E839A
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

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x060046E5 RID: 18149 RVA: 0x000EA1A3 File Offset: 0x000E83A3
		// (set) Token: 0x060046E6 RID: 18150 RVA: 0x000EA1AB File Offset: 0x000E83AB
		[DefaultValue(true)]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		[WebSysDescription("WebPart_AllowHide")]
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

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x060046E7 RID: 18151 RVA: 0x000EA1B4 File Offset: 0x000E83B4
		// (set) Token: 0x060046E8 RID: 18152 RVA: 0x000EA1BC File Offset: 0x000E83BC
		[DefaultValue(true)]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		[WebSysDescription("WebPart_AllowMinimize")]
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

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x060046E9 RID: 18153 RVA: 0x000EA1C5 File Offset: 0x000E83C5
		// (set) Token: 0x060046EA RID: 18154 RVA: 0x000EA1CD File Offset: 0x000E83CD
		[DefaultValue(true)]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		[WebSysDescription("WebPart_AllowZoneChange")]
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

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x060046EB RID: 18155 RVA: 0x000EA1D6 File Offset: 0x000E83D6
		// (set) Token: 0x060046EC RID: 18156 RVA: 0x000EA1EC File Offset: 0x000E83EC
		[DefaultValue("")]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		[WebSysDescription("WebPart_AuthorizationFilter")]
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

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x060046ED RID: 18157 RVA: 0x000EA1F5 File Offset: 0x000E83F5
		// (set) Token: 0x060046EE RID: 18158 RVA: 0x000EA20B File Offset: 0x000E840B
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
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

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x060046EF RID: 18159 RVA: 0x000EA23B File Offset: 0x000E843B
		// (set) Token: 0x060046F0 RID: 18160 RVA: 0x000EA243 File Offset: 0x000E8443
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

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x060046F1 RID: 18161 RVA: 0x000EA25F File Offset: 0x000E845F
		// (set) Token: 0x060046F2 RID: 18162 RVA: 0x000EA267 File Offset: 0x000E8467
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

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x060046F3 RID: 18163 RVA: 0x000EA270 File Offset: 0x000E8470
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x000EA286 File Offset: 0x000E8486
		// (set) Token: 0x060046F5 RID: 18165 RVA: 0x000EA28E File Offset: 0x000E848E
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

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x060046F6 RID: 18166 RVA: 0x000E1DD8 File Offset: 0x000DFFD8
		// (set) Token: 0x060046F7 RID: 18167 RVA: 0x000E1DE0 File Offset: 0x000DFFE0
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

		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x060046F8 RID: 18168 RVA: 0x000EA298 File Offset: 0x000E8498
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

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x060046F9 RID: 18169 RVA: 0x000EA2D5 File Offset: 0x000E84D5
		// (set) Token: 0x060046FA RID: 18170 RVA: 0x000EA2E0 File Offset: 0x000E84E0
		[DefaultValue(WebPartExportMode.None)]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		[WebSysDescription("WebPart_ExportMode")]
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

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x000EA342 File Offset: 0x000E8542
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasUserData
		{
			get
			{
				return this._hasUserData;
			}
		}

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x060046FC RID: 18172 RVA: 0x000EA34A File Offset: 0x000E854A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasSharedData
		{
			get
			{
				return this._hasSharedData;
			}
		}

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x060046FD RID: 18173 RVA: 0x000E1E03 File Offset: 0x000E0003
		// (set) Token: 0x060046FE RID: 18174 RVA: 0x000E1E0B File Offset: 0x000E000B
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

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x060046FF RID: 18175 RVA: 0x000EA352 File Offset: 0x000E8552
		// (set) Token: 0x06004700 RID: 18176 RVA: 0x000EA35A File Offset: 0x000E855A
		[DefaultValue(WebPartHelpMode.Navigate)]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		[WebSysDescription("WebPart_HelpMode")]
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

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x06004701 RID: 18177 RVA: 0x000EA376 File Offset: 0x000E8576
		// (set) Token: 0x06004702 RID: 18178 RVA: 0x000EA38C File Offset: 0x000E858C
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x06004703 RID: 18179 RVA: 0x000EA3BC File Offset: 0x000E85BC
		// (set) Token: 0x06004704 RID: 18180 RVA: 0x000EA3C4 File Offset: 0x000E85C4
		[DefaultValue(false)]
		[Personalizable]
		[Themeable(false)]
		[WebCategory("WebPartAppearance")]
		[WebSysDescription("WebPart_Hidden")]
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

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x06004705 RID: 18181 RVA: 0x000EA3CD File Offset: 0x000E85CD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x06004706 RID: 18182 RVA: 0x000EA3D5 File Offset: 0x000E85D5
		internal bool IsOrphaned
		{
			get
			{
				return this.Zone == null && !this.IsClosed;
			}
		}

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x06004707 RID: 18183 RVA: 0x000EA3EA File Offset: 0x000E85EA
		// (set) Token: 0x06004708 RID: 18184 RVA: 0x000EA405 File Offset: 0x000E8605
		[Localizable(true)]
		[WebCategory("WebPartAppearance")]
		[WebSysDefaultValue("WebPart_DefaultImportErrorMessage")]
		[Personalizable(PersonalizationScope.Shared)]
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

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x06004709 RID: 18185 RVA: 0x000EA40E File Offset: 0x000E860E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsShared
		{
			get
			{
				return this._isShared;
			}
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x0600470A RID: 18186 RVA: 0x000EA416 File Offset: 0x000E8616
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsStandalone
		{
			get
			{
				return this._isStandalone;
			}
		}

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x0600470B RID: 18187 RVA: 0x000EA41E File Offset: 0x000E861E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsStatic
		{
			get
			{
				return this._isStatic;
			}
		}

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x0600470C RID: 18188 RVA: 0x00028752 File Offset: 0x00026952
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

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x0600470D RID: 18189 RVA: 0x000EA426 File Offset: 0x000E8626
		// (set) Token: 0x0600470E RID: 18190 RVA: 0x000EA42E File Offset: 0x000E862E
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

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x0600470F RID: 18191 RVA: 0x000EA437 File Offset: 0x000E8637
		internal string TitleBarID
		{
			get
			{
				return "WebPartTitle_" + this.ID;
			}
		}

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x06004710 RID: 18192 RVA: 0x000EA449 File Offset: 0x000E8649
		// (set) Token: 0x06004711 RID: 18193 RVA: 0x000EA45F File Offset: 0x000E865F
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("WebPartAppearance")]
		[Personalizable(PersonalizationScope.Shared)]
		[WebSysDescription("WebPart_TitleIconImageUrl")]
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

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x06004712 RID: 18194 RVA: 0x000EA48F File Offset: 0x000E868F
		// (set) Token: 0x06004713 RID: 18195 RVA: 0x000EA4A5 File Offset: 0x000E86A5
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[Personalizable(PersonalizationScope.Shared)]
		[Themeable(false)]
		[WebCategory("WebPartBehavior")]
		[WebSysDescription("WebPart_TitleUrl")]
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

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x06004714 RID: 18196 RVA: 0x000EA4D5 File Offset: 0x000E86D5
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

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x06004715 RID: 18197 RVA: 0x000EA4F0 File Offset: 0x000E86F0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual WebPartVerbCollection Verbs
		{
			get
			{
				return WebPartVerbCollection.Empty;
			}
		}

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x06004716 RID: 18198 RVA: 0x00004335 File Offset: 0x00002535
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual object WebBrowsableObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x06004717 RID: 18199 RVA: 0x000EA4F7 File Offset: 0x000E86F7
		protected WebPartManager WebPartManager
		{
			get
			{
				return this._webPartManager;
			}
		}

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x06004718 RID: 18200 RVA: 0x000EA4FF File Offset: 0x000E86FF
		internal string WholePartID
		{
			get
			{
				return "WebPart_" + this.ID;
			}
		}

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x06004719 RID: 18201 RVA: 0x000E1E58 File Offset: 0x000E0058
		// (set) Token: 0x0600471A RID: 18202 RVA: 0x000E1E60 File Offset: 0x000E0060
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

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x0600471B RID: 18203 RVA: 0x000EA514 File Offset: 0x000E8714
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

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x0600471C RID: 18204 RVA: 0x000EA562 File Offset: 0x000E8762
		// (set) Token: 0x0600471D RID: 18205 RVA: 0x000EA56A File Offset: 0x000E876A
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

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x0600471E RID: 18206 RVA: 0x000EA588 File Offset: 0x000E8788
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ZoneIndex
		{
			get
			{
				return this._zoneIndex;
			}
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x000EA590 File Offset: 0x000E8790
		public virtual EditorPartCollection CreateEditorParts()
		{
			return EditorPartCollection.Empty;
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x00006164 File Offset: 0x00004364
		protected internal virtual void OnClosing(EventArgs e)
		{
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x00006164 File Offset: 0x00004364
		protected internal virtual void OnConnectModeChanged(EventArgs e)
		{
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x00006164 File Offset: 0x00004364
		protected internal virtual void OnDeleting(EventArgs e)
		{
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x00006164 File Offset: 0x00004364
		protected internal virtual void OnEditModeChanged(EventArgs e)
		{
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x000EA598 File Offset: 0x000E8798
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

		// Token: 0x06004725 RID: 18213 RVA: 0x000EA611 File Offset: 0x000E8811
		internal void SetConnectErrorMessage(string connectErrorMessage)
		{
			if (string.IsNullOrEmpty(this._connectErrorMessage))
			{
				this._connectErrorMessage = connectErrorMessage;
			}
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x000EA627 File Offset: 0x000E8827
		internal void SetHasUserData(bool hasUserData)
		{
			this._hasUserData = hasUserData;
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x000EA630 File Offset: 0x000E8830
		internal void SetHasSharedData(bool hasSharedData)
		{
			this._hasSharedData = hasSharedData;
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x000EA639 File Offset: 0x000E8839
		internal void SetIsClosed(bool isClosed)
		{
			this._isClosed = isClosed;
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x000EA642 File Offset: 0x000E8842
		internal void SetIsShared(bool isShared)
		{
			this._isShared = isShared;
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x000EA64B File Offset: 0x000E884B
		internal void SetIsStandalone(bool isStandalone)
		{
			this._isStandalone = isStandalone;
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x000EA654 File Offset: 0x000E8854
		internal void SetIsStatic(bool isStatic)
		{
			this._isStatic = isStatic;
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x000EA65D File Offset: 0x000E885D
		protected void SetPersonalizationDirty()
		{
			if (this.WebPartManager == null)
			{
				throw new InvalidOperationException(SR.GetString("WebPartManagerRequired"));
			}
			this.WebPartManager.Personalization.SetDirty(this);
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x000EA688 File Offset: 0x000E8888
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

		// Token: 0x0600472E RID: 18222 RVA: 0x000EA713 File Offset: 0x000E8913
		internal void SetWebPartManager(WebPartManager webPartManager)
		{
			this._webPartManager = webPartManager;
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x000EA71C File Offset: 0x000E891C
		internal void SetZoneIndex(int zoneIndex)
		{
			if (zoneIndex < 0)
			{
				throw new ArgumentOutOfRangeException("zoneIndex");
			}
			this._zoneIndex = zoneIndex;
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x000EA734 File Offset: 0x000E8934
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

		// Token: 0x06004731 RID: 18225 RVA: 0x000EA768 File Offset: 0x000E8968
		protected override void TrackViewState()
		{
			if (this.WebPartManager != null)
			{
				this.WebPartManager.Personalization.ApplyPersonalizationState(this);
			}
			base.TrackViewState();
		}

		// Token: 0x040026C6 RID: 9926
		private WebPartManager _webPartManager;

		// Token: 0x040026C7 RID: 9927
		private string _zoneID;

		// Token: 0x040026C8 RID: 9928
		private int _zoneIndex;

		// Token: 0x040026C9 RID: 9929
		private WebPartZoneBase _zone;

		// Token: 0x040026CA RID: 9930
		private bool _allowClose;

		// Token: 0x040026CB RID: 9931
		private bool _allowConnect;

		// Token: 0x040026CC RID: 9932
		private bool _allowEdit;

		// Token: 0x040026CD RID: 9933
		private bool _allowHide;

		// Token: 0x040026CE RID: 9934
		private bool _allowMinimize;

		// Token: 0x040026CF RID: 9935
		private bool _allowZoneChange;

		// Token: 0x040026D0 RID: 9936
		private string _authorizationFilter;

		// Token: 0x040026D1 RID: 9937
		private string _catalogIconImageUrl;

		// Token: 0x040026D2 RID: 9938
		private PartChromeState _chromeState;

		// Token: 0x040026D3 RID: 9939
		private string _connectErrorMessage;

		// Token: 0x040026D4 RID: 9940
		private WebPartExportMode _exportMode;

		// Token: 0x040026D5 RID: 9941
		private WebPartHelpMode _helpMode;

		// Token: 0x040026D6 RID: 9942
		private string _helpUrl;

		// Token: 0x040026D7 RID: 9943
		private bool _hidden;

		// Token: 0x040026D8 RID: 9944
		private string _importErrorMessage;

		// Token: 0x040026D9 RID: 9945
		private string _titleIconImageUrl;

		// Token: 0x040026DA RID: 9946
		private string _titleUrl;

		// Token: 0x040026DB RID: 9947
		private bool _hasUserData;

		// Token: 0x040026DC RID: 9948
		private bool _hasSharedData;

		// Token: 0x040026DD RID: 9949
		private bool _isClosed;

		// Token: 0x040026DE RID: 9950
		private bool _isShared;

		// Token: 0x040026DF RID: 9951
		private bool _isStandalone;

		// Token: 0x040026E0 RID: 9952
		private bool _isStatic;

		// Token: 0x040026E1 RID: 9953
		private Dictionary<ProviderConnectionPoint, int> _trackerCounter;

		// Token: 0x040026E2 RID: 9954
		internal const string WholePartIDPrefix = "WebPart_";

		// Token: 0x040026E3 RID: 9955
		private const string titleBarIDPrefix = "WebPartTitle_";

		// Token: 0x020009F4 RID: 2548
		internal sealed class ZoneIndexComparer : IComparer
		{
			// Token: 0x06006D32 RID: 27954 RVA: 0x00186F04 File Offset: 0x00185104
			public int Compare(object x, object y)
			{
				WebPart webPart = (WebPart)x;
				WebPart webPart2 = (WebPart)y;
				int num = webPart.ZoneIndex - webPart2.ZoneIndex;
				if (num == 0)
				{
					num = string.Compare(webPart.ID, webPart2.ID, StringComparison.CurrentCulture);
				}
				return num;
			}
		}
	}
}
