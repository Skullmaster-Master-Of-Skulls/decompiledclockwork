using System;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000B1 RID: 177
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class CreateUserWizardStepCollectionEditor : WizardStepCollectionEditor
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x0001C156 File Offset: 0x0001A356
		public CreateUserWizardStepCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001C15F File Offset: 0x0001A35F
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.CreateUserWizard.StepCollectionEditor";
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001C166 File Offset: 0x0001A366
		protected override bool CanRemoveInstance(object value)
		{
			return !(value is CompleteWizardStep) && !(value is CreateUserWizardStep);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001C180 File Offset: 0x0001A380
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
			collectionForm.Text = SR.GetString("CreateUserWizardStepCollectionEditor_Caption");
			return collectionForm;
		}
	}
}
