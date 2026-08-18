using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200049E RID: 1182
	[Bindable(false)]
	[DefaultEvent("SendingMail")]
	[Designer("System.Web.UI.Design.WebControls.PasswordRecoveryDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class PasswordRecovery : CompositeControl, IBorderPaddingControl, IRenderOuterTableControl
	{
		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x000BF1E3 File Offset: 0x000BD3E3
		[Browsable(false)]
		[Filterable(false)]
		[Themeable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
		}

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x000BF1FC File Offset: 0x000BD3FC
		private string AnswerInternal
		{
			get
			{
				string answer = this.Answer;
				if (string.IsNullOrEmpty(answer) && this._questionContainer != null)
				{
					ITextControl textControl = (ITextControl)this._questionContainer.AnswerTextBox;
					if (textControl != null && textControl.Text != null)
					{
						return textControl.Text;
					}
				}
				return answer;
			}
		}

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x000BF244 File Offset: 0x000BD444
		// (set) Token: 0x06003AF7 RID: 15095 RVA: 0x0009123A File Offset: 0x0008F43A
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultAnswerLabelText")]
		[WebSysDescription("PasswordRecovery_AnswerLabelText")]
		public virtual string AnswerLabelText
		{
			get
			{
				object obj = this.ViewState["AnswerLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("PasswordRecovery_DefaultAnswerLabelText");
			}
			set
			{
				this.ViewState["AnswerLabelText"] = value;
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x000BF278 File Offset: 0x000BD478
		// (set) Token: 0x06003AF9 RID: 15097 RVA: 0x00091282 File Offset: 0x0008F482
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("PasswordRecovery_DefaultAnswerRequiredErrorMessage")]
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
				return SR.GetString("PasswordRecovery_DefaultAnswerRequiredErrorMessage");
			}
			set
			{
				this.ViewState["AnswerRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x000BF2AC File Offset: 0x000BD4AC
		// (set) Token: 0x06003AFB RID: 15099 RVA: 0x000BF2D5 File Offset: 0x000BD4D5
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
					throw new ArgumentOutOfRangeException("value", SR.GetString("PasswordRecovery_InvalidBorderPadding"));
				}
				this.ViewState["BorderPadding"] = value;
			}
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x000BF306 File Offset: 0x000BD506
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("PasswordRecovery_SubmitButtonStyle")]
		public Style SubmitButtonStyle
		{
			get
			{
				if (this._submitButtonStyle == null)
				{
					this._submitButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._submitButtonStyle).TrackViewState();
					}
				}
				return this._submitButtonStyle;
			}
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x000BF334 File Offset: 0x000BD534
		// (set) Token: 0x06003AFE RID: 15102 RVA: 0x000BF35D File Offset: 0x000BD55D
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("PasswordRecovery_SubmitButtonType")]
		public virtual ButtonType SubmitButtonType
		{
			get
			{
				object obj = this.ViewState["SubmitButtonType"];
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
				this.ViewState["SubmitButtonType"] = value;
			}
		}

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x06003AFF RID: 15103 RVA: 0x000BF388 File Offset: 0x000BD588
		private bool ConvertingToTemplate
		{
			get
			{
				return base.DesignMode && this._convertingToTemplate;
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000BF39A File Offset: 0x000BD59A
		// (set) Token: 0x06003B01 RID: 15105 RVA: 0x000BF3A2 File Offset: 0x000BD5A2
		internal PasswordRecovery.View CurrentView
		{
			get
			{
				return this._currentView;
			}
			set
			{
				if (value < PasswordRecovery.View.UserName || value > PasswordRecovery.View.Success)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this.CurrentView)
				{
					this._currentView = value;
					this.UpdateValidators();
				}
			}
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x000BF3CD File Offset: 0x000BD5CD
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

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x000BF3FC File Offset: 0x000BD5FC
		// (set) Token: 0x06003B04 RID: 15108 RVA: 0x000BF42E File Offset: 0x000BD62E
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultGeneralFailureText")]
		[WebSysDescription("PasswordRecovery_GeneralFailureText")]
		public virtual string GeneralFailureText
		{
			get
			{
				object obj = this.ViewState["GeneralFailureText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("PasswordRecovery_DefaultGeneralFailureText");
			}
			set
			{
				this.ViewState["GeneralFailureText"] = value;
			}
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x000BF444 File Offset: 0x000BD644
		// (set) Token: 0x06003B06 RID: 15110 RVA: 0x0008B76D File Offset: 0x0008996D
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

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x06003B07 RID: 15111 RVA: 0x000BF474 File Offset: 0x000BD674
		// (set) Token: 0x06003B08 RID: 15112 RVA: 0x0008B72D File Offset: 0x0008992D
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

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x000BF4A4 File Offset: 0x000BD6A4
		// (set) Token: 0x06003B0A RID: 15114 RVA: 0x0008B7AD File Offset: 0x000899AD
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

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x000BF4D1 File Offset: 0x000BD6D1
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

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06003B0C RID: 15116 RVA: 0x000BF4FF File Offset: 0x000BD6FF
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

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x000BF52D File Offset: 0x000BD72D
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

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06003B0E RID: 15118 RVA: 0x000BF55B File Offset: 0x000BD75B
		[WebCategory("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Themeable(false)]
		[WebSysDescription("PasswordRecovery_MailDefinition")]
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

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06003B0F RID: 15119 RVA: 0x000BF58C File Offset: 0x000BD78C
		// (set) Token: 0x06003B10 RID: 15120 RVA: 0x0008B8B9 File Offset: 0x00089AB9
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

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x000BF5B9 File Offset: 0x000BD7B9
		// (set) Token: 0x06003B12 RID: 15122 RVA: 0x000BF5CF File Offset: 0x000BD7CF
		[Browsable(false)]
		[Filterable(false)]
		[Themeable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Question
		{
			get
			{
				if (this._question == null)
				{
					return string.Empty;
				}
				return this._question;
			}
			private set
			{
				this._question = value;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06003B13 RID: 15123 RVA: 0x000BF5D8 File Offset: 0x000BD7D8
		// (set) Token: 0x06003B14 RID: 15124 RVA: 0x000BF60A File Offset: 0x000BD80A
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultQuestionFailureText")]
		[WebSysDescription("PasswordRecovery_QuestionFailureText")]
		public virtual string QuestionFailureText
		{
			get
			{
				object obj = this.ViewState["QuestionFailureText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("PasswordRecovery_DefaultQuestionFailureText");
			}
			set
			{
				this.ViewState["QuestionFailureText"] = value;
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x000BF620 File Offset: 0x000BD820
		// (set) Token: 0x06003B16 RID: 15126 RVA: 0x000BF652 File Offset: 0x000BD852
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultQuestionInstructionText")]
		[WebSysDescription("PasswordRecovery_QuestionInstructionText")]
		public virtual string QuestionInstructionText
		{
			get
			{
				object obj = this.ViewState["QuestionInstructionText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("PasswordRecovery_DefaultQuestionInstructionText");
			}
			set
			{
				this.ViewState["QuestionInstructionText"] = value;
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06003B17 RID: 15127 RVA: 0x000BF668 File Offset: 0x000BD868
		// (set) Token: 0x06003B18 RID: 15128 RVA: 0x0009201E File Offset: 0x0009021E
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultQuestionLabelText")]
		[WebSysDescription("PasswordRecovery_QuestionLabelText")]
		public virtual string QuestionLabelText
		{
			get
			{
				object obj = this.ViewState["QuestionLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("PasswordRecovery_DefaultQuestionLabelText");
			}
			set
			{
				this.ViewState["QuestionLabelText"] = value;
			}
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x000BF69C File Offset: 0x000BD89C
		// (set) Token: 0x06003B1A RID: 15130 RVA: 0x000BF6CE File Offset: 0x000BD8CE
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultQuestionTitleText")]
		[WebSysDescription("PasswordRecovery_QuestionTitleText")]
		public virtual string QuestionTitleText
		{
			get
			{
				object obj = this.ViewState["QuestionTitleText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("PasswordRecovery_DefaultQuestionTitleText");
			}
			set
			{
				this.ViewState["QuestionTitleText"] = value;
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x000BF6E1 File Offset: 0x000BD8E1
		// (set) Token: 0x06003B1C RID: 15132 RVA: 0x000BF6E9 File Offset: 0x000BD8E9
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(PasswordRecovery))]
		[WebSysDescription("PasswordRecovery_QuestionTemplate")]
		public virtual ITemplate QuestionTemplate
		{
			get
			{
				return this._questionTemplate;
			}
			set
			{
				this._questionTemplate = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x000BF6F9 File Offset: 0x000BD8F9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("PasswordRecovery_QuestionTemplateContainer")]
		public Control QuestionTemplateContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._questionContainer;
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06003B1E RID: 15134 RVA: 0x000BF708 File Offset: 0x000BD908
		// (set) Token: 0x06003B1F RID: 15135 RVA: 0x000BF735 File Offset: 0x000BD935
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_ChangePasswordButtonImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string SubmitButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["SubmitButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["SubmitButtonImageUrl"] = value;
			}
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06003B20 RID: 15136 RVA: 0x000BF748 File Offset: 0x000BD948
		// (set) Token: 0x06003B21 RID: 15137 RVA: 0x000BF77A File Offset: 0x000BD97A
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultSubmitButtonText")]
		[WebSysDescription("ChangePassword_ChangePasswordButtonText")]
		public virtual string SubmitButtonText
		{
			get
			{
				object obj = this.ViewState["SubmitButtonText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("PasswordRecovery_DefaultSubmitButtonText");
			}
			set
			{
				this.ViewState["SubmitButtonText"] = value;
			}
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x000BF790 File Offset: 0x000BD990
		// (set) Token: 0x06003B23 RID: 15139 RVA: 0x0008BCB9 File Offset: 0x00089EB9
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

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x000BF7BD File Offset: 0x000BD9BD
		// (set) Token: 0x06003B25 RID: 15141 RVA: 0x000BF7C5 File Offset: 0x000BD9C5
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("PasswordRecovery_SuccessTemplate")]
		[TemplateContainer(typeof(PasswordRecovery))]
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

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x06003B26 RID: 15142 RVA: 0x000BF7D5 File Offset: 0x000BD9D5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("PasswordRecovery_SuccessTemplateContainer")]
		public Control SuccessTemplateContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._successContainer;
			}
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x000BF7E4 File Offset: 0x000BD9E4
		// (set) Token: 0x06003B28 RID: 15144 RVA: 0x0008BD26 File Offset: 0x00089F26
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultSuccessText")]
		[WebSysDescription("PasswordRecovery_SuccessText")]
		public virtual string SuccessText
		{
			get
			{
				object obj = this.ViewState["SuccessText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("PasswordRecovery_DefaultSuccessText");
			}
			set
			{
				this.ViewState["SuccessText"] = value;
			}
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06003B29 RID: 15145 RVA: 0x000BF816 File Offset: 0x000BDA16
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("PasswordRecovery_SuccessTextStyle")]
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

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x000BF844 File Offset: 0x000BDA44
		// (set) Token: 0x06003B2B RID: 15147 RVA: 0x0008BC71 File Offset: 0x00089E71
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

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x0008BDAD File Offset: 0x00089FAD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06003B2D RID: 15149 RVA: 0x000BF86D File Offset: 0x000BDA6D
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

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06003B2E RID: 15150 RVA: 0x000BF89C File Offset: 0x000BDA9C
		// (set) Token: 0x06003B2F RID: 15151 RVA: 0x000AF40D File Offset: 0x000AD60D
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

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x000BF8C5 File Offset: 0x000BDAC5
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

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06003B31 RID: 15153 RVA: 0x000BF8F3 File Offset: 0x000BDAF3
		// (set) Token: 0x06003B32 RID: 15154 RVA: 0x000BF909 File Offset: 0x000BDB09
		[Localizable(true)]
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

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06003B33 RID: 15155 RVA: 0x000BF914 File Offset: 0x000BDB14
		internal string UserNameInternal
		{
			get
			{
				string userName = this.UserName;
				if (string.IsNullOrEmpty(userName) && this._userNameContainer != null)
				{
					ITextControl textControl = this._userNameContainer.UserNameTextBox as ITextControl;
					if (textControl != null)
					{
						return textControl.Text;
					}
				}
				return userName;
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06003B34 RID: 15156 RVA: 0x000BF954 File Offset: 0x000BDB54
		// (set) Token: 0x06003B35 RID: 15157 RVA: 0x000BF986 File Offset: 0x000BDB86
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultUserNameFailureText")]
		[WebSysDescription("PasswordRecovery_UserNameFailureText")]
		public virtual string UserNameFailureText
		{
			get
			{
				object obj = this.ViewState["UserNameFailureText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("PasswordRecovery_DefaultUserNameFailureText");
			}
			set
			{
				this.ViewState["UserNameFailureText"] = value;
			}
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x06003B36 RID: 15158 RVA: 0x000BF99C File Offset: 0x000BDB9C
		// (set) Token: 0x06003B37 RID: 15159 RVA: 0x000BF9CE File Offset: 0x000BDBCE
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultUserNameInstructionText")]
		[WebSysDescription("PasswordRecovery_UserNameInstructionText")]
		public virtual string UserNameInstructionText
		{
			get
			{
				object obj = this.ViewState["UserNameInstructionText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("PasswordRecovery_DefaultUserNameInstructionText");
			}
			set
			{
				this.ViewState["UserNameInstructionText"] = value;
			}
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06003B38 RID: 15160 RVA: 0x000BF9E4 File Offset: 0x000BDBE4
		// (set) Token: 0x06003B39 RID: 15161 RVA: 0x0008BEA6 File Offset: 0x0008A0A6
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultUserNameLabelText")]
		[WebSysDescription("PasswordRecovery_UserNameLabelText")]
		public virtual string UserNameLabelText
		{
			get
			{
				object obj = this.ViewState["UserNameLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("PasswordRecovery_DefaultUserNameLabelText");
			}
			set
			{
				this.ViewState["UserNameLabelText"] = value;
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06003B3A RID: 15162 RVA: 0x000BFA18 File Offset: 0x000BDC18
		// (set) Token: 0x06003B3B RID: 15163 RVA: 0x0008BEEE File Offset: 0x0008A0EE
		[Localizable(true)]
		[WebCategory("Validation")]
		[WebSysDefaultValue("PasswordRecovery_DefaultUserNameRequiredErrorMessage")]
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
				return SR.GetString("PasswordRecovery_DefaultUserNameRequiredErrorMessage");
			}
			set
			{
				this.ViewState["UserNameRequiredErrorMessage"] = value;
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06003B3C RID: 15164 RVA: 0x000BFA4A File Offset: 0x000BDC4A
		// (set) Token: 0x06003B3D RID: 15165 RVA: 0x000BFA52 File Offset: 0x000BDC52
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(PasswordRecovery))]
		[WebSysDescription("PasswordRecovery_UserNameTemplate")]
		public virtual ITemplate UserNameTemplate
		{
			get
			{
				return this._userNameTemplate;
			}
			set
			{
				this._userNameTemplate = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06003B3E RID: 15166 RVA: 0x000BFA62 File Offset: 0x000BDC62
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("PasswordRecovery_UserNameTemplateContainer")]
		public Control UserNameTemplateContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._userNameContainer;
			}
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x000BFA70 File Offset: 0x000BDC70
		// (set) Token: 0x06003B40 RID: 15168 RVA: 0x000BFAA2 File Offset: 0x000BDCA2
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultUserNameTitleText")]
		[WebSysDescription("PasswordRecovery_UserNameTitleText")]
		public virtual string UserNameTitleText
		{
			get
			{
				object obj = this.ViewState["UserNameTitleText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("PasswordRecovery_DefaultUserNameTitleText");
			}
			set
			{
				this.ViewState["UserNameTitleText"] = value;
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06003B41 RID: 15169 RVA: 0x000BFAB5 File Offset: 0x000BDCB5
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

		// Token: 0x140000DB RID: 219
		// (add) Token: 0x06003B42 RID: 15170 RVA: 0x000BFAE3 File Offset: 0x000BDCE3
		// (remove) Token: 0x06003B43 RID: 15171 RVA: 0x000BFAF6 File Offset: 0x000BDCF6
		[WebCategory("Action")]
		[WebSysDescription("PasswordRecovery_AnswerLookupError")]
		public event EventHandler AnswerLookupError
		{
			add
			{
				base.Events.AddHandler(PasswordRecovery.EventAnswerLookupError, value);
			}
			remove
			{
				base.Events.RemoveHandler(PasswordRecovery.EventAnswerLookupError, value);
			}
		}

		// Token: 0x140000DC RID: 220
		// (add) Token: 0x06003B44 RID: 15172 RVA: 0x000BFB09 File Offset: 0x000BDD09
		// (remove) Token: 0x06003B45 RID: 15173 RVA: 0x000BFB1C File Offset: 0x000BDD1C
		[WebCategory("Action")]
		[WebSysDescription("PasswordRecovery_VerifyingAnswer")]
		public event LoginCancelEventHandler VerifyingAnswer
		{
			add
			{
				base.Events.AddHandler(PasswordRecovery.EventVerifyingAnswer, value);
			}
			remove
			{
				base.Events.RemoveHandler(PasswordRecovery.EventVerifyingAnswer, value);
			}
		}

		// Token: 0x140000DD RID: 221
		// (add) Token: 0x06003B46 RID: 15174 RVA: 0x000BFB2F File Offset: 0x000BDD2F
		// (remove) Token: 0x06003B47 RID: 15175 RVA: 0x000BFB42 File Offset: 0x000BDD42
		[WebCategory("Action")]
		[WebSysDescription("PasswordRecovery_SendingMail")]
		public event MailMessageEventHandler SendingMail
		{
			add
			{
				base.Events.AddHandler(PasswordRecovery.EventSendingMail, value);
			}
			remove
			{
				base.Events.RemoveHandler(PasswordRecovery.EventSendingMail, value);
			}
		}

		// Token: 0x140000DE RID: 222
		// (add) Token: 0x06003B48 RID: 15176 RVA: 0x000BFB55 File Offset: 0x000BDD55
		// (remove) Token: 0x06003B49 RID: 15177 RVA: 0x000BFB68 File Offset: 0x000BDD68
		[WebCategory("Action")]
		[WebSysDescription("CreateUserWizard_SendMailError")]
		public event SendMailErrorEventHandler SendMailError
		{
			add
			{
				base.Events.AddHandler(PasswordRecovery.EventSendMailError, value);
			}
			remove
			{
				base.Events.RemoveHandler(PasswordRecovery.EventSendMailError, value);
			}
		}

		// Token: 0x140000DF RID: 223
		// (add) Token: 0x06003B4A RID: 15178 RVA: 0x000BFB7B File Offset: 0x000BDD7B
		// (remove) Token: 0x06003B4B RID: 15179 RVA: 0x000BFB8E File Offset: 0x000BDD8E
		[WebCategory("Action")]
		[WebSysDescription("PasswordRecovery_VerifyingUser")]
		public event LoginCancelEventHandler VerifyingUser
		{
			add
			{
				base.Events.AddHandler(PasswordRecovery.EventVerifyingUser, value);
			}
			remove
			{
				base.Events.RemoveHandler(PasswordRecovery.EventVerifyingUser, value);
			}
		}

		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x06003B4C RID: 15180 RVA: 0x000BFBA1 File Offset: 0x000BDDA1
		// (remove) Token: 0x06003B4D RID: 15181 RVA: 0x000BFBB4 File Offset: 0x000BDDB4
		[WebCategory("Action")]
		[WebSysDescription("PasswordRecovery_UserLookupError")]
		public event EventHandler UserLookupError
		{
			add
			{
				base.Events.AddHandler(PasswordRecovery.EventUserLookupError, value);
			}
			remove
			{
				base.Events.RemoveHandler(PasswordRecovery.EventUserLookupError, value);
			}
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000BFBC7 File Offset: 0x000BDDC7
		private void AnswerTextChanged(object source, EventArgs e)
		{
			this._answer = ((ITextControl)source).Text;
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x000BFBDA File Offset: 0x000BDDDA
		private void AttemptSendPassword()
		{
			if (this.Page != null && !this.Page.IsValid)
			{
				return;
			}
			if (this.CurrentView == PasswordRecovery.View.UserName)
			{
				this.AttemptSendPasswordUserNameView();
				return;
			}
			if (this.CurrentView == PasswordRecovery.View.Question)
			{
				this.AttemptSendPasswordQuestionView();
			}
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x000BFC10 File Offset: 0x000BDE10
		private void AttemptSendPasswordQuestionView()
		{
			MembershipProvider provider = LoginUtil.GetProvider(this.MembershipProvider);
			MembershipUser user = provider.GetUser(this.UserNameInternal, false, false);
			if (user == null)
			{
				this.SetFailureTextLabel(this._questionContainer, this.GeneralFailureText);
				return;
			}
			if (user.IsLockedOut)
			{
				this.SetFailureTextLabel(this._questionContainer, this.GeneralFailureText);
				return;
			}
			this.Question = user.PasswordQuestion;
			if (string.IsNullOrEmpty(this.Question))
			{
				this.SetFailureTextLabel(this._questionContainer, this.GeneralFailureText);
				return;
			}
			LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
			this.OnVerifyingAnswer(loginCancelEventArgs);
			if (loginCancelEventArgs.Cancel)
			{
				return;
			}
			string answerInternal = this.AnswerInternal;
			string email = user.Email;
			if (string.IsNullOrEmpty(email))
			{
				this.SetFailureTextLabel(this._questionContainer, this.GeneralFailureText);
				return;
			}
			string text;
			if (provider.EnablePasswordRetrieval)
			{
				text = user.GetPassword(answerInternal, false);
			}
			else
			{
				if (!provider.EnablePasswordReset)
				{
					throw new HttpException(SR.GetString("PasswordRecovery_RecoveryNotSupported"));
				}
				text = user.ResetPassword(answerInternal, false);
			}
			if (text != null)
			{
				LoginUtil.SendPasswordMail(email, user.UserName, text, this.MailDefinition, SR.GetString("PasswordRecovery_DefaultSubject"), SR.GetString("PasswordRecovery_DefaultBody"), new LoginUtil.OnSendingMailDelegate(this.OnSendingMail), new LoginUtil.OnSendMailErrorDelegate(this.OnSendMailError), this);
				this.PerformSuccessAction();
				return;
			}
			this.OnAnswerLookupError(EventArgs.Empty);
			this.SetFailureTextLabel(this._questionContainer, this.QuestionFailureText);
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000BFD80 File Offset: 0x000BDF80
		private void AttemptSendPasswordUserNameView()
		{
			LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
			this.OnVerifyingUser(loginCancelEventArgs);
			if (loginCancelEventArgs.Cancel)
			{
				return;
			}
			MembershipProvider provider = LoginUtil.GetProvider(this.MembershipProvider);
			MembershipUser user = provider.GetUser(this.UserNameInternal, false, false);
			if (user == null)
			{
				this.OnUserLookupError(EventArgs.Empty);
				this.SetFailureTextLabel(this._userNameContainer, this.UserNameFailureText);
				return;
			}
			if (user.IsLockedOut)
			{
				this.SetFailureTextLabel(this._userNameContainer, this.UserNameFailureText);
				return;
			}
			if (provider.RequiresQuestionAndAnswer)
			{
				this.Question = user.PasswordQuestion;
				if (string.IsNullOrEmpty(this.Question))
				{
					this.SetFailureTextLabel(this._userNameContainer, this.GeneralFailureText);
					return;
				}
				this.CurrentView = PasswordRecovery.View.Question;
				return;
			}
			else
			{
				string email = user.Email;
				if (string.IsNullOrEmpty(email))
				{
					this.SetFailureTextLabel(this._userNameContainer, this.GeneralFailureText);
					return;
				}
				string text;
				if (provider.EnablePasswordRetrieval)
				{
					text = user.GetPassword(false);
				}
				else
				{
					if (!provider.EnablePasswordReset)
					{
						throw new HttpException(SR.GetString("PasswordRecovery_RecoveryNotSupported"));
					}
					text = user.ResetPassword(false);
				}
				if (text != null)
				{
					LoginUtil.SendPasswordMail(email, user.UserName, text, this.MailDefinition, SR.GetString("PasswordRecovery_DefaultSubject"), SR.GetString("PasswordRecovery_DefaultBody"), new LoginUtil.OnSendingMailDelegate(this.OnSendingMail), new LoginUtil.OnSendMailErrorDelegate(this.OnSendMailError), this);
					this.PerformSuccessAction();
					return;
				}
				this.SetFailureTextLabel(this._userNameContainer, this.GeneralFailureText);
				return;
			}
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x000BFEF2 File Offset: 0x000BE0F2
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			this.CreateUserView();
			this.CreateQuestionView();
			this.CreateSuccessView();
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x000BFF14 File Offset: 0x000BE114
		private void CreateQuestionView()
		{
			this._questionContainer = new PasswordRecovery.QuestionContainer(this);
			this._questionContainer.ID = "QuestionContainerID";
			this._questionContainer.RenderDesignerRegion = this._renderDesignerRegion;
			ITemplate template;
			if (this.QuestionTemplate != null)
			{
				template = this.QuestionTemplate;
			}
			else
			{
				template = new PasswordRecovery.DefaultQuestionTemplate(this);
				this._questionContainer.EnableViewState = false;
				this._questionContainer.EnableTheming = false;
			}
			template.InstantiateIn(this._questionContainer);
			this.Controls.Add(this._questionContainer);
			IEditableTextControl editableTextControl = this._questionContainer.AnswerTextBox as IEditableTextControl;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += this.AnswerTextChanged;
			}
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x000BFFC4 File Offset: 0x000BE1C4
		private void CreateSuccessView()
		{
			this._successContainer = new PasswordRecovery.SuccessContainer(this);
			this._successContainer.ID = "SuccessContainerID";
			this._successContainer.RenderDesignerRegion = this._renderDesignerRegion;
			ITemplate template;
			if (this.SuccessTemplate != null)
			{
				template = this.SuccessTemplate;
			}
			else
			{
				template = new PasswordRecovery.DefaultSuccessTemplate(this);
				this._successContainer.EnableViewState = false;
				this._successContainer.EnableTheming = false;
			}
			template.InstantiateIn(this._successContainer);
			this.Controls.Add(this._successContainer);
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x000C0050 File Offset: 0x000BE250
		private void CreateUserView()
		{
			this._userNameContainer = new PasswordRecovery.UserNameContainer(this);
			this._userNameContainer.ID = "UserNameContainerID";
			this._userNameContainer.RenderDesignerRegion = this._renderDesignerRegion;
			ITemplate template;
			if (this.UserNameTemplate != null)
			{
				template = this.UserNameTemplate;
			}
			else
			{
				template = new PasswordRecovery.DefaultUserNameTemplate(this);
				this._userNameContainer.EnableViewState = false;
				this._userNameContainer.EnableTheming = false;
			}
			template.InstantiateIn(this._userNameContainer);
			this.Controls.Add(this._userNameContainer);
			this.SetUserNameEditableChildProperties();
			IEditableTextControl editableTextControl = this._userNameContainer.UserNameTextBox as IEditableTextControl;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += this.UserNameTextChanged;
			}
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x000C0108 File Offset: 0x000BE308
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
					this.CurrentView = (PasswordRecovery.View)((int)triplet.Second);
				}
				if (triplet.Third != null)
				{
					this._userName = (string)triplet.Third;
				}
			}
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x000C0168 File Offset: 0x000BE368
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 11)
			{
				throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
			}
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.SubmitButtonStyle).LoadViewState(array[1]);
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
				((IStateManager)this.FailureTextStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.SuccessTextStyle).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.MailDefinition).LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)this.ValidatorTextStyle).LoadViewState(array[10]);
			}
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x000C026C File Offset: 0x000BE46C
		protected virtual void OnAnswerLookupError(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PasswordRecovery.EventAnswerLookupError];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x000C029C File Offset: 0x000BE49C
		protected virtual void OnVerifyingAnswer(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[PasswordRecovery.EventVerifyingAnswer];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x000C02CC File Offset: 0x000BE4CC
		protected virtual void OnSendingMail(MailMessageEventArgs e)
		{
			MailMessageEventHandler mailMessageEventHandler = (MailMessageEventHandler)base.Events[PasswordRecovery.EventSendingMail];
			if (mailMessageEventHandler != null)
			{
				mailMessageEventHandler(this, e);
			}
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x000C02FC File Offset: 0x000BE4FC
		protected virtual void OnSendMailError(SendMailErrorEventArgs e)
		{
			SendMailErrorEventHandler sendMailErrorEventHandler = (SendMailErrorEventHandler)base.Events[PasswordRecovery.EventSendMailError];
			if (sendMailErrorEventHandler != null)
			{
				sendMailErrorEventHandler(this, e);
			}
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x000C032C File Offset: 0x000BE52C
		protected virtual void OnVerifyingUser(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[PasswordRecovery.EventVerifyingUser];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000C035C File Offset: 0x000BE55C
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			bool result = false;
			if (e is CommandEventArgs)
			{
				CommandEventArgs commandEventArgs = (CommandEventArgs)e;
				if (commandEventArgs.CommandName.Equals(PasswordRecovery.SubmitButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
				{
					this.AttemptSendPassword();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x000C0396 File Offset: 0x000BE596
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.RegisterRequiresControlState(this);
			this.Page.LoadComplete += this.OnPageLoadComplete;
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x000C03C4 File Offset: 0x000BE5C4
		private void OnPageLoadComplete(object sender, EventArgs e)
		{
			if (this.CurrentView == PasswordRecovery.View.Question && string.IsNullOrEmpty(this.Question))
			{
				MembershipProvider provider = LoginUtil.GetProvider(this.MembershipProvider);
				MembershipUser user = provider.GetUser(this.UserNameInternal, false, false);
				if (user != null)
				{
					this.Question = user.PasswordQuestion;
					if (string.IsNullOrEmpty(this.Question))
					{
						this.SetFailureTextLabel(this._questionContainer, this.GeneralFailureText);
						return;
					}
				}
				else
				{
					this.SetFailureTextLabel(this._questionContainer, this.GeneralFailureText);
				}
			}
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x000C0444 File Offset: 0x000BE644
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this._userNameContainer.Visible = false;
			this._questionContainer.Visible = false;
			this._successContainer.Visible = false;
			switch (this.CurrentView)
			{
			case PasswordRecovery.View.UserName:
				this._userNameContainer.Visible = true;
				this.SetUserNameEditableChildProperties();
				return;
			case PasswordRecovery.View.Question:
				this._questionContainer.Visible = true;
				return;
			case PasswordRecovery.View.Success:
				this._successContainer.Visible = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x000C04C4 File Offset: 0x000BE6C4
		protected virtual void OnUserLookupError(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PasswordRecovery.EventUserLookupError];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x000C04F4 File Offset: 0x000BE6F4
		private void PerformSuccessAction()
		{
			string successPageUrl = this.SuccessPageUrl;
			if (!string.IsNullOrEmpty(successPageUrl))
			{
				this.Page.Response.Redirect(base.ResolveClientUrl(successPageUrl), false);
				return;
			}
			this.CurrentView = PasswordRecovery.View.Success;
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000C0530 File Offset: 0x000BE730
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			this.EnsureChildControls();
			if (base.DesignMode)
			{
				this._userNameContainer.Visible = false;
				this._questionContainer.Visible = false;
				this._successContainer.Visible = false;
				switch (this.CurrentView)
				{
				case PasswordRecovery.View.UserName:
					this._userNameContainer.Visible = true;
					break;
				case PasswordRecovery.View.Question:
					this._questionContainer.Visible = true;
					break;
				case PasswordRecovery.View.Success:
					this._successContainer.Visible = true;
					break;
				}
			}
			switch (this.CurrentView)
			{
			case PasswordRecovery.View.UserName:
				this.SetUserNameChildProperties();
				break;
			case PasswordRecovery.View.Question:
				this.SetQuestionChildProperties();
				break;
			case PasswordRecovery.View.Success:
				this.SetSuccessChildProperties();
				break;
			}
			this.RenderContents(writer);
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x000C0600 File Offset: 0x000BE800
		protected internal override object SaveControlState()
		{
			object obj = base.SaveControlState();
			if (obj != null || this._currentView != PasswordRecovery.View.UserName || this._userName != null)
			{
				object y = null;
				object z = null;
				if (this._currentView != PasswordRecovery.View.UserName)
				{
					y = (int)this._currentView;
				}
				if (this._userName != null && this._currentView != PasswordRecovery.View.Success)
				{
					z = this._userName;
				}
				return new Triplet(obj, y, z);
			}
			return null;
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x000C0664 File Offset: 0x000BE864
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this._submitButtonStyle != null) ? ((IStateManager)this._submitButtonStyle).SaveViewState() : null,
				(this._labelStyle != null) ? ((IStateManager)this._labelStyle).SaveViewState() : null,
				(this._textBoxStyle != null) ? ((IStateManager)this._textBoxStyle).SaveViewState() : null,
				(this._hyperLinkStyle != null) ? ((IStateManager)this._hyperLinkStyle).SaveViewState() : null,
				(this._instructionTextStyle != null) ? ((IStateManager)this._instructionTextStyle).SaveViewState() : null,
				(this._titleTextStyle != null) ? ((IStateManager)this._titleTextStyle).SaveViewState() : null,
				(this._failureTextStyle != null) ? ((IStateManager)this._failureTextStyle).SaveViewState() : null,
				(this._successTextStyle != null) ? ((IStateManager)this._successTextStyle).SaveViewState() : null,
				(this._mailDefinition != null) ? ((IStateManager)this._mailDefinition).SaveViewState() : null,
				(this._validatorTextStyle != null) ? ((IStateManager)this._validatorTextStyle).SaveViewState() : null
			};
			for (int i = 0; i < 11; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x000C0794 File Offset: 0x000BE994
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			if (data != null)
			{
				object obj = data["CurrentView"];
				if (obj != null)
				{
					this.CurrentView = (PasswordRecovery.View)obj;
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

		// Token: 0x06003B67 RID: 15207 RVA: 0x000C07F8 File Offset: 0x000BE9F8
		private void SetFailureTextLabel(PasswordRecovery.QuestionContainer container, string failureText)
		{
			ITextControl textControl = (ITextControl)container.FailureTextLabel;
			if (textControl != null)
			{
				textControl.Text = failureText;
			}
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x000C081C File Offset: 0x000BEA1C
		private void SetFailureTextLabel(PasswordRecovery.UserNameContainer container, string failureText)
		{
			ITextControl textControl = (ITextControl)container.FailureTextLabel;
			if (textControl != null)
			{
				textControl.Text = failureText;
			}
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x000C083F File Offset: 0x000BEA3F
		internal void SetQuestionChildProperties()
		{
			this.SetQuestionCommonChildProperties();
			if (this.QuestionTemplate == null)
			{
				this.SetQuestionDefaultChildProperties();
			}
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x000C0858 File Offset: 0x000BEA58
		private void SetQuestionCommonChildProperties()
		{
			PasswordRecovery.QuestionContainer questionContainer = this._questionContainer;
			Util.CopyBaseAttributesToInnerControl(this, questionContainer);
			questionContainer.ApplyStyle(base.ControlStyle);
			ITextControl textControl = (ITextControl)questionContainer.UserName;
			if (textControl != null)
			{
				textControl.Text = HttpUtility.HtmlEncode(this.UserNameInternal);
			}
			ITextControl textControl2 = (ITextControl)questionContainer.Question;
			if (textControl2 != null)
			{
				textControl2.Text = HttpUtility.HtmlEncode(this.Question);
			}
			ITextControl textControl3 = (ITextControl)questionContainer.AnswerTextBox;
			if (textControl3 != null)
			{
				textControl3.Text = string.Empty;
			}
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x000C08DC File Offset: 0x000BEADC
		private void SetQuestionDefaultChildProperties()
		{
			PasswordRecovery.QuestionContainer questionContainer = this._questionContainer;
			questionContainer.BorderTable.CellPadding = this.BorderPadding;
			questionContainer.BorderTable.CellSpacing = 0;
			Literal title = questionContainer.Title;
			string questionTitleText = this.QuestionTitleText;
			if (questionTitleText.Length > 0)
			{
				title.Text = questionTitleText;
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
			Literal instruction = questionContainer.Instruction;
			string questionInstructionText = this.QuestionInstructionText;
			if (questionInstructionText.Length > 0)
			{
				instruction.Text = questionInstructionText;
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
			Literal userNameLabel = questionContainer.UserNameLabel;
			string userNameLabelText = this.UserNameLabelText;
			if (userNameLabelText.Length > 0)
			{
				userNameLabel.Text = userNameLabelText;
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
			Control userName = questionContainer.UserName;
			if (this.UserNameInternal.Length > 0)
			{
				userName.Visible = true;
			}
			else
			{
				userName.Visible = false;
			}
			if (userName is WebControl)
			{
				((WebControl)userName).TabIndex = this.TabIndex;
			}
			Literal questionLabel = questionContainer.QuestionLabel;
			string questionLabelText = this.QuestionLabelText;
			if (questionLabelText.Length > 0)
			{
				questionLabel.Text = questionLabelText;
				if (this._labelStyle != null)
				{
					LoginUtil.SetTableCellStyle(questionLabel, this.LabelStyle);
				}
				questionLabel.Visible = true;
			}
			else
			{
				questionLabel.Visible = false;
			}
			Control question = questionContainer.Question;
			if (this.Question.Length > 0)
			{
				question.Visible = true;
			}
			else
			{
				question.Visible = false;
			}
			Literal answerLabel = questionContainer.AnswerLabel;
			string answerLabelText = this.AnswerLabelText;
			if (answerLabelText.Length > 0)
			{
				answerLabel.Text = answerLabelText;
				if (this._labelStyle != null)
				{
					LoginUtil.SetTableCellStyle(answerLabel, this.LabelStyle);
				}
				answerLabel.Visible = true;
			}
			else
			{
				answerLabel.Visible = false;
			}
			WebControl webControl = (WebControl)questionContainer.AnswerTextBox;
			if (this._textBoxStyle != null)
			{
				webControl.ApplyStyle(this.TextBoxStyle);
			}
			webControl.TabIndex = this.TabIndex;
			webControl.AccessKey = this.AccessKey;
			bool flag = this.CurrentView == PasswordRecovery.View.Question;
			RequiredFieldValidator answerRequired = questionContainer.AnswerRequired;
			answerRequired.ErrorMessage = this.AnswerRequiredErrorMessage;
			answerRequired.ToolTip = this.AnswerRequiredErrorMessage;
			answerRequired.Enabled = flag;
			answerRequired.Visible = flag;
			if (this._validatorTextStyle != null)
			{
				answerRequired.ApplyStyle(this._validatorTextStyle);
			}
			LinkButton linkButton = questionContainer.LinkButton;
			ImageButton imageButton = questionContainer.ImageButton;
			Button pushButton = questionContainer.PushButton;
			WebControl webControl2 = null;
			switch (this.SubmitButtonType)
			{
			case ButtonType.Button:
				pushButton.Text = this.SubmitButtonText;
				webControl2 = pushButton;
				break;
			case ButtonType.Image:
				imageButton.ImageUrl = this.SubmitButtonImageUrl;
				imageButton.AlternateText = this.SubmitButtonText;
				webControl2 = imageButton;
				break;
			case ButtonType.Link:
				linkButton.Text = this.SubmitButtonText;
				webControl2 = linkButton;
				break;
			}
			linkButton.Visible = false;
			imageButton.Visible = false;
			pushButton.Visible = false;
			webControl2.Visible = true;
			webControl2.TabIndex = this.TabIndex;
			if (this._submitButtonStyle != null)
			{
				webControl2.ApplyStyle(this.SubmitButtonStyle);
			}
			HyperLink helpPageLink = questionContainer.HelpPageLink;
			string helpPageText = this.HelpPageText;
			Image helpPageIcon = questionContainer.HelpPageIcon;
			if (helpPageText.Length > 0)
			{
				helpPageLink.Text = helpPageText;
				helpPageLink.NavigateUrl = this.HelpPageUrl;
				helpPageLink.TabIndex = this.TabIndex;
				helpPageLink.Visible = true;
			}
			else
			{
				helpPageLink.Visible = false;
			}
			string helpPageIconUrl = this.HelpPageIconUrl;
			bool flag2 = helpPageIconUrl.Length > 0;
			helpPageIcon.Visible = flag2;
			if (flag2)
			{
				helpPageIcon.ImageUrl = helpPageIconUrl;
				helpPageIcon.AlternateText = helpPageText;
			}
			if (helpPageLink.Visible || helpPageIcon.Visible)
			{
				if (this._hyperLinkStyle != null)
				{
					TableItemStyle tableItemStyle = new TableItemStyle();
					tableItemStyle.CopyFrom(this.HyperLinkStyle);
					tableItemStyle.Font.Reset();
					LoginUtil.SetTableCellStyle(helpPageLink, tableItemStyle);
					helpPageLink.Font.CopyFrom(this.HyperLinkStyle.Font);
					helpPageLink.ForeColor = this.HyperLinkStyle.ForeColor;
				}
				LoginUtil.SetTableCellVisible(helpPageLink, true);
			}
			else
			{
				LoginUtil.SetTableCellVisible(helpPageLink, false);
			}
			Control failureTextLabel = questionContainer.FailureTextLabel;
			if (((ITextControl)failureTextLabel).Text.Length > 0)
			{
				LoginUtil.SetTableCellStyle(failureTextLabel, this.FailureTextStyle);
				LoginUtil.SetTableCellVisible(failureTextLabel, true);
				return;
			}
			LoginUtil.SetTableCellVisible(failureTextLabel, false);
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000C0D70 File Offset: 0x000BEF70
		internal void SetSuccessChildProperties()
		{
			PasswordRecovery.SuccessContainer successContainer = this._successContainer;
			Util.CopyBaseAttributesToInnerControl(this, successContainer);
			successContainer.ApplyStyle(base.ControlStyle);
			if (this.SuccessTemplate == null)
			{
				successContainer.BorderTable.CellPadding = this.BorderPadding;
				successContainer.BorderTable.CellSpacing = 0;
				Literal successTextLabel = successContainer.SuccessTextLabel;
				string successText = this.SuccessText;
				if (successText.Length > 0)
				{
					successTextLabel.Text = successText;
					if (this._successTextStyle != null)
					{
						LoginUtil.SetTableCellStyle(successTextLabel, this._successTextStyle);
					}
					LoginUtil.SetTableCellVisible(successTextLabel, true);
					return;
				}
				LoginUtil.SetTableCellVisible(successTextLabel, false);
			}
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000C0DFD File Offset: 0x000BEFFD
		internal void SetUserNameChildProperties()
		{
			this.SetUserNameCommonChildProperties();
			if (this.UserNameTemplate == null)
			{
				this.SetUserNameDefaultChildProperties();
			}
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x000C0E13 File Offset: 0x000BF013
		private void SetUserNameCommonChildProperties()
		{
			Util.CopyBaseAttributesToInnerControl(this, this._userNameContainer);
			this._userNameContainer.ApplyStyle(base.ControlStyle);
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000C0E34 File Offset: 0x000BF034
		private void SetUserNameDefaultChildProperties()
		{
			PasswordRecovery.UserNameContainer userNameContainer = this._userNameContainer;
			if (this.UserNameTemplate == null)
			{
				this._userNameContainer.BorderTable.CellPadding = this.BorderPadding;
				this._userNameContainer.BorderTable.CellSpacing = 0;
				Literal title = userNameContainer.Title;
				string userNameTitleText = this.UserNameTitleText;
				if (userNameTitleText.Length > 0)
				{
					title.Text = userNameTitleText;
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
				Literal instruction = userNameContainer.Instruction;
				string userNameInstructionText = this.UserNameInstructionText;
				if (userNameInstructionText.Length > 0)
				{
					instruction.Text = userNameInstructionText;
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
				Literal userNameLabel = userNameContainer.UserNameLabel;
				string userNameLabelText = this.UserNameLabelText;
				if (userNameLabelText.Length > 0)
				{
					userNameLabel.Text = userNameLabelText;
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
				WebControl webControl = (WebControl)userNameContainer.UserNameTextBox;
				if (this._textBoxStyle != null)
				{
					webControl.ApplyStyle(this.TextBoxStyle);
				}
				webControl.TabIndex = this.TabIndex;
				webControl.AccessKey = this.AccessKey;
				bool flag = this.CurrentView == PasswordRecovery.View.UserName;
				RequiredFieldValidator userNameRequired = userNameContainer.UserNameRequired;
				userNameRequired.ErrorMessage = this.UserNameRequiredErrorMessage;
				userNameRequired.ToolTip = this.UserNameRequiredErrorMessage;
				userNameRequired.Enabled = flag;
				userNameRequired.Visible = flag;
				if (this._validatorTextStyle != null)
				{
					userNameRequired.ApplyStyle(this._validatorTextStyle);
				}
				LinkButton linkButton = userNameContainer.LinkButton;
				ImageButton imageButton = userNameContainer.ImageButton;
				Button pushButton = userNameContainer.PushButton;
				WebControl webControl2 = null;
				switch (this.SubmitButtonType)
				{
				case ButtonType.Button:
					pushButton.Text = this.SubmitButtonText;
					webControl2 = pushButton;
					break;
				case ButtonType.Image:
					imageButton.ImageUrl = this.SubmitButtonImageUrl;
					imageButton.AlternateText = this.SubmitButtonText;
					webControl2 = imageButton;
					break;
				case ButtonType.Link:
					linkButton.Text = this.SubmitButtonText;
					webControl2 = linkButton;
					break;
				}
				linkButton.Visible = false;
				imageButton.Visible = false;
				pushButton.Visible = false;
				webControl2.Visible = true;
				webControl2.TabIndex = this.TabIndex;
				if (this._submitButtonStyle != null)
				{
					webControl2.ApplyStyle(this.SubmitButtonStyle);
				}
				HyperLink helpPageLink = userNameContainer.HelpPageLink;
				string helpPageText = this.HelpPageText;
				Image helpPageIcon = userNameContainer.HelpPageIcon;
				if (helpPageText.Length > 0)
				{
					helpPageLink.Text = helpPageText;
					helpPageLink.NavigateUrl = this.HelpPageUrl;
					helpPageLink.Visible = true;
					helpPageLink.TabIndex = this.TabIndex;
				}
				else
				{
					helpPageLink.Visible = false;
				}
				string helpPageIconUrl = this.HelpPageIconUrl;
				bool flag2 = helpPageIconUrl.Length > 0;
				helpPageIcon.Visible = flag2;
				if (flag2)
				{
					helpPageIcon.ImageUrl = helpPageIconUrl;
					helpPageIcon.AlternateText = helpPageText;
				}
				if (helpPageLink.Visible || helpPageIcon.Visible)
				{
					if (this._hyperLinkStyle != null)
					{
						Style style = new TableItemStyle();
						style.CopyFrom(this.HyperLinkStyle);
						style.Font.Reset();
						LoginUtil.SetTableCellStyle(helpPageLink, style);
						helpPageLink.Font.CopyFrom(this.HyperLinkStyle.Font);
						helpPageLink.ForeColor = this.HyperLinkStyle.ForeColor;
					}
					LoginUtil.SetTableCellVisible(helpPageLink, true);
				}
				else
				{
					LoginUtil.SetTableCellVisible(helpPageLink, false);
				}
				Control failureTextLabel = userNameContainer.FailureTextLabel;
				if (((ITextControl)failureTextLabel).Text.Length > 0)
				{
					LoginUtil.SetTableCellStyle(failureTextLabel, this.FailureTextStyle);
					LoginUtil.SetTableCellVisible(failureTextLabel, true);
					return;
				}
				LoginUtil.SetTableCellVisible(failureTextLabel, false);
			}
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x000C11DC File Offset: 0x000BF3DC
		private void SetUserNameEditableChildProperties()
		{
			string userNameInternal = this.UserNameInternal;
			if (userNameInternal.Length > 0)
			{
				ITextControl textControl = (ITextControl)this._userNameContainer.UserNameTextBox;
				if (textControl != null)
				{
					textControl.Text = userNameInternal;
				}
			}
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x000C1214 File Offset: 0x000BF414
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._submitButtonStyle != null)
			{
				((IStateManager)this._submitButtonStyle).TrackViewState();
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
			if (this._failureTextStyle != null)
			{
				((IStateManager)this._failureTextStyle).TrackViewState();
			}
			if (this._successTextStyle != null)
			{
				((IStateManager)this._successTextStyle).TrackViewState();
			}
			if (this._mailDefinition != null)
			{
				((IStateManager)this._mailDefinition).TrackViewState();
			}
			if (this._validatorTextStyle != null)
			{
				((IStateManager)this._validatorTextStyle).TrackViewState();
			}
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x000C12E8 File Offset: 0x000BF4E8
		private void UpdateValidators()
		{
			if (this.UserNameTemplate == null && this._userNameContainer != null)
			{
				bool flag = this.CurrentView == PasswordRecovery.View.UserName;
				this._userNameContainer.UserNameRequired.Enabled = flag;
				this._userNameContainer.UserNameRequired.Visible = flag;
			}
			if (this.QuestionTemplate == null && this._questionContainer != null)
			{
				bool flag2 = this.CurrentView == PasswordRecovery.View.Question;
				this._questionContainer.AnswerRequired.Enabled = flag2;
				this._questionContainer.AnswerRequired.Visible = flag2;
			}
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x000C136D File Offset: 0x000BF56D
		private void UserNameTextChanged(object source, EventArgs e)
		{
			this.UserName = ((ITextControl)source).Text;
		}

		// Token: 0x04002313 RID: 8979
		public static readonly string SubmitButtonCommandName = "Submit";

		// Token: 0x04002314 RID: 8980
		private const string _userNameID = "UserName";

		// Token: 0x04002315 RID: 8981
		private const string _questionID = "Question";

		// Token: 0x04002316 RID: 8982
		private const string _answerID = "Answer";

		// Token: 0x04002317 RID: 8983
		private const string _failureTextID = "FailureText";

		// Token: 0x04002318 RID: 8984
		private const string _userNameRequiredID = "UserNameRequired";

		// Token: 0x04002319 RID: 8985
		private const string _answerRequiredID = "AnswerRequired";

		// Token: 0x0400231A RID: 8986
		private const string _pushButtonID = "SubmitButton";

		// Token: 0x0400231B RID: 8987
		private const string _imageButtonID = "SubmitImageButton";

		// Token: 0x0400231C RID: 8988
		private const string _linkButtonID = "SubmitLinkButton";

		// Token: 0x0400231D RID: 8989
		private const string _helpLinkID = "HelpLink";

		// Token: 0x0400231E RID: 8990
		private const string _userNameContainerID = "UserNameContainerID";

		// Token: 0x0400231F RID: 8991
		private const string _questionContainerID = "QuestionContainerID";

		// Token: 0x04002320 RID: 8992
		private const string _successContainerID = "SuccessContainerID";

		// Token: 0x04002321 RID: 8993
		private const ValidatorDisplay _requiredFieldValidatorDisplay = ValidatorDisplay.Static;

		// Token: 0x04002322 RID: 8994
		private const string _userNameReplacementKey = "<%\\s*UserName\\s*%>";

		// Token: 0x04002323 RID: 8995
		private const string _passwordReplacementKey = "<%\\s*Password\\s*%>";

		// Token: 0x04002324 RID: 8996
		private string _answer;

		// Token: 0x04002325 RID: 8997
		private PasswordRecovery.View _currentView;

		// Token: 0x04002326 RID: 8998
		private string _question;

		// Token: 0x04002327 RID: 8999
		private string _userName;

		// Token: 0x04002328 RID: 9000
		private bool _convertingToTemplate;

		// Token: 0x04002329 RID: 9001
		private bool _renderDesignerRegion;

		// Token: 0x0400232A RID: 9002
		private ITemplate _userNameTemplate;

		// Token: 0x0400232B RID: 9003
		private PasswordRecovery.UserNameContainer _userNameContainer;

		// Token: 0x0400232C RID: 9004
		private ITemplate _questionTemplate;

		// Token: 0x0400232D RID: 9005
		private PasswordRecovery.QuestionContainer _questionContainer;

		// Token: 0x0400232E RID: 9006
		private ITemplate _successTemplate;

		// Token: 0x0400232F RID: 9007
		private PasswordRecovery.SuccessContainer _successContainer;

		// Token: 0x04002330 RID: 9008
		private const int _viewStateArrayLength = 11;

		// Token: 0x04002331 RID: 9009
		private Style _submitButtonStyle;

		// Token: 0x04002332 RID: 9010
		private TableItemStyle _labelStyle;

		// Token: 0x04002333 RID: 9011
		private Style _textBoxStyle;

		// Token: 0x04002334 RID: 9012
		private TableItemStyle _hyperLinkStyle;

		// Token: 0x04002335 RID: 9013
		private TableItemStyle _instructionTextStyle;

		// Token: 0x04002336 RID: 9014
		private TableItemStyle _titleTextStyle;

		// Token: 0x04002337 RID: 9015
		private TableItemStyle _failureTextStyle;

		// Token: 0x04002338 RID: 9016
		private TableItemStyle _successTextStyle;

		// Token: 0x04002339 RID: 9017
		private MailDefinition _mailDefinition;

		// Token: 0x0400233A RID: 9018
		private Style _validatorTextStyle;

		// Token: 0x0400233B RID: 9019
		private static readonly object EventVerifyingUser = new object();

		// Token: 0x0400233C RID: 9020
		private static readonly object EventUserLookupError = new object();

		// Token: 0x0400233D RID: 9021
		private static readonly object EventVerifyingAnswer = new object();

		// Token: 0x0400233E RID: 9022
		private static readonly object EventAnswerLookupError = new object();

		// Token: 0x0400233F RID: 9023
		private static readonly object EventSendMailError = new object();

		// Token: 0x04002340 RID: 9024
		private static readonly object EventSendingMail = new object();

		// Token: 0x020009BF RID: 2495
		private sealed class DefaultQuestionTemplate : ITemplate
		{
			// Token: 0x06006C12 RID: 27666 RVA: 0x001827B6 File Offset: 0x001809B6
			public DefaultQuestionTemplate(PasswordRecovery owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006C13 RID: 27667 RVA: 0x001827C8 File Offset: 0x001809C8
			private void CreateControls(PasswordRecovery.QuestionContainer questionContainer)
			{
				string uniqueID = this._owner.UniqueID;
				questionContainer.Title = new Literal();
				questionContainer.Instruction = new Literal();
				questionContainer.UserNameLabel = new Literal();
				questionContainer.UserName = new Literal();
				questionContainer.QuestionLabel = new Literal();
				questionContainer.Question = new Literal();
				questionContainer.UserName.ID = "UserName";
				questionContainer.Question.ID = "Question";
				TextBox textBox = new TextBox();
				textBox.ID = "Answer";
				questionContainer.AnswerTextBox = textBox;
				questionContainer.AnswerLabel = new LabelLiteral(textBox);
				bool flag = this._owner.CurrentView == PasswordRecovery.View.Question;
				questionContainer.AnswerRequired = new RequiredFieldValidator
				{
					ID = "AnswerRequired",
					ValidationGroup = uniqueID,
					ControlToValidate = textBox.ID,
					Display = ValidatorDisplay.Static,
					Text = SR.GetString("LoginControls_DefaultRequiredFieldValidatorText"),
					Enabled = flag,
					Visible = flag
				};
				questionContainer.LinkButton = new LinkButton
				{
					ID = "SubmitLinkButton",
					ValidationGroup = uniqueID,
					CommandName = PasswordRecovery.SubmitButtonCommandName
				};
				questionContainer.ImageButton = new ImageButton
				{
					ID = "SubmitImageButton",
					ValidationGroup = uniqueID,
					CommandName = PasswordRecovery.SubmitButtonCommandName
				};
				questionContainer.PushButton = new Button
				{
					ID = "SubmitButton",
					ValidationGroup = uniqueID,
					CommandName = PasswordRecovery.SubmitButtonCommandName
				};
				questionContainer.HelpPageLink = new HyperLink();
				questionContainer.HelpPageLink.ID = "HelpLink";
				questionContainer.HelpPageIcon = new Image();
				questionContainer.FailureTextLabel = new Literal
				{
					ID = "FailureText"
				};
			}

			// Token: 0x06006C14 RID: 27668 RVA: 0x00182994 File Offset: 0x00180B94
			private void LayoutControls(PasswordRecovery.QuestionContainer questionContainer)
			{
				if (this._owner.TextLayout == LoginTextLayout.TextOnLeft)
				{
					this.LayoutTextOnLeft(questionContainer);
					return;
				}
				this.LayoutTextOnTop(questionContainer);
			}

			// Token: 0x06006C15 RID: 27669 RVA: 0x001829C0 File Offset: 0x00180BC0
			private void LayoutTextOnLeft(PasswordRecovery.QuestionContainer questionContainer)
			{
				Table table = new Table();
				table.CellPadding = 0;
				TableRow tableRow = new LoginUtil.DisappearingTableRow();
				TableCell tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(questionContainer.Title);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(questionContainer.Instruction);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(questionContainer.UserNameLabel);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(questionContainer.UserName);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(questionContainer.QuestionLabel);
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(questionContainer.Question);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(questionContainer.AnswerLabel);
				if (this._owner.ConvertingToTemplate)
				{
					questionContainer.AnswerLabel.RenderAsLabel = true;
				}
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(questionContainer.AnswerTextBox);
				tableCell.Controls.Add(questionContainer.AnswerRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(questionContainer.FailureTextLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(questionContainer.LinkButton);
				tableCell.Controls.Add(questionContainer.ImageButton);
				tableCell.Controls.Add(questionContainer.PushButton);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(questionContainer.HelpPageIcon);
				tableCell.Controls.Add(questionContainer.HelpPageLink);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				Table table2 = LoginUtil.CreateChildTable(this._owner.ConvertingToTemplate);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(table);
				tableRow.Cells.Add(tableCell);
				table2.Rows.Add(tableRow);
				questionContainer.LayoutTable = table;
				questionContainer.BorderTable = table2;
				questionContainer.Controls.Add(table2);
			}

			// Token: 0x06006C16 RID: 27670 RVA: 0x00182D0C File Offset: 0x00180F0C
			private void LayoutTextOnTop(PasswordRecovery.QuestionContainer questionContainer)
			{
				Table table = new Table();
				table.CellPadding = 0;
				TableRow tableRow = new LoginUtil.DisappearingTableRow();
				TableCell tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(questionContainer.Title);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(questionContainer.Instruction);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(questionContainer.UserNameLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(questionContainer.UserName);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(questionContainer.QuestionLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(questionContainer.Question);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(questionContainer.AnswerLabel);
				if (this._owner.ConvertingToTemplate)
				{
					questionContainer.AnswerLabel.RenderAsLabel = true;
				}
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(questionContainer.AnswerTextBox);
				tableCell.Controls.Add(questionContainer.AnswerRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(questionContainer.FailureTextLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(questionContainer.LinkButton);
				tableCell.Controls.Add(questionContainer.ImageButton);
				tableCell.Controls.Add(questionContainer.PushButton);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(questionContainer.HelpPageIcon);
				tableCell.Controls.Add(questionContainer.HelpPageLink);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				Table table2 = LoginUtil.CreateChildTable(this._owner.ConvertingToTemplate);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(table);
				tableRow.Cells.Add(tableCell);
				table2.Rows.Add(tableRow);
				questionContainer.LayoutTable = table;
				questionContainer.BorderTable = table2;
				questionContainer.Controls.Add(table2);
			}

			// Token: 0x06006C17 RID: 27671 RVA: 0x0018305C File Offset: 0x0018125C
			void ITemplate.InstantiateIn(Control container)
			{
				PasswordRecovery.QuestionContainer questionContainer = (PasswordRecovery.QuestionContainer)container;
				this.CreateControls(questionContainer);
				this.LayoutControls(questionContainer);
			}

			// Token: 0x0400398F RID: 14735
			private PasswordRecovery _owner;
		}

		// Token: 0x020009C0 RID: 2496
		private sealed class DefaultSuccessTemplate : ITemplate
		{
			// Token: 0x06006C18 RID: 27672 RVA: 0x0018307E File Offset: 0x0018127E
			public DefaultSuccessTemplate(PasswordRecovery owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006C19 RID: 27673 RVA: 0x0018308D File Offset: 0x0018128D
			private void CreateControls(PasswordRecovery.SuccessContainer successContainer)
			{
				successContainer.SuccessTextLabel = new Literal();
			}

			// Token: 0x06006C1A RID: 27674 RVA: 0x0018309C File Offset: 0x0018129C
			private void LayoutControls(PasswordRecovery.SuccessContainer successContainer)
			{
				Table table = new Table();
				table.CellPadding = 0;
				TableRow tableRow = new LoginUtil.DisappearingTableRow();
				TableCell tableCell = new TableCell();
				tableCell.Controls.Add(successContainer.SuccessTextLabel);
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

			// Token: 0x06006C1B RID: 27675 RVA: 0x0018314C File Offset: 0x0018134C
			void ITemplate.InstantiateIn(Control container)
			{
				PasswordRecovery.SuccessContainer successContainer = (PasswordRecovery.SuccessContainer)container;
				this.CreateControls(successContainer);
				this.LayoutControls(successContainer);
			}

			// Token: 0x04003990 RID: 14736
			private PasswordRecovery _owner;
		}

		// Token: 0x020009C1 RID: 2497
		private sealed class DefaultUserNameTemplate : ITemplate
		{
			// Token: 0x06006C1C RID: 27676 RVA: 0x0018316E File Offset: 0x0018136E
			public DefaultUserNameTemplate(PasswordRecovery owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006C1D RID: 27677 RVA: 0x00183180 File Offset: 0x00181380
			private void CreateControls(PasswordRecovery.UserNameContainer userNameContainer)
			{
				string uniqueID = this._owner.UniqueID;
				userNameContainer.Title = new Literal();
				userNameContainer.Instruction = new Literal();
				TextBox textBox = new TextBox();
				textBox.ID = "UserName";
				userNameContainer.UserNameTextBox = textBox;
				userNameContainer.UserNameLabel = new LabelLiteral(textBox);
				bool flag = this._owner.CurrentView == PasswordRecovery.View.UserName;
				userNameContainer.UserNameRequired = new RequiredFieldValidator
				{
					ID = "UserNameRequired",
					ValidationGroup = uniqueID,
					ControlToValidate = textBox.ID,
					Display = ValidatorDisplay.Static,
					Text = SR.GetString("LoginControls_DefaultRequiredFieldValidatorText"),
					Enabled = flag,
					Visible = flag
				};
				userNameContainer.LinkButton = new LinkButton
				{
					ID = "SubmitLinkButton",
					ValidationGroup = uniqueID,
					CommandName = PasswordRecovery.SubmitButtonCommandName
				};
				userNameContainer.ImageButton = new ImageButton
				{
					ID = "SubmitImageButton",
					ValidationGroup = uniqueID,
					CommandName = PasswordRecovery.SubmitButtonCommandName
				};
				userNameContainer.PushButton = new Button
				{
					ID = "SubmitButton",
					ValidationGroup = uniqueID,
					CommandName = PasswordRecovery.SubmitButtonCommandName
				};
				userNameContainer.HelpPageLink = new HyperLink();
				userNameContainer.HelpPageLink.ID = "HelpLink";
				userNameContainer.HelpPageIcon = new Image();
				userNameContainer.FailureTextLabel = new Literal
				{
					ID = "FailureText"
				};
			}

			// Token: 0x06006C1E RID: 27678 RVA: 0x00183300 File Offset: 0x00181500
			private void LayoutControls(PasswordRecovery.UserNameContainer userNameContainer)
			{
				if (this._owner.TextLayout == LoginTextLayout.TextOnLeft)
				{
					this.LayoutTextOnLeft(userNameContainer);
					return;
				}
				this.LayoutTextOnTop(userNameContainer);
			}

			// Token: 0x06006C1F RID: 27679 RVA: 0x0018332C File Offset: 0x0018152C
			private void LayoutTextOnLeft(PasswordRecovery.UserNameContainer userNameContainer)
			{
				Table table = new Table();
				table.CellPadding = 0;
				TableRow tableRow = new LoginUtil.DisappearingTableRow();
				TableCell tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(userNameContainer.Title);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(userNameContainer.Instruction);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(userNameContainer.UserNameLabel);
				if (this._owner.ConvertingToTemplate)
				{
					userNameContainer.UserNameLabel.RenderAsLabel = true;
				}
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Controls.Add(userNameContainer.UserNameTextBox);
				tableCell.Controls.Add(userNameContainer.UserNameRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(userNameContainer.FailureTextLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(userNameContainer.LinkButton);
				tableCell.Controls.Add(userNameContainer.ImageButton);
				tableCell.Controls.Add(userNameContainer.PushButton);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.Controls.Add(userNameContainer.HelpPageIcon);
				tableCell.Controls.Add(userNameContainer.HelpPageLink);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				Table table2 = LoginUtil.CreateChildTable(this._owner.ConvertingToTemplate);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(table);
				tableRow.Cells.Add(tableCell);
				table2.Rows.Add(tableRow);
				userNameContainer.LayoutTable = table;
				userNameContainer.BorderTable = table2;
				userNameContainer.Controls.Add(table2);
			}

			// Token: 0x06006C20 RID: 27680 RVA: 0x001835B4 File Offset: 0x001817B4
			private void LayoutTextOnTop(PasswordRecovery.UserNameContainer userNameContainer)
			{
				Table table = new Table();
				table.CellPadding = 0;
				TableRow tableRow = new LoginUtil.DisappearingTableRow();
				TableCell tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(userNameContainer.Title);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(userNameContainer.Instruction);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(userNameContainer.UserNameLabel);
				if (this._owner.ConvertingToTemplate)
				{
					userNameContainer.UserNameLabel.RenderAsLabel = true;
				}
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(userNameContainer.UserNameTextBox);
				tableCell.Controls.Add(userNameContainer.UserNameRequired);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Controls.Add(userNameContainer.FailureTextLabel);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Controls.Add(userNameContainer.LinkButton);
				tableCell.Controls.Add(userNameContainer.ImageButton);
				tableCell.Controls.Add(userNameContainer.PushButton);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new LoginUtil.DisappearingTableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(userNameContainer.HelpPageIcon);
				tableCell.Controls.Add(userNameContainer.HelpPageLink);
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				Table table2 = LoginUtil.CreateChildTable(this._owner.ConvertingToTemplate);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Controls.Add(table);
				tableRow.Cells.Add(tableCell);
				table2.Rows.Add(tableRow);
				userNameContainer.LayoutTable = table;
				userNameContainer.BorderTable = table2;
				userNameContainer.Controls.Add(table2);
			}

			// Token: 0x06006C21 RID: 27681 RVA: 0x00183828 File Offset: 0x00181A28
			void ITemplate.InstantiateIn(Control container)
			{
				PasswordRecovery.UserNameContainer userNameContainer = (PasswordRecovery.UserNameContainer)container;
				this.CreateControls(userNameContainer);
				this.LayoutControls(userNameContainer);
			}

			// Token: 0x04003991 RID: 14737
			private PasswordRecovery _owner;
		}

		// Token: 0x020009C2 RID: 2498
		internal sealed class QuestionContainer : LoginUtil.GenericContainer<PasswordRecovery>, INonBindingContainer, INamingContainer
		{
			// Token: 0x06006C22 RID: 27682 RVA: 0x0018384A File Offset: 0x00181A4A
			public QuestionContainer(PasswordRecovery owner) : base(owner)
			{
			}

			// Token: 0x17001DC7 RID: 7623
			// (get) Token: 0x06006C23 RID: 27683 RVA: 0x00183853 File Offset: 0x00181A53
			// (set) Token: 0x06006C24 RID: 27684 RVA: 0x0018385B File Offset: 0x00181A5B
			public LabelLiteral AnswerLabel
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

			// Token: 0x17001DC8 RID: 7624
			// (get) Token: 0x06006C25 RID: 27685 RVA: 0x00183864 File Offset: 0x00181A64
			// (set) Token: 0x06006C26 RID: 27686 RVA: 0x0018386C File Offset: 0x00181A6C
			public RequiredFieldValidator AnswerRequired
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

			// Token: 0x17001DC9 RID: 7625
			// (get) Token: 0x06006C27 RID: 27687 RVA: 0x00183875 File Offset: 0x00181A75
			// (set) Token: 0x06006C28 RID: 27688 RVA: 0x00183896 File Offset: 0x00181A96
			public Control AnswerTextBox
			{
				get
				{
					if (this._answerTextBox != null)
					{
						return this._answerTextBox;
					}
					return base.FindRequiredControl<IEditableTextControl>("Answer", "PasswordRecovery_NoAnswerTextBox");
				}
				set
				{
					this._answerTextBox = value;
				}
			}

			// Token: 0x17001DCA RID: 7626
			// (get) Token: 0x06006C29 RID: 27689 RVA: 0x0018389F File Offset: 0x00181A9F
			protected override bool ConvertingToTemplate
			{
				get
				{
					return base.Owner.ConvertingToTemplate;
				}
			}

			// Token: 0x17001DCB RID: 7627
			// (get) Token: 0x06006C2A RID: 27690 RVA: 0x001838AC File Offset: 0x00181AAC
			// (set) Token: 0x06006C2B RID: 27691 RVA: 0x001838C8 File Offset: 0x00181AC8
			public Control FailureTextLabel
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

			// Token: 0x17001DCC RID: 7628
			// (get) Token: 0x06006C2C RID: 27692 RVA: 0x001838D1 File Offset: 0x00181AD1
			// (set) Token: 0x06006C2D RID: 27693 RVA: 0x001838D9 File Offset: 0x00181AD9
			public Image HelpPageIcon
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

			// Token: 0x17001DCD RID: 7629
			// (get) Token: 0x06006C2E RID: 27694 RVA: 0x001838E2 File Offset: 0x00181AE2
			// (set) Token: 0x06006C2F RID: 27695 RVA: 0x001838EA File Offset: 0x00181AEA
			public HyperLink HelpPageLink
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

			// Token: 0x17001DCE RID: 7630
			// (get) Token: 0x06006C30 RID: 27696 RVA: 0x001838F3 File Offset: 0x00181AF3
			// (set) Token: 0x06006C31 RID: 27697 RVA: 0x001838FB File Offset: 0x00181AFB
			public ImageButton ImageButton
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

			// Token: 0x17001DCF RID: 7631
			// (get) Token: 0x06006C32 RID: 27698 RVA: 0x00183904 File Offset: 0x00181B04
			// (set) Token: 0x06006C33 RID: 27699 RVA: 0x0018390C File Offset: 0x00181B0C
			public Literal Instruction
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

			// Token: 0x17001DD0 RID: 7632
			// (get) Token: 0x06006C34 RID: 27700 RVA: 0x00183915 File Offset: 0x00181B15
			// (set) Token: 0x06006C35 RID: 27701 RVA: 0x0018391D File Offset: 0x00181B1D
			public LinkButton LinkButton
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

			// Token: 0x17001DD1 RID: 7633
			// (get) Token: 0x06006C36 RID: 27702 RVA: 0x00183926 File Offset: 0x00181B26
			// (set) Token: 0x06006C37 RID: 27703 RVA: 0x0018392E File Offset: 0x00181B2E
			public Button PushButton
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

			// Token: 0x17001DD2 RID: 7634
			// (get) Token: 0x06006C38 RID: 27704 RVA: 0x00183937 File Offset: 0x00181B37
			// (set) Token: 0x06006C39 RID: 27705 RVA: 0x00183953 File Offset: 0x00181B53
			public Control Question
			{
				get
				{
					if (this._question != null)
					{
						return this._question;
					}
					return base.FindOptionalControl<ITextControl>("Question");
				}
				set
				{
					this._question = value;
				}
			}

			// Token: 0x17001DD3 RID: 7635
			// (get) Token: 0x06006C3A RID: 27706 RVA: 0x0018395C File Offset: 0x00181B5C
			// (set) Token: 0x06006C3B RID: 27707 RVA: 0x00183964 File Offset: 0x00181B64
			public Literal QuestionLabel
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

			// Token: 0x17001DD4 RID: 7636
			// (get) Token: 0x06006C3C RID: 27708 RVA: 0x0018396D File Offset: 0x00181B6D
			// (set) Token: 0x06006C3D RID: 27709 RVA: 0x00183975 File Offset: 0x00181B75
			public Literal Title
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

			// Token: 0x17001DD5 RID: 7637
			// (get) Token: 0x06006C3E RID: 27710 RVA: 0x0018397E File Offset: 0x00181B7E
			// (set) Token: 0x06006C3F RID: 27711 RVA: 0x0018399A File Offset: 0x00181B9A
			public Control UserName
			{
				get
				{
					if (this._userName != null)
					{
						return this._userName;
					}
					return base.FindOptionalControl<ITextControl>("UserName");
				}
				set
				{
					this._userName = value;
				}
			}

			// Token: 0x17001DD6 RID: 7638
			// (get) Token: 0x06006C40 RID: 27712 RVA: 0x001839A3 File Offset: 0x00181BA3
			// (set) Token: 0x06006C41 RID: 27713 RVA: 0x001839AB File Offset: 0x00181BAB
			public Literal UserNameLabel
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

			// Token: 0x04003992 RID: 14738
			private LabelLiteral _answerLabel;

			// Token: 0x04003993 RID: 14739
			private RequiredFieldValidator _answerRequired;

			// Token: 0x04003994 RID: 14740
			private Control _answerTextBox;

			// Token: 0x04003995 RID: 14741
			private Control _failureTextLabel;

			// Token: 0x04003996 RID: 14742
			private HyperLink _helpPageLink;

			// Token: 0x04003997 RID: 14743
			private Image _helpPageIcon;

			// Token: 0x04003998 RID: 14744
			private ImageButton _imageButton;

			// Token: 0x04003999 RID: 14745
			private Literal _instruction;

			// Token: 0x0400399A RID: 14746
			private LinkButton _linkButton;

			// Token: 0x0400399B RID: 14747
			private Button _pushButton;

			// Token: 0x0400399C RID: 14748
			private Control _question;

			// Token: 0x0400399D RID: 14749
			private Literal _questionLabel;

			// Token: 0x0400399E RID: 14750
			private Literal _title;

			// Token: 0x0400399F RID: 14751
			private Literal _userNameLabel;

			// Token: 0x040039A0 RID: 14752
			private Control _userName;
		}

		// Token: 0x020009C3 RID: 2499
		internal sealed class SuccessContainer : LoginUtil.GenericContainer<PasswordRecovery>, INonBindingContainer, INamingContainer
		{
			// Token: 0x06006C42 RID: 27714 RVA: 0x0018384A File Offset: 0x00181A4A
			public SuccessContainer(PasswordRecovery owner) : base(owner)
			{
			}

			// Token: 0x17001DD7 RID: 7639
			// (get) Token: 0x06006C43 RID: 27715 RVA: 0x0018389F File Offset: 0x00181A9F
			protected override bool ConvertingToTemplate
			{
				get
				{
					return base.Owner.ConvertingToTemplate;
				}
			}

			// Token: 0x17001DD8 RID: 7640
			// (get) Token: 0x06006C44 RID: 27716 RVA: 0x001839B4 File Offset: 0x00181BB4
			// (set) Token: 0x06006C45 RID: 27717 RVA: 0x001839BC File Offset: 0x00181BBC
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

			// Token: 0x040039A1 RID: 14753
			private Literal _successTextLabel;
		}

		// Token: 0x020009C4 RID: 2500
		internal sealed class UserNameContainer : LoginUtil.GenericContainer<PasswordRecovery>, INonBindingContainer, INamingContainer
		{
			// Token: 0x06006C46 RID: 27718 RVA: 0x0018384A File Offset: 0x00181A4A
			public UserNameContainer(PasswordRecovery owner) : base(owner)
			{
			}

			// Token: 0x17001DD9 RID: 7641
			// (get) Token: 0x06006C47 RID: 27719 RVA: 0x0018389F File Offset: 0x00181A9F
			protected override bool ConvertingToTemplate
			{
				get
				{
					return base.Owner.ConvertingToTemplate;
				}
			}

			// Token: 0x17001DDA RID: 7642
			// (get) Token: 0x06006C48 RID: 27720 RVA: 0x001839C5 File Offset: 0x00181BC5
			// (set) Token: 0x06006C49 RID: 27721 RVA: 0x001839E1 File Offset: 0x00181BE1
			public Control FailureTextLabel
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

			// Token: 0x17001DDB RID: 7643
			// (get) Token: 0x06006C4A RID: 27722 RVA: 0x001839EA File Offset: 0x00181BEA
			// (set) Token: 0x06006C4B RID: 27723 RVA: 0x001839F2 File Offset: 0x00181BF2
			public Image HelpPageIcon
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

			// Token: 0x17001DDC RID: 7644
			// (get) Token: 0x06006C4C RID: 27724 RVA: 0x001839FB File Offset: 0x00181BFB
			// (set) Token: 0x06006C4D RID: 27725 RVA: 0x00183A03 File Offset: 0x00181C03
			public HyperLink HelpPageLink
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

			// Token: 0x17001DDD RID: 7645
			// (get) Token: 0x06006C4E RID: 27726 RVA: 0x00183A0C File Offset: 0x00181C0C
			// (set) Token: 0x06006C4F RID: 27727 RVA: 0x00183A14 File Offset: 0x00181C14
			public ImageButton ImageButton
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

			// Token: 0x17001DDE RID: 7646
			// (get) Token: 0x06006C50 RID: 27728 RVA: 0x00183A1D File Offset: 0x00181C1D
			// (set) Token: 0x06006C51 RID: 27729 RVA: 0x00183A25 File Offset: 0x00181C25
			public Literal Instruction
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

			// Token: 0x17001DDF RID: 7647
			// (get) Token: 0x06006C52 RID: 27730 RVA: 0x00183A2E File Offset: 0x00181C2E
			// (set) Token: 0x06006C53 RID: 27731 RVA: 0x00183A36 File Offset: 0x00181C36
			public LinkButton LinkButton
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

			// Token: 0x17001DE0 RID: 7648
			// (get) Token: 0x06006C54 RID: 27732 RVA: 0x00183A3F File Offset: 0x00181C3F
			// (set) Token: 0x06006C55 RID: 27733 RVA: 0x00183A47 File Offset: 0x00181C47
			public Button PushButton
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

			// Token: 0x17001DE1 RID: 7649
			// (get) Token: 0x06006C56 RID: 27734 RVA: 0x00183A50 File Offset: 0x00181C50
			// (set) Token: 0x06006C57 RID: 27735 RVA: 0x00183A58 File Offset: 0x00181C58
			public Literal Title
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

			// Token: 0x17001DE2 RID: 7650
			// (get) Token: 0x06006C58 RID: 27736 RVA: 0x00183A61 File Offset: 0x00181C61
			// (set) Token: 0x06006C59 RID: 27737 RVA: 0x00183A69 File Offset: 0x00181C69
			public LabelLiteral UserNameLabel
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

			// Token: 0x17001DE3 RID: 7651
			// (get) Token: 0x06006C5A RID: 27738 RVA: 0x00183A72 File Offset: 0x00181C72
			// (set) Token: 0x06006C5B RID: 27739 RVA: 0x00183A7A File Offset: 0x00181C7A
			public RequiredFieldValidator UserNameRequired
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

			// Token: 0x17001DE4 RID: 7652
			// (get) Token: 0x06006C5C RID: 27740 RVA: 0x00183A83 File Offset: 0x00181C83
			// (set) Token: 0x06006C5D RID: 27741 RVA: 0x00183AA4 File Offset: 0x00181CA4
			public Control UserNameTextBox
			{
				get
				{
					if (this._userNameTextBox != null)
					{
						return this._userNameTextBox;
					}
					return base.FindRequiredControl<IEditableTextControl>("UserName", "PasswordRecovery_NoUserNameTextBox");
				}
				set
				{
					this._userNameTextBox = value;
				}
			}

			// Token: 0x040039A2 RID: 14754
			private Control _failureTextLabel;

			// Token: 0x040039A3 RID: 14755
			private Image _helpPageIcon;

			// Token: 0x040039A4 RID: 14756
			private HyperLink _helpPageLink;

			// Token: 0x040039A5 RID: 14757
			private ImageButton _imageButton;

			// Token: 0x040039A6 RID: 14758
			private Literal _instruction;

			// Token: 0x040039A7 RID: 14759
			private LinkButton _linkButton;

			// Token: 0x040039A8 RID: 14760
			private Button _pushButton;

			// Token: 0x040039A9 RID: 14761
			private Literal _title;

			// Token: 0x040039AA RID: 14762
			private LabelLiteral _userNameLabel;

			// Token: 0x040039AB RID: 14763
			private RequiredFieldValidator _userNameRequired;

			// Token: 0x040039AC RID: 14764
			private Control _userNameTextBox;
		}

		// Token: 0x020009C5 RID: 2501
		internal enum View
		{
			// Token: 0x040039AE RID: 14766
			UserName,
			// Token: 0x040039AF RID: 14767
			Question,
			// Token: 0x040039B0 RID: 14768
			Success
		}
	}
}
