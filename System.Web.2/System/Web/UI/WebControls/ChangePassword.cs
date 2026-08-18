using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000390 RID: 912
	[Bindable(false)]
	[DefaultEvent("ChangedPassword")]
	[Designer("System.Web.UI.Design.WebControls.ChangePasswordDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ChangePassword : CompositeControl, IBorderPaddingControl, INamingContainer, IRenderOuterTableControl
	{
		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x0008AEC4 File Offset: 0x000890C4
		// (set) Token: 0x06002AA8 RID: 10920 RVA: 0x0008AEED File Offset: 0x000890ED
		[WebCategory("Appearance")]
		[DefaultValue(1)]
		[WebSysDescription("Login_BorderPadding")]
		public virtual int BorderPadding
		{
			get
			{
				object obj = this.ViewState["BorderPadding"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 1;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("ChangePassword_InvalidBorderPadding"));
				}
				this.ViewState["BorderPadding"] = value;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x0008AF20 File Offset: 0x00089120
		// (set) Token: 0x06002AAA RID: 10922 RVA: 0x0008AF4D File Offset: 0x0008914D
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_CancelButtonImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06002AAB RID: 10923 RVA: 0x0008AF60 File Offset: 0x00089160
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("ChangePassword_CancelButtonStyle")]
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

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x0008AF90 File Offset: 0x00089190
		// (set) Token: 0x06002AAD RID: 10925 RVA: 0x0008AFC2 File Offset: 0x000891C2
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ChangePassword_DefaultCancelButtonText")]
		[WebSysDescription("ChangePassword_CancelButtonText")]
		public virtual string CancelButtonText
		{
			get
			{
				object obj = this.ViewState["CancelButtonText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultCancelButtonText");
			}
			set
			{
				this.ViewState["CancelButtonText"] = value;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x0008AFD8 File Offset: 0x000891D8
		// (set) Token: 0x06002AAF RID: 10927 RVA: 0x0008B001 File Offset: 0x00089201
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("ChangePassword_CancelButtonType")]
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
				if (value < ButtonType.Button || value > ButtonType.Link)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["CancelButtonType"] = value;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x0008B02C File Offset: 0x0008922C
		// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x0008B059 File Offset: 0x00089259
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_CancelDestinationPageUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
		[UrlProperty]
		public virtual string CancelDestinationPageUrl
		{
			get
			{
				object obj = this.ViewState["CancelDestinationPageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CancelDestinationPageUrl"] = value;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x0008B06C File Offset: 0x0008926C
		// (set) Token: 0x06002AB3 RID: 10931 RVA: 0x0008B099 File Offset: 0x00089299
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_ChangePasswordButtonImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string ChangePasswordButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["ChangePasswordButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ChangePasswordButtonImageUrl"] = value;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x0008B0AC File Offset: 0x000892AC
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("ChangePassword_ChangePasswordButtonStyle")]
		public Style ChangePasswordButtonStyle
		{
			get
			{
				if (this._changePasswordButtonStyle == null)
				{
					this._changePasswordButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._changePasswordButtonStyle).TrackViewState();
					}
				}
				return this._changePasswordButtonStyle;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x0008B0DC File Offset: 0x000892DC
		// (set) Token: 0x06002AB6 RID: 10934 RVA: 0x0008B10E File Offset: 0x0008930E
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ChangePassword_DefaultChangePasswordButtonText")]
		[WebSysDescription("ChangePassword_ChangePasswordButtonText")]
		public virtual string ChangePasswordButtonText
		{
			get
			{
				object obj = this.ViewState["ChangePasswordButtonText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultChangePasswordButtonText");
			}
			set
			{
				this.ViewState["ChangePasswordButtonText"] = value;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x0008B124 File Offset: 0x00089324
		// (set) Token: 0x06002AB8 RID: 10936 RVA: 0x0008B14D File Offset: 0x0008934D
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("ChangePassword_ChangePasswordButtonType")]
		public virtual ButtonType ChangePasswordButtonType
		{
			get
			{
				object obj = this.ViewState["ChangePasswordButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				if (value < ButtonType.Button || value > ButtonType.Link)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ChangePasswordButtonType"] = value;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x0008B178 File Offset: 0x00089378
		// (set) Token: 0x06002ABA RID: 10938 RVA: 0x0008B1AA File Offset: 0x000893AA
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ChangePassword_DefaultChangePasswordFailureText")]
		[WebSysDescription("ChangePassword_ChangePasswordFailureText")]
		public virtual string ChangePasswordFailureText
		{
			get
			{
				object obj = this.ViewState["ChangePasswordFailureText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultChangePasswordFailureText");
			}
			set
			{
				this.ViewState["ChangePasswordFailureText"] = value;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x0008B1BD File Offset: 0x000893BD
		// (set) Token: 0x06002ABC RID: 10940 RVA: 0x0008B1C5 File Offset: 0x000893C5
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ChangePassword))]
		public virtual ITemplate ChangePasswordTemplate
		{
			get
			{
				return this._changePasswordTemplate;
			}
			set
			{
				this._changePasswordTemplate = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x0008B1D5 File Offset: 0x000893D5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control ChangePasswordTemplateContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._changePasswordContainer;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x0008B1E4 File Offset: 0x000893E4
		// (set) Token: 0x06002ABF RID: 10943 RVA: 0x0008B216 File Offset: 0x00089416
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ChangePassword_DefaultChangePasswordTitleText")]
		[WebSysDescription("LoginControls_TitleText")]
		public virtual string ChangePasswordTitleText
		{
			get
			{
				object obj = this.ViewState["ChangePasswordTitleText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultChangePasswordTitleText");
			}
			set
			{
				this.ViewState["ChangePasswordTitleText"] = value;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x0008B229 File Offset: 0x00089429
		[Browsable(false)]
		[Themeable(false)]
		[Filterable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string ConfirmNewPassword
		{
			get
			{
				if (this._confirmNewPassword != null)
				{
					return this._confirmNewPassword;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x0008B240 File Offset: 0x00089440
		// (set) Token: 0x06002AC2 RID: 10946 RVA: 0x0008B272 File Offset: 0x00089472
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ChangePassword_DefaultConfirmNewPasswordLabelText")]
		[WebSysDescription("ChangePassword_ConfirmNewPasswordLabelText")]
		public virtual string ConfirmNewPasswordLabelText
		{
			get
			{
				object obj = this.ViewState["ConfirmNewPasswordLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultConfirmNewPasswordLabelText");
			}
			set
			{
				this.ViewState["ConfirmNewPasswordLabelText"] = value;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x0008B288 File Offset: 0x00089488
		// (set) Token: 0x06002AC4 RID: 10948 RVA: 0x0008B2BA File Offset: 0x000894BA
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("ChangePassword_DefaultConfirmPasswordCompareErrorMessage")]
		[WebSysDescription("ChangePassword_ConfirmPasswordCompareErrorMessage")]
		public virtual string ConfirmPasswordCompareErrorMessage
		{
			get
			{
				object obj = this.ViewState["ConfirmPasswordCompareErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultConfirmPasswordCompareErrorMessage");
			}
			set
			{
				this.ViewState["ConfirmPasswordCompareErrorMessage"] = value;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x0008B2D0 File Offset: 0x000894D0
		// (set) Token: 0x06002AC6 RID: 10950 RVA: 0x0008B302 File Offset: 0x00089502
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("ChangePassword_DefaultConfirmPasswordRequiredErrorMessage")]
		[WebSysDescription("LoginControls_ConfirmPasswordRequiredErrorMessage")]
		public virtual string ConfirmPasswordRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["ConfirmPasswordRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultConfirmPasswordRequiredErrorMessage");
			}
			set
			{
				this.ViewState["ConfirmPasswordRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x0008B318 File Offset: 0x00089518
		// (set) Token: 0x06002AC8 RID: 10952 RVA: 0x0008B345 File Offset: 0x00089545
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_ContinueButtonImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string ContinueButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["ContinueButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ContinueButtonImageUrl"] = value;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x0008B358 File Offset: 0x00089558
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("ChangePassword_ContinueButtonStyle")]
		public Style ContinueButtonStyle
		{
			get
			{
				if (this._continueButtonStyle == null)
				{
					this._continueButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._continueButtonStyle).TrackViewState();
					}
				}
				return this._continueButtonStyle;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x0008B388 File Offset: 0x00089588
		// (set) Token: 0x06002ACB RID: 10955 RVA: 0x0008B3BA File Offset: 0x000895BA
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ChangePassword_DefaultContinueButtonText")]
		[WebSysDescription("ChangePassword_ContinueButtonText")]
		public virtual string ContinueButtonText
		{
			get
			{
				object obj = this.ViewState["ContinueButtonText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultContinueButtonText");
			}
			set
			{
				this.ViewState["ContinueButtonText"] = value;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x0008B3D0 File Offset: 0x000895D0
		// (set) Token: 0x06002ACD RID: 10957 RVA: 0x0008B3F9 File Offset: 0x000895F9
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("ChangePassword_ContinueButtonType")]
		public virtual ButtonType ContinueButtonType
		{
			get
			{
				object obj = this.ViewState["ContinueButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				if (value < ButtonType.Button || value > ButtonType.Link)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ContinueButtonType"] = value;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x0008B424 File Offset: 0x00089624
		// (set) Token: 0x06002ACF RID: 10959 RVA: 0x0008B451 File Offset: 0x00089651
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("LoginControls_ContinueDestinationPageUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
		[UrlProperty]
		public virtual string ContinueDestinationPageUrl
		{
			get
			{
				object obj = this.ViewState["ContinueDestinationPageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ContinueDestinationPageUrl"] = value;
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x0008B464 File Offset: 0x00089664
		private bool ConvertingToTemplate
		{
			get
			{
				return base.DesignMode && this._convertingToTemplate;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x0008B478 File Offset: 0x00089678
		// (set) Token: 0x06002AD2 RID: 10962 RVA: 0x0008B4A5 File Offset: 0x000896A5
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_CreateUserIconUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string CreateUserIconUrl
		{
			get
			{
				object obj = this.ViewState["CreateUserIconUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CreateUserIconUrl"] = value;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x0008B4B8 File Offset: 0x000896B8
		// (set) Token: 0x06002AD4 RID: 10964 RVA: 0x0008B4E5 File Offset: 0x000896E5
		[Localizable(true)]
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_CreateUserText")]
		public virtual string CreateUserText
		{
			get
			{
				object obj = this.ViewState["CreateUserText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CreateUserText"] = value;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x0008B4F8 File Offset: 0x000896F8
		// (set) Token: 0x06002AD6 RID: 10966 RVA: 0x0008B525 File Offset: 0x00089725
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_CreateUserUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string CreateUserUrl
		{
			get
			{
				object obj = this.ViewState["CreateUserUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CreateUserUrl"] = value;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x0008B538 File Offset: 0x00089738
		[Browsable(false)]
		[Themeable(false)]
		[Filterable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string CurrentPassword
		{
			get
			{
				if (this._password != null)
				{
					return this._password;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06002AD8 RID: 10968 RVA: 0x0008B550 File Offset: 0x00089750
		private string CurrentPasswordInternal
		{
			get
			{
				string currentPassword = this.CurrentPassword;
				if (string.IsNullOrEmpty(currentPassword) && this._changePasswordContainer != null)
				{
					ITextControl textControl = (ITextControl)this._changePasswordContainer.CurrentPasswordTextBox;
					if (textControl != null)
					{
						return textControl.Text;
					}
				}
				return currentPassword;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x0008B590 File Offset: 0x00089790
		// (set) Token: 0x06002ADA RID: 10970 RVA: 0x0008B598 File Offset: 0x00089798
		internal ChangePassword.View CurrentView
		{
			get
			{
				return this._currentView;
			}
			set
			{
				if (value < ChangePassword.View.ChangePassword || value > ChangePassword.View.Success)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this.CurrentView)
				{
					this._currentView = value;
				}
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x0008B5C0 File Offset: 0x000897C0
		// (set) Token: 0x06002ADC RID: 10972 RVA: 0x0008B5E9 File Offset: 0x000897E9
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("ChangePassword_DisplayUserName")]
		public virtual bool DisplayUserName
		{
			get
			{
				object obj = this.ViewState["DisplayUserName"];
				return obj != null && (bool)obj;
			}
			set
			{
				if (this.DisplayUserName != value)
				{
					this.ViewState["DisplayUserName"] = value;
					this.UpdateValidators();
				}
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x0008B610 File Offset: 0x00089810
		// (set) Token: 0x06002ADE RID: 10974 RVA: 0x0008B63D File Offset: 0x0008983D
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("LoginControls_EditProfileIconUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string EditProfileIconUrl
		{
			get
			{
				object obj = this.ViewState["EditProfileIconUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["EditProfileIconUrl"] = value;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002ADF RID: 10975 RVA: 0x0008B650 File Offset: 0x00089850
		// (set) Token: 0x06002AE0 RID: 10976 RVA: 0x0008B67D File Offset: 0x0008987D
		[Localizable(true)]
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_EditProfileText")]
		public virtual string EditProfileText
		{
			get
			{
				object obj = this.ViewState["EditProfileText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["EditProfileText"] = value;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x0008B690 File Offset: 0x00089890
		// (set) Token: 0x06002AE2 RID: 10978 RVA: 0x0008B6BD File Offset: 0x000898BD
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_EditProfileUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string EditProfileUrl
		{
			get
			{
				object obj = this.ViewState["EditProfileUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["EditProfileUrl"] = value;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x0008B6D0 File Offset: 0x000898D0
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("WebControl_FailureTextStyle")]
		public TableItemStyle FailureTextStyle
		{
			get
			{
				if (this._failureTextStyle == null)
				{
					this._failureTextStyle = new ErrorTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._failureTextStyle).TrackViewState();
					}
				}
				return this._failureTextStyle;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x0008B700 File Offset: 0x00089900
		// (set) Token: 0x06002AE5 RID: 10981 RVA: 0x0008B72D File Offset: 0x0008992D
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("LoginControls_HelpPageIconUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string HelpPageIconUrl
		{
			get
			{
				object obj = this.ViewState["HelpPageIconUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HelpPageIconUrl"] = value;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x0008B740 File Offset: 0x00089940
		// (set) Token: 0x06002AE7 RID: 10983 RVA: 0x0008B76D File Offset: 0x0008996D
		[Localizable(true)]
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_HelpPageText")]
		public virtual string HelpPageText
		{
			get
			{
				object obj = this.ViewState["HelpPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HelpPageText"] = value;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x0008B780 File Offset: 0x00089980
		// (set) Token: 0x06002AE9 RID: 10985 RVA: 0x0008B7AD File Offset: 0x000899AD
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("LoginControls_HelpPageUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string HelpPageUrl
		{
			get
			{
				object obj = this.ViewState["HelpPageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HelpPageUrl"] = value;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x0008B7C0 File Offset: 0x000899C0
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("WebControl_HyperLinkStyle")]
		public TableItemStyle HyperLinkStyle
		{
			get
			{
				if (this._hyperLinkStyle == null)
				{
					this._hyperLinkStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._hyperLinkStyle).TrackViewState();
					}
				}
				return this._hyperLinkStyle;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x0008B7F0 File Offset: 0x000899F0
		// (set) Token: 0x06002AEC RID: 10988 RVA: 0x0008B81D File Offset: 0x00089A1D
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("WebControl_InstructionText")]
		public virtual string InstructionText
		{
			get
			{
				object obj = this.ViewState["InstructionText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["InstructionText"] = value;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x0008B830 File Offset: 0x00089A30
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("WebControl_InstructionTextStyle")]
		public TableItemStyle InstructionTextStyle
		{
			get
			{
				if (this._instructionTextStyle == null)
				{
					this._instructionTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._instructionTextStyle).TrackViewState();
					}
				}
				return this._instructionTextStyle;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x0008B85E File Offset: 0x00089A5E
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("LoginControls_LabelStyle")]
		public TableItemStyle LabelStyle
		{
			get
			{
				if (this._labelStyle == null)
				{
					this._labelStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._labelStyle).TrackViewState();
					}
				}
				return this._labelStyle;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x0008B88C File Offset: 0x00089A8C
		// (set) Token: 0x06002AF0 RID: 10992 RVA: 0x0008B8B9 File Offset: 0x00089AB9
		[WebCategory("Data")]
		[DefaultValue("")]
		[Themeable(false)]
		[WebSysDescription("MembershipProvider_Name")]
		public virtual string MembershipProvider
		{
			get
			{
				object obj = this.ViewState["MembershipProvider"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["MembershipProvider"] = value;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x0008B8CC File Offset: 0x00089ACC
		[Browsable(false)]
		[Themeable(false)]
		[Filterable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string NewPassword
		{
			get
			{
				if (this._newPassword != null)
				{
					return this._newPassword;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x0008B8E4 File Offset: 0x00089AE4
		private string NewPasswordInternal
		{
			get
			{
				string newPassword = this.NewPassword;
				if (string.IsNullOrEmpty(newPassword) && this._changePasswordContainer != null)
				{
					ITextControl textControl = (ITextControl)this._changePasswordContainer.NewPasswordTextBox;
					if (textControl != null)
					{
						return textControl.Text;
					}
				}
				return newPassword;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x0008B924 File Offset: 0x00089B24
		// (set) Token: 0x06002AF4 RID: 10996 RVA: 0x0008B956 File Offset: 0x00089B56
		[WebCategory("Validation")]
		[WebSysDefaultValue("Password_InvalidPasswordErrorMessage")]
		[WebSysDescription("ChangePassword_NewPasswordRegularExpressionErrorMessage")]
		public virtual string NewPasswordRegularExpressionErrorMessage
		{
			get
			{
				object obj = this.ViewState["NewPasswordRegularExpressionErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("Password_InvalidPasswordErrorMessage");
			}
			set
			{
				this.ViewState["NewPasswordRegularExpressionErrorMessage"] = value;
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x0008B96C File Offset: 0x00089B6C
		// (set) Token: 0x06002AF6 RID: 10998 RVA: 0x0008B99E File Offset: 0x00089B9E
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ChangePassword_DefaultNewPasswordLabelText")]
		[WebSysDescription("ChangePassword_NewPasswordLabelText")]
		public virtual string NewPasswordLabelText
		{
			get
			{
				object obj = this.ViewState["NewPasswordLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultNewPasswordLabelText");
			}
			set
			{
				this.ViewState["NewPasswordLabelText"] = value;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x0008B9B4 File Offset: 0x00089BB4
		// (set) Token: 0x06002AF8 RID: 11000 RVA: 0x0008B9E1 File Offset: 0x00089BE1
		[WebCategory("Validation")]
		[WebSysDefaultValue("")]
		[WebSysDescription("ChangePassword_NewPasswordRegularExpression")]
		public virtual string NewPasswordRegularExpression
		{
			get
			{
				object obj = this.ViewState["NewPasswordRegularExpression"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (this.NewPasswordRegularExpression != value)
				{
					this.ViewState["NewPasswordRegularExpression"] = value;
					this.UpdateValidators();
				}
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x0008BA08 File Offset: 0x00089C08
		// (set) Token: 0x06002AFA RID: 11002 RVA: 0x0008BA3A File Offset: 0x00089C3A
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("ChangePassword_DefaultNewPasswordRequiredErrorMessage")]
		[WebSysDescription("ChangePassword_NewPasswordRequiredErrorMessage")]
		public virtual string NewPasswordRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["NewPasswordRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultNewPasswordRequiredErrorMessage");
			}
			set
			{
				this.ViewState["NewPasswordRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x0008BA4D File Offset: 0x00089C4D
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("ChangePassword_PasswordHintStyle")]
		public TableItemStyle PasswordHintStyle
		{
			get
			{
				if (this._passwordHintStyle == null)
				{
					this._passwordHintStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._passwordHintStyle).TrackViewState();
					}
				}
				return this._passwordHintStyle;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x0008BA7C File Offset: 0x00089C7C
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x0008BAA9 File Offset: 0x00089CA9
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_PasswordHintText")]
		public virtual string PasswordHintText
		{
			get
			{
				object obj = this.ViewState["PasswordHintText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PasswordHintText"] = value;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x0008BABC File Offset: 0x00089CBC
		// (set) Token: 0x06002AFF RID: 11007 RVA: 0x0008BAEE File Offset: 0x00089CEE
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("LoginControls_DefaultPasswordLabelText")]
		[WebSysDescription("LoginControls_PasswordLabelText")]
		public virtual string PasswordLabelText
		{
			get
			{
				object obj = this.ViewState["PasswordLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("LoginControls_DefaultPasswordLabelText");
			}
			set
			{
				this.ViewState["PasswordLabelText"] = value;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x0008BB04 File Offset: 0x00089D04
		// (set) Token: 0x06002B01 RID: 11009 RVA: 0x0008BB31 File Offset: 0x00089D31
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_PasswordRecoveryIconUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string PasswordRecoveryIconUrl
		{
			get
			{
				object obj = this.ViewState["PasswordRecoveryIconUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PasswordRecoveryIconUrl"] = value;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x0008BB44 File Offset: 0x00089D44
		// (set) Token: 0x06002B03 RID: 11011 RVA: 0x0008BB71 File Offset: 0x00089D71
		[Localizable(true)]
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_PasswordRecoveryText")]
		public virtual string PasswordRecoveryText
		{
			get
			{
				object obj = this.ViewState["PasswordRecoveryText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PasswordRecoveryText"] = value;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x0008BB84 File Offset: 0x00089D84
		// (set) Token: 0x06002B05 RID: 11013 RVA: 0x0008BBB1 File Offset: 0x00089DB1
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_PasswordRecoveryUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string PasswordRecoveryUrl
		{
			get
			{
				object obj = this.ViewState["PasswordRecoveryUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PasswordRecoveryUrl"] = value;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x0008BBC4 File Offset: 0x00089DC4
		// (set) Token: 0x06002B07 RID: 11015 RVA: 0x0008BBF6 File Offset: 0x00089DF6
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("ChangePassword_DefaultPasswordRequiredErrorMessage")]
		[WebSysDescription("ChangePassword_PasswordRequiredErrorMessage")]
		public virtual string PasswordRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["PasswordRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultPasswordRequiredErrorMessage");
			}
			set
			{
				this.ViewState["PasswordRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x0008BC09 File Offset: 0x00089E09
		private bool RegExpEnabled
		{
			get
			{
				return this.NewPasswordRegularExpression.Length > 0;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x0008BC19 File Offset: 0x00089E19
		[WebCategory("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Themeable(false)]
		[WebSysDescription("ChangePassword_MailDefinition")]
		public MailDefinition MailDefinition
		{
			get
			{
				if (this._mailDefinition == null)
				{
					this._mailDefinition = new MailDefinition();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._mailDefinition).TrackViewState();
					}
				}
				return this._mailDefinition;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x0008BC48 File Offset: 0x00089E48
		// (set) Token: 0x06002B0B RID: 11019 RVA: 0x0008BC71 File Offset: 0x00089E71
		[WebCategory("Layout")]
		[DefaultValue(true)]
		[WebSysDescription("LoginControls_RenderOuterTable")]
		public virtual bool RenderOuterTable
		{
			get
			{
				object obj = this.ViewState["RenderOuterTable"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["RenderOuterTable"] = value;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x0008BC8C File Offset: 0x00089E8C
		// (set) Token: 0x06002B0D RID: 11021 RVA: 0x0008BCB9 File Offset: 0x00089EB9
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("LoginControls_SuccessPageUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
		[UrlProperty]
		public virtual string SuccessPageUrl
		{
			get
			{
				object obj = this.ViewState["SuccessPageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["SuccessPageUrl"] = value;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x0008BCCC File Offset: 0x00089ECC
		// (set) Token: 0x06002B0F RID: 11023 RVA: 0x0008BCD4 File Offset: 0x00089ED4
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ChangePassword))]
		public virtual ITemplate SuccessTemplate
		{
			get
			{
				return this._successTemplate;
			}
			set
			{
				this._successTemplate = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x0008BCE4 File Offset: 0x00089EE4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control SuccessTemplateContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._successContainer;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x0008BCF4 File Offset: 0x00089EF4
		// (set) Token: 0x06002B12 RID: 11026 RVA: 0x0008BD26 File Offset: 0x00089F26
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ChangePassword_DefaultSuccessText")]
		[WebSysDescription("ChangePassword_SuccessText")]
		public virtual string SuccessText
		{
			get
			{
				object obj = this.ViewState["SuccessText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultSuccessText");
			}
			set
			{
				this.ViewState["SuccessText"] = value;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x0008BD39 File Offset: 0x00089F39
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("ChangePassword_SuccessTextStyle")]
		public TableItemStyle SuccessTextStyle
		{
			get
			{
				if (this._successTextStyle == null)
				{
					this._successTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._successTextStyle).TrackViewState();
					}
				}
				return this._successTextStyle;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x0008BD68 File Offset: 0x00089F68
		// (set) Token: 0x06002B15 RID: 11029 RVA: 0x0008BD9A File Offset: 0x00089F9A
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ChangePassword_DefaultSuccessTitleText")]
		[WebSysDescription("ChangePassword_SuccessTitleText")]
		public virtual string SuccessTitleText
		{
			get
			{
				object obj = this.ViewState["SuccessTitleText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultSuccessTitleText");
			}
			set
			{
				this.ViewState["SuccessTitleText"] = value;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06002B16 RID: 11030 RVA: 0x0008BDAD File Offset: 0x00089FAD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x0008BDB1 File Offset: 0x00089FB1
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("LoginControls_TextBoxStyle")]
		public Style TextBoxStyle
		{
			get
			{
				if (this._textBoxStyle == null)
				{
					this._textBoxStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._textBoxStyle).TrackViewState();
					}
				}
				return this._textBoxStyle;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x0008BDDF File Offset: 0x00089FDF
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("LoginControls_TitleTextStyle")]
		public TableItemStyle TitleTextStyle
		{
			get
			{
				if (this._titleTextStyle == null)
				{
					this._titleTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._titleTextStyle).TrackViewState();
					}
				}
				return this._titleTextStyle;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x0008BE0D File Offset: 0x0008A00D
		// (set) Token: 0x06002B1A RID: 11034 RVA: 0x0008BE23 File Offset: 0x0008A023
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("UserName_InitialValue")]
		public virtual string UserName
		{
			get
			{
				if (this._userName != null)
				{
					return this._userName;
				}
				return string.Empty;
			}
			set
			{
				this._userName = value;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x0008BE2C File Offset: 0x0008A02C
		private string UserNameInternal
		{
			get
			{
				string userName = this.UserName;
				if (string.IsNullOrEmpty(userName) && this._changePasswordContainer != null && this.DisplayUserName)
				{
					ITextControl textControl = (ITextControl)this._changePasswordContainer.UserNameTextBox;
					if (textControl != null)
					{
						return textControl.Text;
					}
				}
				return userName;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x0008BE74 File Offset: 0x0008A074
		// (set) Token: 0x06002B1D RID: 11037 RVA: 0x0008BEA6 File Offset: 0x0008A0A6
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ChangePassword_DefaultUserNameLabelText")]
		[WebSysDescription("LoginControls_UserNameLabelText")]
		public virtual string UserNameLabelText
		{
			get
			{
				object obj = this.ViewState["UserNameLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultUserNameLabelText");
			}
			set
			{
				this.ViewState["UserNameLabelText"] = value;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06002B1E RID: 11038 RVA: 0x0008BEBC File Offset: 0x0008A0BC
		// (set) Token: 0x06002B1F RID: 11039 RVA: 0x0008BEEE File Offset: 0x0008A0EE
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("ChangePassword_DefaultUserNameRequiredErrorMessage")]
		[WebSysDescription("ChangePassword_UserNameRequiredErrorMessage")]
		public virtual string UserNameRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["UserNameRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("ChangePassword_DefaultUserNameRequiredErrorMessage");
			}
			set
			{
				this.ViewState["UserNameRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x0008BF01 File Offset: 0x0008A101
		// (set) Token: 0x06002B21 RID: 11041 RVA: 0x0008BF09 File Offset: 0x0008A109
		internal Control ValidatorRow
		{
			get
			{
				return this._validatorRow;
			}
			set
			{
				this._validatorRow = value;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x0008BF12 File Offset: 0x0008A112
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("ChangePassword_ValidatorTextStyle")]
		public Style ValidatorTextStyle
		{
			get
			{
				if (this._validatorTextStyle == null)
				{
					this._validatorTextStyle = new ErrorStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._validatorTextStyle).TrackViewState();
					}
				}
				return this._validatorTextStyle;
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06002B23 RID: 11043 RVA: 0x0008BF40 File Offset: 0x0008A140
		// (remove) Token: 0x06002B24 RID: 11044 RVA: 0x0008BF53 File Offset: 0x0008A153
		[WebCategory("Action")]
		[WebSysDescription("ChangePassword_CancelButtonClick")]
		public event EventHandler CancelButtonClick
		{
			add
			{
				base.Events.AddHandler(ChangePassword.EventCancelButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ChangePassword.EventCancelButtonClick, value);
			}
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06002B25 RID: 11045 RVA: 0x0008BF66 File Offset: 0x0008A166
		// (remove) Token: 0x06002B26 RID: 11046 RVA: 0x0008BF79 File Offset: 0x0008A179
		[WebCategory("Action")]
		[WebSysDescription("ChangePassword_ChangedPassword")]
		public event EventHandler ChangedPassword
		{
			add
			{
				base.Events.AddHandler(ChangePassword.EventChangedPassword, value);
			}
			remove
			{
				base.Events.RemoveHandler(ChangePassword.EventChangedPassword, value);
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06002B27 RID: 11047 RVA: 0x0008BF8C File Offset: 0x0008A18C
		// (remove) Token: 0x06002B28 RID: 11048 RVA: 0x0008BF9F File Offset: 0x0008A19F
		[WebCategory("Action")]
		[WebSysDescription("ChangePassword_ChangePasswordError")]
		public event EventHandler ChangePasswordError
		{
			add
			{
				base.Events.AddHandler(ChangePassword.EventChangePasswordError, value);
			}
			remove
			{
				base.Events.RemoveHandler(ChangePassword.EventChangePasswordError, value);
			}
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06002B29 RID: 11049 RVA: 0x0008BFB2 File Offset: 0x0008A1B2
		// (remove) Token: 0x06002B2A RID: 11050 RVA: 0x0008BFC5 File Offset: 0x0008A1C5
		[WebCategory("Action")]
		[WebSysDescription("ChangePassword_ChangingPassword")]
		public event LoginCancelEventHandler ChangingPassword
		{
			add
			{
				base.Events.AddHandler(ChangePassword.EventChangingPassword, value);
			}
			remove
			{
				base.Events.RemoveHandler(ChangePassword.EventChangingPassword, value);
			}
		}

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06002B2B RID: 11051 RVA: 0x0008BFD8 File Offset: 0x0008A1D8
		// (remove) Token: 0x06002B2C RID: 11052 RVA: 0x0008BFEB File Offset: 0x0008A1EB
		[WebCategory("Action")]
		[WebSysDescription("ChangePassword_ContinueButtonClick")]
		public event EventHandler ContinueButtonClick
		{
			add
			{
				base.Events.AddHandler(ChangePassword.EventContinueButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ChangePassword.EventContinueButtonClick, value);
			}
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06002B2D RID: 11053 RVA: 0x0008BFFE File Offset: 0x0008A1FE
		// (remove) Token: 0x06002B2E RID: 11054 RVA: 0x0008C011 File Offset: 0x0008A211
		[WebCategory("Action")]
		[WebSysDescription("ChangePassword_SendingMail")]
		public event MailMessageEventHandler SendingMail
		{
			add
			{
				base.Events.AddHandler(ChangePassword.EventSendingMail, value);
			}
			remove
			{
				base.Events.RemoveHandler(ChangePassword.EventSendingMail, value);
			}
		}

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06002B2F RID: 11055 RVA: 0x0008C024 File Offset: 0x0008A224
		// (remove) Token: 0x06002B30 RID: 11056 RVA: 0x0008C037 File Offset: 0x0008A237
		[WebCategory("Action")]
		[WebSysDescription("ChangePassword_SendMailError")]
		public event SendMailErrorEventHandler SendMailError
		{
			add
			{
				base.Events.AddHandler(ChangePassword.EventSendMailError, value);
			}
			remove
			{
				base.Events.RemoveHandler(ChangePassword.EventSendMailError, value);
			}
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x0008C04C File Offset: 0x0008A24C
		private void AttemptChangePassword()
		{
			if (this.Page != null && !this.Page.IsValid)
			{
				return;
			}
			LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
			this.OnChangingPassword(loginCancelEventArgs);
			if (loginCancelEventArgs.Cancel)
			{
				return;
			}
			MembershipProvider provider = LoginUtil.GetProvider(this.MembershipProvider);
			MembershipUser user = provider.GetUser(this.UserNameInternal, false, false);
			string newPasswordInternal = this.NewPasswordInternal;
			if (user != null && user.ChangePassword(this.CurrentPasswordInternal, newPasswordInternal, false))
			{
				if (user.IsApproved && !user.IsLockedOut)
				{
					FormsAuthentication.SetAuthCookie(this.UserNameInternal, false);
				}
				this.OnChangedPassword(EventArgs.Empty);
				this.PerformSuccessAction(user.Email, user.UserName, newPasswordInternal);
				return;
			}
			this.OnChangePasswordError(EventArgs.Empty);
			string text = this.ChangePasswordFailureText;
			if (!string.IsNullOrEmpty(text))
			{
				text = string.Format(CultureInfo.CurrentCulture, text, new object[]
				{
					provider.MinRequiredPasswordLength,
					provider.MinRequiredNonAlphanumericCharacters
				});
			}
			this.SetFailureTextLabel(this._changePasswordContainer, text);
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x0008C14F File Offset: 0x0008A34F
		private void ConfirmNewPasswordTextChanged(object source, EventArgs e)
		{
			this._confirmNewPassword = ((ITextControl)source).Text;
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x0008C164 File Offset: 0x0008A364
		private void CreateChangePasswordViewControls()
		{
			this._changePasswordContainer = new ChangePassword.ChangePasswordContainer(this);
			this._changePasswordContainer.ID = "ChangePasswordContainerID";
			this._changePasswordContainer.RenderDesignerRegion = this._renderDesignerRegion;
			ITemplate template = this.ChangePasswordTemplate;
			bool flag = template == null;
			if (flag)
			{
				this._changePasswordContainer.EnableViewState = false;
				this._changePasswordContainer.EnableTheming = false;
				template = new ChangePassword.DefaultChangePasswordTemplate(this);
			}
			template.InstantiateIn(this._changePasswordContainer);
			this.Controls.Add(this._changePasswordContainer);
			IEditableTextControl editableTextControl = this._changePasswordContainer.UserNameTextBox as IEditableTextControl;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += this.UserNameTextChanged;
			}
			IEditableTextControl editableTextControl2 = this._changePasswordContainer.CurrentPasswordTextBox as IEditableTextControl;
			if (editableTextControl2 != null)
			{
				editableTextControl2.TextChanged += this.PasswordTextChanged;
			}
			IEditableTextControl editableTextControl3 = this._changePasswordContainer.NewPasswordTextBox as IEditableTextControl;
			if (editableTextControl3 != null)
			{
				editableTextControl3.TextChanged += this.NewPasswordTextChanged;
			}
			IEditableTextControl editableTextControl4 = this._changePasswordContainer.ConfirmNewPasswordTextBox as IEditableTextControl;
			if (editableTextControl4 != null)
			{
				editableTextControl4.TextChanged += this.ConfirmNewPasswordTextChanged;
			}
			this.SetEditableChildProperties();
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x0008C28D File Offset: 0x0008A48D
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			this.CreateChangePasswordViewControls();
			this.CreateSuccessViewControls();
			this.UpdateValidators();
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x0008C2AC File Offset: 0x0008A4AC
		private void CreateSuccessViewControls()
		{
			this._successContainer = new ChangePassword.SuccessContainer(this);
			this._successContainer.ID = "SuccessContainerID";
			this._successContainer.RenderDesignerRegion = this._renderDesignerRegion;
			ITemplate template;
			if (this.SuccessTemplate != null)
			{
				template = this.SuccessTemplate;
			}
			else
			{
				template = new ChangePassword.DefaultSuccessTemplate(this);
				this._successContainer.EnableViewState = false;
				this._successContainer.EnableTheming = false;
			}
			template.InstantiateIn(this._successContainer);
			this.Controls.Add(this._successContainer);
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x0008C338 File Offset: 0x0008A538
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState != null)
			{
				Triplet triplet = (Triplet)savedState;
				if (triplet.First != null)
				{
					base.LoadControlState(triplet.First);
				}
				if (triplet.Second != null)
				{
					this._currentView = (ChangePassword.View)((int)triplet.Second);
				}
				if (triplet.Third != null)
				{
					this._userName = (string)triplet.Third;
				}
			}
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x0008C398 File Offset: 0x0008A598
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
			}
			else
			{
				object[] array = (object[])savedState;
				if (array.Length != 14)
				{
					throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
				}
				base.LoadViewState(array[0]);
				if (array[1] != null)
				{
					((IStateManager)this.ChangePasswordButtonStyle).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					((IStateManager)this.LabelStyle).LoadViewState(array[2]);
				}
				if (array[3] != null)
				{
					((IStateManager)this.TextBoxStyle).LoadViewState(array[3]);
				}
				if (array[4] != null)
				{
					((IStateManager)this.HyperLinkStyle).LoadViewState(array[4]);
				}
				if (array[5] != null)
				{
					((IStateManager)this.InstructionTextStyle).LoadViewState(array[5]);
				}
				if (array[6] != null)
				{
					((IStateManager)this.TitleTextStyle).LoadViewState(array[6]);
				}
				if (array[7] != null)
				{
					((IStateManager)this.PasswordHintStyle).LoadViewState(array[7]);
				}
				if (array[8] != null)
				{
					((IStateManager)this.FailureTextStyle).LoadViewState(array[8]);
				}
				if (array[9] != null)
				{
					((IStateManager)this.MailDefinition).LoadViewState(array[9]);
				}
				if (array[10] != null)
				{
					((IStateManager)this.CancelButtonStyle).LoadViewState(array[10]);
				}
				if (array[11] != null)
				{
					((IStateManager)this.ContinueButtonStyle).LoadViewState(array[11]);
				}
				if (array[12] != null)
				{
					((IStateManager)this.SuccessTextStyle).LoadViewState(array[12]);
				}
				if (array[13] != null)
				{
					((IStateManager)this.ValidatorTextStyle).LoadViewState(array[13]);
				}
			}
			this.UpdateValidators();
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x0008C4E2 File Offset: 0x0008A6E2
		private void NewPasswordTextChanged(object source, EventArgs e)
		{
			this._newPassword = ((ITextControl)source).Text;
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x0008C4F8 File Offset: 0x0008A6F8
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			bool result = false;
			if (e is CommandEventArgs)
			{
				CommandEventArgs commandEventArgs = (CommandEventArgs)e;
				if (commandEventArgs.CommandName.Equals(ChangePassword.ChangePasswordButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
				{
					this.AttemptChangePassword();
					result = true;
				}
				else if (commandEventArgs.CommandName.Equals(ChangePassword.CancelButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
				{
					this.OnCancelButtonClick(commandEventArgs);
					result = true;
				}
				else if (commandEventArgs.CommandName.Equals(ChangePassword.ContinueButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
				{
					this.OnContinueButtonClick(commandEventArgs);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x0008C570 File Offset: 0x0008A770
		protected virtual void OnCancelButtonClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ChangePassword.EventCancelButtonClick];
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

		// Token: 0x06002B3B RID: 11067 RVA: 0x0008C5C8 File Offset: 0x0008A7C8
		protected virtual void OnChangedPassword(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ChangePassword.EventChangedPassword];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x0008C5F8 File Offset: 0x0008A7F8
		protected virtual void OnChangePasswordError(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ChangePassword.EventChangePasswordError];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x0008C628 File Offset: 0x0008A828
		protected virtual void OnChangingPassword(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[ChangePassword.EventChangingPassword];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x0008C658 File Offset: 0x0008A858
		protected virtual void OnContinueButtonClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ChangePassword.EventContinueButtonClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			string continueDestinationPageUrl = this.ContinueDestinationPageUrl;
			if (!string.IsNullOrEmpty(continueDestinationPageUrl))
			{
				this.Page.Response.Redirect(base.ResolveClientUrl(continueDestinationPageUrl), false);
			}
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x0008C6B0 File Offset: 0x0008A8B0
		protected internal override void OnInit(EventArgs e)
		{
			if (!base.DesignMode)
			{
				string userName = LoginUtil.GetUserName(this);
				if (!string.IsNullOrEmpty(userName))
				{
					this.UserName = userName;
				}
			}
			base.OnInit(e);
			this.Page.RegisterRequiresControlState(this);
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x0008C6F0 File Offset: 0x0008A8F0
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.CurrentView == ChangePassword.View.ChangePassword)
			{
				this.SetEditableChildProperties();
			}
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x0008C714 File Offset: 0x0008A914
		protected virtual void OnSendingMail(MailMessageEventArgs e)
		{
			MailMessageEventHandler mailMessageEventHandler = (MailMessageEventHandler)base.Events[ChangePassword.EventSendingMail];
			if (mailMessageEventHandler != null)
			{
				mailMessageEventHandler(this, e);
			}
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x0008C744 File Offset: 0x0008A944
		protected virtual void OnSendMailError(SendMailErrorEventArgs e)
		{
			SendMailErrorEventHandler sendMailErrorEventHandler = (SendMailErrorEventHandler)base.Events[ChangePassword.EventSendMailError];
			if (sendMailErrorEventHandler != null)
			{
				sendMailErrorEventHandler(this, e);
			}
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x0008C772 File Offset: 0x0008A972
		private void PasswordTextChanged(object source, EventArgs e)
		{
			this._password = ((ITextControl)source).Text;
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x0008C788 File Offset: 0x0008A988
		private void PerformSuccessAction(string email, string userName, string newPassword)
		{
			if (this._mailDefinition != null && !string.IsNullOrEmpty(email))
			{
				LoginUtil.SendPasswordMail(email, userName, newPassword, this.MailDefinition, null, null, new LoginUtil.OnSendingMailDelegate(this.OnSendingMail), new LoginUtil.OnSendMailErrorDelegate(this.OnSendMailError), this);
			}
			string successPageUrl = this.SuccessPageUrl;
			if (!string.IsNullOrEmpty(successPageUrl))
			{
				this.Page.Response.Redirect(base.ResolveClientUrl(successPageUrl), false);
				return;
			}
			this.CurrentView = ChangePassword.View.Success;
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x0008C7FF File Offset: 0x0008A9FF
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			if (base.DesignMode)
			{
				base.ChildControlsCreated = false;
			}
			this.EnsureChildControls();
			this.SetChildProperties();
			this.RenderContents(writer);
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x0008C838 File Offset: 0x0008AA38
		protected internal override object SaveControlState()
		{
			object x = base.SaveControlState();
			object z = null;
			object y = (int)this._currentView;
			if (this._userName != null && this._currentView != ChangePassword.View.Success)
			{
				z = this._userName;
			}
			return new Triplet(x, y, z);
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x0008C87C File Offset: 0x0008AA7C
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this._changePasswordButtonStyle != null) ? ((IStateManager)this._changePasswordButtonStyle).SaveViewState() : null,
				(this._labelStyle != null) ? ((IStateManager)this._labelStyle).SaveViewState() : null,
				(this._textBoxStyle != null) ? ((IStateManager)this._textBoxStyle).SaveViewState() : null,
				(this._hyperLinkStyle != null) ? ((IStateManager)this._hyperLinkStyle).SaveViewState() : null,
				(this._instructionTextStyle != null) ? ((IStateManager)this._instructionTextStyle).SaveViewState() : null,
				(this._titleTextStyle != null) ? ((IStateManager)this._titleTextStyle).SaveViewState() : null,
				(this._passwordHintStyle != null) ? ((IStateManager)this._passwordHintStyle).SaveViewState() : null,
				(this._failureTextStyle != null) ? ((IStateManager)this._failureTextStyle).SaveViewState() : null,
				(this._mailDefinition != null) ? ((IStateManager)this._mailDefinition).SaveViewState() : null,
				(this._cancelButtonStyle != null) ? ((IStateManager)this._cancelButtonStyle).SaveViewState() : null,
				(this._continueButtonStyle != null) ? ((IStateManager)this._continueButtonStyle).SaveViewState() : null,
				(this._successTextStyle != null) ? ((IStateManager)this._successTextStyle).SaveViewState() : null,
				(this._validatorTextStyle != null) ? ((IStateManager)this._validatorTextStyle).SaveViewState() : null
			};
			for (int i = 0; i < 14; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x0008C9FC File Offset: 0x0008ABFC
		private void SetFailureTextLabel(ChangePassword.ChangePasswordContainer container, string failureText)
		{
			ITextControl textControl = (ITextControl)container.FailureTextLabel;
			if (textControl != null)
			{
				textControl.Text = failureText;
			}
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x0008CA20 File Offset: 0x0008AC20
		internal void SetChildProperties()
		{
			ChangePassword.View currentView = this.CurrentView;
			if (currentView != ChangePassword.View.ChangePassword)
			{
				if (currentView != ChangePassword.View.Success)
				{
					return;
				}
				this.SetCommonSuccessViewProperties();
				if (this.SuccessTemplate == null)
				{
					this.SetDefaultSuccessViewProperties();
				}
			}
			else
			{
				this.SetCommonChangePasswordViewProperties();
				if (this.ChangePasswordTemplate == null)
				{
					this.SetDefaultChangePasswordViewProperties();
					return;
				}
			}
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x0008CA65 File Offset: 0x0008AC65
		private void SetCommonChangePasswordViewProperties()
		{
			Util.CopyBaseAttributesToInnerControl(this, this._changePasswordContainer);
			this._changePasswordContainer.ApplyStyle(base.ControlStyle);
			this._successContainer.Visible = false;
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x0008CA90 File Offset: 0x0008AC90
		private void SetCommonSuccessViewProperties()
		{
			Util.CopyBaseAttributesToInnerControl(this, this._successContainer);
			this._successContainer.ApplyStyle(base.ControlStyle);
			this._changePasswordContainer.Visible = false;
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x0008CABC File Offset: 0x0008ACBC
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			if (data != null)
			{
				object obj = data["CurrentView"];
				if (obj != null)
				{
					this.CurrentView = (ChangePassword.View)obj;
				}
				obj = data["ConvertToTemplate"];
				if (obj != null)
				{
					this._convertingToTemplate = (bool)obj;
				}
				obj = data["RegionEditing"];
				if (obj != null)
				{
					this._renderDesignerRegion = (bool)obj;
				}
			}
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x0008CB20 File Offset: 0x0008AD20
		private void SetDefaultChangePasswordViewProperties()
		{
			ChangePassword.ChangePasswordContainer changePasswordContainer = this._changePasswordContainer;
			changePasswordContainer.BorderTable.CellPadding = this.BorderPadding;
			changePasswordContainer.BorderTable.CellSpacing = 0;
			LoginUtil.ApplyStyleToLiteral(changePasswordContainer.Title, this.ChangePasswordTitleText, this.TitleTextStyle, true);
			LoginUtil.ApplyStyleToLiteral(changePasswordContainer.Instruction, this.InstructionText, this.InstructionTextStyle, true);
			LoginUtil.ApplyStyleToLiteral(changePasswordContainer.UserNameLabel, this.UserNameLabelText, this.LabelStyle, false);
			LoginUtil.ApplyStyleToLiteral(changePasswordContainer.CurrentPasswordLabel, this.PasswordLabelText, this.LabelStyle, false);
			LoginUtil.ApplyStyleToLiteral(changePasswordContainer.NewPasswordLabel, this.NewPasswordLabelText, this.LabelStyle, false);
			LoginUtil.ApplyStyleToLiteral(changePasswordContainer.ConfirmNewPasswordLabel, this.ConfirmNewPasswordLabelText, this.LabelStyle, false);
			LoginUtil.ApplyStyleToLiteral(changePasswordContainer.PasswordHintLabel, this.PasswordHintText, this.PasswordHintStyle, false);
			if (this._textBoxStyle != null)
			{
				if (this.DisplayUserName)
				{
					((WebControl)changePasswordContainer.UserNameTextBox).ApplyStyle(this.TextBoxStyle);
				}
				((WebControl)changePasswordContainer.CurrentPasswordTextBox).ApplyStyle(this.TextBoxStyle);
				((WebControl)changePasswordContainer.NewPasswordTextBox).ApplyStyle(this.TextBoxStyle);
				((WebControl)changePasswordContainer.ConfirmNewPasswordTextBox).ApplyStyle(this.TextBoxStyle);
			}
			this._passwordHintTableRow.Visible = !string.IsNullOrEmpty(this.PasswordHintText);
			this._userNameTableRow.Visible = this.DisplayUserName;
			if (this.DisplayUserName)
			{
				((WebControl)changePasswordContainer.UserNameTextBox).TabIndex = this.TabIndex;
				((WebControl)changePasswordContainer.UserNameTextBox).AccessKey = this.AccessKey;
			}
			else
			{
				((WebControl)changePasswordContainer.CurrentPasswordTextBox).AccessKey = this.AccessKey;
			}
			((WebControl)changePasswordContainer.CurrentPasswordTextBox).TabIndex = this.TabIndex;
			((WebControl)changePasswordContainer.NewPasswordTextBox).TabIndex = this.TabIndex;
			((WebControl)changePasswordContainer.ConfirmNewPasswordTextBox).TabIndex = this.TabIndex;
			bool flag = true;
			this.ValidatorRow.Visible = flag;
			RequiredFieldValidator userNameRequired = changePasswordContainer.UserNameRequired;
			userNameRequired.ErrorMessage = this.UserNameRequiredErrorMessage;
			userNameRequired.ToolTip = this.UserNameRequiredErrorMessage;
			userNameRequired.Enabled = flag;
			userNameRequired.Visible = flag;
			if (this._validatorTextStyle != null)
			{
				userNameRequired.ApplyStyle(this._validatorTextStyle);
			}
			RequiredFieldValidator passwordRequired = changePasswordContainer.PasswordRequired;
			passwordRequired.ErrorMessage = this.PasswordRequiredErrorMessage;
			passwordRequired.ToolTip = this.PasswordRequiredErrorMessage;
			passwordRequired.Enabled = flag;
			passwordRequired.Visible = flag;
			RequiredFieldValidator newPasswordRequired = changePasswordContainer.NewPasswordRequired;
			newPasswordRequired.ErrorMessage = this.NewPasswordRequiredErrorMessage;
			newPasswordRequired.ToolTip = this.NewPasswordRequiredErrorMessage;
			newPasswordRequired.Enabled = flag;
			newPasswordRequired.Visible = flag;
			RequiredFieldValidator confirmNewPasswordRequired = changePasswordContainer.ConfirmNewPasswordRequired;
			confirmNewPasswordRequired.ErrorMessage = this.ConfirmPasswordRequiredErrorMessage;
			confirmNewPasswordRequired.ToolTip = this.ConfirmPasswordRequiredErrorMessage;
			confirmNewPasswordRequired.Enabled = flag;
			confirmNewPasswordRequired.Visible = flag;
			CompareValidator newPasswordCompareValidator = changePasswordContainer.NewPasswordCompareValidator;
			newPasswordCompareValidator.ErrorMessage = this.ConfirmPasswordCompareErrorMessage;
			newPasswordCompareValidator.Enabled = flag;
			newPasswordCompareValidator.Visible = flag;
			if (this._validatorTextStyle != null)
			{
				passwordRequired.ApplyStyle(this._validatorTextStyle);
				newPasswordRequired.ApplyStyle(this._validatorTextStyle);
				confirmNewPasswordRequired.ApplyStyle(this._validatorTextStyle);
				newPasswordCompareValidator.ApplyStyle(this._validatorTextStyle);
			}
			RegularExpressionValidator regExpValidator = changePasswordContainer.RegExpValidator;
			regExpValidator.ErrorMessage = this.NewPasswordRegularExpressionErrorMessage;
			regExpValidator.Enabled = flag;
			regExpValidator.Visible = flag;
			if (this._validatorTextStyle != null)
			{
				regExpValidator.ApplyStyle(this._validatorTextStyle);
			}
			LinkButton changePasswordLinkButton = changePasswordContainer.ChangePasswordLinkButton;
			LinkButton cancelLinkButton = changePasswordContainer.CancelLinkButton;
			ImageButton changePasswordImageButton = changePasswordContainer.ChangePasswordImageButton;
			ImageButton cancelImageButton = changePasswordContainer.CancelImageButton;
			Button changePasswordPushButton = changePasswordContainer.ChangePasswordPushButton;
			Button cancelPushButton = changePasswordContainer.CancelPushButton;
			WebControl webControl = null;
			WebControl webControl2 = null;
			switch (this.CancelButtonType)
			{
			case ButtonType.Button:
				cancelPushButton.Text = this.CancelButtonText;
				webControl2 = cancelPushButton;
				break;
			case ButtonType.Image:
				cancelImageButton.ImageUrl = this.CancelButtonImageUrl;
				cancelImageButton.AlternateText = this.CancelButtonText;
				webControl2 = cancelImageButton;
				break;
			case ButtonType.Link:
				cancelLinkButton.Text = this.CancelButtonText;
				webControl2 = cancelLinkButton;
				break;
			}
			switch (this.ChangePasswordButtonType)
			{
			case ButtonType.Button:
				changePasswordPushButton.Text = this.ChangePasswordButtonText;
				webControl = changePasswordPushButton;
				break;
			case ButtonType.Image:
				changePasswordImageButton.ImageUrl = this.ChangePasswordButtonImageUrl;
				changePasswordImageButton.AlternateText = this.ChangePasswordButtonText;
				webControl = changePasswordImageButton;
				break;
			case ButtonType.Link:
				changePasswordLinkButton.Text = this.ChangePasswordButtonText;
				webControl = changePasswordLinkButton;
				break;
			}
			changePasswordLinkButton.Visible = false;
			changePasswordImageButton.Visible = false;
			changePasswordPushButton.Visible = false;
			cancelLinkButton.Visible = false;
			cancelImageButton.Visible = false;
			cancelPushButton.Visible = false;
			webControl.Visible = true;
			webControl2.Visible = true;
			webControl2.TabIndex = this.TabIndex;
			webControl.TabIndex = this.TabIndex;
			if (this.CancelButtonStyle != null)
			{
				webControl2.ApplyStyle(this.CancelButtonStyle);
			}
			if (this.ChangePasswordButtonStyle != null)
			{
				webControl.ApplyStyle(this.ChangePasswordButtonStyle);
			}
			Image createUserIcon = changePasswordContainer.CreateUserIcon;
			HyperLink createUserLink = changePasswordContainer.CreateUserLink;
			LiteralControl createUserLinkSeparator = changePasswordContainer.CreateUserLinkSeparator;
			HyperLink passwordRecoveryLink = changePasswordContainer.PasswordRecoveryLink;
			Image passwordRecoveryIcon = changePasswordContainer.PasswordRecoveryIcon;
			HyperLink helpPageLink = changePasswordContainer.HelpPageLink;
			Image helpPageIcon = changePasswordContainer.HelpPageIcon;
			LiteralControl helpPageLinkSeparator = changePasswordContainer.HelpPageLinkSeparator;
			LiteralControl editProfileLinkSeparator = changePasswordContainer.EditProfileLinkSeparator;
			HyperLink editProfileLink = changePasswordContainer.EditProfileLink;
			Image editProfileIcon = changePasswordContainer.EditProfileIcon;
			string createUserText = this.CreateUserText;
			string createUserIconUrl = this.CreateUserIconUrl;
			string passwordRecoveryText = this.PasswordRecoveryText;
			string passwordRecoveryIconUrl = this.PasswordRecoveryIconUrl;
			string helpPageText = this.HelpPageText;
			string helpPageIconUrl = this.HelpPageIconUrl;
			string editProfileText = this.EditProfileText;
			string editProfileIconUrl = this.EditProfileIconUrl;
			bool flag2 = createUserText.Length > 0;
			bool flag3 = passwordRecoveryText.Length > 0;
			bool flag4 = helpPageText.Length > 0;
			bool flag5 = helpPageIconUrl.Length > 0;
			bool flag6 = createUserIconUrl.Length > 0;
			bool flag7 = passwordRecoveryIconUrl.Length > 0;
			bool flag8 = flag4 || flag5;
			bool flag9 = flag2 || flag6;
			bool flag10 = flag3 || flag7;
			bool flag11 = editProfileText.Length > 0;
			bool flag12 = editProfileIconUrl.Length > 0;
			bool flag13 = flag11 || flag12;
			helpPageLink.Visible = flag4;
			helpPageLinkSeparator.Visible = (flag8 && (flag10 || flag9 || flag13));
			if (flag4)
			{
				helpPageLink.Text = helpPageText;
				helpPageLink.NavigateUrl = this.HelpPageUrl;
				helpPageLink.TabIndex = this.TabIndex;
			}
			helpPageIcon.Visible = flag5;
			if (flag5)
			{
				helpPageIcon.ImageUrl = helpPageIconUrl;
				helpPageIcon.AlternateText = this.HelpPageText;
			}
			createUserLink.Visible = flag2;
			createUserLinkSeparator.Visible = (flag9 && (flag10 || flag13));
			if (flag2)
			{
				createUserLink.Text = createUserText;
				createUserLink.NavigateUrl = this.CreateUserUrl;
				createUserLink.TabIndex = this.TabIndex;
			}
			createUserIcon.Visible = flag6;
			if (flag6)
			{
				createUserIcon.ImageUrl = createUserIconUrl;
				createUserIcon.AlternateText = this.CreateUserText;
			}
			passwordRecoveryLink.Visible = flag3;
			if (flag3)
			{
				passwordRecoveryLink.Text = passwordRecoveryText;
				passwordRecoveryLink.NavigateUrl = this.PasswordRecoveryUrl;
				passwordRecoveryLink.TabIndex = this.TabIndex;
			}
			passwordRecoveryIcon.Visible = flag7;
			if (flag7)
			{
				passwordRecoveryIcon.ImageUrl = passwordRecoveryIconUrl;
				passwordRecoveryIcon.AlternateText = this.PasswordRecoveryText;
			}
			editProfileLinkSeparator.Visible = (flag10 && flag13);
			editProfileLink.Visible = flag11;
			editProfileIcon.Visible = flag12;
			if (flag11)
			{
				editProfileLink.Text = editProfileText;
				editProfileLink.NavigateUrl = this.EditProfileUrl;
				editProfileLink.TabIndex = this.TabIndex;
			}
			if (flag12)
			{
				editProfileIcon.ImageUrl = editProfileIconUrl;
				editProfileIcon.AlternateText = this.EditProfileText;
			}
			if (flag9 || flag10 || flag8 || flag13)
			{
				if (this._hyperLinkStyle != null)
				{
					TableItemStyle tableItemStyle = new TableItemStyle();
					tableItemStyle.CopyFrom(this._hyperLinkStyle);
					tableItemStyle.Font.Reset();
					LoginUtil.SetTableCellStyle(createUserLink, tableItemStyle);
					createUserLink.Font.CopyFrom(this._hyperLinkStyle.Font);
					createUserLink.ForeColor = this._hyperLinkStyle.ForeColor;
					passwordRecoveryLink.Font.CopyFrom(this._hyperLinkStyle.Font);
					passwordRecoveryLink.ForeColor = this._hyperLinkStyle.ForeColor;
					helpPageLink.Font.CopyFrom(this._hyperLinkStyle.Font);
					helpPageLink.ForeColor = this._hyperLinkStyle.ForeColor;
					editProfileLink.Font.CopyFrom(this._hyperLinkStyle.Font);
					editProfileLink.ForeColor = this._hyperLinkStyle.ForeColor;
				}
				LoginUtil.SetTableCellVisible(helpPageLink, true);
			}
			else
			{
				LoginUtil.SetTableCellVisible(helpPageLink, false);
			}
			Control failureTextLabel = changePasswordContainer.FailureTextLabel;
			if (((ITextControl)failureTextLabel).Text.Length > 0)
			{
				LoginUtil.SetTableCellStyle(failureTextLabel, this.FailureTextStyle);
				LoginUtil.SetTableCellVisible(failureTextLabel, true);
				return;
			}
			LoginUtil.SetTableCellVisible(failureTextLabel, false);
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x0008D3D8 File Offset: 0x0008B5D8
		internal void SetDefaultSuccessViewProperties()
		{
			ChangePassword.SuccessContainer successContainer = this._successContainer;
			LinkButton continueLinkButton = successContainer.ContinueLinkButton;
			ImageButton continueImageButton = successContainer.ContinueImageButton;
			Button continuePushButton = successContainer.ContinuePushButton;
			successContainer.BorderTable.CellPadding = this.BorderPadding;
			successContainer.BorderTable.CellSpacing = 0;
			WebControl webControl = null;
			switch (this.ContinueButtonType)
			{
			case ButtonType.Button:
				continuePushButton.Text = this.ContinueButtonText;
				webControl = continuePushButton;
				break;
			case ButtonType.Image:
				continueImageButton.ImageUrl = this.ContinueButtonImageUrl;
				continueImageButton.AlternateText = this.ContinueButtonText;
				webControl = continueImageButton;
				break;
			case ButtonType.Link:
				continueLinkButton.Text = this.ContinueButtonText;
				webControl = continueLinkButton;
				break;
			}
			continueLinkButton.Visible = false;
			continueImageButton.Visible = false;
			continuePushButton.Visible = false;
			webControl.Visible = true;
			webControl.TabIndex = this.TabIndex;
			webControl.AccessKey = this.AccessKey;
			if (this.ContinueButtonStyle != null)
			{
				webControl.ApplyStyle(this.ContinueButtonStyle);
			}
			LoginUtil.ApplyStyleToLiteral(successContainer.Title, this.SuccessTitleText, this._titleTextStyle, true);
			LoginUtil.ApplyStyleToLiteral(successContainer.SuccessTextLabel, this.SuccessText, this._successTextStyle, true);
			string editProfileText = this.EditProfileText;
			string editProfileIconUrl = this.EditProfileIconUrl;
			bool flag = editProfileText.Length > 0;
			bool flag2 = editProfileIconUrl.Length > 0;
			HyperLink editProfileLink = successContainer.EditProfileLink;
			Image editProfileIcon = successContainer.EditProfileIcon;
			editProfileIcon.Visible = flag2;
			editProfileLink.Visible = flag;
			if (flag)
			{
				editProfileLink.Text = editProfileText;
				editProfileLink.NavigateUrl = this.EditProfileUrl;
				editProfileLink.TabIndex = this.TabIndex;
				if (this._hyperLinkStyle != null)
				{
					Style style = new TableItemStyle();
					style.CopyFrom(this._hyperLinkStyle);
					style.Font.Reset();
					LoginUtil.SetTableCellStyle(editProfileLink, style);
					editProfileLink.Font.CopyFrom(this._hyperLinkStyle.Font);
					editProfileLink.ForeColor = this._hyperLinkStyle.ForeColor;
				}
			}
			if (flag2)
			{
				editProfileIcon.ImageUrl = editProfileIconUrl;
				editProfileIcon.AlternateText = this.EditProfileText;
			}
			LoginUtil.SetTableCellVisible(editProfileLink, flag || flag2);
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0008D5E8 File Offset: 0x0008B7E8
		private void SetEditableChildProperties()
		{
			if (this.UserNameInternal.Length > 0 && this.DisplayUserName)
			{
				ITextControl textControl = (ITextControl)this._changePasswordContainer.UserNameTextBox;
				if (textControl != null)
				{
					textControl.Text = this.UserNameInternal;
				}
			}
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x0008D62C File Offset: 0x0008B82C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._changePasswordButtonStyle != null)
			{
				((IStateManager)this._changePasswordButtonStyle).TrackViewState();
			}
			if (this._labelStyle != null)
			{
				((IStateManager)this._labelStyle).TrackViewState();
			}
			if (this._textBoxStyle != null)
			{
				((IStateManager)this._textBoxStyle).TrackViewState();
			}
			if (this._successTextStyle != null)
			{
				((IStateManager)this._successTextStyle).TrackViewState();
			}
			if (this._hyperLinkStyle != null)
			{
				((IStateManager)this._hyperLinkStyle).TrackViewState();
			}
			if (this._instructionTextStyle != null)
			{
				((IStateManager)this._instructionTextStyle).TrackViewState();
			}
			if (this._titleTextStyle != null)
			{
				((IStateManager)this._titleTextStyle).TrackViewState();
			}
			if (this._passwordHintStyle != null)
			{
				((IStateManager)this._passwordHintStyle).TrackViewState();
			}
			if (this._failureTextStyle != null)
			{
				((IStateManager)this._failureTextStyle).TrackViewState();
			}
			if (this._mailDefinition != null)
			{
				((IStateManager)this._mailDefinition).TrackViewState();
			}
			if (this._cancelButtonStyle != null)
			{
				((IStateManager)this._cancelButtonStyle).TrackViewState();
			}
			if (this._continueButtonStyle != null)
			{
				((IStateManager)this._continueButtonStyle).TrackViewState();
			}
			if (this._validatorTextStyle != null)
			{
				((IStateManager)this._validatorTextStyle).TrackViewState();
			}
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x0008D738 File Offset: 0x0008B938
		private void UpdateValidators()
		{
			if (base.DesignMode)
			{
				return;
			}
			ChangePassword.ChangePasswordContainer changePasswordContainer = this._changePasswordContainer;
			if (changePasswordContainer != null)
			{
				bool displayUserName = this.DisplayUserName;
				RequiredFieldValidator userNameRequired = changePasswordContainer.UserNameRequired;
				if (userNameRequired != null)
				{
					userNameRequired.Enabled = displayUserName;
					userNameRequired.Visible = displayUserName;
				}
				bool regExpEnabled = this.RegExpEnabled;
				RegularExpressionValidator regExpValidator = changePasswordContainer.RegExpValidator;
				if (regExpValidator != null)
				{
					regExpValidator.Enabled = regExpEnabled;
					regExpValidator.Visible = regExpEnabled;
				}
			}
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0008D79C File Offset: 0x0008B99C
		private void UserNameTextChanged(object source, EventArgs e)
		{
			string text = ((ITextControl)source).Text;
			if (!string.IsNullOrEmpty(text))
			{
				this.UserName = text;
			}
		}

		// Token: 0x04001ECC RID: 7884
		public static readonly string ChangePasswordButtonCommandName = "ChangePassword";

		// Token: 0x04001ECD RID: 7885
		public static readonly string CancelButtonCommandName = "Cancel";

		// Token: 0x04001ECE RID: 7886
		public static readonly string ContinueButtonCommandName = "Continue";

		// Token: 0x04001ECF RID: 7887
		private ITemplate _changePasswordTemplate;

		// Token: 0x04001ED0 RID: 7888
		private ChangePassword.ChangePasswordContainer _changePasswordContainer;

		// Token: 0x04001ED1 RID: 7889
		private ITemplate _successTemplate;

		// Token: 0x04001ED2 RID: 7890
		private ChangePassword.SuccessContainer _successContainer;

		// Token: 0x04001ED3 RID: 7891
		private string _userName;

		// Token: 0x04001ED4 RID: 7892
		private string _password;

		// Token: 0x04001ED5 RID: 7893
		private string _newPassword;

		// Token: 0x04001ED6 RID: 7894
		private string _confirmNewPassword;

		// Token: 0x04001ED7 RID: 7895
		private bool _convertingToTemplate;

		// Token: 0x04001ED8 RID: 7896
		private bool _renderDesignerRegion;

		// Token: 0x04001ED9 RID: 7897
		private ChangePassword.View _currentView;

		// Token: 0x04001EDA RID: 7898
		private const string _userNameID = "UserName";

		// Token: 0x04001EDB RID: 7899
		private const string _currentPasswordID = "CurrentPassword";

		// Token: 0x04001EDC RID: 7900
		private const string _newPasswordID = "NewPassword";

		// Token: 0x04001EDD RID: 7901
		private const string _confirmNewPasswordID = "ConfirmNewPassword";

		// Token: 0x04001EDE RID: 7902
		private const string _failureTextID = "FailureText";

		// Token: 0x04001EDF RID: 7903
		private const string _userNameRequiredID = "UserNameRequired";

		// Token: 0x04001EE0 RID: 7904
		private const string _currentPasswordRequiredID = "CurrentPasswordRequired";

		// Token: 0x04001EE1 RID: 7905
		private const string _newPasswordRequiredID = "NewPasswordRequired";

		// Token: 0x04001EE2 RID: 7906
		private const string _confirmNewPasswordRequiredID = "ConfirmNewPasswordRequired";

		// Token: 0x04001EE3 RID: 7907
		private const string _newPasswordCompareID = "NewPasswordCompare";

		// Token: 0x04001EE4 RID: 7908
		private const string _newPasswordRegExpID = "NewPasswordRegExp";

		// Token: 0x04001EE5 RID: 7909
		private const string _changePasswordPushButtonID = "ChangePasswordPushButton";

		// Token: 0x04001EE6 RID: 7910
		private const string _changePasswordImageButtonID = "ChangePasswordImageButton";

		// Token: 0x04001EE7 RID: 7911
		private const string _changePasswordLinkButtonID = "ChangePasswordLinkButton";

		// Token: 0x04001EE8 RID: 7912
		private const string _cancelPushButtonID = "CancelPushButton";

		// Token: 0x04001EE9 RID: 7913
		private const string _cancelImageButtonID = "CancelImageButton";

		// Token: 0x04001EEA RID: 7914
		private const string _cancelLinkButtonID = "CancelLinkButton";

		// Token: 0x04001EEB RID: 7915
		private const string _continuePushButtonID = "ContinuePushButton";

		// Token: 0x04001EEC RID: 7916
		private const string _continueImageButtonID = "ContinueImageButton";

		// Token: 0x04001EED RID: 7917
		private const string _continueLinkButtonID = "ContinueLinkButton";

		// Token: 0x04001EEE RID: 7918
		private const string _passwordRecoveryLinkID = "PasswordRecoveryLink";

		// Token: 0x04001EEF RID: 7919
		private const string _helpLinkID = "HelpLink";

		// Token: 0x04001EF0 RID: 7920
		private const string _createUserLinkID = "CreateUserLink";

		// Token: 0x04001EF1 RID: 7921
		private const string _editProfileLinkID = "EditProfileLink";

		// Token: 0x04001EF2 RID: 7922
		private const string _editProfileSuccessLinkID = "EditProfileLinkSuccess";

		// Token: 0x04001EF3 RID: 7923
		private const string _changePasswordViewContainerID = "ChangePasswordContainerID";

		// Token: 0x04001EF4 RID: 7924
		private const string _successViewContainerID = "SuccessContainerID";

		// Token: 0x04001EF5 RID: 7925
		private const ValidatorDisplay _requiredFieldValidatorDisplay = ValidatorDisplay.Static;

		// Token: 0x04001EF6 RID: 7926
		private const ValidatorDisplay _compareFieldValidatorDisplay = ValidatorDisplay.Dynamic;

		// Token: 0x04001EF7 RID: 7927
		private const ValidatorDisplay _regexpFieldValidatorDisplay = ValidatorDisplay.Dynamic;

		// Token: 0x04001EF8 RID: 7928
		private const string _userNameReplacementKey = "<%\\s*UserName\\s*%>";

		// Token: 0x04001EF9 RID: 7929
		private const string _passwordReplacementKey = "<%\\s*Password\\s*%>";

		// Token: 0x04001EFA RID: 7930
		private const int _viewStateArrayLength = 14;

		// Token: 0x04001EFB RID: 7931
		private Style _changePasswordButtonStyle;

		// Token: 0x04001EFC RID: 7932
		private TableItemStyle _labelStyle;

		// Token: 0x04001EFD RID: 7933
		private Style _textBoxStyle;

		// Token: 0x04001EFE RID: 7934
		private TableItemStyle _hyperLinkStyle;

		// Token: 0x04001EFF RID: 7935
		private TableItemStyle _instructionTextStyle;

		// Token: 0x04001F00 RID: 7936
		private TableItemStyle _titleTextStyle;

		// Token: 0x04001F01 RID: 7937
		private TableItemStyle _failureTextStyle;

		// Token: 0x04001F02 RID: 7938
		private TableItemStyle _successTextStyle;

		// Token: 0x04001F03 RID: 7939
		private TableItemStyle _passwordHintStyle;

		// Token: 0x04001F04 RID: 7940
		private Style _cancelButtonStyle;

		// Token: 0x04001F05 RID: 7941
		private Style _continueButtonStyle;

		// Token: 0x04001F06 RID: 7942
		private Style _validatorTextStyle;

		// Token: 0x04001F07 RID: 7943
		private MailDefinition _mailDefinition;

		// Token: 0x04001F08 RID: 7944
		private Control _validatorRow;

		// Token: 0x04001F09 RID: 7945
		private Control _passwordHintTableRow;

		// Token: 0x04001F0A RID: 7946
		private Control _userNameTableRow;

		// Token: 0x04001F0B RID: 7947
		private static readonly object EventChangePasswordError = new object();

		// Token: 0x04001F0C RID: 7948
		private static readonly object EventCancelButtonClick = new object();

		// Token: 0x04001F0D RID: 7949
		private static readonly object EventContinueButtonClick = new object();

		// Token: 0x04001F0E RID: 7950
		private static readonly object EventChangingPassword = new object();

		// Token: 0x04001F0F RID: 7951
		private static readonly object EventChangedPassword = new object();

		// Token: 0x04001F10 RID: 7952
		private static readonly object EventSendingMail = new object();

		// Token: 0x04001F11 RID: 7953
		private static readonly object EventSendMailError = new object();

		// Token: 0x02000993 RID: 2451
		private sealed class DefaultSuccessTemplate : ITemplate
		{
			// Token: 0x06006A7E RID: 27262 RVA: 0x0017C29D File Offset: 0x0017A49D
			public DefaultSuccessTemplate(ChangePassword owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006A7F RID: 27263 RVA: 0x0017C2AC File Offset: 0x0017A4AC
			private void CreateControls(ChangePassword.SuccessContainer successContainer)
			{
				successContainer.Title = new Literal();
				successContainer.SuccessTextLabel = new Literal();
				successContainer.EditProfileLink = new HyperLink();
				successContainer.EditProfileLink.ID = "EditProfileLinkSuccess";
				successContainer.EditProfileIcon = new Image();
				successContainer.ContinueLinkButton = new LinkButton
				{
					ID = "ContinueLinkButton",
					CommandName = ChangePassword.ContinueButtonCommandName,
					CausesValidation = false
				};
				successContainer.ContinueImageButton = new ImageButton
				{
					ID = "ContinueImageButton",
					CommandName = ChangePassword.ContinueButtonCommandName,
					CausesValidation = false
				};
				successContainer.ContinuePushButton = new Button
				{
					ID = "ContinuePushButton",
					CommandName = ChangePassword.ContinueButtonCommandName,
					CausesValidation = false
				};
			}

			// Token: 0x06006A80 RID: 27264 RVA: 0x0017C374 File Offset: 0x0017A574
			private void LayoutControls(ChangePassword.SuccessContainer successContainer)
			{
				Table table = new Table();
				table.CellPadding = 0;
				TableRow tableRow = new LoginUtil.DisappearingTableRow();
				TableCell tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(successContainer.Title);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(successContainer.SuccessTextLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(successContainer.ContinuePushButton);
				tableCell.Controls.Add(successContainer.ContinueLinkButton);
				tableCell.Controls.Add(successContainer.ContinueImageButton);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(successContainer.EditProfileIcon);
				tableCell.Controls.Add(successContainer.EditProfileLink);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				Table table2 = LoginUtil.CreateChildTable(this._owner.ConvertingToTemplate);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(table);
				tableRow.Cells.Add(tableCell);
				table2.Rows.Add(tableRow);
				successContainer.LayoutTable = table;
				successContainer.BorderTable = table2;
				successContainer.Controls.Add(table2);
			}

			// Token: 0x06006A81 RID: 27265 RVA: 0x0017C520 File Offset: 0x0017A720
			void ITemplate.InstantiateIn(Control container)
			{
				ChangePassword.SuccessContainer successContainer = (ChangePassword.SuccessContainer)container;
				this.CreateControls(successContainer);
				this.LayoutControls(successContainer);
			}

			// Token: 0x040038D9 RID: 14553
			private ChangePassword _owner;
		}

		// Token: 0x02000994 RID: 2452
		internal sealed class SuccessContainer : LoginUtil.GenericContainer<ChangePassword>, INonBindingContainer, INamingContainer
		{
			// Token: 0x06006A82 RID: 27266 RVA: 0x0017C542 File Offset: 0x0017A742
			public SuccessContainer(ChangePassword owner) : base(owner)
			{
			}

			// Token: 0x17001D47 RID: 7495
			// (get) Token: 0x06006A83 RID: 27267 RVA: 0x0017C54B File Offset: 0x0017A74B
			// (set) Token: 0x06006A84 RID: 27268 RVA: 0x0017C553 File Offset: 0x0017A753
			internal ImageButton ContinueImageButton
			{
				get
				{
					return this._continueImageButton;
				}
				set
				{
					this._continueImageButton = value;
				}
			}

			// Token: 0x17001D48 RID: 7496
			// (get) Token: 0x06006A85 RID: 27269 RVA: 0x0017C55C File Offset: 0x0017A75C
			// (set) Token: 0x06006A86 RID: 27270 RVA: 0x0017C564 File Offset: 0x0017A764
			internal LinkButton ContinueLinkButton
			{
				get
				{
					return this._continueLinkButton;
				}
				set
				{
					this._continueLinkButton = value;
				}
			}

			// Token: 0x17001D49 RID: 7497
			// (get) Token: 0x06006A87 RID: 27271 RVA: 0x0017C56D File Offset: 0x0017A76D
			// (set) Token: 0x06006A88 RID: 27272 RVA: 0x0017C575 File Offset: 0x0017A775
			internal Button ContinuePushButton
			{
				get
				{
					return this._continuePushButton;
				}
				set
				{
					this._continuePushButton = value;
				}
			}

			// Token: 0x17001D4A RID: 7498
			// (get) Token: 0x06006A89 RID: 27273 RVA: 0x0017C57E File Offset: 0x0017A77E
			protected override bool ConvertingToTemplate
			{
				get
				{
					return base.Owner.ConvertingToTemplate;
				}
			}

			// Token: 0x17001D4B RID: 7499
			// (get) Token: 0x06006A8A RID: 27274 RVA: 0x0017C58B File Offset: 0x0017A78B
			// (set) Token: 0x06006A8B RID: 27275 RVA: 0x0017C593 File Offset: 0x0017A793
			internal Image EditProfileIcon
			{
				get
				{
					return this._editProfileIcon;
				}
				set
				{
					this._editProfileIcon = value;
				}
			}

			// Token: 0x17001D4C RID: 7500
			// (get) Token: 0x06006A8C RID: 27276 RVA: 0x0017C59C File Offset: 0x0017A79C
			// (set) Token: 0x06006A8D RID: 27277 RVA: 0x0017C5A4 File Offset: 0x0017A7A4
			internal HyperLink EditProfileLink
			{
				get
				{
					return this._editProfileLink;
				}
				set
				{
					this._editProfileLink = value;
				}
			}

			// Token: 0x17001D4D RID: 7501
			// (get) Token: 0x06006A8E RID: 27278 RVA: 0x0017C5AD File Offset: 0x0017A7AD
			// (set) Token: 0x06006A8F RID: 27279 RVA: 0x0017C5B5 File Offset: 0x0017A7B5
			public Literal SuccessTextLabel
			{
				get
				{
					return this._successTextLabel;
				}
				set
				{
					this._successTextLabel = value;
				}
			}

			// Token: 0x17001D4E RID: 7502
			// (get) Token: 0x06006A90 RID: 27280 RVA: 0x0017C5BE File Offset: 0x0017A7BE
			// (set) Token: 0x06006A91 RID: 27281 RVA: 0x0017C5C6 File Offset: 0x0017A7C6
			internal Literal Title
			{
				get
				{
					return this._title;
				}
				set
				{
					this._title = value;
				}
			}

			// Token: 0x040038DA RID: 14554
			private Literal _successTextLabel;

			// Token: 0x040038DB RID: 14555
			private Button _continuePushButton;

			// Token: 0x040038DC RID: 14556
			private LinkButton _continueLinkButton;

			// Token: 0x040038DD RID: 14557
			private ImageButton _continueImageButton;

			// Token: 0x040038DE RID: 14558
			private Image _editProfileIcon;

			// Token: 0x040038DF RID: 14559
			private HyperLink _editProfileLink;

			// Token: 0x040038E0 RID: 14560
			private Literal _title;
		}

		// Token: 0x02000995 RID: 2453
		private sealed class DefaultChangePasswordTemplate : ITemplate
		{
			// Token: 0x06006A92 RID: 27282 RVA: 0x0017C5CF File Offset: 0x0017A7CF
			public DefaultChangePasswordTemplate(ChangePassword owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006A93 RID: 27283 RVA: 0x0017C5E0 File Offset: 0x0017A7E0
			private RequiredFieldValidator CreateRequiredFieldValidator(string id, TextBox textBox, string validationGroup, bool enableValidation)
			{
				return new RequiredFieldValidator
				{
					ID = id,
					ValidationGroup = validationGroup,
					ControlToValidate = textBox.ID,
					Display = ValidatorDisplay.Static,
					Text = SR.GetString("LoginControls_DefaultRequiredFieldValidatorText"),
					Enabled = enableValidation,
					Visible = enableValidation
				};
			}

			// Token: 0x06006A94 RID: 27284 RVA: 0x0017C638 File Offset: 0x0017A838
			private void CreateControls(ChangePassword.ChangePasswordContainer container)
			{
				string uniqueID = this._owner.UniqueID;
				container.Title = new Literal();
				container.Instruction = new Literal();
				container.PasswordHintLabel = new Literal();
				TextBox textBox = new TextBox();
				textBox.ID = "UserName";
				container.UserNameTextBox = textBox;
				container.UserNameLabel = new LabelLiteral(textBox);
				bool flag = this._owner.CurrentView == ChangePassword.View.ChangePassword;
				container.UserNameRequired = this.CreateRequiredFieldValidator("UserNameRequired", textBox, uniqueID, flag);
				TextBox textBox2 = new TextBox();
				textBox2.ID = "CurrentPassword";
				textBox2.TextMode = TextBoxMode.Password;
				container.CurrentPasswordTextBox = textBox2;
				container.CurrentPasswordLabel = new LabelLiteral(textBox2);
				container.PasswordRequired = this.CreateRequiredFieldValidator("CurrentPasswordRequired", textBox2, uniqueID, flag);
				TextBox textBox3 = new TextBox();
				textBox3.ID = "NewPassword";
				textBox3.TextMode = TextBoxMode.Password;
				container.NewPasswordTextBox = textBox3;
				container.NewPasswordLabel = new LabelLiteral(textBox3);
				container.NewPasswordRequired = this.CreateRequiredFieldValidator("NewPasswordRequired", textBox3, uniqueID, flag);
				TextBox textBox4 = new TextBox();
				textBox4.ID = "ConfirmNewPassword";
				textBox4.TextMode = TextBoxMode.Password;
				container.ConfirmNewPasswordTextBox = textBox4;
				container.ConfirmNewPasswordLabel = new LabelLiteral(textBox4);
				container.ConfirmNewPasswordRequired = this.CreateRequiredFieldValidator("ConfirmNewPasswordRequired", textBox4, uniqueID, flag);
				container.NewPasswordCompareValidator = new CompareValidator
				{
					ID = "NewPasswordCompare",
					ValidationGroup = uniqueID,
					ControlToValidate = "ConfirmNewPassword",
					ControlToCompare = "NewPassword",
					Operator = ValidationCompareOperator.Equal,
					ErrorMessage = this._owner.ConfirmPasswordCompareErrorMessage,
					Display = ValidatorDisplay.Dynamic,
					Enabled = flag,
					Visible = flag
				};
				container.RegExpValidator = new RegularExpressionValidator
				{
					ID = "NewPasswordRegExp",
					ValidationGroup = uniqueID,
					ControlToValidate = "NewPassword",
					ErrorMessage = this._owner.NewPasswordRegularExpressionErrorMessage,
					ValidationExpression = this._owner.NewPasswordRegularExpression,
					Display = ValidatorDisplay.Dynamic,
					Enabled = flag,
					Visible = flag
				};
				container.ChangePasswordLinkButton = new LinkButton
				{
					ID = "ChangePasswordLinkButton",
					ValidationGroup = uniqueID,
					CommandName = ChangePassword.ChangePasswordButtonCommandName
				};
				container.CancelLinkButton = new LinkButton
				{
					ID = "CancelLinkButton",
					CausesValidation = false,
					CommandName = ChangePassword.CancelButtonCommandName
				};
				container.ChangePasswordImageButton = new ImageButton
				{
					ID = "ChangePasswordImageButton",
					ValidationGroup = uniqueID,
					CommandName = ChangePassword.ChangePasswordButtonCommandName
				};
				container.CancelImageButton = new ImageButton
				{
					ID = "CancelImageButton",
					CommandName = ChangePassword.CancelButtonCommandName,
					CausesValidation = false
				};
				container.ChangePasswordPushButton = new Button
				{
					ID = "ChangePasswordPushButton",
					ValidationGroup = uniqueID,
					CommandName = ChangePassword.ChangePasswordButtonCommandName
				};
				container.CancelPushButton = new Button
				{
					ID = "CancelPushButton",
					CommandName = ChangePassword.CancelButtonCommandName,
					CausesValidation = false
				};
				container.PasswordRecoveryIcon = new Image();
				container.PasswordRecoveryLink = new HyperLink();
				container.PasswordRecoveryLink.ID = "PasswordRecoveryLink";
				container.CreateUserIcon = new Image();
				container.CreateUserLink = new HyperLink();
				container.CreateUserLink.ID = "CreateUserLink";
				container.CreateUserLinkSeparator = new LiteralControl();
				container.HelpPageIcon = new Image();
				container.HelpPageLink = new HyperLink();
				container.HelpPageLink.ID = "HelpLink";
				container.HelpPageLinkSeparator = new LiteralControl();
				container.EditProfileLink = new HyperLink();
				container.EditProfileLink.ID = "EditProfileLink";
				container.EditProfileIcon = new Image();
				container.EditProfileLinkSeparator = new LiteralControl();
				container.FailureTextLabel = new Literal
				{
					ID = "FailureText"
				};
			}

			// Token: 0x06006A95 RID: 27285 RVA: 0x0017CA48 File Offset: 0x0017AC48
			private void LayoutControls(ChangePassword.ChangePasswordContainer container)
			{
				Table table = new Table();
				table.CellPadding = 0;
				TableRow tableRow = new LoginUtil.DisappearingTableRow();
				TableCell tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(container.Title);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(container.Instruction);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				if (this._owner.ConvertingToTemplate)
				{
					container.UserNameLabel.RenderAsLabel = true;
				}
				tableCell.Controls.Add(container.UserNameLabel);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(container.UserNameTextBox);
				tableCell.Controls.Add(container.UserNameRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				this._owner._userNameTableRow = tableRow;
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(container.CurrentPasswordLabel);
				if (this._owner.ConvertingToTemplate)
				{
					container.CurrentPasswordLabel.RenderAsLabel = true;
				}
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(container.CurrentPasswordTextBox);
				tableCell.Controls.Add(container.PasswordRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(container.NewPasswordLabel);
				if (this._owner.ConvertingToTemplate)
				{
					container.NewPasswordLabel.RenderAsLabel = true;
				}
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(container.NewPasswordTextBox);
				tableCell.Controls.Add(container.NewPasswordRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(container.PasswordHintLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				this._owner._passwordHintTableRow = tableRow;
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(container.ConfirmNewPasswordLabel);
				if (this._owner.ConvertingToTemplate)
				{
					container.ConfirmNewPasswordLabel.RenderAsLabel = true;
				}
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(container.ConfirmNewPasswordTextBox);
				tableCell.Controls.Add(container.ConfirmNewPasswordRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(container.NewPasswordCompareValidator);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				if (this._owner.RegExpEnabled)
				{
					tableRow = new LoginUtil.DisappearingTableRow();
					tableCell = new TableCell();
					tableCell.HorizontalAlign = HorizontalAlign.Center;
					tableCell.ColumnSpan = 2;
					tableCell.Controls.Add(container.RegExpValidator);
					tableRow.Cells.Add(tableCell);
					table.Rows.Add(tableRow);
				}
				this._owner.ValidatorRow = tableRow;
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(container.FailureTextLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(container.ChangePasswordLinkButton);
				tableCell.Controls.Add(container.ChangePasswordImageButton);
				tableCell.Controls.Add(container.ChangePasswordPushButton);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(container.CancelLinkButton);
				tableCell.Controls.Add(container.CancelImageButton);
				tableCell.Controls.Add(container.CancelPushButton);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(container.HelpPageIcon);
				tableCell.Controls.Add(container.HelpPageLink);
				tableCell.Controls.Add(container.HelpPageLinkSeparator);
				tableCell.Controls.Add(container.CreateUserIcon);
				tableCell.Controls.Add(container.CreateUserLink);
				container.HelpPageLinkSeparator.Text = "<br />";
				container.CreateUserLinkSeparator.Text = "<br />";
				container.EditProfileLinkSeparator.Text = "<br />";
				tableCell.Controls.Add(container.CreateUserLinkSeparator);
				tableCell.Controls.Add(container.PasswordRecoveryIcon);
				tableCell.Controls.Add(container.PasswordRecoveryLink);
				tableCell.Controls.Add(container.EditProfileLinkSeparator);
				tableCell.Controls.Add(container.EditProfileIcon);
				tableCell.Controls.Add(container.EditProfileLink);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				Table table2 = LoginUtil.CreateChildTable(this._owner.ConvertingToTemplate);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(table);
				tableRow.Cells.Add(tableCell);
				table2.Rows.Add(tableRow);
				container.LayoutTable = table;
				container.BorderTable = table2;
				container.Controls.Add(table2);
			}

			// Token: 0x06006A96 RID: 27286 RVA: 0x0017D084 File Offset: 0x0017B284
			void ITemplate.InstantiateIn(Control container)
			{
				ChangePassword.ChangePasswordContainer container2 = (ChangePassword.ChangePasswordContainer)container;
				this.CreateControls(container2);
				this.LayoutControls(container2);
			}

			// Token: 0x040038E1 RID: 14561
			private ChangePassword _owner;
		}

		// Token: 0x02000996 RID: 2454
		internal sealed class ChangePasswordContainer : LoginUtil.GenericContainer<ChangePassword>, INonBindingContainer, INamingContainer
		{
			// Token: 0x06006A97 RID: 27287 RVA: 0x0017C542 File Offset: 0x0017A742
			public ChangePasswordContainer(ChangePassword owner) : base(owner)
			{
			}

			// Token: 0x17001D4F RID: 7503
			// (get) Token: 0x06006A98 RID: 27288 RVA: 0x0017D0A6 File Offset: 0x0017B2A6
			// (set) Token: 0x06006A99 RID: 27289 RVA: 0x0017D0AE File Offset: 0x0017B2AE
			internal ImageButton CancelImageButton
			{
				get
				{
					return this._cancelImageButton;
				}
				set
				{
					this._cancelImageButton = value;
				}
			}

			// Token: 0x17001D50 RID: 7504
			// (get) Token: 0x06006A9A RID: 27290 RVA: 0x0017D0B7 File Offset: 0x0017B2B7
			// (set) Token: 0x06006A9B RID: 27291 RVA: 0x0017D0BF File Offset: 0x0017B2BF
			internal LinkButton CancelLinkButton
			{
				get
				{
					return this._cancelLinkButton;
				}
				set
				{
					this._cancelLinkButton = value;
				}
			}

			// Token: 0x17001D51 RID: 7505
			// (get) Token: 0x06006A9C RID: 27292 RVA: 0x0017D0C8 File Offset: 0x0017B2C8
			// (set) Token: 0x06006A9D RID: 27293 RVA: 0x0017D0D0 File Offset: 0x0017B2D0
			internal Button CancelPushButton
			{
				get
				{
					return this._cancelPushButton;
				}
				set
				{
					this._cancelPushButton = value;
				}
			}

			// Token: 0x17001D52 RID: 7506
			// (get) Token: 0x06006A9E RID: 27294 RVA: 0x0017D0D9 File Offset: 0x0017B2D9
			// (set) Token: 0x06006A9F RID: 27295 RVA: 0x0017D0E1 File Offset: 0x0017B2E1
			internal ImageButton ChangePasswordImageButton
			{
				get
				{
					return this._changePasswordImageButton;
				}
				set
				{
					this._changePasswordImageButton = value;
				}
			}

			// Token: 0x17001D53 RID: 7507
			// (get) Token: 0x06006AA0 RID: 27296 RVA: 0x0017D0EA File Offset: 0x0017B2EA
			// (set) Token: 0x06006AA1 RID: 27297 RVA: 0x0017D0F2 File Offset: 0x0017B2F2
			internal LinkButton ChangePasswordLinkButton
			{
				get
				{
					return this._changePasswordLinkButton;
				}
				set
				{
					this._changePasswordLinkButton = value;
				}
			}

			// Token: 0x17001D54 RID: 7508
			// (get) Token: 0x06006AA2 RID: 27298 RVA: 0x0017D0FB File Offset: 0x0017B2FB
			// (set) Token: 0x06006AA3 RID: 27299 RVA: 0x0017D103 File Offset: 0x0017B303
			internal Button ChangePasswordPushButton
			{
				get
				{
					return this._changePasswordPushButton;
				}
				set
				{
					this._changePasswordPushButton = value;
				}
			}

			// Token: 0x17001D55 RID: 7509
			// (get) Token: 0x06006AA4 RID: 27300 RVA: 0x0017D10C File Offset: 0x0017B30C
			// (set) Token: 0x06006AA5 RID: 27301 RVA: 0x0017D114 File Offset: 0x0017B314
			internal LabelLiteral ConfirmNewPasswordLabel
			{
				get
				{
					return this._confirmNewPasswordLabel;
				}
				set
				{
					this._confirmNewPasswordLabel = value;
				}
			}

			// Token: 0x17001D56 RID: 7510
			// (get) Token: 0x06006AA6 RID: 27302 RVA: 0x0017D11D File Offset: 0x0017B31D
			// (set) Token: 0x06006AA7 RID: 27303 RVA: 0x0017D125 File Offset: 0x0017B325
			internal RequiredFieldValidator ConfirmNewPasswordRequired
			{
				get
				{
					return this._confirmNewPasswordRequired;
				}
				set
				{
					this._confirmNewPasswordRequired = value;
				}
			}

			// Token: 0x17001D57 RID: 7511
			// (get) Token: 0x06006AA8 RID: 27304 RVA: 0x0017D12E File Offset: 0x0017B32E
			// (set) Token: 0x06006AA9 RID: 27305 RVA: 0x0017D14A File Offset: 0x0017B34A
			internal Control ConfirmNewPasswordTextBox
			{
				get
				{
					if (this._confirmNewPasswordTextBox != null)
					{
						return this._confirmNewPasswordTextBox;
					}
					return base.FindOptionalControl<IEditableTextControl>("ConfirmNewPassword");
				}
				set
				{
					this._confirmNewPasswordTextBox = value;
				}
			}

			// Token: 0x17001D58 RID: 7512
			// (get) Token: 0x06006AAA RID: 27306 RVA: 0x0017C57E File Offset: 0x0017A77E
			protected override bool ConvertingToTemplate
			{
				get
				{
					return base.Owner.ConvertingToTemplate;
				}
			}

			// Token: 0x17001D59 RID: 7513
			// (get) Token: 0x06006AAB RID: 27307 RVA: 0x0017D153 File Offset: 0x0017B353
			// (set) Token: 0x06006AAC RID: 27308 RVA: 0x0017D15B File Offset: 0x0017B35B
			internal Image CreateUserIcon
			{
				get
				{
					return this._createUserIcon;
				}
				set
				{
					this._createUserIcon = value;
				}
			}

			// Token: 0x17001D5A RID: 7514
			// (get) Token: 0x06006AAD RID: 27309 RVA: 0x0017D164 File Offset: 0x0017B364
			// (set) Token: 0x06006AAE RID: 27310 RVA: 0x0017D16C File Offset: 0x0017B36C
			internal HyperLink CreateUserLink
			{
				get
				{
					return this._createUserLink;
				}
				set
				{
					this._createUserLink = value;
				}
			}

			// Token: 0x17001D5B RID: 7515
			// (get) Token: 0x06006AAF RID: 27311 RVA: 0x0017D175 File Offset: 0x0017B375
			// (set) Token: 0x06006AB0 RID: 27312 RVA: 0x0017D17D File Offset: 0x0017B37D
			internal LiteralControl CreateUserLinkSeparator
			{
				get
				{
					return this._createUserLinkSeparator;
				}
				set
				{
					this._createUserLinkSeparator = value;
				}
			}

			// Token: 0x17001D5C RID: 7516
			// (get) Token: 0x06006AB1 RID: 27313 RVA: 0x0017D186 File Offset: 0x0017B386
			// (set) Token: 0x06006AB2 RID: 27314 RVA: 0x0017D18E File Offset: 0x0017B38E
			internal LabelLiteral CurrentPasswordLabel
			{
				get
				{
					return this._currentPasswordLabel;
				}
				set
				{
					this._currentPasswordLabel = value;
				}
			}

			// Token: 0x17001D5D RID: 7517
			// (get) Token: 0x06006AB3 RID: 27315 RVA: 0x0017D197 File Offset: 0x0017B397
			// (set) Token: 0x06006AB4 RID: 27316 RVA: 0x0017D1B8 File Offset: 0x0017B3B8
			internal Control CurrentPasswordTextBox
			{
				get
				{
					if (this._currentPasswordTextBox != null)
					{
						return this._currentPasswordTextBox;
					}
					return base.FindRequiredControl<IEditableTextControl>("CurrentPassword", "ChangePassword_NoCurrentPasswordTextBox");
				}
				set
				{
					this._currentPasswordTextBox = value;
				}
			}

			// Token: 0x17001D5E RID: 7518
			// (get) Token: 0x06006AB5 RID: 27317 RVA: 0x0017D1C1 File Offset: 0x0017B3C1
			// (set) Token: 0x06006AB6 RID: 27318 RVA: 0x0017D1C9 File Offset: 0x0017B3C9
			internal Image EditProfileIcon
			{
				get
				{
					return this._editProfileIcon;
				}
				set
				{
					this._editProfileIcon = value;
				}
			}

			// Token: 0x17001D5F RID: 7519
			// (get) Token: 0x06006AB7 RID: 27319 RVA: 0x0017D1D2 File Offset: 0x0017B3D2
			// (set) Token: 0x06006AB8 RID: 27320 RVA: 0x0017D1DA File Offset: 0x0017B3DA
			internal HyperLink EditProfileLink
			{
				get
				{
					return this._editProfileLink;
				}
				set
				{
					this._editProfileLink = value;
				}
			}

			// Token: 0x17001D60 RID: 7520
			// (get) Token: 0x06006AB9 RID: 27321 RVA: 0x0017D1E3 File Offset: 0x0017B3E3
			// (set) Token: 0x06006ABA RID: 27322 RVA: 0x0017D1EB File Offset: 0x0017B3EB
			internal LiteralControl EditProfileLinkSeparator
			{
				get
				{
					return this._editProfileLinkSeparator;
				}
				set
				{
					this._editProfileLinkSeparator = value;
				}
			}

			// Token: 0x17001D61 RID: 7521
			// (get) Token: 0x06006ABB RID: 27323 RVA: 0x0017D1F4 File Offset: 0x0017B3F4
			// (set) Token: 0x06006ABC RID: 27324 RVA: 0x0017D210 File Offset: 0x0017B410
			internal Control FailureTextLabel
			{
				get
				{
					if (this._failureTextLabel != null)
					{
						return this._failureTextLabel;
					}
					return base.FindOptionalControl<ITextControl>("FailureText");
				}
				set
				{
					this._failureTextLabel = value;
				}
			}

			// Token: 0x17001D62 RID: 7522
			// (get) Token: 0x06006ABD RID: 27325 RVA: 0x0017D219 File Offset: 0x0017B419
			// (set) Token: 0x06006ABE RID: 27326 RVA: 0x0017D221 File Offset: 0x0017B421
			internal Image HelpPageIcon
			{
				get
				{
					return this._helpPageIcon;
				}
				set
				{
					this._helpPageIcon = value;
				}
			}

			// Token: 0x17001D63 RID: 7523
			// (get) Token: 0x06006ABF RID: 27327 RVA: 0x0017D22A File Offset: 0x0017B42A
			// (set) Token: 0x06006AC0 RID: 27328 RVA: 0x0017D232 File Offset: 0x0017B432
			internal HyperLink HelpPageLink
			{
				get
				{
					return this._helpPageLink;
				}
				set
				{
					this._helpPageLink = value;
				}
			}

			// Token: 0x17001D64 RID: 7524
			// (get) Token: 0x06006AC1 RID: 27329 RVA: 0x0017D23B File Offset: 0x0017B43B
			// (set) Token: 0x06006AC2 RID: 27330 RVA: 0x0017D243 File Offset: 0x0017B443
			internal LiteralControl HelpPageLinkSeparator
			{
				get
				{
					return this._helpPageLinkSeparator;
				}
				set
				{
					this._helpPageLinkSeparator = value;
				}
			}

			// Token: 0x17001D65 RID: 7525
			// (get) Token: 0x06006AC3 RID: 27331 RVA: 0x0017D24C File Offset: 0x0017B44C
			// (set) Token: 0x06006AC4 RID: 27332 RVA: 0x0017D254 File Offset: 0x0017B454
			internal Literal Instruction
			{
				get
				{
					return this._instruction;
				}
				set
				{
					this._instruction = value;
				}
			}

			// Token: 0x17001D66 RID: 7526
			// (get) Token: 0x06006AC5 RID: 27333 RVA: 0x0017D25D File Offset: 0x0017B45D
			// (set) Token: 0x06006AC6 RID: 27334 RVA: 0x0017D265 File Offset: 0x0017B465
			internal CompareValidator NewPasswordCompareValidator
			{
				get
				{
					return this._newPasswordCompareValidator;
				}
				set
				{
					this._newPasswordCompareValidator = value;
				}
			}

			// Token: 0x17001D67 RID: 7527
			// (get) Token: 0x06006AC7 RID: 27335 RVA: 0x0017D26E File Offset: 0x0017B46E
			// (set) Token: 0x06006AC8 RID: 27336 RVA: 0x0017D276 File Offset: 0x0017B476
			internal LabelLiteral NewPasswordLabel
			{
				get
				{
					return this._newPasswordLabel;
				}
				set
				{
					this._newPasswordLabel = value;
				}
			}

			// Token: 0x17001D68 RID: 7528
			// (get) Token: 0x06006AC9 RID: 27337 RVA: 0x0017D27F File Offset: 0x0017B47F
			// (set) Token: 0x06006ACA RID: 27338 RVA: 0x0017D287 File Offset: 0x0017B487
			internal RequiredFieldValidator NewPasswordRequired
			{
				get
				{
					return this._newPasswordRequired;
				}
				set
				{
					this._newPasswordRequired = value;
				}
			}

			// Token: 0x17001D69 RID: 7529
			// (get) Token: 0x06006ACB RID: 27339 RVA: 0x0017D290 File Offset: 0x0017B490
			// (set) Token: 0x06006ACC RID: 27340 RVA: 0x0017D2B1 File Offset: 0x0017B4B1
			internal Control NewPasswordTextBox
			{
				get
				{
					if (this._newPasswordTextBox != null)
					{
						return this._newPasswordTextBox;
					}
					return base.FindRequiredControl<IEditableTextControl>("NewPassword", "ChangePassword_NoNewPasswordTextBox");
				}
				set
				{
					this._newPasswordTextBox = value;
				}
			}

			// Token: 0x17001D6A RID: 7530
			// (get) Token: 0x06006ACD RID: 27341 RVA: 0x0017D2BA File Offset: 0x0017B4BA
			// (set) Token: 0x06006ACE RID: 27342 RVA: 0x0017D2C2 File Offset: 0x0017B4C2
			internal Literal PasswordHintLabel
			{
				get
				{
					return this._passwordHintLabel;
				}
				set
				{
					this._passwordHintLabel = value;
				}
			}

			// Token: 0x17001D6B RID: 7531
			// (get) Token: 0x06006ACF RID: 27343 RVA: 0x0017D2CB File Offset: 0x0017B4CB
			// (set) Token: 0x06006AD0 RID: 27344 RVA: 0x0017D2D3 File Offset: 0x0017B4D3
			internal Image PasswordRecoveryIcon
			{
				get
				{
					return this._passwordRecoveryIcon;
				}
				set
				{
					this._passwordRecoveryIcon = value;
				}
			}

			// Token: 0x17001D6C RID: 7532
			// (get) Token: 0x06006AD1 RID: 27345 RVA: 0x0017D2DC File Offset: 0x0017B4DC
			// (set) Token: 0x06006AD2 RID: 27346 RVA: 0x0017D2E4 File Offset: 0x0017B4E4
			internal HyperLink PasswordRecoveryLink
			{
				get
				{
					return this._passwordRecoveryLink;
				}
				set
				{
					this._passwordRecoveryLink = value;
				}
			}

			// Token: 0x17001D6D RID: 7533
			// (get) Token: 0x06006AD3 RID: 27347 RVA: 0x0017D2ED File Offset: 0x0017B4ED
			// (set) Token: 0x06006AD4 RID: 27348 RVA: 0x0017D2F5 File Offset: 0x0017B4F5
			internal RequiredFieldValidator PasswordRequired
			{
				get
				{
					return this._passwordRequired;
				}
				set
				{
					this._passwordRequired = value;
				}
			}

			// Token: 0x17001D6E RID: 7534
			// (get) Token: 0x06006AD5 RID: 27349 RVA: 0x0017D2FE File Offset: 0x0017B4FE
			// (set) Token: 0x06006AD6 RID: 27350 RVA: 0x0017D306 File Offset: 0x0017B506
			internal RegularExpressionValidator RegExpValidator
			{
				get
				{
					return this._regExpValidator;
				}
				set
				{
					this._regExpValidator = value;
				}
			}

			// Token: 0x17001D6F RID: 7535
			// (get) Token: 0x06006AD7 RID: 27351 RVA: 0x0017D30F File Offset: 0x0017B50F
			// (set) Token: 0x06006AD8 RID: 27352 RVA: 0x0017D317 File Offset: 0x0017B517
			internal Literal Title
			{
				get
				{
					return this._title;
				}
				set
				{
					this._title = value;
				}
			}

			// Token: 0x17001D70 RID: 7536
			// (get) Token: 0x06006AD9 RID: 27353 RVA: 0x0017D320 File Offset: 0x0017B520
			// (set) Token: 0x06006ADA RID: 27354 RVA: 0x0017D328 File Offset: 0x0017B528
			internal LabelLiteral UserNameLabel
			{
				get
				{
					return this._userNameLabel;
				}
				set
				{
					this._userNameLabel = value;
				}
			}

			// Token: 0x17001D71 RID: 7537
			// (get) Token: 0x06006ADB RID: 27355 RVA: 0x0017D331 File Offset: 0x0017B531
			// (set) Token: 0x06006ADC RID: 27356 RVA: 0x0017D339 File Offset: 0x0017B539
			internal RequiredFieldValidator UserNameRequired
			{
				get
				{
					return this._userNameRequired;
				}
				set
				{
					this._userNameRequired = value;
				}
			}

			// Token: 0x17001D72 RID: 7538
			// (get) Token: 0x06006ADD RID: 27357 RVA: 0x0017D342 File Offset: 0x0017B542
			// (set) Token: 0x06006ADE RID: 27358 RVA: 0x0017D382 File Offset: 0x0017B582
			internal Control UserNameTextBox
			{
				get
				{
					if (this._userNameTextBox != null)
					{
						return this._userNameTextBox;
					}
					if (base.Owner.DisplayUserName)
					{
						return base.FindRequiredControl<IEditableTextControl>("UserName", "ChangePassword_NoUserNameTextBox");
					}
					base.VerifyControlNotPresent<IEditableTextControl>("UserName", "ChangePassword_UserNameTextBoxNotAllowed");
					return null;
				}
				set
				{
					this._userNameTextBox = value;
				}
			}

			// Token: 0x040038E2 RID: 14562
			private LiteralControl _createUserLinkSeparator;

			// Token: 0x040038E3 RID: 14563
			private LiteralControl _helpPageLinkSeparator;

			// Token: 0x040038E4 RID: 14564
			private LiteralControl _editProfileLinkSeparator;

			// Token: 0x040038E5 RID: 14565
			private Control _failureTextLabel;

			// Token: 0x040038E6 RID: 14566
			private ImageButton _changePasswordImageButton;

			// Token: 0x040038E7 RID: 14567
			private LinkButton _changePasswordLinkButton;

			// Token: 0x040038E8 RID: 14568
			private Button _changePasswordPushButton;

			// Token: 0x040038E9 RID: 14569
			private ImageButton _cancelImageButton;

			// Token: 0x040038EA RID: 14570
			private LinkButton _cancelLinkButton;

			// Token: 0x040038EB RID: 14571
			private Button _cancelPushButton;

			// Token: 0x040038EC RID: 14572
			private Image _createUserIcon;

			// Token: 0x040038ED RID: 14573
			private Image _helpPageIcon;

			// Token: 0x040038EE RID: 14574
			private Image _passwordRecoveryIcon;

			// Token: 0x040038EF RID: 14575
			private Image _editProfileIcon;

			// Token: 0x040038F0 RID: 14576
			private RequiredFieldValidator _passwordRequired;

			// Token: 0x040038F1 RID: 14577
			private RequiredFieldValidator _userNameRequired;

			// Token: 0x040038F2 RID: 14578
			private RequiredFieldValidator _confirmNewPasswordRequired;

			// Token: 0x040038F3 RID: 14579
			private RequiredFieldValidator _newPasswordRequired;

			// Token: 0x040038F4 RID: 14580
			private CompareValidator _newPasswordCompareValidator;

			// Token: 0x040038F5 RID: 14581
			private RegularExpressionValidator _regExpValidator;

			// Token: 0x040038F6 RID: 14582
			private Literal _title;

			// Token: 0x040038F7 RID: 14583
			private Literal _instruction;

			// Token: 0x040038F8 RID: 14584
			private LabelLiteral _userNameLabel;

			// Token: 0x040038F9 RID: 14585
			private LabelLiteral _currentPasswordLabel;

			// Token: 0x040038FA RID: 14586
			private LabelLiteral _newPasswordLabel;

			// Token: 0x040038FB RID: 14587
			private LabelLiteral _confirmNewPasswordLabel;

			// Token: 0x040038FC RID: 14588
			private Literal _passwordHintLabel;

			// Token: 0x040038FD RID: 14589
			private Control _userNameTextBox;

			// Token: 0x040038FE RID: 14590
			private Control _currentPasswordTextBox;

			// Token: 0x040038FF RID: 14591
			private Control _newPasswordTextBox;

			// Token: 0x04003900 RID: 14592
			private Control _confirmNewPasswordTextBox;

			// Token: 0x04003901 RID: 14593
			private HyperLink _helpPageLink;

			// Token: 0x04003902 RID: 14594
			private HyperLink _passwordRecoveryLink;

			// Token: 0x04003903 RID: 14595
			private HyperLink _createUserLink;

			// Token: 0x04003904 RID: 14596
			private HyperLink _editProfileLink;
		}

		// Token: 0x02000997 RID: 2455
		internal enum View
		{
			// Token: 0x04003906 RID: 14598
			ChangePassword,
			// Token: 0x04003907 RID: 14599
			Success
		}
	}
}
