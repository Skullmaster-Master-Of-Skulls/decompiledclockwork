using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200039C RID: 924
	[Browsable(false)]
	public sealed class CompleteWizardStep : TemplatedWizardStep
	{
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x00090683 File Offset: 0x0008E883
		// (set) Token: 0x06002C37 RID: 11319 RVA: 0x0009068B File Offset: 0x0008E88B
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
				throw new HttpException(SR.GetString("CompleteWizardStep_OnlyAllowedInCreateUserWizard"));
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x000097B7 File Offset: 0x000079B7
		// (set) Token: 0x06002C39 RID: 11321 RVA: 0x000906AF File Offset: 0x0008E8AF
		[Browsable(false)]
		[Themeable(false)]
		[Filterable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override WizardStepType StepType
		{
			get
			{
				return WizardStepType.Complete;
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("CreateUserWizardStep_StepTypeCannotBeSet"));
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x000906C0 File Offset: 0x0008E8C0
		// (set) Token: 0x06002C3B RID: 11323 RVA: 0x000906E3 File Offset: 0x0008E8E3
		[Localizable(true)]
		[WebSysDefaultValue("CreateUserWizard_DefaultCompleteTitleText")]
		public override string Title
		{
			get
			{
				string titleInternal = base.TitleInternal;
				if (titleInternal == null)
				{
					return SR.GetString("CreateUserWizard_DefaultCompleteTitleText");
				}
				return titleInternal;
			}
			set
			{
				base.Title = value;
			}
		}
	}
}
