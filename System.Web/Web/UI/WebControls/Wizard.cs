using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200050E RID: 1294
	[ToolboxData("<{0}:Wizard runat=\"server\"> <WizardSteps> <asp:WizardStep title=\"Step 1\" runat=\"server\"></asp:WizardStep> <asp:WizardStep title=\"Step 2\" runat=\"server\"></asp:WizardStep> </WizardSteps> </{0}:Wizard>")]
	[Bindable(false)]
	[DefaultEvent("FinishButtonClick")]
	[Designer("System.Web.UI.Design.WebControls.WizardDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Wizard : CompositeControl
	{
		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06003F0D RID: 16141 RVA: 0x00105F3A File Offset: 0x00104F3A
		[WebSysDescription("Wizard_ActiveStep")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public WizardStepBase ActiveStep
		{
			get
			{
				if (this.ActiveStepIndex < -1 || this.ActiveStepIndex >= this.WizardSteps.Count)
				{
					throw new InvalidOperationException(SR.GetString("Wizard_ActiveStepIndex_out_of_range"));
				}
				return this.MultiView.GetActiveView() as WizardStepBase;
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06003F0E RID: 16142 RVA: 0x00105F78 File Offset: 0x00104F78
		// (set) Token: 0x06003F0F RID: 16143 RVA: 0x00105F88 File Offset: 0x00104F88
		[DefaultValue(-1)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Wizard_ActiveStepIndex")]
		public virtual int ActiveStepIndex
		{
			get
			{
				return this.MultiView.ActiveViewIndex;
			}
			set
			{
				if (value < -1 || (value >= this.WizardSteps.Count && base.ControlState >= ControlState.FrameworkInitialized))
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("Wizard_ActiveStepIndex_out_of_range"));
				}
				if (this.MultiView.ActiveViewIndex != value)
				{
					this.MultiView.ActiveViewIndex = value;
					this._activeStepIndexSet = true;
					if (this._sideBarDataList != null && this.SideBarTemplate != null)
					{
						this._sideBarDataList.SelectedIndex = this.ActiveStepIndex;
						this._sideBarDataList.DataBind();
					}
				}
			}
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06003F10 RID: 16144 RVA: 0x00106014 File Offset: 0x00105014
		// (set) Token: 0x06003F11 RID: 16145 RVA: 0x00106041 File Offset: 0x00105041
		[WebSysDescription("Wizard_CancelButtonImageUrl")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		[UrlProperty]
		public virtual string CancelButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["CancelButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CancelButtonImageUrl"] = value;
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06003F12 RID: 16146 RVA: 0x00106054 File Offset: 0x00105054
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[WebCategory("Styles")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_CancelButtonStyle")]
		public Style CancelButtonStyle
		{
			get
			{
				if (this._cancelButtonStyle == null)
				{
					this._cancelButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._cancelButtonStyle).TrackViewState();
					}
				}
				return this._cancelButtonStyle;
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06003F13 RID: 16147 RVA: 0x00106084 File Offset: 0x00105084
		// (set) Token: 0x06003F14 RID: 16148 RVA: 0x001060B6 File Offset: 0x001050B6
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Wizard_Default_CancelButtonText")]
		[WebSysDescription("Wizard_CancelButtonText")]
		public virtual string CancelButtonText
		{
			get
			{
				string text = this.ViewState["CancelButtonText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("Wizard_Default_CancelButtonText");
			}
			set
			{
				if (value != this.CancelButtonText)
				{
					this.ViewState["CancelButtonText"] = value;
				}
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06003F15 RID: 16149 RVA: 0x001060D8 File Offset: 0x001050D8
		// (set) Token: 0x06003F16 RID: 16150 RVA: 0x00106101 File Offset: 0x00105101
		[DefaultValue(ButtonType.Button)]
		[WebCategory("Appearance")]
		[WebSysDescription("Wizard_CancelButtonType")]
		public virtual ButtonType CancelButtonType
		{
			get
			{
				object obj = this.ViewState["CancelButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				this.ValidateButtonType(value);
				this.ViewState["CancelButtonType"] = value;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003F17 RID: 16151 RVA: 0x00106120 File Offset: 0x00105120
		// (set) Token: 0x06003F18 RID: 16152 RVA: 0x0010614D File Offset: 0x0010514D
		[WebSysDescription("Wizard_CancelDestinationPageUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[UrlProperty]
		public virtual string CancelDestinationPageUrl
		{
			get
			{
				string text = this.ViewState["CancelDestinationPageUrl"] as string;
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CancelDestinationPageUrl"] = value;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06003F19 RID: 16153 RVA: 0x00106160 File Offset: 0x00105160
		// (set) Token: 0x06003F1A RID: 16154 RVA: 0x0010617C File Offset: 0x0010517C
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("Wizard_CellPadding")]
		public virtual int CellPadding
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return 0;
				}
				return ((TableStyle)base.ControlStyle).CellPadding;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellPadding = value;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06003F1B RID: 16155 RVA: 0x0010618F File Offset: 0x0010518F
		// (set) Token: 0x06003F1C RID: 16156 RVA: 0x001061AB File Offset: 0x001051AB
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("Wizard_CellSpacing")]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return 0;
				}
				return ((TableStyle)base.ControlStyle).CellSpacing;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellSpacing = value;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06003F1D RID: 16157 RVA: 0x001061BE File Offset: 0x001051BE
		internal IDictionary CustomNavigationContainers
		{
			get
			{
				if (this._customNavigationContainers == null)
				{
					this._customNavigationContainers = new Hashtable();
				}
				return this._customNavigationContainers;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x001061D9 File Offset: 0x001051D9
		internal ITemplate CustomNavigationTemplate
		{
			get
			{
				if (this.ActiveStep == null || !(this.ActiveStep is TemplatedWizardStep))
				{
					return null;
				}
				return ((TemplatedWizardStep)this.ActiveStep).CustomNavigationTemplate;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06003F1F RID: 16159 RVA: 0x00106204 File Offset: 0x00105204
		// (set) Token: 0x06003F20 RID: 16160 RVA: 0x0010622D File Offset: 0x0010522D
		[WebSysDescription("Wizard_DisplayCancelButton")]
		[DefaultValue(false)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		public virtual bool DisplayCancelButton
		{
			get
			{
				object obj = this.ViewState["DisplayCancelButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["DisplayCancelButton"] = value;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06003F21 RID: 16161 RVA: 0x00106245 File Offset: 0x00105245
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebCategory("Styles")]
		[WebSysDescription("Wizard_FinishCompleteButtonStyle")]
		public Style FinishCompleteButtonStyle
		{
			get
			{
				if (this._finishCompleteButtonStyle == null)
				{
					this._finishCompleteButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._finishCompleteButtonStyle).TrackViewState();
					}
				}
				return this._finishCompleteButtonStyle;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06003F22 RID: 16162 RVA: 0x00106274 File Offset: 0x00105274
		// (set) Token: 0x06003F23 RID: 16163 RVA: 0x001062A6 File Offset: 0x001052A6
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Wizard_Default_FinishButtonText")]
		[WebSysDescription("Wizard_FinishCompleteButtonText")]
		public virtual string FinishCompleteButtonText
		{
			get
			{
				string text = this.ViewState["FinishCompleteButtonText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("Wizard_Default_FinishButtonText");
			}
			set
			{
				this.ViewState["FinishCompleteButtonText"] = value;
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06003F24 RID: 16164 RVA: 0x001062BC File Offset: 0x001052BC
		// (set) Token: 0x06003F25 RID: 16165 RVA: 0x001062E5 File Offset: 0x001052E5
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("Wizard_FinishCompleteButtonType")]
		public virtual ButtonType FinishCompleteButtonType
		{
			get
			{
				object obj = this.ViewState["FinishCompleteButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				this.ValidateButtonType(value);
				this.ViewState["FinishCompleteButtonType"] = value;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x00106304 File Offset: 0x00105304
		// (set) Token: 0x06003F27 RID: 16167 RVA: 0x00106331 File Offset: 0x00105331
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Wizard_FinishDestinationPageUrl")]
		[UrlProperty]
		public virtual string FinishDestinationPageUrl
		{
			get
			{
				object obj = this.ViewState["FinishDestinationPageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FinishDestinationPageUrl"] = value;
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06003F28 RID: 16168 RVA: 0x00106344 File Offset: 0x00105344
		// (set) Token: 0x06003F29 RID: 16169 RVA: 0x00106371 File Offset: 0x00105371
		[WebSysDescription("Wizard_FinishCompleteButtonImageUrl")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		[UrlProperty]
		public virtual string FinishCompleteButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["FinishCompleteButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FinishCompleteButtonImageUrl"] = value;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06003F2A RID: 16170 RVA: 0x00106384 File Offset: 0x00105384
		[DefaultValue(null)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebSysDescription("Wizard_FinishPreviousButtonStyle")]
		public Style FinishPreviousButtonStyle
		{
			get
			{
				if (this._finishPreviousButtonStyle == null)
				{
					this._finishPreviousButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._finishPreviousButtonStyle).TrackViewState();
					}
				}
				return this._finishPreviousButtonStyle;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06003F2B RID: 16171 RVA: 0x001063B4 File Offset: 0x001053B4
		// (set) Token: 0x06003F2C RID: 16172 RVA: 0x001063E6 File Offset: 0x001053E6
		[WebSysDescription("Wizard_FinishPreviousButtonText")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Wizard_Default_StepPreviousButtonText")]
		public virtual string FinishPreviousButtonText
		{
			get
			{
				string text = this.ViewState["FinishPreviousButtonText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("Wizard_Default_StepPreviousButtonText");
			}
			set
			{
				this.ViewState["FinishPreviousButtonText"] = value;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06003F2D RID: 16173 RVA: 0x001063FC File Offset: 0x001053FC
		// (set) Token: 0x06003F2E RID: 16174 RVA: 0x00106425 File Offset: 0x00105425
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("Wizard_FinishPreviousButtonType")]
		public virtual ButtonType FinishPreviousButtonType
		{
			get
			{
				object obj = this.ViewState["FinishPreviousButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				this.ValidateButtonType(value);
				this.ViewState["FinishPreviousButtonType"] = value;
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06003F2F RID: 16175 RVA: 0x00106444 File Offset: 0x00105444
		// (set) Token: 0x06003F30 RID: 16176 RVA: 0x00106471 File Offset: 0x00105471
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("Wizard_FinishPreviousButtonImageUrl")]
		[DefaultValue("")]
		public virtual string FinishPreviousButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["FinishPreviousButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FinishPreviousButtonImageUrl"] = value;
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06003F31 RID: 16177 RVA: 0x00106484 File Offset: 0x00105484
		internal bool IsMacIE5
		{
			get
			{
				if (!this._isMacIESet && !base.DesignMode)
				{
					HttpBrowserCapabilities httpBrowserCapabilities = null;
					if (this.Page != null)
					{
						httpBrowserCapabilities = this.Page.Request.Browser;
					}
					else
					{
						HttpContext httpContext = HttpContext.Current;
						if (httpContext != null)
						{
							httpBrowserCapabilities = httpContext.Request.Browser;
						}
					}
					if (httpBrowserCapabilities != null)
					{
						this._isMacIE = (httpBrowserCapabilities.Type == "IE5" && httpBrowserCapabilities.Platform == "MacPPC");
					}
					this._isMacIESet = true;
				}
				return this._isMacIE;
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06003F32 RID: 16178 RVA: 0x0010650E File Offset: 0x0010550E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebCategory("Styles")]
		[WebSysDescription("Wizard_StartNextButtonStyle")]
		public Style StartNextButtonStyle
		{
			get
			{
				if (this._startNextButtonStyle == null)
				{
					this._startNextButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._startNextButtonStyle).TrackViewState();
					}
				}
				return this._startNextButtonStyle;
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06003F33 RID: 16179 RVA: 0x0010653C File Offset: 0x0010553C
		// (set) Token: 0x06003F34 RID: 16180 RVA: 0x0010656E File Offset: 0x0010556E
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Wizard_Default_StepNextButtonText")]
		[Localizable(true)]
		[WebSysDescription("Wizard_StartNextButtonText")]
		public virtual string StartNextButtonText
		{
			get
			{
				string text = this.ViewState["StartNextButtonText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("Wizard_Default_StepNextButtonText");
			}
			set
			{
				this.ViewState["StartNextButtonText"] = value;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06003F35 RID: 16181 RVA: 0x00106584 File Offset: 0x00105584
		// (set) Token: 0x06003F36 RID: 16182 RVA: 0x001065AD File Offset: 0x001055AD
		[WebSysDescription("Wizard_StartNextButtonType")]
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType StartNextButtonType
		{
			get
			{
				object obj = this.ViewState["StartNextButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				this.ValidateButtonType(value);
				this.ViewState["StartNextButtonType"] = value;
			}
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x001065CC File Offset: 0x001055CC
		// (set) Token: 0x06003F38 RID: 16184 RVA: 0x001065F9 File Offset: 0x001055F9
		[WebCategory("Appearance")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[WebSysDescription("Wizard_StartNextButtonImageUrl")]
		public virtual string StartNextButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["StartNextButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["StartNextButtonImageUrl"] = value;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x0010660C File Offset: 0x0010560C
		// (set) Token: 0x06003F3A RID: 16186 RVA: 0x00106614 File Offset: 0x00105614
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard))]
		[WebSysDescription("Wizard_FinishNavigationTemplate")]
		[DefaultValue(null)]
		public virtual ITemplate FinishNavigationTemplate
		{
			get
			{
				return this._finishNavigationTemplate;
			}
			set
			{
				this._finishNavigationTemplate = value;
				this.RequiresControlsRecreation();
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x00106623 File Offset: 0x00105623
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("WebControl_HeaderStyle")]
		public TableItemStyle HeaderStyle
		{
			get
			{
				if (this._headerStyle == null)
				{
					this._headerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._headerStyle).TrackViewState();
					}
				}
				return this._headerStyle;
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06003F3C RID: 16188 RVA: 0x00106651 File Offset: 0x00105651
		// (set) Token: 0x06003F3D RID: 16189 RVA: 0x00106659 File Offset: 0x00105659
		[Browsable(false)]
		[TemplateContainer(typeof(Wizard))]
		[WebSysDescription("WebControl_HeaderTemplate")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this._headerTemplate;
			}
			set
			{
				this._headerTemplate = value;
				this.RequiresControlsRecreation();
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06003F3E RID: 16190 RVA: 0x00106668 File Offset: 0x00105668
		// (set) Token: 0x06003F3F RID: 16191 RVA: 0x00106695 File Offset: 0x00105695
		[DefaultValue("")]
		[WebSysDescription("Wizard_HeaderText")]
		[WebCategory("Appearance")]
		[Localizable(true)]
		public virtual string HeaderText
		{
			get
			{
				string text = this.ViewState["HeaderText"] as string;
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06003F40 RID: 16192 RVA: 0x001066A8 File Offset: 0x001056A8
		private Stack History
		{
			get
			{
				if (this._historyStack == null)
				{
					this._historyStack = new Stack();
				}
				return this._historyStack;
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06003F41 RID: 16193 RVA: 0x001066C4 File Offset: 0x001056C4
		internal MultiView MultiView
		{
			get
			{
				if (this._multiView == null)
				{
					this._multiView = new MultiView();
					this._multiView.EnableTheming = true;
					this._multiView.ID = "WizardMultiView";
					this._multiView.ActiveViewChanged += this.MultiViewActiveViewChanged;
					this._multiView.IgnoreBubbleEvents();
				}
				return this._multiView;
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06003F42 RID: 16194 RVA: 0x00106728 File Offset: 0x00105728
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_NavigationButtonStyle")]
		public Style NavigationButtonStyle
		{
			get
			{
				if (this._navigationButtonStyle == null)
				{
					this._navigationButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._navigationButtonStyle).TrackViewState();
					}
				}
				return this._navigationButtonStyle;
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06003F43 RID: 16195 RVA: 0x00106756 File Offset: 0x00105756
		internal TableCell NavigationTableCell
		{
			get
			{
				if (this._navigationTableCell == null)
				{
					this._navigationTableCell = new TableCell();
				}
				return this._navigationTableCell;
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06003F44 RID: 16196 RVA: 0x00106771 File Offset: 0x00105771
		[NotifyParentProperty(true)]
		[WebSysDescription("Wizard_NavigationStyle")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle NavigationStyle
		{
			get
			{
				if (this._navigationStyle == null)
				{
					this._navigationStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._navigationStyle).TrackViewState();
					}
				}
				return this._navigationStyle;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06003F45 RID: 16197 RVA: 0x0010679F File Offset: 0x0010579F
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_StepNextButtonStyle")]
		public Style StepNextButtonStyle
		{
			get
			{
				if (this._stepNextButtonStyle == null)
				{
					this._stepNextButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._stepNextButtonStyle).TrackViewState();
					}
				}
				return this._stepNextButtonStyle;
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06003F46 RID: 16198 RVA: 0x001067D0 File Offset: 0x001057D0
		// (set) Token: 0x06003F47 RID: 16199 RVA: 0x00106802 File Offset: 0x00105802
		[WebSysDescription("Wizard_StepNextButtonText")]
		[Localizable(true)]
		[WebSysDefaultValue("Wizard_Default_StepNextButtonText")]
		[WebCategory("Appearance")]
		public virtual string StepNextButtonText
		{
			get
			{
				string text = this.ViewState["StepNextButtonText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("Wizard_Default_StepNextButtonText");
			}
			set
			{
				this.ViewState["StepNextButtonText"] = value;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06003F48 RID: 16200 RVA: 0x00106818 File Offset: 0x00105818
		// (set) Token: 0x06003F49 RID: 16201 RVA: 0x00106841 File Offset: 0x00105841
		[WebSysDescription("Wizard_StepNextButtonType")]
		[DefaultValue(ButtonType.Button)]
		[WebCategory("Appearance")]
		public virtual ButtonType StepNextButtonType
		{
			get
			{
				object obj = this.ViewState["StepNextButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				this.ValidateButtonType(value);
				this.ViewState["StepNextButtonType"] = value;
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x00106860 File Offset: 0x00105860
		// (set) Token: 0x06003F4B RID: 16203 RVA: 0x0010688D File Offset: 0x0010588D
		[WebSysDescription("Wizard_StepNextButtonImageUrl")]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public virtual string StepNextButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["StepNextButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["StepNextButtonImageUrl"] = value;
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06003F4C RID: 16204 RVA: 0x001068A0 File Offset: 0x001058A0
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebCategory("Styles")]
		[WebSysDescription("Wizard_StepPreviousButtonStyle")]
		[DefaultValue(null)]
		public Style StepPreviousButtonStyle
		{
			get
			{
				if (this._stepPreviousButtonStyle == null)
				{
					this._stepPreviousButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._stepPreviousButtonStyle).TrackViewState();
					}
				}
				return this._stepPreviousButtonStyle;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06003F4D RID: 16205 RVA: 0x001068D0 File Offset: 0x001058D0
		// (set) Token: 0x06003F4E RID: 16206 RVA: 0x00106902 File Offset: 0x00105902
		[Localizable(true)]
		[WebSysDefaultValue("Wizard_Default_StepPreviousButtonText")]
		[WebSysDescription("Wizard_StepPreviousButtonText")]
		[WebCategory("Appearance")]
		public virtual string StepPreviousButtonText
		{
			get
			{
				string text = this.ViewState["StepPreviousButtonText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("Wizard_Default_StepPreviousButtonText");
			}
			set
			{
				this.ViewState["StepPreviousButtonText"] = value;
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06003F4F RID: 16207 RVA: 0x00106918 File Offset: 0x00105918
		// (set) Token: 0x06003F50 RID: 16208 RVA: 0x00106941 File Offset: 0x00105941
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("Wizard_StepPreviousButtonType")]
		public virtual ButtonType StepPreviousButtonType
		{
			get
			{
				object obj = this.ViewState["StepPreviousButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				this.ValidateButtonType(value);
				this.ViewState["StepPreviousButtonType"] = value;
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06003F51 RID: 16209 RVA: 0x00106960 File Offset: 0x00105960
		// (set) Token: 0x06003F52 RID: 16210 RVA: 0x0010698D File Offset: 0x0010598D
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("Wizard_StepPreviousButtonImageUrl")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public virtual string StepPreviousButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["StepPreviousButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["StepPreviousButtonImageUrl"] = value;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06003F53 RID: 16211 RVA: 0x001069A0 File Offset: 0x001059A0
		internal virtual bool ShowCustomNavigationTemplate
		{
			get
			{
				return this.CustomNavigationTemplate != null;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06003F54 RID: 16212 RVA: 0x001069AE File Offset: 0x001059AE
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_SideBarButtonStyle")]
		public Style SideBarButtonStyle
		{
			get
			{
				if (this._sideBarButtonStyle == null)
				{
					this._sideBarButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._sideBarButtonStyle).TrackViewState();
					}
				}
				return this._sideBarButtonStyle;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x001069DC File Offset: 0x001059DC
		internal DataList SideBarDataList
		{
			get
			{
				return this._sideBarDataList;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x001069E4 File Offset: 0x001059E4
		// (set) Token: 0x06003F57 RID: 16215 RVA: 0x001069EC File Offset: 0x001059EC
		[DefaultValue(true)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Wizard_DisplaySideBar")]
		public virtual bool DisplaySideBar
		{
			get
			{
				return this._displaySideBar;
			}
			set
			{
				if (value != this._displaySideBar)
				{
					this._displaySideBar = value;
					this._sideBarTableCell = null;
					this.RequiresControlsRecreation();
				}
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06003F58 RID: 16216 RVA: 0x00106A0B File Offset: 0x00105A0B
		internal bool SideBarEnabled
		{
			get
			{
				return this._sideBarDataList != null && this.DisplaySideBar;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06003F59 RID: 16217 RVA: 0x00106A1D File Offset: 0x00105A1D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebSysDescription("Wizard_SideBarStyle")]
		public TableItemStyle SideBarStyle
		{
			get
			{
				if (this._sideBarStyle == null)
				{
					this._sideBarStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._sideBarStyle).TrackViewState();
					}
				}
				return this._sideBarStyle;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06003F5A RID: 16218 RVA: 0x00106A4B File Offset: 0x00105A4B
		// (set) Token: 0x06003F5B RID: 16219 RVA: 0x00106A53 File Offset: 0x00105A53
		[TemplateContainer(typeof(Wizard))]
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_SideBarTemplate")]
		public virtual ITemplate SideBarTemplate
		{
			get
			{
				return this._sideBarTemplate;
			}
			set
			{
				this._sideBarTemplate = value;
				this._sideBarTableCell = null;
				this.RequiresControlsRecreation();
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06003F5C RID: 16220 RVA: 0x00106A6C File Offset: 0x00105A6C
		// (set) Token: 0x06003F5D RID: 16221 RVA: 0x00106A8F File Offset: 0x00105A8F
		[WebCategory("Appearance")]
		[Localizable(true)]
		[WebSysDescription("WebControl_SkipLinkText")]
		[WebSysDefaultValue("Wizard_Default_SkipToContentText")]
		public virtual string SkipLinkText
		{
			get
			{
				string skipLinkTextInternal = this.SkipLinkTextInternal;
				if (skipLinkTextInternal != null)
				{
					return skipLinkTextInternal;
				}
				return SR.GetString("Wizard_Default_SkipToContentText");
			}
			set
			{
				this.ViewState["SkipLinkText"] = value;
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06003F5E RID: 16222 RVA: 0x00106AA2 File Offset: 0x00105AA2
		internal string SkipLinkTextInternal
		{
			get
			{
				return this.ViewState["SkipLinkText"] as string;
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06003F5F RID: 16223 RVA: 0x00106AB9 File Offset: 0x00105AB9
		// (set) Token: 0x06003F60 RID: 16224 RVA: 0x00106AC1 File Offset: 0x00105AC1
		[WebSysDescription("Wizard_StartNavigationTemplate")]
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard))]
		public virtual ITemplate StartNavigationTemplate
		{
			get
			{
				return this._startNavigationTemplate;
			}
			set
			{
				this._startNavigationTemplate = value;
				this.RequiresControlsRecreation();
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x00106AD0 File Offset: 0x00105AD0
		// (set) Token: 0x06003F62 RID: 16226 RVA: 0x00106AD8 File Offset: 0x00105AD8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_StepNavigationTemplate")]
		[Browsable(false)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(Wizard))]
		public virtual ITemplate StepNavigationTemplate
		{
			get
			{
				return this._stepNavigationTemplate;
			}
			set
			{
				this._stepNavigationTemplate = value;
				this.RequiresControlsRecreation();
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06003F63 RID: 16227 RVA: 0x00106AE7 File Offset: 0x00105AE7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		[WebSysDescription("Wizard_StepStyle")]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle StepStyle
		{
			get
			{
				if (this._stepStyle == null)
				{
					this._stepStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._stepStyle).TrackViewState();
					}
				}
				return this._stepStyle;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x00106B15 File Offset: 0x00105B15
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x00106B19 File Offset: 0x00105B19
		internal ArrayList TemplatedSteps
		{
			get
			{
				if (this._templatedSteps == null)
				{
					this._templatedSteps = new ArrayList();
				}
				return this._templatedSteps;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x00106B34 File Offset: 0x00105B34
		[Editor("System.Web.UI.Design.WebControls.WizardStepCollectionEditor,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("Wizard_WizardSteps")]
		public virtual WizardStepCollection WizardSteps
		{
			get
			{
				if (this._wizardStepCollection == null)
				{
					this._wizardStepCollection = new WizardStepCollection(this);
				}
				return this._wizardStepCollection;
			}
		}

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06003F67 RID: 16231 RVA: 0x00106B50 File Offset: 0x00105B50
		// (remove) Token: 0x06003F68 RID: 16232 RVA: 0x00106B63 File Offset: 0x00105B63
		[WebSysDescription("Wizard_ActiveStepChanged")]
		[WebCategory("Action")]
		public event EventHandler ActiveStepChanged
		{
			add
			{
				base.Events.AddHandler(Wizard._eventActiveStepChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard._eventActiveStepChanged, value);
			}
		}

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06003F69 RID: 16233 RVA: 0x00106B76 File Offset: 0x00105B76
		// (remove) Token: 0x06003F6A RID: 16234 RVA: 0x00106B89 File Offset: 0x00105B89
		[WebSysDescription("Wizard_CancelButtonClick")]
		[WebCategory("Action")]
		public event EventHandler CancelButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard._eventCancelButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard._eventCancelButtonClick, value);
			}
		}

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x06003F6B RID: 16235 RVA: 0x00106B9C File Offset: 0x00105B9C
		// (remove) Token: 0x06003F6C RID: 16236 RVA: 0x00106BAF File Offset: 0x00105BAF
		[WebSysDescription("Wizard_FinishButtonClick")]
		[WebCategory("Action")]
		public event WizardNavigationEventHandler FinishButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard._eventFinishButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard._eventFinishButtonClick, value);
			}
		}

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x06003F6D RID: 16237 RVA: 0x00106BC2 File Offset: 0x00105BC2
		// (remove) Token: 0x06003F6E RID: 16238 RVA: 0x00106BD5 File Offset: 0x00105BD5
		[WebSysDescription("Wizard_NextButtonClick")]
		[WebCategory("Action")]
		public event WizardNavigationEventHandler NextButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard._eventNextButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard._eventNextButtonClick, value);
			}
		}

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x06003F6F RID: 16239 RVA: 0x00106BE8 File Offset: 0x00105BE8
		// (remove) Token: 0x06003F70 RID: 16240 RVA: 0x00106BFB File Offset: 0x00105BFB
		[WebCategory("Action")]
		[WebSysDescription("Wizard_PreviousButtonClick")]
		public event WizardNavigationEventHandler PreviousButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard._eventPreviousButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard._eventPreviousButtonClick, value);
			}
		}

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06003F71 RID: 16241 RVA: 0x00106C0E File Offset: 0x00105C0E
		// (remove) Token: 0x06003F72 RID: 16242 RVA: 0x00106C21 File Offset: 0x00105C21
		[WebCategory("Action")]
		[WebSysDescription("Wizard_SideBarButtonClick")]
		public virtual event WizardNavigationEventHandler SideBarButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard._eventSideBarButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard._eventSideBarButtonClick, value);
			}
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x00106C34 File Offset: 0x00105C34
		private void MultiViewActiveViewChanged(object source, EventArgs e)
		{
			this.OnActiveStepChanged(this, EventArgs.Empty);
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x00106C42 File Offset: 0x00105C42
		private void ApplyButtonProperties(ButtonType type, string text, string imageUrl, IButtonControl button)
		{
			this.ApplyButtonProperties(type, text, imageUrl, button, true);
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x00106C50 File Offset: 0x00105C50
		private void ApplyButtonProperties(ButtonType type, string text, string imageUrl, IButtonControl button, bool imageButtonVisible)
		{
			if (button == null)
			{
				return;
			}
			if (button is ImageButton)
			{
				ImageButton imageButton = (ImageButton)button;
				imageButton.ImageUrl = imageUrl;
				imageButton.AlternateText = text;
				if (button is Control)
				{
					((Control)button).Visible = imageButtonVisible;
					return;
				}
			}
			else
			{
				button.Text = text;
			}
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x00106CA4 File Offset: 0x00105CA4
		internal virtual void ApplyControlProperties()
		{
			if (!base.DesignMode && (this.ActiveStepIndex < 0 || this.ActiveStepIndex >= this.WizardSteps.Count || this.WizardSteps.Count == 0))
			{
				return;
			}
			if (this.SideBarEnabled && this._sideBarStyle != null)
			{
				this._sideBarTableCell.ApplyStyle(this._sideBarStyle);
			}
			if (this._headerTableRow != null)
			{
				if (this.HeaderTemplate == null && string.IsNullOrEmpty(this.HeaderText))
				{
					this._headerTableRow.Visible = false;
				}
				else
				{
					this._headerTableCell.ApplyStyle(this._headerStyle);
					if (this.HeaderTemplate != null)
					{
						if (this._titleLiteral != null)
						{
							this._titleLiteral.Visible = false;
						}
					}
					else if (this._titleLiteral != null)
					{
						this._titleLiteral.Text = this.HeaderText;
					}
				}
			}
			if (this._stepTableCell != null && this._stepStyle != null)
			{
				if (!base.DesignMode && this.IsMacIE5 && this._stepStyle.Height == Unit.Empty)
				{
					this._stepStyle.Height = Unit.Pixel(1);
				}
				this._stepTableCell.ApplyStyle(this._stepStyle);
			}
			this.ApplyNavigationTemplateProperties();
			foreach (object obj in this.CustomNavigationContainers.Values)
			{
				Control control = (Control)obj;
				control.Visible = false;
			}
			if (this._navigationTableCell != null)
			{
				this.NavigationTableCell.HorizontalAlign = HorizontalAlign.Right;
				if (this._navigationStyle != null)
				{
					if (!base.DesignMode && this.IsMacIE5 && this._navigationStyle.Height == Unit.Empty)
					{
						this._navigationStyle.Height = Unit.Pixel(1);
					}
					this._navigationTableCell.ApplyStyle(this._navigationStyle);
				}
			}
			if (this.ShowCustomNavigationTemplate)
			{
				Wizard.BaseNavigationTemplateContainer baseNavigationTemplateContainer = (Wizard.BaseNavigationTemplateContainer)this._customNavigationContainers[this.ActiveStep];
				baseNavigationTemplateContainer.Visible = true;
				this._startNavigationTemplateContainer.Visible = false;
				this._stepNavigationTemplateContainer.Visible = false;
				this._finishNavigationTemplateContainer.Visible = false;
				this._navigationRow.Visible = true;
			}
			if (this.SideBarEnabled)
			{
				this._sideBarDataList.DataSource = this.WizardSteps;
				this._sideBarDataList.SelectedIndex = this.ActiveStepIndex;
				this._sideBarDataList.DataBind();
				if (this.SideBarTemplate == null)
				{
					foreach (object obj2 in this._sideBarDataList.Items)
					{
						DataListItem dataListItem = (DataListItem)obj2;
						WebControl webControl = dataListItem.FindControl(Wizard.SideBarButtonID) as WebControl;
						if (webControl != null)
						{
							webControl.MergeStyle(this._sideBarButtonStyle);
						}
					}
				}
			}
			if (this._renderTable != null)
			{
				Util.CopyBaseAttributesToInnerControl(this, this._renderTable);
				if (base.ControlStyleCreated)
				{
					this._renderTable.ApplyStyle(base.ControlStyle);
				}
				else
				{
					this._renderTable.CellSpacing = 0;
					this._renderTable.CellPadding = 0;
				}
				if (!base.DesignMode && this.IsMacIE5 && (!base.ControlStyleCreated || base.ControlStyle.Height == Unit.Empty))
				{
					this._renderTable.ControlStyle.Height = Unit.Pixel(1);
				}
			}
			if (!base.DesignMode && this._navigationTableCell != null && this.IsMacIE5)
			{
				this._navigationTableCell.ControlStyle.Height = Unit.Pixel(1);
			}
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x00107054 File Offset: 0x00106054
		private void ApplyNavigationTemplateProperties()
		{
			if (this._finishNavigationTemplateContainer == null || this._startNavigationTemplateContainer == null || this._stepNavigationTemplateContainer == null)
			{
				return;
			}
			if (this.ActiveStepIndex >= this.WizardSteps.Count || this.ActiveStepIndex < 0)
			{
				return;
			}
			WizardStepType wizardStepType = this.SetActiveTemplates();
			bool flag = wizardStepType != WizardStepType.Finish || this.ActiveStepIndex != 0 || this.ActiveStep.StepType != WizardStepType.Auto;
			if (this.StartNavigationTemplate == null)
			{
				if (base.DesignMode)
				{
					this._defaultStartNavigationTemplate.ResetButtonsVisibility();
				}
				this._startNavigationTemplateContainer.NextButton = this._defaultStartNavigationTemplate.SecondButton;
				((Control)this._startNavigationTemplateContainer.NextButton).Visible = true;
				this._startNavigationTemplateContainer.CancelButton = this._defaultStartNavigationTemplate.CancelButton;
				this.ApplyButtonProperties(this.StartNextButtonType, this.StartNextButtonText, this.StartNextButtonImageUrl, this._startNavigationTemplateContainer.NextButton);
				this.ApplyButtonProperties(this.CancelButtonType, this.CancelButtonText, this.CancelButtonImageUrl, this._startNavigationTemplateContainer.CancelButton);
				this.SetCancelButtonVisibility(this._startNavigationTemplateContainer);
				this._startNavigationTemplateContainer.ApplyButtonStyle(this.FinishCompleteButtonStyle, this.StepPreviousButtonStyle, this.StartNextButtonStyle, this.CancelButtonStyle);
			}
			bool imageButtonVisible = true;
			int previousStepIndex = this.GetPreviousStepIndex(false);
			if (previousStepIndex >= 0)
			{
				imageButtonVisible = this.WizardSteps[previousStepIndex].AllowReturn;
			}
			if (this.FinishNavigationTemplate == null)
			{
				if (base.DesignMode)
				{
					this._defaultFinishNavigationTemplate.ResetButtonsVisibility();
				}
				this._finishNavigationTemplateContainer.PreviousButton = this._defaultFinishNavigationTemplate.FirstButton;
				((Control)this._finishNavigationTemplateContainer.PreviousButton).Visible = true;
				this._finishNavigationTemplateContainer.FinishButton = this._defaultFinishNavigationTemplate.SecondButton;
				((Control)this._finishNavigationTemplateContainer.FinishButton).Visible = true;
				this._finishNavigationTemplateContainer.CancelButton = this._defaultFinishNavigationTemplate.CancelButton;
				this._finishNavigationTemplateContainer.FinishButton.CommandName = Wizard.MoveCompleteCommandName;
				this.ApplyButtonProperties(this.FinishCompleteButtonType, this.FinishCompleteButtonText, this.FinishCompleteButtonImageUrl, this._finishNavigationTemplateContainer.FinishButton);
				this.ApplyButtonProperties(this.FinishPreviousButtonType, this.FinishPreviousButtonText, this.FinishPreviousButtonImageUrl, this._finishNavigationTemplateContainer.PreviousButton, imageButtonVisible);
				this.ApplyButtonProperties(this.CancelButtonType, this.CancelButtonText, this.CancelButtonImageUrl, this._finishNavigationTemplateContainer.CancelButton);
				int previousStepIndex2 = this.GetPreviousStepIndex(false);
				if (previousStepIndex2 != -1 && !this.WizardSteps[previousStepIndex2].AllowReturn)
				{
					((Control)this._finishNavigationTemplateContainer.PreviousButton).Visible = false;
				}
				this.SetCancelButtonVisibility(this._finishNavigationTemplateContainer);
				this._finishNavigationTemplateContainer.ApplyButtonStyle(this.FinishCompleteButtonStyle, this.FinishPreviousButtonStyle, this.StepNextButtonStyle, this.CancelButtonStyle);
			}
			if (this.StepNavigationTemplate == null)
			{
				if (base.DesignMode)
				{
					this._defaultStepNavigationTemplate.ResetButtonsVisibility();
				}
				this._stepNavigationTemplateContainer.PreviousButton = this._defaultStepNavigationTemplate.FirstButton;
				((Control)this._stepNavigationTemplateContainer.PreviousButton).Visible = true;
				this._stepNavigationTemplateContainer.NextButton = this._defaultStepNavigationTemplate.SecondButton;
				((Control)this._stepNavigationTemplateContainer.NextButton).Visible = true;
				this._stepNavigationTemplateContainer.CancelButton = this._defaultStepNavigationTemplate.CancelButton;
				this.ApplyButtonProperties(this.StepNextButtonType, this.StepNextButtonText, this.StepNextButtonImageUrl, this._stepNavigationTemplateContainer.NextButton);
				this.ApplyButtonProperties(this.StepPreviousButtonType, this.StepPreviousButtonText, this.StepPreviousButtonImageUrl, this._stepNavigationTemplateContainer.PreviousButton, imageButtonVisible);
				this.ApplyButtonProperties(this.CancelButtonType, this.CancelButtonText, this.CancelButtonImageUrl, this._stepNavigationTemplateContainer.CancelButton);
				int previousStepIndex3 = this.GetPreviousStepIndex(false);
				if (previousStepIndex3 != -1 && !this.WizardSteps[previousStepIndex3].AllowReturn)
				{
					((Control)this._stepNavigationTemplateContainer.PreviousButton).Visible = false;
				}
				this.SetCancelButtonVisibility(this._stepNavigationTemplateContainer);
				this._stepNavigationTemplateContainer.ApplyButtonStyle(this.FinishCompleteButtonStyle, this.StepPreviousButtonStyle, this.StepNextButtonStyle, this.CancelButtonStyle);
			}
			if (!flag)
			{
				Control control = this._finishNavigationTemplateContainer.PreviousButton as Control;
				if (control != null)
				{
					if (this.FinishNavigationTemplate == null)
					{
						control.Parent.Visible = false;
						return;
					}
					control.Visible = false;
				}
			}
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x001074BC File Offset: 0x001064BC
		internal Wizard.BaseNavigationTemplateContainer CreateBaseNavigationTemplateContainer(string id)
		{
			return new Wizard.BaseNavigationTemplateContainer(this)
			{
				ID = id
			};
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x001074D8 File Offset: 0x001064D8
		protected internal override void CreateChildControls()
		{
			using (new Wizard.WizardControlCollectionModifier(this))
			{
				this.Controls.Clear();
				this._customNavigationContainers = null;
				this._navigationTableCell = null;
			}
			this.CreateControlHierarchy();
			base.ClearChildViewState();
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x00107530 File Offset: 0x00106530
		protected override ControlCollection CreateControlCollection()
		{
			return new Wizard.WizardControlCollection(this);
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x00107538 File Offset: 0x00106538
		protected virtual void CreateControlHierarchy()
		{
			Table table = null;
			if (this.DisplaySideBar)
			{
				Table table2 = new Wizard.WizardChildTable(this);
				table2.EnableTheming = false;
				table = new WizardDefaultInnerTable();
				table.CellSpacing = 0;
				table.Height = Unit.Percentage(100.0);
				table.Width = Unit.Percentage(100.0);
				TableRow tableRow = new TableRow();
				table2.Controls.Add(tableRow);
				if (this._sideBarTableCell == null)
				{
					TableCell tableCell = new Wizard.AccessibleTableCell(this);
					tableCell.ID = "SideBarContainer";
					tableCell.Height = Unit.Percentage(100.0);
					this._sideBarTableCell = tableCell;
					tableRow.Controls.Add(tableCell);
					ITemplate template = this.SideBarTemplate;
					if (template == null)
					{
						this._sideBarTableCell.EnableViewState = false;
						template = this.CreateDefaultSideBarTemplate();
					}
					else
					{
						this._sideBarTableCell.EnableTheming = this.EnableTheming;
					}
					template.InstantiateIn(this._sideBarTableCell);
				}
				else
				{
					tableRow.Controls.Add(this._sideBarTableCell);
				}
				this._renderSideBarDataList = false;
				TableCell tableCell2 = new TableCell();
				tableCell2.Height = Unit.Percentage(100.0);
				tableRow.Controls.Add(tableCell2);
				tableCell2.Controls.Add(table);
				if (!base.DesignMode && this.IsMacIE5)
				{
					tableCell2.Height = Unit.Pixel(1);
				}
				using (new Wizard.WizardControlCollectionModifier(this))
				{
					this.Controls.Add(table2);
				}
				if (this._sideBarDataList != null)
				{
					this._sideBarDataList.ItemCommand -= this.DataListItemCommand;
					this._sideBarDataList.ItemDataBound -= this.DataListItemDataBound;
				}
				this._sideBarDataList = (this._sideBarTableCell.FindControl(Wizard.DataListID) as DataList);
				if (this._sideBarDataList != null)
				{
					this._sideBarDataList.ItemCommand += this.DataListItemCommand;
					this._sideBarDataList.ItemDataBound += this.DataListItemDataBound;
					this._sideBarDataList.DataSource = this.WizardSteps;
					this._sideBarDataList.SelectedIndex = this.ActiveStepIndex;
					this._sideBarDataList.DataBind();
				}
				else if (!base.DesignMode)
				{
					throw new InvalidOperationException(SR.GetString("Wizard_DataList_Not_Found", new object[]
					{
						Wizard.DataListID
					}));
				}
				this._renderTable = table2;
			}
			else
			{
				table = new Wizard.WizardChildTable(this);
				table.EnableTheming = false;
				using (new Wizard.WizardControlCollectionModifier(this))
				{
					this.Controls.Add(table);
				}
				this._renderTable = table;
			}
			this._headerTableRow = new TableRow();
			table.Controls.Add(this._headerTableRow);
			this._headerTableCell = new Wizard.InternalTableCell(this);
			this._headerTableCell.ID = "HeaderContainer";
			if (this.HeaderTemplate != null)
			{
				this._headerTableCell.EnableTheming = this.EnableTheming;
				this.HeaderTemplate.InstantiateIn(this._headerTableCell);
			}
			else
			{
				this._titleLiteral = new LiteralControl();
				this._headerTableCell.Controls.Add(this._titleLiteral);
			}
			this._headerTableRow.Controls.Add(this._headerTableCell);
			TableRow tableRow2 = new TableRow();
			tableRow2.Height = Unit.Percentage(100.0);
			table.Controls.Add(tableRow2);
			this._stepTableCell = new TableCell();
			tableRow2.Controls.Add(this._stepTableCell);
			this._navigationRow = new TableRow();
			table.Controls.Add(this._navigationRow);
			this._navigationRow.Controls.Add(this.NavigationTableCell);
			this._stepTableCell.Controls.Add(this.MultiView);
			this.InstantiateStepContentTemplates();
			this.CreateNavigationControlHierarchy();
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x00107930 File Offset: 0x00106930
		internal virtual ITemplate CreateDefaultSideBarTemplate()
		{
			return new Wizard.DefaultSideBarTemplate(this);
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x00107938 File Offset: 0x00106938
		internal virtual ITemplate CreateDefaultDataListItemTemplate()
		{
			return new Wizard.DataListItemTemplate(this);
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x00107940 File Offset: 0x00106940
		private void CreateStartNavigationTemplate()
		{
			ITemplate template = this.StartNavigationTemplate;
			this._startNavigationTemplateContainer = new Wizard.StartNavigationTemplateContainer(this);
			this._startNavigationTemplateContainer.ID = "StartNavigationTemplateContainerID";
			if (template == null)
			{
				this._startNavigationTemplateContainer.EnableViewState = false;
				this._defaultStartNavigationTemplate = Wizard.NavigationTemplate.GetDefaultStartNavigationTemplate(this);
				template = this._defaultStartNavigationTemplate;
			}
			else
			{
				this._startNavigationTemplateContainer.SetEnableTheming();
			}
			template.InstantiateIn(this._startNavigationTemplateContainer);
			this.NavigationTableCell.Controls.Add(this._startNavigationTemplateContainer);
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x001079C4 File Offset: 0x001069C4
		private void CreateStepNavigationTemplate()
		{
			ITemplate template = this.StepNavigationTemplate;
			this._stepNavigationTemplateContainer = new Wizard.StepNavigationTemplateContainer(this);
			this._stepNavigationTemplateContainer.ID = "StepNavigationTemplateContainerID";
			if (template == null)
			{
				this._stepNavigationTemplateContainer.EnableViewState = false;
				this._defaultStepNavigationTemplate = Wizard.NavigationTemplate.GetDefaultStepNavigationTemplate(this);
				template = this._defaultStepNavigationTemplate;
			}
			else
			{
				this._stepNavigationTemplateContainer.SetEnableTheming();
			}
			template.InstantiateIn(this._stepNavigationTemplateContainer);
			this.NavigationTableCell.Controls.Add(this._stepNavigationTemplateContainer);
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x00107A48 File Offset: 0x00106A48
		private void CreateFinishNavigationTemplate()
		{
			ITemplate template = this.FinishNavigationTemplate;
			this._finishNavigationTemplateContainer = new Wizard.FinishNavigationTemplateContainer(this);
			this._finishNavigationTemplateContainer.ID = "FinishNavigationTemplateContainerID";
			if (template == null)
			{
				this._finishNavigationTemplateContainer.EnableViewState = false;
				this._defaultFinishNavigationTemplate = Wizard.NavigationTemplate.GetDefaultFinishNavigationTemplate(this);
				template = this._defaultFinishNavigationTemplate;
			}
			else
			{
				this._finishNavigationTemplateContainer.SetEnableTheming();
			}
			template.InstantiateIn(this._finishNavigationTemplateContainer);
			this.NavigationTableCell.Controls.Add(this._finishNavigationTemplateContainer);
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x00107ACC File Offset: 0x00106ACC
		protected override Style CreateControlStyle()
		{
			return new TableStyle
			{
				CellSpacing = 0,
				CellPadding = 0
			};
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x00107AF0 File Offset: 0x00106AF0
		internal virtual void CreateCustomNavigationTemplates()
		{
			for (int i = 0; i < this.WizardSteps.Count; i++)
			{
				TemplatedWizardStep templatedWizardStep = this.WizardSteps[i] as TemplatedWizardStep;
				if (templatedWizardStep != null)
				{
					this.RegisterCustomNavigationContainers(templatedWizardStep);
				}
			}
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x00107B30 File Offset: 0x00106B30
		internal void RegisterCustomNavigationContainers(TemplatedWizardStep step)
		{
			this.InstantiateStepContentTemplate(step);
			if (!this.CustomNavigationContainers.Contains(step))
			{
				string customContainerID = this.GetCustomContainerID(this.WizardSteps.IndexOf(step));
				Wizard.BaseNavigationTemplateContainer baseNavigationTemplateContainer;
				if (step.CustomNavigationTemplate != null)
				{
					baseNavigationTemplateContainer = this.CreateBaseNavigationTemplateContainer(customContainerID);
					step.CustomNavigationTemplate.InstantiateIn(baseNavigationTemplateContainer);
					step.CustomNavigationTemplateContainer = baseNavigationTemplateContainer;
					baseNavigationTemplateContainer.RegisterButtonCommandEvents();
				}
				else
				{
					baseNavigationTemplateContainer = this.CreateBaseNavigationTemplateContainer(customContainerID);
					baseNavigationTemplateContainer.RegisterButtonCommandEvents();
				}
				this.CustomNavigationContainers[step] = baseNavigationTemplateContainer;
			}
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x00107BB0 File Offset: 0x00106BB0
		internal void CreateNavigationControlHierarchy()
		{
			this.NavigationTableCell.Controls.Clear();
			this.CustomNavigationContainers.Clear();
			this.CreateCustomNavigationTemplates();
			foreach (object obj in this.CustomNavigationContainers.Values)
			{
				Control child = (Control)obj;
				this.NavigationTableCell.Controls.Add(child);
			}
			this.CreateStartNavigationTemplate();
			this.CreateFinishNavigationTemplate();
			this.CreateStepNavigationTemplate();
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x00107C4C File Offset: 0x00106C4C
		internal virtual void DataListItemDataBound(object sender, DataListItemEventArgs e)
		{
			DataListItem item = e.Item;
			if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem && item.ItemType != ListItemType.SelectedItem && item.ItemType != ListItemType.EditItem)
			{
				return;
			}
			IButtonControl buttonControl = item.FindControl(Wizard.SideBarButtonID) as IButtonControl;
			if (buttonControl != null)
			{
				if (buttonControl is Button)
				{
					((Button)buttonControl).UseSubmitBehavior = false;
				}
				WebControl webControl = buttonControl as WebControl;
				if (webControl != null)
				{
					webControl.TabIndex = this.TabIndex;
				}
				WizardStepBase wizardStepBase = item.DataItem as WizardStepBase;
				if (wizardStepBase != null)
				{
					if (this.GetStepType(wizardStepBase) == WizardStepType.Complete && webControl != null)
					{
						webControl.Enabled = false;
					}
					this.RegisterSideBarDataListForRender();
					if (wizardStepBase.Title.Length > 0)
					{
						buttonControl.Text = wizardStepBase.Title;
					}
					else
					{
						buttonControl.Text = wizardStepBase.ID;
					}
					int num = this.WizardSteps.IndexOf(wizardStepBase);
					buttonControl.CommandName = Wizard.MoveToCommandName;
					buttonControl.CommandArgument = num.ToString(NumberFormatInfo.InvariantInfo);
					this.RegisterCommandEvents(buttonControl);
				}
				return;
			}
			if (!base.DesignMode)
			{
				throw new InvalidOperationException(SR.GetString("Wizard_SideBar_Button_Not_Found", new object[]
				{
					Wizard.DataListID,
					Wizard.SideBarButtonID
				}));
			}
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x00107D83 File Offset: 0x00106D83
		internal void RegisterSideBarDataListForRender()
		{
			this._renderSideBarDataList = true;
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x00107D8C File Offset: 0x00106D8C
		internal virtual void DataListItemCommand(object sender, DataListCommandEventArgs e)
		{
			DataListItem item = e.Item;
			if (!Wizard.MoveToCommandName.Equals(e.CommandName, StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			int activeStepIndex = this.ActiveStepIndex;
			int num = int.Parse((string)e.CommandArgument, CultureInfo.InvariantCulture);
			WizardNavigationEventArgs wizardNavigationEventArgs = new WizardNavigationEventArgs(activeStepIndex, num);
			if (this._commandSender != null && !base.DesignMode && this.Page != null && !this.Page.IsValid)
			{
				wizardNavigationEventArgs.Cancel = true;
			}
			this._activeStepIndexSet = false;
			this.OnSideBarButtonClick(wizardNavigationEventArgs);
			if (!wizardNavigationEventArgs.Cancel)
			{
				if (!this._activeStepIndexSet && this.AllowNavigationToStep(num))
				{
					this.ActiveStepIndex = num;
					return;
				}
			}
			else
			{
				this.ActiveStepIndex = activeStepIndex;
			}
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x00107E3B File Offset: 0x00106E3B
		internal string GetCustomContainerID(int index)
		{
			return "__CustomNav" + index;
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x00107E50 File Offset: 0x00106E50
		internal bool ShouldRenderChildControl
		{
			get
			{
				if (!base.DesignMode)
				{
					return true;
				}
				if (this._designModeState == null)
				{
					return true;
				}
				object obj = this._designModeState["ShouldRenderWizardSteps"];
				return obj == null || (bool)obj;
			}
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x00107E90 File Offset: 0x00106E90
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			IDictionary designModeState = base.GetDesignModeState();
			this._designModeState = designModeState;
			int activeStepIndex = this.ActiveStepIndex;
			try
			{
				if (activeStepIndex == -1 && this.WizardSteps.Count > 0)
				{
					this.ActiveStepIndex = 0;
				}
				this.RequiresControlsRecreation();
				this.EnsureChildControls();
				this.ApplyControlProperties();
				designModeState["StepTableCell"] = this._stepTableCell;
				if (this._startNavigationTemplateContainer != null)
				{
					designModeState[Wizard.StartNextButtonID] = this._startNavigationTemplateContainer.NextButton;
					designModeState[Wizard.CancelButtonID] = this._startNavigationTemplateContainer.CancelButton;
				}
				if (this._stepNavigationTemplateContainer != null)
				{
					designModeState[Wizard.StepNextButtonID] = this._stepNavigationTemplateContainer.NextButton;
					designModeState[Wizard.StepPreviousButtonID] = this._stepNavigationTemplateContainer.PreviousButton;
					designModeState[Wizard.CancelButtonID] = this._stepNavigationTemplateContainer.CancelButton;
				}
				if (this._finishNavigationTemplateContainer != null)
				{
					designModeState[Wizard.FinishPreviousButtonID] = this._finishNavigationTemplateContainer.PreviousButton;
					designModeState[Wizard.FinishButtonID] = this._finishNavigationTemplateContainer.FinishButton;
					designModeState[Wizard.CancelButtonID] = this._finishNavigationTemplateContainer.CancelButton;
				}
				if (this.ShowCustomNavigationTemplate)
				{
					Wizard.BaseNavigationTemplateContainer baseNavigationTemplateContainer = (Wizard.BaseNavigationTemplateContainer)this.CustomNavigationContainers[this.ActiveStep];
					designModeState[Wizard.CustomNextButtonID] = baseNavigationTemplateContainer.NextButton;
					designModeState[Wizard.CustomPreviousButtonID] = baseNavigationTemplateContainer.PreviousButton;
					designModeState[Wizard.CustomFinishButtonID] = baseNavigationTemplateContainer.PreviousButton;
					designModeState[Wizard.CancelButtonID] = baseNavigationTemplateContainer.CancelButton;
					designModeState["CustomNavigationControls"] = baseNavigationTemplateContainer.Controls;
				}
				if (this.SideBarTemplate == null && this._sideBarDataList != null)
				{
					this._sideBarDataList.ItemTemplate = this.CreateDefaultDataListItemTemplate();
				}
				designModeState[Wizard.DataListID] = this._sideBarDataList;
				designModeState["TemplatedWizardSteps"] = this.TemplatedSteps;
			}
			finally
			{
				this.ActiveStepIndex = activeStepIndex;
			}
			return designModeState;
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x00108094 File Offset: 0x00107094
		public ICollection GetHistory()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.History)
			{
				int index = (int)obj;
				arrayList.Add(this.WizardSteps[index]);
			}
			return arrayList;
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x00108100 File Offset: 0x00107100
		internal int GetPreviousStepIndex(bool popStack)
		{
			int num = -1;
			int activeStepIndex = this.ActiveStepIndex;
			if (this._historyStack == null || this._historyStack.Count == 0)
			{
				return num;
			}
			if (popStack)
			{
				num = (int)this._historyStack.Pop();
				if (num == activeStepIndex && this._historyStack.Count > 0)
				{
					num = (int)this._historyStack.Pop();
				}
			}
			else
			{
				num = (int)this._historyStack.Peek();
				if (num == activeStepIndex && this._historyStack.Count > 1)
				{
					int num2 = (int)this._historyStack.Pop();
					num = (int)this._historyStack.Peek();
					this._historyStack.Push(num2);
				}
			}
			if (num == activeStepIndex)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x001081C4 File Offset: 0x001071C4
		internal WizardStepType GetStepType(int index)
		{
			WizardStepBase wizardStep = this.WizardSteps[index];
			return this.GetStepType(wizardStep, index);
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x001081E8 File Offset: 0x001071E8
		internal WizardStepType GetStepType(WizardStepBase step)
		{
			int index = this.WizardSteps.IndexOf(step);
			return this.GetStepType(step, index);
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x0010820C File Offset: 0x0010720C
		public WizardStepType GetStepType(WizardStepBase wizardStep, int index)
		{
			if (wizardStep.StepType != WizardStepType.Auto)
			{
				return wizardStep.StepType;
			}
			if (this.WizardSteps.Count == 1 || (index < this.WizardSteps.Count - 1 && this.WizardSteps[index + 1].StepType == WizardStepType.Complete))
			{
				return WizardStepType.Finish;
			}
			if (index == 0)
			{
				return WizardStepType.Start;
			}
			if (index == this.WizardSteps.Count - 1)
			{
				return WizardStepType.Finish;
			}
			return WizardStepType.Step;
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x00108278 File Offset: 0x00107278
		internal virtual void InstantiateStepContentTemplates()
		{
			foreach (object obj in this.TemplatedSteps)
			{
				TemplatedWizardStep templatedWizardStep = (TemplatedWizardStep)obj;
				TemplatedWizardStep step = templatedWizardStep;
				this.InstantiateStepContentTemplate(step);
			}
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x001082D4 File Offset: 0x001072D4
		internal void InstantiateStepContentTemplate(TemplatedWizardStep step)
		{
			step.Controls.Clear();
			Wizard.BaseContentTemplateContainer baseContentTemplateContainer = new Wizard.BaseContentTemplateContainer(this);
			ITemplate contentTemplate = step.ContentTemplate;
			if (contentTemplate != null)
			{
				baseContentTemplateContainer.SetEnableTheming();
				contentTemplate.InstantiateIn(baseContentTemplateContainer.InnerCell);
			}
			step.ContentTemplateContainer = baseContentTemplateContainer;
			step.Controls.Add(baseContentTemplateContainer);
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x00108324 File Offset: 0x00107324
		protected internal override void LoadControlState(object state)
		{
			Triplet triplet = state as Triplet;
			if (triplet != null)
			{
				base.LoadControlState(triplet.First);
				Array array = triplet.Second as Array;
				if (array != null)
				{
					Array.Reverse(array);
					this._historyStack = new Stack(array);
				}
				this.ActiveStepIndex = (int)triplet.Third;
			}
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x0010837C File Offset: 0x0010737C
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 15)
			{
				throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
			}
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.NavigationButtonStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.SideBarButtonStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.HeaderStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.NavigationStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.SideBarStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.StepStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.StartNextButtonStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.StepPreviousButtonStyle).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.StepNextButtonStyle).LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)this.FinishPreviousButtonStyle).LoadViewState(array[10]);
			}
			if (array[11] != null)
			{
				((IStateManager)this.FinishCompleteButtonStyle).LoadViewState(array[11]);
			}
			if (array[12] != null)
			{
				((IStateManager)this.CancelButtonStyle).LoadViewState(array[12]);
			}
			if (array[13] != null)
			{
				((IStateManager)base.ControlStyle).LoadViewState(array[13]);
			}
			if (array[14] != null)
			{
				this.DisplaySideBar = (bool)array[14];
			}
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x001084D4 File Offset: 0x001074D4
		public void MoveTo(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			int num = this.WizardSteps.IndexOf(wizardStep);
			if (num == -1)
			{
				throw new ArgumentException(SR.GetString("Wizard_Step_Not_In_Wizard"));
			}
			this.ActiveStepIndex = num;
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x00108518 File Offset: 0x00107518
		protected virtual void OnActiveStepChanged(object source, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Wizard._eventActiveStepChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x00108548 File Offset: 0x00107548
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			bool flag = false;
			if (e is CommandEventArgs)
			{
				CommandEventArgs commandEventArgs = (CommandEventArgs)e;
				if (string.Equals(Wizard.CancelCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
				{
					this.OnCancelButtonClick(EventArgs.Empty);
					return true;
				}
				int activeStepIndex = this.ActiveStepIndex;
				int nextStepIndex = activeStepIndex;
				bool flag2 = true;
				WizardStepType wizardStepType = WizardStepType.Auto;
				WizardStepBase wizardStepBase = this.WizardSteps[activeStepIndex];
				if (wizardStepBase is TemplatedWizardStep)
				{
					flag2 = false;
				}
				else
				{
					wizardStepType = this.GetStepType(wizardStepBase);
				}
				WizardNavigationEventArgs wizardNavigationEventArgs = new WizardNavigationEventArgs(activeStepIndex, nextStepIndex);
				if (this._commandSender != null && this.Page != null && !this.Page.IsValid)
				{
					wizardNavigationEventArgs.Cancel = true;
				}
				bool flag3 = false;
				this._activeStepIndexSet = false;
				if (string.Equals(Wizard.MoveNextCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
				{
					if (flag2 && wizardStepType != WizardStepType.Start && wizardStepType != WizardStepType.Step)
					{
						throw new InvalidOperationException(SR.GetString("Wizard_InvalidBubbleEvent", new object[]
						{
							Wizard.MoveNextCommandName
						}));
					}
					if (activeStepIndex < this.WizardSteps.Count - 1)
					{
						wizardNavigationEventArgs.SetNextStepIndex(activeStepIndex + 1);
					}
					this.OnNextButtonClick(wizardNavigationEventArgs);
					flag = true;
				}
				else if (string.Equals(Wizard.MovePreviousCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
				{
					if (flag2 && wizardStepType != WizardStepType.Step && wizardStepType != WizardStepType.Finish)
					{
						throw new InvalidOperationException(SR.GetString("Wizard_InvalidBubbleEvent", new object[]
						{
							Wizard.MovePreviousCommandName
						}));
					}
					flag3 = true;
					int previousStepIndex = this.GetPreviousStepIndex(false);
					if (previousStepIndex != -1)
					{
						wizardNavigationEventArgs.SetNextStepIndex(previousStepIndex);
					}
					this.OnPreviousButtonClick(wizardNavigationEventArgs);
					flag = true;
				}
				else if (string.Equals(Wizard.MoveCompleteCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
				{
					if (flag2 && wizardStepType != WizardStepType.Finish)
					{
						throw new InvalidOperationException(SR.GetString("Wizard_InvalidBubbleEvent", new object[]
						{
							Wizard.MoveCompleteCommandName
						}));
					}
					if (activeStepIndex < this.WizardSteps.Count - 1)
					{
						wizardNavigationEventArgs.SetNextStepIndex(activeStepIndex + 1);
					}
					this.OnFinishButtonClick(wizardNavigationEventArgs);
					flag = true;
				}
				else if (string.Equals(Wizard.MoveToCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
				{
					nextStepIndex = int.Parse((string)commandEventArgs.CommandArgument, CultureInfo.InvariantCulture);
					wizardNavigationEventArgs.SetNextStepIndex(nextStepIndex);
					flag = true;
				}
				if (flag)
				{
					if (!wizardNavigationEventArgs.Cancel)
					{
						if (!this._activeStepIndexSet && this.AllowNavigationToStep(wizardNavigationEventArgs.NextStepIndex))
						{
							if (flag3)
							{
								this.GetPreviousStepIndex(true);
							}
							this.ActiveStepIndex = wizardNavigationEventArgs.NextStepIndex;
						}
					}
					else
					{
						this.ActiveStepIndex = activeStepIndex;
					}
				}
			}
			return flag;
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x001087AE File Offset: 0x001077AE
		internal void OnWizardStepsChanged()
		{
			if (this._sideBarDataList != null)
			{
				this._sideBarDataList.DataSource = this.WizardSteps;
				this._sideBarDataList.SelectedIndex = this.ActiveStepIndex;
				this._sideBarDataList.DataBind();
			}
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x001087E5 File Offset: 0x001077E5
		protected virtual bool AllowNavigationToStep(int index)
		{
			return this._historyStack == null || !this._historyStack.Contains(index) || this.WizardSteps[index].AllowReturn;
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x00108818 File Offset: 0x00107818
		protected virtual void OnCancelButtonClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Wizard._eventCancelButtonClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			string cancelDestinationPageUrl = this.CancelDestinationPageUrl;
			if (!string.IsNullOrEmpty(cancelDestinationPageUrl))
			{
				this.Page.Response.Redirect(base.ResolveClientUrl(cancelDestinationPageUrl), false);
			}
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x0010886D File Offset: 0x0010786D
		private void OnCommand(object sender, CommandEventArgs e)
		{
			this._commandSender = (sender as IButtonControl);
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x0010887C File Offset: 0x0010787C
		protected virtual void OnFinishButtonClick(WizardNavigationEventArgs e)
		{
			WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard._eventFinishButtonClick];
			if (wizardNavigationEventHandler != null)
			{
				wizardNavigationEventHandler(this, e);
			}
			string finishDestinationPageUrl = this.FinishDestinationPageUrl;
			if (!string.IsNullOrEmpty(finishDestinationPageUrl))
			{
				this.Page.Response.Redirect(base.ResolveClientUrl(finishDestinationPageUrl), false);
			}
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x001088D4 File Offset: 0x001078D4
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.ActiveStepIndex == -1 && this.WizardSteps.Count > 0 && !base.DesignMode)
			{
				this.ActiveStepIndex = 0;
			}
			this.EnsureChildControls();
			if (this.Page != null)
			{
				this.Page.RegisterRequiresControlState(this);
			}
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x00108928 File Offset: 0x00107928
		protected virtual void OnNextButtonClick(WizardNavigationEventArgs e)
		{
			WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard._eventNextButtonClick];
			if (wizardNavigationEventHandler != null)
			{
				wizardNavigationEventHandler(this, e);
			}
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x00108958 File Offset: 0x00107958
		protected virtual void OnPreviousButtonClick(WizardNavigationEventArgs e)
		{
			WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard._eventPreviousButtonClick];
			if (wizardNavigationEventHandler != null)
			{
				wizardNavigationEventHandler(this, e);
			}
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x00108988 File Offset: 0x00107988
		protected virtual void OnSideBarButtonClick(WizardNavigationEventArgs e)
		{
			WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard._eventSideBarButtonClick];
			if (wizardNavigationEventHandler != null)
			{
				wizardNavigationEventHandler(this, e);
			}
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x001089B8 File Offset: 0x001079B8
		internal void RequiresControlsRecreation()
		{
			if (base.ChildControlsCreated)
			{
				using (new Wizard.WizardControlCollectionModifier(this))
				{
					base.ChildControlsCreated = false;
				}
			}
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x001089F8 File Offset: 0x001079F8
		protected internal void RegisterCommandEvents(IButtonControl button)
		{
			if (button != null && button.CausesValidation)
			{
				button.Command += this.OnCommand;
			}
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x00108A17 File Offset: 0x00107A17
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			this.EnsureChildControls();
			this.ApplyControlProperties();
			if (this.ActiveStepIndex == -1 || this.WizardSteps.Count == 0)
			{
				return;
			}
			this.RenderContents(writer);
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x00108A58 File Offset: 0x00107A58
		protected internal override object SaveControlState()
		{
			int activeStepIndex = this.ActiveStepIndex;
			if (this._historyStack == null || this._historyStack.Count == 0 || (int)this._historyStack.Peek() != activeStepIndex)
			{
				this.History.Push(this.ActiveStepIndex);
			}
			object obj = base.SaveControlState();
			bool flag = this._historyStack != null && this._historyStack.Count > 0;
			if (obj != null || flag || activeStepIndex != -1)
			{
				object y = flag ? this._historyStack.ToArray() : null;
				return new Triplet(obj, y, activeStepIndex);
			}
			return null;
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x00108AF8 File Offset: 0x00107AF8
		protected override object SaveViewState()
		{
			object[] array = new object[15];
			array[0] = base.SaveViewState();
			array[1] = ((this._navigationButtonStyle != null) ? ((IStateManager)this._navigationButtonStyle).SaveViewState() : null);
			array[2] = ((this._sideBarButtonStyle != null) ? ((IStateManager)this._sideBarButtonStyle).SaveViewState() : null);
			array[3] = ((this._headerStyle != null) ? ((IStateManager)this._headerStyle).SaveViewState() : null);
			array[4] = ((this._navigationStyle != null) ? ((IStateManager)this._navigationStyle).SaveViewState() : null);
			array[5] = ((this._sideBarStyle != null) ? ((IStateManager)this._sideBarStyle).SaveViewState() : null);
			array[6] = ((this._stepStyle != null) ? ((IStateManager)this._stepStyle).SaveViewState() : null);
			array[7] = ((this._startNextButtonStyle != null) ? ((IStateManager)this._startNextButtonStyle).SaveViewState() : null);
			array[8] = ((this._stepNextButtonStyle != null) ? ((IStateManager)this._stepNextButtonStyle).SaveViewState() : null);
			array[9] = ((this._stepPreviousButtonStyle != null) ? ((IStateManager)this._stepPreviousButtonStyle).SaveViewState() : null);
			array[10] = ((this._finishPreviousButtonStyle != null) ? ((IStateManager)this._finishPreviousButtonStyle).SaveViewState() : null);
			array[11] = ((this._finishCompleteButtonStyle != null) ? ((IStateManager)this._finishCompleteButtonStyle).SaveViewState() : null);
			array[12] = ((this._cancelButtonStyle != null) ? ((IStateManager)this._cancelButtonStyle).SaveViewState() : null);
			array[13] = (base.ControlStyleCreated ? ((IStateManager)base.ControlStyle).SaveViewState() : null);
			if (this.DisplaySideBar != this._displaySideBarDefault)
			{
				array[14] = this.DisplaySideBar;
			}
			for (int i = 0; i < 15; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x00108C94 File Offset: 0x00107C94
		private WizardStepType SetActiveTemplates()
		{
			WizardStepType stepType = this.GetStepType(this.ActiveStepIndex);
			if (stepType == WizardStepType.Complete)
			{
				if (this._headerTableRow != null)
				{
					this._headerTableRow.Visible = false;
				}
				if (this._sideBarTableCell != null)
				{
					this._sideBarTableCell.Visible = false;
				}
				this._navigationRow.Visible = false;
			}
			else if (this._sideBarTableCell != null)
			{
				this._sideBarTableCell.Visible = (this.SideBarEnabled && this._renderSideBarDataList);
			}
			this._startNavigationTemplateContainer.Visible = (stepType == WizardStepType.Start);
			this._stepNavigationTemplateContainer.Visible = (stepType == WizardStepType.Step);
			this._finishNavigationTemplateContainer.Visible = (stepType == WizardStepType.Finish);
			return stepType;
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x00108D3C File Offset: 0x00107D3C
		private void SetCancelButtonVisibility(Wizard.BaseNavigationTemplateContainer container)
		{
			Control control = container.CancelButton as Control;
			if (control != null)
			{
				Control parent = control.Parent;
				if (parent != null)
				{
					parent.Visible = this.DisplayCancelButton;
				}
				control.Visible = this.DisplayCancelButton;
			}
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x00108D7C File Offset: 0x00107D7C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._navigationButtonStyle != null)
			{
				((IStateManager)this._navigationButtonStyle).TrackViewState();
			}
			if (this._sideBarButtonStyle != null)
			{
				((IStateManager)this._sideBarButtonStyle).TrackViewState();
			}
			if (this._headerStyle != null)
			{
				((IStateManager)this._headerStyle).TrackViewState();
			}
			if (this._navigationStyle != null)
			{
				((IStateManager)this._navigationStyle).TrackViewState();
			}
			if (this._sideBarStyle != null)
			{
				((IStateManager)this._sideBarStyle).TrackViewState();
			}
			if (this._stepStyle != null)
			{
				((IStateManager)this._stepStyle).TrackViewState();
			}
			if (this._startNextButtonStyle != null)
			{
				((IStateManager)this._startNextButtonStyle).TrackViewState();
			}
			if (this._stepPreviousButtonStyle != null)
			{
				((IStateManager)this._stepPreviousButtonStyle).TrackViewState();
			}
			if (this._stepNextButtonStyle != null)
			{
				((IStateManager)this._stepNextButtonStyle).TrackViewState();
			}
			if (this._finishPreviousButtonStyle != null)
			{
				((IStateManager)this._finishPreviousButtonStyle).TrackViewState();
			}
			if (this._finishCompleteButtonStyle != null)
			{
				((IStateManager)this._finishCompleteButtonStyle).TrackViewState();
			}
			if (this._cancelButtonStyle != null)
			{
				((IStateManager)this._cancelButtonStyle).TrackViewState();
			}
			if (base.ControlStyleCreated)
			{
				((IStateManager)base.ControlStyle).TrackViewState();
			}
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x00108E86 File Offset: 0x00107E86
		private void ValidateButtonType(ButtonType value)
		{
			if (value < ButtonType.Button || value > ButtonType.Link)
			{
				throw new ArgumentOutOfRangeException("value");
			}
		}

		// Token: 0x040027AD RID: 10157
		private const string StepTableCellID = "StepTableCell";

		// Token: 0x040027AE RID: 10158
		private const string _multiViewID = "WizardMultiView";

		// Token: 0x040027AF RID: 10159
		private const string _stepNavigationTemplateName = "StepNavigationTemplate";

		// Token: 0x040027B0 RID: 10160
		private const string _finishNavigationTemplateName = "FinishNavigationTemplate";

		// Token: 0x040027B1 RID: 10161
		private const string _startNavigationTemplateName = "StartNavigationTemplate";

		// Token: 0x040027B2 RID: 10162
		private const string _sideBarTemplateName = "SideBarTemplate";

		// Token: 0x040027B3 RID: 10163
		internal const string _customNavigationControls = "CustomNavigationControls";

		// Token: 0x040027B4 RID: 10164
		internal const string _startNavigationTemplateContainerID = "StartNavigationTemplateContainerID";

		// Token: 0x040027B5 RID: 10165
		internal const string _stepNavigationTemplateContainerID = "StepNavigationTemplateContainerID";

		// Token: 0x040027B6 RID: 10166
		internal const string _finishNavigationTemplateContainerID = "FinishNavigationTemplateContainerID";

		// Token: 0x040027B7 RID: 10167
		internal const string _customNavigationContainerIdPrefix = "__CustomNav";

		// Token: 0x040027B8 RID: 10168
		internal const string _templatedStepsID = "TemplatedWizardSteps";

		// Token: 0x040027B9 RID: 10169
		private const string _wizardContentMark = "_SkipLink";

		// Token: 0x040027BA RID: 10170
		private const string _sideBarCellID = "SideBarContainer";

		// Token: 0x040027BB RID: 10171
		private const string _headerCellID = "HeaderContainer";

		// Token: 0x040027BC RID: 10172
		private const int _viewStateArrayLength = 15;

		// Token: 0x040027BD RID: 10173
		private ITemplate _finishNavigationTemplate;

		// Token: 0x040027BE RID: 10174
		private ITemplate _headerTemplate;

		// Token: 0x040027BF RID: 10175
		private ITemplate _startNavigationTemplate;

		// Token: 0x040027C0 RID: 10176
		private ITemplate _stepNavigationTemplate;

		// Token: 0x040027C1 RID: 10177
		private ITemplate _sideBarTemplate;

		// Token: 0x040027C2 RID: 10178
		private Wizard.NavigationTemplate _defaultStartNavigationTemplate;

		// Token: 0x040027C3 RID: 10179
		private Wizard.NavigationTemplate _defaultStepNavigationTemplate;

		// Token: 0x040027C4 RID: 10180
		private Wizard.NavigationTemplate _defaultFinishNavigationTemplate;

		// Token: 0x040027C5 RID: 10181
		private MultiView _multiView;

		// Token: 0x040027C6 RID: 10182
		private Wizard.FinishNavigationTemplateContainer _finishNavigationTemplateContainer;

		// Token: 0x040027C7 RID: 10183
		private Wizard.StartNavigationTemplateContainer _startNavigationTemplateContainer;

		// Token: 0x040027C8 RID: 10184
		private Wizard.StepNavigationTemplateContainer _stepNavigationTemplateContainer;

		// Token: 0x040027C9 RID: 10185
		private IDictionary _customNavigationContainers;

		// Token: 0x040027CA RID: 10186
		private ArrayList _templatedSteps;

		// Token: 0x040027CB RID: 10187
		private static readonly object _eventActiveStepChanged = new object();

		// Token: 0x040027CC RID: 10188
		private static readonly object _eventFinishButtonClick = new object();

		// Token: 0x040027CD RID: 10189
		private static readonly object _eventNextButtonClick = new object();

		// Token: 0x040027CE RID: 10190
		private static readonly object _eventPreviousButtonClick = new object();

		// Token: 0x040027CF RID: 10191
		private static readonly object _eventSideBarButtonClick = new object();

		// Token: 0x040027D0 RID: 10192
		private static readonly object _eventCancelButtonClick = new object();

		// Token: 0x040027D1 RID: 10193
		public static readonly string CancelCommandName = "Cancel";

		// Token: 0x040027D2 RID: 10194
		public static readonly string MoveNextCommandName = "MoveNext";

		// Token: 0x040027D3 RID: 10195
		public static readonly string MovePreviousCommandName = "MovePrevious";

		// Token: 0x040027D4 RID: 10196
		public static readonly string MoveToCommandName = "Move";

		// Token: 0x040027D5 RID: 10197
		public static readonly string MoveCompleteCommandName = "MoveComplete";

		// Token: 0x040027D6 RID: 10198
		protected static readonly string CancelButtonID = "CancelButton";

		// Token: 0x040027D7 RID: 10199
		protected static readonly string StartNextButtonID = "StartNextButton";

		// Token: 0x040027D8 RID: 10200
		protected static readonly string StepPreviousButtonID = "StepPreviousButton";

		// Token: 0x040027D9 RID: 10201
		protected static readonly string StepNextButtonID = "StepNextButton";

		// Token: 0x040027DA RID: 10202
		protected static readonly string FinishButtonID = "FinishButton";

		// Token: 0x040027DB RID: 10203
		protected static readonly string FinishPreviousButtonID = "FinishPreviousButton";

		// Token: 0x040027DC RID: 10204
		protected static readonly string CustomPreviousButtonID = "CustomPreviousButton";

		// Token: 0x040027DD RID: 10205
		protected static readonly string CustomNextButtonID = "CustomNextButton";

		// Token: 0x040027DE RID: 10206
		protected static readonly string CustomFinishButtonID = "CustomFinishButton";

		// Token: 0x040027DF RID: 10207
		protected static readonly string DataListID = "SideBarList";

		// Token: 0x040027E0 RID: 10208
		protected static readonly string SideBarButtonID = "SideBarButton";

		// Token: 0x040027E1 RID: 10209
		private TableRow _headerTableRow;

		// Token: 0x040027E2 RID: 10210
		private TableRow _navigationRow;

		// Token: 0x040027E3 RID: 10211
		private TableCell _sideBarTableCell;

		// Token: 0x040027E4 RID: 10212
		private TableCell _headerTableCell;

		// Token: 0x040027E5 RID: 10213
		private TableCell _stepTableCell;

		// Token: 0x040027E6 RID: 10214
		internal TableCell _navigationTableCell;

		// Token: 0x040027E7 RID: 10215
		private Table _renderTable;

		// Token: 0x040027E8 RID: 10216
		private Stack _historyStack;

		// Token: 0x040027E9 RID: 10217
		private DataList _sideBarDataList;

		// Token: 0x040027EA RID: 10218
		private bool _renderSideBarDataList;

		// Token: 0x040027EB RID: 10219
		private LiteralControl _titleLiteral;

		// Token: 0x040027EC RID: 10220
		private bool _activeStepIndexSet;

		// Token: 0x040027ED RID: 10221
		private WizardStepCollection _wizardStepCollection;

		// Token: 0x040027EE RID: 10222
		private IButtonControl _commandSender;

		// Token: 0x040027EF RID: 10223
		internal bool _displaySideBarDefault = true;

		// Token: 0x040027F0 RID: 10224
		internal bool _displaySideBar = true;

		// Token: 0x040027F1 RID: 10225
		private Style _cancelButtonStyle;

		// Token: 0x040027F2 RID: 10226
		private Style _navigationButtonStyle;

		// Token: 0x040027F3 RID: 10227
		private Style _sideBarButtonStyle;

		// Token: 0x040027F4 RID: 10228
		private Style _startNextButtonStyle;

		// Token: 0x040027F5 RID: 10229
		private Style _stepNextButtonStyle;

		// Token: 0x040027F6 RID: 10230
		private Style _stepPreviousButtonStyle;

		// Token: 0x040027F7 RID: 10231
		private Style _finishCompleteButtonStyle;

		// Token: 0x040027F8 RID: 10232
		private Style _finishPreviousButtonStyle;

		// Token: 0x040027F9 RID: 10233
		private TableItemStyle _headerStyle;

		// Token: 0x040027FA RID: 10234
		private TableItemStyle _navigationStyle;

		// Token: 0x040027FB RID: 10235
		private TableItemStyle _sideBarStyle;

		// Token: 0x040027FC RID: 10236
		private TableItemStyle _stepStyle;

		// Token: 0x040027FD RID: 10237
		private bool _isMacIESet;

		// Token: 0x040027FE RID: 10238
		private bool _isMacIE;

		// Token: 0x040027FF RID: 10239
		private IDictionary _designModeState;

		// Token: 0x0200050F RID: 1295
		internal class WizardControlCollection : ControlCollection
		{
			// Token: 0x06003FAA RID: 16298 RVA: 0x00108F85 File Offset: 0x00107F85
			public WizardControlCollection(Wizard wizard) : base(wizard)
			{
				if (!wizard.DesignMode)
				{
					base.SetCollectionReadOnly("Wizard_Cannot_Modify_ControlCollection");
				}
			}
		}

		// Token: 0x02000510 RID: 1296
		internal class WizardControlCollectionModifier : IDisposable
		{
			// Token: 0x06003FAB RID: 16299 RVA: 0x00108FA2 File Offset: 0x00107FA2
			public WizardControlCollectionModifier(Wizard wizard)
			{
				this._wizard = wizard;
				if (!this._wizard.DesignMode)
				{
					this._controls = this._wizard.Controls;
					this._originalError = this._controls.SetCollectionReadOnly(null);
				}
			}

			// Token: 0x06003FAC RID: 16300 RVA: 0x00108FE1 File Offset: 0x00107FE1
			void IDisposable.Dispose()
			{
				if (!this._wizard.DesignMode)
				{
					this._controls.SetCollectionReadOnly(this._originalError);
				}
			}

			// Token: 0x04002800 RID: 10240
			private Wizard _wizard;

			// Token: 0x04002801 RID: 10241
			private ControlCollection _controls;

			// Token: 0x04002802 RID: 10242
			private string _originalError;
		}

		// Token: 0x02000511 RID: 1297
		[SupportsEventValidation]
		private class WizardChildTable : ChildTable
		{
			// Token: 0x06003FAD RID: 16301 RVA: 0x00109002 File Offset: 0x00108002
			internal WizardChildTable(Wizard owner)
			{
				this._owner = owner;
			}

			// Token: 0x06003FAE RID: 16302 RVA: 0x00109011 File Offset: 0x00108011
			protected override bool OnBubbleEvent(object source, EventArgs args)
			{
				return this._owner.OnBubbleEvent(source, args);
			}

			// Token: 0x04002803 RID: 10243
			private Wizard _owner;
		}

		// Token: 0x02000512 RID: 1298
		private enum WizardTemplateType
		{
			// Token: 0x04002805 RID: 10245
			StartNavigationTemplate,
			// Token: 0x04002806 RID: 10246
			StepNavigationTemplate,
			// Token: 0x04002807 RID: 10247
			FinishNavigationTemplate
		}

		// Token: 0x02000513 RID: 1299
		private sealed class NavigationTemplate : ITemplate
		{
			// Token: 0x06003FAF RID: 16303 RVA: 0x00109020 File Offset: 0x00108020
			internal static Wizard.NavigationTemplate GetDefaultStartNavigationTemplate(Wizard wizard)
			{
				return new Wizard.NavigationTemplate(wizard, Wizard.WizardTemplateType.StartNavigationTemplate, true, null, "StartNext", "Cancel");
			}

			// Token: 0x06003FB0 RID: 16304 RVA: 0x00109035 File Offset: 0x00108035
			internal static Wizard.NavigationTemplate GetDefaultStepNavigationTemplate(Wizard wizard)
			{
				return new Wizard.NavigationTemplate(wizard, Wizard.WizardTemplateType.StepNavigationTemplate, false, "StepPrevious", "StepNext", "Cancel");
			}

			// Token: 0x06003FB1 RID: 16305 RVA: 0x0010904E File Offset: 0x0010804E
			internal static Wizard.NavigationTemplate GetDefaultFinishNavigationTemplate(Wizard wizard)
			{
				return new Wizard.NavigationTemplate(wizard, Wizard.WizardTemplateType.FinishNavigationTemplate, false, "FinishPrevious", "Finish", "Cancel");
			}

			// Token: 0x06003FB2 RID: 16306 RVA: 0x00109068 File Offset: 0x00108068
			internal void ResetButtonsVisibility()
			{
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						Control control = this._buttons[i][j] as Control;
						if (control != null)
						{
							control.Visible = false;
						}
					}
				}
			}

			// Token: 0x06003FB3 RID: 16307 RVA: 0x001090A8 File Offset: 0x001080A8
			private NavigationTemplate(Wizard wizard, Wizard.WizardTemplateType templateType, bool button1CausesValidation, string label1ID, string label2ID, string label3ID)
			{
				this._wizard = wizard;
				this._button1ID = label1ID;
				this._button2ID = label2ID;
				this._button3ID = label3ID;
				this._templateType = templateType;
				this._buttons = new IButtonControl[3][];
				this._buttons[0] = new IButtonControl[3];
				this._buttons[1] = new IButtonControl[3];
				this._buttons[2] = new IButtonControl[3];
				this._button1CausesValidation = button1CausesValidation;
			}

			// Token: 0x06003FB4 RID: 16308 RVA: 0x00109120 File Offset: 0x00108120
			void ITemplate.InstantiateIn(Control container)
			{
				Table table = new WizardDefaultInnerTable();
				table.CellSpacing = 5;
				table.CellPadding = 5;
				container.Controls.Add(table);
				this._row = new TableRow();
				table.Rows.Add(this._row);
				if (this._button1ID != null)
				{
					this.CreateButtonControl(this._buttons[0], this._button1ID, this._button1CausesValidation, Wizard.MovePreviousCommandName);
				}
				if (this._button2ID != null)
				{
					this.CreateButtonControl(this._buttons[1], this._button2ID, true, (this._templateType == Wizard.WizardTemplateType.FinishNavigationTemplate) ? Wizard.MoveCompleteCommandName : Wizard.MoveNextCommandName);
				}
				this.CreateButtonControl(this._buttons[2], this._button3ID, false, Wizard.CancelCommandName);
			}

			// Token: 0x06003FB5 RID: 16309 RVA: 0x001091DD File Offset: 0x001081DD
			private void OnPreRender(object source, EventArgs e)
			{
				((ImageButton)source).Visible = false;
			}

			// Token: 0x06003FB6 RID: 16310 RVA: 0x001091EC File Offset: 0x001081EC
			private void CreateButtonControl(IButtonControl[] buttons, string id, bool causesValidation, string commandName)
			{
				LinkButton linkButton = new LinkButton();
				linkButton.CausesValidation = causesValidation;
				linkButton.ID = id + "LinkButton";
				linkButton.Visible = false;
				linkButton.CommandName = commandName;
				linkButton.TabIndex = this._wizard.TabIndex;
				this._wizard.RegisterCommandEvents(linkButton);
				buttons[0] = linkButton;
				ImageButton imageButton = new ImageButton();
				imageButton.CausesValidation = causesValidation;
				imageButton.ID = id + "ImageButton";
				imageButton.Visible = true;
				imageButton.CommandName = commandName;
				imageButton.TabIndex = this._wizard.TabIndex;
				this._wizard.RegisterCommandEvents(imageButton);
				imageButton.PreRender += this.OnPreRender;
				buttons[1] = imageButton;
				Button button = new Button();
				button.CausesValidation = causesValidation;
				button.ID = id + "Button";
				button.Visible = false;
				button.CommandName = commandName;
				button.TabIndex = this._wizard.TabIndex;
				this._wizard.RegisterCommandEvents(button);
				buttons[2] = button;
				TableCell tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				this._row.Cells.Add(tableCell);
				tableCell.Controls.Add(linkButton);
				tableCell.Controls.Add(imageButton);
				tableCell.Controls.Add(button);
			}

			// Token: 0x17000F22 RID: 3874
			// (get) Token: 0x06003FB7 RID: 16311 RVA: 0x00109338 File Offset: 0x00108338
			internal IButtonControl FirstButton
			{
				get
				{
					ButtonType type = ButtonType.Button;
					switch (this._templateType)
					{
					case Wizard.WizardTemplateType.StartNavigationTemplate:
						goto IL_37;
					case Wizard.WizardTemplateType.StepNavigationTemplate:
						type = this._wizard.StepPreviousButtonType;
						goto IL_37;
					}
					type = this._wizard.FinishPreviousButtonType;
					IL_37:
					return this.GetButtonBasedOnType(0, type);
				}
			}

			// Token: 0x17000F23 RID: 3875
			// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x00109384 File Offset: 0x00108384
			internal IButtonControl SecondButton
			{
				get
				{
					ButtonType type;
					switch (this._templateType)
					{
					case Wizard.WizardTemplateType.StartNavigationTemplate:
						type = this._wizard.StartNextButtonType;
						goto IL_45;
					case Wizard.WizardTemplateType.StepNavigationTemplate:
						type = this._wizard.StepNextButtonType;
						goto IL_45;
					}
					type = this._wizard.FinishCompleteButtonType;
					IL_45:
					return this.GetButtonBasedOnType(1, type);
				}
			}

			// Token: 0x17000F24 RID: 3876
			// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x001093E0 File Offset: 0x001083E0
			internal IButtonControl CancelButton
			{
				get
				{
					ButtonType cancelButtonType = this._wizard.CancelButtonType;
					return this.GetButtonBasedOnType(2, cancelButtonType);
				}
			}

			// Token: 0x06003FBA RID: 16314 RVA: 0x00109404 File Offset: 0x00108404
			private IButtonControl GetButtonBasedOnType(int pos, ButtonType type)
			{
				switch (type)
				{
				case ButtonType.Button:
					return this._buttons[pos][2];
				case ButtonType.Image:
					return this._buttons[pos][1];
				case ButtonType.Link:
					return this._buttons[pos][0];
				default:
					return null;
				}
			}

			// Token: 0x04002808 RID: 10248
			private const string _startNextButtonID = "StartNext";

			// Token: 0x04002809 RID: 10249
			private const string _stepNextButtonID = "StepNext";

			// Token: 0x0400280A RID: 10250
			private const string _stepPreviousButtonID = "StepPrevious";

			// Token: 0x0400280B RID: 10251
			private const string _finishPreviousButtonID = "FinishPrevious";

			// Token: 0x0400280C RID: 10252
			private const string _finishButtonID = "Finish";

			// Token: 0x0400280D RID: 10253
			private const string _cancelButtonID = "Cancel";

			// Token: 0x0400280E RID: 10254
			private Wizard _wizard;

			// Token: 0x0400280F RID: 10255
			private Wizard.WizardTemplateType _templateType;

			// Token: 0x04002810 RID: 10256
			private string _button1ID;

			// Token: 0x04002811 RID: 10257
			private string _button2ID;

			// Token: 0x04002812 RID: 10258
			private string _button3ID;

			// Token: 0x04002813 RID: 10259
			private TableRow _row;

			// Token: 0x04002814 RID: 10260
			private IButtonControl[][] _buttons;

			// Token: 0x04002815 RID: 10261
			private bool _button1CausesValidation;
		}

		// Token: 0x02000514 RID: 1300
		private class DataListItemTemplate : ITemplate
		{
			// Token: 0x06003FBB RID: 16315 RVA: 0x00109449 File Offset: 0x00108449
			internal DataListItemTemplate(Wizard owner)
			{
				this._owner = owner;
			}

			// Token: 0x06003FBC RID: 16316 RVA: 0x00109458 File Offset: 0x00108458
			public void InstantiateIn(Control container)
			{
				LinkButton linkButton = new LinkButton();
				container.Controls.Add(linkButton);
				linkButton.ID = Wizard.SideBarButtonID;
				if (this._owner.DesignMode)
				{
					linkButton.MergeStyle(this._owner.SideBarButtonStyle);
				}
			}

			// Token: 0x04002816 RID: 10262
			private Wizard _owner;
		}

		// Token: 0x02000515 RID: 1301
		private class DefaultSideBarTemplate : ITemplate
		{
			// Token: 0x06003FBD RID: 16317 RVA: 0x001094A0 File Offset: 0x001084A0
			internal DefaultSideBarTemplate(Wizard owner)
			{
				this._owner = owner;
			}

			// Token: 0x06003FBE RID: 16318 RVA: 0x001094B0 File Offset: 0x001084B0
			public void InstantiateIn(Control container)
			{
				DataList dataList;
				if (this._owner.SideBarDataList == null)
				{
					dataList = new DataList();
					dataList.ID = Wizard.DataListID;
					dataList.SelectedItemStyle.Font.Bold = true;
					dataList.ItemTemplate = this._owner.CreateDefaultDataListItemTemplate();
				}
				else
				{
					dataList = this._owner.SideBarDataList;
				}
				container.Controls.Add(dataList);
			}

			// Token: 0x04002817 RID: 10263
			private Wizard _owner;
		}

		// Token: 0x02000516 RID: 1302
		internal abstract class BlockControl : WebControl, INonBindingContainer, INamingContainer
		{
			// Token: 0x06003FBF RID: 16319 RVA: 0x0010951C File Offset: 0x0010851C
			internal BlockControl(Wizard owner)
			{
				this._owner = owner;
				this._table = new WizardDefaultInnerTable();
				this._table.EnableTheming = false;
				this.Controls.Add(this._table);
				TableRow tableRow = new TableRow();
				this._table.Controls.Add(tableRow);
				this._cell = new TableCell();
				this._cell.Height = Unit.Percentage(100.0);
				this._cell.Width = Unit.Percentage(100.0);
				tableRow.Controls.Add(this._cell);
				this.HandleMacIECellHeight();
				base.PreventAutoID();
			}

			// Token: 0x17000F25 RID: 3877
			// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x001095CF File Offset: 0x001085CF
			protected Table Table
			{
				get
				{
					return this._table;
				}
			}

			// Token: 0x17000F26 RID: 3878
			// (get) Token: 0x06003FC1 RID: 16321 RVA: 0x001095D7 File Offset: 0x001085D7
			internal TableCell InnerCell
			{
				get
				{
					return this._cell;
				}
			}

			// Token: 0x06003FC2 RID: 16322 RVA: 0x001095DF File Offset: 0x001085DF
			protected override Style CreateControlStyle()
			{
				return new TableItemStyle(this.ViewState);
			}

			// Token: 0x06003FC3 RID: 16323 RVA: 0x001095EC File Offset: 0x001085EC
			public override void Focus()
			{
				throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
				{
					base.GetType().Name
				}));
			}

			// Token: 0x06003FC4 RID: 16324 RVA: 0x0010961E File Offset: 0x0010861E
			internal void HandleMacIECellHeight()
			{
				if (!this._owner.DesignMode && this._owner.IsMacIE5)
				{
					this._cell.Height = Unit.Pixel(1);
				}
			}

			// Token: 0x06003FC5 RID: 16325 RVA: 0x0010964B File Offset: 0x0010864B
			protected internal override void Render(HtmlTextWriter writer)
			{
				this.RenderContents(writer);
			}

			// Token: 0x06003FC6 RID: 16326 RVA: 0x00109654 File Offset: 0x00108654
			internal void SetEnableTheming()
			{
				this._cell.EnableTheming = this._owner.EnableTheming;
			}

			// Token: 0x04002818 RID: 10264
			private Table _table;

			// Token: 0x04002819 RID: 10265
			internal TableCell _cell;

			// Token: 0x0400281A RID: 10266
			internal Wizard _owner;
		}

		// Token: 0x02000518 RID: 1304
		private class InternalTableCell : TableCell, INonBindingContainer, INamingContainer
		{
			// Token: 0x06003FDB RID: 16347 RVA: 0x00109A34 File Offset: 0x00108A34
			internal InternalTableCell(Wizard owner)
			{
				this._owner = owner;
			}

			// Token: 0x06003FDC RID: 16348 RVA: 0x00109A43 File Offset: 0x00108A43
			protected override void AddAttributesToRender(HtmlTextWriter writer)
			{
				if (base.ControlStyleCreated && !base.ControlStyle.IsEmpty)
				{
					base.ControlStyle.AddAttributesToRender(writer, this);
				}
			}

			// Token: 0x0400281C RID: 10268
			protected Wizard _owner;
		}

		// Token: 0x02000519 RID: 1305
		private class AccessibleTableCell : Wizard.InternalTableCell
		{
			// Token: 0x06003FDD RID: 16349 RVA: 0x00109A67 File Offset: 0x00108A67
			internal AccessibleTableCell(Wizard owner) : base(owner)
			{
			}

			// Token: 0x06003FDE RID: 16350 RVA: 0x00109A70 File Offset: 0x00108A70
			protected internal override void RenderChildren(HtmlTextWriter writer)
			{
				bool flag = !string.IsNullOrEmpty(this._owner.SkipLinkText) && !this._owner.DesignMode;
				string text = this._owner.ClientID + "_SkipLink";
				if (flag)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + text);
					writer.RenderBeginTag(HtmlTextWriterTag.A);
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, this._owner.SkipLinkText);
					writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
					writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
					writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
					writer.AddAttribute(HtmlTextWriterAttribute.Src, base.SpacerImageUrl);
					writer.RenderBeginTag(HtmlTextWriterTag.Img);
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
				base.RenderChildren(writer);
				if (flag)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Id, text);
					writer.RenderBeginTag(HtmlTextWriterTag.A);
					writer.RenderEndTag();
				}
			}
		}

		// Token: 0x0200051A RID: 1306
		internal class BaseContentTemplateContainer : Wizard.BlockControl
		{
			// Token: 0x06003FDF RID: 16351 RVA: 0x00109B4E File Offset: 0x00108B4E
			internal BaseContentTemplateContainer(Wizard owner) : base(owner)
			{
				base.Table.Width = Unit.Percentage(100.0);
				base.Table.Height = Unit.Percentage(100.0);
			}
		}

		// Token: 0x0200051B RID: 1307
		internal class BaseNavigationTemplateContainer : WebControl, INonBindingContainer, INamingContainer
		{
			// Token: 0x06003FE0 RID: 16352 RVA: 0x00109B89 File Offset: 0x00108B89
			internal BaseNavigationTemplateContainer(Wizard owner)
			{
				this._owner = owner;
			}

			// Token: 0x17000F2E RID: 3886
			// (get) Token: 0x06003FE1 RID: 16353 RVA: 0x00109B98 File Offset: 0x00108B98
			internal Wizard Owner
			{
				get
				{
					return this._owner;
				}
			}

			// Token: 0x06003FE2 RID: 16354 RVA: 0x00109BA0 File Offset: 0x00108BA0
			internal virtual void ApplyButtonStyle(Style finishStyle, Style prevStyle, Style nextStyle, Style cancelStyle)
			{
				if (this.FinishButton != null)
				{
					this.ApplyButtonStyleInternal(this.FinishButton, finishStyle);
				}
				if (this.PreviousButton != null)
				{
					this.ApplyButtonStyleInternal(this.PreviousButton, prevStyle);
				}
				if (this.NextButton != null)
				{
					this.ApplyButtonStyleInternal(this.NextButton, nextStyle);
				}
				if (this.CancelButton != null)
				{
					this.ApplyButtonStyleInternal(this.CancelButton, cancelStyle);
				}
			}

			// Token: 0x06003FE3 RID: 16355 RVA: 0x00109C04 File Offset: 0x00108C04
			protected void ApplyButtonStyleInternal(IButtonControl control, Style buttonStyle)
			{
				WebControl webControl = control as WebControl;
				if (webControl != null)
				{
					webControl.ApplyStyle(buttonStyle);
					webControl.ControlStyle.MergeWith(this.Owner.NavigationButtonStyle);
				}
			}

			// Token: 0x06003FE4 RID: 16356 RVA: 0x00109C38 File Offset: 0x00108C38
			public override void Focus()
			{
				throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
				{
					base.GetType().Name
				}));
			}

			// Token: 0x06003FE5 RID: 16357 RVA: 0x00109C6C File Offset: 0x00108C6C
			internal virtual void RegisterButtonCommandEvents()
			{
				this.Owner.RegisterCommandEvents(this.NextButton);
				this.Owner.RegisterCommandEvents(this.FinishButton);
				this.Owner.RegisterCommandEvents(this.PreviousButton);
				this.Owner.RegisterCommandEvents(this.CancelButton);
			}

			// Token: 0x17000F2F RID: 3887
			// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x00109CBD File Offset: 0x00108CBD
			// (set) Token: 0x06003FE7 RID: 16359 RVA: 0x00109CEA File Offset: 0x00108CEA
			internal virtual IButtonControl CancelButton
			{
				get
				{
					if (this._cancelButton != null)
					{
						return this._cancelButton;
					}
					this._cancelButton = (this.FindControl(Wizard.CancelButtonID) as IButtonControl);
					return this._cancelButton;
				}
				set
				{
					this._cancelButton = value;
				}
			}

			// Token: 0x17000F30 RID: 3888
			// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x00109CF3 File Offset: 0x00108CF3
			// (set) Token: 0x06003FE9 RID: 16361 RVA: 0x00109D20 File Offset: 0x00108D20
			internal virtual IButtonControl NextButton
			{
				get
				{
					if (this._nextButton != null)
					{
						return this._nextButton;
					}
					this._nextButton = (this.FindControl(Wizard.StepNextButtonID) as IButtonControl);
					return this._nextButton;
				}
				set
				{
					this._nextButton = value;
				}
			}

			// Token: 0x17000F31 RID: 3889
			// (get) Token: 0x06003FEA RID: 16362 RVA: 0x00109D29 File Offset: 0x00108D29
			// (set) Token: 0x06003FEB RID: 16363 RVA: 0x00109D56 File Offset: 0x00108D56
			internal virtual IButtonControl PreviousButton
			{
				get
				{
					if (this._previousButton != null)
					{
						return this._previousButton;
					}
					this._previousButton = (this.FindControl(Wizard.StepPreviousButtonID) as IButtonControl);
					return this._previousButton;
				}
				set
				{
					this._previousButton = value;
				}
			}

			// Token: 0x17000F32 RID: 3890
			// (get) Token: 0x06003FEC RID: 16364 RVA: 0x00109D5F File Offset: 0x00108D5F
			// (set) Token: 0x06003FED RID: 16365 RVA: 0x00109D8C File Offset: 0x00108D8C
			internal virtual IButtonControl FinishButton
			{
				get
				{
					if (this._finishButton != null)
					{
						return this._finishButton;
					}
					this._finishButton = (this.FindControl(Wizard.FinishButtonID) as IButtonControl);
					return this._finishButton;
				}
				set
				{
					this._finishButton = value;
				}
			}

			// Token: 0x06003FEE RID: 16366 RVA: 0x00109D95 File Offset: 0x00108D95
			internal void SetEnableTheming()
			{
				this.EnableTheming = this._owner.EnableTheming;
			}

			// Token: 0x06003FEF RID: 16367 RVA: 0x00109DA8 File Offset: 0x00108DA8
			protected internal override void Render(HtmlTextWriter writer)
			{
				this.RenderContents(writer);
			}

			// Token: 0x0400281D RID: 10269
			private IButtonControl _finishButton;

			// Token: 0x0400281E RID: 10270
			private IButtonControl _previousButton;

			// Token: 0x0400281F RID: 10271
			private IButtonControl _nextButton;

			// Token: 0x04002820 RID: 10272
			private IButtonControl _cancelButton;

			// Token: 0x04002821 RID: 10273
			private Wizard _owner;
		}

		// Token: 0x0200051C RID: 1308
		internal class FinishNavigationTemplateContainer : Wizard.BaseNavigationTemplateContainer
		{
			// Token: 0x06003FF0 RID: 16368 RVA: 0x00109DB1 File Offset: 0x00108DB1
			internal FinishNavigationTemplateContainer(Wizard owner) : base(owner)
			{
			}

			// Token: 0x17000F33 RID: 3891
			// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x00109DBA File Offset: 0x00108DBA
			// (set) Token: 0x06003FF2 RID: 16370 RVA: 0x00109DE7 File Offset: 0x00108DE7
			internal override IButtonControl PreviousButton
			{
				get
				{
					if (this._previousButton != null)
					{
						return this._previousButton;
					}
					this._previousButton = (this.FindControl(Wizard.FinishPreviousButtonID) as IButtonControl);
					return this._previousButton;
				}
				set
				{
					this._previousButton = value;
				}
			}

			// Token: 0x04002822 RID: 10274
			private IButtonControl _previousButton;
		}

		// Token: 0x0200051D RID: 1309
		internal class StartNavigationTemplateContainer : Wizard.BaseNavigationTemplateContainer
		{
			// Token: 0x06003FF3 RID: 16371 RVA: 0x00109DF0 File Offset: 0x00108DF0
			internal StartNavigationTemplateContainer(Wizard owner) : base(owner)
			{
			}

			// Token: 0x17000F34 RID: 3892
			// (get) Token: 0x06003FF4 RID: 16372 RVA: 0x00109DF9 File Offset: 0x00108DF9
			// (set) Token: 0x06003FF5 RID: 16373 RVA: 0x00109E26 File Offset: 0x00108E26
			internal override IButtonControl NextButton
			{
				get
				{
					if (this._nextButton != null)
					{
						return this._nextButton;
					}
					this._nextButton = (this.FindControl(Wizard.StartNextButtonID) as IButtonControl);
					return this._nextButton;
				}
				set
				{
					this._nextButton = value;
				}
			}

			// Token: 0x04002823 RID: 10275
			private IButtonControl _nextButton;
		}

		// Token: 0x0200051E RID: 1310
		internal class StepNavigationTemplateContainer : Wizard.BaseNavigationTemplateContainer
		{
			// Token: 0x06003FF6 RID: 16374 RVA: 0x00109E2F File Offset: 0x00108E2F
			internal StepNavigationTemplateContainer(Wizard owner) : base(owner)
			{
			}
		}
	}
}
