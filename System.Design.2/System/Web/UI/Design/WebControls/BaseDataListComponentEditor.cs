using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200009E RID: 158
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public abstract class BaseDataListComponentEditor : WindowsFormsComponentEditor
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00015F76 File Offset: 0x00014176
		public BaseDataListComponentEditor(int initialPage)
		{
			this.initialPage = initialPage;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00015F88 File Offset: 0x00014188
		public override bool EditComponent(ITypeDescriptorContext context, object obj, IWin32Window parent)
		{
			bool result = false;
			bool flag = false;
			IComponent component = (IComponent)obj;
			ISite site = component.Site;
			if (site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
				IDesigner designer = designerHost.GetDesigner(component);
				TemplatedControlDesigner templatedControlDesigner = (TemplatedControlDesigner)designer;
				flag = templatedControlDesigner.InTemplateModeInternal;
			}
			if (!flag)
			{
				Type[] componentEditorPages = this.GetComponentEditorPages();
				if (componentEditorPages != null && componentEditorPages.Length != 0)
				{
					ComponentEditorForm componentEditorForm = new ComponentEditorForm(obj, componentEditorPages);
					string @string = SR.GetString("RTL");
					if (!string.Equals(@string, "RTL_False", StringComparison.Ordinal))
					{
						componentEditorForm.RightToLeft = RightToLeft.Yes;
						componentEditorForm.RightToLeftLayout = true;
					}
					if (componentEditorForm.ShowForm(parent, this.GetInitialComponentEditorPageIndex()) == DialogResult.OK)
					{
						result = true;
					}
				}
			}
			else
			{
				RTLAwareMessageBox.Show(null, SR.GetString("BDL_TemplateModePropBuilder"), SR.GetString("BDL_PropertyBuilder"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
			}
			return result;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001605B File Offset: 0x0001425B
		protected override int GetInitialComponentEditorPageIndex()
		{
			return this.initialPage;
		}

		// Token: 0x04000215 RID: 533
		private int initialPage;
	}
}
