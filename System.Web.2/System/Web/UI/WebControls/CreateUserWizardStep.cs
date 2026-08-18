using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003AB RID: 939
	[Browsable(false)]
	public sealed class CreateUserWizardStep : TemplatedWizardStep
	{
		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x000945B7 File Offset: 0x000927B7
		// (set) Token: 0x06002D54 RID: 11604 RVA: 0x000945BF File Offset: 0x000927BF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AllowReturn
		{
			get
			{
				return this.AllowReturnInternal;
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("CreateUserWizardStep_AllowReturnCannotBeSet"));
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06002D55 RID: 11605 RVA: 0x000945D0 File Offset: 0x000927D0
		// (set) Token: 0x06002D56 RID: 11606 RVA: 0x000945F9 File Offset: 0x000927F9
		internal bool AllowReturnInternal
		{
			get
			{
				object obj = this.ViewState["AllowReturnInternal"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AllowReturnInternal"] = value;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x00090683 File Offset: 0x0008E883
		// (set) Token: 0x06002D58 RID: 11608 RVA: 0x00094611 File Offset: 0x00092811
		internal override Wizard Owner
		{
			get
			{
				return base.Owner;
			}
			set
			{
				if (value is CreateUserWizard || value == null)
				{
					base.Owner = value;
					return;
				}
				throw new HttpException(SR.GetString("CreateUserWizardStep_OnlyAllowedInCreateUserWizard"));
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x00094638 File Offset: 0x00092838
		// (set) Token: 0x06002D5A RID: 11610 RVA: 0x000906E3 File Offset: 0x0008E8E3
		[Localizable(true)]
		[WebSysDefaultValue("CreateUserWizard_DefaultCreateUserTitleText")]
		public override string Title
		{
			get
			{
				string titleInternal = base.TitleInternal;
				if (titleInternal == null)
				{
					return SR.GetString("CreateUserWizard_DefaultCreateUserTitleText");
				}
				return titleInternal;
			}
			set
			{
				base.Title = value;
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x0009465B File Offset: 0x0009285B
		// (set) Token: 0x06002D5C RID: 11612 RVA: 0x000906AF File Offset: 0x0008E8AF
		[Browsable(false)]
		[Themeable(false)]
		[Filterable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override WizardStepType StepType
		{
			get
			{
				return base.StepType;
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("CreateUserWizardStep_StepTypeCannotBeSet"));
			}
		}
	}
}
