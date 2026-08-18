using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200045E RID: 1118
	[Bindable(false)]
	[DefaultEvent("Authenticate")]
	[Designer("System.Web.UI.Design.WebControls.LoginDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class Login : CompositeControl, IBorderPaddingControl, IRenderOuterTableControl
	{
		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x000AEC30 File Offset: 0x000ACE30
		// (set) Token: 0x06003607 RID: 13831 RVA: 0x000AEC59 File Offset: 0x000ACE59
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
					throw new ArgumentOutOfRangeException("value", SR.GetString("Login_InvalidBorderPadding"));
				}
				this.ViewState["BorderPadding"] = value;
			}
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06003608 RID: 13832 RVA: 0x000AEC8A File Offset: 0x000ACE8A
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Login_CheckBoxStyle")]
		public TableItemStyle CheckBoxStyle
		{
			get
			{
				if (this._checkBoxStyle == null)
				{
					this._checkBoxStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._checkBoxStyle).TrackViewState();
					}
				}
				return this._checkBoxStyle;
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06003609 RID: 13833 RVA: 0x000AECB8 File Offset: 0x000ACEB8
		// (set) Token: 0x0600360A RID: 13834 RVA: 0x0008B4E5 File Offset: 0x000896E5
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

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000AECE5 File Offset: 0x000ACEE5
		private bool ConvertingToTemplate
		{
			get
			{
				return base.DesignMode && this._convertingToTemplate;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x0600360C RID: 13836 RVA: 0x000AECF8 File Offset: 0x000ACEF8
		// (set) Token: 0x0600360D RID: 13837 RVA: 0x0008B525 File Offset: 0x00089725
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("Login_CreateUserUrl")]
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

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x0600360E RID: 13838 RVA: 0x000AED28 File Offset: 0x000ACF28
		// (set) Token: 0x0600360F RID: 13839 RVA: 0x000AED55 File Offset: 0x000ACF55
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("Login_DestinationPageUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
		[UrlProperty]
		public virtual string DestinationPageUrl
		{
			get
			{
				object obj = this.ViewState["DestinationPageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["DestinationPageUrl"] = value;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06003610 RID: 13840 RVA: 0x000AED68 File Offset: 0x000ACF68
		// (set) Token: 0x06003611 RID: 13841 RVA: 0x000AED91 File Offset: 0x000ACF91
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[Themeable(false)]
		[WebSysDescription("Login_DisplayRememberMe")]
		public virtual bool DisplayRememberMe
		{
			get
			{
				object obj = this.ViewState["DisplayRememberMe"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["DisplayRememberMe"] = value;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x000AEDAC File Offset: 0x000ACFAC
		// (set) Token: 0x06003613 RID: 13843 RVA: 0x0008B76D File Offset: 0x0008996D
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

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06003614 RID: 13844 RVA: 0x000AEDDC File Offset: 0x000ACFDC
		// (set) Token: 0x06003615 RID: 13845 RVA: 0x0008B7AD File Offset: 0x000899AD
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

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x000AEE0C File Offset: 0x000AD00C
		// (set) Token: 0x06003617 RID: 13847 RVA: 0x0008B4A5 File Offset: 0x000896A5
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("Login_CreateUserIconUrl")]
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

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x000AEE3C File Offset: 0x000AD03C
		// (set) Token: 0x06003619 RID: 13849 RVA: 0x0008B72D File Offset: 0x0008992D
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("Login_HelpPageIconUrl")]
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

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x000AEE69 File Offset: 0x000AD069
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

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x0600361B RID: 13851 RVA: 0x000AEE98 File Offset: 0x000AD098
		// (set) Token: 0x0600361C RID: 13852 RVA: 0x0008B81D File Offset: 0x00089A1D
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

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x0600361D RID: 13853 RVA: 0x000AEEC5 File Offset: 0x000AD0C5
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

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x000AEEF3 File Offset: 0x000AD0F3
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

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x0600361F RID: 13855 RVA: 0x000AEF21 File Offset: 0x000AD121
		// (set) Token: 0x06003620 RID: 13856 RVA: 0x000AEF29 File Offset: 0x000AD129
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Login))]
		public virtual ITemplate LayoutTemplate
		{
			get
			{
				return this._loginTemplate;
			}
			set
			{
				this._loginTemplate = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06003621 RID: 13857 RVA: 0x000AEF3C File Offset: 0x000AD13C
		// (set) Token: 0x06003622 RID: 13858 RVA: 0x000AEF65 File Offset: 0x000AD165
		[WebCategory("Behavior")]
		[DefaultValue(LoginFailureAction.Refresh)]
		[Themeable(false)]
		[WebSysDescription("Login_FailureAction")]
		public virtual LoginFailureAction FailureAction
		{
			get
			{
				object obj = this.ViewState["FailureAction"];
				if (obj != null)
				{
					return (LoginFailureAction)obj;
				}
				return LoginFailureAction.Refresh;
			}
			set
			{
				if (value < LoginFailureAction.Refresh || value > LoginFailureAction.RedirectToLoginPage)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["FailureAction"] = value;
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06003623 RID: 13859 RVA: 0x000AEF90 File Offset: 0x000AD190
		// (set) Token: 0x06003624 RID: 13860 RVA: 0x000AEFC2 File Offset: 0x000AD1C2
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Login_DefaultFailureText")]
		[WebSysDescription("Login_FailureText")]
		public virtual string FailureText
		{
			get
			{
				object obj = this.ViewState["FailureText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("Login_DefaultFailureText");
			}
			set
			{
				this.ViewState["FailureText"] = value;
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x000AEFD5 File Offset: 0x000AD1D5
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

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000AF004 File Offset: 0x000AD204
		// (set) Token: 0x06003627 RID: 13863 RVA: 0x000AF031 File Offset: 0x000AD231
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("Login_LoginButtonImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string LoginButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["LoginButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["LoginButtonImageUrl"] = value;
			}
		}

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000AF044 File Offset: 0x000AD244
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Login_LoginButtonStyle")]
		public Style LoginButtonStyle
		{
			get
			{
				if (this._loginButtonStyle == null)
				{
					this._loginButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._loginButtonStyle).TrackViewState();
					}
				}
				return this._loginButtonStyle;
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x000AF074 File Offset: 0x000AD274
		// (set) Token: 0x0600362A RID: 13866 RVA: 0x000AF0A6 File Offset: 0x000AD2A6
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Login_DefaultLoginButtonText")]
		[WebSysDescription("Login_LoginButtonText")]
		public virtual string LoginButtonText
		{
			get
			{
				object obj = this.ViewState["LoginButtonText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("Login_DefaultLoginButtonText");
			}
			set
			{
				this.ViewState["LoginButtonText"] = value;
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x0600362B RID: 13867 RVA: 0x000AF0BC File Offset: 0x000AD2BC
		// (set) Token: 0x0600362C RID: 13868 RVA: 0x000AF0E5 File Offset: 0x000AD2E5
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("Login_LoginButtonType")]
		public virtual ButtonType LoginButtonType
		{
			get
			{
				object obj = this.ViewState["LoginButtonType"];
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
				this.ViewState["LoginButtonType"] = value;
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x0600362D RID: 13869 RVA: 0x000AF110 File Offset: 0x000AD310
		// (set) Token: 0x0600362E RID: 13870 RVA: 0x000AF139 File Offset: 0x000AD339
		[DefaultValue(Orientation.Vertical)]
		[WebCategory("Layout")]
		[WebSysDescription("Login_Orientation")]
		public virtual Orientation Orientation
		{
			get
			{
				object obj = this.ViewState["Orientation"];
				if (obj != null)
				{
					return (Orientation)obj;
				}
				return Orientation.Vertical;
			}
			set
			{
				if (value < Orientation.Horizontal || value > Orientation.Vertical)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Orientation"] = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x0600362F RID: 13871 RVA: 0x000AF16C File Offset: 0x000AD36C
		// (set) Token: 0x06003630 RID: 13872 RVA: 0x0008B8B9 File Offset: 0x00089AB9
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

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x000AF199 File Offset: 0x000AD399
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Password
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

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x000AF1B0 File Offset: 0x000AD3B0
		private string PasswordInternal
		{
			get
			{
				string password = this.Password;
				if (string.IsNullOrEmpty(password) && this._templateContainer != null)
				{
					ITextControl textControl = (ITextControl)this._templateContainer.PasswordTextBox;
					if (textControl != null && textControl.Text != null)
					{
						return textControl.Text;
					}
				}
				return password;
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x000AF1F8 File Offset: 0x000AD3F8
		// (set) Token: 0x06003634 RID: 13876 RVA: 0x0008BAEE File Offset: 0x00089CEE
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

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x000AF22C File Offset: 0x000AD42C
		// (set) Token: 0x06003636 RID: 13878 RVA: 0x0008BB71 File Offset: 0x00089D71
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

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000AF25C File Offset: 0x000AD45C
		// (set) Token: 0x06003638 RID: 13880 RVA: 0x0008BBB1 File Offset: 0x00089DB1
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("Login_PasswordRecoveryUrl")]
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

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000AF28C File Offset: 0x000AD48C
		// (set) Token: 0x0600363A RID: 13882 RVA: 0x0008BB31 File Offset: 0x00089D31
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("Login_PasswordRecoveryIconUrl")]
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

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x000AF2BC File Offset: 0x000AD4BC
		// (set) Token: 0x0600363C RID: 13884 RVA: 0x0008BBF6 File Offset: 0x00089DF6
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("Login_DefaultPasswordRequiredErrorMessage")]
		[WebSysDescription("Login_PasswordRequiredErrorMessage")]
		public virtual string PasswordRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["PasswordRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("Login_DefaultPasswordRequiredErrorMessage");
			}
			set
			{
				this.ViewState["PasswordRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x000AF2F0 File Offset: 0x000AD4F0
		// (set) Token: 0x0600363E RID: 13886 RVA: 0x000AF319 File Offset: 0x000AD519
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[Themeable(false)]
		[WebSysDescription("Login_RememberMeSet")]
		public virtual bool RememberMeSet
		{
			get
			{
				object obj = this.ViewState["RememberMeSet"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["RememberMeSet"] = value;
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x0600363F RID: 13887 RVA: 0x000AF334 File Offset: 0x000AD534
		// (set) Token: 0x06003640 RID: 13888 RVA: 0x000AF366 File Offset: 0x000AD566
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Login_DefaultRememberMeText")]
		[WebSysDescription("Login_RememberMeText")]
		public virtual string RememberMeText
		{
			get
			{
				object obj = this.ViewState["RememberMeText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("Login_DefaultRememberMeText");
			}
			set
			{
				this.ViewState["RememberMeText"] = value;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06003641 RID: 13889 RVA: 0x000AF37C File Offset: 0x000AD57C
		// (set) Token: 0x06003642 RID: 13890 RVA: 0x0008BC71 File Offset: 0x00089E71
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

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06003643 RID: 13891 RVA: 0x0008BDAD File Offset: 0x00089FAD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06003644 RID: 13892 RVA: 0x000AF3A5 File Offset: 0x000AD5A5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private Login.LoginContainer TemplateContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._templateContainer;
			}
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06003645 RID: 13893 RVA: 0x000AF3B3 File Offset: 0x000AD5B3
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

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06003646 RID: 13894 RVA: 0x000AF3E4 File Offset: 0x000AD5E4
		// (set) Token: 0x06003647 RID: 13895 RVA: 0x000AF40D File Offset: 0x000AD60D
		[WebCategory("Layout")]
		[DefaultValue(LoginTextLayout.TextOnLeft)]
		[WebSysDescription("LoginControls_TextLayout")]
		public virtual LoginTextLayout TextLayout
		{
			get
			{
				object obj = this.ViewState["TextLayout"];
				if (obj != null)
				{
					return (LoginTextLayout)obj;
				}
				return LoginTextLayout.TextOnLeft;
			}
			set
			{
				if (value < LoginTextLayout.TextOnLeft || value > LoginTextLayout.TextOnTop)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["TextLayout"] = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06003648 RID: 13896 RVA: 0x000AF440 File Offset: 0x000AD640
		// (set) Token: 0x06003649 RID: 13897 RVA: 0x000AF472 File Offset: 0x000AD672
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Login_DefaultTitleText")]
		[WebSysDescription("LoginControls_TitleText")]
		public virtual string TitleText
		{
			get
			{
				object obj = this.ViewState["TitleText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("Login_DefaultTitleText");
			}
			set
			{
				this.ViewState["TitleText"] = value;
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x0600364A RID: 13898 RVA: 0x000AF485 File Offset: 0x000AD685
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

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x000AF4B4 File Offset: 0x000AD6B4
		// (set) Token: 0x0600364C RID: 13900 RVA: 0x00092191 File Offset: 0x00090391
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("UserName_InitialValue")]
		public virtual string UserName
		{
			get
			{
				object obj = this.ViewState["UserName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["UserName"] = value;
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x0600364D RID: 13901 RVA: 0x000AF4E4 File Offset: 0x000AD6E4
		private string UserNameInternal
		{
			get
			{
				string userName = this.UserName;
				if (string.IsNullOrEmpty(userName) && this._templateContainer != null)
				{
					ITextControl textControl = (ITextControl)this._templateContainer.UserNameTextBox;
					if (textControl != null && textControl.Text != null)
					{
						return textControl.Text;
					}
				}
				return userName;
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x0600364E RID: 13902 RVA: 0x000AF52C File Offset: 0x000AD72C
		// (set) Token: 0x0600364F RID: 13903 RVA: 0x0008BEA6 File Offset: 0x0008A0A6
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Login_DefaultUserNameLabelText")]
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
				return SR.GetString("Login_DefaultUserNameLabelText");
			}
			set
			{
				this.ViewState["UserNameLabelText"] = value;
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06003650 RID: 13904 RVA: 0x000AF560 File Offset: 0x000AD760
		// (set) Token: 0x06003651 RID: 13905 RVA: 0x0008BEEE File Offset: 0x0008A0EE
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("Login_DefaultUserNameRequiredErrorMessage")]
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
				return SR.GetString("Login_DefaultUserNameRequiredErrorMessage");
			}
			set
			{
				this.ViewState["UserNameRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06003652 RID: 13906 RVA: 0x000AF592 File Offset: 0x000AD792
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Login_ValidatorTextStyle")]
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

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06003653 RID: 13907 RVA: 0x000AF5C0 File Offset: 0x000AD7C0
		// (set) Token: 0x06003654 RID: 13908 RVA: 0x000AF5E9 File Offset: 0x000AD7E9
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[Themeable(false)]
		[WebSysDescription("Login_VisibleWhenLoggedIn")]
		public virtual bool VisibleWhenLoggedIn
		{
			get
			{
				object obj = this.ViewState["VisibleWhenLoggedIn"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["VisibleWhenLoggedIn"] = value;
			}
		}

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06003655 RID: 13909 RVA: 0x000AF601 File Offset: 0x000AD801
		// (remove) Token: 0x06003656 RID: 13910 RVA: 0x000AF614 File Offset: 0x000AD814
		[WebCategory("Action")]
		[WebSysDescription("Login_LoggedIn")]
		public event EventHandler LoggedIn
		{
			add
			{
				base.Events.AddHandler(Login.EventLoggedIn, value);
			}
			remove
			{
				base.Events.RemoveHandler(Login.EventLoggedIn, value);
			}
		}

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06003657 RID: 13911 RVA: 0x000AF627 File Offset: 0x000AD827
		// (remove) Token: 0x06003658 RID: 13912 RVA: 0x000AF63A File Offset: 0x000AD83A
		[WebCategory("Action")]
		[WebSysDescription("Login_Authenticate")]
		public event AuthenticateEventHandler Authenticate
		{
			add
			{
				base.Events.AddHandler(Login.EventAuthenticate, value);
			}
			remove
			{
				base.Events.RemoveHandler(Login.EventAuthenticate, value);
			}
		}

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x06003659 RID: 13913 RVA: 0x000AF64D File Offset: 0x000AD84D
		// (remove) Token: 0x0600365A RID: 13914 RVA: 0x000AF660 File Offset: 0x000AD860
		[WebCategory("Action")]
		[WebSysDescription("Login_LoggingIn")]
		public event LoginCancelEventHandler LoggingIn
		{
			add
			{
				base.Events.AddHandler(Login.EventLoggingIn, value);
			}
			remove
			{
				base.Events.RemoveHandler(Login.EventLoggingIn, value);
			}
		}

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x0600365B RID: 13915 RVA: 0x000AF673 File Offset: 0x000AD873
		// (remove) Token: 0x0600365C RID: 13916 RVA: 0x000AF686 File Offset: 0x000AD886
		[WebCategory("Action")]
		[WebSysDescription("Login_LoginError")]
		public event EventHandler LoginError
		{
			add
			{
				base.Events.AddHandler(Login.EventLoginError, value);
			}
			remove
			{
				base.Events.RemoveHandler(Login.EventLoginError, value);
			}
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x000AF69C File Offset: 0x000AD89C
		private void AttemptLogin()
		{
			if (this.Page != null && !this.Page.IsValid)
			{
				return;
			}
			LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
			this.OnLoggingIn(loginCancelEventArgs);
			if (loginCancelEventArgs.Cancel)
			{
				return;
			}
			AuthenticateEventArgs authenticateEventArgs = new AuthenticateEventArgs();
			this.OnAuthenticate(authenticateEventArgs);
			if (authenticateEventArgs.Authenticated)
			{
				FormsAuthentication.SetAuthCookie(this.UserNameInternal, this.RememberMeSet);
				this.OnLoggedIn(EventArgs.Empty);
				this.Page.Response.Redirect(this.GetRedirectUrl(), false);
				return;
			}
			this.OnLoginError(EventArgs.Empty);
			if (this.FailureAction == LoginFailureAction.RedirectToLoginPage)
			{
				FormsAuthentication.RedirectToLoginPage("loginfailure=1");
			}
			ITextControl textControl = (ITextControl)this.TemplateContainer.FailureTextLabel;
			if (textControl != null)
			{
				textControl.Text = this.FailureText;
			}
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000AF75C File Offset: 0x000AD95C
		private void AuthenticateUsingMembershipProvider(AuthenticateEventArgs e)
		{
			MembershipProvider provider = LoginUtil.GetProvider(this.MembershipProvider);
			e.Authenticated = provider.ValidateUser(this.UserNameInternal, this.PasswordInternal);
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000AF790 File Offset: 0x000AD990
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			this._templateContainer = new Login.LoginContainer(this);
			this._templateContainer.RenderDesignerRegion = this._renderDesignerRegion;
			ITemplate template = this.LayoutTemplate;
			if (template == null)
			{
				this._templateContainer.EnableViewState = false;
				this._templateContainer.EnableTheming = false;
				template = new Login.LoginTemplate(this);
			}
			template.InstantiateIn(this._templateContainer);
			this._templateContainer.Visible = true;
			this.Controls.Add(this._templateContainer);
			this.SetEditableChildProperties();
			IEditableTextControl editableTextControl = this._templateContainer.UserNameTextBox as IEditableTextControl;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += this.UserNameTextChanged;
			}
			IEditableTextControl editableTextControl2 = this._templateContainer.PasswordTextBox as IEditableTextControl;
			if (editableTextControl2 != null)
			{
				editableTextControl2.TextChanged += this.PasswordTextChanged;
			}
			ICheckBoxControl checkBoxControl = (ICheckBoxControl)this._templateContainer.RememberMeCheckBox;
			if (checkBoxControl != null)
			{
				checkBoxControl.CheckedChanged += this.RememberMeCheckedChanged;
			}
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000AF890 File Offset: 0x000ADA90
		private string GetRedirectUrl()
		{
			if (this.OnLoginPage())
			{
				string returnUrl = FormsAuthentication.GetReturnUrl(false);
				if (!string.IsNullOrEmpty(returnUrl))
				{
					return returnUrl;
				}
				string destinationPageUrl = this.DestinationPageUrl;
				if (!string.IsNullOrEmpty(destinationPageUrl))
				{
					return base.ResolveClientUrl(destinationPageUrl);
				}
				return FormsAuthentication.DefaultUrl;
			}
			else
			{
				string destinationPageUrl2 = this.DestinationPageUrl;
				if (!string.IsNullOrEmpty(destinationPageUrl2))
				{
					return base.ResolveClientUrl(destinationPageUrl2);
				}
				if (this.Page.Form != null && string.Equals(this.Page.Form.Method, "get", StringComparison.OrdinalIgnoreCase))
				{
					return this.Page.Request.ClientFilePath.VirtualPathString;
				}
				return this.Page.Request.RawUrl;
			}
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000AF93C File Offset: 0x000ADB3C
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 10)
			{
				throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
			}
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.LoginButtonStyle).LoadViewState(array[1]);
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
				((IStateManager)this.CheckBoxStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.FailureTextStyle).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.ValidatorTextStyle).LoadViewState(array[9]);
			}
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000AFA28 File Offset: 0x000ADC28
		protected virtual void OnLoggedIn(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Login.EventLoggedIn];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000AFA58 File Offset: 0x000ADC58
		protected virtual void OnAuthenticate(AuthenticateEventArgs e)
		{
			AuthenticateEventHandler authenticateEventHandler = (AuthenticateEventHandler)base.Events[Login.EventAuthenticate];
			if (authenticateEventHandler != null)
			{
				authenticateEventHandler(this, e);
				return;
			}
			this.AuthenticateUsingMembershipProvider(e);
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000AFA90 File Offset: 0x000ADC90
		protected virtual void OnLoggingIn(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[Login.EventLoggingIn];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000AFAC0 File Offset: 0x000ADCC0
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			bool result = false;
			if (e is CommandEventArgs)
			{
				CommandEventArgs commandEventArgs = (CommandEventArgs)e;
				if (string.Equals(commandEventArgs.CommandName, Login.LoginButtonCommandName, StringComparison.OrdinalIgnoreCase))
				{
					this.AttemptLogin();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000AFAFC File Offset: 0x000ADCFC
		protected virtual void OnLoginError(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Login.EventLoginError];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000AFB2A File Offset: 0x000ADD2A
		private bool OnLoginPage()
		{
			return AuthenticationConfig.AccessingLoginPage(this.Context, FormsAuthentication.LoginUrl);
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000AFB3C File Offset: 0x000ADD3C
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.SetEditableChildProperties();
			this.TemplateContainer.Visible = (this.VisibleWhenLoggedIn || !this.Page.Request.IsAuthenticated || this.OnLoginPage());
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x000AFB79 File Offset: 0x000ADD79
		private void PasswordTextChanged(object source, EventArgs e)
		{
			this._password = ((ITextControl)source).Text;
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x000AFB8C File Offset: 0x000ADD8C
		private bool RedirectedFromFailedLogin()
		{
			return !base.DesignMode && this.Page != null && !this.Page.IsPostBack && this.Page.Request.QueryString["loginfailure"] != null;
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x000AFBDC File Offset: 0x000ADDDC
		private void RememberMeCheckedChanged(object source, EventArgs e)
		{
			this.RememberMeSet = ((ICheckBoxControl)source).Checked;
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000AFBF0 File Offset: 0x000ADDF0
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			if (base.DesignMode)
			{
				base.ChildControlsCreated = false;
				this.EnsureChildControls();
			}
			if (this.TemplateContainer.Visible)
			{
				this.SetChildProperties();
				this.RenderContents(writer);
			}
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000AFC40 File Offset: 0x000ADE40
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this._loginButtonStyle != null) ? ((IStateManager)this._loginButtonStyle).SaveViewState() : null,
				(this._labelStyle != null) ? ((IStateManager)this._labelStyle).SaveViewState() : null,
				(this._textBoxStyle != null) ? ((IStateManager)this._textBoxStyle).SaveViewState() : null,
				(this._hyperLinkStyle != null) ? ((IStateManager)this._hyperLinkStyle).SaveViewState() : null,
				(this._instructionTextStyle != null) ? ((IStateManager)this._instructionTextStyle).SaveViewState() : null,
				(this._titleTextStyle != null) ? ((IStateManager)this._titleTextStyle).SaveViewState() : null,
				(this._checkBoxStyle != null) ? ((IStateManager)this._checkBoxStyle).SaveViewState() : null,
				(this._failureTextStyle != null) ? ((IStateManager)this._failureTextStyle).SaveViewState() : null,
				(this._validatorTextStyle != null) ? ((IStateManager)this._validatorTextStyle).SaveViewState() : null
			};
			for (int i = 0; i < 10; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000AFD55 File Offset: 0x000ADF55
		internal void SetChildProperties()
		{
			this.SetCommonChildProperties();
			if (this.LayoutTemplate == null)
			{
				this.SetDefaultTemplateChildProperties();
			}
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000AFD6C File Offset: 0x000ADF6C
		private void SetCommonChildProperties()
		{
			Login.LoginContainer templateContainer = this.TemplateContainer;
			Util.CopyBaseAttributesToInnerControl(this, templateContainer);
			templateContainer.ApplyStyle(base.ControlStyle);
			ITextControl textControl = (ITextControl)templateContainer.FailureTextLabel;
			string failureText = this.FailureText;
			if (textControl != null && failureText.Length > 0 && this.RedirectedFromFailedLogin())
			{
				textControl.Text = failureText;
			}
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000AFDC4 File Offset: 0x000ADFC4
		private void SetDefaultTemplateChildProperties()
		{
			Login.LoginContainer templateContainer = this.TemplateContainer;
			templateContainer.BorderTable.CellPadding = this.BorderPadding;
			templateContainer.BorderTable.CellSpacing = 0;
			Literal title = templateContainer.Title;
			string titleText = this.TitleText;
			if (titleText.Length > 0)
			{
				title.Text = titleText;
				if (this._titleTextStyle != null)
				{
					LoginUtil.SetTableCellStyle(title, this.TitleTextStyle);
				}
				LoginUtil.SetTableCellVisible(title, true);
			}
			else
			{
				LoginUtil.SetTableCellVisible(title, false);
			}
			Literal instruction = templateContainer.Instruction;
			string instructionText = this.InstructionText;
			if (instructionText.Length > 0)
			{
				instruction.Text = instructionText;
				if (this._instructionTextStyle != null)
				{
					LoginUtil.SetTableCellStyle(instruction, this.InstructionTextStyle);
				}
				LoginUtil.SetTableCellVisible(instruction, true);
			}
			else
			{
				LoginUtil.SetTableCellVisible(instruction, false);
			}
			Control userNameLabel = templateContainer.UserNameLabel;
			string userNameLabelText = this.UserNameLabelText;
			if (userNameLabelText.Length > 0)
			{
				((ITextControl)userNameLabel).Text = userNameLabelText;
				if (this._labelStyle != null)
				{
					LoginUtil.SetTableCellStyle(userNameLabel, this.LabelStyle);
				}
				userNameLabel.Visible = true;
			}
			else
			{
				userNameLabel.Visible = false;
			}
			WebControl webControl = (WebControl)templateContainer.UserNameTextBox;
			if (this._textBoxStyle != null)
			{
				webControl.ApplyStyle(this.TextBoxStyle);
			}
			webControl.TabIndex = this.TabIndex;
			webControl.AccessKey = this.AccessKey;
			bool flag = true;
			RequiredFieldValidator userNameRequired = templateContainer.UserNameRequired;
			userNameRequired.ErrorMessage = this.UserNameRequiredErrorMessage;
			userNameRequired.ToolTip = this.UserNameRequiredErrorMessage;
			userNameRequired.Enabled = flag;
			userNameRequired.Visible = flag;
			if (this._validatorTextStyle != null)
			{
				userNameRequired.ApplyStyle(this._validatorTextStyle);
			}
			Control passwordLabel = templateContainer.PasswordLabel;
			string passwordLabelText = this.PasswordLabelText;
			if (passwordLabelText.Length > 0)
			{
				((ITextControl)passwordLabel).Text = passwordLabelText;
				if (this._labelStyle != null)
				{
					LoginUtil.SetTableCellStyle(passwordLabel, this.LabelStyle);
				}
				passwordLabel.Visible = true;
			}
			else
			{
				passwordLabel.Visible = false;
			}
			WebControl webControl2 = (WebControl)templateContainer.PasswordTextBox;
			if (this._textBoxStyle != null)
			{
				webControl2.ApplyStyle(this.TextBoxStyle);
			}
			webControl2.TabIndex = this.TabIndex;
			RequiredFieldValidator passwordRequired = templateContainer.PasswordRequired;
			passwordRequired.ErrorMessage = this.PasswordRequiredErrorMessage;
			passwordRequired.ToolTip = this.PasswordRequiredErrorMessage;
			passwordRequired.Enabled = flag;
			passwordRequired.Visible = flag;
			if (this._validatorTextStyle != null)
			{
				passwordRequired.ApplyStyle(this._validatorTextStyle);
			}
			CheckBox checkBox = (CheckBox)templateContainer.RememberMeCheckBox;
			if (this.DisplayRememberMe)
			{
				checkBox.Text = this.RememberMeText;
				if (this._checkBoxStyle != null)
				{
					LoginUtil.SetTableCellStyle(checkBox, this.CheckBoxStyle);
				}
				LoginUtil.SetTableCellVisible(checkBox, true);
			}
			else
			{
				LoginUtil.SetTableCellVisible(checkBox, false);
			}
			checkBox.TabIndex = this.TabIndex;
			LinkButton linkButton = templateContainer.LinkButton;
			ImageButton imageButton = templateContainer.ImageButton;
			Button pushButton = templateContainer.PushButton;
			WebControl webControl3 = null;
			switch (this.LoginButtonType)
			{
			case ButtonType.Button:
				pushButton.Text = this.LoginButtonText;
				webControl3 = pushButton;
				break;
			case ButtonType.Image:
				imageButton.ImageUrl = this.LoginButtonImageUrl;
				imageButton.AlternateText = this.LoginButtonText;
				webControl3 = imageButton;
				break;
			case ButtonType.Link:
				linkButton.Text = this.LoginButtonText;
				webControl3 = linkButton;
				break;
			}
			linkButton.Visible = false;
			imageButton.Visible = false;
			pushButton.Visible = false;
			webControl3.Visible = true;
			webControl3.TabIndex = this.TabIndex;
			if (this._loginButtonStyle != null)
			{
				webControl3.ApplyStyle(this.LoginButtonStyle);
			}
			Image createUserIcon = templateContainer.CreateUserIcon;
			HyperLink createUserLink = templateContainer.CreateUserLink;
			LiteralControl createUserLinkSeparator = templateContainer.CreateUserLinkSeparator;
			HyperLink passwordRecoveryLink = templateContainer.PasswordRecoveryLink;
			Image passwordRecoveryIcon = templateContainer.PasswordRecoveryIcon;
			HyperLink helpPageLink = templateContainer.HelpPageLink;
			Image helpPageIcon = templateContainer.HelpPageIcon;
			LiteralControl passwordRecoveryLinkSeparator = templateContainer.PasswordRecoveryLinkSeparator;
			string createUserText = this.CreateUserText;
			string createUserIconUrl = this.CreateUserIconUrl;
			string passwordRecoveryText = this.PasswordRecoveryText;
			string passwordRecoveryIconUrl = this.PasswordRecoveryIconUrl;
			string helpPageText = this.HelpPageText;
			string helpPageIconUrl = this.HelpPageIconUrl;
			bool flag2 = createUserText.Length > 0;
			bool flag3 = passwordRecoveryText.Length > 0;
			bool flag4 = helpPageText.Length > 0;
			bool flag5 = helpPageIconUrl.Length > 0;
			bool flag6 = createUserIconUrl.Length > 0;
			bool flag7 = passwordRecoveryIconUrl.Length > 0;
			bool flag8 = flag4 || flag5;
			bool flag9 = flag2 || flag6;
			bool flag10 = flag3 || flag7;
			helpPageLink.Visible = flag4;
			passwordRecoveryLinkSeparator.Visible = (flag8 && (flag10 || flag9));
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
			createUserLinkSeparator.Visible = (flag9 && flag10);
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
			if (flag9 || flag10 || flag8)
			{
				if (this._hyperLinkStyle != null)
				{
					TableItemStyle tableItemStyle = new TableItemStyle();
					tableItemStyle.CopyFrom(this.HyperLinkStyle);
					tableItemStyle.Font.Reset();
					LoginUtil.SetTableCellStyle(createUserLink, tableItemStyle);
					createUserLink.Font.CopyFrom(this.HyperLinkStyle.Font);
					createUserLink.ForeColor = this.HyperLinkStyle.ForeColor;
					passwordRecoveryLink.Font.CopyFrom(this.HyperLinkStyle.Font);
					passwordRecoveryLink.ForeColor = this.HyperLinkStyle.ForeColor;
					helpPageLink.Font.CopyFrom(this.HyperLinkStyle.Font);
					helpPageLink.ForeColor = this.HyperLinkStyle.ForeColor;
				}
				LoginUtil.SetTableCellVisible(helpPageLink, true);
			}
			else
			{
				LoginUtil.SetTableCellVisible(helpPageLink, false);
			}
			Control failureTextLabel = templateContainer.FailureTextLabel;
			if (((ITextControl)failureTextLabel).Text.Length > 0)
			{
				LoginUtil.SetTableCellStyle(failureTextLabel, this.FailureTextStyle);
				LoginUtil.SetTableCellVisible(failureTextLabel, true);
				return;
			}
			LoginUtil.SetTableCellVisible(failureTextLabel, false);
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000B041C File Offset: 0x000AE61C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			if (data != null)
			{
				object obj = data["ConvertToTemplate"];
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

		// Token: 0x06003672 RID: 13938 RVA: 0x000B0464 File Offset: 0x000AE664
		private void SetEditableChildProperties()
		{
			Login.LoginContainer templateContainer = this.TemplateContainer;
			string userNameInternal = this.UserNameInternal;
			if (!string.IsNullOrEmpty(userNameInternal))
			{
				ITextControl textControl = (ITextControl)templateContainer.UserNameTextBox;
				if (textControl != null)
				{
					textControl.Text = userNameInternal;
				}
			}
			ICheckBoxControl checkBoxControl = (ICheckBoxControl)templateContainer.RememberMeCheckBox;
			if (checkBoxControl != null)
			{
				if (this.LayoutTemplate == null)
				{
					LoginUtil.SetTableCellVisible(templateContainer.RememberMeCheckBox, this.DisplayRememberMe);
				}
				checkBoxControl.Checked = this.RememberMeSet;
			}
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000B04D4 File Offset: 0x000AE6D4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._loginButtonStyle != null)
			{
				((IStateManager)this._loginButtonStyle).TrackViewState();
			}
			if (this._labelStyle != null)
			{
				((IStateManager)this._labelStyle).TrackViewState();
			}
			if (this._textBoxStyle != null)
			{
				((IStateManager)this._textBoxStyle).TrackViewState();
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
			if (this._checkBoxStyle != null)
			{
				((IStateManager)this._checkBoxStyle).TrackViewState();
			}
			if (this._failureTextStyle != null)
			{
				((IStateManager)this._failureTextStyle).TrackViewState();
			}
			if (this._validatorTextStyle != null)
			{
				((IStateManager)this._validatorTextStyle).TrackViewState();
			}
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000B0592 File Offset: 0x000AE792
		private void UserNameTextChanged(object source, EventArgs e)
		{
			this.UserName = ((ITextControl)source).Text;
		}

		// Token: 0x040021E5 RID: 8677
		public static readonly string LoginButtonCommandName = "Login";

		// Token: 0x040021E6 RID: 8678
		private ITemplate _loginTemplate;

		// Token: 0x040021E7 RID: 8679
		private Login.LoginContainer _templateContainer;

		// Token: 0x040021E8 RID: 8680
		private string _password;

		// Token: 0x040021E9 RID: 8681
		private bool _convertingToTemplate;

		// Token: 0x040021EA RID: 8682
		private bool _renderDesignerRegion;

		// Token: 0x040021EB RID: 8683
		private const string _userNameID = "UserName";

		// Token: 0x040021EC RID: 8684
		private const string _passwordID = "Password";

		// Token: 0x040021ED RID: 8685
		private const string _rememberMeID = "RememberMe";

		// Token: 0x040021EE RID: 8686
		private const string _failureTextID = "FailureText";

		// Token: 0x040021EF RID: 8687
		private const string _userNameRequiredID = "UserNameRequired";

		// Token: 0x040021F0 RID: 8688
		private const string _passwordRequiredID = "PasswordRequired";

		// Token: 0x040021F1 RID: 8689
		private const string _pushButtonID = "LoginButton";

		// Token: 0x040021F2 RID: 8690
		private const string _imageButtonID = "LoginImageButton";

		// Token: 0x040021F3 RID: 8691
		private const string _linkButtonID = "LoginLinkButton";

		// Token: 0x040021F4 RID: 8692
		private const string _passwordRecoveryLinkID = "PasswordRecoveryLink";

		// Token: 0x040021F5 RID: 8693
		private const string _helpLinkID = "HelpLink";

		// Token: 0x040021F6 RID: 8694
		private const string _createUserLinkID = "CreateUserLink";

		// Token: 0x040021F7 RID: 8695
		private const string _failureParameterName = "loginfailure";

		// Token: 0x040021F8 RID: 8696
		private const ValidatorDisplay _requiredFieldValidatorDisplay = ValidatorDisplay.Static;

		// Token: 0x040021F9 RID: 8697
		private const int _viewStateArrayLength = 10;

		// Token: 0x040021FA RID: 8698
		private Style _loginButtonStyle;

		// Token: 0x040021FB RID: 8699
		private TableItemStyle _labelStyle;

		// Token: 0x040021FC RID: 8700
		private Style _textBoxStyle;

		// Token: 0x040021FD RID: 8701
		private TableItemStyle _hyperLinkStyle;

		// Token: 0x040021FE RID: 8702
		private TableItemStyle _instructionTextStyle;

		// Token: 0x040021FF RID: 8703
		private TableItemStyle _titleTextStyle;

		// Token: 0x04002200 RID: 8704
		private TableItemStyle _checkBoxStyle;

		// Token: 0x04002201 RID: 8705
		private TableItemStyle _failureTextStyle;

		// Token: 0x04002202 RID: 8706
		private Style _validatorTextStyle;

		// Token: 0x04002203 RID: 8707
		private static readonly object EventLoggingIn = new object();

		// Token: 0x04002204 RID: 8708
		private static readonly object EventAuthenticate = new object();

		// Token: 0x04002205 RID: 8709
		private static readonly object EventLoggedIn = new object();

		// Token: 0x04002206 RID: 8710
		private static readonly object EventLoginError = new object();

		// Token: 0x020009A4 RID: 2468
		private sealed class LoginTemplate : ITemplate
		{
			// Token: 0x06006B61 RID: 27489 RVA: 0x0017E85D File Offset: 0x0017CA5D
			public LoginTemplate(Login owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006B62 RID: 27490 RVA: 0x0017E86C File Offset: 0x0017CA6C
			private void CreateControls(Login.LoginContainer loginContainer)
			{
				string uniqueID = this._owner.UniqueID;
				Literal title = new Literal();
				loginContainer.Title = title;
				Literal instruction = new Literal();
				loginContainer.Instruction = instruction;
				TextBox textBox = new TextBox();
				textBox.ID = "UserName";
				loginContainer.UserNameTextBox = textBox;
				LabelLiteral userNameLabel = new LabelLiteral(textBox);
				loginContainer.UserNameLabel = userNameLabel;
				bool flag = true;
				loginContainer.UserNameRequired = new RequiredFieldValidator
				{
					ID = "UserNameRequired",
					ValidationGroup = uniqueID,
					ControlToValidate = textBox.ID,
					Display = ValidatorDisplay.Static,
					Text = SR.GetString("LoginControls_DefaultRequiredFieldValidatorText"),
					Enabled = flag,
					Visible = flag
				};
				TextBox textBox2 = new TextBox();
				textBox2.ID = "Password";
				textBox2.TextMode = TextBoxMode.Password;
				loginContainer.PasswordTextBox = textBox2;
				LabelLiteral passwordLabel = new LabelLiteral(textBox2);
				loginContainer.PasswordLabel = passwordLabel;
				loginContainer.PasswordRequired = new RequiredFieldValidator
				{
					ID = "PasswordRequired",
					ValidationGroup = uniqueID,
					ControlToValidate = textBox2.ID,
					Display = ValidatorDisplay.Static,
					Text = SR.GetString("LoginControls_DefaultRequiredFieldValidatorText"),
					Enabled = flag,
					Visible = flag
				};
				loginContainer.RememberMeCheckBox = new CheckBox
				{
					ID = "RememberMe"
				};
				loginContainer.LinkButton = new LinkButton
				{
					ID = "LoginLinkButton",
					ValidationGroup = uniqueID,
					CommandName = Login.LoginButtonCommandName
				};
				loginContainer.ImageButton = new ImageButton
				{
					ID = "LoginImageButton",
					ValidationGroup = uniqueID,
					CommandName = Login.LoginButtonCommandName
				};
				loginContainer.PushButton = new Button
				{
					ID = "LoginButton",
					ValidationGroup = uniqueID,
					CommandName = Login.LoginButtonCommandName
				};
				HyperLink hyperLink = new HyperLink();
				loginContainer.PasswordRecoveryLink = hyperLink;
				LiteralControl passwordRecoveryLinkSeparator = new LiteralControl();
				hyperLink.ID = "PasswordRecoveryLink";
				loginContainer.PasswordRecoveryLinkSeparator = passwordRecoveryLinkSeparator;
				HyperLink hyperLink2 = new HyperLink();
				loginContainer.CreateUserLink = hyperLink2;
				hyperLink2.ID = "CreateUserLink";
				LiteralControl createUserLinkSeparator = new LiteralControl();
				loginContainer.CreateUserLinkSeparator = createUserLinkSeparator;
				loginContainer.HelpPageLink = new HyperLink
				{
					ID = "HelpLink"
				};
				loginContainer.FailureTextLabel = new Literal
				{
					ID = "FailureText"
				};
				loginContainer.PasswordRecoveryIcon = new Image();
				loginContainer.HelpPageIcon = new Image();
				loginContainer.CreateUserIcon = new Image();
			}

			// Token: 0x06006B63 RID: 27491 RVA: 0x0017EB08 File Offset: 0x0017CD08
			private void LayoutControls(Login.LoginContainer loginContainer)
			{
				Orientation orientation = this._owner.Orientation;
				LoginTextLayout textLayout = this._owner.TextLayout;
				if (orientation == Orientation.Vertical && textLayout == LoginTextLayout.TextOnLeft)
				{
					this.LayoutVerticalTextOnLeft(loginContainer);
					return;
				}
				if (orientation == Orientation.Vertical && textLayout == LoginTextLayout.TextOnTop)
				{
					this.LayoutVerticalTextOnTop(loginContainer);
					return;
				}
				if (orientation == Orientation.Horizontal && textLayout == LoginTextLayout.TextOnLeft)
				{
					this.LayoutHorizontalTextOnLeft(loginContainer);
					return;
				}
				this.LayoutHorizontalTextOnTop(loginContainer);
			}

			// Token: 0x06006B64 RID: 27492 RVA: 0x0017EB64 File Offset: 0x0017CD64
			private void LayoutHorizontalTextOnLeft(Login.LoginContainer loginContainer)
			{
				Table table = new Table();
				table.CellPadding = 0;
				TableRow tableRow = new LoginUtil.DisappearingTableRow();
				TableCell tableCell = new TableCell();
				tableCell.ColumnSpan = 6;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(loginContainer.Title);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 6;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(loginContainer.Instruction);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				if (this._owner.ConvertingToTemplate)
				{
					loginContainer.UserNameLabel.RenderAsLabel = true;
				}
				tableCell.Controls.Add(loginContainer.UserNameLabel);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.UserNameTextBox);
				tableCell.Controls.Add(loginContainer.UserNameRequired);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				if (this._owner.ConvertingToTemplate)
				{
					loginContainer.PasswordLabel.RenderAsLabel = true;
				}
				tableCell.Controls.Add(loginContainer.PasswordLabel);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.PasswordTextBox);
				tableCell.Controls.Add(loginContainer.PasswordRequired);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.RememberMeCheckBox);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.LinkButton);
				tableCell.Controls.Add(loginContainer.ImageButton);
				tableCell.Controls.Add(loginContainer.PushButton);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 6;
				tableCell.Controls.Add(loginContainer.FailureTextLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 6;
				tableCell.Controls.Add(loginContainer.CreateUserIcon);
				tableCell.Controls.Add(loginContainer.CreateUserLink);
				loginContainer.CreateUserLinkSeparator.Text = " ";
				tableCell.Controls.Add(loginContainer.CreateUserLinkSeparator);
				tableCell.Controls.Add(loginContainer.PasswordRecoveryIcon);
				tableCell.Controls.Add(loginContainer.PasswordRecoveryLink);
				loginContainer.PasswordRecoveryLinkSeparator.Text = " ";
				tableCell.Controls.Add(loginContainer.PasswordRecoveryLinkSeparator);
				tableCell.Controls.Add(loginContainer.HelpPageIcon);
				tableCell.Controls.Add(loginContainer.HelpPageLink);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				Table table2 = LoginUtil.CreateChildTable(this._owner.ConvertingToTemplate);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(table);
				tableRow.Cells.Add(tableCell);
				table2.Rows.Add(tableRow);
				loginContainer.LayoutTable = table;
				loginContainer.BorderTable = table2;
				loginContainer.Controls.Add(table2);
			}

			// Token: 0x06006B65 RID: 27493 RVA: 0x0017EEDC File Offset: 0x0017D0DC
			private void LayoutHorizontalTextOnTop(Login.LoginContainer loginContainer)
			{
				Table table = new Table();
				table.CellPadding = 0;
				TableRow tableRow = new LoginUtil.DisappearingTableRow();
				TableCell tableCell = new TableCell();
				tableCell.ColumnSpan = 4;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(loginContainer.Title);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 4;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(loginContainer.Instruction);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				if (this._owner.ConvertingToTemplate)
				{
					loginContainer.UserNameLabel.RenderAsLabel = true;
				}
				tableCell.Controls.Add(loginContainer.UserNameLabel);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				if (this._owner.ConvertingToTemplate)
				{
					loginContainer.PasswordLabel.RenderAsLabel = true;
				}
				tableCell.Controls.Add(loginContainer.PasswordLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.UserNameTextBox);
				tableCell.Controls.Add(loginContainer.UserNameRequired);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.PasswordTextBox);
				tableCell.Controls.Add(loginContainer.PasswordRequired);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.RememberMeCheckBox);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(loginContainer.LinkButton);
				tableCell.Controls.Add(loginContainer.ImageButton);
				tableCell.Controls.Add(loginContainer.PushButton);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 4;
				tableCell.Controls.Add(loginContainer.FailureTextLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 4;
				tableCell.Controls.Add(loginContainer.CreateUserIcon);
				tableCell.Controls.Add(loginContainer.CreateUserLink);
				loginContainer.CreateUserLinkSeparator.Text = " ";
				tableCell.Controls.Add(loginContainer.CreateUserLinkSeparator);
				tableCell.Controls.Add(loginContainer.PasswordRecoveryIcon);
				tableCell.Controls.Add(loginContainer.PasswordRecoveryLink);
				loginContainer.PasswordRecoveryLinkSeparator.Text = " ";
				tableCell.Controls.Add(loginContainer.PasswordRecoveryLinkSeparator);
				tableCell.Controls.Add(loginContainer.HelpPageIcon);
				tableCell.Controls.Add(loginContainer.HelpPageLink);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				Table table2 = LoginUtil.CreateChildTable(this._owner.ConvertingToTemplate);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(table);
				tableRow.Cells.Add(tableCell);
				table2.Rows.Add(tableRow);
				loginContainer.LayoutTable = table;
				loginContainer.BorderTable = table2;
				loginContainer.Controls.Add(table2);
			}

			// Token: 0x06006B66 RID: 27494 RVA: 0x0017F26C File Offset: 0x0017D46C
			private void LayoutVerticalTextOnLeft(Login.LoginContainer loginContainer)
			{
				Table table = new Table();
				table.CellPadding = 0;
				TableRow tableRow = new LoginUtil.DisappearingTableRow();
				TableCell tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(loginContainer.Title);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(loginContainer.Instruction);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				if (this._owner.ConvertingToTemplate)
				{
					loginContainer.UserNameLabel.RenderAsLabel = true;
				}
				tableCell.Controls.Add(loginContainer.UserNameLabel);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.UserNameTextBox);
				tableCell.Controls.Add(loginContainer.UserNameRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				if (this._owner.ConvertingToTemplate)
				{
					loginContainer.PasswordLabel.RenderAsLabel = true;
				}
				tableCell.Controls.Add(loginContainer.PasswordLabel);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.PasswordTextBox);
				tableCell.Controls.Add(loginContainer.PasswordRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(loginContainer.RememberMeCheckBox);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(loginContainer.FailureTextLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(loginContainer.LinkButton);
				tableCell.Controls.Add(loginContainer.ImageButton);
				tableCell.Controls.Add(loginContainer.PushButton);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(loginContainer.CreateUserIcon);
				tableCell.Controls.Add(loginContainer.CreateUserLink);
				tableCell.Controls.Add(loginContainer.CreateUserLinkSeparator);
				tableCell.Controls.Add(loginContainer.PasswordRecoveryIcon);
				tableCell.Controls.Add(loginContainer.PasswordRecoveryLink);
				loginContainer.PasswordRecoveryLinkSeparator.Text = "<br />";
				loginContainer.CreateUserLinkSeparator.Text = "<br />";
				tableCell.Controls.Add(loginContainer.PasswordRecoveryLinkSeparator);
				tableCell.Controls.Add(loginContainer.HelpPageIcon);
				tableCell.Controls.Add(loginContainer.HelpPageLink);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				Table table2 = LoginUtil.CreateChildTable(this._owner.ConvertingToTemplate);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(table);
				tableRow.Cells.Add(tableCell);
				table2.Rows.Add(tableRow);
				loginContainer.LayoutTable = table;
				loginContainer.BorderTable = table2;
				loginContainer.Controls.Add(table2);
			}

			// Token: 0x06006B67 RID: 27495 RVA: 0x0017F644 File Offset: 0x0017D844
			private void LayoutVerticalTextOnTop(Login.LoginContainer loginContainer)
			{
				Table table = new Table();
				table.CellPadding = 0;
				TableRow tableRow = new LoginUtil.DisappearingTableRow();
				TableCell tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(loginContainer.Title);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(loginContainer.Instruction);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				if (this._owner.ConvertingToTemplate)
				{
					loginContainer.UserNameLabel.RenderAsLabel = true;
				}
				tableCell.Controls.Add(loginContainer.UserNameLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.UserNameTextBox);
				tableCell.Controls.Add(loginContainer.UserNameRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				if (this._owner.ConvertingToTemplate)
				{
					loginContainer.PasswordLabel.RenderAsLabel = true;
				}
				tableCell.Controls.Add(loginContainer.PasswordLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.PasswordTextBox);
				tableCell.Controls.Add(loginContainer.PasswordRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.RememberMeCheckBox);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(loginContainer.FailureTextLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(loginContainer.LinkButton);
				tableCell.Controls.Add(loginContainer.ImageButton);
				tableCell.Controls.Add(loginContainer.PushButton);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(loginContainer.CreateUserIcon);
				tableCell.Controls.Add(loginContainer.CreateUserLink);
				loginContainer.CreateUserLinkSeparator.Text = "<br />";
				tableCell.Controls.Add(loginContainer.CreateUserLinkSeparator);
				tableCell.Controls.Add(loginContainer.PasswordRecoveryIcon);
				tableCell.Controls.Add(loginContainer.PasswordRecoveryLink);
				loginContainer.PasswordRecoveryLinkSeparator.Text = "<br />";
				tableCell.Controls.Add(loginContainer.PasswordRecoveryLinkSeparator);
				tableCell.Controls.Add(loginContainer.HelpPageIcon);
				tableCell.Controls.Add(loginContainer.HelpPageLink);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				Table table2 = LoginUtil.CreateChildTable(this._owner.ConvertingToTemplate);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(table);
				tableRow.Cells.Add(tableCell);
				table2.Rows.Add(tableRow);
				loginContainer.LayoutTable = table;
				loginContainer.BorderTable = table2;
				loginContainer.Controls.Add(table2);
			}

			// Token: 0x06006B68 RID: 27496 RVA: 0x0017FA0C File Offset: 0x0017DC0C
			void ITemplate.InstantiateIn(Control container)
			{
				Login.LoginContainer loginContainer = (Login.LoginContainer)container;
				this.CreateControls(loginContainer);
				this.LayoutControls(loginContainer);
			}

			// Token: 0x04003938 RID: 14648
			private Login _owner;
		}

		// Token: 0x020009A5 RID: 2469
		internal sealed class LoginContainer : LoginUtil.GenericContainer<Login>
		{
			// Token: 0x06006B69 RID: 27497 RVA: 0x0017FA2E File Offset: 0x0017DC2E
			public LoginContainer(Login owner) : base(owner)
			{
			}

			// Token: 0x17001D9C RID: 7580
			// (get) Token: 0x06006B6A RID: 27498 RVA: 0x0017FA37 File Offset: 0x0017DC37
			protected override bool ConvertingToTemplate
			{
				get
				{
					return base.Owner.ConvertingToTemplate;
				}
			}

			// Token: 0x17001D9D RID: 7581
			// (get) Token: 0x06006B6B RID: 27499 RVA: 0x0017FA44 File Offset: 0x0017DC44
			// (set) Token: 0x06006B6C RID: 27500 RVA: 0x0017FA4C File Offset: 0x0017DC4C
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

			// Token: 0x17001D9E RID: 7582
			// (get) Token: 0x06006B6D RID: 27501 RVA: 0x0017FA55 File Offset: 0x0017DC55
			// (set) Token: 0x06006B6E RID: 27502 RVA: 0x0017FA5D File Offset: 0x0017DC5D
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

			// Token: 0x17001D9F RID: 7583
			// (get) Token: 0x06006B6F RID: 27503 RVA: 0x0017FA66 File Offset: 0x0017DC66
			// (set) Token: 0x06006B70 RID: 27504 RVA: 0x0017FA6E File Offset: 0x0017DC6E
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

			// Token: 0x17001DA0 RID: 7584
			// (get) Token: 0x06006B71 RID: 27505 RVA: 0x0017FA77 File Offset: 0x0017DC77
			// (set) Token: 0x06006B72 RID: 27506 RVA: 0x0017FA7F File Offset: 0x0017DC7F
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

			// Token: 0x17001DA1 RID: 7585
			// (get) Token: 0x06006B73 RID: 27507 RVA: 0x0017FA88 File Offset: 0x0017DC88
			// (set) Token: 0x06006B74 RID: 27508 RVA: 0x0017FA90 File Offset: 0x0017DC90
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

			// Token: 0x17001DA2 RID: 7586
			// (get) Token: 0x06006B75 RID: 27509 RVA: 0x0017FA99 File Offset: 0x0017DC99
			// (set) Token: 0x06006B76 RID: 27510 RVA: 0x0017FAB5 File Offset: 0x0017DCB5
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

			// Token: 0x17001DA3 RID: 7587
			// (get) Token: 0x06006B77 RID: 27511 RVA: 0x0017FABE File Offset: 0x0017DCBE
			// (set) Token: 0x06006B78 RID: 27512 RVA: 0x0017FAC6 File Offset: 0x0017DCC6
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

			// Token: 0x17001DA4 RID: 7588
			// (get) Token: 0x06006B79 RID: 27513 RVA: 0x0017FACF File Offset: 0x0017DCCF
			// (set) Token: 0x06006B7A RID: 27514 RVA: 0x0017FAD7 File Offset: 0x0017DCD7
			internal ImageButton ImageButton
			{
				get
				{
					return this._imageButton;
				}
				set
				{
					this._imageButton = value;
				}
			}

			// Token: 0x17001DA5 RID: 7589
			// (get) Token: 0x06006B7B RID: 27515 RVA: 0x0017FAE0 File Offset: 0x0017DCE0
			// (set) Token: 0x06006B7C RID: 27516 RVA: 0x0017FAE8 File Offset: 0x0017DCE8
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

			// Token: 0x17001DA6 RID: 7590
			// (get) Token: 0x06006B7D RID: 27517 RVA: 0x0017FAF1 File Offset: 0x0017DCF1
			// (set) Token: 0x06006B7E RID: 27518 RVA: 0x0017FAF9 File Offset: 0x0017DCF9
			internal LinkButton LinkButton
			{
				get
				{
					return this._linkButton;
				}
				set
				{
					this._linkButton = value;
				}
			}

			// Token: 0x17001DA7 RID: 7591
			// (get) Token: 0x06006B7F RID: 27519 RVA: 0x0017FB02 File Offset: 0x0017DD02
			// (set) Token: 0x06006B80 RID: 27520 RVA: 0x0017FB0A File Offset: 0x0017DD0A
			internal LabelLiteral PasswordLabel
			{
				get
				{
					return this._passwordLabel;
				}
				set
				{
					this._passwordLabel = value;
				}
			}

			// Token: 0x17001DA8 RID: 7592
			// (get) Token: 0x06006B81 RID: 27521 RVA: 0x0017FB13 File Offset: 0x0017DD13
			// (set) Token: 0x06006B82 RID: 27522 RVA: 0x0017FB1B File Offset: 0x0017DD1B
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

			// Token: 0x17001DA9 RID: 7593
			// (get) Token: 0x06006B83 RID: 27523 RVA: 0x0017FB24 File Offset: 0x0017DD24
			// (set) Token: 0x06006B84 RID: 27524 RVA: 0x0017FB2C File Offset: 0x0017DD2C
			internal LiteralControl PasswordRecoveryLinkSeparator
			{
				get
				{
					return this._passwordRecoveryLinkSeparator;
				}
				set
				{
					this._passwordRecoveryLinkSeparator = value;
				}
			}

			// Token: 0x17001DAA RID: 7594
			// (get) Token: 0x06006B85 RID: 27525 RVA: 0x0017FB35 File Offset: 0x0017DD35
			// (set) Token: 0x06006B86 RID: 27526 RVA: 0x0017FB3D File Offset: 0x0017DD3D
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

			// Token: 0x17001DAB RID: 7595
			// (get) Token: 0x06006B87 RID: 27527 RVA: 0x0017FB46 File Offset: 0x0017DD46
			// (set) Token: 0x06006B88 RID: 27528 RVA: 0x0017FB67 File Offset: 0x0017DD67
			internal Control PasswordTextBox
			{
				get
				{
					if (this._passwordTextBox != null)
					{
						return this._passwordTextBox;
					}
					return base.FindRequiredControl<IEditableTextControl>("Password", "Login_NoPasswordTextBox");
				}
				set
				{
					this._passwordTextBox = value;
				}
			}

			// Token: 0x17001DAC RID: 7596
			// (get) Token: 0x06006B89 RID: 27529 RVA: 0x0017FB70 File Offset: 0x0017DD70
			// (set) Token: 0x06006B8A RID: 27530 RVA: 0x0017FB78 File Offset: 0x0017DD78
			internal Button PushButton
			{
				get
				{
					return this._pushButton;
				}
				set
				{
					this._pushButton = value;
				}
			}

			// Token: 0x17001DAD RID: 7597
			// (get) Token: 0x06006B8B RID: 27531 RVA: 0x0017FB81 File Offset: 0x0017DD81
			// (set) Token: 0x06006B8C RID: 27532 RVA: 0x0017FB9D File Offset: 0x0017DD9D
			internal Control RememberMeCheckBox
			{
				get
				{
					if (this._rememberMeCheckBox != null)
					{
						return this._rememberMeCheckBox;
					}
					return base.FindOptionalControl<ICheckBoxControl>("RememberMe");
				}
				set
				{
					this._rememberMeCheckBox = value;
				}
			}

			// Token: 0x17001DAE RID: 7598
			// (get) Token: 0x06006B8D RID: 27533 RVA: 0x0017FBA6 File Offset: 0x0017DDA6
			// (set) Token: 0x06006B8E RID: 27534 RVA: 0x0017FBAE File Offset: 0x0017DDAE
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

			// Token: 0x17001DAF RID: 7599
			// (get) Token: 0x06006B8F RID: 27535 RVA: 0x0017FBB7 File Offset: 0x0017DDB7
			// (set) Token: 0x06006B90 RID: 27536 RVA: 0x0017FBBF File Offset: 0x0017DDBF
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

			// Token: 0x17001DB0 RID: 7600
			// (get) Token: 0x06006B91 RID: 27537 RVA: 0x0017FBC8 File Offset: 0x0017DDC8
			// (set) Token: 0x06006B92 RID: 27538 RVA: 0x0017FBD0 File Offset: 0x0017DDD0
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

			// Token: 0x17001DB1 RID: 7601
			// (get) Token: 0x06006B93 RID: 27539 RVA: 0x0017FBD9 File Offset: 0x0017DDD9
			// (set) Token: 0x06006B94 RID: 27540 RVA: 0x0017FBFA File Offset: 0x0017DDFA
			internal Control UserNameTextBox
			{
				get
				{
					if (this._userNameTextBox != null)
					{
						return this._userNameTextBox;
					}
					return base.FindRequiredControl<IEditableTextControl>("UserName", "Login_NoUserNameTextBox");
				}
				set
				{
					this._userNameTextBox = value;
				}
			}

			// Token: 0x04003939 RID: 14649
			private HyperLink _createUserLink;

			// Token: 0x0400393A RID: 14650
			private LiteralControl _createUserLinkSeparator;

			// Token: 0x0400393B RID: 14651
			private Control _failureTextLabel;

			// Token: 0x0400393C RID: 14652
			private HyperLink _helpPageLink;

			// Token: 0x0400393D RID: 14653
			private ImageButton _imageButton;

			// Token: 0x0400393E RID: 14654
			private Literal _instruction;

			// Token: 0x0400393F RID: 14655
			private LinkButton _linkButton;

			// Token: 0x04003940 RID: 14656
			private LabelLiteral _passwordLabel;

			// Token: 0x04003941 RID: 14657
			private HyperLink _passwordRecoveryLink;

			// Token: 0x04003942 RID: 14658
			private LiteralControl _passwordRecoveryLinkSeparator;

			// Token: 0x04003943 RID: 14659
			private RequiredFieldValidator _passwordRequired;

			// Token: 0x04003944 RID: 14660
			private Control _passwordTextBox;

			// Token: 0x04003945 RID: 14661
			private Button _pushButton;

			// Token: 0x04003946 RID: 14662
			private Control _rememberMeCheckBox;

			// Token: 0x04003947 RID: 14663
			private Literal _title;

			// Token: 0x04003948 RID: 14664
			private LabelLiteral _userNameLabel;

			// Token: 0x04003949 RID: 14665
			private RequiredFieldValidator _userNameRequired;

			// Token: 0x0400394A RID: 14666
			private Control _userNameTextBox;

			// Token: 0x0400394B RID: 14667
			private Image _createUserIcon;

			// Token: 0x0400394C RID: 14668
			private Image _helpPageIcon;

			// Token: 0x0400394D RID: 14669
			private Image _passwordRecoveryIcon;
		}
	}
}
