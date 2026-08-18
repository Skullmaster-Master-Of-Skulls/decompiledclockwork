using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000610 RID: 1552
	[Designer("System.Web.UI.Design.WebControls.PasswordRecoveryDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("SendingMail")]
	[Bindable(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class PasswordRecovery : CompositeControl
	{
		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06004CC7 RID: 19655 RVA: 0x00137719 File Offset: 0x00136719
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

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06004CC8 RID: 19656 RVA: 0x00137730 File Offset: 0x00136730
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

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06004CC9 RID: 19657 RVA: 0x00137778 File Offset: 0x00136778
		// (set) Token: 0x06004CCA RID: 19658 RVA: 0x001377AA File Offset: 0x001367AA
		[WebCategory("Appearance")]
		[Localizable(true)]
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

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06004CCB RID: 19659 RVA: 0x001377C0 File Offset: 0x001367C0
		// (set) Token: 0x06004CCC RID: 19660 RVA: 0x001377F2 File Offset: 0x001367F2
		[WebCategory("Validation")]
		[Localizable(true)]
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

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06004CCD RID: 19661 RVA: 0x00137808 File Offset: 0x00136808
		// (set) Token: 0x06004CCE RID: 19662 RVA: 0x00137831 File Offset: 0x00136831
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

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06004CCF RID: 19663 RVA: 0x00137862 File Offset: 0x00136862
		[WebSysDescription("PasswordRecovery_SubmitButtonStyle")]
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06004CD0 RID: 19664 RVA: 0x00137890 File Offset: 0x00136890
		// (set) Token: 0x06004CD1 RID: 19665 RVA: 0x001378B9 File Offset: 0x001368B9
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

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06004CD2 RID: 19666 RVA: 0x001378E4 File Offset: 0x001368E4
		private bool ConvertingToTemplate
		{
			get
			{
				return base.DesignMode && this._convertingToTemplate;
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x06004CD3 RID: 19667 RVA: 0x001378F6 File Offset: 0x001368F6
		// (set) Token: 0x06004CD4 RID: 19668 RVA: 0x001378FE File Offset: 0x001368FE
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

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x00137929 File Offset: 0x00136929
		[DefaultValue(null)]
		[WebCategory("Styles")]
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

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06004CD6 RID: 19670 RVA: 0x00137958 File Offset: 0x00136958
		// (set) Token: 0x06004CD7 RID: 19671 RVA: 0x0013798A File Offset: 0x0013698A
		[Localizable(true)]
		[WebSysDescription("PasswordRecovery_GeneralFailureText")]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultGeneralFailureText")]
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

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06004CD8 RID: 19672 RVA: 0x001379A0 File Offset: 0x001369A0
		// (set) Token: 0x06004CD9 RID: 19673 RVA: 0x001379CD File Offset: 0x001369CD
		[WebSysDescription("ChangePassword_HelpPageText")]
		[Localizable(true)]
		[WebCategory("Links")]
		[DefaultValue("")]
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

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06004CDA RID: 19674 RVA: 0x001379E0 File Offset: 0x001369E0
		// (set) Token: 0x06004CDB RID: 19675 RVA: 0x00137A0D File Offset: 0x00136A0D
		[UrlProperty]
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("LoginControls_HelpPageIconUrl")]
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

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06004CDC RID: 19676 RVA: 0x00137A20 File Offset: 0x00136A20
		// (set) Token: 0x06004CDD RID: 19677 RVA: 0x00137A4D File Offset: 0x00136A4D
		[WebCategory("Links")]
		[DefaultValue("")]
		[WebSysDescription("LoginControls_HelpPageUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06004CDE RID: 19678 RVA: 0x00137A60 File Offset: 0x00136A60
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebCategory("Styles")]
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

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06004CDF RID: 19679 RVA: 0x00137A8E File Offset: 0x00136A8E
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
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

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06004CE0 RID: 19680 RVA: 0x00137ABC File Offset: 0x00136ABC
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

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06004CE1 RID: 19681 RVA: 0x00137AEA File Offset: 0x00136AEA
		[Themeable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Behavior")]
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

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06004CE2 RID: 19682 RVA: 0x00137B18 File Offset: 0x00136B18
		// (set) Token: 0x06004CE3 RID: 19683 RVA: 0x00137B45 File Offset: 0x00136B45
		[WebSysDescription("MembershipProvider_Name")]
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
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

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06004CE4 RID: 19684 RVA: 0x00137B58 File Offset: 0x00136B58
		// (set) Token: 0x06004CE5 RID: 19685 RVA: 0x00137B6E File Offset: 0x00136B6E
		[Themeable(false)]
		[Filterable(false)]
		[Browsable(false)]
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

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06004CE6 RID: 19686 RVA: 0x00137B78 File Offset: 0x00136B78
		// (set) Token: 0x06004CE7 RID: 19687 RVA: 0x00137BAA File Offset: 0x00136BAA
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

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06004CE8 RID: 19688 RVA: 0x00137BC0 File Offset: 0x00136BC0
		// (set) Token: 0x06004CE9 RID: 19689 RVA: 0x00137BF2 File Offset: 0x00136BF2
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

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06004CEA RID: 19690 RVA: 0x00137C08 File Offset: 0x00136C08
		// (set) Token: 0x06004CEB RID: 19691 RVA: 0x00137C3A File Offset: 0x00136C3A
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

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06004CEC RID: 19692 RVA: 0x00137C50 File Offset: 0x00136C50
		// (set) Token: 0x06004CED RID: 19693 RVA: 0x00137C82 File Offset: 0x00136C82
		[WebCategory("Appearance")]
		[Localizable(true)]
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

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06004CEE RID: 19694 RVA: 0x00137C95 File Offset: 0x00136C95
		// (set) Token: 0x06004CEF RID: 19695 RVA: 0x00137C9D File Offset: 0x00136C9D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
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

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06004CF0 RID: 19696 RVA: 0x00137CAD File Offset: 0x00136CAD
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

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06004CF1 RID: 19697 RVA: 0x00137CBC File Offset: 0x00136CBC
		// (set) Token: 0x06004CF2 RID: 19698 RVA: 0x00137CE9 File Offset: 0x00136CE9
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("ChangePassword_ChangePasswordButtonImageUrl")]
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

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06004CF3 RID: 19699 RVA: 0x00137CFC File Offset: 0x00136CFC
		// (set) Token: 0x06004CF4 RID: 19700 RVA: 0x00137D2E File Offset: 0x00136D2E
		[WebSysDefaultValue("PasswordRecovery_DefaultSubmitButtonText")]
		[WebCategory("Appearance")]
		[Localizable(true)]
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

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06004CF5 RID: 19701 RVA: 0x00137D44 File Offset: 0x00136D44
		// (set) Token: 0x06004CF6 RID: 19702 RVA: 0x00137D71 File Offset: 0x00136D71
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[WebSysDescription("LoginControls_SuccessPageUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06004CF7 RID: 19703 RVA: 0x00137D84 File Offset: 0x00136D84
		// (set) Token: 0x06004CF8 RID: 19704 RVA: 0x00137D8C File Offset: 0x00136D8C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
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

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x00137D9C File Offset: 0x00136D9C
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

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06004CFA RID: 19706 RVA: 0x00137DAC File Offset: 0x00136DAC
		// (set) Token: 0x06004CFB RID: 19707 RVA: 0x00137DDE File Offset: 0x00136DDE
		[WebCategory("Appearance")]
		[Localizable(true)]
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

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06004CFC RID: 19708 RVA: 0x00137DF1 File Offset: 0x00136DF1
		[DefaultValue(null)]
		[WebCategory("Styles")]
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

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06004CFD RID: 19709 RVA: 0x00137E1F File Offset: 0x00136E1F
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06004CFE RID: 19710 RVA: 0x00137E23 File Offset: 0x00136E23
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
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

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06004CFF RID: 19711 RVA: 0x00137E54 File Offset: 0x00136E54
		// (set) Token: 0x06004D00 RID: 19712 RVA: 0x00137E7D File Offset: 0x00136E7D
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

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06004D01 RID: 19713 RVA: 0x00137EAF File Offset: 0x00136EAF
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

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06004D02 RID: 19714 RVA: 0x00137EDD File Offset: 0x00136EDD
		// (set) Token: 0x06004D03 RID: 19715 RVA: 0x00137EF3 File Offset: 0x00136EF3
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

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06004D04 RID: 19716 RVA: 0x00137EFC File Offset: 0x00136EFC
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

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06004D05 RID: 19717 RVA: 0x00137F3C File Offset: 0x00136F3C
		// (set) Token: 0x06004D06 RID: 19718 RVA: 0x00137F6E File Offset: 0x00136F6E
		[WebCategory("Appearance")]
		[Localizable(true)]
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

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06004D07 RID: 19719 RVA: 0x00137F84 File Offset: 0x00136F84
		// (set) Token: 0x06004D08 RID: 19720 RVA: 0x00137FB6 File Offset: 0x00136FB6
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

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06004D09 RID: 19721 RVA: 0x00137FCC File Offset: 0x00136FCC
		// (set) Token: 0x06004D0A RID: 19722 RVA: 0x00137FFE File Offset: 0x00136FFE
		[WebSysDefaultValue("PasswordRecovery_DefaultUserNameLabelText")]
		[Localizable(true)]
		[WebCategory("Appearance")]
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

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06004D0B RID: 19723 RVA: 0x00138014 File Offset: 0x00137014
		// (set) Token: 0x06004D0C RID: 19724 RVA: 0x00138046 File Offset: 0x00137046
		[WebSysDefaultValue("PasswordRecovery_DefaultUserNameRequiredErrorMessage")]
		[Localizable(true)]
		[WebCategory("Validation")]
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

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06004D0D RID: 19725 RVA: 0x00138059 File Offset: 0x00137059
		// (set) Token: 0x06004D0E RID: 19726 RVA: 0x00138061 File Offset: 0x00137061
		[Browsable(false)]
		[TemplateContainer(typeof(PasswordRecovery))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06004D0F RID: 19727 RVA: 0x00138071 File Offset: 0x00137071
		[WebSysDescription("PasswordRecovery_UserNameTemplateContainer")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control UserNameTemplateContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._userNameContainer;
			}
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06004D10 RID: 19728 RVA: 0x00138080 File Offset: 0x00137080
		// (set) Token: 0x06004D11 RID: 19729 RVA: 0x001380B2 File Offset: 0x001370B2
		[Localizable(true)]
		[WebSysDescription("PasswordRecovery_UserNameTitleText")]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("PasswordRecovery_DefaultUserNameTitleText")]
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

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06004D12 RID: 19730 RVA: 0x001380C5 File Offset: 0x001370C5
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
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

		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x06004D13 RID: 19731 RVA: 0x001380F3 File Offset: 0x001370F3
		// (remove) Token: 0x06004D14 RID: 19732 RVA: 0x00138106 File Offset: 0x00137106
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

		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x06004D15 RID: 19733 RVA: 0x00138119 File Offset: 0x00137119
		// (remove) Token: 0x06004D16 RID: 19734 RVA: 0x0013812C File Offset: 0x0013712C
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

		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x06004D17 RID: 19735 RVA: 0x0013813F File Offset: 0x0013713F
		// (remove) Token: 0x06004D18 RID: 19736 RVA: 0x00138152 File Offset: 0x00137152
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

		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x06004D19 RID: 19737 RVA: 0x00138165 File Offset: 0x00137165
		// (remove) Token: 0x06004D1A RID: 19738 RVA: 0x00138178 File Offset: 0x00137178
		[WebSysDescription("CreateUserWizard_SendMailError")]
		[WebCategory("Action")]
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

		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x06004D1B RID: 19739 RVA: 0x0013818B File Offset: 0x0013718B
		// (remove) Token: 0x06004D1C RID: 19740 RVA: 0x0013819E File Offset: 0x0013719E
		[WebSysDescription("PasswordRecovery_VerifyingUser")]
		[WebCategory("Action")]
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

		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x06004D1D RID: 19741 RVA: 0x001381B1 File Offset: 0x001371B1
		// (remove) Token: 0x06004D1E RID: 19742 RVA: 0x001381C4 File Offset: 0x001371C4
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

		// Token: 0x06004D1F RID: 19743 RVA: 0x001381D7 File Offset: 0x001371D7
		private void AnswerTextChanged(object source, EventArgs e)
		{
			this._answer = ((ITextControl)source).Text;
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x001381EA File Offset: 0x001371EA
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

		// Token: 0x06004D21 RID: 19745 RVA: 0x00138220 File Offset: 0x00137220
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

		// Token: 0x06004D22 RID: 19746 RVA: 0x00138390 File Offset: 0x00137390
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

		// Token: 0x06004D23 RID: 19747 RVA: 0x00138502 File Offset: 0x00137502
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			this.CreateUserView();
			this.CreateQuestionView();
			this.CreateSuccessView();
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x00138524 File Offset: 0x00137524
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

		// Token: 0x06004D25 RID: 19749 RVA: 0x001385D4 File Offset: 0x001375D4
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

		// Token: 0x06004D26 RID: 19750 RVA: 0x00138660 File Offset: 0x00137660
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

		// Token: 0x06004D27 RID: 19751 RVA: 0x00138718 File Offset: 0x00137718
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

		// Token: 0x06004D28 RID: 19752 RVA: 0x00138778 File Offset: 0x00137778
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

		// Token: 0x06004D29 RID: 19753 RVA: 0x0013887C File Offset: 0x0013787C
		protected virtual void OnAnswerLookupError(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PasswordRecovery.EventAnswerLookupError];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x001388AC File Offset: 0x001378AC
		protected virtual void OnVerifyingAnswer(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[PasswordRecovery.EventVerifyingAnswer];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x001388DC File Offset: 0x001378DC
		protected virtual void OnSendingMail(MailMessageEventArgs e)
		{
			MailMessageEventHandler mailMessageEventHandler = (MailMessageEventHandler)base.Events[PasswordRecovery.EventSendingMail];
			if (mailMessageEventHandler != null)
			{
				mailMessageEventHandler(this, e);
			}
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x0013890C File Offset: 0x0013790C
		protected virtual void OnSendMailError(SendMailErrorEventArgs e)
		{
			SendMailErrorEventHandler sendMailErrorEventHandler = (SendMailErrorEventHandler)base.Events[PasswordRecovery.EventSendMailError];
			if (sendMailErrorEventHandler != null)
			{
				sendMailErrorEventHandler(this, e);
			}
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x0013893C File Offset: 0x0013793C
		protected virtual void OnVerifyingUser(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[PasswordRecovery.EventVerifyingUser];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x0013896C File Offset: 0x0013796C
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

		// Token: 0x06004D2F RID: 19759 RVA: 0x001389A6 File Offset: 0x001379A6
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.RegisterRequiresControlState(this);
			this.Page.LoadComplete += this.OnPageLoadComplete;
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x001389D4 File Offset: 0x001379D4
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

		// Token: 0x06004D31 RID: 19761 RVA: 0x00138A54 File Offset: 0x00137A54
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

		// Token: 0x06004D32 RID: 19762 RVA: 0x00138AD4 File Offset: 0x00137AD4
		protected virtual void OnUserLookupError(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PasswordRecovery.EventUserLookupError];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x00138B04 File Offset: 0x00137B04
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

		// Token: 0x06004D34 RID: 19764 RVA: 0x00138B40 File Offset: 0x00137B40
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

		// Token: 0x06004D35 RID: 19765 RVA: 0x00138C10 File Offset: 0x00137C10
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

		// Token: 0x06004D36 RID: 19766 RVA: 0x00138C74 File Offset: 0x00137C74
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

		// Token: 0x06004D37 RID: 19767 RVA: 0x00138DA4 File Offset: 0x00137DA4
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

		// Token: 0x06004D38 RID: 19768 RVA: 0x00138E08 File Offset: 0x00137E08
		private void SetFailureTextLabel(PasswordRecovery.QuestionContainer container, string failureText)
		{
			ITextControl textControl = (ITextControl)container.FailureTextLabel;
			if (textControl != null)
			{
				textControl.Text = failureText;
			}
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x00138E2C File Offset: 0x00137E2C
		private void SetFailureTextLabel(PasswordRecovery.UserNameContainer container, string failureText)
		{
			ITextControl textControl = (ITextControl)container.FailureTextLabel;
			if (textControl != null)
			{
				textControl.Text = failureText;
			}
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x00138E4F File Offset: 0x00137E4F
		internal void SetQuestionChildProperties()
		{
			this.SetQuestionCommonChildProperties();
			if (this.QuestionTemplate == null)
			{
				this.SetQuestionDefaultChildProperties();
			}
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x00138E68 File Offset: 0x00137E68
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

		// Token: 0x06004D3C RID: 19772 RVA: 0x00138EEC File Offset: 0x00137EEC
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

		// Token: 0x06004D3D RID: 19773 RVA: 0x00139380 File Offset: 0x00138380
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

		// Token: 0x06004D3E RID: 19774 RVA: 0x0013940D File Offset: 0x0013840D
		internal void SetUserNameChildProperties()
		{
			this.SetUserNameCommonChildProperties();
			if (this.UserNameTemplate == null)
			{
				this.SetUserNameDefaultChildProperties();
			}
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x00139423 File Offset: 0x00138423
		private void SetUserNameCommonChildProperties()
		{
			Util.CopyBaseAttributesToInnerControl(this, this._userNameContainer);
			this._userNameContainer.ApplyStyle(base.ControlStyle);
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x00139444 File Offset: 0x00138444
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

		// Token: 0x06004D41 RID: 19777 RVA: 0x001397EC File Offset: 0x001387EC
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

		// Token: 0x06004D42 RID: 19778 RVA: 0x00139824 File Offset: 0x00138824
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

		// Token: 0x06004D43 RID: 19779 RVA: 0x001398F8 File Offset: 0x001388F8
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

		// Token: 0x06004D44 RID: 19780 RVA: 0x0013997D File Offset: 0x0013897D
		private void UserNameTextChanged(object source, EventArgs e)
		{
			this.UserName = ((ITextControl)source).Text;
		}

		// Token: 0x04002C16 RID: 11286
		private const string _userNameID = "UserName";

		// Token: 0x04002C17 RID: 11287
		private const string _questionID = "Question";

		// Token: 0x04002C18 RID: 11288
		private const string _answerID = "Answer";

		// Token: 0x04002C19 RID: 11289
		private const string _failureTextID = "FailureText";

		// Token: 0x04002C1A RID: 11290
		private const string _userNameRequiredID = "UserNameRequired";

		// Token: 0x04002C1B RID: 11291
		private const string _answerRequiredID = "AnswerRequired";

		// Token: 0x04002C1C RID: 11292
		private const string _pushButtonID = "SubmitButton";

		// Token: 0x04002C1D RID: 11293
		private const string _imageButtonID = "SubmitImageButton";

		// Token: 0x04002C1E RID: 11294
		private const string _linkButtonID = "SubmitLinkButton";

		// Token: 0x04002C1F RID: 11295
		private const string _helpLinkID = "HelpLink";

		// Token: 0x04002C20 RID: 11296
		private const string _userNameContainerID = "UserNameContainerID";

		// Token: 0x04002C21 RID: 11297
		private const string _questionContainerID = "QuestionContainerID";

		// Token: 0x04002C22 RID: 11298
		private const string _successContainerID = "SuccessContainerID";

		// Token: 0x04002C23 RID: 11299
		private const ValidatorDisplay _requiredFieldValidatorDisplay = ValidatorDisplay.Static;

		// Token: 0x04002C24 RID: 11300
		private const string _userNameReplacementKey = "<%\\s*UserName\\s*%>";

		// Token: 0x04002C25 RID: 11301
		private const string _passwordReplacementKey = "<%\\s*Password\\s*%>";

		// Token: 0x04002C26 RID: 11302
		private const int _viewStateArrayLength = 11;

		// Token: 0x04002C27 RID: 11303
		public static readonly string SubmitButtonCommandName = "Submit";

		// Token: 0x04002C28 RID: 11304
		private string _answer;

		// Token: 0x04002C29 RID: 11305
		private PasswordRecovery.View _currentView;

		// Token: 0x04002C2A RID: 11306
		private string _question;

		// Token: 0x04002C2B RID: 11307
		private string _userName;

		// Token: 0x04002C2C RID: 11308
		private bool _convertingToTemplate;

		// Token: 0x04002C2D RID: 11309
		private bool _renderDesignerRegion;

		// Token: 0x04002C2E RID: 11310
		private ITemplate _userNameTemplate;

		// Token: 0x04002C2F RID: 11311
		private PasswordRecovery.UserNameContainer _userNameContainer;

		// Token: 0x04002C30 RID: 11312
		private ITemplate _questionTemplate;

		// Token: 0x04002C31 RID: 11313
		private PasswordRecovery.QuestionContainer _questionContainer;

		// Token: 0x04002C32 RID: 11314
		private ITemplate _successTemplate;

		// Token: 0x04002C33 RID: 11315
		private PasswordRecovery.SuccessContainer _successContainer;

		// Token: 0x04002C34 RID: 11316
		private Style _submitButtonStyle;

		// Token: 0x04002C35 RID: 11317
		private TableItemStyle _labelStyle;

		// Token: 0x04002C36 RID: 11318
		private Style _textBoxStyle;

		// Token: 0x04002C37 RID: 11319
		private TableItemStyle _hyperLinkStyle;

		// Token: 0x04002C38 RID: 11320
		private TableItemStyle _instructionTextStyle;

		// Token: 0x04002C39 RID: 11321
		private TableItemStyle _titleTextStyle;

		// Token: 0x04002C3A RID: 11322
		private TableItemStyle _failureTextStyle;

		// Token: 0x04002C3B RID: 11323
		private TableItemStyle _successTextStyle;

		// Token: 0x04002C3C RID: 11324
		private MailDefinition _mailDefinition;

		// Token: 0x04002C3D RID: 11325
		private Style _validatorTextStyle;

		// Token: 0x04002C3E RID: 11326
		private static readonly object EventVerifyingUser = new object();

		// Token: 0x04002C3F RID: 11327
		private static readonly object EventUserLookupError = new object();

		// Token: 0x04002C40 RID: 11328
		private static readonly object EventVerifyingAnswer = new object();

		// Token: 0x04002C41 RID: 11329
		private static readonly object EventAnswerLookupError = new object();

		// Token: 0x04002C42 RID: 11330
		private static readonly object EventSendMailError = new object();

		// Token: 0x04002C43 RID: 11331
		private static readonly object EventSendingMail = new object();

		// Token: 0x02000611 RID: 1553
		private sealed class DefaultQuestionTemplate : ITemplate
		{
			// Token: 0x06004D47 RID: 19783 RVA: 0x001399EB File Offset: 0x001389EB
			public DefaultQuestionTemplate(PasswordRecovery owner)
			{
				this._owner = owner;
			}

			// Token: 0x06004D48 RID: 19784 RVA: 0x001399FC File Offset: 0x001389FC
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

			// Token: 0x06004D49 RID: 19785 RVA: 0x00139BC8 File Offset: 0x00138BC8
			private void LayoutControls(PasswordRecovery.QuestionContainer questionContainer)
			{
				if (this._owner.TextLayout == LoginTextLayout.TextOnLeft)
				{
					this.LayoutTextOnLeft(questionContainer);
					return;
				}
				this.LayoutTextOnTop(questionContainer);
			}

			// Token: 0x06004D4A RID: 19786 RVA: 0x00139BF4 File Offset: 0x00138BF4
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

			// Token: 0x06004D4B RID: 19787 RVA: 0x00139F40 File Offset: 0x00138F40
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

			// Token: 0x06004D4C RID: 19788 RVA: 0x0013A290 File Offset: 0x00139290
			void ITemplate.InstantiateIn(Control container)
			{
				PasswordRecovery.QuestionContainer questionContainer = (PasswordRecovery.QuestionContainer)container;
				this.CreateControls(questionContainer);
				this.LayoutControls(questionContainer);
			}

			// Token: 0x04002C44 RID: 11332
			private PasswordRecovery _owner;
		}

		// Token: 0x02000612 RID: 1554
		private sealed class DefaultSuccessTemplate : ITemplate
		{
			// Token: 0x06004D4D RID: 19789 RVA: 0x0013A2B2 File Offset: 0x001392B2
			public DefaultSuccessTemplate(PasswordRecovery owner)
			{
				this._owner = owner;
			}

			// Token: 0x06004D4E RID: 19790 RVA: 0x0013A2C1 File Offset: 0x001392C1
			private void CreateControls(PasswordRecovery.SuccessContainer successContainer)
			{
				successContainer.SuccessTextLabel = new Literal();
			}

			// Token: 0x06004D4F RID: 19791 RVA: 0x0013A2D0 File Offset: 0x001392D0
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

			// Token: 0x06004D50 RID: 19792 RVA: 0x0013A380 File Offset: 0x00139380
			void ITemplate.InstantiateIn(Control container)
			{
				PasswordRecovery.SuccessContainer successContainer = (PasswordRecovery.SuccessContainer)container;
				this.CreateControls(successContainer);
				this.LayoutControls(successContainer);
			}

			// Token: 0x04002C45 RID: 11333
			private PasswordRecovery _owner;
		}

		// Token: 0x02000613 RID: 1555
		private sealed class DefaultUserNameTemplate : ITemplate
		{
			// Token: 0x06004D51 RID: 19793 RVA: 0x0013A3A2 File Offset: 0x001393A2
			public DefaultUserNameTemplate(PasswordRecovery owner)
			{
				this._owner = owner;
			}

			// Token: 0x06004D52 RID: 19794 RVA: 0x0013A3B4 File Offset: 0x001393B4
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

			// Token: 0x06004D53 RID: 19795 RVA: 0x0013A534 File Offset: 0x00139534
			private void LayoutControls(PasswordRecovery.UserNameContainer userNameContainer)
			{
				if (this._owner.TextLayout == LoginTextLayout.TextOnLeft)
				{
					this.LayoutTextOnLeft(userNameContainer);
					return;
				}
				this.LayoutTextOnTop(userNameContainer);
			}

			// Token: 0x06004D54 RID: 19796 RVA: 0x0013A560 File Offset: 0x00139560
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

			// Token: 0x06004D55 RID: 19797 RVA: 0x0013A7E8 File Offset: 0x001397E8
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

			// Token: 0x06004D56 RID: 19798 RVA: 0x0013AA5C File Offset: 0x00139A5C
			void ITemplate.InstantiateIn(Control container)
			{
				PasswordRecovery.UserNameContainer userNameContainer = (PasswordRecovery.UserNameContainer)container;
				this.CreateControls(userNameContainer);
				this.LayoutControls(userNameContainer);
			}

			// Token: 0x04002C46 RID: 11334
			private PasswordRecovery _owner;
		}

		// Token: 0x02000614 RID: 1556
		internal sealed class QuestionContainer : LoginUtil.GenericContainer<PasswordRecovery>, INonBindingContainer, INamingContainer
		{
			// Token: 0x06004D57 RID: 19799 RVA: 0x0013AA7E File Offset: 0x00139A7E
			public QuestionContainer(PasswordRecovery owner) : base(owner)
			{
			}

			// Token: 0x17001379 RID: 4985
			// (get) Token: 0x06004D58 RID: 19800 RVA: 0x0013AA87 File Offset: 0x00139A87
			// (set) Token: 0x06004D59 RID: 19801 RVA: 0x0013AA8F File Offset: 0x00139A8F
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

			// Token: 0x1700137A RID: 4986
			// (get) Token: 0x06004D5A RID: 19802 RVA: 0x0013AA98 File Offset: 0x00139A98
			// (set) Token: 0x06004D5B RID: 19803 RVA: 0x0013AAA0 File Offset: 0x00139AA0
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

			// Token: 0x1700137B RID: 4987
			// (get) Token: 0x06004D5C RID: 19804 RVA: 0x0013AAA9 File Offset: 0x00139AA9
			// (set) Token: 0x06004D5D RID: 19805 RVA: 0x0013AACA File Offset: 0x00139ACA
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

			// Token: 0x1700137C RID: 4988
			// (get) Token: 0x06004D5E RID: 19806 RVA: 0x0013AAD3 File Offset: 0x00139AD3
			protected override bool ConvertingToTemplate
			{
				get
				{
					return base.Owner.ConvertingToTemplate;
				}
			}

			// Token: 0x1700137D RID: 4989
			// (get) Token: 0x06004D5F RID: 19807 RVA: 0x0013AAE0 File Offset: 0x00139AE0
			// (set) Token: 0x06004D60 RID: 19808 RVA: 0x0013AAFC File Offset: 0x00139AFC
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

			// Token: 0x1700137E RID: 4990
			// (get) Token: 0x06004D61 RID: 19809 RVA: 0x0013AB05 File Offset: 0x00139B05
			// (set) Token: 0x06004D62 RID: 19810 RVA: 0x0013AB0D File Offset: 0x00139B0D
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

			// Token: 0x1700137F RID: 4991
			// (get) Token: 0x06004D63 RID: 19811 RVA: 0x0013AB16 File Offset: 0x00139B16
			// (set) Token: 0x06004D64 RID: 19812 RVA: 0x0013AB1E File Offset: 0x00139B1E
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

			// Token: 0x17001380 RID: 4992
			// (get) Token: 0x06004D65 RID: 19813 RVA: 0x0013AB27 File Offset: 0x00139B27
			// (set) Token: 0x06004D66 RID: 19814 RVA: 0x0013AB2F File Offset: 0x00139B2F
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

			// Token: 0x17001381 RID: 4993
			// (get) Token: 0x06004D67 RID: 19815 RVA: 0x0013AB38 File Offset: 0x00139B38
			// (set) Token: 0x06004D68 RID: 19816 RVA: 0x0013AB40 File Offset: 0x00139B40
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

			// Token: 0x17001382 RID: 4994
			// (get) Token: 0x06004D69 RID: 19817 RVA: 0x0013AB49 File Offset: 0x00139B49
			// (set) Token: 0x06004D6A RID: 19818 RVA: 0x0013AB51 File Offset: 0x00139B51
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

			// Token: 0x17001383 RID: 4995
			// (get) Token: 0x06004D6B RID: 19819 RVA: 0x0013AB5A File Offset: 0x00139B5A
			// (set) Token: 0x06004D6C RID: 19820 RVA: 0x0013AB62 File Offset: 0x00139B62
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

			// Token: 0x17001384 RID: 4996
			// (get) Token: 0x06004D6D RID: 19821 RVA: 0x0013AB6B File Offset: 0x00139B6B
			// (set) Token: 0x06004D6E RID: 19822 RVA: 0x0013AB87 File Offset: 0x00139B87
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

			// Token: 0x17001385 RID: 4997
			// (get) Token: 0x06004D6F RID: 19823 RVA: 0x0013AB90 File Offset: 0x00139B90
			// (set) Token: 0x06004D70 RID: 19824 RVA: 0x0013AB98 File Offset: 0x00139B98
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

			// Token: 0x17001386 RID: 4998
			// (get) Token: 0x06004D71 RID: 19825 RVA: 0x0013ABA1 File Offset: 0x00139BA1
			// (set) Token: 0x06004D72 RID: 19826 RVA: 0x0013ABA9 File Offset: 0x00139BA9
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

			// Token: 0x17001387 RID: 4999
			// (get) Token: 0x06004D73 RID: 19827 RVA: 0x0013ABB2 File Offset: 0x00139BB2
			// (set) Token: 0x06004D74 RID: 19828 RVA: 0x0013ABCE File Offset: 0x00139BCE
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

			// Token: 0x17001388 RID: 5000
			// (get) Token: 0x06004D75 RID: 19829 RVA: 0x0013ABD7 File Offset: 0x00139BD7
			// (set) Token: 0x06004D76 RID: 19830 RVA: 0x0013ABDF File Offset: 0x00139BDF
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

			// Token: 0x04002C47 RID: 11335
			private LabelLiteral _answerLabel;

			// Token: 0x04002C48 RID: 11336
			private RequiredFieldValidator _answerRequired;

			// Token: 0x04002C49 RID: 11337
			private Control _answerTextBox;

			// Token: 0x04002C4A RID: 11338
			private Control _failureTextLabel;

			// Token: 0x04002C4B RID: 11339
			private HyperLink _helpPageLink;

			// Token: 0x04002C4C RID: 11340
			private Image _helpPageIcon;

			// Token: 0x04002C4D RID: 11341
			private ImageButton _imageButton;

			// Token: 0x04002C4E RID: 11342
			private Literal _instruction;

			// Token: 0x04002C4F RID: 11343
			private LinkButton _linkButton;

			// Token: 0x04002C50 RID: 11344
			private Button _pushButton;

			// Token: 0x04002C51 RID: 11345
			private Control _question;

			// Token: 0x04002C52 RID: 11346
			private Literal _questionLabel;

			// Token: 0x04002C53 RID: 11347
			private Literal _title;

			// Token: 0x04002C54 RID: 11348
			private Literal _userNameLabel;

			// Token: 0x04002C55 RID: 11349
			private Control _userName;
		}

		// Token: 0x02000615 RID: 1557
		internal sealed class SuccessContainer : LoginUtil.GenericContainer<PasswordRecovery>, INonBindingContainer, INamingContainer
		{
			// Token: 0x06004D77 RID: 19831 RVA: 0x0013ABE8 File Offset: 0x00139BE8
			public SuccessContainer(PasswordRecovery owner) : base(owner)
			{
			}

			// Token: 0x17001389 RID: 5001
			// (get) Token: 0x06004D78 RID: 19832 RVA: 0x0013ABF1 File Offset: 0x00139BF1
			protected override bool ConvertingToTemplate
			{
				get
				{
					return base.Owner.ConvertingToTemplate;
				}
			}

			// Token: 0x1700138A RID: 5002
			// (get) Token: 0x06004D79 RID: 19833 RVA: 0x0013ABFE File Offset: 0x00139BFE
			// (set) Token: 0x06004D7A RID: 19834 RVA: 0x0013AC06 File Offset: 0x00139C06
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

			// Token: 0x04002C56 RID: 11350
			private Literal _successTextLabel;
		}

		// Token: 0x02000616 RID: 1558
		internal sealed class UserNameContainer : LoginUtil.GenericContainer<PasswordRecovery>, INonBindingContainer, INamingContainer
		{
			// Token: 0x06004D7B RID: 19835 RVA: 0x0013AC0F File Offset: 0x00139C0F
			public UserNameContainer(PasswordRecovery owner) : base(owner)
			{
			}

			// Token: 0x1700138B RID: 5003
			// (get) Token: 0x06004D7C RID: 19836 RVA: 0x0013AC18 File Offset: 0x00139C18
			protected override bool ConvertingToTemplate
			{
				get
				{
					return base.Owner.ConvertingToTemplate;
				}
			}

			// Token: 0x1700138C RID: 5004
			// (get) Token: 0x06004D7D RID: 19837 RVA: 0x0013AC25 File Offset: 0x00139C25
			// (set) Token: 0x06004D7E RID: 19838 RVA: 0x0013AC41 File Offset: 0x00139C41
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

			// Token: 0x1700138D RID: 5005
			// (get) Token: 0x06004D7F RID: 19839 RVA: 0x0013AC4A File Offset: 0x00139C4A
			// (set) Token: 0x06004D80 RID: 19840 RVA: 0x0013AC52 File Offset: 0x00139C52
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

			// Token: 0x1700138E RID: 5006
			// (get) Token: 0x06004D81 RID: 19841 RVA: 0x0013AC5B File Offset: 0x00139C5B
			// (set) Token: 0x06004D82 RID: 19842 RVA: 0x0013AC63 File Offset: 0x00139C63
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

			// Token: 0x1700138F RID: 5007
			// (get) Token: 0x06004D83 RID: 19843 RVA: 0x0013AC6C File Offset: 0x00139C6C
			// (set) Token: 0x06004D84 RID: 19844 RVA: 0x0013AC74 File Offset: 0x00139C74
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

			// Token: 0x17001390 RID: 5008
			// (get) Token: 0x06004D85 RID: 19845 RVA: 0x0013AC7D File Offset: 0x00139C7D
			// (set) Token: 0x06004D86 RID: 19846 RVA: 0x0013AC85 File Offset: 0x00139C85
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

			// Token: 0x17001391 RID: 5009
			// (get) Token: 0x06004D87 RID: 19847 RVA: 0x0013AC8E File Offset: 0x00139C8E
			// (set) Token: 0x06004D88 RID: 19848 RVA: 0x0013AC96 File Offset: 0x00139C96
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

			// Token: 0x17001392 RID: 5010
			// (get) Token: 0x06004D89 RID: 19849 RVA: 0x0013AC9F File Offset: 0x00139C9F
			// (set) Token: 0x06004D8A RID: 19850 RVA: 0x0013ACA7 File Offset: 0x00139CA7
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

			// Token: 0x17001393 RID: 5011
			// (get) Token: 0x06004D8B RID: 19851 RVA: 0x0013ACB0 File Offset: 0x00139CB0
			// (set) Token: 0x06004D8C RID: 19852 RVA: 0x0013ACB8 File Offset: 0x00139CB8
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

			// Token: 0x17001394 RID: 5012
			// (get) Token: 0x06004D8D RID: 19853 RVA: 0x0013ACC1 File Offset: 0x00139CC1
			// (set) Token: 0x06004D8E RID: 19854 RVA: 0x0013ACC9 File Offset: 0x00139CC9
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

			// Token: 0x17001395 RID: 5013
			// (get) Token: 0x06004D8F RID: 19855 RVA: 0x0013ACD2 File Offset: 0x00139CD2
			// (set) Token: 0x06004D90 RID: 19856 RVA: 0x0013ACDA File Offset: 0x00139CDA
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

			// Token: 0x17001396 RID: 5014
			// (get) Token: 0x06004D91 RID: 19857 RVA: 0x0013ACE3 File Offset: 0x00139CE3
			// (set) Token: 0x06004D92 RID: 19858 RVA: 0x0013AD04 File Offset: 0x00139D04
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

			// Token: 0x04002C57 RID: 11351
			private Control _failureTextLabel;

			// Token: 0x04002C58 RID: 11352
			private Image _helpPageIcon;

			// Token: 0x04002C59 RID: 11353
			private HyperLink _helpPageLink;

			// Token: 0x04002C5A RID: 11354
			private ImageButton _imageButton;

			// Token: 0x04002C5B RID: 11355
			private Literal _instruction;

			// Token: 0x04002C5C RID: 11356
			private LinkButton _linkButton;

			// Token: 0x04002C5D RID: 11357
			private Button _pushButton;

			// Token: 0x04002C5E RID: 11358
			private Literal _title;

			// Token: 0x04002C5F RID: 11359
			private LabelLiteral _userNameLabel;

			// Token: 0x04002C60 RID: 11360
			private RequiredFieldValidator _userNameRequired;

			// Token: 0x04002C61 RID: 11361
			private Control _userNameTextBox;
		}

		// Token: 0x02000617 RID: 1559
		internal enum View
		{
			// Token: 0x04002C63 RID: 11363
			UserName,
			// Token: 0x04002C64 RID: 11364
			Question,
			// Token: 0x04002C65 RID: 11365
			Success
		}
	}
}
