using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000490 RID: 1168
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class WindowsFormsComponentEditor : ComponentEditor
	{
		// Token: 0x06004E49 RID: 20041 RVA: 0x00142AC1 File Offset: 0x00140CC1
		public override bool EditComponent(ITypeDescriptorContext context, object component)
		{
			return this.EditComponent(context, component, null);
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x00142ACC File Offset: 0x00140CCC
		public bool EditComponent(object component, IWin32Window owner)
		{
			return this.EditComponent(null, component, owner);
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x00142AD8 File Offset: 0x00140CD8
		public virtual bool EditComponent(ITypeDescriptorContext context, object component, IWin32Window owner)
		{
			bool result = false;
			Type[] componentEditorPages = this.GetComponentEditorPages();
			if (componentEditorPages != null && componentEditorPages.Length != 0)
			{
				ComponentEditorForm componentEditorForm = new ComponentEditorForm(component, componentEditorPages);
				if (componentEditorForm.ShowForm(owner, this.GetInitialComponentEditorPageIndex()) == DialogResult.OK)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x00015ECC File Offset: 0x000140CC
		protected virtual Type[] GetComponentEditorPages()
		{
			return null;
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual int GetInitialComponentEditorPageIndex()
		{
			return 0;
		}
	}
}
