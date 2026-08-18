using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200012B RID: 299
	public class TreeViewBindingsEditor : UITypeEditor
	{
		// Token: 0x06000ABE RID: 2750 RVA: 0x000444AC File Offset: 0x000426AC
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IDesignerHost designerHost = (IDesignerHost)context.GetService(typeof(IDesignerHost));
			TreeView component = (TreeView)context.Instance;
			TreeViewDesigner treeViewDesigner = (TreeViewDesigner)designerHost.GetDesigner(component);
			treeViewDesigner.InvokeTreeViewBindingsEditor();
			return value;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
