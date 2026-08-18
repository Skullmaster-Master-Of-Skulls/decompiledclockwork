using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004F1 RID: 1265
	public class TreeViewBindingsEditor : UITypeEditor
	{
		// Token: 0x06002D33 RID: 11571 RVA: 0x000FF92C File Offset: 0x000FE92C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IDesignerHost designerHost = (IDesignerHost)context.GetService(typeof(IDesignerHost));
			TreeView component = (TreeView)context.Instance;
			TreeViewDesigner treeViewDesigner = (TreeViewDesigner)designerHost.GetDesigner(component);
			treeViewDesigner.InvokeTreeViewBindingsEditor();
			return value;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000FF96F File Offset: 0x000FE96F
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
