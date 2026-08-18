using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003AA RID: 938
	[Bindable(false)]
	[DefaultEvent("CreatedUser")]
	[Designer("System.Web.UI.Design.WebControls.CreateUserWizardDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxData("<{0}:CreateUserWizard runat=\"server\"> <WizardSteps> <asp:CreateUserWizardStep runat=\"server\"/> <asp:CompleteWizardStep runat=\"server\"/> </WizardSteps> </{0}:CreateUserWizard>")]
	public class CreateUserWizard : Wizard
	{
		// Token: 0x06002C91 RID: 11409 RVA: 0x0009117C File Offset: 0x0008F37C
		public CreateUserWizard() : base(false)
		{
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x00091185 File Offset: 0x0008F385
		// (set) Token: 0x06002C93 RID: 11411 RVA: 0x0009118D File Offset: 0x0008F38D
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

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x00091196 File Offset: 0x0008F396
		// (set) Token: 0x06002C95 RID: 11413 RVA: 0x000911AC File Offset: 0x0008F3AC
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Themeable(false)]
		[WebSysDescription("CreateUserWizard_Answer")]
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

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x000911B8 File Offset: 0x0008F3B8
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

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x00091208 File Offset: 0x0008F408
		// (set) Token: 0x06002C98 RID: 11416 RVA: 0x0009123A File Offset: 0x0008F43A
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultAnswerLabelText")]
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

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x00091250 File Offset: 0x0008F450
		// (set) Token: 0x06002C9A RID: 11418 RVA: 0x00091282 File Offset: 0x0008F482
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

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06002C9B RID: 11419 RVA: 0x00091298 File Offset: 0x0008F498
		// (set) Token: 0x06002C9C RID: 11420 RVA: 0x000912C1 File Offset: 0x0008F4C1
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[Themeable(false)]
		[WebSysDescription("CreateUserWizard_AutoGeneratePassword")]
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

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000912E8 File Offset: 0x0008F4E8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
		[WebSysDescription("CreateUserWizard_CompleteStep")]
		public CompleteWizardStep CompleteStep
		{
			get
			{
				this.EnsureChildControls();
				return this._completeStep;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06002C9E RID: 11422 RVA: 0x000912F8 File Offset: 0x0008F4F8
		// (set) Token: 0x06002C9F RID: 11423 RVA: 0x0009132A File Offset: 0x0008F52A
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultCompleteSuccessText")]
		[WebSysDescription("CreateUserWizard_CompleteSuccessText")]
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

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x0009133D File Offset: 0x0008F53D
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("CreateUserWizard_CompleteSuccessTextStyle")]
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

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x0009136B File Offset: 0x0008F56B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x00091384 File Offset: 0x0008F584
		// (set) Token: 0x06002CA3 RID: 11427 RVA: 0x0008B2BA File Offset: 0x000894BA
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("CreateUserWizard_DefaultConfirmPasswordCompareErrorMessage")]
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
				return SR.GetString("CreateUserWizard_DefaultConfirmPasswordCompareErrorMessage");
			}
			set
			{
				this.ViewState["ConfirmPasswordCompareErrorMessage"] = value;
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x000913B8 File Offset: 0x0008F5B8
		// (set) Token: 0x06002CA5 RID: 11429 RVA: 0x000913EA File Offset: 0x0008F5EA
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultConfirmPasswordLabelText")]
		[WebSysDescription("CreateUserWizard_ConfirmPasswordLabelText")]
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

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x00091400 File Offset: 0x0008F600
		// (set) Token: 0x06002CA7 RID: 11431 RVA: 0x0008B302 File Offset: 0x00089502
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

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x00091434 File Offset: 0x0008F634
		// (set) Token: 0x06002CA9 RID: 11433 RVA: 0x0008B345 File Offset: 0x00089545
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

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x00091461 File Offset: 0x0008F661
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("CreateUserWizard_ContinueButtonStyle")]
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

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x00091490 File Offset: 0x0008F690
		// (set) Token: 0x06002CAC RID: 11436 RVA: 0x0008B3BA File Offset: 0x000895BA
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultContinueButtonText")]
		[WebSysDescription("CreateUserWizard_ContinueButtonText")]
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

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000914C4 File Offset: 0x0008F6C4
		// (set) Token: 0x06002CAE RID: 11438 RVA: 0x000914ED File Offset: 0x0008F6ED
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("CreateUserWizard_ContinueButtonType")]
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

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x00091524 File Offset: 0x0008F724
		// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x0008B451 File Offset: 0x00089651
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

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06002CB1 RID: 11441 RVA: 0x00091551 File Offset: 0x0008F751
		private bool ConvertingToTemplate
		{
			get
			{
				return base.DesignMode && this._convertingToTemplate;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x00091563 File Offset: 0x0008F763
		[WebCategory("Appearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("CreateUserWizard_CreateUserStep")]
		public CreateUserWizardStep CreateUserStep
		{
			get
			{
				this.EnsureChildControls();
				return this._createUserStep;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x00091574 File Offset: 0x0008F774
		// (set) Token: 0x06002CB4 RID: 11444 RVA: 0x000915A1 File Offset: 0x0008F7A1
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("CreateUserWizard_CreateUserButtonImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
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

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x000915B4 File Offset: 0x0008F7B4
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("CreateUserWizard_CreateUserButtonStyle")]
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

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000915E4 File Offset: 0x0008F7E4
		// (set) Token: 0x06002CB7 RID: 11447 RVA: 0x00091616 File Offset: 0x0008F816
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultCreateUserButtonText")]
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

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x0009162C File Offset: 0x0008F82C
		// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x00091655 File Offset: 0x0008F855
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
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

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x0009168C File Offset: 0x0008F88C
		private bool DefaultCreateUserStep
		{
			get
			{
				CreateUserWizardStep createUserStep = this.CreateUserStep;
				return createUserStep != null && createUserStep.ContentTemplate == null;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x000916B0 File Offset: 0x0008F8B0
		private bool DefaultCompleteStep
		{
			get
			{
				CompleteWizardStep completeStep = this.CompleteStep;
				return completeStep != null && completeStep.ContentTemplate == null;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x000916D4 File Offset: 0x0008F8D4
		// (set) Token: 0x06002CBD RID: 11453 RVA: 0x000916FD File Offset: 0x0008F8FD
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

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x00091715 File Offset: 0x0008F915
		// (set) Token: 0x06002CBF RID: 11455 RVA: 0x0009171D File Offset: 0x0008F91D
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

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x00091728 File Offset: 0x0008F928
		// (set) Token: 0x06002CC1 RID: 11457 RVA: 0x0009175A File Offset: 0x0008F95A
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultDuplicateEmailErrorMessage")]
		[WebSysDescription("CreateUserWizard_DuplicateEmailErrorMessage")]
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

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x00091770 File Offset: 0x0008F970
		// (set) Token: 0x06002CC3 RID: 11459 RVA: 0x000917A2 File Offset: 0x0008F9A2
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultDuplicateUserNameErrorMessage")]
		[WebSysDescription("CreateUserWizard_DuplicateUserNameErrorMessage")]
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

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000917B8 File Offset: 0x0008F9B8
		// (set) Token: 0x06002CC5 RID: 11461 RVA: 0x0008B63D File Offset: 0x0008983D
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

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x000917E8 File Offset: 0x0008F9E8
		// (set) Token: 0x06002CC7 RID: 11463 RVA: 0x0008B67D File Offset: 0x0008987D
		[Localizable(true)]
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("CreateUserWizard_EditProfileText")]
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

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x00091818 File Offset: 0x0008FA18
		// (set) Token: 0x06002CC9 RID: 11465 RVA: 0x0008B6BD File Offset: 0x000898BD
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("CreateUserWizard_EditProfileUrl")]
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

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x00091848 File Offset: 0x0008FA48
		// (set) Token: 0x06002CCB RID: 11467 RVA: 0x00091875 File Offset: 0x0008FA75
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("CreateUserWizard_Email")]
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

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x00091888 File Offset: 0x0008FA88
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

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002CCD RID: 11469 RVA: 0x000918C8 File Offset: 0x0008FAC8
		// (set) Token: 0x06002CCE RID: 11470 RVA: 0x000918FA File Offset: 0x0008FAFA
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultEmailLabelText")]
		[WebSysDescription("CreateUserWizard_EmailLabelText")]
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

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x00091910 File Offset: 0x0008FB10
		// (set) Token: 0x06002CD0 RID: 11472 RVA: 0x0009193D File Offset: 0x0008FB3D
		[WebCategory("Validation")]
		[WebSysDefaultValue("")]
		[WebSysDescription("CreateUserWizard_EmailRegularExpression")]
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

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x00091950 File Offset: 0x0008FB50
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x00091982 File Offset: 0x0008FB82
		[WebCategory("Validation")]
		[WebSysDefaultValue("CreateUserWizard_DefaultEmailRegularExpressionErrorMessage")]
		[WebSysDescription("CreateUserWizard_EmailRegularExpressionErrorMessage")]
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

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x00091998 File Offset: 0x0008FB98
		// (set) Token: 0x06002CD4 RID: 11476 RVA: 0x000919CA File Offset: 0x0008FBCA
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("CreateUserWizard_DefaultEmailRequiredErrorMessage")]
		[WebSysDescription("CreateUserWizard_EmailRequiredErrorMessage")]
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

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x000919E0 File Offset: 0x0008FBE0
		// (set) Token: 0x06002CD6 RID: 11478 RVA: 0x00091A12 File Offset: 0x0008FC12
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultUnknownErrorMessage")]
		[WebSysDescription("CreateUserWizard_UnknownErrorMessage")]
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

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x00091A25 File Offset: 0x0008FC25
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("CreateUserWizard_ErrorMessageStyle")]
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

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x00091A54 File Offset: 0x0008FC54
		// (set) Token: 0x06002CD9 RID: 11481 RVA: 0x0008B72D File Offset: 0x0008992D
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

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x00091A84 File Offset: 0x0008FC84
		// (set) Token: 0x06002CDB RID: 11483 RVA: 0x0008B76D File Offset: 0x0008996D
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

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x00091AB4 File Offset: 0x0008FCB4
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x0008B7AD File Offset: 0x000899AD
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

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x00091AE1 File Offset: 0x0008FCE1
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

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06002CDF RID: 11487 RVA: 0x00091B10 File Offset: 0x0008FD10
		// (set) Token: 0x06002CE0 RID: 11488 RVA: 0x0008B81D File Offset: 0x00089A1D
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

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x00091B3D File Offset: 0x0008FD3D
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

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x00091B6C File Offset: 0x0008FD6C
		// (set) Token: 0x06002CE3 RID: 11491 RVA: 0x00091B9E File Offset: 0x0008FD9E
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultInvalidAnswerErrorMessage")]
		[WebSysDescription("CreateUserWizard_InvalidAnswerErrorMessage")]
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

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x00091BB4 File Offset: 0x0008FDB4
		// (set) Token: 0x06002CE5 RID: 11493 RVA: 0x00091BE6 File Offset: 0x0008FDE6
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultInvalidEmailErrorMessage")]
		[WebSysDescription("CreateUserWizard_InvalidEmailErrorMessage")]
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

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x00091BFC File Offset: 0x0008FDFC
		// (set) Token: 0x06002CE7 RID: 11495 RVA: 0x00091C2E File Offset: 0x0008FE2E
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultInvalidPasswordErrorMessage")]
		[WebSysDescription("CreateUserWizard_InvalidPasswordErrorMessage")]
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

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06002CE8 RID: 11496 RVA: 0x00091C44 File Offset: 0x0008FE44
		// (set) Token: 0x06002CE9 RID: 11497 RVA: 0x00091C76 File Offset: 0x0008FE76
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultInvalidQuestionErrorMessage")]
		[WebSysDescription("CreateUserWizard_InvalidQuestionErrorMessage")]
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

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x00091C89 File Offset: 0x0008FE89
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

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x00091CB8 File Offset: 0x0008FEB8
		// (set) Token: 0x06002CEC RID: 11500 RVA: 0x00091CE1 File Offset: 0x0008FEE1
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[Themeable(false)]
		[WebSysDescription("CreateUserWizard_LoginCreatedUser")]
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

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x00091CF9 File Offset: 0x0008FEF9
		[WebCategory("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Themeable(false)]
		[WebSysDescription("CreateUserWizard_MailDefinition")]
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

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x00091D28 File Offset: 0x0008FF28
		// (set) Token: 0x06002CEF RID: 11503 RVA: 0x00091D55 File Offset: 0x0008FF55
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
				if (this.MembershipProvider != value)
				{
					this.ViewState["MembershipProvider"] = value;
					base.RequiresControlsRecreation();
				}
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x00091D7C File Offset: 0x0008FF7C
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

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x00091D94 File Offset: 0x0008FF94
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

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x00091DDC File Offset: 0x0008FFDC
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x00091E0C File Offset: 0x0009000C
		// (set) Token: 0x06002CF4 RID: 11508 RVA: 0x0008BAA9 File Offset: 0x00089CA9
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("")]
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

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x00091E3C File Offset: 0x0009003C
		// (set) Token: 0x06002CF6 RID: 11510 RVA: 0x0008BAEE File Offset: 0x00089CEE
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

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x00091E70 File Offset: 0x00090070
		// (set) Token: 0x06002CF8 RID: 11512 RVA: 0x00091E9D File Offset: 0x0009009D
		[WebCategory("Validation")]
		[WebSysDefaultValue("")]
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

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06002CF9 RID: 11513 RVA: 0x00091EB0 File Offset: 0x000900B0
		// (set) Token: 0x06002CFA RID: 11514 RVA: 0x00091EE2 File Offset: 0x000900E2
		[WebCategory("Validation")]
		[WebSysDefaultValue("Password_InvalidPasswordErrorMessage")]
		[WebSysDescription("CreateUserWizard_PasswordRegularExpressionErrorMessage")]
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

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06002CFB RID: 11515 RVA: 0x00091EF8 File Offset: 0x000900F8
		// (set) Token: 0x06002CFC RID: 11516 RVA: 0x0008BBF6 File Offset: 0x00089DF6
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("CreateUserWizard_DefaultPasswordRequiredErrorMessage")]
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

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x00091F2C File Offset: 0x0009012C
		// (set) Token: 0x06002CFE RID: 11518 RVA: 0x00091F59 File Offset: 0x00090159
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Themeable(false)]
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

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x00091F6C File Offset: 0x0009016C
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

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x00091FB6 File Offset: 0x000901B6
		[WebCategory("Validation")]
		[DefaultValue(true)]
		[WebSysDescription("CreateUserWizard_QuestionAndAnswerRequired")]
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

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x00091FEC File Offset: 0x000901EC
		// (set) Token: 0x06002D02 RID: 11522 RVA: 0x0009201E File Offset: 0x0009021E
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultQuestionLabelText")]
		[WebSysDescription("CreateUserWizard_QuestionLabelText")]
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

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x00092034 File Offset: 0x00090234
		// (set) Token: 0x06002D04 RID: 11524 RVA: 0x00092066 File Offset: 0x00090266
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("CreateUserWizard_DefaultQuestionRequiredErrorMessage")]
		[WebSysDescription("CreateUserWizard_QuestionRequiredErrorMessage")]
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

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x0009207C File Offset: 0x0009027C
		// (set) Token: 0x06002D06 RID: 11526 RVA: 0x000920A5 File Offset: 0x000902A5
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[Themeable(false)]
		[WebSysDescription("CreateUserWizard_RequireEmail")]
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

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x000920C6 File Offset: 0x000902C6
		internal override bool ShowCustomNavigationTemplate
		{
			get
			{
				return base.ShowCustomNavigationTemplate || base.ActiveStep == this.CreateUserStep;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06002D08 RID: 11528 RVA: 0x000920E0 File Offset: 0x000902E0
		// (set) Token: 0x06002D09 RID: 11529 RVA: 0x000920FE File Offset: 0x000902FE
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

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x00092107 File Offset: 0x00090307
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

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x00092135 File Offset: 0x00090335
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

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x00092164 File Offset: 0x00090364
		// (set) Token: 0x06002D0D RID: 11533 RVA: 0x00092191 File Offset: 0x00090391
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

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x000921A4 File Offset: 0x000903A4
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

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x000921E4 File Offset: 0x000903E4
		// (set) Token: 0x06002D10 RID: 11536 RVA: 0x0008BEA6 File Offset: 0x0008A0A6
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("CreateUserWizard_DefaultUserNameLabelText")]
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

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06002D11 RID: 11537 RVA: 0x00092218 File Offset: 0x00090418
		// (set) Token: 0x06002D12 RID: 11538 RVA: 0x0008BEEE File Offset: 0x0008A0EE
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("CreateUserWizard_DefaultUserNameRequiredErrorMessage")]
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
				return SR.GetString("CreateUserWizard_DefaultUserNameRequiredErrorMessage");
			}
			set
			{
				this.ViewState["UserNameRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x0009224A File Offset: 0x0009044A
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("CreateUserWizard_ValidatorTextStyle")]
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

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x00092278 File Offset: 0x00090478
		private string ValidationGroup
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

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x0009229A File Offset: 0x0009049A
		[Editor("System.Web.UI.Design.WebControls.CreateUserWizardStepCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public override WizardStepCollection WizardSteps
		{
			get
			{
				return base.WizardSteps;
			}
		}

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06002D16 RID: 11542 RVA: 0x000922A2 File Offset: 0x000904A2
		// (remove) Token: 0x06002D17 RID: 11543 RVA: 0x000922B5 File Offset: 0x000904B5
		[WebCategory("Action")]
		[WebSysDescription("CreateUserWizard_ContinueButtonClick")]
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

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06002D18 RID: 11544 RVA: 0x000922C8 File Offset: 0x000904C8
		// (remove) Token: 0x06002D19 RID: 11545 RVA: 0x000922DB File Offset: 0x000904DB
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

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06002D1A RID: 11546 RVA: 0x000922EE File Offset: 0x000904EE
		// (remove) Token: 0x06002D1B RID: 11547 RVA: 0x00092301 File Offset: 0x00090501
		[WebCategory("Action")]
		[WebSysDescription("CreateUserWizard_CreatedUser")]
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

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06002D1C RID: 11548 RVA: 0x00092314 File Offset: 0x00090514
		// (remove) Token: 0x06002D1D RID: 11549 RVA: 0x00092327 File Offset: 0x00090527
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

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06002D1E RID: 11550 RVA: 0x0009233A File Offset: 0x0009053A
		// (remove) Token: 0x06002D1F RID: 11551 RVA: 0x0009234D File Offset: 0x0009054D
		[WebCategory("Action")]
		[WebSysDescription("ChangePassword_SendingMail")]
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

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06002D20 RID: 11552 RVA: 0x00092360 File Offset: 0x00090560
		// (remove) Token: 0x06002D21 RID: 11553 RVA: 0x00092373 File Offset: 0x00090573
		[WebCategory("Action")]
		[WebSysDescription("CreateUserWizard_SendMailError")]
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

		// Token: 0x06002D22 RID: 11554 RVA: 0x00092386 File Offset: 0x00090586
		private void AnswerTextChanged(object source, EventArgs e)
		{
			this.Answer = ((ITextControl)source).Text;
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x0009239C File Offset: 0x0009059C
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

		// Token: 0x06002D24 RID: 11556 RVA: 0x00092460 File Offset: 0x00090660
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

		// Token: 0x06002D25 RID: 11557 RVA: 0x00092C9C File Offset: 0x00090E9C
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

		// Token: 0x06002D26 RID: 11558 RVA: 0x00093048 File Offset: 0x00091248
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

		// Token: 0x06002D27 RID: 11559 RVA: 0x00093234 File Offset: 0x00091434
		private void AttemptLogin()
		{
			MembershipProvider provider = LoginUtil.GetProvider(this.MembershipProvider);
			if (provider.ValidateUser(this.UserName, this.Password))
			{
				FormsAuthentication.SetAuthCookie(this.UserNameInternal, false);
			}
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x0009326D File Offset: 0x0009146D
		private void ConfirmPasswordTextChanged(object source, EventArgs e)
		{
			if (!this.AutoGeneratePassword)
			{
				this._confirmPassword = ((ITextControl)source).Text;
			}
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x00093288 File Offset: 0x00091488
		protected internal override void CreateChildControls()
		{
			this._createUserStep = null;
			this._completeStep = null;
			base.CreateChildControls();
			this.UpdateValidators();
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000932A4 File Offset: 0x000914A4
		private void RegisterEvents()
		{
			CreateUserWizard.RegisterTextChangedEvent(this._createUserStepContainer.UserNameTextBox, new Action<object, EventArgs>(this.UserNameTextChanged));
			CreateUserWizard.RegisterTextChangedEvent(this._createUserStepContainer.EmailTextBox, new Action<object, EventArgs>(this.EmailTextChanged));
			CreateUserWizard.RegisterTextChangedEvent(this._createUserStepContainer.QuestionTextBox, new Action<object, EventArgs>(this.QuestionTextChanged));
			CreateUserWizard.RegisterTextChangedEvent(this._createUserStepContainer.AnswerTextBox, new Action<object, EventArgs>(this.AnswerTextChanged));
			CreateUserWizard.RegisterTextChangedEvent(this._createUserStepContainer.PasswordTextBox, new Action<object, EventArgs>(this.PasswordTextChanged));
			CreateUserWizard.RegisterTextChangedEvent(this._createUserStepContainer.ConfirmPasswordTextBox, new Action<object, EventArgs>(this.ConfirmPasswordTextChanged));
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x0009335C File Offset: 0x0009155C
		private static void RegisterTextChangedEvent(Control control, Action<object, EventArgs> textChangedHandler)
		{
			IEditableTextControl editableTextControl = control as IEditableTextControl;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += textChangedHandler.Invoke;
			}
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x00093385 File Offset: 0x00091585
		internal override Wizard.TableWizardRendering CreateTableRendering()
		{
			return new CreateUserWizard.TableWizardRendering(this);
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x0009338D File Offset: 0x0009158D
		internal override Wizard.LayoutTemplateWizardRendering CreateLayoutTemplateRendering()
		{
			return new CreateUserWizard.LayoutTemplateWizardRendering(this);
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x00093395 File Offset: 0x00091595
		internal override ITemplate CreateDefaultSideBarTemplate()
		{
			return new CreateUserWizard.DefaultSideBarTemplate();
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x0009339C File Offset: 0x0009159C
		internal override ITemplate CreateDefaultDataListItemTemplate()
		{
			return new CreateUserWizard.DataListItemTemplate();
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x000933A4 File Offset: 0x000915A4
		private static TableRow CreateTwoColumnRow(Control leftCellControl, params Control[] rightCellControls)
		{
			TableRow tableRow = CreateUserWizard.CreateTableRow();
			TableCell tableCell = CreateUserWizard.CreateTableCell();
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			tableCell.Controls.Add(leftCellControl);
			tableRow.Cells.Add(tableCell);
			TableCell tableCell2 = CreateUserWizard.CreateTableCell();
			foreach (Control child in rightCellControls)
			{
				tableCell2.Controls.Add(child);
			}
			tableRow.Cells.Add(tableCell2);
			return tableRow;
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x00093418 File Offset: 0x00091618
		private static TableRow CreateDoubleSpannedColumnRow(params Control[] cellControls)
		{
			return CreateUserWizard.CreateDoubleSpannedColumnRow(null, cellControls);
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x00093434 File Offset: 0x00091634
		private static TableRow CreateDoubleSpannedColumnRow(HorizontalAlign? cellHorizontalAlignment, params Control[] cellControls)
		{
			TableRow tableRow = CreateUserWizard.CreateTableRow();
			TableCell tableCell = CreateUserWizard.CreateTableCell();
			tableCell.ColumnSpan = 2;
			if (cellHorizontalAlignment != null)
			{
				tableCell.HorizontalAlign = cellHorizontalAlignment.Value;
			}
			foreach (Control child in cellControls)
			{
				tableCell.Controls.Add(child);
			}
			tableRow.Cells.Add(tableCell);
			return tableRow;
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x0009349C File Offset: 0x0009169C
		private static LabelLiteral CreateLabelLiteral(Control control)
		{
			LabelLiteral labelLiteral = new LabelLiteral(control);
			labelLiteral.PreventAutoID();
			return labelLiteral;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000934B8 File Offset: 0x000916B8
		private static Literal CreateLiteral()
		{
			Literal literal = new Literal();
			literal.PreventAutoID();
			return literal;
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000934D4 File Offset: 0x000916D4
		private static RequiredFieldValidator CreateRequiredFieldValidator(string id, string validationGroup, Control targetTextBox, bool enableValidation)
		{
			return new RequiredFieldValidator
			{
				ID = id,
				ControlToValidate = targetTextBox.ID,
				ValidationGroup = validationGroup,
				Display = ValidatorDisplay.Static,
				Text = SR.GetString("LoginControls_DefaultRequiredFieldValidatorText"),
				Enabled = enableValidation,
				Visible = enableValidation
			};
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x00093528 File Offset: 0x00091728
		private static Table CreateTable()
		{
			Table table = new Table();
			table.Width = Unit.Percentage(100.0);
			table.Height = Unit.Percentage(100.0);
			table.PreventAutoID();
			return table;
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x0009356C File Offset: 0x0009176C
		private static TableCell CreateTableCell()
		{
			TableCell tableCell = new TableCell();
			tableCell.PreventAutoID();
			return tableCell;
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x00093588 File Offset: 0x00091788
		private static TableRow CreateTableRow()
		{
			TableRow tableRow = new LoginUtil.DisappearingTableRow();
			tableRow.PreventAutoID();
			return tableRow;
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000935A4 File Offset: 0x000917A4
		internal override void CreateCustomNavigationTemplates()
		{
			for (int i = 0; i < this.WizardSteps.Count; i++)
			{
				TemplatedWizardStep templatedWizardStep = this.WizardSteps[i] as TemplatedWizardStep;
				if (templatedWizardStep != null)
				{
					string customContainerID = Wizard.GetCustomContainerID(i);
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

		// Token: 0x06002D3A RID: 11578 RVA: 0x00093644 File Offset: 0x00091844
		internal override void DataListItemDataBound(object sender, WizardSideBarListControlItemEventArgs e)
		{
			WizardSideBarListControlItem item = e.Item;
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

		// Token: 0x06002D3B RID: 11579 RVA: 0x00093725 File Offset: 0x00091925
		private void EmailTextChanged(object source, EventArgs e)
		{
			this.Email = ((ITextControl)source).Text;
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x00093738 File Offset: 0x00091938
		private void EnsureCreateUserSteps()
		{
			bool flag = false;
			bool flag2 = false;
			foreach (object obj in this.WizardSteps)
			{
				WizardStepBase wizardStepBase = (WizardStepBase)obj;
				CreateUserWizardStep createUserWizardStep = wizardStepBase as CreateUserWizardStep;
				if (createUserWizardStep != null)
				{
					if (flag)
					{
						throw new HttpException(SR.GetString("CreateUserWizard_DuplicateCreateUserWizardStep"));
					}
					flag = true;
					this._createUserStep = createUserWizardStep;
				}
				else
				{
					CompleteWizardStep completeWizardStep = wizardStepBase as CompleteWizardStep;
					if (completeWizardStep != null)
					{
						if (flag2)
						{
							throw new HttpException(SR.GetString("CreateUserWizard_DuplicateCompleteWizardStep"));
						}
						flag2 = true;
						this._completeStep = completeWizardStep;
					}
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

		// Token: 0x06002D3D RID: 11581 RVA: 0x00093864 File Offset: 0x00091A64
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			IDictionary designModeState = base.GetDesignModeState();
			WizardStepBase activeStep = base.ActiveStep;
			if (activeStep != null && activeStep == this.CreateUserStep)
			{
				designModeState["CustomNavigationControls"] = base.CustomNavigationContainers[base.ActiveStep].Controls;
			}
			Control errorMessageLabel = this._createUserStepContainer.ErrorMessageLabel;
			if (errorMessageLabel != null)
			{
				LoginUtil.SetTableCellVisible(errorMessageLabel, true);
			}
			return designModeState;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000938C4 File Offset: 0x00091AC4
		internal override void InstantiateStepContentTemplates()
		{
			bool useInnerTable = this.LayoutTemplate == null;
			foreach (object obj in this.WizardSteps)
			{
				WizardStepBase wizardStepBase = (WizardStepBase)obj;
				if (wizardStepBase == this.CreateUserStep)
				{
					wizardStepBase.Controls.Clear();
					this._createUserStepContainer = new CreateUserWizard.CreateUserStepContainer(this, useInnerTable);
					this._createUserStepContainer.ID = "CreateUserStepContainer";
					ITemplate template = this.CreateUserStep.ContentTemplate;
					if (template == null)
					{
						template = new CreateUserWizard.DefaultCreateUserContentTemplate(this);
					}
					else
					{
						this._createUserStepContainer.SetEnableTheming();
					}
					template.InstantiateIn(this._createUserStepContainer.Container);
					this.CreateUserStep.ContentTemplateContainer = this._createUserStepContainer;
					wizardStepBase.Controls.Add(this._createUserStepContainer);
				}
				else if (wizardStepBase == this.CompleteStep)
				{
					wizardStepBase.Controls.Clear();
					this._completeStepContainer = new CreateUserWizard.CompleteStepContainer(this, useInnerTable);
					this._completeStepContainer.ID = "CompleteStepContainer";
					ITemplate template2 = this.CompleteStep.ContentTemplate;
					if (template2 == null)
					{
						template2 = new CreateUserWizard.DefaultCompleteStepContentTemplate(this._completeStepContainer);
					}
					else
					{
						this._completeStepContainer.SetEnableTheming();
					}
					template2.InstantiateIn(this._completeStepContainer.Container);
					this.CompleteStep.ContentTemplateContainer = this._completeStepContainer;
					wizardStepBase.Controls.Add(this._completeStepContainer);
				}
				else
				{
					TemplatedWizardStep templatedWizardStep = wizardStepBase as TemplatedWizardStep;
					if (templatedWizardStep != null)
					{
						base.InstantiateStepContentTemplate(templatedWizardStep);
					}
				}
			}
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x00093A70 File Offset: 0x00091C70
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

		// Token: 0x06002D40 RID: 11584 RVA: 0x00093BA8 File Offset: 0x00091DA8
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

		// Token: 0x06002D41 RID: 11585 RVA: 0x00093DB0 File Offset: 0x00091FB0
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (commandEventArgs != null && commandEventArgs.CommandName.Equals(CreateUserWizard.ContinueButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
			{
				this.OnContinueButtonClick(EventArgs.Empty);
				return true;
			}
			return base.OnBubbleEvent(source, e);
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x00093DF0 File Offset: 0x00091FF0
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

		// Token: 0x06002D43 RID: 11587 RVA: 0x00093E48 File Offset: 0x00092048
		protected virtual void OnCreatedUser(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CreateUserWizard.EventCreatedUser];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x00093E78 File Offset: 0x00092078
		protected virtual void OnCreateUserError(CreateUserErrorEventArgs e)
		{
			CreateUserErrorEventHandler createUserErrorEventHandler = (CreateUserErrorEventHandler)base.Events[CreateUserWizard.EventCreateUserError];
			if (createUserErrorEventHandler != null)
			{
				createUserErrorEventHandler(this, e);
			}
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x00093EA8 File Offset: 0x000920A8
		protected virtual void OnCreatingUser(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[CreateUserWizard.EventCreatingUser];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x00093ED8 File Offset: 0x000920D8
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
							Control control = textControl as Control;
							if (control != null)
							{
								control.Visible = true;
							}
						}
					}
				}
			}
			base.OnNextButtonClick(e);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x00093F8C File Offset: 0x0009218C
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

		// Token: 0x06002D48 RID: 11592 RVA: 0x00093FD4 File Offset: 0x000921D4
		protected virtual void OnSendingMail(MailMessageEventArgs e)
		{
			MailMessageEventHandler mailMessageEventHandler = (MailMessageEventHandler)base.Events[CreateUserWizard.EventSendingMail];
			if (mailMessageEventHandler != null)
			{
				mailMessageEventHandler(this, e);
			}
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x00094004 File Offset: 0x00092204
		protected virtual void OnSendMailError(SendMailErrorEventArgs e)
		{
			SendMailErrorEventHandler sendMailErrorEventHandler = (SendMailErrorEventHandler)base.Events[CreateUserWizard.EventSendMailError];
			if (sendMailErrorEventHandler != null)
			{
				sendMailErrorEventHandler(this, e);
			}
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x00094032 File Offset: 0x00092232
		private void PasswordTextChanged(object source, EventArgs e)
		{
			if (!this.AutoGeneratePassword)
			{
				this._password = ((ITextControl)source).Text;
			}
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x0009404D File Offset: 0x0009224D
		private void QuestionTextChanged(object source, EventArgs e)
		{
			this.Question = ((ITextControl)source).Text;
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x00094060 File Offset: 0x00092260
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

		// Token: 0x06002D4D RID: 11597 RVA: 0x000941C4 File Offset: 0x000923C4
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

		// Token: 0x06002D4E RID: 11598 RVA: 0x000941F0 File Offset: 0x000923F0
		private void SetChildProperties()
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

		// Token: 0x06002D4F RID: 11599 RVA: 0x00094264 File Offset: 0x00092464
		private void SetDefaultCreateUserNavigationTemplateProperties()
		{
			WebControl webControl = (WebControl)this._defaultCreateUserNavigationTemplate.CreateUserButton;
			WebControl webControl2 = (WebControl)this._defaultCreateUserNavigationTemplate.PreviousButton;
			WebControl webControl3 = (WebControl)this._defaultCreateUserNavigationTemplate.CancelButton;
			this._defaultCreateUserNavigationTemplate.ApplyLayoutStyleToInnerCells(base.NavigationStyle);
			IButtonControl buttonControl = (IButtonControl)webControl;
			buttonControl.CausesValidation = true;
			buttonControl.Text = this.CreateUserButtonText;
			buttonControl.ValidationGroup = this.ValidationGroup;
			IButtonControl buttonControl2 = (IButtonControl)webControl2;
			buttonControl2.CausesValidation = false;
			buttonControl2.Text = this.StepPreviousButtonText;
			((IButtonControl)webControl3).Text = this.CancelButtonText;
			if (this._createUserButtonStyle != null)
			{
				webControl.ApplyStyle(this._createUserButtonStyle);
			}
			webControl.ControlStyle.MergeWith(base.NavigationButtonStyle);
			webControl.TabIndex = this.TabIndex;
			webControl.Visible = true;
			ImageButton imageButton = webControl as ImageButton;
			if (imageButton != null)
			{
				imageButton.ImageUrl = this.CreateUserButtonImageUrl;
				imageButton.AlternateText = this.CreateUserButtonText;
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
			ImageButton imageButton2 = webControl2 as ImageButton;
			if (imageButton2 != null)
			{
				imageButton2.AlternateText = this.StepPreviousButtonText;
				imageButton2.ImageUrl = this.StepPreviousButtonImageUrl;
			}
			if (this.DisplayCancelButton)
			{
				webControl3.ApplyStyle(base.CancelButtonStyle);
				webControl3.ControlStyle.MergeWith(base.NavigationButtonStyle);
				webControl3.TabIndex = this.TabIndex;
				webControl3.Visible = true;
				ImageButton imageButton3 = webControl3 as ImageButton;
				if (imageButton3 != null)
				{
					imageButton3.ImageUrl = this.CancelButtonImageUrl;
					imageButton3.AlternateText = this.CancelButtonText;
					return;
				}
			}
			else
			{
				webControl3.Parent.Visible = false;
			}
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x00094458 File Offset: 0x00092658
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

		// Token: 0x06002D51 RID: 11601 RVA: 0x0009454F File Offset: 0x0009274F
		private void UserNameTextChanged(object source, EventArgs e)
		{
			this.UserName = ((ITextControl)source).Text;
		}

		// Token: 0x04001F3C RID: 7996
		public static readonly string ContinueButtonCommandName = "Continue";

		// Token: 0x04001F3D RID: 7997
		private string _password;

		// Token: 0x04001F3E RID: 7998
		private string _confirmPassword;

		// Token: 0x04001F3F RID: 7999
		private string _answer;

		// Token: 0x04001F40 RID: 8000
		private string _unknownErrorMessage;

		// Token: 0x04001F41 RID: 8001
		private string _validationGroup;

		// Token: 0x04001F42 RID: 8002
		private CreateUserWizardStep _createUserStep;

		// Token: 0x04001F43 RID: 8003
		private CompleteWizardStep _completeStep;

		// Token: 0x04001F44 RID: 8004
		private CreateUserWizard.CreateUserStepContainer _createUserStepContainer;

		// Token: 0x04001F45 RID: 8005
		private CreateUserWizard.CompleteStepContainer _completeStepContainer;

		// Token: 0x04001F46 RID: 8006
		private const string _userNameReplacementKey = "<%\\s*UserName\\s*%>";

		// Token: 0x04001F47 RID: 8007
		private const string _passwordReplacementKey = "<%\\s*Password\\s*%>";

		// Token: 0x04001F48 RID: 8008
		private bool _failure;

		// Token: 0x04001F49 RID: 8009
		private bool _convertingToTemplate;

		// Token: 0x04001F4A RID: 8010
		private CreateUserWizard.DefaultCreateUserNavigationTemplate _defaultCreateUserNavigationTemplate;

		// Token: 0x04001F4B RID: 8011
		private const int _viewStateArrayLength = 13;

		// Token: 0x04001F4C RID: 8012
		private Style _createUserButtonStyle;

		// Token: 0x04001F4D RID: 8013
		private TableItemStyle _labelStyle;

		// Token: 0x04001F4E RID: 8014
		private Style _textBoxStyle;

		// Token: 0x04001F4F RID: 8015
		private TableItemStyle _hyperLinkStyle;

		// Token: 0x04001F50 RID: 8016
		private TableItemStyle _instructionTextStyle;

		// Token: 0x04001F51 RID: 8017
		private TableItemStyle _titleTextStyle;

		// Token: 0x04001F52 RID: 8018
		private TableItemStyle _errorMessageStyle;

		// Token: 0x04001F53 RID: 8019
		private TableItemStyle _passwordHintStyle;

		// Token: 0x04001F54 RID: 8020
		private Style _continueButtonStyle;

		// Token: 0x04001F55 RID: 8021
		private TableItemStyle _completeSuccessTextStyle;

		// Token: 0x04001F56 RID: 8022
		private Style _validatorTextStyle;

		// Token: 0x04001F57 RID: 8023
		private MailDefinition _mailDefinition;

		// Token: 0x04001F58 RID: 8024
		private static readonly object EventCreatingUser = new object();

		// Token: 0x04001F59 RID: 8025
		private static readonly object EventCreateUserError = new object();

		// Token: 0x04001F5A RID: 8026
		private static readonly object EventCreatedUser = new object();

		// Token: 0x04001F5B RID: 8027
		private static readonly object EventButtonContinueClick = new object();

		// Token: 0x04001F5C RID: 8028
		private static readonly object EventSendingMail = new object();

		// Token: 0x04001F5D RID: 8029
		private static readonly object EventSendMailError = new object();

		// Token: 0x04001F5E RID: 8030
		private const string _createUserNavigationTemplateName = "CreateUserNavigationTemplate";

		// Token: 0x04001F5F RID: 8031
		private const string _userNameID = "UserName";

		// Token: 0x04001F60 RID: 8032
		private const string _passwordID = "Password";

		// Token: 0x04001F61 RID: 8033
		private const string _confirmPasswordID = "ConfirmPassword";

		// Token: 0x04001F62 RID: 8034
		private const string _errorMessageID = "ErrorMessage";

		// Token: 0x04001F63 RID: 8035
		private const string _emailID = "Email";

		// Token: 0x04001F64 RID: 8036
		private const string _questionID = "Question";

		// Token: 0x04001F65 RID: 8037
		private const string _answerID = "Answer";

		// Token: 0x04001F66 RID: 8038
		private const string _userNameRequiredID = "UserNameRequired";

		// Token: 0x04001F67 RID: 8039
		private const string _passwordRequiredID = "PasswordRequired";

		// Token: 0x04001F68 RID: 8040
		private const string _confirmPasswordRequiredID = "ConfirmPasswordRequired";

		// Token: 0x04001F69 RID: 8041
		private const string _passwordRegExpID = "PasswordRegExp";

		// Token: 0x04001F6A RID: 8042
		private const string _emailRegExpID = "EmailRegExp";

		// Token: 0x04001F6B RID: 8043
		private const string _emailRequiredID = "EmailRequired";

		// Token: 0x04001F6C RID: 8044
		private const string _questionRequiredID = "QuestionRequired";

		// Token: 0x04001F6D RID: 8045
		private const string _answerRequiredID = "AnswerRequired";

		// Token: 0x04001F6E RID: 8046
		private const string _passwordCompareID = "PasswordCompare";

		// Token: 0x04001F6F RID: 8047
		private const string _continueButtonID = "ContinueButton";

		// Token: 0x04001F70 RID: 8048
		private const string _helpLinkID = "HelpLink";

		// Token: 0x04001F71 RID: 8049
		private const string _editProfileLinkID = "EditProfileLink";

		// Token: 0x04001F72 RID: 8050
		private const string _createUserStepContainerID = "CreateUserStepContainer";

		// Token: 0x04001F73 RID: 8051
		private const string _completeStepContainerID = "CompleteStepContainer";

		// Token: 0x04001F74 RID: 8052
		private const string _sideBarLabelID = "SideBarLabel";

		// Token: 0x04001F75 RID: 8053
		private const ValidatorDisplay _requiredFieldValidatorDisplay = ValidatorDisplay.Static;

		// Token: 0x04001F76 RID: 8054
		private const ValidatorDisplay _compareFieldValidatorDisplay = ValidatorDisplay.Dynamic;

		// Token: 0x04001F77 RID: 8055
		private const ValidatorDisplay _regexpFieldValidatorDisplay = ValidatorDisplay.Dynamic;

		// Token: 0x04001F78 RID: 8056
		private TableRow _passwordHintTableRow;

		// Token: 0x04001F79 RID: 8057
		private TableRow _questionRow;

		// Token: 0x04001F7A RID: 8058
		private TableRow _answerRow;

		// Token: 0x04001F7B RID: 8059
		private TableRow _emailRow;

		// Token: 0x04001F7C RID: 8060
		private TableRow _passwordCompareRow;

		// Token: 0x04001F7D RID: 8061
		private TableRow _passwordRegExpRow;

		// Token: 0x04001F7E RID: 8062
		private TableRow _emailRegExpRow;

		// Token: 0x04001F7F RID: 8063
		private TableRow _passwordTableRow;

		// Token: 0x04001F80 RID: 8064
		private TableRow _confirmPasswordTableRow;

		// Token: 0x04001F81 RID: 8065
		private const bool _displaySideBarDefaultValue = false;

		// Token: 0x02000998 RID: 2456
		private new class LayoutTemplateWizardRendering : Wizard.LayoutTemplateWizardRendering
		{
			// Token: 0x17001D73 RID: 7539
			// (get) Token: 0x06006ADF RID: 27359 RVA: 0x0017D38B File Offset: 0x0017B58B
			// (set) Token: 0x06006AE0 RID: 27360 RVA: 0x0017D393 File Offset: 0x0017B593
			private new CreateUserWizard Owner { get; set; }

			// Token: 0x06006AE1 RID: 27361 RVA: 0x0017D39C File Offset: 0x0017B59C
			public LayoutTemplateWizardRendering(CreateUserWizard owner) : base(owner)
			{
				this.Owner = owner;
			}

			// Token: 0x06006AE2 RID: 27362 RVA: 0x0017D3AC File Offset: 0x0017B5AC
			public override void CreateControlHierarchy()
			{
				this.Owner.EnsureCreateUserSteps();
				base.CreateControlHierarchy();
				this.Owner.InstantiateStepContentTemplates();
				this.Owner.RegisterEvents();
				this.Owner.ApplyCommonCreateUserValues();
			}

			// Token: 0x06006AE3 RID: 27363 RVA: 0x0017D3E0 File Offset: 0x0017B5E0
			public override void ApplyControlProperties()
			{
				this.Owner.SetChildProperties();
				if (this.Owner.CreateUserStep.CustomNavigationTemplate == null)
				{
					this.Owner.SetDefaultCreateUserNavigationTemplateProperties();
				}
				base.ApplyControlProperties();
			}
		}

		// Token: 0x02000999 RID: 2457
		private new class TableWizardRendering : Wizard.TableWizardRendering
		{
			// Token: 0x17001D74 RID: 7540
			// (get) Token: 0x06006AE4 RID: 27364 RVA: 0x0017D410 File Offset: 0x0017B610
			// (set) Token: 0x06006AE5 RID: 27365 RVA: 0x0017D418 File Offset: 0x0017B618
			private new CreateUserWizard Owner { get; set; }

			// Token: 0x06006AE6 RID: 27366 RVA: 0x0017D421 File Offset: 0x0017B621
			public TableWizardRendering(CreateUserWizard wizard) : base(wizard)
			{
				this.Owner = wizard;
			}

			// Token: 0x06006AE7 RID: 27367 RVA: 0x0017D431 File Offset: 0x0017B631
			public override void ApplyControlProperties()
			{
				this.Owner.SetChildProperties();
				if (this.Owner.CreateUserStep.CustomNavigationTemplate == null)
				{
					this.Owner.SetDefaultCreateUserNavigationTemplateProperties();
				}
				base.ApplyControlProperties();
			}

			// Token: 0x06006AE8 RID: 27368 RVA: 0x0017D461 File Offset: 0x0017B661
			public override void CreateControlHierarchy()
			{
				this.Owner.EnsureCreateUserSteps();
				base.CreateControlHierarchy();
				this.Owner.RegisterEvents();
				this.Owner.ApplyCommonCreateUserValues();
			}
		}

		// Token: 0x0200099A RID: 2458
		private sealed class DefaultCompleteStepContentTemplate : ITemplate
		{
			// Token: 0x06006AE9 RID: 27369 RVA: 0x0017D48A File Offset: 0x0017B68A
			public DefaultCompleteStepContentTemplate(CreateUserWizard.CompleteStepContainer container)
			{
				this._completeContainer = container;
			}

			// Token: 0x06006AEA RID: 27370 RVA: 0x0017D49C File Offset: 0x0017B69C
			private static void ConstructControls(CreateUserWizard.CompleteStepContainer container)
			{
				container.Title = CreateUserWizard.CreateLiteral();
				container.SuccessTextLabel = CreateUserWizard.CreateLiteral();
				container.EditProfileLink = new HyperLink
				{
					ID = "EditProfileLink"
				};
				container.EditProfileIcon = new Image();
				container.EditProfileIcon.PreventAutoID();
				container.ContinueLinkButton = new LinkButton
				{
					ID = "ContinueButtonLinkButton",
					CommandName = CreateUserWizard.ContinueButtonCommandName,
					CausesValidation = false
				};
				container.ContinuePushButton = new Button
				{
					ID = "ContinueButtonButton",
					CommandName = CreateUserWizard.ContinueButtonCommandName,
					CausesValidation = false
				};
				container.ContinueImageButton = new ImageButton
				{
					ID = "ContinueButtonImageButton",
					CommandName = CreateUserWizard.ContinueButtonCommandName,
					CausesValidation = false
				};
			}

			// Token: 0x06006AEB RID: 27371 RVA: 0x0017D564 File Offset: 0x0017B764
			private static void LayoutControls(CreateUserWizard.CompleteStepContainer container)
			{
				Table table = CreateUserWizard.CreateTable();
				table.EnableViewState = false;
				CreateUserWizard.DefaultCompleteStepContentTemplate.AddTitleRow(table, container);
				CreateUserWizard.DefaultCompleteStepContentTemplate.AddSuccessTextRow(table, container);
				CreateUserWizard.DefaultCompleteStepContentTemplate.AddContinueRow(table, container);
				CreateUserWizard.DefaultCompleteStepContentTemplate.AddEditRow(table, container);
				container.LayoutTable = table;
				container.AddChildControl(table);
			}

			// Token: 0x06006AEC RID: 27372 RVA: 0x0017D5A8 File Offset: 0x0017B7A8
			private static void AddTitleRow(Table table, CreateUserWizard.CompleteStepContainer container)
			{
				TableRow row = CreateUserWizard.CreateDoubleSpannedColumnRow(new HorizontalAlign?(HorizontalAlign.Center), new Control[]
				{
					container.Title
				});
				table.Rows.Add(row);
			}

			// Token: 0x06006AED RID: 27373 RVA: 0x0017D5E0 File Offset: 0x0017B7E0
			private static void AddSuccessTextRow(Table table, CreateUserWizard.CompleteStepContainer container)
			{
				TableRow tableRow = CreateUserWizard.CreateTableRow();
				TableCell tableCell = CreateUserWizard.CreateTableCell();
				tableCell.Controls.Add(container.SuccessTextLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
			}

			// Token: 0x06006AEE RID: 27374 RVA: 0x0017D624 File Offset: 0x0017B824
			private static void AddContinueRow(Table table, CreateUserWizard.CompleteStepContainer container)
			{
				TableRow row = CreateUserWizard.CreateDoubleSpannedColumnRow(new HorizontalAlign?(HorizontalAlign.Right), new Control[]
				{
					container.ContinuePushButton,
					container.ContinueLinkButton,
					container.ContinueImageButton
				});
				table.Rows.Add(row);
			}

			// Token: 0x06006AEF RID: 27375 RVA: 0x0017D66C File Offset: 0x0017B86C
			private static void AddEditRow(Table table, CreateUserWizard.CompleteStepContainer container)
			{
				TableRow row = CreateUserWizard.CreateDoubleSpannedColumnRow(new Control[]
				{
					container.EditProfileIcon,
					container.EditProfileLink
				});
				table.Rows.Add(row);
			}

			// Token: 0x06006AF0 RID: 27376 RVA: 0x0017D6A4 File Offset: 0x0017B8A4
			void ITemplate.InstantiateIn(Control container)
			{
				CreateUserWizard.DefaultCompleteStepContentTemplate.ConstructControls(this._completeContainer);
				CreateUserWizard.DefaultCompleteStepContentTemplate.LayoutControls(this._completeContainer);
			}

			// Token: 0x0400390A RID: 14602
			private CreateUserWizard.CompleteStepContainer _completeContainer;
		}

		// Token: 0x0200099B RID: 2459
		private sealed class DefaultCreateUserContentTemplate : ITemplate
		{
			// Token: 0x06006AF1 RID: 27377 RVA: 0x0017D6BC File Offset: 0x0017B8BC
			internal DefaultCreateUserContentTemplate(CreateUserWizard wizard)
			{
				this._wizard = wizard;
			}

			// Token: 0x06006AF2 RID: 27378 RVA: 0x0017D6CC File Offset: 0x0017B8CC
			private void ConstructControls(CreateUserWizard.CreateUserStepContainer container)
			{
				string validationGroup = this._wizard.ValidationGroup;
				container.Title = CreateUserWizard.CreateLiteral();
				container.InstructionLabel = CreateUserWizard.CreateLiteral();
				container.PasswordHintLabel = CreateUserWizard.CreateLiteral();
				container.UserNameTextBox = new TextBox
				{
					ID = "UserName"
				};
				container.PasswordTextBox = new TextBox
				{
					ID = "Password",
					TextMode = TextBoxMode.Password
				};
				container.ConfirmPasswordTextBox = new TextBox
				{
					ID = "ConfirmPassword",
					TextMode = TextBoxMode.Password
				};
				bool flag = true;
				container.UserNameRequired = CreateUserWizard.CreateRequiredFieldValidator("UserNameRequired", validationGroup, container.UserNameTextBox, flag);
				container.UserNameLabel = CreateUserWizard.CreateLabelLiteral(container.UserNameTextBox);
				container.PasswordLabel = CreateUserWizard.CreateLabelLiteral(container.PasswordTextBox);
				container.ConfirmPasswordLabel = CreateUserWizard.CreateLabelLiteral(container.ConfirmPasswordTextBox);
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
				container.EmailTextBox = new TextBox
				{
					ID = "Email"
				};
				container.EmailRequired = CreateUserWizard.CreateRequiredFieldValidator("EmailRequired", validationGroup, container.EmailTextBox, flag);
				container.EmailLabel = CreateUserWizard.CreateLabelLiteral(container.EmailTextBox);
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
				container.PasswordRequired = CreateUserWizard.CreateRequiredFieldValidator("PasswordRequired", validationGroup, container.PasswordTextBox, flag);
				container.ConfirmPasswordRequired = CreateUserWizard.CreateRequiredFieldValidator("ConfirmPasswordRequired", validationGroup, container.ConfirmPasswordTextBox, flag);
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
				container.QuestionTextBox = new TextBox
				{
					ID = "Question"
				};
				container.AnswerTextBox = new TextBox
				{
					ID = "Answer"
				};
				container.QuestionRequired = CreateUserWizard.CreateRequiredFieldValidator("QuestionRequired", validationGroup, container.QuestionTextBox, flag);
				container.AnswerRequired = CreateUserWizard.CreateRequiredFieldValidator("AnswerRequired", validationGroup, container.AnswerTextBox, flag);
				container.QuestionLabel = CreateUserWizard.CreateLabelLiteral(container.QuestionTextBox);
				container.AnswerLabel = CreateUserWizard.CreateLabelLiteral(container.AnswerTextBox);
			}

			// Token: 0x06006AF3 RID: 27379 RVA: 0x0017D9F0 File Offset: 0x0017BBF0
			private void LayoutControls(CreateUserWizard.CreateUserStepContainer container)
			{
				Table table = CreateUserWizard.CreateTable();
				table.EnableViewState = false;
				CreateUserWizard.DefaultCreateUserContentTemplate.AddTitleRow(table, container);
				CreateUserWizard.DefaultCreateUserContentTemplate.AddInstructionRow(table, container);
				this.AddUserNameRow(table, container);
				this.AddPasswordRow(table, container);
				this.AddPasswordHintRow(table, container);
				this.AddConfirmPasswordRow(table, container);
				this.AddEmailRow(table, container);
				this.AddQuestionRow(table, container);
				this.AddAnswerRow(table, container);
				this.AddPasswordCompareValidatorRow(table, container);
				this.AddPasswordRegexValidatorRow(table, container);
				this.AddEmailRegexValidatorRow(table, container);
				CreateUserWizard.DefaultCreateUserContentTemplate.AddErrorMessageRow(table, container);
				CreateUserWizard.DefaultCreateUserContentTemplate.AddHelpPageLinkRow(table, container);
				container.AddChildControl(table);
			}

			// Token: 0x06006AF4 RID: 27380 RVA: 0x0017DA80 File Offset: 0x0017BC80
			private static void AddTitleRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				TableRow row = CreateUserWizard.CreateDoubleSpannedColumnRow(new HorizontalAlign?(HorizontalAlign.Center), new Control[]
				{
					container.Title
				});
				table.Rows.Add(row);
			}

			// Token: 0x06006AF5 RID: 27381 RVA: 0x0017DAB8 File Offset: 0x0017BCB8
			private static void AddInstructionRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				TableRow tableRow = CreateUserWizard.CreateDoubleSpannedColumnRow(new HorizontalAlign?(HorizontalAlign.Center), new Control[]
				{
					container.InstructionLabel
				});
				tableRow.PreventAutoID();
				table.Rows.Add(tableRow);
			}

			// Token: 0x06006AF6 RID: 27382 RVA: 0x0017DAF4 File Offset: 0x0017BCF4
			private void AddUserNameRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				if (this._wizard.ConvertingToTemplate)
				{
					container.UserNameLabel.RenderAsLabel = true;
				}
				TableRow row = CreateUserWizard.CreateTwoColumnRow(container.UserNameLabel, new Control[]
				{
					container.UserNameTextBox,
					container.UserNameRequired
				});
				table.Rows.Add(row);
			}

			// Token: 0x06006AF7 RID: 27383 RVA: 0x0017DB4C File Offset: 0x0017BD4C
			private void AddPasswordRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				if (this._wizard.ConvertingToTemplate)
				{
					container.PasswordLabel.RenderAsLabel = true;
				}
				List<Control> list = new List<Control>
				{
					container.PasswordTextBox
				};
				if (!this._wizard.AutoGeneratePassword)
				{
					list.Add(container.PasswordRequired);
				}
				TableRow tableRow = CreateUserWizard.CreateTwoColumnRow(container.PasswordLabel, list.ToArray());
				this._wizard._passwordTableRow = tableRow;
				table.Rows.Add(tableRow);
			}

			// Token: 0x06006AF8 RID: 27384 RVA: 0x0017DBC8 File Offset: 0x0017BDC8
			private void AddPasswordHintRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				TableRow tableRow = CreateUserWizard.CreateTableRow();
				TableCell cell = CreateUserWizard.CreateTableCell();
				tableRow.Cells.Add(cell);
				TableCell tableCell = CreateUserWizard.CreateTableCell();
				tableCell.Controls.Add(container.PasswordHintLabel);
				tableRow.Cells.Add(tableCell);
				this._wizard._passwordHintTableRow = tableRow;
				table.Rows.Add(tableRow);
			}

			// Token: 0x06006AF9 RID: 27385 RVA: 0x0017DC2C File Offset: 0x0017BE2C
			private void AddConfirmPasswordRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				if (this._wizard.ConvertingToTemplate)
				{
					container.ConfirmPasswordLabel.RenderAsLabel = true;
				}
				List<Control> list = new List<Control>
				{
					container.ConfirmPasswordTextBox
				};
				if (!this._wizard.AutoGeneratePassword)
				{
					list.Add(container.ConfirmPasswordRequired);
				}
				TableRow tableRow = CreateUserWizard.CreateTwoColumnRow(container.ConfirmPasswordLabel, list.ToArray());
				this._wizard._confirmPasswordTableRow = tableRow;
				table.Rows.Add(tableRow);
			}

			// Token: 0x06006AFA RID: 27386 RVA: 0x0017DCA8 File Offset: 0x0017BEA8
			private void AddEmailRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				if (this._wizard.ConvertingToTemplate)
				{
					container.EmailLabel.RenderAsLabel = true;
				}
				TableRow tableRow = CreateUserWizard.CreateTwoColumnRow(container.EmailLabel, new Control[]
				{
					container.EmailTextBox,
					container.EmailRequired
				});
				this._wizard._emailRow = tableRow;
				table.Rows.Add(tableRow);
			}

			// Token: 0x06006AFB RID: 27387 RVA: 0x0017DD0C File Offset: 0x0017BF0C
			private void AddQuestionRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				if (this._wizard.ConvertingToTemplate)
				{
					container.QuestionLabel.RenderAsLabel = true;
				}
				TableRow tableRow = CreateUserWizard.CreateTwoColumnRow(container.QuestionLabel, new Control[]
				{
					container.QuestionTextBox,
					container.QuestionRequired
				});
				this._wizard._questionRow = tableRow;
				table.Rows.Add(tableRow);
			}

			// Token: 0x06006AFC RID: 27388 RVA: 0x0017DD70 File Offset: 0x0017BF70
			private void AddAnswerRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				if (this._wizard.ConvertingToTemplate)
				{
					container.AnswerLabel.RenderAsLabel = true;
				}
				TableRow tableRow = CreateUserWizard.CreateTwoColumnRow(container.AnswerLabel, new Control[]
				{
					container.AnswerTextBox,
					container.AnswerRequired
				});
				this._wizard._answerRow = tableRow;
				table.Rows.Add(tableRow);
			}

			// Token: 0x06006AFD RID: 27389 RVA: 0x0017DDD4 File Offset: 0x0017BFD4
			private void AddPasswordCompareValidatorRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				TableRow tableRow = CreateUserWizard.CreateDoubleSpannedColumnRow(new HorizontalAlign?(HorizontalAlign.Center), new Control[]
				{
					container.PasswordCompareValidator
				});
				this._wizard._passwordCompareRow = tableRow;
				table.Rows.Add(tableRow);
			}

			// Token: 0x06006AFE RID: 27390 RVA: 0x0017DE18 File Offset: 0x0017C018
			private void AddPasswordRegexValidatorRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				TableRow tableRow = CreateUserWizard.CreateDoubleSpannedColumnRow(new HorizontalAlign?(HorizontalAlign.Center), new Control[]
				{
					container.PasswordRegExpValidator
				});
				this._wizard._passwordRegExpRow = tableRow;
				table.Rows.Add(tableRow);
			}

			// Token: 0x06006AFF RID: 27391 RVA: 0x0017DE5C File Offset: 0x0017C05C
			private void AddEmailRegexValidatorRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				TableRow tableRow = CreateUserWizard.CreateDoubleSpannedColumnRow(new HorizontalAlign?(HorizontalAlign.Center), new Control[]
				{
					container.EmailRegExpValidator
				});
				this._wizard._emailRegExpRow = tableRow;
				table.Rows.Add(tableRow);
			}

			// Token: 0x06006B00 RID: 27392 RVA: 0x0017DEA0 File Offset: 0x0017C0A0
			private static void AddErrorMessageRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				TableRow row = CreateUserWizard.CreateDoubleSpannedColumnRow(new HorizontalAlign?(HorizontalAlign.Center), new Control[]
				{
					container.ErrorMessageLabel
				});
				table.Rows.Add(row);
			}

			// Token: 0x06006B01 RID: 27393 RVA: 0x0017DED8 File Offset: 0x0017C0D8
			private static void AddHelpPageLinkRow(Table table, CreateUserWizard.CreateUserStepContainer container)
			{
				TableRow row = CreateUserWizard.CreateDoubleSpannedColumnRow(new Control[]
				{
					container.HelpPageIcon,
					container.HelpPageLink
				});
				table.Rows.Add(row);
			}

			// Token: 0x06006B02 RID: 27394 RVA: 0x0017DF10 File Offset: 0x0017C110
			void ITemplate.InstantiateIn(Control container)
			{
				CreateUserWizard.CreateUserStepContainer createUserStepContainer = this._wizard._createUserStepContainer;
				this.ConstructControls(createUserStepContainer);
				this.LayoutControls(createUserStepContainer);
			}

			// Token: 0x0400390B RID: 14603
			private CreateUserWizard _wizard;
		}

		// Token: 0x0200099C RID: 2460
		private sealed class DefaultCreateUserNavigationTemplate : ITemplate
		{
			// Token: 0x06006B03 RID: 27395 RVA: 0x0017DF37 File Offset: 0x0017C137
			internal DefaultCreateUserNavigationTemplate(CreateUserWizard wizard)
			{
				this._wizard = wizard;
			}

			// Token: 0x06006B04 RID: 27396 RVA: 0x0017DF48 File Offset: 0x0017C148
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

			// Token: 0x06006B05 RID: 27397 RVA: 0x0017DFA8 File Offset: 0x0017C1A8
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

			// Token: 0x06006B06 RID: 27398 RVA: 0x0017E0D5 File Offset: 0x0017C2D5
			private void OnPreRender(object source, EventArgs e)
			{
				((ImageButton)source).Visible = false;
			}

			// Token: 0x06006B07 RID: 27399 RVA: 0x0017E0E4 File Offset: 0x0017C2E4
			private TableCell CreateButtonControl(IButtonControl[] buttons, string validationGroup, string id, bool causesValidation, string commandName)
			{
				LinkButton linkButton = new LinkButton
				{
					CausesValidation = causesValidation,
					ID = id + "LinkButton",
					Visible = false,
					CommandName = commandName,
					ValidationGroup = validationGroup
				};
				buttons[0] = linkButton;
				ImageButton imageButton = new ImageButton
				{
					CausesValidation = causesValidation,
					ID = id + "ImageButton",
					Visible = !this._wizard.DesignMode,
					CommandName = commandName,
					ValidationGroup = validationGroup
				};
				imageButton.PreRender += this.OnPreRender;
				buttons[1] = imageButton;
				Button button = new Button
				{
					CausesValidation = causesValidation,
					ID = id + "Button",
					Visible = false,
					CommandName = commandName,
					ValidationGroup = validationGroup
				};
				buttons[2] = button;
				TableCell tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				this._row.Cells.Add(tableCell);
				tableCell.Controls.Add(linkButton);
				tableCell.Controls.Add(imageButton);
				tableCell.Controls.Add(button);
				return tableCell;
			}

			// Token: 0x17001D75 RID: 7541
			// (get) Token: 0x06006B08 RID: 27400 RVA: 0x0017E1FF File Offset: 0x0017C3FF
			internal IButtonControl PreviousButton
			{
				get
				{
					return this.GetButtonBasedOnType(0, this._wizard.StepPreviousButtonType);
				}
			}

			// Token: 0x17001D76 RID: 7542
			// (get) Token: 0x06006B09 RID: 27401 RVA: 0x0017E213 File Offset: 0x0017C413
			internal IButtonControl CreateUserButton
			{
				get
				{
					return this.GetButtonBasedOnType(1, this._wizard.CreateUserButtonType);
				}
			}

			// Token: 0x17001D77 RID: 7543
			// (get) Token: 0x06006B0A RID: 27402 RVA: 0x0017E227 File Offset: 0x0017C427
			internal IButtonControl CancelButton
			{
				get
				{
					return this.GetButtonBasedOnType(2, this._wizard.CancelButtonType);
				}
			}

			// Token: 0x06006B0B RID: 27403 RVA: 0x0017E23B File Offset: 0x0017C43B
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

			// Token: 0x0400390C RID: 14604
			private CreateUserWizard _wizard;

			// Token: 0x0400390D RID: 14605
			private TableRow _row;

			// Token: 0x0400390E RID: 14606
			private IButtonControl[][] _buttons;

			// Token: 0x0400390F RID: 14607
			private TableCell[] _innerCells;
		}

		// Token: 0x0200099D RID: 2461
		private sealed class DataListItemTemplate : ITemplate
		{
			// Token: 0x06006B0C RID: 27404 RVA: 0x0017E274 File Offset: 0x0017C474
			public void InstantiateIn(Control container)
			{
				Label label = new Label();
				label.PreventAutoID();
				label.ID = "SideBarLabel";
				container.Controls.Add(label);
			}
		}

		// Token: 0x0200099E RID: 2462
		private sealed class DefaultSideBarTemplate : ITemplate
		{
			// Token: 0x06006B0E RID: 27406 RVA: 0x0017E2A4 File Offset: 0x0017C4A4
			public void InstantiateIn(Control container)
			{
				DataList dataList = new DataList();
				dataList.ID = Wizard.DataListID;
				container.Controls.Add(dataList);
				dataList.SelectedItemStyle.Font.Bold = true;
				dataList.ItemTemplate = new CreateUserWizard.DataListItemTemplate();
			}
		}

		// Token: 0x0200099F RID: 2463
		private sealed class CreateUserStepContainer : Wizard.BaseContentTemplateContainer
		{
			// Token: 0x06006B10 RID: 27408 RVA: 0x0017E2EA File Offset: 0x0017C4EA
			internal CreateUserStepContainer(CreateUserWizard wizard, bool useInnerTable) : base(wizard, useInnerTable)
			{
				this._createUserWizard = wizard;
			}

			// Token: 0x17001D78 RID: 7544
			// (get) Token: 0x06006B11 RID: 27409 RVA: 0x0017E2FB File Offset: 0x0017C4FB
			// (set) Token: 0x06006B12 RID: 27410 RVA: 0x0017E303 File Offset: 0x0017C503
			internal LabelLiteral AnswerLabel { get; set; }

			// Token: 0x17001D79 RID: 7545
			// (get) Token: 0x06006B13 RID: 27411 RVA: 0x0017E30C File Offset: 0x0017C50C
			// (set) Token: 0x06006B14 RID: 27412 RVA: 0x0017E314 File Offset: 0x0017C514
			internal RequiredFieldValidator AnswerRequired { get; set; }

			// Token: 0x17001D7A RID: 7546
			// (get) Token: 0x06006B15 RID: 27413 RVA: 0x0017E320 File Offset: 0x0017C520
			// (set) Token: 0x06006B16 RID: 27414 RVA: 0x0017E399 File Offset: 0x0017C599
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

			// Token: 0x17001D7B RID: 7547
			// (get) Token: 0x06006B17 RID: 27415 RVA: 0x0017E3A2 File Offset: 0x0017C5A2
			// (set) Token: 0x06006B18 RID: 27416 RVA: 0x0017E3AA File Offset: 0x0017C5AA
			internal LabelLiteral ConfirmPasswordLabel { get; set; }

			// Token: 0x17001D7C RID: 7548
			// (get) Token: 0x06006B19 RID: 27417 RVA: 0x0017E3B3 File Offset: 0x0017C5B3
			// (set) Token: 0x06006B1A RID: 27418 RVA: 0x0017E3BB File Offset: 0x0017C5BB
			internal RequiredFieldValidator ConfirmPasswordRequired { get; set; }

			// Token: 0x17001D7D RID: 7549
			// (get) Token: 0x06006B1B RID: 27419 RVA: 0x0017E3C4 File Offset: 0x0017C5C4
			// (set) Token: 0x06006B1C RID: 27420 RVA: 0x0017E3F7 File Offset: 0x0017C5F7
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

			// Token: 0x17001D7E RID: 7550
			// (get) Token: 0x06006B1D RID: 27421 RVA: 0x0017E400 File Offset: 0x0017C600
			// (set) Token: 0x06006B1E RID: 27422 RVA: 0x0017E408 File Offset: 0x0017C608
			internal LabelLiteral EmailLabel { get; set; }

			// Token: 0x17001D7F RID: 7551
			// (get) Token: 0x06006B1F RID: 27423 RVA: 0x0017E411 File Offset: 0x0017C611
			// (set) Token: 0x06006B20 RID: 27424 RVA: 0x0017E419 File Offset: 0x0017C619
			internal RegularExpressionValidator EmailRegExpValidator { get; set; }

			// Token: 0x17001D80 RID: 7552
			// (get) Token: 0x06006B21 RID: 27425 RVA: 0x0017E422 File Offset: 0x0017C622
			// (set) Token: 0x06006B22 RID: 27426 RVA: 0x0017E42A File Offset: 0x0017C62A
			internal RequiredFieldValidator EmailRequired { get; set; }

			// Token: 0x17001D81 RID: 7553
			// (get) Token: 0x06006B23 RID: 27427 RVA: 0x0017E434 File Offset: 0x0017C634
			// (set) Token: 0x06006B24 RID: 27428 RVA: 0x0017E4AD File Offset: 0x0017C6AD
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

			// Token: 0x17001D82 RID: 7554
			// (get) Token: 0x06006B25 RID: 27429 RVA: 0x0017E4B6 File Offset: 0x0017C6B6
			// (set) Token: 0x06006B26 RID: 27430 RVA: 0x0017E4BE File Offset: 0x0017C6BE
			internal LabelLiteral PasswordLabel { get; set; }

			// Token: 0x17001D83 RID: 7555
			// (get) Token: 0x06006B27 RID: 27431 RVA: 0x0017E4C8 File Offset: 0x0017C6C8
			// (set) Token: 0x06006B28 RID: 27432 RVA: 0x0017E4FD File Offset: 0x0017C6FD
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

			// Token: 0x17001D84 RID: 7556
			// (get) Token: 0x06006B29 RID: 27433 RVA: 0x0017E506 File Offset: 0x0017C706
			// (set) Token: 0x06006B2A RID: 27434 RVA: 0x0017E50E File Offset: 0x0017C70E
			internal Image HelpPageIcon { get; set; }

			// Token: 0x17001D85 RID: 7557
			// (get) Token: 0x06006B2B RID: 27435 RVA: 0x0017E517 File Offset: 0x0017C717
			// (set) Token: 0x06006B2C RID: 27436 RVA: 0x0017E51F File Offset: 0x0017C71F
			internal HyperLink HelpPageLink { get; set; }

			// Token: 0x17001D86 RID: 7558
			// (get) Token: 0x06006B2D RID: 27437 RVA: 0x0017E528 File Offset: 0x0017C728
			// (set) Token: 0x06006B2E RID: 27438 RVA: 0x0017E530 File Offset: 0x0017C730
			internal Literal InstructionLabel { get; set; }

			// Token: 0x17001D87 RID: 7559
			// (get) Token: 0x06006B2F RID: 27439 RVA: 0x0017E539 File Offset: 0x0017C739
			// (set) Token: 0x06006B30 RID: 27440 RVA: 0x0017E541 File Offset: 0x0017C741
			internal CompareValidator PasswordCompareValidator { get; set; }

			// Token: 0x17001D88 RID: 7560
			// (get) Token: 0x06006B31 RID: 27441 RVA: 0x0017E54A File Offset: 0x0017C74A
			// (set) Token: 0x06006B32 RID: 27442 RVA: 0x0017E552 File Offset: 0x0017C752
			internal Literal PasswordHintLabel { get; set; }

			// Token: 0x17001D89 RID: 7561
			// (get) Token: 0x06006B33 RID: 27443 RVA: 0x0017E55B File Offset: 0x0017C75B
			// (set) Token: 0x06006B34 RID: 27444 RVA: 0x0017E563 File Offset: 0x0017C763
			internal RegularExpressionValidator PasswordRegExpValidator { get; set; }

			// Token: 0x17001D8A RID: 7562
			// (get) Token: 0x06006B35 RID: 27445 RVA: 0x0017E56C File Offset: 0x0017C76C
			// (set) Token: 0x06006B36 RID: 27446 RVA: 0x0017E574 File Offset: 0x0017C774
			internal RequiredFieldValidator PasswordRequired { get; set; }

			// Token: 0x17001D8B RID: 7563
			// (get) Token: 0x06006B37 RID: 27447 RVA: 0x0017E580 File Offset: 0x0017C780
			// (set) Token: 0x06006B38 RID: 27448 RVA: 0x0017E5F9 File Offset: 0x0017C7F9
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

			// Token: 0x17001D8C RID: 7564
			// (get) Token: 0x06006B39 RID: 27449 RVA: 0x0017E602 File Offset: 0x0017C802
			// (set) Token: 0x06006B3A RID: 27450 RVA: 0x0017E60A File Offset: 0x0017C80A
			internal Literal Title { get; set; }

			// Token: 0x17001D8D RID: 7565
			// (get) Token: 0x06006B3B RID: 27451 RVA: 0x0017E613 File Offset: 0x0017C813
			// (set) Token: 0x06006B3C RID: 27452 RVA: 0x0017E61B File Offset: 0x0017C81B
			internal LabelLiteral UserNameLabel { get; set; }

			// Token: 0x17001D8E RID: 7566
			// (get) Token: 0x06006B3D RID: 27453 RVA: 0x0017E624 File Offset: 0x0017C824
			// (set) Token: 0x06006B3E RID: 27454 RVA: 0x0017E62C File Offset: 0x0017C82C
			internal RequiredFieldValidator UserNameRequired { get; set; }

			// Token: 0x17001D8F RID: 7567
			// (get) Token: 0x06006B3F RID: 27455 RVA: 0x0017E635 File Offset: 0x0017C835
			// (set) Token: 0x06006B40 RID: 27456 RVA: 0x0017E63D File Offset: 0x0017C83D
			internal LabelLiteral QuestionLabel { get; set; }

			// Token: 0x17001D90 RID: 7568
			// (get) Token: 0x06006B41 RID: 27457 RVA: 0x0017E646 File Offset: 0x0017C846
			// (set) Token: 0x06006B42 RID: 27458 RVA: 0x0017E64E File Offset: 0x0017C84E
			internal RequiredFieldValidator QuestionRequired { get; set; }

			// Token: 0x17001D91 RID: 7569
			// (get) Token: 0x06006B43 RID: 27459 RVA: 0x0017E658 File Offset: 0x0017C858
			// (set) Token: 0x06006B44 RID: 27460 RVA: 0x0017E6D1 File Offset: 0x0017C8D1
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

			// Token: 0x17001D92 RID: 7570
			// (get) Token: 0x06006B45 RID: 27461 RVA: 0x0017E6DC File Offset: 0x0017C8DC
			// (set) Token: 0x06006B46 RID: 27462 RVA: 0x0017E748 File Offset: 0x0017C948
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

			// Token: 0x04003910 RID: 14608
			private CreateUserWizard _createUserWizard;

			// Token: 0x04003911 RID: 14609
			private Control _userNameTextBox;

			// Token: 0x04003912 RID: 14610
			private Control _passwordTextBox;

			// Token: 0x04003913 RID: 14611
			private Control _confirmPasswordTextBox;

			// Token: 0x04003914 RID: 14612
			private Control _emailTextBox;

			// Token: 0x04003915 RID: 14613
			private Control _questionTextBox;

			// Token: 0x04003916 RID: 14614
			private Control _answerTextBox;

			// Token: 0x04003917 RID: 14615
			private Control _unknownErrorMessageLabel;
		}

		// Token: 0x020009A0 RID: 2464
		private sealed class CompleteStepContainer : Wizard.BaseContentTemplateContainer
		{
			// Token: 0x06006B47 RID: 27463 RVA: 0x0017E751 File Offset: 0x0017C951
			internal CompleteStepContainer(CreateUserWizard wizard, bool useInnerTable) : base(wizard, useInnerTable)
			{
			}

			// Token: 0x17001D93 RID: 7571
			// (get) Token: 0x06006B48 RID: 27464 RVA: 0x0017E75B File Offset: 0x0017C95B
			// (set) Token: 0x06006B49 RID: 27465 RVA: 0x0017E763 File Offset: 0x0017C963
			internal LinkButton ContinueLinkButton { get; set; }

			// Token: 0x17001D94 RID: 7572
			// (get) Token: 0x06006B4A RID: 27466 RVA: 0x0017E76C File Offset: 0x0017C96C
			// (set) Token: 0x06006B4B RID: 27467 RVA: 0x0017E774 File Offset: 0x0017C974
			internal Button ContinuePushButton { get; set; }

			// Token: 0x17001D95 RID: 7573
			// (get) Token: 0x06006B4C RID: 27468 RVA: 0x0017E77D File Offset: 0x0017C97D
			// (set) Token: 0x06006B4D RID: 27469 RVA: 0x0017E785 File Offset: 0x0017C985
			internal ImageButton ContinueImageButton { get; set; }

			// Token: 0x17001D96 RID: 7574
			// (get) Token: 0x06006B4E RID: 27470 RVA: 0x0017E78E File Offset: 0x0017C98E
			// (set) Token: 0x06006B4F RID: 27471 RVA: 0x0017E796 File Offset: 0x0017C996
			internal Image EditProfileIcon { get; set; }

			// Token: 0x17001D97 RID: 7575
			// (get) Token: 0x06006B50 RID: 27472 RVA: 0x0017E79F File Offset: 0x0017C99F
			// (set) Token: 0x06006B51 RID: 27473 RVA: 0x0017E7A7 File Offset: 0x0017C9A7
			internal HyperLink EditProfileLink { get; set; }

			// Token: 0x17001D98 RID: 7576
			// (get) Token: 0x06006B52 RID: 27474 RVA: 0x0017E7B0 File Offset: 0x0017C9B0
			// (set) Token: 0x06006B53 RID: 27475 RVA: 0x0017E7B8 File Offset: 0x0017C9B8
			internal Table LayoutTable { get; set; }

			// Token: 0x17001D99 RID: 7577
			// (get) Token: 0x06006B54 RID: 27476 RVA: 0x0017E7C1 File Offset: 0x0017C9C1
			// (set) Token: 0x06006B55 RID: 27477 RVA: 0x0017E7C9 File Offset: 0x0017C9C9
			internal Literal SuccessTextLabel { get; set; }

			// Token: 0x17001D9A RID: 7578
			// (get) Token: 0x06006B56 RID: 27478 RVA: 0x0017E7D2 File Offset: 0x0017C9D2
			// (set) Token: 0x06006B57 RID: 27479 RVA: 0x0017E7DA File Offset: 0x0017C9DA
			internal Literal Title { get; set; }
		}
	}
}
