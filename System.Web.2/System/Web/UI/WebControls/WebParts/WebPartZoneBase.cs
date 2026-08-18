using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005B9 RID: 1465
	[Designer("System.Web.UI.Design.WebControls.WebParts.WebPartZoneBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class WebPartZoneBase : WebZone, IPostBackEventHandler, IWebPartMenuUser
	{
		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x060049FB RID: 18939 RVA: 0x000F56F0 File Offset: 0x000F38F0
		// (set) Token: 0x060049FC RID: 18940 RVA: 0x000F5719 File Offset: 0x000F3919
		[DefaultValue(true)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("WebPartZoneBase_AllowLayoutChange")]
		public virtual bool AllowLayoutChange
		{
			get
			{
				object obj = this.ViewState["AllowLayoutChange"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AllowLayoutChange"] = value;
			}
		}

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x060049FD RID: 18941 RVA: 0x000F5731 File Offset: 0x000F3931
		// (set) Token: 0x060049FE RID: 18942 RVA: 0x0009E7E0 File Offset: 0x0009C9E0
		[DefaultValue(typeof(Color), "Gray")]
		public override Color BorderColor
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return Color.Gray;
				}
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
			}
		}

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x060049FF RID: 18943 RVA: 0x000F5747 File Offset: 0x000F3947
		// (set) Token: 0x06004A00 RID: 18944 RVA: 0x0009E7F1 File Offset: 0x0009C9F1
		[DefaultValue(BorderStyle.Solid)]
		public override BorderStyle BorderStyle
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return BorderStyle.Solid;
				}
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x06004A01 RID: 18945 RVA: 0x000F5759 File Offset: 0x000F3959
		// (set) Token: 0x06004A02 RID: 18946 RVA: 0x0009E802 File Offset: 0x0009CA02
		[DefaultValue(typeof(Unit), "1")]
		public override Unit BorderWidth
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return 1;
				}
				return base.BorderWidth;
			}
			set
			{
				base.BorderWidth = value;
			}
		}

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06004A03 RID: 18947 RVA: 0x000F5770 File Offset: 0x000F3970
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("WebPartZoneBase_CloseVerb")]
		public virtual WebPartVerb CloseVerb
		{
			get
			{
				if (this._closeVerb == null)
				{
					this._closeVerb = new WebPartCloseVerb();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._closeVerb).TrackViewState();
					}
				}
				return this._closeVerb;
			}
		}

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06004A04 RID: 18948 RVA: 0x000F579E File Offset: 0x000F399E
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("WebPartZoneBase_ConnectVerb")]
		public virtual WebPartVerb ConnectVerb
		{
			get
			{
				if (this._connectVerb == null)
				{
					this._connectVerb = new WebPartConnectVerb();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._connectVerb).TrackViewState();
					}
				}
				return this._connectVerb;
			}
		}

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x06004A05 RID: 18949 RVA: 0x000F57CC File Offset: 0x000F39CC
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("WebPartZoneBase_DeleteVerb")]
		public virtual WebPartVerb DeleteVerb
		{
			get
			{
				if (this._deleteVerb == null)
				{
					this._deleteVerb = new WebPartDeleteVerb();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._deleteVerb).TrackViewState();
					}
				}
				return this._deleteVerb;
			}
		}

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06004A06 RID: 18950 RVA: 0x000F57FC File Offset: 0x000F39FC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string DisplayTitle
		{
			get
			{
				string headerText = this.HeaderText;
				if (!string.IsNullOrEmpty(headerText))
				{
					return headerText;
				}
				string id = this.ID;
				if (!string.IsNullOrEmpty(id))
				{
					return id;
				}
				int num = 1;
				if (base.WebPartManager != null)
				{
					num = base.WebPartManager.Zones.IndexOf(this) + 1;
				}
				return SR.GetString("WebPartZoneBase_DisplayTitleFallback", new object[]
				{
					num.ToString(CultureInfo.CurrentCulture)
				});
			}
		}

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x06004A07 RID: 18951 RVA: 0x000F5868 File Offset: 0x000F3A68
		protected internal bool DragDropEnabled
		{
			get
			{
				return !base.DesignMode && base.RenderClientScript && this.AllowLayoutChange && base.WebPartManager != null && base.WebPartManager.DisplayMode.AllowPageDesign;
			}
		}

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x06004A08 RID: 18952 RVA: 0x000F589C File Offset: 0x000F3A9C
		// (set) Token: 0x06004A09 RID: 18953 RVA: 0x000F58D4 File Offset: 0x000F3AD4
		[DefaultValue(typeof(Color), "Blue")]
		[TypeConverter(typeof(WebColorConverter))]
		[WebCategory("Appearance")]
		[WebSysDescription("WebPartZoneBase_DragHighlightColor")]
		public virtual Color DragHighlightColor
		{
			get
			{
				object obj = this.ViewState["DragHighlightColor"];
				if (obj != null)
				{
					Color result = (Color)obj;
					if (!result.IsEmpty)
					{
						return result;
					}
				}
				return Color.Blue;
			}
			set
			{
				this.ViewState["DragHighlightColor"] = value;
			}
		}

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06004A0A RID: 18954 RVA: 0x000F58EC File Offset: 0x000F3AEC
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("WebPartZoneBase_EditVerb")]
		public virtual WebPartVerb EditVerb
		{
			get
			{
				if (this._editVerb == null)
				{
					this._editVerb = new WebPartEditVerb();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._editVerb).TrackViewState();
					}
				}
				return this._editVerb;
			}
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06004A0B RID: 18955 RVA: 0x000F591C File Offset: 0x000F3B1C
		// (set) Token: 0x06004A0C RID: 18956 RVA: 0x000DD332 File Offset: 0x000DB532
		[WebSysDefaultValue("WebPartZoneBase_DefaultEmptyZoneText")]
		public override string EmptyZoneText
		{
			get
			{
				string text = (string)this.ViewState["EmptyZoneText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("WebPartZoneBase_DefaultEmptyZoneText");
			}
			set
			{
				this.ViewState["EmptyZoneText"] = value;
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06004A0D RID: 18957 RVA: 0x000F594E File Offset: 0x000F3B4E
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("WebPartZoneBase_ExportVerb")]
		public virtual WebPartVerb ExportVerb
		{
			get
			{
				if (this._exportVerb == null)
				{
					this._exportVerb = new WebPartExportVerb();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._exportVerb).TrackViewState();
					}
				}
				return this._exportVerb;
			}
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x06004A0E RID: 18958 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool HasFooter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x06004A0F RID: 18959 RVA: 0x000F597C File Offset: 0x000F3B7C
		protected override bool HasHeader
		{
			get
			{
				bool result = false;
				if (base.DesignMode)
				{
					result = true;
				}
				else if (base.WebPartManager != null)
				{
					result = base.WebPartManager.DisplayMode.AllowPageDesign;
				}
				return result;
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x06004A10 RID: 18960 RVA: 0x000F59B1 File Offset: 0x000F3BB1
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("WebPartZoneBase_HelpVerb")]
		public virtual WebPartVerb HelpVerb
		{
			get
			{
				if (this._helpVerb == null)
				{
					this._helpVerb = new WebPartHelpVerb();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._helpVerb).TrackViewState();
					}
				}
				return this._helpVerb;
			}
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x06004A11 RID: 18961 RVA: 0x000F59DF File Offset: 0x000F3BDF
		internal WebPartMenu Menu
		{
			get
			{
				if (this._menu == null)
				{
					this._menu = new WebPartMenu(this);
				}
				return this._menu;
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x06004A12 RID: 18962 RVA: 0x000F59FB File Offset: 0x000F3BFB
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("WebPartZoneBase_MenuCheckImageStyle")]
		public Style MenuCheckImageStyle
		{
			get
			{
				if (this._menuCheckImageStyle == null)
				{
					this._menuCheckImageStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._menuCheckImageStyle).TrackViewState();
					}
				}
				return this._menuCheckImageStyle;
			}
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06004A13 RID: 18963 RVA: 0x000F5A2C File Offset: 0x000F3C2C
		// (set) Token: 0x06004A14 RID: 18964 RVA: 0x000F5A59 File Offset: 0x000F3C59
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("WebPartZoneBase_MenuCheckImageUrl")]
		public virtual string MenuCheckImageUrl
		{
			get
			{
				string text = (string)this.ViewState["MenuCheckImageUrl"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["MenuCheckImageUrl"] = value;
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06004A15 RID: 18965 RVA: 0x000F5A6C File Offset: 0x000F3C6C
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("WebPartZoneBase_MenuLabelHoverStyle")]
		public Style MenuLabelHoverStyle
		{
			get
			{
				if (this._menuLabelHoverStyle == null)
				{
					this._menuLabelHoverStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._menuLabelHoverStyle).TrackViewState();
					}
				}
				return this._menuLabelHoverStyle;
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x06004A16 RID: 18966 RVA: 0x000F5A9A File Offset: 0x000F3C9A
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("WebPartZoneBase_MenuLabelStyle")]
		public Style MenuLabelStyle
		{
			get
			{
				if (this._menuLabelStyle == null)
				{
					this._menuLabelStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._menuLabelStyle).TrackViewState();
					}
				}
				return this._menuLabelStyle;
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x06004A17 RID: 18967 RVA: 0x000F5AC8 File Offset: 0x000F3CC8
		// (set) Token: 0x06004A18 RID: 18968 RVA: 0x000F5AF5 File Offset: 0x000F3CF5
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("WebPartZoneBase_MenuLabelText")]
		public virtual string MenuLabelText
		{
			get
			{
				string text = (string)this.ViewState["MenuLabelText"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["MenuLabelText"] = value;
			}
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x06004A19 RID: 18969 RVA: 0x000F5B08 File Offset: 0x000F3D08
		// (set) Token: 0x06004A1A RID: 18970 RVA: 0x000F5B35 File Offset: 0x000F3D35
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("WebPartZoneBase_MenuPopupImageUrl")]
		public virtual string MenuPopupImageUrl
		{
			get
			{
				string text = (string)this.ViewState["MenuPopupImageUrl"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["MenuPopupImageUrl"] = value;
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06004A1B RID: 18971 RVA: 0x000F5B48 File Offset: 0x000F3D48
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("WebPartZoneBase_MenuPopupStyle")]
		public WebPartMenuStyle MenuPopupStyle
		{
			get
			{
				if (this._menuPopupStyle == null)
				{
					this._menuPopupStyle = new WebPartMenuStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._menuPopupStyle).TrackViewState();
					}
				}
				return this._menuPopupStyle;
			}
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06004A1C RID: 18972 RVA: 0x000F5B76 File Offset: 0x000F3D76
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("WebPartZoneBase_MenuVerbHoverStyle")]
		public Style MenuVerbHoverStyle
		{
			get
			{
				if (this._menuVerbHoverStyle == null)
				{
					this._menuVerbHoverStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._menuVerbHoverStyle).TrackViewState();
					}
				}
				return this._menuVerbHoverStyle;
			}
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06004A1D RID: 18973 RVA: 0x000F5BA4 File Offset: 0x000F3DA4
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("WebPartZoneBase_MenuVerbStyle")]
		public Style MenuVerbStyle
		{
			get
			{
				if (this._menuVerbStyle == null)
				{
					this._menuVerbStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._menuVerbStyle).TrackViewState();
					}
				}
				return this._menuVerbStyle;
			}
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06004A1E RID: 18974 RVA: 0x000F5BD2 File Offset: 0x000F3DD2
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("WebPartZoneBase_MinimizeVerb")]
		public virtual WebPartVerb MinimizeVerb
		{
			get
			{
				if (this._minimizeVerb == null)
				{
					this._minimizeVerb = new WebPartMinimizeVerb();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._minimizeVerb).TrackViewState();
					}
				}
				return this._minimizeVerb;
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06004A1F RID: 18975 RVA: 0x000F5C00 File Offset: 0x000F3E00
		// (set) Token: 0x06004A20 RID: 18976 RVA: 0x000F5C29 File Offset: 0x000F3E29
		[DefaultValue(Orientation.Vertical)]
		[WebCategory("Layout")]
		[WebSysDescription("WebPartZoneBase_LayoutOrientation")]
		public virtual Orientation LayoutOrientation
		{
			get
			{
				object obj = this.ViewState["LayoutOrientation"];
				if (obj == null)
				{
					return Orientation.Vertical;
				}
				return (Orientation)((int)obj);
			}
			set
			{
				if (value < Orientation.Horizontal || value > Orientation.Vertical)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["LayoutOrientation"] = (int)value;
			}
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06004A21 RID: 18977 RVA: 0x000F5C54 File Offset: 0x000F3E54
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("WebPartZoneBase_RestoreVerb")]
		public virtual WebPartVerb RestoreVerb
		{
			get
			{
				if (this._restoreVerb == null)
				{
					this._restoreVerb = new WebPartRestoreVerb();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._restoreVerb).TrackViewState();
					}
				}
				return this._restoreVerb;
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x06004A22 RID: 18978 RVA: 0x000F5C82 File Offset: 0x000F3E82
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("WebPart")]
		[WebSysDescription("WebPartZoneBase_SelectedPartChromeStyle")]
		public Style SelectedPartChromeStyle
		{
			get
			{
				if (this._selectedPartChromeStyle == null)
				{
					this._selectedPartChromeStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._selectedPartChromeStyle).TrackViewState();
					}
				}
				return this._selectedPartChromeStyle;
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x06004A23 RID: 18979 RVA: 0x000F5CB0 File Offset: 0x000F3EB0
		// (set) Token: 0x06004A24 RID: 18980 RVA: 0x000F5CD9 File Offset: 0x000F3ED9
		[DefaultValue(true)]
		[WebCategory("WebPart")]
		[WebSysDescription("WebPartZoneBase_ShowTitleIcons")]
		public virtual bool ShowTitleIcons
		{
			get
			{
				object obj = this.ViewState["ShowTitleIcons"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowTitleIcons"] = value;
			}
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06004A25 RID: 18981 RVA: 0x000F5CF4 File Offset: 0x000F3EF4
		// (set) Token: 0x06004A26 RID: 18982 RVA: 0x000F5D1D File Offset: 0x000F3F1D
		[DefaultValue(ButtonType.Image)]
		[WebCategory("Appearance")]
		[WebSysDescription("WebPartZoneBase_TitleBarVerbButtonType")]
		public virtual ButtonType TitleBarVerbButtonType
		{
			get
			{
				object obj = this.ViewState["TitleBarVerbButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Image;
			}
			set
			{
				if (value < ButtonType.Button || value > ButtonType.Link)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["TitleBarVerbButtonType"] = value;
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06004A27 RID: 18983 RVA: 0x000F5D48 File Offset: 0x000F3F48
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("WebPartZoneBase_TitleBarVerbStyle")]
		public Style TitleBarVerbStyle
		{
			get
			{
				if (this._titleBarVerbStyle == null)
				{
					this._titleBarVerbStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._titleBarVerbStyle).TrackViewState();
					}
				}
				return this._titleBarVerbStyle;
			}
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06004A28 RID: 18984 RVA: 0x000F5D76 File Offset: 0x000F3F76
		// (set) Token: 0x06004A29 RID: 18985 RVA: 0x000F5D7E File Offset: 0x000F3F7E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override ButtonType VerbButtonType
		{
			get
			{
				return base.VerbButtonType;
			}
			set
			{
				base.VerbButtonType = value;
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06004A2A RID: 18986 RVA: 0x000F5D87 File Offset: 0x000F3F87
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPartChrome WebPartChrome
		{
			get
			{
				if (this._webPartChrome == null)
				{
					this._webPartChrome = this.CreateWebPartChrome();
				}
				return this._webPartChrome;
			}
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x06004A2B RID: 18987 RVA: 0x000F5DA4 File Offset: 0x000F3FA4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPartCollection WebParts
		{
			get
			{
				if (base.DesignMode)
				{
					WebPart[] array = new WebPart[this.Controls.Count];
					this.Controls.CopyTo(array, 0);
					return new WebPartCollection(array);
				}
				WebPartCollection result;
				if (base.WebPartManager != null)
				{
					result = base.WebPartManager.GetWebPartsForZone(this);
				}
				else
				{
					result = new WebPartCollection();
				}
				return result;
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06004A2C RID: 18988 RVA: 0x000F5DFC File Offset: 0x000F3FFC
		// (set) Token: 0x06004A2D RID: 18989 RVA: 0x000F5E25 File Offset: 0x000F4025
		[DefaultValue(WebPartVerbRenderMode.Menu)]
		[WebCategory("WebPart")]
		[WebSysDescription("WebPartZoneBase_WebPartVerbRenderMode")]
		public virtual WebPartVerbRenderMode WebPartVerbRenderMode
		{
			get
			{
				object obj = this.ViewState["WebPartVerbRenderMode"];
				if (obj == null)
				{
					return WebPartVerbRenderMode.Menu;
				}
				return (WebPartVerbRenderMode)((int)obj);
			}
			set
			{
				if (value < WebPartVerbRenderMode.Menu || value > WebPartVerbRenderMode.TitleBar)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["WebPartVerbRenderMode"] = (int)value;
			}
		}

		// Token: 0x14000125 RID: 293
		// (add) Token: 0x06004A2E RID: 18990 RVA: 0x000F5E50 File Offset: 0x000F4050
		// (remove) Token: 0x06004A2F RID: 18991 RVA: 0x000F5E63 File Offset: 0x000F4063
		[WebCategory("Action")]
		[WebSysDescription("WebPartZoneBase_CreateVerbs")]
		public event WebPartVerbsEventHandler CreateVerbs
		{
			add
			{
				base.Events.AddHandler(WebPartZoneBase.CreateVerbsEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartZoneBase.CreateVerbsEvent, value);
			}
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x000F5E76 File Offset: 0x000F4076
		protected virtual void CloseWebPart(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (base.WebPartManager != null && webPart.AllowClose && this.AllowLayoutChange)
			{
				base.WebPartManager.CloseWebPart(webPart);
			}
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x000F5EAC File Offset: 0x000F40AC
		protected virtual void ConnectWebPart(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (base.WebPartManager != null && base.WebPartManager.DisplayMode == WebPartManager.ConnectDisplayMode && webPart != base.WebPartManager.SelectedWebPart && webPart.AllowConnect)
			{
				base.WebPartManager.BeginWebPartConnecting(webPart);
			}
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x000F5F04 File Offset: 0x000F4104
		protected internal override void CreateChildControls()
		{
			if (base.DesignMode)
			{
				this.Controls.Clear();
				WebPartCollection initialWebParts = this.GetInitialWebParts();
				foreach (object obj in initialWebParts)
				{
					WebPart child = (WebPart)obj;
					this.Controls.Add(child);
				}
			}
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x000F5F78 File Offset: 0x000F4178
		protected override ControlCollection CreateControlCollection()
		{
			if (base.DesignMode)
			{
				return new ControlCollection(this);
			}
			return new EmptyControlCollection(this);
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x000F5F90 File Offset: 0x000F4190
		protected override Style CreateControlStyle()
		{
			return new Style
			{
				BorderColor = Color.Gray,
				BorderStyle = BorderStyle.Solid,
				BorderWidth = 1
			};
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x000F5FC2 File Offset: 0x000F41C2
		protected virtual WebPartChrome CreateWebPartChrome()
		{
			return new WebPartChrome(this, base.WebPartManager);
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x000F5FD0 File Offset: 0x000F41D0
		protected virtual void DeleteWebPart(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (base.WebPartManager != null && this.AllowLayoutChange)
			{
				base.WebPartManager.DeleteWebPart(webPart);
			}
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x000F5FFC File Offset: 0x000F41FC
		protected virtual void EditWebPart(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (base.WebPartManager != null && base.WebPartManager.DisplayMode == WebPartManager.EditDisplayMode && webPart != base.WebPartManager.SelectedWebPart)
			{
				base.WebPartManager.BeginWebPartEditing(webPart);
			}
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x000F604C File Offset: 0x000F424C
		public override PartChromeType GetEffectiveChromeType(Part part)
		{
			PartChromeType partChromeType = base.GetEffectiveChromeType(part);
			if (base.WebPartManager != null && base.WebPartManager.DisplayMode.AllowPageDesign)
			{
				if (partChromeType == PartChromeType.None)
				{
					partChromeType = PartChromeType.TitleOnly;
				}
				else if (partChromeType == PartChromeType.BorderOnly)
				{
					partChromeType = PartChromeType.TitleAndBorder;
				}
			}
			return partChromeType;
		}

		// Token: 0x06004A39 RID: 19001
		protected internal abstract WebPartCollection GetInitialWebParts();

		// Token: 0x06004A3A RID: 19002 RVA: 0x000F608C File Offset: 0x000F428C
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 18)
			{
				throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
			}
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.SelectedPartChromeStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.CloseVerb).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.ConnectVerb).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.DeleteVerb).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.EditVerb).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.HelpVerb).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.MinimizeVerb).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.RestoreVerb).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.ExportVerb).LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)this.MenuPopupStyle).LoadViewState(array[10]);
			}
			if (array[11] != null)
			{
				((IStateManager)this.MenuLabelStyle).LoadViewState(array[11]);
			}
			if (array[12] != null)
			{
				((IStateManager)this.MenuLabelHoverStyle).LoadViewState(array[12]);
			}
			if (array[13] != null)
			{
				((IStateManager)this.MenuCheckImageStyle).LoadViewState(array[13]);
			}
			if (array[14] != null)
			{
				((IStateManager)this.MenuVerbStyle).LoadViewState(array[14]);
			}
			if (array[15] != null)
			{
				((IStateManager)this.MenuVerbHoverStyle).LoadViewState(array[15]);
			}
			if (array[16] != null)
			{
				((IStateManager)base.ControlStyle).LoadViewState(array[16]);
			}
			if (array[17] != null)
			{
				((IStateManager)this.TitleBarVerbStyle).LoadViewState(array[17]);
			}
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x000F6220 File Offset: 0x000F4420
		private void CreateZoneVerbs()
		{
			WebPartVerbsEventArgs webPartVerbsEventArgs = new WebPartVerbsEventArgs();
			this.OnCreateVerbs(webPartVerbsEventArgs);
			this._verbs = webPartVerbsEventArgs.Verbs;
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x000F6246 File Offset: 0x000F4446
		private bool IsDefaultVerbEvent(string[] eventArguments)
		{
			return eventArguments.Length == 2;
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x000F624E File Offset: 0x000F444E
		private bool IsDragEvent(string[] eventArguments)
		{
			return eventArguments.Length == 3 && string.Equals(eventArguments[0], "Drag", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x000F6266 File Offset: 0x000F4466
		private bool IsPartVerbEvent(string[] eventArguments)
		{
			return eventArguments.Length == 3 && string.Equals(eventArguments[0], "partverb", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x000F627E File Offset: 0x000F447E
		private bool IsZoneVerbEvent(string[] eventArguments)
		{
			return eventArguments.Length == 3 && string.Equals(eventArguments[0], "zoneverb", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x000F6296 File Offset: 0x000F4496
		protected virtual void MinimizeWebPart(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (webPart.ChromeState == PartChromeState.Normal && webPart.AllowMinimize && this.AllowLayoutChange)
			{
				webPart.ChromeState = PartChromeState.Minimized;
			}
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x000F62C8 File Offset: 0x000F44C8
		protected virtual void OnCreateVerbs(WebPartVerbsEventArgs e)
		{
			WebPartVerbsEventHandler webPartVerbsEventHandler = (WebPartVerbsEventHandler)base.Events[WebPartZoneBase.CreateVerbsEvent];
			if (webPartVerbsEventHandler != null)
			{
				webPartVerbsEventHandler(this, e);
			}
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x000F62F6 File Offset: 0x000F44F6
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.CreateZoneVerbs();
			this.WebPartChrome.PerformPreRender();
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x000F6310 File Offset: 0x000F4510
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			if (string.IsNullOrEmpty(eventArgument))
			{
				return;
			}
			string[] array = eventArgument.Split(new char[]
			{
				':'
			});
			if (!this.IsDragEvent(array))
			{
				base.ValidateEvent(this.UniqueID, eventArgument);
			}
			if (base.WebPartManager == null)
			{
				return;
			}
			WebPartCollection webParts = base.WebPartManager.WebParts;
			if (this.IsDefaultVerbEvent(array))
			{
				string a = array[0];
				string id = array[1];
				WebPart webPart = webParts[id];
				if (webPart != null && !webPart.IsClosed)
				{
					if (string.Equals(a, "close", StringComparison.OrdinalIgnoreCase))
					{
						if (this.CloseVerb.Visible && this.CloseVerb.Enabled)
						{
							this.CloseWebPart(webPart);
							return;
						}
					}
					else if (string.Equals(a, "connect", StringComparison.OrdinalIgnoreCase))
					{
						if (this.ConnectVerb.Visible && this.ConnectVerb.Enabled)
						{
							this.ConnectWebPart(webPart);
							return;
						}
					}
					else if (string.Equals(a, "delete", StringComparison.OrdinalIgnoreCase))
					{
						if (this.DeleteVerb.Visible && this.DeleteVerb.Enabled)
						{
							this.DeleteWebPart(webPart);
							return;
						}
					}
					else if (string.Equals(a, "edit", StringComparison.OrdinalIgnoreCase))
					{
						if (this.EditVerb.Visible && this.EditVerb.Enabled)
						{
							this.EditWebPart(webPart);
							return;
						}
					}
					else if (string.Equals(a, "minimize", StringComparison.OrdinalIgnoreCase))
					{
						if (this.MinimizeVerb.Visible && this.MinimizeVerb.Enabled)
						{
							this.MinimizeWebPart(webPart);
							return;
						}
					}
					else if (string.Equals(a, "restore", StringComparison.OrdinalIgnoreCase) && this.RestoreVerb.Visible && this.RestoreVerb.Enabled)
					{
						this.RestoreWebPart(webPart);
						return;
					}
				}
			}
			else if (this.IsDragEvent(array))
			{
				string text = array[1];
				string id2 = null;
				if (text.StartsWith("WebPart_", StringComparison.Ordinal))
				{
					id2 = text.Substring("WebPart_".Length);
				}
				int num = int.Parse(array[2], CultureInfo.InvariantCulture);
				WebPart webPart2 = webParts[id2];
				if (webPart2 != null && !webPart2.IsClosed)
				{
					if (this.WebParts.Contains(webPart2) && webPart2.ZoneIndex < num)
					{
						num--;
					}
					WebPartZoneBase zone = webPart2.Zone;
					if (this.AllowLayoutChange && base.WebPartManager.DisplayMode.AllowPageDesign && zone != null && zone.AllowLayoutChange && (webPart2.AllowZoneChange || zone == this))
					{
						base.WebPartManager.MoveWebPart(webPart2, this, num);
						return;
					}
				}
			}
			else if (this.IsPartVerbEvent(array))
			{
				string id3 = array[1];
				string id4 = array[2];
				WebPart webPart3 = webParts[id4];
				if (webPart3 != null && !webPart3.IsClosed)
				{
					WebPartVerb webPartVerb = webPart3.Verbs[id3];
					if (webPartVerb != null && webPartVerb.Visible && webPartVerb.Enabled)
					{
						webPartVerb.ServerClickHandler(webPartVerb, new WebPartEventArgs(webPart3));
						return;
					}
				}
			}
			else if (this.IsZoneVerbEvent(array))
			{
				this.CreateZoneVerbs();
				string id5 = array[1];
				string id6 = array[2];
				WebPart webPart4 = webParts[id6];
				if (webPart4 != null && !webPart4.IsClosed)
				{
					WebPartVerb webPartVerb2 = this._verbs[id5];
					if (webPartVerb2 != null && webPartVerb2.Visible && webPartVerb2.Enabled)
					{
						webPartVerb2.ServerClickHandler(webPartVerb2, new WebPartEventArgs(webPart4));
					}
				}
			}
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x000F66A0 File Offset: 0x000F48A0
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			this._borderColor = this.BorderColor;
			this._borderStyle = this.BorderStyle;
			this._borderWidth = this.BorderWidth;
			if (base.ControlStyleCreated)
			{
				this.BorderColor = Color.Empty;
				this.BorderStyle = BorderStyle.NotSet;
				this.BorderWidth = Unit.Empty;
			}
			base.Render(writer);
			if (base.ControlStyleCreated)
			{
				this.BorderColor = this._borderColor;
				this.BorderStyle = this._borderStyle;
				this.BorderWidth = this._borderWidth;
			}
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x000F6740 File Offset: 0x000F4940
		protected override void RenderBody(HtmlTextWriter writer)
		{
			Orientation layoutOrientation = this.LayoutOrientation;
			if ((base.DesignMode || (base.WebPartManager != null && base.WebPartManager.DisplayMode.AllowPageDesign)) && (this._borderColor != Color.Empty || this._borderStyle != BorderStyle.NotSet || this._borderWidth != Unit.Empty))
			{
				new Style
				{
					BorderColor = this._borderColor,
					BorderStyle = this._borderStyle,
					BorderWidth = this._borderWidth
				}.AddAttributesToRender(writer, this);
			}
			base.RenderBodyTableBeginTag(writer);
			if (base.DesignMode)
			{
				base.RenderDesignerRegionBeginTag(writer, layoutOrientation);
			}
			if (layoutOrientation == Orientation.Horizontal)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			}
			bool dragDropEnabled = this.DragDropEnabled;
			if (dragDropEnabled)
			{
				this.RenderDropCue(writer);
			}
			WebPartCollection webParts = this.WebParts;
			if (webParts == null || webParts.Count == 0)
			{
				this.RenderEmptyZoneBody(writer);
			}
			else
			{
				WebPartChrome webPartChrome = this.WebPartChrome;
				foreach (object obj in webParts)
				{
					WebPart webPart = (WebPart)obj;
					if (webPart.ChromeState == PartChromeState.Minimized)
					{
						PartChromeType effectiveChromeType = this.GetEffectiveChromeType(webPart);
						if (effectiveChromeType == PartChromeType.None || effectiveChromeType == PartChromeType.BorderOnly)
						{
							writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
						}
					}
					if (layoutOrientation == Orientation.Vertical)
					{
						writer.RenderBeginTag(HtmlTextWriterTag.Tr);
					}
					else
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
						writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					webPartChrome.RenderWebPart(writer, webPart);
					writer.RenderEndTag();
					if (layoutOrientation == Orientation.Vertical)
					{
						writer.RenderEndTag();
					}
					if (dragDropEnabled)
					{
						this.RenderDropCue(writer);
					}
				}
				if (layoutOrientation == Orientation.Vertical)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
					writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
				else
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0");
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					writer.RenderEndTag();
				}
			}
			if (layoutOrientation == Orientation.Horizontal)
			{
				writer.RenderEndTag();
			}
			if (base.DesignMode)
			{
				WebZone.RenderDesignerRegionEndTag(writer);
			}
			WebZone.RenderBodyTableEndTag(writer);
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x000F697C File Offset: 0x000F4B7C
		protected virtual void RenderDropCue(HtmlTextWriter writer)
		{
			if (this.LayoutOrientation == Orientation.Vertical)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingTop, "1");
				writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingBottom, "1");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				this.RenderDropCueIBar(writer, Orientation.Horizontal);
				writer.RenderEndTag();
				writer.RenderEndTag();
				return;
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "1");
			writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "1");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderDropCueIBar(writer, Orientation.Vertical);
			writer.RenderEndTag();
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x000F6A04 File Offset: 0x000F4C04
		private void RenderDropCueIBar(HtmlTextWriter writer, Orientation orientation)
		{
			string text = ColorTranslator.ToHtml(this.DragHighlightColor);
			string value = "solid 3px " + text;
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			if (orientation == Orientation.Horizontal)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
				writer.AddStyleAttribute("border-left", value);
				writer.AddStyleAttribute("border-right", value);
			}
			else
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
				writer.AddStyleAttribute("border-top", value);
				writer.AddStyleAttribute("border-bottom", value);
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (orientation == Orientation.Vertical)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "0px");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (orientation == Orientation.Horizontal)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "2px 0px 2px 0px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "2px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			}
			else
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "0px 2px 0px 2px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "2px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x000F6B58 File Offset: 0x000F4D58
		private void RenderEmptyZoneBody(HtmlTextWriter writer)
		{
			bool flag = this.LayoutOrientation == Orientation.Vertical;
			bool flag2 = !flag;
			string emptyZoneText = this.EmptyZoneText;
			bool flag3 = !base.DesignMode && this.AllowLayoutChange && base.WebPartManager != null && base.WebPartManager.DisplayMode.AllowPageDesign && !string.IsNullOrEmpty(emptyZoneText);
			if (flag)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			}
			if (flag3)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
			}
			if (flag2)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			}
			else
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (flag3)
			{
				Style emptyZoneTextStyle = base.EmptyZoneTextStyle;
				if (!emptyZoneTextStyle.IsEmpty)
				{
					emptyZoneTextStyle.AddAttributesToRender(writer, this);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.Write(emptyZoneText);
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			if (flag)
			{
				writer.RenderEndTag();
			}
			if (flag3 && this.DragDropEnabled)
			{
				this.RenderDropCue(writer);
			}
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x000F6C48 File Offset: 0x000F4E48
		protected override void RenderHeader(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "2");
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			TitleStyle headerStyle = base.HeaderStyle;
			if (!headerStyle.IsEmpty)
			{
				Style style = new Style();
				if (!headerStyle.ForeColor.IsEmpty)
				{
					style.ForeColor = headerStyle.ForeColor;
				}
				style.Font.CopyFrom(headerStyle.Font);
				if (!headerStyle.Font.Size.IsEmpty)
				{
					style.Font.Size = new FontUnit(new Unit(100.0, UnitType.Percentage));
				}
				if (!style.IsEmpty)
				{
					style.AddAttributesToRender(writer, this);
				}
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			HorizontalAlign horizontalAlign = headerStyle.HorizontalAlign;
			if (horizontalAlign != HorizontalAlign.NotSet)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(HorizontalAlign));
				writer.AddAttribute(HtmlTextWriterAttribute.Align, converter.ConvertToString(horizontalAlign));
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write(this.DisplayTitle);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x000F6D7F File Offset: 0x000F4F7F
		protected virtual void RestoreWebPart(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (webPart.ChromeState == PartChromeState.Minimized && this.AllowLayoutChange)
			{
				webPart.ChromeState = PartChromeState.Normal;
			}
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x000F6DA8 File Offset: 0x000F4FA8
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this._selectedPartChromeStyle != null) ? ((IStateManager)this._selectedPartChromeStyle).SaveViewState() : null,
				(this._closeVerb != null) ? ((IStateManager)this._closeVerb).SaveViewState() : null,
				(this._connectVerb != null) ? ((IStateManager)this._connectVerb).SaveViewState() : null,
				(this._deleteVerb != null) ? ((IStateManager)this._deleteVerb).SaveViewState() : null,
				(this._editVerb != null) ? ((IStateManager)this._editVerb).SaveViewState() : null,
				(this._helpVerb != null) ? ((IStateManager)this._helpVerb).SaveViewState() : null,
				(this._minimizeVerb != null) ? ((IStateManager)this._minimizeVerb).SaveViewState() : null,
				(this._restoreVerb != null) ? ((IStateManager)this._restoreVerb).SaveViewState() : null,
				(this._exportVerb != null) ? ((IStateManager)this._exportVerb).SaveViewState() : null,
				(this._menuPopupStyle != null) ? ((IStateManager)this._menuPopupStyle).SaveViewState() : null,
				(this._menuLabelStyle != null) ? ((IStateManager)this._menuLabelStyle).SaveViewState() : null,
				(this._menuLabelHoverStyle != null) ? ((IStateManager)this._menuLabelHoverStyle).SaveViewState() : null,
				(this._menuCheckImageStyle != null) ? ((IStateManager)this._menuCheckImageStyle).SaveViewState() : null,
				(this._menuVerbStyle != null) ? ((IStateManager)this._menuVerbStyle).SaveViewState() : null,
				(this._menuVerbHoverStyle != null) ? ((IStateManager)this._menuVerbHoverStyle).SaveViewState() : null,
				base.ControlStyleCreated ? ((IStateManager)base.ControlStyle).SaveViewState() : null,
				(this._titleBarVerbStyle != null) ? ((IStateManager)this._titleBarVerbStyle).SaveViewState() : null
			};
			for (int i = 0; i < 18; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x000F6F90 File Offset: 0x000F5190
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._selectedPartChromeStyle != null)
			{
				((IStateManager)this._selectedPartChromeStyle).TrackViewState();
			}
			if (this._closeVerb != null)
			{
				((IStateManager)this._closeVerb).TrackViewState();
			}
			if (this._connectVerb != null)
			{
				((IStateManager)this._connectVerb).TrackViewState();
			}
			if (this._deleteVerb != null)
			{
				((IStateManager)this._deleteVerb).TrackViewState();
			}
			if (this._editVerb != null)
			{
				((IStateManager)this._editVerb).TrackViewState();
			}
			if (this._helpVerb != null)
			{
				((IStateManager)this._helpVerb).TrackViewState();
			}
			if (this._minimizeVerb != null)
			{
				((IStateManager)this._minimizeVerb).TrackViewState();
			}
			if (this._restoreVerb != null)
			{
				((IStateManager)this._restoreVerb).TrackViewState();
			}
			if (this._exportVerb != null)
			{
				((IStateManager)this._exportVerb).TrackViewState();
			}
			if (this._menuPopupStyle != null)
			{
				((IStateManager)this._menuPopupStyle).TrackViewState();
			}
			if (this._menuLabelStyle != null)
			{
				((IStateManager)this._menuLabelStyle).TrackViewState();
			}
			if (this._menuLabelHoverStyle != null)
			{
				((IStateManager)this._menuLabelHoverStyle).TrackViewState();
			}
			if (this._menuCheckImageStyle != null)
			{
				((IStateManager)this._menuCheckImageStyle).TrackViewState();
			}
			if (this._menuVerbStyle != null)
			{
				((IStateManager)this._menuVerbStyle).TrackViewState();
			}
			if (this._menuVerbHoverStyle != null)
			{
				((IStateManager)this._menuVerbHoverStyle).TrackViewState();
			}
			if (base.ControlStyleCreated)
			{
				((IStateManager)base.ControlStyle).TrackViewState();
			}
			if (this._titleBarVerbStyle != null)
			{
				((IStateManager)this._titleBarVerbStyle).TrackViewState();
			}
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x000F70E8 File Offset: 0x000F52E8
		internal WebPartVerbCollection VerbsForWebPart(WebPart webPart)
		{
			WebPartVerbCollection webPartVerbCollection = new WebPartVerbCollection();
			WebPartVerbCollection verbs = webPart.Verbs;
			if (verbs != null)
			{
				foreach (object obj in verbs)
				{
					WebPartVerb webPartVerb = (WebPartVerb)obj;
					if (webPartVerb.ServerClickHandler != null)
					{
						webPartVerb.SetEventArgumentPrefix("partverb:");
					}
					webPartVerbCollection.Add(webPartVerb);
				}
			}
			if (this._verbs != null)
			{
				foreach (object obj2 in this._verbs)
				{
					WebPartVerb webPartVerb2 = (WebPartVerb)obj2;
					if (webPartVerb2.ServerClickHandler != null)
					{
						webPartVerb2.SetEventArgumentPrefix("zoneverb:");
					}
					webPartVerbCollection.Add(webPartVerb2);
				}
			}
			WebPartVerb minimizeVerb = this.MinimizeVerb;
			minimizeVerb.SetEventArgumentPrefix("minimize:");
			webPartVerbCollection.Add(minimizeVerb);
			WebPartVerb restoreVerb = this.RestoreVerb;
			restoreVerb.SetEventArgumentPrefix("restore:");
			webPartVerbCollection.Add(restoreVerb);
			WebPartVerb closeVerb = this.CloseVerb;
			closeVerb.SetEventArgumentPrefix("close:");
			webPartVerbCollection.Add(closeVerb);
			WebPartVerb deleteVerb = this.DeleteVerb;
			deleteVerb.SetEventArgumentPrefix("delete:");
			webPartVerbCollection.Add(deleteVerb);
			WebPartVerb editVerb = this.EditVerb;
			editVerb.SetEventArgumentPrefix("edit:");
			webPartVerbCollection.Add(editVerb);
			WebPartVerb connectVerb = this.ConnectVerb;
			connectVerb.SetEventArgumentPrefix("connect:");
			webPartVerbCollection.Add(connectVerb);
			webPartVerbCollection.Add(this.ExportVerb);
			webPartVerbCollection.Add(this.HelpVerb);
			return webPartVerbCollection;
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x000F72A0 File Offset: 0x000F54A0
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x000F72A9 File Offset: 0x000F54A9
		Style IWebPartMenuUser.CheckImageStyle
		{
			get
			{
				return this._menuCheckImageStyle;
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x000F72B4 File Offset: 0x000F54B4
		string IWebPartMenuUser.CheckImageUrl
		{
			get
			{
				string text = this.MenuCheckImageUrl;
				if (!string.IsNullOrEmpty(text))
				{
					text = base.ResolveClientUrl(text);
				}
				return text;
			}
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x06004A51 RID: 19025 RVA: 0x000F72D9 File Offset: 0x000F54D9
		string IWebPartMenuUser.ClientID
		{
			get
			{
				return this.ClientID;
			}
		}

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x000F72E4 File Offset: 0x000F54E4
		string IWebPartMenuUser.PopupImageUrl
		{
			get
			{
				string text = this.MenuPopupImageUrl;
				if (!string.IsNullOrEmpty(text))
				{
					text = base.ResolveClientUrl(text);
				}
				return text;
			}
		}

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x06004A53 RID: 19027 RVA: 0x000F7309 File Offset: 0x000F5509
		Style IWebPartMenuUser.ItemHoverStyle
		{
			get
			{
				return this._menuVerbHoverStyle;
			}
		}

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06004A54 RID: 19028 RVA: 0x000F7311 File Offset: 0x000F5511
		Style IWebPartMenuUser.ItemStyle
		{
			get
			{
				return this._menuVerbStyle;
			}
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06004A55 RID: 19029 RVA: 0x000F7319 File Offset: 0x000F5519
		Style IWebPartMenuUser.LabelHoverStyle
		{
			get
			{
				return this._menuLabelHoverStyle;
			}
		}

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x06004A56 RID: 19030 RVA: 0x0000298D File Offset: 0x00000B8D
		string IWebPartMenuUser.LabelImageUrl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x06004A57 RID: 19031 RVA: 0x000F7321 File Offset: 0x000F5521
		Style IWebPartMenuUser.LabelStyle
		{
			get
			{
				return this.MenuLabelStyle;
			}
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06004A58 RID: 19032 RVA: 0x000F7329 File Offset: 0x000F5529
		string IWebPartMenuUser.LabelText
		{
			get
			{
				return this.MenuLabelText;
			}
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06004A59 RID: 19033 RVA: 0x000F7331 File Offset: 0x000F5531
		WebPartMenuStyle IWebPartMenuUser.MenuPopupStyle
		{
			get
			{
				return this._menuPopupStyle;
			}
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06004A5A RID: 19034 RVA: 0x000F7339 File Offset: 0x000F5539
		Page IWebPartMenuUser.Page
		{
			get
			{
				return this.Page;
			}
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06004A5B RID: 19035 RVA: 0x0007F357 File Offset: 0x0007D557
		string IWebPartMenuUser.PostBackTarget
		{
			get
			{
				return this.UniqueID;
			}
		}

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06004A5C RID: 19036 RVA: 0x00004335 File Offset: 0x00002535
		IUrlResolutionService IWebPartMenuUser.UrlResolver
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x00006164 File Offset: 0x00004364
		void IWebPartMenuUser.OnBeginRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x00006164 File Offset: 0x00004364
		void IWebPartMenuUser.OnEndRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x000F7349 File Offset: 0x000F5549
		// Note: this type is marked as 'beforefieldinit'.
		static WebPartZoneBase()
		{
			WebPartZoneBase.CreateVerbsEvent = new object();
		}

		// Token: 0x040027C8 RID: 10184
		internal const string EventArgumentSeparator = ":";

		// Token: 0x040027C9 RID: 10185
		private const char eventArgumentSeparatorChar = ':';

		// Token: 0x040027CA RID: 10186
		private const string dragEventArgument = "Drag";

		// Token: 0x040027CB RID: 10187
		private const string partVerbEventArgument = "partverb";

		// Token: 0x040027CC RID: 10188
		private const string zoneVerbEventArgument = "zoneverb";

		// Token: 0x040027CD RID: 10189
		private const string closeEventArgument = "close";

		// Token: 0x040027CE RID: 10190
		private const string connectEventArgument = "connect";

		// Token: 0x040027CF RID: 10191
		private const string deleteEventArgument = "delete";

		// Token: 0x040027D0 RID: 10192
		private const string editEventArgument = "edit";

		// Token: 0x040027D1 RID: 10193
		private const string minimizeEventArgument = "minimize";

		// Token: 0x040027D2 RID: 10194
		private const string restoreEventArgument = "restore";

		// Token: 0x040027D3 RID: 10195
		private const string partVerbEventArgumentWithSeparator = "partverb:";

		// Token: 0x040027D4 RID: 10196
		private const string zoneVerbEventArgumentWithSeparator = "zoneverb:";

		// Token: 0x040027D5 RID: 10197
		private const string connectEventArgumentWithSeparator = "connect:";

		// Token: 0x040027D6 RID: 10198
		private const string editEventArgumentWithSeparator = "edit:";

		// Token: 0x040027D7 RID: 10199
		private const string minimizeEventArgumentWithSeparator = "minimize:";

		// Token: 0x040027D8 RID: 10200
		private const string restoreEventArgumentWithSeparator = "restore:";

		// Token: 0x040027D9 RID: 10201
		private const string closeEventArgumentWithSeparator = "close:";

		// Token: 0x040027DA RID: 10202
		private const string deleteEventArgumentWithSeparator = "delete:";

		// Token: 0x040027DB RID: 10203
		private const int baseIndex = 0;

		// Token: 0x040027DC RID: 10204
		private const int selectedPartChromeStyleIndex = 1;

		// Token: 0x040027DD RID: 10205
		private const int closeVerbIndex = 2;

		// Token: 0x040027DE RID: 10206
		private const int connectVerbIndex = 3;

		// Token: 0x040027DF RID: 10207
		private const int deleteVerbIndex = 4;

		// Token: 0x040027E0 RID: 10208
		private const int editVerbIndex = 5;

		// Token: 0x040027E1 RID: 10209
		private const int helpVerbIndex = 6;

		// Token: 0x040027E2 RID: 10210
		private const int minimizeVerbIndex = 7;

		// Token: 0x040027E3 RID: 10211
		private const int restoreVerbIndex = 8;

		// Token: 0x040027E4 RID: 10212
		private const int exportVerbIndex = 9;

		// Token: 0x040027E5 RID: 10213
		private const int menuPopupStyleIndex = 10;

		// Token: 0x040027E6 RID: 10214
		private const int menuLabelStyleIndex = 11;

		// Token: 0x040027E7 RID: 10215
		private const int menuLabelHoverStyleIndex = 12;

		// Token: 0x040027E8 RID: 10216
		private const int menuCheckImageStyleIndex = 13;

		// Token: 0x040027E9 RID: 10217
		private const int menuVerbStyleIndex = 14;

		// Token: 0x040027EA RID: 10218
		private const int menuVerbHoverStyleIndex = 15;

		// Token: 0x040027EB RID: 10219
		private const int controlStyleIndex = 16;

		// Token: 0x040027EC RID: 10220
		private const int titleBarVerbStyleIndex = 17;

		// Token: 0x040027ED RID: 10221
		private const int viewStateArrayLength = 18;

		// Token: 0x040027EE RID: 10222
		private Style _selectedPartChromeStyle;

		// Token: 0x040027EF RID: 10223
		private WebPartVerb _closeVerb;

		// Token: 0x040027F0 RID: 10224
		private WebPartVerb _connectVerb;

		// Token: 0x040027F1 RID: 10225
		private WebPartVerb _deleteVerb;

		// Token: 0x040027F2 RID: 10226
		private WebPartVerb _editVerb;

		// Token: 0x040027F3 RID: 10227
		private WebPartVerb _exportVerb;

		// Token: 0x040027F4 RID: 10228
		private WebPartVerb _helpVerb;

		// Token: 0x040027F5 RID: 10229
		private WebPartVerb _minimizeVerb;

		// Token: 0x040027F6 RID: 10230
		private WebPartVerb _restoreVerb;

		// Token: 0x040027F7 RID: 10231
		private WebPartVerbCollection _verbs;

		// Token: 0x040027F8 RID: 10232
		private WebPartMenuStyle _menuPopupStyle;

		// Token: 0x040027F9 RID: 10233
		private Style _menuLabelStyle;

		// Token: 0x040027FA RID: 10234
		private Style _menuLabelHoverStyle;

		// Token: 0x040027FB RID: 10235
		private Style _menuCheckImageStyle;

		// Token: 0x040027FC RID: 10236
		private Style _menuVerbHoverStyle;

		// Token: 0x040027FD RID: 10237
		private Style _menuVerbStyle;

		// Token: 0x040027FE RID: 10238
		private Style _titleBarVerbStyle;

		// Token: 0x040027FF RID: 10239
		private Color _borderColor;

		// Token: 0x04002800 RID: 10240
		private BorderStyle _borderStyle;

		// Token: 0x04002801 RID: 10241
		private Unit _borderWidth;

		// Token: 0x04002802 RID: 10242
		private WebPartChrome _webPartChrome;

		// Token: 0x04002803 RID: 10243
		private WebPartMenu _menu;
	}
}
