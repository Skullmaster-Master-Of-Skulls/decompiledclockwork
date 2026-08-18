using System;
using System.ComponentModel.Design;
using System.Design;
using System.Reflection;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200013A RID: 314
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class WizardStepCollectionEditor : CollectionEditor
	{
		// Token: 0x06000B51 RID: 2897 RVA: 0x00023ABB File Offset: 0x00021CBB
		public WizardStepCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000496B0 File Offset: 0x000478B0
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
			collectionForm.Text = SR.GetString("CollectionEditorCaption", new object[]
			{
				"WizardStep"
			});
			return collectionForm;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002CBAB File Offset: 0x0002ADAB
		protected override object CreateInstance(Type itemType)
		{
			return Activator.CreateInstance(itemType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x000496E3 File Offset: 0x000478E3
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[]
			{
				typeof(WizardStep),
				typeof(TemplatedWizardStep)
			};
		}
	}
}
