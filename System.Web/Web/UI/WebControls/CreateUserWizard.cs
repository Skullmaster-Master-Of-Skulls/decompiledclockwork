using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200051F RID: 1311
	[Designer("System.Web.UI.Design.WebControls.CreateUserWizardDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Bindable(false)]
	[ToolboxData("<{0}:CreateUserWizard runat=\"server\"> <WizardSteps> <asp:CreateUserWizardStep runat=\"server\"/> <asp:CompleteWizardStep runat=\"server\"/> </WizardSteps> </{0}:CreateUserWizard>")]
	[DefaultEvent("CreatedUser")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CreateUserWizard : Wizard
	{
		// Token: 0x06003FF7 RID: 16375 RVA: 0x00109E38 File Offset: 0x00108E38
		public CreateUserWizard()
		{
			this._displaySideBarDefault = false;
			this._displaySideBar = this._displaySideBarDefault;
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x00109E53 File Offset: 0x00108E53
		// (set) Token: 0x06003FF9 RID: 16377 RVA: 0x00109E5B File Offset: 0x00108E5B
		[DefaultValue(0)]
		public override int ActiveStepIndex
		{
			get
			{
				return base.ActiveStepIndex;
			}
			set
			{
				base.ActiveStepIndex = value;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x00109E64 File Offset: 0x00108E64
		// (set) Token: 0x06003FFB RID: 16379 RVA: 0x00109E7A File Offset: 0x00108E7A
		[Themeable(false)]
		[DefaultValue("")]
		[Localizable(true)]
		[WebSysDescription("CreateUserWizard_Answer")]
		[WebCategory("Appearance")]
		public virtual string Answer
		{
			get
			{
				if (this._answer != null)
				{
					return this._answer;
				}
				return string.Empty;
			}
			set
			{
				this._answer = value;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06003FFC RID: 16380 RVA: 0x00109E84 File Offset: 0x00108E84
		private string AnswerInternal
		{
			get
			{
				string text = this.Answer;
				if (string.IsNullOrEmpty(this.Answer) && this._createUserStepContainer != null)
				{
					ITextControl textControl = (ITextControl)this._createUserStepContainer.AnswerTextBox;
					if (textControl != null)
					{
						text = textControl.Text;
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					text = null;
				}
				return text;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06003FFD RID: 16381 RVA: 0x00109ED4 File Offset: 0x00108ED4
		// (set) Token: 0x06003FFE RID: 16382 RVA: 0x00109F06 File Offset: 0x00108F06
		[WebSysDefaultValue("CreateUserWizard_DefaultAnswerLabelText")]
		[WebCategory("Appearance")]
		[Localizable(true)]
		[WebSysDescription("CreateUserWizard_AnswerLabelText")]
		public virtual string AnswerLabelText
		{
			get
			{
				object obj = this.ViewState["AnswerLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultAnswerLabelText");
			}
			set
			{
				this.ViewState["AnswerLabelText"] = value;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06003FFF RID: 16383 RVA: 0x00109F1C File Offset: 0x00108F1C
		// (set) Token: 0x06004000 RID: 16384 RVA: 0x00109F4E File Offset: 0x00108F4E
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("CreateUserWizard_DefaultAnswerRequiredErrorMessage")]
		[WebSysDescription("LoginControls_AnswerRequiredErrorMessage")]
		public virtual string AnswerRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["AnswerRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultAnswerRequiredErrorMessage");
			}
			set
			{
				this.ViewState["AnswerRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06004001 RID: 16385 RVA: 0x00109F64 File Offset: 0x00108F64
		// (set) Token: 0x06004002 RID: 16386 RVA: 0x00109F8D File Offset: 0x00108F8D
		[WebCategory("Behavior")]
		[WebSysDescription("CreateUserWizard_AutoGeneratePassword")]
		[DefaultValue(false)]
		[Themeable(false)]
		public virtual bool AutoGeneratePassword
		{
			get
			{
				object obj = this.ViewState["AutoGeneratePassword"];
				return obj != null && (bool)obj;
			}
			set
			{
				if (this.AutoGeneratePassword != value)
				{
					this.ViewState["AutoGeneratePassword"] = value;
					base.RequiresControlsRecreation();
				}
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06004003 RID: 16387 RVA: 0x00109FB4 File Offset: 0x00108FB4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[WebSysDescription("CreateUserWizard_CompleteStep")]
		[WebCategory("Appearance")]
		public CompleteWizardStep CompleteStep
		{
			get
			{
				this.EnsureChildControls();
				return this._completeStep;
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x00109FC4 File Offset: 0x00108FC4
		// (set) Token: 0x06004005 RID: 16389 RVA: 0x00109FF6 File Offset: 0x00108FF6
		[Localizable(true)]
		[WebSysDescription("CreateUserWizard_CompleteSuccessText")]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultCompleteSuccessText")]
		public virtual string CompleteSuccessText
		{
			get
			{
				object obj = this.ViewState["CompleteSuccessText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultCompleteSuccessText");
			}
			set
			{
				this.ViewState["CompleteSuccessText"] = value;
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x0010A009 File Offset: 0x00109009
		[WebCategory("Styles")]
		[NotifyParentProperty(true)]
		[WebSysDescription("CreateUserWizard_CompleteSuccessTextStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle CompleteSuccessTextStyle
		{
			get
			{
				if (this._completeSuccessTextStyle == null)
				{
					this._completeSuccessTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._completeSuccessTextStyle).TrackViewState();
					}
				}
				return this._completeSuccessTextStyle;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06004007 RID: 16391 RVA: 0x0010A037 File Offset: 0x00109037
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual string ConfirmPassword
		{
			get
			{
				if (this._confirmPassword != null)
				{
					return this._confirmPassword;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x0010A050 File Offset: 0x00109050
		// (set) Token: 0x06004009 RID: 16393 RVA: 0x0010A082 File Offset: 0x00109082
		[WebCategory("Validation")]
		[WebSysDescription("ChangePassword_ConfirmPasswordCompareErrorMessage")]
		[Localizable(true)]
		[WebSysDefaultValue("CreateUserWizard_DefaultConfirmPasswordCompareErrorMessage")]
		public virtual string ConfirmPasswordCompareErrorMessage
		{
			get
			{
				object obj = this.ViewState["ConfirmPasswordCompareErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultConfirmPasswordCompareErrorMessage");
			}
			set
			{
				this.ViewState["ConfirmPasswordCompareErrorMessage"] = value;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x0010A098 File Offset: 0x00109098
		// (set) Token: 0x0600400B RID: 16395 RVA: 0x0010A0CA File Offset: 0x001090CA
		[WebCategory("Appearance")]
		[WebSysDescription("CreateUserWizard_ConfirmPasswordLabelText")]
		[WebSysDefaultValue("CreateUserWizard_DefaultConfirmPasswordLabelText")]
		[Localizable(true)]
		public virtual string ConfirmPasswordLabelText
		{
			get
			{
				object obj = this.ViewState["ConfirmPasswordLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultConfirmPasswordLabelText");
			}
			set
			{
				this.ViewState["ConfirmPasswordLabelText"] = value;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x0010A0E0 File Offset: 0x001090E0
		// (set) Token: 0x0600400D RID: 16397 RVA: 0x0010A112 File Offset: 0x00109112
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("CreateUserWizard_DefaultConfirmPasswordRequiredErrorMessage")]
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
				return SR.GetString("CreateUserWizard_DefaultConfirmPasswordRequiredErrorMessage");
			}
			set
			{
				this.ViewState["ConfirmPasswordRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x0010A128 File Offset: 0x00109128
		// (set) Token: 0x0600400F RID: 16399 RVA: 0x0010A155 File Offset: 0x00109155
		[WebSysDescription("ChangePassword_ContinueButtonImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[DefaultValue("")]
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

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x0010A168 File Offset: 0x00109168
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("CreateUserWizard_ContinueButtonStyle")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebCategory("Styles")]
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

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06004011 RID: 16401 RVA: 0x0010A198 File Offset: 0x00109198
		// (set) Token: 0x06004012 RID: 16402 RVA: 0x0010A1CA File Offset: 0x001091CA
		[WebSysDefaultValue("CreateUserWizard_DefaultContinueButtonText")]
		[WebCategory("Appearance")]
		[WebSysDescription("CreateUserWizard_ContinueButtonText")]
		[Localizable(true)]
		public virtual string ContinueButtonText
		{
			get
			{
				object obj = this.ViewState["ContinueButtonText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultContinueButtonText");
			}
			set
			{
				this.ViewState["ContinueButtonText"] = value;
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06004013 RID: 16403 RVA: 0x0010A1E0 File Offset: 0x001091E0
		// (set) Token: 0x06004014 RID: 16404 RVA: 0x0010A209 File Offset: 0x00109209
		[WebCategory("Appearance")]
		[WebSysDescription("CreateUserWizard_ContinueButtonType")]
		[DefaultValue(ButtonType.Button)]
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
				if (value != this.ContinueButtonType)
				{
					this.ViewState["ContinueButtonType"] = value;
				}
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06004015 RID: 16405 RVA: 0x0010A240 File Offset: 0x00109240
		// (set) Token: 0x06004016 RID: 16406 RVA: 0x0010A26D File Offset: 0x0010926D
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
		[UrlProperty]
		[WebSysDescription("LoginControls_ContinueDestinationPageUrl")]
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

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06004017 RID: 16407 RVA: 0x0010A280 File Offset: 0x00109280
		private bool ConvertingToTemplate
		{
			get
			{
				return base.DesignMode && this._convertingToTemplate;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x0010A292 File Offset: 0x00109292
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("CreateUserWizard_CreateUserStep")]
		[Browsable(false)]
		public CreateUserWizardStep CreateUserStep
		{
			get
			{
				this.EnsureChildControls();
				return this._createUserStep;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06004019 RID: 16409 RVA: 0x0010A2A0 File Offset: 0x001092A0
		// (set) Token: 0x0600401A RID: 16410 RVA: 0x0010A2CD File Offset: 0x001092CD
		[DefaultValue("")]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("CreateUserWizard_CreateUserButtonImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public virtual string CreateUserButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["CreateUserButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CreateUserButtonImageUrl"] = value;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x0600401B RID: 16411 RVA: 0x0010A2E0 File Offset: 0x001092E0
		[WebSysDescription("CreateUserWizard_CreateUserButtonStyle")]
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style CreateUserButtonStyle
		{
			get
			{
				if (this._createUserButtonStyle == null)
				{
					this._createUserButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._createUserButtonStyle).TrackViewState();
					}
				}
				return this._createUserButtonStyle;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x0010A310 File Offset: 0x00109310
		// (set) Token: 0x0600401D RID: 16413 RVA: 0x0010A342 File Offset: 0x00109342
		[WebSysDefaultValue("CreateUserWizard_DefaultCreateUserButtonText")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("CreateUserWizard_CreateUserButtonText")]
		public virtual string CreateUserButtonText
		{
			get
			{
				object obj = this.ViewState["CreateUserButtonText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultCreateUserButtonText");
			}
			set
			{
				this.ViewState["CreateUserButtonText"] = value;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x0600401E RID: 16414 RVA: 0x0010A358 File Offset: 0x00109358
		// (set) Token: 0x0600401F RID: 16415 RVA: 0x0010A381 File Offset: 0x00109381
		[DefaultValue(ButtonType.Button)]
		[WebCategory("Appearance")]
		[WebSysDescription("CreateUserWizard_CreateUserButtonType")]
		public virtual ButtonType CreateUserButtonType
		{
			get
			{
				object obj = this.ViewState["CreateUserButtonType"];
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
				if (value != this.CreateUserButtonType)
				{
					this.ViewState["CreateUserButtonType"] = value;
				}
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06004020 RID: 16416 RVA: 0x0010A3B8 File Offset: 0x001093B8
		private bool DefaultCreateUserStep
		{
			get
			{
				CreateUserWizardStep createUserStep = this.CreateUserStep;
				return createUserStep != null && createUserStep.ContentTemplate == null;
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06004021 RID: 16417 RVA: 0x0010A3DC File Offset: 0x001093DC
		private bool DefaultCompleteStep
		{
			get
			{
				CompleteWizardStep completeStep = this.CompleteStep;
				return completeStep != null && completeStep.ContentTemplate == null;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06004022 RID: 16418 RVA: 0x0010A400 File Offset: 0x00109400
		// (set) Token: 0x06004023 RID: 16419 RVA: 0x0010A429 File Offset: 0x00109429
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[Themeable(false)]
		[WebSysDescription("CreateUserWizard_DisableCreatedUser")]
		public virtual bool DisableCreatedUser
		{
			get
			{
				object obj = this.ViewState["DisableCreatedUser"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["DisableCreatedUser"] = value;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x0010A441 File Offset: 0x00109441
		// (set) Token: 0x06004025 RID: 16421 RVA: 0x0010A449 File Offset: 0x00109449
		[DefaultValue(false)]
		public override bool DisplaySideBar
		{
			get
			{
				return base.DisplaySideBar;
			}
			set
			{
				base.DisplaySideBar = value;
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x0010A454 File Offset: 0x00109454
		// (set) Token: 0x06004027 RID: 16423 RVA: 0x0010A486 File Offset: 0x00109486
		[Localizable(true)]
		[WebSysDescription("CreateUserWizard_DuplicateEmailErrorMessage")]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultDuplicateEmailErrorMessage")]
		public virtual string DuplicateEmailErrorMessage
		{
			get
			{
				object obj = this.ViewState["DuplicateEmailErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultDuplicateEmailErrorMessage");
			}
			set
			{
				this.ViewState["DuplicateEmailErrorMessage"] = value;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x0010A49C File Offset: 0x0010949C
		// (set) Token: 0x06004029 RID: 16425 RVA: 0x0010A4CE File Offset: 0x001094CE
		[Localizable(true)]
		[WebSysDefaultValue("CreateUserWizard_DefaultDuplicateUserNameErrorMessage")]
		[WebSysDescription("CreateUserWizard_DuplicateUserNameErrorMessage")]
		[WebCategory("Appearance")]
		public virtual string DuplicateUserNameErrorMessage
		{
			get
			{
				object obj = this.ViewState["DuplicateUserNameErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultDuplicateUserNameErrorMessage");
			}
			set
			{
				this.ViewState["DuplicateUserNameErrorMessage"] = value;
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x0010A4E4 File Offset: 0x001094E4
		// (set) Token: 0x0600402B RID: 16427 RVA: 0x0010A511 File Offset: 0x00109511
		[UrlProperty]
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("LoginControls_EditProfileIconUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x0600402C RID: 16428 RVA: 0x0010A524 File Offset: 0x00109524
		// (set) Token: 0x0600402D RID: 16429 RVA: 0x0010A551 File Offset: 0x00109551
		[Localizable(true)]
		[WebSysDescription("CreateUserWizard_EditProfileText")]
		[WebCategory("Links")]
		[DefaultValue("")]
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

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x0600402E RID: 16430 RVA: 0x0010A564 File Offset: 0x00109564
		// (set) Token: 0x0600402F RID: 16431 RVA: 0x0010A591 File Offset: 0x00109591
		[WebCategory("Links")]
		[WebSysDescription("CreateUserWizard_EditProfileUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
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

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06004030 RID: 16432 RVA: 0x0010A5A4 File Offset: 0x001095A4
		// (set) Token: 0x06004031 RID: 16433 RVA: 0x0010A5D1 File Offset: 0x001095D1
		[WebSysDescription("CreateUserWizard_Email")]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		public virtual string Email
		{
			get
			{
				object obj = this.ViewState["Email"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Email"] = value;
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06004032 RID: 16434 RVA: 0x0010A5E4 File Offset: 0x001095E4
		private string EmailInternal
		{
			get
			{
				string email = this.Email;
				if (string.IsNullOrEmpty(email) && this._createUserStepContainer != null)
				{
					ITextControl textControl = (ITextControl)this._createUserStepContainer.EmailTextBox;
					if (textControl != null)
					{
						return textControl.Text;
					}
				}
				return email;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06004033 RID: 16435 RVA: 0x0010A624 File Offset: 0x00109624
		// (set) Token: 0x06004034 RID: 16436 RVA: 0x0010A656 File Offset: 0x00109656
		[WebCategory("Appearance")]
		[WebSysDescription("CreateUserWizard_EmailLabelText")]
		[WebSysDefaultValue("CreateUserWizard_DefaultEmailLabelText")]
		[Localizable(true)]
		public virtual string EmailLabelText
		{
			get
			{
				object obj = this.ViewState["EmailLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultEmailLabelText");
			}
			set
			{
				this.ViewState["EmailLabelText"] = value;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x0010A66C File Offset: 0x0010966C
		// (set) Token: 0x06004036 RID: 16438 RVA: 0x0010A699 File Offset: 0x00109699
		[WebSysDescription("CreateUserWizard_EmailRegularExpression")]
		[WebSysDefaultValue("")]
		[WebCategory("Validation")]
		public virtual string EmailRegularExpression
		{
			get
			{
				object obj = this.ViewState["EmailRegularExpression"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["EmailRegularExpression"] = value;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x0010A6AC File Offset: 0x001096AC
		// (set) Token: 0x06004038 RID: 16440 RVA: 0x0010A6DE File Offset: 0x001096DE
		[WebCategory("Validation")]
		[WebSysDescription("CreateUserWizard_EmailRegularExpressionErrorMessage")]
		[WebSysDefaultValue("CreateUserWizard_DefaultEmailRegularExpressionErrorMessage")]
		public virtual string EmailRegularExpressionErrorMessage
		{
			get
			{
				object obj = this.ViewState["EmailRegularExpressionErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultEmailRegularExpressionErrorMessage");
			}
			set
			{
				this.ViewState["EmailRegularExpressionErrorMessage"] = value;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06004039 RID: 16441 RVA: 0x0010A6F4 File Offset: 0x001096F4
		// (set) Token: 0x0600403A RID: 16442 RVA: 0x0010A726 File Offset: 0x00109726
		[WebSysDescription("CreateUserWizard_EmailRequiredErrorMessage")]
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("CreateUserWizard_DefaultEmailRequiredErrorMessage")]
		public virtual string EmailRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["EmailRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultEmailRequiredErrorMessage");
			}
			set
			{
				this.ViewState["EmailRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x0600403B RID: 16443 RVA: 0x0010A73C File Offset: 0x0010973C
		// (set) Token: 0x0600403C RID: 16444 RVA: 0x0010A76E File Offset: 0x0010976E
		[WebSysDescription("CreateUserWizard_UnknownErrorMessage")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultUnknownErrorMessage")]
		public virtual string UnknownErrorMessage
		{
			get
			{
				object obj = this.ViewState["UnknownErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultUnknownErrorMessage");
			}
			set
			{
				this.ViewState["UnknownErrorMessage"] = value;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x0600403D RID: 16445 RVA: 0x0010A781 File Offset: 0x00109781
		[DefaultValue(null)]
		[WebSysDescription("CreateUserWizard_ErrorMessageStyle")]
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle ErrorMessageStyle
		{
			get
			{
				if (this._errorMessageStyle == null)
				{
					this._errorMessageStyle = new ErrorTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._errorMessageStyle).TrackViewState();
					}
				}
				return this._errorMessageStyle;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x0600403E RID: 16446 RVA: 0x0010A7B0 File Offset: 0x001097B0
		// (set) Token: 0x0600403F RID: 16447 RVA: 0x0010A7DD File Offset: 0x001097DD
		[WebSysDescription("LoginControls_HelpPageIconUrl")]
		[UrlProperty]
		[WebCategory("Links")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06004040 RID: 16448 RVA: 0x0010A7F0 File Offset: 0x001097F0
		// (set) Token: 0x06004041 RID: 16449 RVA: 0x0010A81D File Offset: 0x0010981D
		[Localizable(true)]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_HelpPageText")]
		[WebCategory("Links")]
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

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06004042 RID: 16450 RVA: 0x0010A830 File Offset: 0x00109830
		// (set) Token: 0x06004043 RID: 16451 RVA: 0x0010A85D File Offset: 0x0010985D
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("LoginControls_HelpPageUrl")]
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

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06004044 RID: 16452 RVA: 0x0010A870 File Offset: 0x00109870
		[WebCategory("Styles")]
		[NotifyParentProperty(true)]
		[WebSysDescription("WebControl_HyperLinkStyle")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06004045 RID: 16453 RVA: 0x0010A8A0 File Offset: 0x001098A0
		// (set) Token: 0x06004046 RID: 16454 RVA: 0x0010A8CD File Offset: 0x001098CD
		[WebSysDescription("WebControl_InstructionText")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
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

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x0010A8E0 File Offset: 0x001098E0
		[DefaultValue(null)]
		[WebCategory("Styles")]
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

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06004048 RID: 16456 RVA: 0x0010A910 File Offset: 0x00109910
		// (set) Token: 0x06004049 RID: 16457 RVA: 0x0010A942 File Offset: 0x00109942
		[Localizable(true)]
		[WebSysDescription("CreateUserWizard_InvalidAnswerErrorMessage")]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultInvalidAnswerErrorMessage")]
		public virtual string InvalidAnswerErrorMessage
		{
			get
			{
				object obj = this.ViewState["InvalidAnswerErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultInvalidAnswerErrorMessage");
			}
			set
			{
				this.ViewState["InvalidAnswerErrorMessage"] = value;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x0600404A RID: 16458 RVA: 0x0010A958 File Offset: 0x00109958
		// (set) Token: 0x0600404B RID: 16459 RVA: 0x0010A98A File Offset: 0x0010998A
		[WebSysDescription("CreateUserWizard_InvalidEmailErrorMessage")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultInvalidEmailErrorMessage")]
		public virtual string InvalidEmailErrorMessage
		{
			get
			{
				object obj = this.ViewState["InvalidEmailErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultInvalidEmailErrorMessage");
			}
			set
			{
				this.ViewState["InvalidEmailErrorMessage"] = value;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x0600404C RID: 16460 RVA: 0x0010A9A0 File Offset: 0x001099A0
		// (set) Token: 0x0600404D RID: 16461 RVA: 0x0010A9D2 File Offset: 0x001099D2
		[WebSysDefaultValue("CreateUserWizard_DefaultInvalidPasswordErrorMessage")]
		[Localizable(true)]
		[WebSysDescription("CreateUserWizard_InvalidPasswordErrorMessage")]
		[WebCategory("Appearance")]
		public virtual string InvalidPasswordErrorMessage
		{
			get
			{
				object obj = this.ViewState["InvalidPasswordErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultInvalidPasswordErrorMessage");
			}
			set
			{
				this.ViewState["InvalidPasswordErrorMessage"] = value;
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x0600404E RID: 16462 RVA: 0x0010A9E8 File Offset: 0x001099E8
		// (set) Token: 0x0600404F RID: 16463 RVA: 0x0010AA1A File Offset: 0x00109A1A
		[Localizable(true)]
		[WebSysDefaultValue("CreateUserWizard_DefaultInvalidQuestionErrorMessage")]
		[WebSysDescription("CreateUserWizard_InvalidQuestionErrorMessage")]
		[WebCategory("Appearance")]
		public virtual string InvalidQuestionErrorMessage
		{
			get
			{
				object obj = this.ViewState["InvalidQuestionErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultInvalidQuestionErrorMessage");
			}
			set
			{
				this.ViewState["InvalidQuestionErrorMessage"] = value;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06004050 RID: 16464 RVA: 0x0010AA2D File Offset: 0x00109A2D
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("LoginControls_LabelStyle")]
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
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

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06004051 RID: 16465 RVA: 0x0010AA5C File Offset: 0x00109A5C
		// (set) Token: 0x06004052 RID: 16466 RVA: 0x0010AA85 File Offset: 0x00109A85
		[WebSysDescription("CreateUserWizard_LoginCreatedUser")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		public virtual bool LoginCreatedUser
		{
			get
			{
				object obj = this.ViewState["LoginCreatedUser"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["LoginCreatedUser"] = value;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06004053 RID: 16467 RVA: 0x0010AA9D File Offset: 0x00109A9D
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("CreateUserWizard_MailDefinition")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06004054 RID: 16468 RVA: 0x0010AACC File Offset: 0x00109ACC
		// (set) Token: 0x06004055 RID: 16469 RVA: 0x0010AAF9 File Offset: 0x00109AF9
		[WebSysDescription("MembershipProvider_Name")]
		[WebCategory("Data")]
		[DefaultValue("")]
		[Themeable(false)]
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
				if (this.MembershipProvider != value)
				{
					this.ViewState["MembershipProvider"] = value;
					base.RequiresControlsRecreation();
				}
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06004056 RID: 16470 RVA: 0x0010AB20 File Offset: 0x00109B20
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

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06004057 RID: 16471 RVA: 0x0010AB38 File Offset: 0x00109B38
		private string PasswordInternal
		{
			get
			{
				string password = this.Password;
				if (string.IsNullOrEmpty(password) && !this.AutoGeneratePassword && this._createUserStepContainer != null)
				{
					ITextControl textControl = (ITextControl)this._createUserStepContainer.PasswordTextBox;
					if (textControl != null)
					{
						return textControl.Text;
					}
				}
				return password;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06004058 RID: 16472 RVA: 0x0010AB80 File Offset: 0x00109B80
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[WebSysDescription("CreateUserWizard_PasswordHintStyle")]
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

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06004059 RID: 16473 RVA: 0x0010ABB0 File Offset: 0x00109BB0
		// (set) Token: 0x0600405A RID: 16474 RVA: 0x0010ABDD File Offset: 0x00109BDD
		[WebSysDescription("ChangePassword_PasswordHintText")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("")]
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

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x0600405B RID: 16475 RVA: 0x0010ABF0 File Offset: 0x00109BF0
		// (set) Token: 0x0600405C RID: 16476 RVA: 0x0010AC22 File Offset: 0x00109C22
		[WebSysDefaultValue("LoginControls_DefaultPasswordLabelText")]
		[WebCategory("Appearance")]
		[WebSysDescription("LoginControls_PasswordLabelText")]
		[Localizable(true)]
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

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x0600405D RID: 16477 RVA: 0x0010AC38 File Offset: 0x00109C38
		// (set) Token: 0x0600405E RID: 16478 RVA: 0x0010AC65 File Offset: 0x00109C65
		[WebSysDefaultValue("")]
		[WebCategory("Validation")]
		[WebSysDescription("CreateUserWizard_PasswordRegularExpression")]
		public virtual string PasswordRegularExpression
		{
			get
			{
				object obj = this.ViewState["PasswordRegularExpression"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PasswordRegularExpression"] = value;
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x0600405F RID: 16479 RVA: 0x0010AC78 File Offset: 0x00109C78
		// (set) Token: 0x06004060 RID: 16480 RVA: 0x0010ACAA File Offset: 0x00109CAA
		[WebSysDescription("CreateUserWizard_PasswordRegularExpressionErrorMessage")]
		[WebCategory("Validation")]
		[WebSysDefaultValue("Password_InvalidPasswordErrorMessage")]
		public virtual string PasswordRegularExpressionErrorMessage
		{
			get
			{
				object obj = this.ViewState["PasswordRegularExpressionErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("Password_InvalidPasswordErrorMessage");
			}
			set
			{
				this.ViewState["PasswordRegularExpressionErrorMessage"] = value;
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x0010ACC0 File Offset: 0x00109CC0
		// (set) Token: 0x06004062 RID: 16482 RVA: 0x0010ACF2 File Offset: 0x00109CF2
		[WebSysDefaultValue("CreateUserWizard_DefaultPasswordRequiredErrorMessage")]
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDescription("CreateUserWizard_PasswordRequiredErrorMessage")]
		public virtual string PasswordRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["PasswordRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultPasswordRequiredErrorMessage");
			}
			set
			{
				this.ViewState["PasswordRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x0010AD08 File Offset: 0x00109D08
		// (set) Token: 0x06004064 RID: 16484 RVA: 0x0010AD35 File Offset: 0x00109D35
		[WebCategory("Appearance")]
		[Localizable(true)]
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("CreateUserWizard_Question")]
		public virtual string Question
		{
			get
			{
				object obj = this.ViewState["Question"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Question"] = value;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x0010AD48 File Offset: 0x00109D48
		private string QuestionInternal
		{
			get
			{
				string text = this.Question;
				if (string.IsNullOrEmpty(text) && this._createUserStepContainer != null)
				{
					ITextControl textControl = (ITextControl)this._createUserStepContainer.QuestionTextBox;
					if (textControl != null)
					{
						text = textControl.Text;
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					text = null;
				}
				return text;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06004066 RID: 16486 RVA: 0x0010AD92 File Offset: 0x00109D92
		[WebSysDescription("CreateUserWizard_QuestionAndAnswerRequired")]
		[WebCategory("Validation")]
		[DefaultValue(true)]
		protected internal bool QuestionAndAnswerRequired
		{
			get
			{
				if (base.DesignMode)
				{
					return this.CreateUserStep == null || this.CreateUserStep.ContentTemplate == null;
				}
				return LoginUtil.GetProvider(this.MembershipProvider).RequiresQuestionAndAnswer;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06004067 RID: 16487 RVA: 0x0010ADC8 File Offset: 0x00109DC8
		// (set) Token: 0x06004068 RID: 16488 RVA: 0x0010ADFA File Offset: 0x00109DFA
		[WebSysDescription("CreateUserWizard_QuestionLabelText")]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultQuestionLabelText")]
		[Localizable(true)]
		public virtual string QuestionLabelText
		{
			get
			{
				object obj = this.ViewState["QuestionLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultQuestionLabelText");
			}
			set
			{
				this.ViewState["QuestionLabelText"] = value;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06004069 RID: 16489 RVA: 0x0010AE10 File Offset: 0x00109E10
		// (set) Token: 0x0600406A RID: 16490 RVA: 0x0010AE42 File Offset: 0x00109E42
		[WebSysDefaultValue("CreateUserWizard_DefaultQuestionRequiredErrorMessage")]
		[WebSysDescription("CreateUserWizard_QuestionRequiredErrorMessage")]
		[WebCategory("Validation")]
		[Localizable(true)]
		public virtual string QuestionRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["QuestionRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultQuestionRequiredErrorMessage");
			}
			set
			{
				this.ViewState["QuestionRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x0600406B RID: 16491 RVA: 0x0010AE58 File Offset: 0x00109E58
		// (set) Token: 0x0600406C RID: 16492 RVA: 0x0010AE81 File Offset: 0x00109E81
		[WebSysDescription("CreateUserWizard_RequireEmail")]
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[Themeable(false)]
		public virtual bool RequireEmail
		{
			get
			{
				object obj = this.ViewState["RequireEmail"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (this.RequireEmail != value)
				{
					this.ViewState["RequireEmail"] = value;
				}
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x0010AEA2 File Offset: 0x00109EA2
		internal override bool ShowCustomNavigationTemplate
		{
			get
			{
				return base.ShowCustomNavigationTemplate || base.ActiveStep == this.CreateUserStep;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x0600406E RID: 16494 RVA: 0x0010AEBC File Offset: 0x00109EBC
		// (set) Token: 0x0600406F RID: 16495 RVA: 0x0010AEDA File Offset: 0x00109EDA
		[DefaultValue("")]
		public override string SkipLinkText
		{
			get
			{
				string skipLinkTextInternal = base.SkipLinkTextInternal;
				if (skipLinkTextInternal != null)
				{
					return skipLinkTextInternal;
				}
				return string.Empty;
			}
			set
			{
				base.SkipLinkText = value;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06004070 RID: 16496 RVA: 0x0010AEE3 File Offset: 0x00109EE3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("LoginControls_TextBoxStyle")]
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06004071 RID: 16497 RVA: 0x0010AF11 File Offset: 0x00109F11
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebCategory("Styles")]
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

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x0010AF40 File Offset: 0x00109F40
		// (set) Token: 0x06004073 RID: 16499 RVA: 0x0010AF6D File Offset: 0x00109F6D
		[DefaultValue("")]
		[WebSysDescription("UserName_InitialValue")]
		[WebCategory("Appearance")]
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

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06004074 RID: 16500 RVA: 0x0010AF80 File Offset: 0x00109F80
		private string UserNameInternal
		{
			get
			{
				string userName = this.UserName;
				if (string.IsNullOrEmpty(userName) && this._createUserStepContainer != null)
				{
					ITextControl textControl = (ITextControl)this._createUserStepContainer.UserNameTextBox;
					if (textControl != null)
					{
						return textControl.Text;
					}
				}
				return userName;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06004075 RID: 16501 RVA: 0x0010AFC0 File Offset: 0x00109FC0
		// (set) Token: 0x06004076 RID: 16502 RVA: 0x0010AFF2 File Offset: 0x00109FF2
		[WebSysDefaultValue("CreateUserWizard_DefaultUserNameLabelText")]
		[WebCategory("Appearance")]
		[Localizable(true)]
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
				return SR.GetString("CreateUserWizard_DefaultUserNameLabelText");
			}
			set
			{
				this.ViewState["UserNameLabelText"] = value;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06004077 RID: 16503 RVA: 0x0010B008 File Offset: 0x0010A008
		// (set) Token: 0x06004078 RID: 16504 RVA: 0x0010B03A File Offset: 0x0010A03A
		[WebSysDescription("ChangePassword_UserNameRequiredErrorMessage")]
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("CreateUserWizard_DefaultUserNameRequiredErrorMessage")]
		public virtual string UserNameRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["UserNameRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CreateUserWizard_DefaultUserNameRequiredErrorMessage");
			}
			set
			{
				this.ViewState["UserNameRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06004079 RID: 16505 RVA: 0x0010B04D File Offset: 0x0010A04D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("CreateUserWizard_ValidatorTextStyle")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[WebCategory("Styles")]
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

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x0010B07B File Offset: 0x0010A07B
		internal string ValidationGroup
		{
			get
			{
				if (this._validationGroup == null)
				{
					base.EnsureID();
					this._validationGroup = this.ID;
				}
				return this._validationGroup;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x0600407B RID: 16507 RVA: 0x0010B09D File Offset: 0x0010A09D
		[Editor("System.Web.UI.Design.WebControls.CreateUserWizardStepCollectionEditor,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public override WizardStepCollection WizardSteps
		{
			get
			{
				return base.WizardSteps;
			}
		}

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x0600407C RID: 16508 RVA: 0x0010B0A5 File Offset: 0x0010A0A5
		// (remove) Token: 0x0600407D RID: 16509 RVA: 0x0010B0B8 File Offset: 0x0010A0B8
		[WebSysDescription("CreateUserWizard_ContinueButtonClick")]
		[WebCategory("Action")]
		public event EventHandler ContinueButtonClick
		{
			add
			{
				base.Events.AddHandler(CreateUserWizard.EventButtonContinueClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(CreateUserWizard.EventButtonContinueClick, value);
			}
		}

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x0600407E RID: 16510 RVA: 0x0010B0CB File Offset: 0x0010A0CB
		// (remove) Token: 0x0600407F RID: 16511 RVA: 0x0010B0DE File Offset: 0x0010A0DE
		[WebCategory("Action")]
		[WebSysDescription("CreateUserWizard_CreatingUser")]
		public event LoginCancelEventHandler CreatingUser
		{
			add
			{
				base.Events.AddHandler(CreateUserWizard.EventCreatingUser, value);
			}
			remove
			{
				base.Events.RemoveHandler(CreateUserWizard.EventCreatingUser, value);
			}
		}

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06004080 RID: 16512 RVA: 0x0010B0F1 File Offset: 0x0010A0F1
		// (remove) Token: 0x06004081 RID: 16513 RVA: 0x0010B104 File Offset: 0x0010A104
		[WebSysDescription("CreateUserWizard_CreatedUser")]
		[WebCategory("Action")]
		public event EventHandler CreatedUser
		{
			add
			{
				base.Events.AddHandler(CreateUserWizard.EventCreatedUser, value);
			}
			remove
			{
				base.Events.RemoveHandler(CreateUserWizard.EventCreatedUser, value);
			}
		}

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06004082 RID: 16514 RVA: 0x0010B117 File Offset: 0x0010A117
		// (remove) Token: 0x06004083 RID: 16515 RVA: 0x0010B12A File Offset: 0x0010A12A
		[WebCategory("Action")]
		[WebSysDescription("CreateUserWizard_CreateUserError")]
		public event CreateUserErrorEventHandler CreateUserError
		{
			add
			{
				base.Events.AddHandler(CreateUserWizard.EventCreateUserError, value);
			}
			remove
			{
				base.Events.RemoveHandler(CreateUserWizard.EventCreateUserError, value);
			}
		}

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x06004084 RID: 16516 RVA: 0x0010B13D File Offset: 0x0010A13D
		// (remove) Token: 0x06004085 RID: 16517 RVA: 0x0010B150 File Offset: 0x0010A150
		[WebSysDescription("ChangePassword_SendingMail")]
		[WebCategory("Action")]
		public event MailMessageEventHandler SendingMail
		{
			add
			{
				base.Events.AddHandler(CreateUserWizard.EventSendingMail, value);
			}
			remove
			{
				base.Events.RemoveHandler(CreateUserWizard.EventSendingMail, value);
			}
		}

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06004086 RID: 16518 RVA: 0x0010B163 File Offset: 0x0010A163
		// (remove) Token: 0x06004087 RID: 16519 RVA: 0x0010B176 File Offset: 0x0010A176
		[WebSysDescription("CreateUserWizard_SendMailError")]
		[WebCategory("Action")]
		public event SendMailErrorEventHandler SendMailError
		{
			add
			{
				base.Events.AddHandler(CreateUserWizard.EventSendMailError, value);
			}
			remove
			{
				base.Events.RemoveHandler(CreateUserWizard.EventSendMailError, value);
			}
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x0010B189 File Offset: 0x0010A189
		private void AnswerTextChanged(object source, EventArgs e)
		{
			this.Answer = ((ITextControl)source).Text;
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x0010B19C File Offset: 0x0010A19C
		private void ApplyCommonCreateUserValues()
		{
			if (!string.IsNullOrEmpty(this.UserNameInternal))
			{
				ITextControl textControl = (ITextControl)this._createUserStepContainer.UserNameTextBox;
				if (textControl != null)
				{
					textControl.Text = this.UserNameInternal;
				}
			}
			if (!string.IsNullOrEmpty(this.EmailInternal))
			{
				ITextControl textControl2 = (ITextControl)this._createUserStepContainer.EmailTextBox;
				if (textControl2 != null)
				{
					textControl2.Text = this.EmailInternal;
				}
			}
			if (!string.IsNullOrEmpty(this.QuestionInternal))
			{
				ITextControl textControl3 = (ITextControl)this._createUserStepContainer.QuestionTextBox;
				if (textControl3 != null)
				{
					textControl3.Text = this.QuestionInternal;
				}
			}
			if (!string.IsNullOrEmpty(this.AnswerInternal))
			{
				ITextControl textControl4 = (ITextControl)this._createUserStepContainer.AnswerTextBox;
				if (textControl4 != null)
				{
					textControl4.Text = this.AnswerInternal;
				}
			}
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x0010B25D File Offset: 0x0010A25D
		internal override void ApplyControlProperties()
		{
			this.SetChildProperties();
			if (this.CreateUserStep.CustomNavigationTemplate == null)
			{
				this.SetDefaultCreateUserNavigationTemplateProperties();
			}
			base.ApplyControlProperties();
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x0010B280 File Offset: 0x0010A280
		private void ApplyDefaultCreateUserValues()
		{
			this._createUserStepContainer.UserNameLabel.Text = this.UserNameLabelText;
			WebControl webControl = (WebControl)this._createUserStepContainer.UserNameTextBox;
			webControl.TabIndex = this.TabIndex;
			webControl.AccessKey = this.AccessKey;
			this._createUserStepContainer.PasswordLabel.Text = this.PasswordLabelText;
			WebControl webControl2 = (WebControl)this._createUserStepContainer.PasswordTextBox;
			webControl2.TabIndex = this.TabIndex;
			this._createUserStepContainer.ConfirmPasswordLabel.Text = this.ConfirmPasswordLabelText;
			WebControl webControl3 = (WebControl)this._createUserStepContainer.ConfirmPasswordTextBox;
			webControl3.TabIndex = this.TabIndex;
			if (this._textBoxStyle != null)
			{
				webControl.ApplyStyle(this._textBoxStyle);
				webControl2.ApplyStyle(this._textBoxStyle);
				webControl3.ApplyStyle(this._textBoxStyle);
			}
			LoginUtil.ApplyStyleToLiteral(this._createUserStepContainer.Title, this.CreateUserStep.Title, this.TitleTextStyle, true);
			LoginUtil.ApplyStyleToLiteral(this._createUserStepContainer.InstructionLabel, this.InstructionText, this.InstructionTextStyle, true);
			LoginUtil.ApplyStyleToLiteral(this._createUserStepContainer.UserNameLabel, this.UserNameLabelText, this.LabelStyle, false);
			LoginUtil.ApplyStyleToLiteral(this._createUserStepContainer.PasswordLabel, this.PasswordLabelText, this.LabelStyle, false);
			LoginUtil.ApplyStyleToLiteral(this._createUserStepContainer.ConfirmPasswordLabel, this.ConfirmPasswordLabelText, this.LabelStyle, false);
			if (!string.IsNullOrEmpty(this.PasswordHintText) && !this.AutoGeneratePassword)
			{
				LoginUtil.ApplyStyleToLiteral(this._createUserStepContainer.PasswordHintLabel, this.PasswordHintText, this.PasswordHintStyle, false);
			}
			else
			{
				this._passwordHintTableRow.Visible = false;
			}
			bool flag = true;
			if (this.RequireEmail)
			{
				LoginUtil.ApplyStyleToLiteral(this._createUserStepContainer.EmailLabel, this.EmailLabelText, this.LabelStyle, false);
				WebControl webControl4 = (WebControl)this._createUserStepContainer.EmailTextBox;
				((ITextControl)webControl4).Text = this.Email;
				RequiredFieldValidator emailRequired = this._createUserStepContainer.EmailRequired;
				emailRequired.ToolTip = this.EmailRequiredErrorMessage;
				emailRequired.ErrorMessage = this.EmailRequiredErrorMessage;
				emailRequired.Enabled = flag;
				emailRequired.Visible = flag;
				if (this._validatorTextStyle != null)
				{
					emailRequired.ApplyStyle(this._validatorTextStyle);
				}
				webControl4.TabIndex = this.TabIndex;
				if (this._textBoxStyle != null)
				{
					webControl4.ApplyStyle(this._textBoxStyle);
				}
			}
			else
			{
				this._emailRow.Visible = false;
			}
			RequiredFieldValidator questionRequired = this._createUserStepContainer.QuestionRequired;
			RequiredFieldValidator answerRequired = this._createUserStepContainer.AnswerRequired;
			bool flag2 = flag && this.QuestionAndAnswerRequired;
			questionRequired.Enabled = flag2;
			questionRequired.Visible = flag2;
			answerRequired.Enabled = flag2;
			answerRequired.Visible = flag2;
			if (this.QuestionAndAnswerRequired)
			{
				LoginUtil.ApplyStyleToLiteral(this._createUserStepContainer.QuestionLabel, this.QuestionLabelText, this.LabelStyle, false);
				WebControl webControl5 = (WebControl)this._createUserStepContainer.QuestionTextBox;
				((ITextControl)webControl5).Text = this.Question;
				webControl5.TabIndex = this.TabIndex;
				LoginUtil.ApplyStyleToLiteral(this._createUserStepContainer.AnswerLabel, this.AnswerLabelText, this.LabelStyle, false);
				WebControl webControl6 = (WebControl)this._createUserStepContainer.AnswerTextBox;
				((ITextControl)webControl6).Text = this.Answer;
				webControl6.TabIndex = this.TabIndex;
				if (this._textBoxStyle != null)
				{
					webControl5.ApplyStyle(this._textBoxStyle);
					webControl6.ApplyStyle(this._textBoxStyle);
				}
				questionRequired.ToolTip = this.QuestionRequiredErrorMessage;
				questionRequired.ErrorMessage = this.QuestionRequiredErrorMessage;
				answerRequired.ToolTip = this.AnswerRequiredErrorMessage;
				answerRequired.ErrorMessage = this.AnswerRequiredErrorMessage;
				if (this._validatorTextStyle != null)
				{
					questionRequired.ApplyStyle(this._validatorTextStyle);
					answerRequired.ApplyStyle(this._validatorTextStyle);
				}
			}
			else
			{
				this._questionRow.Visible = false;
				this._answerRow.Visible = false;
			}
			if (this._defaultCreateUserNavigationTemplate != null)
			{
				((Wizard.BaseNavigationTemplateContainer)this.CreateUserStep.CustomNavigationTemplateContainer).NextButton = this._defaultCreateUserNavigationTemplate.CreateUserButton;
				((Wizard.BaseNavigationTemplateContainer)this.CreateUserStep.CustomNavigationTemplateContainer).CancelButton = this._defaultCreateUserNavigationTemplate.CancelButton;
			}
			RequiredFieldValidator passwordRequired = this._createUserStepContainer.PasswordRequired;
			RequiredFieldValidator confirmPasswordRequired = this._createUserStepContainer.ConfirmPasswordRequired;
			CompareValidator passwordCompareValidator = this._createUserStepContainer.PasswordCompareValidator;
			RegularExpressionValidator passwordRegExpValidator = this._createUserStepContainer.PasswordRegExpValidator;
			bool flag3 = flag && !this.AutoGeneratePassword;
			passwordRequired.Enabled = flag3;
			passwordRequired.Visible = flag3;
			confirmPasswordRequired.Enabled = flag3;
			confirmPasswordRequired.Visible = flag3;
			passwordCompareValidator.Enabled = flag3;
			passwordCompareValidator.Visible = flag3;
			bool flag4 = flag3 && this.PasswordRegularExpression.Length > 0;
			passwordRegExpValidator.Enabled = flag4;
			passwordRegExpValidator.Visible = flag4;
			if (!flag)
			{
				this._passwordRegExpRow.Visible = false;
				this._passwordCompareRow.Visible = false;
				this._emailRegExpRow.Visible = false;
			}
			if (this.AutoGeneratePassword)
			{
				this._passwordTableRow.Visible = false;
				this._confirmPasswordTableRow.Visible = false;
				this._passwordRegExpRow.Visible = false;
				this._passwordCompareRow.Visible = false;
			}
			else
			{
				passwordRequired.ErrorMessage = this.PasswordRequiredErrorMessage;
				passwordRequired.ToolTip = this.PasswordRequiredErrorMessage;
				confirmPasswordRequired.ErrorMessage = this.ConfirmPasswordRequiredErrorMessage;
				confirmPasswordRequired.ToolTip = this.ConfirmPasswordRequiredErrorMessage;
				passwordCompareValidator.ErrorMessage = this.ConfirmPasswordCompareErrorMessage;
				if (this._validatorTextStyle != null)
				{
					passwordRequired.ApplyStyle(this._validatorTextStyle);
					confirmPasswordRequired.ApplyStyle(this._validatorTextStyle);
					passwordCompareValidator.ApplyStyle(this._validatorTextStyle);
				}
				if (flag4)
				{
					passwordRegExpValidator.ValidationExpression = this.PasswordRegularExpression;
					passwordRegExpValidator.ErrorMessage = this.PasswordRegularExpressionErrorMessage;
					if (this._validatorTextStyle != null)
					{
						passwordRegExpValidator.ApplyStyle(this._validatorTextStyle);
					}
				}
				else
				{
					this._passwordRegExpRow.Visible = false;
				}
			}
			RequiredFieldValidator userNameRequired = this._createUserStepContainer.UserNameRequired;
			userNameRequired.ErrorMessage = this.UserNameRequiredErrorMessage;
			userNameRequired.ToolTip = this.UserNameRequiredErrorMessage;
			userNameRequired.Enabled = flag;
			userNameRequired.Visible = flag;
			if (this._validatorTextStyle != null)
			{
				userNameRequired.ApplyStyle(this._validatorTextStyle);
			}
			bool flag5 = flag && this.EmailRegularExpression.Length > 0 && this.RequireEmail;
			RegularExpressionValidator emailRegExpValidator = this._createUserStepContainer.EmailRegExpValidator;
			emailRegExpValidator.Enabled = flag5;
			emailRegExpValidator.Visible = flag5;
			if (this.EmailRegularExpression.Length > 0 && this.RequireEmail)
			{
				emailRegExpValidator.ValidationExpression = this.EmailRegularExpression;
				emailRegExpValidator.ErrorMessage = this.EmailRegularExpressionErrorMessage;
				if (this._validatorTextStyle != null)
				{
					emailRegExpValidator.ApplyStyle(this._validatorTextStyle);
				}
			}
			else
			{
				this._emailRegExpRow.Visible = false;
			}
			string helpPageText = this.HelpPageText;
			bool flag6 = helpPageText.Length > 0;
			HyperLink helpPageLink = this._createUserStepContainer.HelpPageLink;
			Image helpPageIcon = this._createUserStepContainer.HelpPageIcon;
			helpPageLink.Visible = flag6;
			if (flag6)
			{
				helpPageLink.Text = helpPageText;
				helpPageLink.NavigateUrl = this.HelpPageUrl;
				helpPageLink.TabIndex = this.TabIndex;
			}
			string helpPageIconUrl = this.HelpPageIconUrl;
			bool flag7 = helpPageIconUrl.Length > 0;
			helpPageIcon.Visible = flag7;
			if (flag7)
			{
				helpPageIcon.ImageUrl = helpPageIconUrl;
				helpPageIcon.AlternateText = helpPageText;
			}
			LoginUtil.SetTableCellVisible(helpPageLink, flag6 || flag7);
			if (this._hyperLinkStyle != null && (flag6 || flag7))
			{
				TableItemStyle tableItemStyle = new TableItemStyle();
				tableItemStyle.CopyFrom(this._hyperLinkStyle);
				tableItemStyle.Font.Reset();
				LoginUtil.SetTableCellStyle(helpPageLink, tableItemStyle);
				helpPageLink.Font.CopyFrom(this._hyperLinkStyle.Font);
				helpPageLink.ForeColor = this._hyperLinkStyle.ForeColor;
			}
			Control errorMessageLabel = this._createUserStepContainer.ErrorMessageLabel;
			if (errorMessageLabel != null)
			{
				if (this._failure && !string.IsNullOrEmpty(this._unknownErrorMessage))
				{
					((ITextControl)errorMessageLabel).Text = this._unknownErrorMessage;
					LoginUtil.SetTableCellStyle(errorMessageLabel, this.ErrorMessageStyle);
					LoginUtil.SetTableCellVisible(errorMessageLabel, true);
					return;
				}
				LoginUtil.SetTableCellVisible(errorMessageLabel, false);
			}
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x0010BAC0 File Offset: 0x0010AAC0
		private void ApplyCompleteValues()
		{
			LoginUtil.ApplyStyleToLiteral(this._completeStepContainer.SuccessTextLabel, this.CompleteSuccessText, this._completeSuccessTextStyle, true);
			switch (this.ContinueButtonType)
			{
			case ButtonType.Button:
				this._completeStepContainer.ContinueLinkButton.Visible = false;
				this._completeStepContainer.ContinueImageButton.Visible = false;
				this._completeStepContainer.ContinuePushButton.Text = this.ContinueButtonText;
				this._completeStepContainer.ContinuePushButton.ValidationGroup = this.ValidationGroup;
				this._completeStepContainer.ContinuePushButton.TabIndex = this.TabIndex;
				this._completeStepContainer.ContinuePushButton.AccessKey = this.AccessKey;
				break;
			case ButtonType.Image:
				this._completeStepContainer.ContinueLinkButton.Visible = false;
				this._completeStepContainer.ContinuePushButton.Visible = false;
				this._completeStepContainer.ContinueImageButton.ImageUrl = this.ContinueButtonImageUrl;
				this._completeStepContainer.ContinueImageButton.AlternateText = this.ContinueButtonText;
				this._completeStepContainer.ContinueImageButton.ValidationGroup = this.ValidationGroup;
				this._completeStepContainer.ContinueImageButton.TabIndex = this.TabIndex;
				this._completeStepContainer.ContinueImageButton.AccessKey = this.AccessKey;
				break;
			case ButtonType.Link:
				this._completeStepContainer.ContinuePushButton.Visible = false;
				this._completeStepContainer.ContinueImageButton.Visible = false;
				this._completeStepContainer.ContinueLinkButton.Text = this.ContinueButtonText;
				this._completeStepContainer.ContinueLinkButton.ValidationGroup = this.ValidationGroup;
				this._completeStepContainer.ContinueLinkButton.TabIndex = this.TabIndex;
				this._completeStepContainer.ContinueLinkButton.AccessKey = this.AccessKey;
				break;
			}
			if (!base.NavigationButtonStyle.IsEmpty)
			{
				this._completeStepContainer.ContinuePushButton.ApplyStyle(base.NavigationButtonStyle);
				this._completeStepContainer.ContinueImageButton.ApplyStyle(base.NavigationButtonStyle);
				this._completeStepContainer.ContinueLinkButton.ApplyStyle(base.NavigationButtonStyle);
			}
			if (this._continueButtonStyle != null)
			{
				this._completeStepContainer.ContinuePushButton.ApplyStyle(this._continueButtonStyle);
				this._completeStepContainer.ContinueImageButton.ApplyStyle(this._continueButtonStyle);
				this._completeStepContainer.ContinueLinkButton.ApplyStyle(this._continueButtonStyle);
			}
			LoginUtil.ApplyStyleToLiteral(this._completeStepContainer.Title, this.CompleteStep.Title, this._titleTextStyle, true);
			string editProfileText = this.EditProfileText;
			bool flag = editProfileText.Length > 0;
			HyperLink editProfileLink = this._completeStepContainer.EditProfileLink;
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
			string editProfileIconUrl = this.EditProfileIconUrl;
			bool flag2 = editProfileIconUrl.Length > 0;
			Image editProfileIcon = this._completeStepContainer.EditProfileIcon;
			editProfileIcon.Visible = flag2;
			if (flag2)
			{
				editProfileIcon.ImageUrl = editProfileIconUrl;
				editProfileIcon.AlternateText = this.EditProfileText;
			}
			LoginUtil.SetTableCellVisible(editProfileLink, flag || flag2);
			Table layoutTable = ((CreateUserWizard.CompleteStepContainer)this.CompleteStep.ContentTemplateContainer).LayoutTable;
			layoutTable.Height = this.Height;
			layoutTable.Width = this.Width;
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x0010BE70 File Offset: 0x0010AE70
		private bool AttemptCreateUser()
		{
			if (this.Page != null && !this.Page.IsValid)
			{
				return false;
			}
			LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
			this.OnCreatingUser(loginCancelEventArgs);
			if (loginCancelEventArgs.Cancel)
			{
				return false;
			}
			MembershipProvider provider = LoginUtil.GetProvider(this.MembershipProvider);
			if (this.AutoGeneratePassword)
			{
				int length = Math.Max(10, Membership.MinRequiredPasswordLength);
				this._password = Membership.GeneratePassword(length, Membership.MinRequiredNonAlphanumericCharacters);
			}
			MembershipCreateStatus membershipCreateStatus;
			provider.CreateUser(this.UserNameInternal, this.PasswordInternal, this.EmailInternal, this.QuestionInternal, this.AnswerInternal, !this.DisableCreatedUser, null, out membershipCreateStatus);
			if (membershipCreateStatus == MembershipCreateStatus.Success)
			{
				this.OnCreatedUser(EventArgs.Empty);
				if (this._mailDefinition != null && !string.IsNullOrEmpty(this.EmailInternal))
				{
					LoginUtil.SendPasswordMail(this.EmailInternal, this.UserNameInternal, this.PasswordInternal, this.MailDefinition, null, null, new LoginUtil.OnSendingMailDelegate(this.OnSendingMail), new LoginUtil.OnSendMailErrorDelegate(this.OnSendMailError), this);
				}
				this.CreateUserStep.AllowReturnInternal = false;
				if (this.LoginCreatedUser)
				{
					this.AttemptLogin();
				}
				return true;
			}
			this.OnCreateUserError(new CreateUserErrorEventArgs(membershipCreateStatus));
			switch (membershipCreateStatus)
			{
			case MembershipCreateStatus.InvalidPassword:
			{
				string text = this.InvalidPasswordErrorMessage;
				if (!string.IsNullOrEmpty(text))
				{
					text = string.Format(CultureInfo.InvariantCulture, text, new object[]
					{
						provider.MinRequiredPasswordLength,
						provider.MinRequiredNonAlphanumericCharacters
					});
				}
				this._unknownErrorMessage = text;
				break;
			}
			case MembershipCreateStatus.InvalidQuestion:
				this._unknownErrorMessage = this.InvalidQuestionErrorMessage;
				break;
			case MembershipCreateStatus.InvalidAnswer:
				this._unknownErrorMessage = this.InvalidAnswerErrorMessage;
				break;
			case MembershipCreateStatus.InvalidEmail:
				this._unknownErrorMessage = this.InvalidEmailErrorMessage;
				break;
			case MembershipCreateStatus.DuplicateUserName:
				this._unknownErrorMessage = this.DuplicateUserNameErrorMessage;
				break;
			case MembershipCreateStatus.DuplicateEmail:
				this._unknownErrorMessage = this.DuplicateEmailErrorMessage;
				break;
			default:
				this._unknownErrorMessage = this.UnknownErrorMessage;
				break;
			}
			return false;
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x0010C068 File Offset: 0x0010B068
		private void AttemptLogin()
		{
			MembershipProvider provider = LoginUtil.GetProvider(this.MembershipProvider);
			if (provider.ValidateUser(this.UserName, this.Password))
			{
				FormsAuthentication.SetAuthCookie(this.UserNameInternal, false);
			}
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x0010C0A1 File Offset: 0x0010B0A1
		private void ConfirmPasswordTextChanged(object source, EventArgs e)
		{
			if (!this.AutoGeneratePassword)
			{
				this._confirmPassword = ((ITextControl)source).Text;
			}
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x0010C0BC File Offset: 0x0010B0BC
		protected internal override void CreateChildControls()
		{
			this._createUserStep = null;
			this._completeStep = null;
			base.CreateChildControls();
			this.UpdateValidators();
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x0010C0D8 File Offset: 0x0010B0D8
		protected override void CreateControlHierarchy()
		{
			this.EnsureCreateUserSteps();
			base.CreateControlHierarchy();
			IEditableTextControl editableTextControl = this._createUserStepContainer.UserNameTextBox as IEditableTextControl;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += this.UserNameTextChanged;
			}
			IEditableTextControl editableTextControl2 = this._createUserStepContainer.EmailTextBox as IEditableTextControl;
			if (editableTextControl2 != null)
			{
				editableTextControl2.TextChanged += this.EmailTextChanged;
			}
			IEditableTextControl editableTextControl3 = this._createUserStepContainer.QuestionTextBox as IEditableTextControl;
			if (editableTextControl3 != null)
			{
				editableTextControl3.TextChanged += this.QuestionTextChanged;
			}
			IEditableTextControl editableTextControl4 = this._createUserStepContainer.AnswerTextBox as IEditableTextControl;
			if (editableTextControl4 != null)
			{
				editableTextControl4.TextChanged += this.AnswerTextChanged;
			}
			IEditableTextControl editableTextControl5 = this._createUserStepContainer.PasswordTextBox as IEditableTextControl;
			if (editableTextControl5 != null)
			{
				editableTextControl5.TextChanged += this.PasswordTextChanged;
			}
			editableTextControl5 = (this._createUserStepContainer.ConfirmPasswordTextBox as IEditableTextControl);
			if (editableTextControl5 != null)
			{
				editableTextControl5.TextChanged += this.ConfirmPasswordTextChanged;
			}
			this.ApplyCommonCreateUserValues();
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x0010C1E1 File Offset: 0x0010B1E1
		internal override ITemplate CreateDefaultSideBarTemplate()
		{
			return new CreateUserWizard.DefaultSideBarTemplate();
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x0010C1E8 File Offset: 0x0010B1E8
		internal override ITemplate CreateDefaultDataListItemTemplate()
		{
			return new CreateUserWizard.DataListItemTemplate();
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x0010C1F0 File Offset: 0x0010B1F0
		private static LabelLiteral CreateLabelLiteral(Control control)
		{
			LabelLiteral labelLiteral = new LabelLiteral(control);
			labelLiteral.PreventAutoID();
			return labelLiteral;
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x0010C20C File Offset: 0x0010B20C
		private static Literal CreateLiteral()
		{
			Literal literal = new Literal();
			literal.PreventAutoID();
			return literal;
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x0010C228 File Offset: 0x0010B228
		private static RequiredFieldValidator CreateRequiredFieldValidator(string id, string validationGroup, TextBox textBox, bool enableValidation)
		{
			return new RequiredFieldValidator
			{
				ID = id,
				ControlToValidate = textBox.ID,
				ValidationGroup = validationGroup,
				Display = ValidatorDisplay.Static,
				Text = SR.GetString("LoginControls_DefaultRequiredFieldValidatorText"),
				Enabled = enableValidation,
				Visible = enableValidation
			};
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x0010C27C File Offset: 0x0010B27C
		private static Table CreateTable()
		{
			Table table = new Table();
			table.Width = Unit.Percentage(100.0);
			table.Height = Unit.Percentage(100.0);
			table.PreventAutoID();
			return table;
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x0010C2C0 File Offset: 0x0010B2C0
		private static TableCell CreateTableCell()
		{
			TableCell tableCell = new TableCell();
			tableCell.PreventAutoID();
			return tableCell;
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x0010C2DC File Offset: 0x0010B2DC
		private static TableRow CreateTableRow()
		{
			TableRow tableRow = new LoginUtil.DisappearingTableRow();
			tableRow.PreventAutoID();
			return tableRow;
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x0010C2F8 File Offset: 0x0010B2F8
		internal override void CreateCustomNavigationTemplates()
		{
			for (int i = 0; i < this.WizardSteps.Count; i++)
			{
				TemplatedWizardStep templatedWizardStep = this.WizardSteps[i] as TemplatedWizardStep;
				if (templatedWizardStep != null)
				{
					string customContainerID = base.GetCustomContainerID(i);
					Wizard.BaseNavigationTemplateContainer baseNavigationTemplateContainer = base.CreateBaseNavigationTemplateContainer(customContainerID);
					if (templatedWizardStep.CustomNavigationTemplate != null)
					{
						templatedWizardStep.CustomNavigationTemplate.InstantiateIn(baseNavigationTemplateContainer);
						templatedWizardStep.CustomNavigationTemplateContainer = baseNavigationTemplateContainer;
						baseNavigationTemplateContainer.SetEnableTheming();
					}
					else if (templatedWizardStep == this.CreateUserStep)
					{
						ITemplate template = new CreateUserWizard.DefaultCreateUserNavigationTemplate(this);
						template.InstantiateIn(baseNavigationTemplateContainer);
						templatedWizardStep.CustomNavigationTemplateContainer = baseNavigationTemplateContainer;
						baseNavigationTemplateContainer.RegisterButtonCommandEvents();
					}
					base.CustomNavigationContainers[templatedWizardStep] = baseNavigationTemplateContainer;
				}
			}
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x0010C39C File Offset: 0x0010B39C
		internal override void DataListItemDataBound(object sender, DataListItemEventArgs e)
		{
			DataListItem item = e.Item;
			if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem && item.ItemType != ListItemType.SelectedItem && item.ItemType != ListItemType.EditItem)
			{
				return;
			}
			IButtonControl buttonControl = item.FindControl(Wizard.SideBarButtonID) as IButtonControl;
			if (buttonControl != null)
			{
				base.DataListItemDataBound(sender, e);
				return;
			}
			Label label = item.FindControl("SideBarLabel") as Label;
			if (label != null)
			{
				label.MergeStyle(base.SideBarButtonStyle);
				WizardStepBase wizardStepBase = item.DataItem as WizardStepBase;
				if (wizardStepBase != null)
				{
					base.RegisterSideBarDataListForRender();
					if (wizardStepBase.Title.Length > 0)
					{
						label.Text = wizardStepBase.Title;
						return;
					}
					label.Text = wizardStepBase.ID;
				}
				return;
			}
			if (!base.DesignMode)
			{
				throw new InvalidOperationException(SR.GetString("CreateUserWizard_SideBar_Label_Not_Found", new object[]
				{
					Wizard.DataListID,
					"SideBarLabel"
				}));
			}
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x0010C483 File Offset: 0x0010B483
		private void EmailTextChanged(object source, EventArgs e)
		{
			this.Email = ((ITextControl)source).Text;
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x0010C498 File Offset: 0x0010B498
		private void EnsureCreateUserSteps()
		{
			bool flag = false;
			bool flag2 = false;
			foreach (object obj in this.WizardSteps)
			{
				WizardStepBase wizardStepBase = (WizardStepBase)obj;
				if (wizardStepBase is CreateUserWizardStep)
				{
					if (flag)
					{
						throw new HttpException(SR.GetString("CreateUserWizard_DuplicateCreateUserWizardStep"));
					}
					flag = true;
					this._createUserStep = (CreateUserWizardStep)wizardStepBase;
				}
				else if (wizardStepBase is CompleteWizardStep)
				{
					if (flag2)
					{
						throw new HttpException(SR.GetString("CreateUserWizard_DuplicateCompleteWizardStep"));
					}
					flag2 = true;
					this._completeStep = (CompleteWizardStep)wizardStepBase;
				}
			}
			if (!flag)
			{
				this._createUserStep = new CreateUserWizardStep();
				this._createUserStep.ApplyStyleSheetSkin(this.Page);
				this.WizardSteps.AddAt(0, this._createUserStep);
				this._createUserStep.Active = true;
			}
			if (!flag2)
			{
				this._completeStep = new CompleteWizardStep();
				this._completeStep.ApplyStyleSheetSkin(this.Page);
				this.WizardSteps.Add(this._completeStep);
			}
			if (this.ActiveStepIndex == -1)
			{
				this.ActiveStepIndex = 0;
			}
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x0010C5C4 File Offset: 0x0010B5C4
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			IDictionary designModeState = base.GetDesignModeState();
			WizardStepBase activeStep = base.ActiveStep;
			if (activeStep != null && activeStep == this.CreateUserStep)
			{
				designModeState["CustomNavigationControls"] = ((Wizard.BaseNavigationTemplateContainer)base.CustomNavigationContainers[base.ActiveStep]).Controls;
			}
			Control errorMessageLabel = this._createUserStepContainer.ErrorMessageLabel;
			if (errorMessageLabel != null)
			{
				LoginUtil.SetTableCellVisible(errorMessageLabel, true);
			}
			return designModeState;
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x0010C628 File Offset: 0x0010B628
		internal override void InstantiateStepContentTemplates()
		{
			foreach (object obj in this.WizardSteps)
			{
				WizardStepBase wizardStepBase = (WizardStepBase)obj;
				if (wizardStepBase == this.CreateUserStep)
				{
					wizardStepBase.Controls.Clear();
					this._createUserStepContainer = new CreateUserWizard.CreateUserStepContainer(this);
					this._createUserStepContainer.ID = "CreateUserStepContainer";
					ITemplate template = ((CreateUserWizardStep)wizardStepBase).ContentTemplate;
					if (template == null)
					{
						template = new CreateUserWizard.DefaultCreateUserContentTemplate(this);
					}
					else
					{
						this._createUserStepContainer.SetEnableTheming();
					}
					template.InstantiateIn(this._createUserStepContainer.InnerCell);
					((CreateUserWizardStep)wizardStepBase).ContentTemplateContainer = this._createUserStepContainer;
					wizardStepBase.Controls.Add(this._createUserStepContainer);
				}
				else if (wizardStepBase == this.CompleteStep)
				{
					wizardStepBase.Controls.Clear();
					this._completeStepContainer = new CreateUserWizard.CompleteStepContainer(this);
					this._completeStepContainer.ID = "CompleteStepContainer";
					ITemplate template2 = ((CompleteWizardStep)wizardStepBase).ContentTemplate;
					if (template2 == null)
					{
						template2 = new CreateUserWizard.DefaultCompleteStepContentTemplate(this);
					}
					else
					{
						this._completeStepContainer.SetEnableTheming();
					}
					template2.InstantiateIn(this._completeStepContainer.InnerCell);
					((CompleteWizardStep)wizardStepBase).ContentTemplateContainer = this._completeStepContainer;
					wizardStepBase.Controls.Add(this._completeStepContainer);
				}
				else if (wizardStepBase is TemplatedWizardStep)
				{
					base.InstantiateStepContentTemplate((TemplatedWizardStep)wizardStepBase);
				}
			}
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x0010C7BC File Offset: 0x0010B7BC
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
			}
			else
			{
				object[] array = (object[])savedState;
				if (array.Length != 13)
				{
					throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
				}
				base.LoadViewState(array[0]);
				if (array[1] != null)
				{
					((IStateManager)this.CreateUserButtonStyle).LoadViewState(array[1]);
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
					((IStateManager)this.ErrorMessageStyle).LoadViewState(array[7]);
				}
				if (array[8] != null)
				{
					((IStateManager)this.PasswordHintStyle).LoadViewState(array[8]);
				}
				if (array[9] != null)
				{
					((IStateManager)this.MailDefinition).LoadViewState(array[9]);
				}
				if (array[10] != null)
				{
					((IStateManager)this.ContinueButtonStyle).LoadViewState(array[10]);
				}
				if (array[11] != null)
				{
					((IStateManager)this.CompleteSuccessTextStyle).LoadViewState(array[11]);
				}
				if (array[12] != null)
				{
					((IStateManager)this.ValidatorTextStyle).LoadViewState(array[12]);
				}
			}
			this.UpdateValidators();
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x0010C8F4 File Offset: 0x0010B8F4
		private void UpdateValidators()
		{
			if (base.DesignMode)
			{
				return;
			}
			if (this.DefaultCreateUserStep && this._createUserStepContainer != null)
			{
				if (this.AutoGeneratePassword)
				{
					BaseValidator confirmPasswordRequired = this._createUserStepContainer.ConfirmPasswordRequired;
					if (confirmPasswordRequired != null)
					{
						this.Page.Validators.Remove(confirmPasswordRequired);
						confirmPasswordRequired.Enabled = false;
					}
					BaseValidator passwordRequired = this._createUserStepContainer.PasswordRequired;
					if (passwordRequired != null)
					{
						this.Page.Validators.Remove(passwordRequired);
						passwordRequired.Enabled = false;
					}
					BaseValidator passwordRegExpValidator = this._createUserStepContainer.PasswordRegExpValidator;
					if (passwordRegExpValidator != null)
					{
						this.Page.Validators.Remove(passwordRegExpValidator);
						passwordRegExpValidator.Enabled = false;
					}
				}
				else if (this.PasswordRegularExpression.Length <= 0)
				{
					BaseValidator passwordRegExpValidator2 = this._createUserStepContainer.PasswordRegExpValidator;
					if (passwordRegExpValidator2 != null)
					{
						if (this.Page != null)
						{
							this.Page.Validators.Remove(passwordRegExpValidator2);
						}
						passwordRegExpValidator2.Enabled = false;
					}
				}
				if (!this.RequireEmail)
				{
					BaseValidator emailRequired = this._createUserStepContainer.EmailRequired;
					if (emailRequired != null)
					{
						if (this.Page != null)
						{
							this.Page.Validators.Remove(emailRequired);
						}
						emailRequired.Enabled = false;
					}
					BaseValidator emailRegExpValidator = this._createUserStepContainer.EmailRegExpValidator;
					if (emailRegExpValidator != null)
					{
						if (this.Page != null)
						{
							this.Page.Validators.Remove(emailRegExpValidator);
						}
						emailRegExpValidator.Enabled = false;
					}
				}
				else if (this.EmailRegularExpression.Length <= 0)
				{
					BaseValidator emailRegExpValidator2 = this._createUserStepContainer.EmailRegExpValidator;
					if (emailRegExpValidator2 != null)
					{
						if (this.Page != null)
						{
							this.Page.Validators.Remove(emailRegExpValidator2);
						}
						emailRegExpValidator2.Enabled = false;
					}
				}
				if (!this.QuestionAndAnswerRequired)
				{
					BaseValidator questionRequired = this._createUserStepContainer.QuestionRequired;
					if (questionRequired != null)
					{
						if (this.Page != null)
						{
							this.Page.Validators.Remove(questionRequired);
						}
						questionRequired.Enabled = false;
					}
					BaseValidator answerRequired = this._createUserStepContainer.AnswerRequired;
					if (answerRequired != null)
					{
						if (this.Page != null)
						{
							this.Page.Validators.Remove(answerRequired);
						}
						answerRequired.Enabled = false;
					}
				}
			}
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x0010CAFC File Offset: 0x0010BAFC
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				CommandEventArgs commandEventArgs = (CommandEventArgs)e;
				if (commandEventArgs.CommandName.Equals(CreateUserWizard.ContinueButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
				{
					this.OnContinueButtonClick(EventArgs.Empty);
					return true;
				}
			}
			return base.OnBubbleEvent(source, e);
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x0010CB40 File Offset: 0x0010BB40
		protected virtual void OnContinueButtonClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CreateUserWizard.EventButtonContinueClick];
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

		// Token: 0x060040A4 RID: 16548 RVA: 0x0010CB98 File Offset: 0x0010BB98
		protected virtual void OnCreatedUser(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CreateUserWizard.EventCreatedUser];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x0010CBC8 File Offset: 0x0010BBC8
		protected virtual void OnCreateUserError(CreateUserErrorEventArgs e)
		{
			CreateUserErrorEventHandler createUserErrorEventHandler = (CreateUserErrorEventHandler)base.Events[CreateUserWizard.EventCreateUserError];
			if (createUserErrorEventHandler != null)
			{
				createUserErrorEventHandler(this, e);
			}
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x0010CBF8 File Offset: 0x0010BBF8
		protected virtual void OnCreatingUser(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[CreateUserWizard.EventCreatingUser];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x0010CC28 File Offset: 0x0010BC28
		protected override void OnNextButtonClick(WizardNavigationEventArgs e)
		{
			if (this.WizardSteps[e.CurrentStepIndex] == this._createUserStep)
			{
				e.Cancel = (this.Page != null && !this.Page.IsValid);
				if (!e.Cancel)
				{
					this._failure = !this.AttemptCreateUser();
					if (this._failure)
					{
						e.Cancel = true;
						ITextControl textControl = (ITextControl)this._createUserStepContainer.ErrorMessageLabel;
						if (textControl != null && !string.IsNullOrEmpty(this._unknownErrorMessage))
						{
							textControl.Text = this._unknownErrorMessage;
							if (textControl is Control)
							{
								((Control)textControl).Visible = true;
							}
						}
					}
				}
			}
			base.OnNextButtonClick(e);
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x0010CCE0 File Offset: 0x0010BCE0
		protected internal override void OnPreRender(EventArgs e)
		{
			this.EnsureCreateUserSteps();
			base.OnPreRender(e);
			string membershipProvider = this.MembershipProvider;
			if (!string.IsNullOrEmpty(membershipProvider) && Membership.Providers[membershipProvider] == null)
			{
				throw new HttpException(SR.GetString("WebControl_CantFindProvider"));
			}
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x0010CD28 File Offset: 0x0010BD28
		protected virtual void OnSendingMail(MailMessageEventArgs e)
		{
			MailMessageEventHandler mailMessageEventHandler = (MailMessageEventHandler)base.Events[CreateUserWizard.EventSendingMail];
			if (mailMessageEventHandler != null)
			{
				mailMessageEventHandler(this, e);
			}
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x0010CD58 File Offset: 0x0010BD58
		protected virtual void OnSendMailError(SendMailErrorEventArgs e)
		{
			SendMailErrorEventHandler sendMailErrorEventHandler = (SendMailErrorEventHandler)base.Events[CreateUserWizard.EventSendMailError];
			if (sendMailErrorEventHandler != null)
			{
				sendMailErrorEventHandler(this, e);
			}
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x0010CD86 File Offset: 0x0010BD86
		private void PasswordTextChanged(object source, EventArgs e)
		{
			if (!this.AutoGeneratePassword)
			{
				this._password = ((ITextControl)source).Text;
			}
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x0010CDA1 File Offset: 0x0010BDA1
		private void QuestionTextChanged(object source, EventArgs e)
		{
			this.Question = ((ITextControl)source).Text;
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x0010CDB4 File Offset: 0x0010BDB4
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this._createUserButtonStyle != null) ? ((IStateManager)this._createUserButtonStyle).SaveViewState() : null,
				(this._labelStyle != null) ? ((IStateManager)this._labelStyle).SaveViewState() : null,
				(this._textBoxStyle != null) ? ((IStateManager)this._textBoxStyle).SaveViewState() : null,
				(this._hyperLinkStyle != null) ? ((IStateManager)this._hyperLinkStyle).SaveViewState() : null,
				(this._instructionTextStyle != null) ? ((IStateManager)this._instructionTextStyle).SaveViewState() : null,
				(this._titleTextStyle != null) ? ((IStateManager)this._titleTextStyle).SaveViewState() : null,
				(this._errorMessageStyle != null) ? ((IStateManager)this._errorMessageStyle).SaveViewState() : null,
				(this._passwordHintStyle != null) ? ((IStateManager)this._passwordHintStyle).SaveViewState() : null,
				(this._mailDefinition != null) ? ((IStateManager)this._mailDefinition).SaveViewState() : null,
				(this._continueButtonStyle != null) ? ((IStateManager)this._continueButtonStyle).SaveViewState() : null,
				(this._completeSuccessTextStyle != null) ? ((IStateManager)this._completeSuccessTextStyle).SaveViewState() : null,
				(this._validatorTextStyle != null) ? ((IStateManager)this._validatorTextStyle).SaveViewState() : null
			};
			for (int i = 0; i < 13; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x0010CF18 File Offset: 0x0010BF18
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
			}
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x0010CF44 File Offset: 0x0010BF44
		internal void SetChildProperties()
		{
			this.ApplyCommonCreateUserValues();
			if (this.DefaultCreateUserStep)
			{
				this.ApplyDefaultCreateUserValues();
			}
			if (this.DefaultCompleteStep)
			{
				this.ApplyCompleteValues();
			}
			Control errorMessageLabel = this._createUserStepContainer.ErrorMessageLabel;
			if (errorMessageLabel != null)
			{
				if (this._failure && !string.IsNullOrEmpty(this._unknownErrorMessage))
				{
					((ITextControl)errorMessageLabel).Text = this._unknownErrorMessage;
					errorMessageLabel.Visible = true;
					return;
				}
				errorMessageLabel.Visible = false;
			}
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x0010CFB8 File Offset: 0x0010BFB8
		private void SetDefaultCreateUserNavigationTemplateProperties()
		{
			WebControl webControl = (WebControl)this._defaultCreateUserNavigationTemplate.CreateUserButton;
			WebControl webControl2 = (WebControl)this._defaultCreateUserNavigationTemplate.PreviousButton;
			WebControl webControl3 = (WebControl)this._defaultCreateUserNavigationTemplate.CancelButton;
			this._defaultCreateUserNavigationTemplate.ApplyLayoutStyleToInnerCells(base.NavigationStyle);
			this.WizardSteps.IndexOf(this.CreateUserStep);
			((IButtonControl)webControl).CausesValidation = true;
			((IButtonControl)webControl).Text = this.CreateUserButtonText;
			((IButtonControl)webControl).ValidationGroup = this.ValidationGroup;
			((IButtonControl)webControl2).CausesValidation = false;
			((IButtonControl)webControl2).Text = this.StepPreviousButtonText;
			((IButtonControl)webControl3).Text = this.CancelButtonText;
			if (this._createUserButtonStyle != null)
			{
				webControl.ApplyStyle(this._createUserButtonStyle);
			}
			webControl.ControlStyle.MergeWith(base.NavigationButtonStyle);
			webControl.TabIndex = this.TabIndex;
			webControl.Visible = true;
			if (webControl is ImageButton)
			{
				((ImageButton)webControl).ImageUrl = this.CreateUserButtonImageUrl;
				((ImageButton)webControl).AlternateText = this.CreateUserButtonText;
			}
			webControl2.ApplyStyle(base.StepPreviousButtonStyle);
			webControl2.ControlStyle.MergeWith(base.NavigationButtonStyle);
			webControl2.TabIndex = this.TabIndex;
			int previousStepIndex = base.GetPreviousStepIndex(false);
			if (previousStepIndex != -1 && this.WizardSteps[previousStepIndex].AllowReturn)
			{
				webControl2.Visible = true;
			}
			else
			{
				webControl2.Parent.Visible = false;
			}
			if (webControl2 is ImageButton)
			{
				((ImageButton)webControl2).AlternateText = this.StepPreviousButtonText;
				((ImageButton)webControl2).ImageUrl = this.StepPreviousButtonImageUrl;
			}
			if (this.DisplayCancelButton)
			{
				webControl3.ApplyStyle(base.CancelButtonStyle);
				webControl3.ControlStyle.MergeWith(base.NavigationButtonStyle);
				webControl3.TabIndex = this.TabIndex;
				webControl3.Visible = true;
				if (webControl3 is ImageButton)
				{
					((ImageButton)webControl3).ImageUrl = this.CancelButtonImageUrl;
					((ImageButton)webControl3).AlternateText = this.CancelButtonText;
					return;
				}
			}
			else
			{
				webControl3.Parent.Visible = false;
			}
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x0010D1D0 File Offset: 0x0010C1D0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._createUserButtonStyle != null)
			{
				((IStateManager)this._createUserButtonStyle).TrackViewState();
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
			if (this._errorMessageStyle != null)
			{
				((IStateManager)this._errorMessageStyle).TrackViewState();
			}
			if (this._passwordHintStyle != null)
			{
				((IStateManager)this._passwordHintStyle).TrackViewState();
			}
			if (this._mailDefinition != null)
			{
				((IStateManager)this._mailDefinition).TrackViewState();
			}
			if (this._continueButtonStyle != null)
			{
				((IStateManager)this._continueButtonStyle).TrackViewState();
			}
			if (this._completeSuccessTextStyle != null)
			{
				((IStateManager)this._completeSuccessTextStyle).TrackViewState();
			}
			if (this._validatorTextStyle != null)
			{
				((IStateManager)this._validatorTextStyle).TrackViewState();
			}
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x0010D2C7 File Offset: 0x0010C2C7
		private void UserNameTextChanged(object source, EventArgs e)
		{
			this.UserName = ((ITextControl)source).Text;
		}

		// Token: 0x04002824 RID: 10276
		private const string _userNameReplacementKey = "<%\\s*UserName\\s*%>";

		// Token: 0x04002825 RID: 10277
		private const string _passwordReplacementKey = "<%\\s*Password\\s*%>";

		// Token: 0x04002826 RID: 10278
		private const int _viewStateArrayLength = 13;

		// Token: 0x04002827 RID: 10279
		private const string _createUserNavigationTemplateName = "CreateUserNavigationTemplate";

		// Token: 0x04002828 RID: 10280
		private const string _userNameID = "UserName";

		// Token: 0x04002829 RID: 10281
		private const string _passwordID = "Password";

		// Token: 0x0400282A RID: 10282
		private const string _confirmPasswordID = "ConfirmPassword";

		// Token: 0x0400282B RID: 10283
		private const string _errorMessageID = "ErrorMessage";

		// Token: 0x0400282C RID: 10284
		private const string _emailID = "Email";

		// Token: 0x0400282D RID: 10285
		private const string _questionID = "Question";

		// Token: 0x0400282E RID: 10286
		private const string _answerID = "Answer";

		// Token: 0x0400282F RID: 10287
		private const string _userNameRequiredID = "UserNameRequired";

		// Token: 0x04002830 RID: 10288
		private const string _passwordRequiredID = "PasswordRequired";

		// Token: 0x04002831 RID: 10289
		private const string _confirmPasswordRequiredID = "ConfirmPasswordRequired";

		// Token: 0x04002832 RID: 10290
		private const string _passwordRegExpID = "PasswordRegExp";

		// Token: 0x04002833 RID: 10291
		private const string _emailRegExpID = "EmailRegExp";

		// Token: 0x04002834 RID: 10292
		private const string _emailRequiredID = "EmailRequired";

		// Token: 0x04002835 RID: 10293
		private const string _questionRequiredID = "QuestionRequired";

		// Token: 0x04002836 RID: 10294
		private const string _answerRequiredID = "AnswerRequired";

		// Token: 0x04002837 RID: 10295
		private const string _passwordCompareID = "PasswordCompare";

		// Token: 0x04002838 RID: 10296
		private const string _continueButtonID = "ContinueButton";

		// Token: 0x04002839 RID: 10297
		private const string _helpLinkID = "HelpLink";

		// Token: 0x0400283A RID: 10298
		private const string _editProfileLinkID = "EditProfileLink";

		// Token: 0x0400283B RID: 10299
		private const string _createUserStepContainerID = "CreateUserStepContainer";

		// Token: 0x0400283C RID: 10300
		private const string _completeStepContainerID = "CompleteStepContainer";

		// Token: 0x0400283D RID: 10301
		private const string _sideBarLabelID = "SideBarLabel";

		// Token: 0x0400283E RID: 10302
		private const ValidatorDisplay _requiredFieldValidatorDisplay = ValidatorDisplay.Static;

		// Token: 0x0400283F RID: 10303
		private const ValidatorDisplay _compareFieldValidatorDisplay = ValidatorDisplay.Dynamic;

		// Token: 0x04002840 RID: 10304
		private const ValidatorDisplay _regexpFieldValidatorDisplay = ValidatorDisplay.Dynamic;

		// Token: 0x04002841 RID: 10305
		public static readonly string ContinueButtonCommandName = "Continue";

		// Token: 0x04002842 RID: 10306
		private string _password;

		// Token: 0x04002843 RID: 10307
		private string _confirmPassword;

		// Token: 0x04002844 RID: 10308
		private string _answer;

		// Token: 0x04002845 RID: 10309
		private string _unknownErrorMessage;

		// Token: 0x04002846 RID: 10310
		private string _validationGroup;

		// Token: 0x04002847 RID: 10311
		private CreateUserWizardStep _createUserStep;

		// Token: 0x04002848 RID: 10312
		private CompleteWizardStep _completeStep;

		// Token: 0x04002849 RID: 10313
		private CreateUserWizard.CreateUserStepContainer _createUserStepContainer;

		// Token: 0x0400284A RID: 10314
		private CreateUserWizard.CompleteStepContainer _completeStepContainer;

		// Token: 0x0400284B RID: 10315
		private bool _failure;

		// Token: 0x0400284C RID: 10316
		private bool _convertingToTemplate;

		// Token: 0x0400284D RID: 10317
		private CreateUserWizard.DefaultCreateUserNavigationTemplate _defaultCreateUserNavigationTemplate;

		// Token: 0x0400284E RID: 10318
		private Style _createUserButtonStyle;

		// Token: 0x0400284F RID: 10319
		private TableItemStyle _labelStyle;

		// Token: 0x04002850 RID: 10320
		private Style _textBoxStyle;

		// Token: 0x04002851 RID: 10321
		private TableItemStyle _hyperLinkStyle;

		// Token: 0x04002852 RID: 10322
		private TableItemStyle _instructionTextStyle;

		// Token: 0x04002853 RID: 10323
		private TableItemStyle _titleTextStyle;

		// Token: 0x04002854 RID: 10324
		private TableItemStyle _errorMessageStyle;

		// Token: 0x04002855 RID: 10325
		private TableItemStyle _passwordHintStyle;

		// Token: 0x04002856 RID: 10326
		private Style _continueButtonStyle;

		// Token: 0x04002857 RID: 10327
		private TableItemStyle _completeSuccessTextStyle;

		// Token: 0x04002858 RID: 10328
		private Style _validatorTextStyle;

		// Token: 0x04002859 RID: 10329
		private MailDefinition _mailDefinition;

		// Token: 0x0400285A RID: 10330
		private static readonly object EventCreatingUser = new object();

		// Token: 0x0400285B RID: 10331
		private static readonly object EventCreateUserError = new object();

		// Token: 0x0400285C RID: 10332
		private static readonly object EventCreatedUser = new object();

		// Token: 0x0400285D RID: 10333
		private static readonly object EventButtonContinueClick = new object();

		// Token: 0x0400285E RID: 10334
		private static readonly object EventCancelClick = new object();

		// Token: 0x0400285F RID: 10335
		private static readonly object EventSendingMail = new object();

		// Token: 0x04002860 RID: 10336
		private static readonly object EventSendMailError = new object();

		// Token: 0x04002861 RID: 10337
		private TableRow _passwordHintTableRow;

		// Token: 0x04002862 RID: 10338
		private TableRow _questionRow;

		// Token: 0x04002863 RID: 10339
		private TableRow _answerRow;

		// Token: 0x04002864 RID: 10340
		private TableRow _emailRow;

		// Token: 0x04002865 RID: 10341
		private TableRow _passwordCompareRow;

		// Token: 0x04002866 RID: 10342
		private TableRow _passwordRegExpRow;

		// Token: 0x04002867 RID: 10343
		private TableRow _emailRegExpRow;

		// Token: 0x04002868 RID: 10344
		private TableRow _passwordTableRow;

		// Token: 0x04002869 RID: 10345
		private TableRow _confirmPasswordTableRow;

		// Token: 0x02000520 RID: 1312
		internal sealed class DefaultCompleteStepContentTemplate : ITemplate
		{
			// Token: 0x060040B4 RID: 16564 RVA: 0x0010D339 File Offset: 0x0010C339
			internal DefaultCompleteStepContentTemplate(CreateUserWizard wizard)
			{
				this._wizard = wizard;
			}

			// Token: 0x060040B5 RID: 16565 RVA: 0x0010D348 File Offset: 0x0010C348
			private void ConstructControls(CreateUserWizard.CompleteStepContainer container)
			{
				container.Title = CreateUserWizard.CreateLiteral();
				container.SuccessTextLabel = CreateUserWizard.CreateLiteral();
				container.EditProfileLink = new HyperLink();
				container.EditProfileLink.ID = "EditProfileLink";
				container.EditProfileIcon = new Image();
				container.EditProfileIcon.PreventAutoID();
				LinkButton linkButton = new LinkButton();
				linkButton.ID = "ContinueButtonLinkButton";
				linkButton.CommandName = CreateUserWizard.ContinueButtonCommandName;
				linkButton.CausesValidation = false;
				ImageButton imageButton = new ImageButton();
				imageButton.ID = "ContinueButtonImageButton";
				imageButton.CommandName = CreateUserWizard.ContinueButtonCommandName;
				imageButton.CausesValidation = false;
				Button button = new Button();
				button.ID = "ContinueButtonButton";
				button.CommandName = CreateUserWizard.ContinueButtonCommandName;
				button.CausesValidation = false;
				container.ContinueLinkButton = linkButton;
				container.ContinuePushButton = button;
				container.ContinueImageButton = imageButton;
			}

			// Token: 0x060040B6 RID: 16566 RVA: 0x0010D41C File Offset: 0x0010C41C
			private void LayoutControls(CreateUserWizard.CompleteStepContainer container)
			{
				Table table = CreateUserWizard.CreateTable();
				table.EnableViewState = false;
				TableRow tableRow = CreateUserWizard.CreateTableRow();
				TableCell tableCell = CreateUserWizard.CreateTableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(container.Title);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.Controls.Add(container.SuccessTextLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(container.ContinuePushButton);
				tableCell.Controls.Add(container.ContinueLinkButton);
				tableCell.Controls.Add(container.ContinueImageButton);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(container.EditProfileIcon);
				tableCell.Controls.Add(container.EditProfileLink);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				container.LayoutTable = table;
				container.InnerCell.Controls.Add(table);
			}

			// Token: 0x060040B7 RID: 16567 RVA: 0x0010D580 File Offset: 0x0010C580
			void ITemplate.InstantiateIn(Control container)
			{
				CreateUserWizard.CompleteStepContainer container2 = (CreateUserWizard.CompleteStepContainer)container.Parent.Parent.Parent;
				this.ConstructControls(container2);
				this.LayoutControls(container2);
			}

			// Token: 0x0400286A RID: 10346
			private CreateUserWizard _wizard;
		}

		// Token: 0x02000521 RID: 1313
		internal sealed class DefaultCreateUserContentTemplate : ITemplate
		{
			// Token: 0x060040B8 RID: 16568 RVA: 0x0010D5B1 File Offset: 0x0010C5B1
			internal DefaultCreateUserContentTemplate(CreateUserWizard wizard)
			{
				this._wizard = wizard;
			}

			// Token: 0x060040B9 RID: 16569 RVA: 0x0010D5C0 File Offset: 0x0010C5C0
			private void ConstructControls(CreateUserWizard.CreateUserStepContainer container)
			{
				string validationGroup = this._wizard.ValidationGroup;
				container.Title = CreateUserWizard.CreateLiteral();
				container.InstructionLabel = CreateUserWizard.CreateLiteral();
				container.PasswordHintLabel = CreateUserWizard.CreateLiteral();
				TextBox textBox = new TextBox();
				textBox.ID = "UserName";
				container.UserNameTextBox = textBox;
				TextBox textBox2 = new TextBox();
				textBox2.ID = "Password";
				textBox2.TextMode = TextBoxMode.Password;
				container.PasswordTextBox = textBox2;
				TextBox textBox3 = new TextBox();
				textBox3.ID = "ConfirmPassword";
				textBox3.TextMode = TextBoxMode.Password;
				container.ConfirmPasswordTextBox = textBox3;
				bool flag = true;
				container.UserNameRequired = CreateUserWizard.CreateRequiredFieldValidator("UserNameRequired", validationGroup, textBox, flag);
				container.UserNameLabel = CreateUserWizard.CreateLabelLiteral(textBox);
				container.PasswordLabel = CreateUserWizard.CreateLabelLiteral(textBox2);
				container.ConfirmPasswordLabel = CreateUserWizard.CreateLabelLiteral(textBox3);
				Image image = new Image();
				image.PreventAutoID();
				container.HelpPageIcon = image;
				container.HelpPageLink = new HyperLink
				{
					ID = "HelpLink"
				};
				container.ErrorMessageLabel = new Literal
				{
					ID = "ErrorMessage"
				};
				TextBox textBox4 = new TextBox();
				textBox4.ID = "Email";
				container.EmailRequired = CreateUserWizard.CreateRequiredFieldValidator("EmailRequired", validationGroup, textBox4, flag);
				container.EmailTextBox = textBox4;
				container.EmailLabel = CreateUserWizard.CreateLabelLiteral(textBox4);
				container.EmailRegExpValidator = new RegularExpressionValidator
				{
					ID = "EmailRegExp",
					ControlToValidate = "Email",
					ErrorMessage = this._wizard.EmailRegularExpressionErrorMessage,
					ValidationExpression = this._wizard.EmailRegularExpression,
					ValidationGroup = validationGroup,
					Display = ValidatorDisplay.Dynamic,
					Enabled = flag,
					Visible = flag
				};
				container.PasswordRequired = CreateUserWizard.CreateRequiredFieldValidator("PasswordRequired", validationGroup, textBox2, flag);
				container.ConfirmPasswordRequired = CreateUserWizard.CreateRequiredFieldValidator("ConfirmPasswordRequired", validationGroup, textBox3, flag);
				container.PasswordRegExpValidator = new RegularExpressionValidator
				{
					ID = "PasswordRegExp",
					ControlToValidate = "Password",
					ErrorMessage = this._wizard.PasswordRegularExpressionErrorMessage,
					ValidationExpression = this._wizard.PasswordRegularExpression,
					ValidationGroup = validationGroup,
					Display = ValidatorDisplay.Dynamic,
					Enabled = flag,
					Visible = flag
				};
				container.PasswordCompareValidator = new CompareValidator
				{
					ID = "PasswordCompare",
					ControlToValidate = "ConfirmPassword",
					ControlToCompare = "Password",
					Operator = ValidationCompareOperator.Equal,
					ErrorMessage = this._wizard.ConfirmPasswordCompareErrorMessage,
					ValidationGroup = validationGroup,
					Display = ValidatorDisplay.Dynamic,
					Enabled = flag,
					Visible = flag
				};
				TextBox textBox5 = new TextBox();
				textBox5.ID = "Question";
				container.QuestionTextBox = textBox5;
				TextBox textBox6 = new TextBox();
				textBox6.ID = "Answer";
				container.AnswerTextBox = textBox6;
				container.QuestionRequired = CreateUserWizard.CreateRequiredFieldValidator("QuestionRequired", validationGroup, textBox5, flag);
				container.AnswerRequired = CreateUserWizard.CreateRequiredFieldValidator("AnswerRequired", validationGroup, textBox6, flag);
				container.QuestionLabel = CreateUserWizard.CreateLabelLiteral(textBox5);
				container.AnswerLabel = CreateUserWizard.CreateLabelLiteral(textBox6);
			}

			// Token: 0x060040BA RID: 16570 RVA: 0x0010D900 File Offset: 0x0010C900
			private void LayoutControls(CreateUserWizard.CreateUserStepContainer container)
			{
				Table table = CreateUserWizard.CreateTable();
				table.EnableViewState = false;
				TableRow tableRow = CreateUserWizard.CreateTableRow();
				TableCell tableCell = CreateUserWizard.CreateTableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(container.Title);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				tableRow.PreventAutoID();
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(container.InstructionLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				if (this._wizard.ConvertingToTemplate)
				{
					container.UserNameLabel.RenderAsLabel = true;
				}
				tableCell.Controls.Add(container.UserNameLabel);
				tableRow.Cells.Add(tableCell);
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.Controls.Add(container.UserNameTextBox);
				tableCell.Controls.Add(container.UserNameRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				this._wizard._passwordTableRow = tableRow;
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				if (this._wizard.ConvertingToTemplate)
				{
					container.PasswordLabel.RenderAsLabel = true;
				}
				tableCell.Controls.Add(container.PasswordLabel);
				tableRow.Cells.Add(tableCell);
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.Controls.Add(container.PasswordTextBox);
				if (!this._wizard.AutoGeneratePassword)
				{
					tableCell.Controls.Add(container.PasswordRequired);
				}
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				this._wizard._passwordHintTableRow = tableRow;
				tableCell = CreateUserWizard.CreateTableCell();
				tableRow.Cells.Add(tableCell);
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.Controls.Add(container.PasswordHintLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				this._wizard._confirmPasswordTableRow = tableRow;
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				if (this._wizard.ConvertingToTemplate)
				{
					container.ConfirmPasswordLabel.RenderAsLabel = true;
				}
				tableCell.Controls.Add(container.ConfirmPasswordLabel);
				tableRow.Cells.Add(tableCell);
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.Controls.Add(container.ConfirmPasswordTextBox);
				if (!this._wizard.AutoGeneratePassword)
				{
					tableCell.Controls.Add(container.ConfirmPasswordRequired);
				}
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				this._wizard._emailRow = tableRow;
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(container.EmailLabel);
				if (this._wizard.ConvertingToTemplate)
				{
					container.EmailLabel.RenderAsLabel = true;
				}
				tableRow.Cells.Add(tableCell);
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.Controls.Add(container.EmailTextBox);
				tableCell.Controls.Add(container.EmailRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				this._wizard._questionRow = tableRow;
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(container.QuestionLabel);
				if (this._wizard.ConvertingToTemplate)
				{
					container.QuestionLabel.RenderAsLabel = true;
				}
				tableRow.Cells.Add(tableCell);
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.Controls.Add(container.QuestionTextBox);
				tableCell.Controls.Add(container.QuestionRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				this._wizard._answerRow = tableRow;
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(container.AnswerLabel);
				if (this._wizard.ConvertingToTemplate)
				{
					container.AnswerLabel.RenderAsLabel = true;
				}
				tableRow.Cells.Add(tableCell);
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.Controls.Add(container.AnswerTextBox);
				tableCell.Controls.Add(container.AnswerRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				this._wizard._passwordCompareRow = tableRow;
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(container.PasswordCompareValidator);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				this._wizard._passwordRegExpRow = tableRow;
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(container.PasswordRegExpValidator);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				this._wizard._emailRegExpRow = tableRow;
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(container.EmailRegExpValidator);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(container.ErrorMessageLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = CreateUserWizard.CreateTableRow();
				tableCell = CreateUserWizard.CreateTableCell();
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(container.HelpPageIcon);
				tableCell.Controls.Add(container.HelpPageLink);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				container.InnerCell.Controls.Add(table);
			}

			// Token: 0x060040BB RID: 16571 RVA: 0x0010DF38 File Offset: 0x0010CF38
			void ITemplate.InstantiateIn(Control container)
			{
				CreateUserWizard.CreateUserStepContainer container2 = (CreateUserWizard.CreateUserStepContainer)container.Parent.Parent.Parent;
				this.ConstructControls(container2);
				this.LayoutControls(container2);
			}

			// Token: 0x0400286B RID: 10347
			private CreateUserWizard _wizard;
		}

		// Token: 0x02000522 RID: 1314
		internal sealed class DefaultCreateUserNavigationTemplate : ITemplate
		{
			// Token: 0x060040BC RID: 16572 RVA: 0x0010DF69 File Offset: 0x0010CF69
			internal DefaultCreateUserNavigationTemplate(CreateUserWizard wizard)
			{
				this._wizard = wizard;
			}

			// Token: 0x060040BD RID: 16573 RVA: 0x0010DF78 File Offset: 0x0010CF78
			internal void ApplyLayoutStyleToInnerCells(TableItemStyle tableItemStyle)
			{
				for (int i = 0; i < this._innerCells.Length; i++)
				{
					if (tableItemStyle.IsSet(65536))
					{
						this._innerCells[i].HorizontalAlign = tableItemStyle.HorizontalAlign;
					}
					if (tableItemStyle.IsSet(131072))
					{
						this._innerCells[i].VerticalAlign = tableItemStyle.VerticalAlign;
					}
				}
			}

			// Token: 0x060040BE RID: 16574 RVA: 0x0010DFD8 File Offset: 0x0010CFD8
			void ITemplate.InstantiateIn(Control container)
			{
				this._wizard._defaultCreateUserNavigationTemplate = this;
				container.EnableViewState = false;
				Table table = CreateUserWizard.CreateTable();
				table.CellSpacing = 5;
				table.CellPadding = 5;
				container.Controls.Add(table);
				TableRow tableRow = new TableRow();
				this._row = tableRow;
				tableRow.PreventAutoID();
				tableRow.HorizontalAlign = HorizontalAlign.Right;
				table.Rows.Add(tableRow);
				this._buttons = new IButtonControl[3][];
				this._buttons[0] = new IButtonControl[3];
				this._buttons[1] = new IButtonControl[3];
				this._buttons[2] = new IButtonControl[3];
				this._innerCells = new TableCell[3];
				this._innerCells[0] = this.CreateButtonControl(this._buttons[0], this._wizard.ValidationGroup, Wizard.StepPreviousButtonID, false, Wizard.MovePreviousCommandName);
				this._innerCells[1] = this.CreateButtonControl(this._buttons[1], this._wizard.ValidationGroup, Wizard.StepNextButtonID, true, Wizard.MoveNextCommandName);
				this._innerCells[2] = this.CreateButtonControl(this._buttons[2], this._wizard.ValidationGroup, Wizard.CancelButtonID, false, Wizard.CancelCommandName);
			}

			// Token: 0x060040BF RID: 16575 RVA: 0x0010E105 File Offset: 0x0010D105
			private void OnPreRender(object source, EventArgs e)
			{
				((ImageButton)source).Visible = false;
			}

			// Token: 0x060040C0 RID: 16576 RVA: 0x0010E114 File Offset: 0x0010D114
			private TableCell CreateButtonControl(IButtonControl[] buttons, string validationGroup, string id, bool causesValidation, string commandName)
			{
				LinkButton linkButton = new LinkButton();
				linkButton.CausesValidation = causesValidation;
				linkButton.ID = id + "LinkButton";
				linkButton.Visible = false;
				linkButton.CommandName = commandName;
				linkButton.ValidationGroup = validationGroup;
				buttons[0] = linkButton;
				ImageButton imageButton = new ImageButton();
				imageButton.CausesValidation = causesValidation;
				imageButton.ID = id + "ImageButton";
				imageButton.Visible = !this._wizard.DesignMode;
				imageButton.CommandName = commandName;
				imageButton.ValidationGroup = validationGroup;
				imageButton.PreRender += this.OnPreRender;
				buttons[1] = imageButton;
				Button button = new Button();
				button.CausesValidation = causesValidation;
				button.ID = id + "Button";
				button.Visible = false;
				button.CommandName = commandName;
				button.ValidationGroup = validationGroup;
				buttons[2] = button;
				TableCell tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				this._row.Cells.Add(tableCell);
				tableCell.Controls.Add(linkButton);
				tableCell.Controls.Add(imageButton);
				tableCell.Controls.Add(button);
				return tableCell;
			}

			// Token: 0x17000F85 RID: 3973
			// (get) Token: 0x060040C1 RID: 16577 RVA: 0x0010E22F File Offset: 0x0010D22F
			internal IButtonControl PreviousButton
			{
				get
				{
					return this.GetButtonBasedOnType(0, this._wizard.StepPreviousButtonType);
				}
			}

			// Token: 0x17000F86 RID: 3974
			// (get) Token: 0x060040C2 RID: 16578 RVA: 0x0010E243 File Offset: 0x0010D243
			internal IButtonControl CreateUserButton
			{
				get
				{
					return this.GetButtonBasedOnType(1, this._wizard.CreateUserButtonType);
				}
			}

			// Token: 0x17000F87 RID: 3975
			// (get) Token: 0x060040C3 RID: 16579 RVA: 0x0010E257 File Offset: 0x0010D257
			internal IButtonControl CancelButton
			{
				get
				{
					return this.GetButtonBasedOnType(2, this._wizard.CancelButtonType);
				}
			}

			// Token: 0x060040C4 RID: 16580 RVA: 0x0010E26C File Offset: 0x0010D26C
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

			// Token: 0x0400286C RID: 10348
			private CreateUserWizard _wizard;

			// Token: 0x0400286D RID: 10349
			private TableRow _row;

			// Token: 0x0400286E RID: 10350
			private IButtonControl[][] _buttons;

			// Token: 0x0400286F RID: 10351
			private TableCell[] _innerCells;
		}

		// Token: 0x02000523 RID: 1315
		private sealed class DataListItemTemplate : ITemplate
		{
			// Token: 0x060040C5 RID: 16581 RVA: 0x0010E2B4 File Offset: 0x0010D2B4
			public void InstantiateIn(Control container)
			{
				Label label = new Label();
				label.PreventAutoID();
				label.ID = "SideBarLabel";
				container.Controls.Add(label);
			}
		}

		// Token: 0x02000524 RID: 1316
		private sealed class DefaultSideBarTemplate : ITemplate
		{
			// Token: 0x060040C7 RID: 16583 RVA: 0x0010E2EC File Offset: 0x0010D2EC
			public void InstantiateIn(Control container)
			{
				DataList dataList = new DataList();
				dataList.ID = Wizard.DataListID;
				container.Controls.Add(dataList);
				dataList.SelectedItemStyle.Font.Bold = true;
				dataList.ItemTemplate = new CreateUserWizard.DataListItemTemplate();
			}
		}

		// Token: 0x02000525 RID: 1317
		internal sealed class CreateUserStepContainer : Wizard.BaseContentTemplateContainer
		{
			// Token: 0x060040C9 RID: 16585 RVA: 0x0010E33A File Offset: 0x0010D33A
			internal CreateUserStepContainer(CreateUserWizard wizard) : base(wizard)
			{
				this._createUserWizard = wizard;
			}

			// Token: 0x17000F88 RID: 3976
			// (get) Token: 0x060040CA RID: 16586 RVA: 0x0010E34A File Offset: 0x0010D34A
			// (set) Token: 0x060040CB RID: 16587 RVA: 0x0010E352 File Offset: 0x0010D352
			internal LabelLiteral AnswerLabel
			{
				get
				{
					return this._answerLabel;
				}
				set
				{
					this._answerLabel = value;
				}
			}

			// Token: 0x17000F89 RID: 3977
			// (get) Token: 0x060040CC RID: 16588 RVA: 0x0010E35B File Offset: 0x0010D35B
			// (set) Token: 0x060040CD RID: 16589 RVA: 0x0010E363 File Offset: 0x0010D363
			internal RequiredFieldValidator AnswerRequired
			{
				get
				{
					return this._answerRequired;
				}
				set
				{
					this._answerRequired = value;
				}
			}

			// Token: 0x17000F8A RID: 3978
			// (get) Token: 0x060040CE RID: 16590 RVA: 0x0010E36C File Offset: 0x0010D36C
			// (set) Token: 0x060040CF RID: 16591 RVA: 0x0010E3E7 File Offset: 0x0010D3E7
			internal Control AnswerTextBox
			{
				get
				{
					if (this._answerTextBox != null)
					{
						return this._answerTextBox;
					}
					Control control = this.FindControl("Answer");
					if (control is IEditableTextControl)
					{
						return control;
					}
					if (!this._createUserWizard.DesignMode && this._createUserWizard.QuestionAndAnswerRequired)
					{
						throw new HttpException(SR.GetString("CreateUserWizard_NoAnswerTextBox", new object[]
						{
							this._createUserWizard.ID,
							"Answer"
						}));
					}
					return null;
				}
				set
				{
					this._answerTextBox = value;
				}
			}

			// Token: 0x17000F8B RID: 3979
			// (get) Token: 0x060040D0 RID: 16592 RVA: 0x0010E3F0 File Offset: 0x0010D3F0
			// (set) Token: 0x060040D1 RID: 16593 RVA: 0x0010E3F8 File Offset: 0x0010D3F8
			internal LabelLiteral ConfirmPasswordLabel
			{
				get
				{
					return this._confirmPasswordLabel;
				}
				set
				{
					this._confirmPasswordLabel = value;
				}
			}

			// Token: 0x17000F8C RID: 3980
			// (get) Token: 0x060040D2 RID: 16594 RVA: 0x0010E401 File Offset: 0x0010D401
			// (set) Token: 0x060040D3 RID: 16595 RVA: 0x0010E409 File Offset: 0x0010D409
			internal RequiredFieldValidator ConfirmPasswordRequired
			{
				get
				{
					return this._confirmPasswordRequired;
				}
				set
				{
					this._confirmPasswordRequired = value;
				}
			}

			// Token: 0x17000F8D RID: 3981
			// (get) Token: 0x060040D4 RID: 16596 RVA: 0x0010E414 File Offset: 0x0010D414
			// (set) Token: 0x060040D5 RID: 16597 RVA: 0x0010E447 File Offset: 0x0010D447
			internal Control ConfirmPasswordTextBox
			{
				get
				{
					if (this._confirmPasswordTextBox != null)
					{
						return this._confirmPasswordTextBox;
					}
					Control control = this.FindControl("ConfirmPassword");
					if (control is IEditableTextControl)
					{
						return control;
					}
					return null;
				}
				set
				{
					this._confirmPasswordTextBox = value;
				}
			}

			// Token: 0x17000F8E RID: 3982
			// (get) Token: 0x060040D6 RID: 16598 RVA: 0x0010E450 File Offset: 0x0010D450
			// (set) Token: 0x060040D7 RID: 16599 RVA: 0x0010E458 File Offset: 0x0010D458
			internal LabelLiteral EmailLabel
			{
				get
				{
					return this._emailLabel;
				}
				set
				{
					this._emailLabel = value;
				}
			}

			// Token: 0x17000F8F RID: 3983
			// (get) Token: 0x060040D8 RID: 16600 RVA: 0x0010E461 File Offset: 0x0010D461
			// (set) Token: 0x060040D9 RID: 16601 RVA: 0x0010E469 File Offset: 0x0010D469
			internal RegularExpressionValidator EmailRegExpValidator
			{
				get
				{
					return this._emailRegExpValidator;
				}
				set
				{
					this._emailRegExpValidator = value;
				}
			}

			// Token: 0x17000F90 RID: 3984
			// (get) Token: 0x060040DA RID: 16602 RVA: 0x0010E472 File Offset: 0x0010D472
			// (set) Token: 0x060040DB RID: 16603 RVA: 0x0010E47A File Offset: 0x0010D47A
			internal RequiredFieldValidator EmailRequired
			{
				get
				{
					return this._emailRequired;
				}
				set
				{
					this._emailRequired = value;
				}
			}

			// Token: 0x17000F91 RID: 3985
			// (get) Token: 0x060040DC RID: 16604 RVA: 0x0010E484 File Offset: 0x0010D484
			// (set) Token: 0x060040DD RID: 16605 RVA: 0x0010E4FF File Offset: 0x0010D4FF
			internal Control EmailTextBox
			{
				get
				{
					if (this._emailTextBox != null)
					{
						return this._emailTextBox;
					}
					Control control = this.FindControl("Email");
					if (control is IEditableTextControl)
					{
						return control;
					}
					if (!this._createUserWizard.DesignMode && this._createUserWizard.RequireEmail)
					{
						throw new HttpException(SR.GetString("CreateUserWizard_NoEmailTextBox", new object[]
						{
							this._createUserWizard.ID,
							"Email"
						}));
					}
					return null;
				}
				set
				{
					this._emailTextBox = value;
				}
			}

			// Token: 0x17000F92 RID: 3986
			// (get) Token: 0x060040DE RID: 16606 RVA: 0x0010E508 File Offset: 0x0010D508
			// (set) Token: 0x060040DF RID: 16607 RVA: 0x0010E510 File Offset: 0x0010D510
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

			// Token: 0x17000F93 RID: 3987
			// (get) Token: 0x060040E0 RID: 16608 RVA: 0x0010E51C File Offset: 0x0010D51C
			// (set) Token: 0x060040E1 RID: 16609 RVA: 0x0010E551 File Offset: 0x0010D551
			internal Control ErrorMessageLabel
			{
				get
				{
					if (this._unknownErrorMessageLabel != null)
					{
						return this._unknownErrorMessageLabel;
					}
					Control control = this.FindControl("ErrorMessage");
					if (!(control is ITextControl))
					{
						return null;
					}
					return control;
				}
				set
				{
					this._unknownErrorMessageLabel = value;
				}
			}

			// Token: 0x17000F94 RID: 3988
			// (get) Token: 0x060040E2 RID: 16610 RVA: 0x0010E55A File Offset: 0x0010D55A
			// (set) Token: 0x060040E3 RID: 16611 RVA: 0x0010E562 File Offset: 0x0010D562
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

			// Token: 0x17000F95 RID: 3989
			// (get) Token: 0x060040E4 RID: 16612 RVA: 0x0010E56B File Offset: 0x0010D56B
			// (set) Token: 0x060040E5 RID: 16613 RVA: 0x0010E573 File Offset: 0x0010D573
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

			// Token: 0x17000F96 RID: 3990
			// (get) Token: 0x060040E6 RID: 16614 RVA: 0x0010E57C File Offset: 0x0010D57C
			// (set) Token: 0x060040E7 RID: 16615 RVA: 0x0010E584 File Offset: 0x0010D584
			internal Literal InstructionLabel
			{
				get
				{
					return this._instructionLabel;
				}
				set
				{
					this._instructionLabel = value;
				}
			}

			// Token: 0x17000F97 RID: 3991
			// (get) Token: 0x060040E8 RID: 16616 RVA: 0x0010E58D File Offset: 0x0010D58D
			// (set) Token: 0x060040E9 RID: 16617 RVA: 0x0010E595 File Offset: 0x0010D595
			internal CompareValidator PasswordCompareValidator
			{
				get
				{
					return this._passwordCompareValidator;
				}
				set
				{
					this._passwordCompareValidator = value;
				}
			}

			// Token: 0x17000F98 RID: 3992
			// (get) Token: 0x060040EA RID: 16618 RVA: 0x0010E59E File Offset: 0x0010D59E
			// (set) Token: 0x060040EB RID: 16619 RVA: 0x0010E5A6 File Offset: 0x0010D5A6
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

			// Token: 0x17000F99 RID: 3993
			// (get) Token: 0x060040EC RID: 16620 RVA: 0x0010E5AF File Offset: 0x0010D5AF
			// (set) Token: 0x060040ED RID: 16621 RVA: 0x0010E5B7 File Offset: 0x0010D5B7
			internal RegularExpressionValidator PasswordRegExpValidator
			{
				get
				{
					return this._passwordRegExpValidator;
				}
				set
				{
					this._passwordRegExpValidator = value;
				}
			}

			// Token: 0x17000F9A RID: 3994
			// (get) Token: 0x060040EE RID: 16622 RVA: 0x0010E5C0 File Offset: 0x0010D5C0
			// (set) Token: 0x060040EF RID: 16623 RVA: 0x0010E5C8 File Offset: 0x0010D5C8
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

			// Token: 0x17000F9B RID: 3995
			// (get) Token: 0x060040F0 RID: 16624 RVA: 0x0010E5D4 File Offset: 0x0010D5D4
			// (set) Token: 0x060040F1 RID: 16625 RVA: 0x0010E64F File Offset: 0x0010D64F
			internal Control PasswordTextBox
			{
				get
				{
					if (this._passwordTextBox != null)
					{
						return this._passwordTextBox;
					}
					Control control = this.FindControl("Password");
					if (control is IEditableTextControl)
					{
						return control;
					}
					if (!this._createUserWizard.DesignMode && !this._createUserWizard.AutoGeneratePassword)
					{
						throw new HttpException(SR.GetString("CreateUserWizard_NoPasswordTextBox", new object[]
						{
							this._createUserWizard.ID,
							"Password"
						}));
					}
					return null;
				}
				set
				{
					this._passwordTextBox = value;
				}
			}

			// Token: 0x17000F9C RID: 3996
			// (get) Token: 0x060040F2 RID: 16626 RVA: 0x0010E658 File Offset: 0x0010D658
			// (set) Token: 0x060040F3 RID: 16627 RVA: 0x0010E660 File Offset: 0x0010D660
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

			// Token: 0x17000F9D RID: 3997
			// (get) Token: 0x060040F4 RID: 16628 RVA: 0x0010E669 File Offset: 0x0010D669
			// (set) Token: 0x060040F5 RID: 16629 RVA: 0x0010E671 File Offset: 0x0010D671
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

			// Token: 0x17000F9E RID: 3998
			// (get) Token: 0x060040F6 RID: 16630 RVA: 0x0010E67A File Offset: 0x0010D67A
			// (set) Token: 0x060040F7 RID: 16631 RVA: 0x0010E682 File Offset: 0x0010D682
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

			// Token: 0x17000F9F RID: 3999
			// (get) Token: 0x060040F8 RID: 16632 RVA: 0x0010E68B File Offset: 0x0010D68B
			// (set) Token: 0x060040F9 RID: 16633 RVA: 0x0010E693 File Offset: 0x0010D693
			internal LabelLiteral QuestionLabel
			{
				get
				{
					return this._questionLabel;
				}
				set
				{
					this._questionLabel = value;
				}
			}

			// Token: 0x17000FA0 RID: 4000
			// (get) Token: 0x060040FA RID: 16634 RVA: 0x0010E69C File Offset: 0x0010D69C
			// (set) Token: 0x060040FB RID: 16635 RVA: 0x0010E6A4 File Offset: 0x0010D6A4
			internal RequiredFieldValidator QuestionRequired
			{
				get
				{
					return this._questionRequired;
				}
				set
				{
					this._questionRequired = value;
				}
			}

			// Token: 0x17000FA1 RID: 4001
			// (get) Token: 0x060040FC RID: 16636 RVA: 0x0010E6B0 File Offset: 0x0010D6B0
			// (set) Token: 0x060040FD RID: 16637 RVA: 0x0010E72B File Offset: 0x0010D72B
			internal Control QuestionTextBox
			{
				get
				{
					if (this._questionTextBox != null)
					{
						return this._questionTextBox;
					}
					Control control = this.FindControl("Question");
					if (control is IEditableTextControl)
					{
						return control;
					}
					if (!this._createUserWizard.DesignMode && this._createUserWizard.QuestionAndAnswerRequired)
					{
						throw new HttpException(SR.GetString("CreateUserWizard_NoQuestionTextBox", new object[]
						{
							this._createUserWizard.ID,
							"Question"
						}));
					}
					return null;
				}
				set
				{
					this._questionTextBox = value;
				}
			}

			// Token: 0x17000FA2 RID: 4002
			// (get) Token: 0x060040FE RID: 16638 RVA: 0x0010E734 File Offset: 0x0010D734
			// (set) Token: 0x060040FF RID: 16639 RVA: 0x0010E7A2 File Offset: 0x0010D7A2
			internal Control UserNameTextBox
			{
				get
				{
					if (this._userNameTextBox != null)
					{
						return this._userNameTextBox;
					}
					Control control = this.FindControl("UserName");
					if (control is IEditableTextControl)
					{
						return control;
					}
					if (!this._createUserWizard.DesignMode)
					{
						throw new HttpException(SR.GetString("CreateUserWizard_NoUserNameTextBox", new object[]
						{
							this._createUserWizard.ID,
							"UserName"
						}));
					}
					return null;
				}
				set
				{
					this._userNameTextBox = value;
				}
			}

			// Token: 0x04002870 RID: 10352
			private CreateUserWizard _createUserWizard;

			// Token: 0x04002871 RID: 10353
			private Literal _title;

			// Token: 0x04002872 RID: 10354
			private Literal _instructionLabel;

			// Token: 0x04002873 RID: 10355
			private LabelLiteral _userNameLabel;

			// Token: 0x04002874 RID: 10356
			private LabelLiteral _passwordLabel;

			// Token: 0x04002875 RID: 10357
			private LabelLiteral _confirmPasswordLabel;

			// Token: 0x04002876 RID: 10358
			private LabelLiteral _emailLabel;

			// Token: 0x04002877 RID: 10359
			private LabelLiteral _questionLabel;

			// Token: 0x04002878 RID: 10360
			private LabelLiteral _answerLabel;

			// Token: 0x04002879 RID: 10361
			private Literal _passwordHintLabel;

			// Token: 0x0400287A RID: 10362
			private Control _userNameTextBox;

			// Token: 0x0400287B RID: 10363
			private Control _passwordTextBox;

			// Token: 0x0400287C RID: 10364
			private Control _confirmPasswordTextBox;

			// Token: 0x0400287D RID: 10365
			private Control _emailTextBox;

			// Token: 0x0400287E RID: 10366
			private Control _questionTextBox;

			// Token: 0x0400287F RID: 10367
			private Control _answerTextBox;

			// Token: 0x04002880 RID: 10368
			private Control _unknownErrorMessageLabel;

			// Token: 0x04002881 RID: 10369
			private RequiredFieldValidator _userNameRequired;

			// Token: 0x04002882 RID: 10370
			private RequiredFieldValidator _passwordRequired;

			// Token: 0x04002883 RID: 10371
			private RequiredFieldValidator _confirmPasswordRequired;

			// Token: 0x04002884 RID: 10372
			private RequiredFieldValidator _questionRequired;

			// Token: 0x04002885 RID: 10373
			private RequiredFieldValidator _answerRequired;

			// Token: 0x04002886 RID: 10374
			private RequiredFieldValidator _emailRequired;

			// Token: 0x04002887 RID: 10375
			private CompareValidator _passwordCompareValidator;

			// Token: 0x04002888 RID: 10376
			private RegularExpressionValidator _passwordRegExpValidator;

			// Token: 0x04002889 RID: 10377
			private RegularExpressionValidator _emailRegExpValidator;

			// Token: 0x0400288A RID: 10378
			private Image _helpPageIcon;

			// Token: 0x0400288B RID: 10379
			private HyperLink _helpPageLink;
		}

		// Token: 0x02000526 RID: 1318
		internal sealed class CompleteStepContainer : Wizard.BaseContentTemplateContainer
		{
			// Token: 0x06004100 RID: 16640 RVA: 0x0010E7AB File Offset: 0x0010D7AB
			internal CompleteStepContainer(CreateUserWizard wizard) : base(wizard)
			{
				this._createUserWizard = wizard;
			}

			// Token: 0x17000FA3 RID: 4003
			// (get) Token: 0x06004101 RID: 16641 RVA: 0x0010E7BB File Offset: 0x0010D7BB
			// (set) Token: 0x06004102 RID: 16642 RVA: 0x0010E7C3 File Offset: 0x0010D7C3
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

			// Token: 0x17000FA4 RID: 4004
			// (get) Token: 0x06004103 RID: 16643 RVA: 0x0010E7CC File Offset: 0x0010D7CC
			// (set) Token: 0x06004104 RID: 16644 RVA: 0x0010E7D4 File Offset: 0x0010D7D4
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

			// Token: 0x17000FA5 RID: 4005
			// (get) Token: 0x06004105 RID: 16645 RVA: 0x0010E7DD File Offset: 0x0010D7DD
			// (set) Token: 0x06004106 RID: 16646 RVA: 0x0010E7E5 File Offset: 0x0010D7E5
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

			// Token: 0x17000FA6 RID: 4006
			// (get) Token: 0x06004107 RID: 16647 RVA: 0x0010E7EE File Offset: 0x0010D7EE
			// (set) Token: 0x06004108 RID: 16648 RVA: 0x0010E7F6 File Offset: 0x0010D7F6
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

			// Token: 0x17000FA7 RID: 4007
			// (get) Token: 0x06004109 RID: 16649 RVA: 0x0010E7FF File Offset: 0x0010D7FF
			// (set) Token: 0x0600410A RID: 16650 RVA: 0x0010E807 File Offset: 0x0010D807
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

			// Token: 0x17000FA8 RID: 4008
			// (get) Token: 0x0600410B RID: 16651 RVA: 0x0010E810 File Offset: 0x0010D810
			// (set) Token: 0x0600410C RID: 16652 RVA: 0x0010E818 File Offset: 0x0010D818
			internal Table LayoutTable
			{
				get
				{
					return this._layoutTable;
				}
				set
				{
					this._layoutTable = value;
				}
			}

			// Token: 0x17000FA9 RID: 4009
			// (get) Token: 0x0600410D RID: 16653 RVA: 0x0010E821 File Offset: 0x0010D821
			// (set) Token: 0x0600410E RID: 16654 RVA: 0x0010E829 File Offset: 0x0010D829
			internal Literal SuccessTextLabel
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

			// Token: 0x17000FAA RID: 4010
			// (get) Token: 0x0600410F RID: 16655 RVA: 0x0010E832 File Offset: 0x0010D832
			// (set) Token: 0x06004110 RID: 16656 RVA: 0x0010E83A File Offset: 0x0010D83A
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

			// Token: 0x0400288C RID: 10380
			private CreateUserWizard _createUserWizard;

			// Token: 0x0400288D RID: 10381
			private Literal _title;

			// Token: 0x0400288E RID: 10382
			private Literal _successTextLabel;

			// Token: 0x0400288F RID: 10383
			private LinkButton _continueLinkButton;

			// Token: 0x04002890 RID: 10384
			private Button _continuePushButton;

			// Token: 0x04002891 RID: 10385
			private ImageButton _continueImageButton;

			// Token: 0x04002892 RID: 10386
			private Image _editProfileIcon;

			// Token: 0x04002893 RID: 10387
			private Table _layoutTable;

			// Token: 0x04002894 RID: 10388
			private HyperLink _editProfileLink;
		}
	}
}
